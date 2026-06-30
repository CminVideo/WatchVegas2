<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Replace
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Replace))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        RTBlog = New RichTextBox()
        btnRefresh = New Button()
        BtnCopyErrData = New Button()
        BtnExit = New Button()
        Panel1 = New Panel()
        lblGpuLoad = New Label()
        btnSD = New Button()
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        OpenSavedDataToolStripMenuItem = New ToolStripMenuItem()
        OpenDataToolStripMenuItem = New ToolStripMenuItem()
        RefreshDataToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        PrintLogToPDFToolStripMenuItem = New ToolStripMenuItem()
        OpenLofFolderToolStripMenuItem = New ToolStripMenuItem()
        OpenScreenshotFolderToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        EXITToolStripMenuItem = New ToolStripMenuItem()
        EditToolStripMenuItem = New ToolStripMenuItem()
        OpenSettingsToolStripMenuItem = New ToolStripMenuItem()
        ToolsToolStripMenuItem = New ToolStripMenuItem()
        HelpToolStripMenuItem = New ToolStripMenuItem()
        Btnsfxc = New Button()
        StatusStrip1 = New StatusStrip()
        toolCpuTemp = New ToolStripStatusLabel()
        ToolVRAM = New ToolStripStatusLabel()
        toolStripWatchedPlugins = New ToolStripStatusLabel()
        Label2 = New Label()
        txtSearch = New TextBox()
        Button1 = New Button()
        ChartCPU = New DataVisualization.Charting.Chart()
        ChartGPU = New DataVisualization.Charting.Chart()
        ChartRAM = New DataVisualization.Charting.Chart()
        ChartDisk = New DataVisualization.Charting.Chart()
        BtnCF = New Button()
        BtnMM = New Button()
        BtnCs = New Button()
        BtnBorisFX = New Button()
        TrayIcon = New NotifyIcon(components)
        RevertTrayIconTimer = New Timer(components)
        mnuSessionWarnings = New Timer(components)
        dataGatherTimer = New Timer(components)
        Panel1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        CType(ChartCPU, ComponentModel.ISupportInitialize).BeginInit()
        CType(ChartGPU, ComponentModel.ISupportInitialize).BeginInit()
        CType(ChartRAM, ComponentModel.ISupportInitialize).BeginInit()
        CType(ChartDisk, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' RTBlog
        ' 
        RTBlog.Location = New Point(12, 143)
        RTBlog.Name = "RTBlog"
        RTBlog.Size = New Size(583, 686)
        RTBlog.TabIndex = 0
        RTBlog.Text = ""
        ' 
        ' btnRefresh
        ' 
        btnRefresh.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        btnRefresh.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), Image)
        btnRefresh.Location = New Point(13, 852)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(171, 56)
        btnRefresh.TabIndex = 38
        btnRefresh.Text = "Refresh Data"
        btnRefresh.UseVisualStyleBackColor = False
        ' 
        ' BtnCopyErrData
        ' 
        BtnCopyErrData.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        BtnCopyErrData.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCopyErrData.Image = CType(resources.GetObject("BtnCopyErrData.Image"), Image)
        BtnCopyErrData.Location = New Point(190, 852)
        BtnCopyErrData.Name = "BtnCopyErrData"
        BtnCopyErrData.Size = New Size(299, 56)
        BtnCopyErrData.TabIndex = 39
        BtnCopyErrData.Text = "Copy Data to Clipboard"
        BtnCopyErrData.UseVisualStyleBackColor = False
        ' 
        ' BtnExit
        ' 
        BtnExit.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        BtnExit.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnExit.Image = CType(resources.GetObject("BtnExit.Image"), Image)
        BtnExit.Location = New Point(799, 852)
        BtnExit.Name = "BtnExit"
        BtnExit.Size = New Size(164, 56)
        BtnExit.TabIndex = 40
        BtnExit.Text = "Exit"
        BtnExit.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(lblGpuLoad)
        Panel1.Location = New Point(495, 852)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(289, 60)
        Panel1.TabIndex = 41
        ' 
        ' lblGpuLoad
        ' 
        lblGpuLoad.AutoSize = True
        lblGpuLoad.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblGpuLoad.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(64))
        lblGpuLoad.Location = New Point(16, 9)
        lblGpuLoad.Name = "lblGpuLoad"
        lblGpuLoad.Size = New Size(242, 25)
        lblGpuLoad.TabIndex = 0
        lblGpuLoad.Text = "System Load : Normal (13%)"
        ' 
        ' btnSD
        ' 
        btnSD.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        btnSD.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSD.ForeColor = Color.Navy
        btnSD.Image = CType(resources.GetObject("btnSD.Image"), Image)
        btnSD.Location = New Point(13, 45)
        btnSD.Name = "btnSD"
        btnSD.Size = New Size(148, 46)
        btnSD.TabIndex = 42
        btnSD.Text = "Save Data"
        btnSD.UseVisualStyleBackColor = False
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.BackColor = SystemColors.ActiveCaption
        MenuStrip1.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MenuStrip1.ImageScalingSize = New Size(24, 24)
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, EditToolStripMenuItem, ToolsToolStripMenuItem, HelpToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.RenderMode = ToolStripRenderMode.Professional
        MenuStrip1.Size = New Size(965, 33)
        MenuStrip1.TabIndex = 43
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OpenSavedDataToolStripMenuItem, OpenDataToolStripMenuItem, RefreshDataToolStripMenuItem, ToolStripSeparator2, PrintLogToPDFToolStripMenuItem, OpenLofFolderToolStripMenuItem, OpenScreenshotFolderToolStripMenuItem, ToolStripSeparator1, EXITToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(57, 29)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' OpenSavedDataToolStripMenuItem
        ' 
        OpenSavedDataToolStripMenuItem.Name = "OpenSavedDataToolStripMenuItem"
        OpenSavedDataToolStripMenuItem.Size = New Size(315, 34)
        OpenSavedDataToolStripMenuItem.Text = "Open Saved Data"
        ' 
        ' OpenDataToolStripMenuItem
        ' 
        OpenDataToolStripMenuItem.Name = "OpenDataToolStripMenuItem"
        OpenDataToolStripMenuItem.Size = New Size(315, 34)
        OpenDataToolStripMenuItem.Text = "Open Data"
        ' 
        ' RefreshDataToolStripMenuItem
        ' 
        RefreshDataToolStripMenuItem.Name = "RefreshDataToolStripMenuItem"
        RefreshDataToolStripMenuItem.Size = New Size(315, 34)
        RefreshDataToolStripMenuItem.Text = "Refresh Data"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(312, 6)
        ' 
        ' PrintLogToPDFToolStripMenuItem
        ' 
        PrintLogToPDFToolStripMenuItem.Name = "PrintLogToPDFToolStripMenuItem"
        PrintLogToPDFToolStripMenuItem.Size = New Size(315, 34)
        PrintLogToPDFToolStripMenuItem.Text = "Print Log to PDF"
        ' 
        ' OpenLofFolderToolStripMenuItem
        ' 
        OpenLofFolderToolStripMenuItem.Name = "OpenLofFolderToolStripMenuItem"
        OpenLofFolderToolStripMenuItem.Size = New Size(315, 34)
        OpenLofFolderToolStripMenuItem.Text = "Open Log Folder"
        ' 
        ' OpenScreenshotFolderToolStripMenuItem
        ' 
        OpenScreenshotFolderToolStripMenuItem.Name = "OpenScreenshotFolderToolStripMenuItem"
        OpenScreenshotFolderToolStripMenuItem.Size = New Size(315, 34)
        OpenScreenshotFolderToolStripMenuItem.Text = "Open Screenshot Folder"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(312, 6)
        ' 
        ' EXITToolStripMenuItem
        ' 
        EXITToolStripMenuItem.Name = "EXITToolStripMenuItem"
        EXITToolStripMenuItem.Size = New Size(315, 34)
        EXITToolStripMenuItem.Text = "EXIT"
        ' 
        ' EditToolStripMenuItem
        ' 
        EditToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OpenSettingsToolStripMenuItem})
        EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        EditToolStripMenuItem.Size = New Size(60, 29)
        EditToolStripMenuItem.Text = "Edit"
        ' 
        ' OpenSettingsToolStripMenuItem
        ' 
        OpenSettingsToolStripMenuItem.Name = "OpenSettingsToolStripMenuItem"
        OpenSettingsToolStripMenuItem.Size = New Size(234, 34)
        OpenSettingsToolStripMenuItem.Text = "Open Settings"
        ' 
        ' ToolsToolStripMenuItem
        ' 
        ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        ToolsToolStripMenuItem.Size = New Size(71, 29)
        ToolsToolStripMenuItem.Text = "Tools"
        ' 
        ' HelpToolStripMenuItem
        ' 
        HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        HelpToolStripMenuItem.Size = New Size(67, 29)
        HelpToolStripMenuItem.Text = "Help"
        ' 
        ' Btnsfxc
        ' 
        Btnsfxc.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        Btnsfxc.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Btnsfxc.ForeColor = Color.Navy
        Btnsfxc.Image = CType(resources.GetObject("Btnsfxc.Image"), Image)
        Btnsfxc.Location = New Point(167, 45)
        Btnsfxc.Name = "Btnsfxc"
        Btnsfxc.Size = New Size(160, 46)
        Btnsfxc.TabIndex = 44
        Btnsfxc.Text = "Show FX Ctls"
        Btnsfxc.UseVisualStyleBackColor = False
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.BackColor = Color.White
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {toolCpuTemp, ToolVRAM, toolStripWatchedPlugins})
        StatusStrip1.Location = New Point(0, 922)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.RenderMode = ToolStripRenderMode.Professional
        StatusStrip1.Size = New Size(965, 37)
        StatusStrip1.TabIndex = 45
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' toolCpuTemp
        ' 
        toolCpuTemp.Name = "toolCpuTemp"
        toolCpuTemp.Size = New Size(119, 30)
        toolCpuTemp.Text = "CPU Temp:"
        ' 
        ' ToolVRAM
        ' 
        ToolVRAM.Name = "ToolVRAM"
        ToolVRAM.Size = New Size(65, 30)
        ToolVRAM.Text = "RAM:"
        ' 
        ' toolStripWatchedPlugins
        ' 
        toolStripWatchedPlugins.Name = "toolStripWatchedPlugins"
        toolStripWatchedPlugins.Size = New Size(172, 30)
        toolStripWatchedPlugins.Text = "Watched Plugins"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Navy
        Label2.Location = New Point(14, 111)
        Label2.Name = "Label2"
        Label2.Size = New Size(174, 25)
        Label2.TabIndex = 46
        Label2.Text = "Search Dll/Modules"
        ' 
        ' txtSearch
        ' 
        txtSearch.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSearch.Location = New Point(195, 105)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(225, 31)
        txtSearch.TabIndex = 47
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        Button1.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.Image = CType(resources.GetObject("Button1.Image"), Image)
        Button1.Location = New Point(426, 99)
        Button1.Name = "Button1"
        Button1.Size = New Size(169, 37)
        Button1.TabIndex = 48
        Button1.Text = "Clear Search"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' ChartCPU
        ' 
        ChartArea1.Name = "ChartArea1"
        ChartCPU.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        ChartCPU.Legends.Add(Legend1)
        ChartCPU.Location = New Point(615, 108)
        ChartCPU.Name = "ChartCPU"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        ChartCPU.Series.Add(Series1)
        ChartCPU.Size = New Size(338, 177)
        ChartCPU.TabIndex = 49
        ChartCPU.Text = "Chart1"
        ' 
        ' ChartGPU
        ' 
        ChartArea2.Name = "ChartArea1"
        ChartGPU.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        ChartGPU.Legends.Add(Legend2)
        ChartGPU.Location = New Point(615, 287)
        ChartGPU.Name = "ChartGPU"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        ChartGPU.Series.Add(Series2)
        ChartGPU.Size = New Size(338, 177)
        ChartGPU.TabIndex = 50
        ChartGPU.Text = "Chart2"
        ' 
        ' ChartRAM
        ' 
        ChartArea3.Name = "ChartArea1"
        ChartRAM.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        ChartRAM.Legends.Add(Legend3)
        ChartRAM.Location = New Point(615, 466)
        ChartRAM.Name = "ChartRAM"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        ChartRAM.Series.Add(Series3)
        ChartRAM.Size = New Size(338, 177)
        ChartRAM.TabIndex = 51
        ChartRAM.Text = "Chart3"
        ' 
        ' ChartDisk
        ' 
        ChartArea4.Name = "ChartArea1"
        ChartDisk.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        ChartDisk.Legends.Add(Legend4)
        ChartDisk.Location = New Point(615, 645)
        ChartDisk.Name = "ChartDisk"
        Series4.ChartArea = "ChartArea1"
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        ChartDisk.Series.Add(Series4)
        ChartDisk.Size = New Size(338, 177)
        ChartDisk.TabIndex = 52
        ChartDisk.Text = "Chart4"
        ' 
        ' BtnCF
        ' 
        BtnCF.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        BtnCF.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCF.ForeColor = Color.Navy
        BtnCF.Image = CType(resources.GetObject("BtnCF.Image"), Image)
        BtnCF.Location = New Point(335, 45)
        BtnCF.Name = "BtnCF"
        BtnCF.Size = New Size(154, 46)
        BtnCF.TabIndex = 53
        BtnCF.Text = "Conflict Filter"
        BtnCF.UseVisualStyleBackColor = False
        ' 
        ' BtnMM
        ' 
        BtnMM.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        BtnMM.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnMM.ForeColor = Color.Navy
        BtnMM.Image = CType(resources.GetObject("BtnMM.Image"), Image)
        BtnMM.Location = New Point(494, 45)
        BtnMM.Name = "BtnMM"
        BtnMM.Size = New Size(140, 46)
        BtnMM.TabIndex = 54
        BtnMM.Text = "Monitor"
        BtnMM.UseVisualStyleBackColor = False
        ' 
        ' BtnCs
        ' 
        BtnCs.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        BtnCs.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCs.ForeColor = Color.Navy
        BtnCs.Image = CType(resources.GetObject("BtnCs.Image"), Image)
        BtnCs.Location = New Point(638, 45)
        BtnCs.Name = "BtnCs"
        BtnCs.Size = New Size(159, 46)
        BtnCs.TabIndex = 55
        BtnCs.Text = "Capture Screen"
        BtnCs.UseVisualStyleBackColor = False
        ' 
        ' BtnBorisFX
        ' 
        BtnBorisFX.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        BtnBorisFX.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnBorisFX.ForeColor = Color.Navy
        BtnBorisFX.Image = CType(resources.GetObject("BtnBorisFX.Image"), Image)
        BtnBorisFX.Location = New Point(803, 45)
        BtnBorisFX.Name = "BtnBorisFX"
        BtnBorisFX.Size = New Size(142, 46)
        BtnBorisFX.TabIndex = 56
        BtnBorisFX.Text = "BorisFX Log"
        BtnBorisFX.UseVisualStyleBackColor = False
        ' 
        ' TrayIcon
        ' 
        TrayIcon.Text = "NotifyIcon1"
        TrayIcon.Visible = True
        ' 
        ' RevertTrayIconTimer
        ' 
        RevertTrayIconTimer.Interval = 2000
        ' 
        ' Replace
        ' 
        AutoScaleDimensions = New SizeF(12.0F, 30.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(965, 959)
        Controls.Add(BtnBorisFX)
        Controls.Add(BtnCs)
        Controls.Add(BtnMM)
        Controls.Add(BtnCF)
        Controls.Add(ChartDisk)
        Controls.Add(ChartRAM)
        Controls.Add(ChartGPU)
        Controls.Add(ChartCPU)
        Controls.Add(Button1)
        Controls.Add(txtSearch)
        Controls.Add(Label2)
        Controls.Add(StatusStrip1)
        Controls.Add(Btnsfxc)
        Controls.Add(btnSD)
        Controls.Add(Panel1)
        Controls.Add(BtnExit)
        Controls.Add(BtnCopyErrData)
        Controls.Add(btnRefresh)
        Controls.Add(RTBlog)
        Controls.Add(MenuStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        MinimumSize = New Size(987, 1015)
        Name = "Replace"
        StartPosition = FormStartPosition.CenterScreen
        Text = "WatchVegas3 V2.4.5 5"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        CType(ChartCPU, ComponentModel.ISupportInitialize).EndInit()
        CType(ChartGPU, ComponentModel.ISupportInitialize).EndInit()
        CType(ChartRAM, ComponentModel.ISupportInitialize).EndInit()
        CType(ChartDisk, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents RTBlog As RichTextBox
    Friend WithEvents btnRefresh As Button
    Friend WithEvents BtnCopyErrData As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblGpuLoad As Label
    Friend WithEvents btnSD As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenSavedDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Btnsfxc As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents Label2 As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents ChartCPU As DataVisualization.Charting.Chart
    Friend WithEvents ChartGPU As DataVisualization.Charting.Chart
    Friend WithEvents ChartRAM As DataVisualization.Charting.Chart
    Friend WithEvents ChartDisk As DataVisualization.Charting.Chart
    Friend WithEvents BtnCF As Button
    Friend WithEvents BtnMM As Button
    Friend WithEvents BtnCs As Button
    Friend WithEvents BtnBorisFX As Button
    Friend WithEvents toolCpuTemp As ToolStripStatusLabel
    Friend WithEvents ToolVRAM As ToolStripStatusLabel
    Friend WithEvents toolStripWatchedPlugins As ToolStripStatusLabel
    Friend WithEvents RefreshDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintLogToPDFToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenLofFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenScreenshotFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EXITToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TrayIcon As NotifyIcon
    Friend WithEvents RevertTrayIconTimer As Timer
    Friend WithEvents OpenSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuSessionWarnings As Timer
    Friend WithEvents dataGatherTimer As Timer
    Private Sub BtnCs_Click(sender As Object, e As EventArgs) Handles BtnCs.Click

    End Sub

    Private Sub btnSD_Click(sender As Object, e As EventArgs) Handles btnSD.Click

    End Sub

    Private Sub Btnsfxc_Click(sender As Object, e As EventArgs) Handles Btnsfxc.Click

    End Sub

    Private Sub BtnCF_Click(sender As Object, e As EventArgs) Handles BtnCF.Click

    End Sub

    Private Sub BtnMM_Click(sender As Object, e As EventArgs) Handles BtnMM.Click

    End Sub

    Private Sub BtnBorisFX_Click(sender As Object, e As EventArgs) Handles BtnBorisFX.Click

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

    End Sub

    Private Sub BtnCopyErrData_Click(sender As Object, e As EventArgs) Handles BtnCopyErrData.Click

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Application.Exit()
    End Sub
End Class
