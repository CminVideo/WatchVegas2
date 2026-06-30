Partial Class frmMain2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

        ' ===== Menus =====
        MenuStrip1 = New MenuStrip()
        mnuFile = New ToolStripMenuItem()
        SaveDataToolStripMenuItem = New ToolStripMenuItem()
        OpenDataToolStripMenuItem = New ToolStripMenuItem()
        PrintLgToPDFToolStripMenuItem = New ToolStripMenuItem()
        ExitToolStripMenuItem1 = New ToolStripMenuItem()
        mnuEdit = New ToolStripMenuItem()
        muOpenScreenshotFolder = New ToolStripMenuItem()
        TollOpenLogFolder = New ToolStripMenuItem()
        mnuTools = New ToolStripMenuItem()
        OpenSettingsToolStripMenuItem = New ToolStripMenuItem()
        mnuHeartbeatLogging = New ToolStripMenuItem()
        mnuCrashAnalysis = New ToolStripMenuItem()
        mnuSessionWarnings = New ToolStripMenuItem()
        SummaryReportToolStripMenuItem = New ToolStripMenuItem()
        mnuHelp = New ToolStripMenuItem()
        AboutToolStripMenuItem = New ToolStripMenuItem()
        mnuContents = New ToolStripMenuItem()
        TsMiRefresh = New ToolStripMenuItem()

        ' ===== Status strip =====
        StatusStrip1 = New StatusStrip()
        toolCpuTemp = New ToolStripStatusLabel()
        ToolVRAM = New ToolStripStatusLabel()
        toolStripWatchedPlugins = New ToolStripStatusLabel()

        ' ===== Buttons =====
        BtnCF = New Button()
        Btnsfxc = New Button()
        BtnCs = New Button()
        BtnMM = New Button()
        BtnBorisFX = New Button()
        Button1 = New Button()
        btnRefresh = New Button()
        btnSD = New Button()
        BtnCopyErrData = New Button()
        BtnExit = New Button()

        ' ===== Other inputs / display =====
        txtSearch = New TextBox()
        RTBlog = New RichTextBox()
        Panel1 = New Panel()
        lblSystemLoad = New Label()
        lblGpuLoad = New Label()

        ' ===== Charts =====
        Dim ChartArea1 As New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea2 As New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea3 As New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea4 As New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        ChartCPU = New System.Windows.Forms.DataVisualization.Charting.Chart()
        ChartGPU = New System.Windows.Forms.DataVisualization.Charting.Chart()
        ChartRAM = New System.Windows.Forms.DataVisualization.Charting.Chart()
        ChartDisk = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(ChartCPU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(ChartGPU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(ChartRAM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(ChartDisk, System.ComponentModel.ISupportInitialize).BeginInit()

        ' ===== Timers / components without a visible surface =====
        Timer1 = New System.Windows.Forms.Timer(components)
        tmrWaitVegas = New System.Windows.Forms.Timer(components)
        RevertTrayIconTimer = New System.Windows.Forms.Timer(components)
        SessionWarningsPopupTimer = New System.Windows.Forms.Timer(components)
        PrintDialog1 = New PrintDialog()
        PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        TrayIcon = New NotifyIcon(components)

        MenuStrip1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()

        ' ===== MenuStrip wiring =====
        MenuStrip1.Items.AddRange(New ToolStripItem() {mnuFile, mnuEdit, mnuTools, mnuHelp})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1000, 28)
        MenuStrip1.Dock = DockStyle.Top

        mnuFile.Text = "File"
        mnuFile.DropDownItems.AddRange(New ToolStripItem() {SaveDataToolStripMenuItem, OpenDataToolStripMenuItem, PrintLgToPDFToolStripMenuItem, ExitToolStripMenuItem1})
        SaveDataToolStripMenuItem.Text = "Save Data"
        OpenDataToolStripMenuItem.Text = "Open Data Folder"
        PrintLgToPDFToolStripMenuItem.Text = "Print Log to PDF"
        ExitToolStripMenuItem1.Text = "Exit"

        mnuEdit.Text = "Edit"
        mnuEdit.DropDownItems.AddRange(New ToolStripItem() {muOpenScreenshotFolder, TollOpenLogFolder})
        muOpenScreenshotFolder.Text = "Open Screenshot Folder"
        TollOpenLogFolder.Text = "Open Log Folder"

        mnuTools.Text = "Tools"
        mnuTools.DropDownItems.AddRange(New ToolStripItem() {OpenSettingsToolStripMenuItem, mnuHeartbeatLogging, mnuCrashAnalysis, mnuSessionWarnings, SummaryReportToolStripMenuItem, TsMiRefresh})
        OpenSettingsToolStripMenuItem.Text = "Settings"
        mnuHeartbeatLogging.Text = "Heartbeat Logging"
        mnuHeartbeatLogging.CheckOnClick = True
        mnuCrashAnalysis.Text = "Crash Analysis"
        mnuSessionWarnings.Text = "Session Warnings"
        SummaryReportToolStripMenuItem.Text = "Summary Report"
        TsMiRefresh.Text = "Refresh Data"

        mnuHelp.Text = "Help"
        mnuHelp.DropDownItems.AddRange(New ToolStripItem() {AboutToolStripMenuItem, mnuContents})
        AboutToolStripMenuItem.Text = "About"
        mnuContents.Text = "Contents"

        ' ===== StatusStrip wiring =====
        StatusStrip1.Items.AddRange(New ToolStripItem() {toolCpuTemp, ToolVRAM, toolStripWatchedPlugins})
        StatusStrip1.Location = New Point(0, 750)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1000, 26)
        StatusStrip1.Dock = DockStyle.Bottom
        toolCpuTemp.Text = "CPU: --°C"
        ToolVRAM.Text = "VRAM: --%"
        toolStripWatchedPlugins.Text = "Watched Plugins: 0"

        ' ===== Top button row =====
        BtnCF.Location = New Point(12, 35)
        BtnCF.Size = New Size(140, 30)
        BtnCF.Text = "Conflict Filter"
        BtnCF.Name = "BtnCF"

        Btnsfxc.Location = New Point(158, 35)
        Btnsfxc.Size = New Size(140, 30)
        Btnsfxc.Text = "Show FX Ctls"
        Btnsfxc.Name = "Btnsfxc"

        BtnCs.Location = New Point(304, 35)
        BtnCs.Size = New Size(140, 30)
        BtnCs.Text = "Capture Screen"
        BtnCs.Name = "BtnCs"

        BtnMM.Location = New Point(450, 35)
        BtnMM.Size = New Size(140, 30)
        BtnMM.Text = "Monitor"
        BtnMM.Name = "BtnMM"

        BtnBorisFX.Location = New Point(596, 35)
        BtnBorisFX.Size = New Size(140, 30)
        BtnBorisFX.Text = "BorisFX Log"
        BtnBorisFX.Name = "BtnBorisFX"

        ' ===== Search row =====
        txtSearch.Location = New Point(160, 75)
        txtSearch.Size = New Size(260, 23)
        txtSearch.Name = "txtSearch"

        Button1.Location = New Point(430, 74)
        Button1.Size = New Size(110, 25)
        Button1.Text = "Clear Search"
        Button1.Name = "Button1"

        ' ===== Log box =====
        RTBlog.Location = New Point(12, 110)
        RTBlog.Size = New Size(600, 480)
        RTBlog.Name = "RTBlog"
        RTBlog.ReadOnly = True
        RTBlog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' ===== Charts (stacked to the right of the log box) =====
        ChartArea1.Name = "ChartArea1"
        ChartCPU.ChartAreas.Add(ChartArea1)
        ChartCPU.Location = New Point(624, 110)
        ChartCPU.Size = New Size(360, 140)
        ChartCPU.Name = "ChartCPU"
        ChartCPU.Text = "ChartCPU"

        ChartArea2.Name = "ChartArea1"
        ChartGPU.ChartAreas.Add(ChartArea2)
        ChartGPU.Location = New Point(624, 260)
        ChartGPU.Size = New Size(360, 140)
        ChartGPU.Name = "ChartGPU"
        ChartGPU.Text = "ChartGPU"

        ChartArea3.Name = "ChartArea1"
        ChartRAM.ChartAreas.Add(ChartArea3)
        ChartRAM.Location = New Point(624, 410)
        ChartRAM.Size = New Size(360, 140)
        ChartRAM.Name = "ChartRAM"
        ChartRAM.Text = "ChartRAM"

        ChartArea4.Name = "ChartArea1"
        ChartDisk.ChartAreas.Add(ChartArea4)
        ChartDisk.Location = New Point(624, 560)
        ChartDisk.Size = New Size(360, 140)
        ChartDisk.Name = "ChartDisk"
        ChartDisk.Text = "ChartDisk"

        ' ===== Bottom button row (Refresh / Copy / System Load / Exit) =====
        btnRefresh.Location = New Point(12, 600)
        btnRefresh.Size = New Size(140, 35)
        btnRefresh.Text = "Refresh Data"
        btnRefresh.Name = "btnRefresh"

        BtnCopyErrData.Location = New Point(158, 600)
        BtnCopyErrData.Size = New Size(160, 35)
        BtnCopyErrData.Text = "Copy Data to Clipboard"
        BtnCopyErrData.Name = "BtnCopyErrData"

        btnSD.Location = New Point(324, 600)
        btnSD.Size = New Size(120, 35)
        btnSD.Text = "Save Log As..."
        btnSD.Name = "btnSD"

        Panel1.Location = New Point(450, 600)
        Panel1.Size = New Size(180, 35)
        Panel1.Name = "Panel1"
        Panel1.BackColor = SystemColors.ActiveCaption
        Panel1.Controls.Add(lblSystemLoad)

        lblSystemLoad.Dock = DockStyle.Fill
        lblSystemLoad.TextAlign = ContentAlignment.MiddleCenter
        lblSystemLoad.Text = "System Load: NORMAL (0%)"
        lblSystemLoad.Name = "lblSystemLoad"

        BtnExit.Location = New Point(636, 600)
        BtnExit.Size = New Size(100, 35)
        BtnExit.Text = "Exit"
        BtnExit.Name = "BtnExit"

        ' lblGpuLoad isn't visible in the original screenshot layout - tuck it near the
        ' GPU chart since that's the metric it reports on.
        lblGpuLoad.Location = New Point(624, 405)
        lblGpuLoad.Size = New Size(360, 18)
        lblGpuLoad.Text = "GPU: 0%"
        lblGpuLoad.Name = "lblGpuLoad"

        ' ===== Form =====
        AutoScaleDimensions = New SizeF(96.0!, 96.0!)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        ClientSize = New Size(1000, 776)
        Controls.Add(MenuStrip1)
        Controls.Add(StatusStrip1)
        Controls.Add(BtnCF)
        Controls.Add(Btnsfxc)
        Controls.Add(BtnCs)
        Controls.Add(BtnMM)
        Controls.Add(BtnBorisFX)
        Controls.Add(txtSearch)
        Controls.Add(Button1)
        Controls.Add(RTBlog)
        Controls.Add(ChartCPU)
        Controls.Add(ChartGPU)
        Controls.Add(ChartRAM)
        Controls.Add(ChartDisk)
        Controls.Add(lblGpuLoad)
        Controls.Add(btnRefresh)
        Controls.Add(BtnCopyErrData)
        Controls.Add(btnSD)
        Controls.Add(Panel1)
        Controls.Add(BtnExit)
        MainMenuStrip = MenuStrip1
        Name = "frmMain2"
        Text = "WatchVegas2"

        CType(ChartCPU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(ChartGPU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(ChartRAM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(ChartDisk, System.ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    ' ===== Field declarations - WithEvents is what makes the Handles clauses in
    ' frmMain2.vb bind correctly to these controls. =====
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents mnuFile As ToolStripMenuItem
    Friend WithEvents SaveDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintLgToPDFToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents mnuEdit As ToolStripMenuItem
    Friend WithEvents muOpenScreenshotFolder As ToolStripMenuItem
    Friend WithEvents TollOpenLogFolder As ToolStripMenuItem
    Friend WithEvents mnuTools As ToolStripMenuItem
    Friend WithEvents OpenSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuHeartbeatLogging As ToolStripMenuItem
    Friend WithEvents mnuCrashAnalysis As ToolStripMenuItem
    Friend WithEvents mnuSessionWarnings As ToolStripMenuItem
    Friend WithEvents SummaryReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuHelp As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuContents As ToolStripMenuItem
    Friend WithEvents TsMiRefresh As ToolStripMenuItem

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents toolCpuTemp As ToolStripStatusLabel
    Friend WithEvents ToolVRAM As ToolStripStatusLabel
    Friend WithEvents toolStripWatchedPlugins As ToolStripStatusLabel

    Friend WithEvents BtnCF As Button
    Friend WithEvents Btnsfxc As Button
    Friend WithEvents BtnCs As Button
    Friend WithEvents BtnMM As Button
    Friend WithEvents BtnBorisFX As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSD As Button
    Friend WithEvents BtnCopyErrData As Button
    Friend WithEvents BtnExit As Button

    Friend WithEvents txtSearch As TextBox
    Friend WithEvents RTBlog As RichTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblSystemLoad As Label
    Friend WithEvents lblGpuLoad As Label

    Friend WithEvents ChartCPU As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ChartGPU As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ChartRAM As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ChartDisk As System.Windows.Forms.DataVisualization.Charting.Chart

    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents tmrWaitVegas As System.Windows.Forms.Timer
    Friend WithEvents RevertTrayIconTimer As System.Windows.Forms.Timer
    Friend WithEvents SessionWarningsPopupTimer As System.Windows.Forms.Timer
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents TrayIcon As NotifyIcon

End Class
