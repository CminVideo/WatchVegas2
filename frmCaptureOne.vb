Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Threading

Public Class frmCaptureOne
    Inherits Form

    Private WithEvents tmrCapture As New System.Windows.Forms.Timer()
    Private buffer As New Queue(Of Bitmap)()
    Private ReadOnly MaxFrames As Integer = 10
    Private isSaving As Boolean = False

    Public Event SilentCaptureCompleted()

    ' --- NEW: Centralized Path Method ---
    Public Function GetScreenshotPath() As String
        ' 1. Use IO.Path.Combine (the 'IO' is required)
        ' 2. Assign the value immediately to prevent the "unassigned" warning
        Dim path As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WATCHVegasScreenshots")

        ' 3. Ensure the folder exists
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If

        Return path
    End Function

    ' --- UPDATED: Property now uses the new path ---
    Private ReadOnly Property SavePath As String
        Get
            Return GetScreenshotPath()
        End Get
    End Property

    Public Sub StartSilentCapture()
        If tmrCapture.Enabled Then Return
        CaptureScreen()
        CaptureScreen()
        CaptureScreen()
        lblStatus.Text = "Status: Recording..."
        tmrCapture.Interval = 2000
        tmrCapture.Start()
    End Sub

    Public Sub StopSilentCapture()
        If Not tmrCapture.Enabled Then Return
        tmrCapture.Stop()
        RaiseEvent SilentCaptureCompleted()
    End Sub

    Public Function CountImagesInCaptureOneFolder() As Integer
        Dim folderPath As String = GetScreenshotPath()
        If Not Directory.Exists(folderPath) Then
            Return 0
        End If
        Dim jpgFiles = Directory.GetFiles(folderPath, "*.jpg")
        Dim jpegFiles = Directory.GetFiles(folderPath, "*.jpeg")
        Dim pngFiles = Directory.GetFiles(folderPath, "*.png")
        Return jpgFiles.Length + jpegFiles.Length + pngFiles.Length
    End Function

    Private Sub EvaluateStatusLimits()
        ' The counter is inside the status checker now
        Dim folderPath As String = GetScreenshotPath()
        Dim count As Integer = 0

        If Directory.Exists(folderPath) Then
            count = Directory.GetFiles(folderPath, "*.jpg").Length +
                Directory.GetFiles(folderPath, "*.jpeg").Length +
                Directory.GetFiles(folderPath, "*.png").Length
        End If

        ' Apply color and text
        lblStatus.Invoke(Sub()
                             If count >= MaxFrames Then
                                 lblStatus.ForeColor = Color.Orange
                                 lblStatus.Text = "LIMIT REACHED: " & count & "/" & MaxFrames
                             Else
                                 lblStatus.ForeColor = Color.Black
                                 lblStatus.Text = "Captured " & count & " screenshots."
                             End If
                         End Sub)
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.StartPosition = FormStartPosition.CenterScreen
        tmrCapture.Interval = 2000
    End Sub

    Private Sub btnStartCap_Click(sender As Object, e As EventArgs) Handles btnStartCap.Click
        Me.Opacity = 0
        frmMain2.Opacity = 0
        If Not tmrCapture.Enabled Then
            tmrCapture.Start()
            EvaluateStatusLimits()
        End If
    End Sub

    Private Sub btnStopCap_Click(sender As Object, e As EventArgs) Handles btnStopCap.Click
        If tmrCapture.Enabled Then
            tmrCapture.Stop()
            SaveBufferedScreenshots()
            ' Just call the checker, it handles the counting AND the status label
            EvaluateStatusLimits()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        frmMain2.Opacity = 100
        Timer1.Enabled = False
        Me.Close()
    End Sub

    Private Sub btnPurgeCap_Click(sender As Object, e As EventArgs) Handles btnPurgeCap.Click
        PurgeScreenshotFolder()
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        OpenScreenshotFolder()
    End Sub

    Private Sub tmrCapture_Tick(sender As Object, e As EventArgs) Handles tmrCapture.Tick
        CaptureScreen()
    End Sub

    Public Sub CaptureScreen()
        If isSaving Then Return

        Try
            Me.Opacity = 0
            Application.DoEvents()
            Thread.Sleep(300)

            Dim bounds = Screen.PrimaryScreen.Bounds

            ' --- 1st Using Block START ---
            Using bmp As New Bitmap(bounds.Width, bounds.Height)

                ' --- 2nd Using Block START ---
                Using g = Graphics.FromImage(bmp)
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size)
                End Using
                ' --- 2nd Using Block END ---

                If buffer.Count >= MaxFrames Then
                    Dim oldBmp = buffer.Dequeue()
                    oldBmp.Dispose()
                End If
                buffer.Enqueue(CType(bmp.Clone(), Bitmap))
            End Using
            ' --- 1st Using Block END ---

        Catch ex As Exception
            MessageBox.Show("Error capturing screen: " & ex.Message)
        Finally
            Me.Opacity = 1
            Me.Activate()
            EvaluateStatusLimits() ' This now handles your status and color updates
        End Try
    End Sub

    Public Sub SaveBufferedScreenshots()
        Dim savePath As String = GetScreenshotPath()
        Try
            isSaving = True
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim i As Integer = 0
            For Each bmp In buffer
                Dim filename = Path.Combine(savePath, $"Capture_{timestamp}_{i}.jpg")
                bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg)
                i += 1
            Next
            buffer.Clear()
            UpdateStatus($"Saved {i} screenshots to {savePath}")
        Catch ex As Exception
            MessageBox.Show("Critical Save Error: " & ex.Message)
        Finally
            isSaving = False
        End Try
    End Sub

    Public Sub PurgeScreenshotFolder()
        Dim path As String = GetScreenshotPath()
        Dim result = MessageBox.Show("Are you sure you want to delete all screenshots?", "Purge Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result <> DialogResult.Yes Then Return
        Try
            While buffer.Count > 0
                Dim bmp = buffer.Dequeue()
                bmp.Dispose()
            End While
            Dim files = Directory.GetFiles(path)
            For Each filePath As String In files
                File.Delete(filePath)
            Next
            UpdateStatus("Purge complete.")
        Catch ex As Exception
            MessageBox.Show("Error purging files: " & ex.Message)
        End Try
    End Sub

    Public Sub SaveSilentCapture()
        tmrCapture.Stop()
        SaveBufferedScreenshots()
    End Sub

    Private Sub OpenScreenshotFolder()
        Try
            Dim path As String = GetScreenshotPath()
            Process.Start("explorer.exe", """" & path & """")
        Catch ex As Exception
            MessageBox.Show("Failed to open folder: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateStatus(text As String)
        If lblStatus.InvokeRequired Then
            lblStatus.Invoke(Sub() lblStatus.Text = text)
        Else
            lblStatus.Text = text
        End If
    End Sub

    Private Sub frmCaptureOne_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        lblCountImages.Text = CountImagesInCaptureOneFolder().ToString()
    End Sub

    ' Ensure this is in your Timer1_Tick
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Use a Try-Catch here to ensure that if something hangs, 
        ' the timer doesn't just die silently
        Try
            EvaluateStatusLimits()
        Catch ex As Exception
            ' Log silently so it doesn't interrupt you
            Debug.WriteLine("Timer tick failed: " & ex.Message)
        End Try
    End Sub
End Class