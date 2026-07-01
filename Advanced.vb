Public Class Advanced

    Private Sub Advanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load checkbox states from settings
        chkEnableCpuWarning.Checked = My.Settings.EnableThermalWarning
        chkEnableGpuWarning.Checked = My.Settings.GpuWarningEnabled
        chkLogPlugins.Checked = My.Settings.LogWatchedPlugins
        chkEnableAutoScreenshot.Checked = My.Settings.AutoScreenshot
        chkTriggerScreenshot.Checked = My.Settings.TriggerScreenshotOnPlugin
        chkHighlightPlugins.Checked = My.Settings.HighlightWatchedPlugins

        ' Load the watched plugins list
        LoadWatchedPlugins()

        ' Load the currently loaded modules from Vegas (if running)
        LoadModules()
    End Sub

    Private Sub LoadWatchedPlugins()
        lstWatchedPlugins.Items.Clear()
        Dim list = My.Settings.WatchedPlugins
        If list IsNot Nothing Then
            For Each item As String In list
                If Not String.IsNullOrWhiteSpace(item) Then
                    lstWatchedPlugins.Items.Add(item)
                End If
            Next
        End If
    End Sub

    Private Sub LoadModules()
        lstLoadedModules.Items.Clear()
        Try
            Dim procs = Process.GetProcessesByName("vegas2026")
            If procs.Length > 0 Then
                Dim p = procs(0)
                For Each m As ProcessModule In p.Modules
                    lstLoadedModules.Items.Add(m.ModuleName)
                Next
            End If
        Catch
            ' Vegas not running or modules not accessible - leave list empty
        End Try
    End Sub

    ' --- Checkbox events - update settings live as user ticks/unticks ---

    Private Sub chkEnableCpuWarning_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableCpuWarning.CheckedChanged
        My.Settings.EnableThermalWarning = chkEnableCpuWarning.Checked
    End Sub

    Private Sub chkEnableGpuWarning_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableGpuWarning.CheckedChanged
        My.Settings.GpuWarningEnabled = chkEnableGpuWarning.Checked
    End Sub

    Private Sub chkLogPlugins_CheckedChanged(sender As Object, e As EventArgs) Handles chkLogPlugins.CheckedChanged
        My.Settings.LogWatchedPlugins = chkLogPlugins.Checked
    End Sub

    Private Sub chkEnableAutoScreenshot_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableAutoScreenshot.CheckedChanged
        My.Settings.AutoScreenshot = chkEnableAutoScreenshot.Checked
    End Sub

    Private Sub chkTriggerScreenshot_CheckedChanged(sender As Object, e As EventArgs) Handles chkTriggerScreenshot.CheckedChanged
        My.Settings.TriggerScreenshotOnPlugin = chkTriggerScreenshot.Checked
    End Sub

    Private Sub chkHighlightPlugins_CheckedChanged(sender As Object, e As EventArgs) Handles chkHighlightPlugins.CheckedChanged
        My.Settings.HighlightWatchedPlugins = chkHighlightPlugins.Checked
    End Sub

    ' --- Module search/filter ---

    Private Sub txtFilterModules_TextChanged(sender As Object, e As EventArgs) Handles txtFilterModules.TextChanged
        Dim searchTerm As String = txtFilterModules.Text.Trim().ToUpper()
        lstLoadedModules.Items.Clear()

        Try
            Dim procs = Process.GetProcessesByName("vegas2026")
            If procs.Length > 0 Then
                Dim p = procs(0)
                For Each m As ProcessModule In p.Modules
                    If String.IsNullOrEmpty(searchTerm) OrElse
                       m.ModuleName.ToUpper().Contains(searchTerm) Then
                        lstLoadedModules.Items.Add(m.ModuleName)
                    End If
                Next
            End If
        Catch
        End Try
    End Sub

    ' --- Click a module in the left list to add it to the watch list ---

    Private Sub lstLoadedModules_Click(sender As Object, e As EventArgs) Handles lstLoadedModules.Click
        If lstLoadedModules.SelectedItem Is Nothing Then Return

        Dim selected As String = lstLoadedModules.SelectedItem.ToString()

        ' Don't add duplicates
        If Not lstWatchedPlugins.Items.Contains(selected) Then
            lstWatchedPlugins.Items.Add(selected)
        End If
    End Sub

    ' --- Remove selected item from watch list ---

    Private Sub btnRemovePlugin_Click(sender As Object, e As EventArgs) Handles btnRemovePlugin.Click
        If lstWatchedPlugins.SelectedItem IsNot Nothing Then
            lstWatchedPlugins.Items.Remove(lstWatchedPlugins.SelectedItem)
        End If
    End Sub

    ' --- Clear the entire watch list ---

    Private Sub btnClearPlugins_Click(sender As Object, e As EventArgs) Handles btnClearPlugins.Click
        If MessageBox.Show("Clear all watched plugins?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            lstWatchedPlugins.Items.Clear()
        End If
    End Sub

    ' --- Save / Close ---

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        SaveWatchedPlugins()
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub SaveWatchedPlugins()
        If My.Settings.WatchedPlugins Is Nothing Then
            My.Settings.WatchedPlugins = New System.Collections.Specialized.StringCollection()
        End If
        My.Settings.WatchedPlugins.Clear()
        For Each item As Object In lstWatchedPlugins.Items
            My.Settings.WatchedPlugins.Add(item.ToString())
        Next
    End Sub

    ' --- Test button (hidden by default in Designer) ---

    Private Sub BtnTest_Click(sender As Object, e As EventArgs) Handles BtnTest.Click
        MessageBox.Show("Test button clicked.", "Test", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
