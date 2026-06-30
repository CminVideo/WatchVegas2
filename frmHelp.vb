

Public Class frmHelp

    Private WithEvents browser As New WebBrowser()

    Private Sub frmHelp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        browser.Dock = DockStyle.Fill
        Me.Controls.Add(browser)
        btnClose.BringToFront() ' ensure close button is visible on top

        ' Handle net8.0-windows output folder path
        Dim basePath As String = Application.StartupPath
        If basePath.EndsWith("net8.0-windows", StringComparison.OrdinalIgnoreCase) Then
            basePath = IO.Path.GetDirectoryName(basePath) ' go up one folder
        End If

        Dim helpPath As String = IO.Path.Combine(basePath, "Help", "help.html")
        If IO.File.Exists(helpPath) Then
            browser.Navigate(New Uri(helpPath))
        Else
            MessageBox.Show("Help file not found: " & helpPath, "Help", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class

