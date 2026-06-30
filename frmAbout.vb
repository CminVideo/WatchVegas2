Imports System.Windows.Forms
Imports System.Reflection

Public Class frmAbout

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "About WatchVegas2"
        lblAppName.Text = "WatchVegas (free and always will be)"

        Dim versionInfo As Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        lblVersion.Text = "Version " & versionInfo.Major & "." & versionInfo.Minor & "." & versionInfo.Build
        lblCopyright.Text = "Copyright © 2026 Christopher J Minchin"

        ' --- Updated Loading Logic ---
        Dim filePath As String = System.IO.Path.Combine(Application.StartupPath, "changelog.txt")

        If System.IO.File.Exists(filePath) Then
            ' Read the text and assign it directly to the .Text property
            RTBHistory.Text = System.IO.File.ReadAllText(filePath)
        Else
            ' This helps us know if it's looking in the wrong folder
            RTBHistory.Text = "Error: File not found at " & filePath
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class