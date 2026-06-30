<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Advanced
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Advanced))
        chkEnableCpuWarning = New CheckBox()
        chkEnableGpuWarning = New CheckBox()
        chkLogPlugins = New CheckBox()
        chkEnableAutoScreenshot = New CheckBox()
        chkTriggerScreenshot = New CheckBox()
        chkHighlightPlugins = New CheckBox()
        lstWatchedPlugins = New ListBox()
        BtnClose = New Button()
        BtnTest = New Button()
        txtNewPlugin = New TextBox()
        btnAddPlugin = New Button()
        btnRemovePlugin = New Button()
        btnClearPlugins = New Button()
        lstLoadedModules = New ListBox()
        btnAddModuleToWatchList = New Button()
        txtFilterModules = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        SuspendLayout()
        ' 
        ' chkEnableCpuWarning
        ' 
        chkEnableCpuWarning.AutoSize = True
        chkEnableCpuWarning.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        chkEnableCpuWarning.Location = New Point(28, 35)
        chkEnableCpuWarning.Name = "chkEnableCpuWarning"
        chkEnableCpuWarning.Size = New Size(206, 29)
        chkEnableCpuWarning.TabIndex = 0
        chkEnableCpuWarning.Text = "Enable Cpu Warning"
        chkEnableCpuWarning.UseVisualStyleBackColor = True
        ' 
        ' chkEnableGpuWarning
        ' 
        chkEnableGpuWarning.AutoSize = True
        chkEnableGpuWarning.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        chkEnableGpuWarning.Location = New Point(28, 85)
        chkEnableGpuWarning.Name = "chkEnableGpuWarning"
        chkEnableGpuWarning.Size = New Size(208, 29)
        chkEnableGpuWarning.TabIndex = 1
        chkEnableGpuWarning.Text = "Enable Gpu Warning"
        chkEnableGpuWarning.UseVisualStyleBackColor = True
        ' 
        ' chkLogPlugins
        ' 
        chkLogPlugins.AutoSize = True
        chkLogPlugins.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        chkLogPlugins.Location = New Point(322, 35)
        chkLogPlugins.Name = "chkLogPlugins"
        chkLogPlugins.Size = New Size(136, 29)
        chkLogPlugins.TabIndex = 3
        chkLogPlugins.Text = "Log Plugins"
        chkLogPlugins.UseVisualStyleBackColor = True
        ' 
        ' chkEnableAutoScreenshot
        ' 
        chkEnableAutoScreenshot.AutoSize = True
        chkEnableAutoScreenshot.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        chkEnableAutoScreenshot.Location = New Point(553, 85)
        chkEnableAutoScreenshot.Name = "chkEnableAutoScreenshot"
        chkEnableAutoScreenshot.Size = New Size(236, 29)
        chkEnableAutoScreenshot.TabIndex = 2
        chkEnableAutoScreenshot.Text = "Enable Auto Screenshot"
        chkEnableAutoScreenshot.UseVisualStyleBackColor = True
        ' 
        ' chkTriggerScreenshot
        ' 
        chkTriggerScreenshot.AutoSize = True
        chkTriggerScreenshot.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        chkTriggerScreenshot.Location = New Point(553, 35)
        chkTriggerScreenshot.Name = "chkTriggerScreenshot"
        chkTriggerScreenshot.Size = New Size(195, 29)
        chkTriggerScreenshot.TabIndex = 5
        chkTriggerScreenshot.Text = "Trigger Screenshot"
        chkTriggerScreenshot.UseVisualStyleBackColor = True
        ' 
        ' chkHighlightPlugins
        ' 
        chkHighlightPlugins.AutoSize = True
        chkHighlightPlugins.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        chkHighlightPlugins.Location = New Point(322, 85)
        chkHighlightPlugins.Name = "chkHighlightPlugins"
        chkHighlightPlugins.Size = New Size(182, 29)
        chkHighlightPlugins.TabIndex = 4
        chkHighlightPlugins.Text = "Highlight Plugins"
        chkHighlightPlugins.UseVisualStyleBackColor = True
        ' 
        ' lstWatchedPlugins
        ' 
        lstWatchedPlugins.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        lstWatchedPlugins.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lstWatchedPlugins.FormattingEnabled = True
        lstWatchedPlugins.ItemHeight = 25
        lstWatchedPlugins.Location = New Point(399, 195)
        lstWatchedPlugins.Name = "lstWatchedPlugins"
        lstWatchedPlugins.Size = New Size(373, 354)
        lstWatchedPlugins.TabIndex = 6
        ' 
        ' BtnClose
        ' 
        BtnClose.AutoSize = True
        BtnClose.BackgroundImage = CType(resources.GetObject("BtnClose.BackgroundImage"), Image)
        BtnClose.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        BtnClose.Location = New Point(600, 787)
        BtnClose.Name = "BtnClose"
        BtnClose.Size = New Size(172, 58)
        BtnClose.TabIndex = 30
        BtnClose.Text = "Save / Close"
        BtnClose.UseVisualStyleBackColor = True
        ' 
        ' BtnTest
        ' 
        BtnTest.AutoSize = True
        BtnTest.BackgroundImage = CType(resources.GetObject("BtnTest.BackgroundImage"), Image)
        BtnTest.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        BtnTest.Location = New Point(35, 778)
        BtnTest.Name = "BtnTest"
        BtnTest.Size = New Size(128, 54)
        BtnTest.TabIndex = 32
        BtnTest.Text = "Test"
        BtnTest.UseVisualStyleBackColor = True
        BtnTest.Visible = False
        ' 
        ' txtNewPlugin
        ' 
        txtNewPlugin.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtNewPlugin.Location = New Point(178, 814)
        txtNewPlugin.Name = "txtNewPlugin"
        txtNewPlugin.Size = New Size(199, 31)
        txtNewPlugin.TabIndex = 33
        txtNewPlugin.Visible = False
        ' 
        ' btnAddPlugin
        ' 
        btnAddPlugin.AutoSize = True
        btnAddPlugin.BackgroundImage = CType(resources.GetObject("btnAddPlugin.BackgroundImage"), Image)
        btnAddPlugin.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnAddPlugin.Location = New Point(178, 773)
        btnAddPlugin.Name = "btnAddPlugin"
        btnAddPlugin.Size = New Size(199, 35)
        btnAddPlugin.TabIndex = 34
        btnAddPlugin.Text = "Add Plugin"
        btnAddPlugin.UseVisualStyleBackColor = True
        btnAddPlugin.Visible = False
        ' 
        ' btnRemovePlugin
        ' 
        btnRemovePlugin.AutoSize = True
        btnRemovePlugin.BackgroundImage = CType(resources.GetObject("btnRemovePlugin.BackgroundImage"), Image)
        btnRemovePlugin.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnRemovePlugin.Location = New Point(451, 563)
        btnRemovePlugin.Name = "btnRemovePlugin"
        btnRemovePlugin.Size = New Size(271, 44)
        btnRemovePlugin.TabIndex = 35
        btnRemovePlugin.Text = "Remove Selected Plugin"
        btnRemovePlugin.UseVisualStyleBackColor = True
        ' 
        ' btnClearPlugins
        ' 
        btnClearPlugins.AutoSize = True
        btnClearPlugins.BackgroundImage = CType(resources.GetObject("btnClearPlugins.BackgroundImage"), Image)
        btnClearPlugins.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnClearPlugins.Location = New Point(451, 613)
        btnClearPlugins.Name = "btnClearPlugins"
        btnClearPlugins.Size = New Size(271, 45)
        btnClearPlugins.TabIndex = 36
        btnClearPlugins.Text = "Clear All (Watched) Plugins"
        btnClearPlugins.UseVisualStyleBackColor = True
        ' 
        ' lstLoadedModules
        ' 
        lstLoadedModules.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lstLoadedModules.FormattingEnabled = True
        lstLoadedModules.ItemHeight = 25
        lstLoadedModules.Location = New Point(21, 195)
        lstLoadedModules.Name = "lstLoadedModules"
        lstLoadedModules.Size = New Size(372, 354)
        lstLoadedModules.TabIndex = 37
        ' 
        ' btnAddModuleToWatchList
        ' 
        btnAddModuleToWatchList.AutoSize = True
        btnAddModuleToWatchList.BackgroundImage = CType(resources.GetObject("btnAddModuleToWatchList.BackgroundImage"), Image)
        btnAddModuleToWatchList.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnAddModuleToWatchList.Location = New Point(21, 613)
        btnAddModuleToWatchList.Name = "btnAddModuleToWatchList"
        btnAddModuleToWatchList.Size = New Size(271, 35)
        btnAddModuleToWatchList.TabIndex = 38
        btnAddModuleToWatchList.Text = "Add To Watch List"
        btnAddModuleToWatchList.UseVisualStyleBackColor = True
        btnAddModuleToWatchList.Visible = False
        ' 
        ' txtFilterModules
        ' 
        txtFilterModules.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtFilterModules.Location = New Point(229, 157)
        txtFilterModules.Name = "txtFilterModules"
        txtFilterModules.Size = New Size(180, 31)
        txtFilterModules.TabIndex = 39
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(23, 157)
        Label1.Name = "Label1"
        Label1.Size = New Size(200, 25)
        Label1.TabIndex = 40
        Label1.Text = "Search Dll's / Modules:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(30, 728)
        Label2.Name = "Label2"
        Label2.Size = New Size(271, 25)
        Label2.TabIndex = 41
        Label2.Text = "Use Test to take screen capture"
        Label2.Visible = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(21, 553)
        Label3.Name = "Label3"
        Label3.Size = New Size(258, 25)
        Label3.TabIndex = 42
        Label3.Text = "Click item to add to watch list"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(528, 163)
        Label4.Name = "Label4"
        Label4.Size = New Size(92, 25)
        Label4.TabIndex = 43
        Label4.Text = "Watch list"
        ' 
        ' Advanced
        ' 
        AutoScaleDimensions = New SizeF(12F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(800, 857)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtFilterModules)
        Controls.Add(btnAddModuleToWatchList)
        Controls.Add(lstLoadedModules)
        Controls.Add(btnClearPlugins)
        Controls.Add(btnRemovePlugin)
        Controls.Add(btnAddPlugin)
        Controls.Add(txtNewPlugin)
        Controls.Add(BtnTest)
        Controls.Add(BtnClose)
        Controls.Add(lstWatchedPlugins)
        Controls.Add(chkTriggerScreenshot)
        Controls.Add(chkHighlightPlugins)
        Controls.Add(chkLogPlugins)
        Controls.Add(chkEnableAutoScreenshot)
        Controls.Add(chkEnableGpuWarning)
        Controls.Add(chkEnableCpuWarning)
        MinimumSize = New Size(822, 913)
        Name = "Advanced"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Advanced"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents chkEnableCpuWarning As CheckBox
    Friend WithEvents chkEnableGpuWarning As CheckBox
    Friend WithEvents chkLogPlugins As CheckBox
    Friend WithEvents chkEnableAutoScreenshot As CheckBox
    Friend WithEvents chkTriggerScreenshot As CheckBox
    Friend WithEvents chkHighlightPlugins As CheckBox
    Friend WithEvents lstWatchedPlugins As ListBox
    Friend WithEvents BtnClose As Button
    Friend WithEvents BtnTest As Button
    Friend WithEvents txtNewPlugin As TextBox
    Friend WithEvents btnAddPlugin As Button
    Friend WithEvents btnRemovePlugin As Button
    Friend WithEvents btnClearPlugins As Button
    Friend WithEvents lstLoadedModules As ListBox
    Friend WithEvents btnAddModuleToWatchList As Button
    Friend WithEvents txtFilterModules As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
