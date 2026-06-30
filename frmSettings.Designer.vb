<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        numRefreshRate = New NumericUpDown()
        Label1 = New Label()
        chkAlwaysOnTop = New CheckBox()
        Label2 = New Label()
        btnApply = New Button()
        btnClose = New Button()
        btnReset = New Button()
        Label4 = New Label()
        rad100 = New RadioButton()
        rad90 = New RadioButton()
        rad70 = New RadioButton()
        rad60 = New RadioButton()
        btnopenLog = New Button()
        Label5 = New Label()
        chkAutoScreenshot = New CheckBox()
        Label3 = New Label()
        cmbTempThreshold = New ComboBox()
        Label6 = New Label()
        trkGpuThreshold = New TrackBar()
        Label7 = New Label()
        lblGpuValue = New Label()
        numMaxScreenshots = New NumericUpDown()
        Label8 = New Label()
        chkThermalWarning = New CheckBox()
        Label9 = New Label()
        Label10 = New Label()
        chkAutoScreenshotVis = New CheckBox()
        btnOpenPlugins = New Button()
        Label11 = New Label()
        Label12 = New Label()
        chkPluginMonitoring = New CheckBox()
        chkUseAdaptiveCpuWarning = New CheckBox()
        Label13 = New Label()
        CType(numRefreshRate, ComponentModel.ISupportInitialize).BeginInit()
        CType(trkGpuThreshold, ComponentModel.ISupportInitialize).BeginInit()
        CType(numMaxScreenshots, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' numRefreshRate
        ' 
        numRefreshRate.AutoSize = True
        numRefreshRate.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        numRefreshRate.Location = New Point(441, 68)
        numRefreshRate.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        numRefreshRate.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numRefreshRate.Name = "numRefreshRate"
        numRefreshRate.Size = New Size(80, 31)
        numRefreshRate.TabIndex = 0
        numRefreshRate.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 8F)
        Label1.Location = New Point(170, 72)
        Label1.Name = "Label1"
        Label1.Size = New Size(222, 20)
        Label1.TabIndex = 1
        Label1.Text = "Data Refresh Rate (seconds):"
        ' 
        ' chkAlwaysOnTop
        ' 
        chkAlwaysOnTop.AutoSize = True
        chkAlwaysOnTop.Font = New Font("Segoe UI", 9F)
        chkAlwaysOnTop.Location = New Point(441, 159)
        chkAlwaysOnTop.Name = "chkAlwaysOnTop"
        chkAlwaysOnTop.Size = New Size(22, 21)
        chkAlwaysOnTop.TabIndex = 2
        chkAlwaysOnTop.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 8F)
        Label2.Location = New Point(281, 153)
        Label2.Name = "Label2"
        Label2.Size = New Size(111, 20)
        Label2.TabIndex = 3
        Label2.Text = "Always on top:"
        ' 
        ' btnApply
        ' 
        btnApply.AutoSize = True
        btnApply.BackgroundImage = CType(resources.GetObject("btnApply.BackgroundImage"), Image)
        btnApply.BackgroundImageLayout = ImageLayout.Stretch
        btnApply.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnApply.Location = New Point(410, 797)
        btnApply.Name = "btnApply"
        btnApply.Size = New Size(197, 49)
        btnApply.TabIndex = 6
        btnApply.Text = "Apply to All"
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Anchor = AnchorStyles.None
        btnClose.AutoSize = True
        btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), Image)
        btnClose.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnClose.Location = New Point(645, 797)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(126, 49)
        btnClose.TabIndex = 7
        btnClose.Text = "&Close"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' btnReset
        ' 
        btnReset.AutoSize = True
        btnReset.BackgroundImage = CType(resources.GetObject("btnReset.BackgroundImage"), Image)
        btnReset.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnReset.Location = New Point(28, 797)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(197, 49)
        btnReset.TabIndex = 8
        btnReset.Text = "Set to default"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Microsoft Sans Serif", 8F)
        Label4.Location = New Point(285, 339)
        Label4.Name = "Label4"
        Label4.Size = New Size(107, 20)
        Label4.TabIndex = 9
        Label4.Text = "Form Opacity "
        ' 
        ' rad100
        ' 
        rad100.AutoSize = True
        rad100.Font = New Font("Segoe UI", 8F)
        rad100.Location = New Point(441, 333)
        rad100.Name = "rad100"
        rad100.Size = New Size(142, 25)
        rad100.TabIndex = 10
        rad100.TabStop = True
        rad100.Text = "100% - Normal"
        rad100.UseVisualStyleBackColor = True
        ' 
        ' rad90
        ' 
        rad90.AutoSize = True
        rad90.Font = New Font("Segoe UI", 8F)
        rad90.Location = New Point(441, 368)
        rad90.Name = "rad90"
        rad90.Size = New Size(150, 25)
        rad90.TabIndex = 11
        rad90.TabStop = True
        rad90.Text = "90% - Slight Tint"
        rad90.UseVisualStyleBackColor = True
        ' 
        ' rad70
        ' 
        rad70.AutoSize = True
        rad70.Font = New Font("Segoe UI", 8F)
        rad70.Location = New Point(441, 407)
        rad70.Name = "rad70"
        rad70.Size = New Size(203, 25)
        rad70.TabIndex = 12
        rad70.TabStop = True
        rad70.Text = "70% - Semi-Transparent"
        rad70.UseVisualStyleBackColor = True
        ' 
        ' rad60
        ' 
        rad60.AutoSize = True
        rad60.Font = New Font("Segoe UI", 8F)
        rad60.Location = New Point(441, 444)
        rad60.Name = "rad60"
        rad60.Size = New Size(162, 25)
        rad60.TabIndex = 13
        rad60.TabStop = True
        rad60.Text = "60% - Transparent"
        rad60.UseVisualStyleBackColor = True
        ' 
        ' btnopenLog
        ' 
        btnopenLog.AutoSize = True
        btnopenLog.BackgroundImage = CType(resources.GetObject("btnopenLog.BackgroundImage"), Image)
        btnopenLog.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnopenLog.Location = New Point(233, 797)
        btnopenLog.Name = "btnopenLog"
        btnopenLog.Size = New Size(174, 49)
        btnopenLog.TabIndex = 14
        btnopenLog.Text = "Open Log File"
        btnopenLog.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Microsoft Sans Serif", 8F)
        Label5.Location = New Point(262, 194)
        Label5.Name = "Label5"
        Label5.Size = New Size(130, 20)
        Label5.TabIndex = 15
        Label5.Text = "Auto screen shot"
        ' 
        ' chkAutoScreenshot
        ' 
        chkAutoScreenshot.AutoSize = True
        chkAutoScreenshot.Font = New Font("Segoe UI", 9F)
        chkAutoScreenshot.Location = New Point(441, 198)
        chkAutoScreenshot.Name = "chkAutoScreenshot"
        chkAutoScreenshot.Size = New Size(22, 21)
        chkAutoScreenshot.TabIndex = 16
        chkAutoScreenshot.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        Label3.Location = New Point(168, 13)
        Label3.Name = "Label3"
        Label3.Size = New Size(456, 28)
        Label3.TabIndex = 17
        Label3.Text = "Remember to click (Apply) after making changes"
        ' 
        ' cmbTempThreshold
        ' 
        cmbTempThreshold.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmbTempThreshold.Font = New Font("Segoe UI", 9F)
        cmbTempThreshold.FormattingEnabled = True
        cmbTempThreshold.Location = New Point(441, 494)
        cmbTempThreshold.Name = "cmbTempThreshold"
        cmbTempThreshold.Size = New Size(201, 33)
        cmbTempThreshold.TabIndex = 18
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 8F, FontStyle.Bold)
        Label6.Location = New Point(168, 500)
        Label6.Name = "Label6"
        Label6.Size = New Size(229, 21)
        Label6.TabIndex = 19
        Label6.Text = "Set CPU Temperature Warning"
        ' 
        ' trkGpuThreshold
        ' 
        trkGpuThreshold.LargeChange = 10
        trkGpuThreshold.Location = New Point(427, 548)
        trkGpuThreshold.Maximum = 100
        trkGpuThreshold.Name = "trkGpuThreshold"
        trkGpuThreshold.Size = New Size(263, 69)
        trkGpuThreshold.TabIndex = 20
        trkGpuThreshold.TickFrequency = 10
        trkGpuThreshold.TickStyle = TickStyle.Both
        trkGpuThreshold.Value = 90
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI Semibold", 8F, FontStyle.Bold)
        Label7.Location = New Point(161, 557)
        Label7.Name = "Label7"
        Label7.Size = New Size(231, 21)
        Label7.TabIndex = 21
        Label7.Text = "GPU Usage Warning Threshold"
        ' 
        ' lblGpuValue
        ' 
        lblGpuValue.AutoSize = True
        lblGpuValue.Font = New Font("Segoe UI", 9F)
        lblGpuValue.Location = New Point(697, 562)
        lblGpuValue.Name = "lblGpuValue"
        lblGpuValue.Size = New Size(63, 25)
        lblGpuValue.TabIndex = 22
        lblGpuValue.Text = "Label8"
        ' 
        ' numMaxScreenshots
        ' 
        numMaxScreenshots.AutoSize = True
        numMaxScreenshots.Font = New Font("Segoe UI", 9F)
        numMaxScreenshots.Location = New Point(441, 623)
        numMaxScreenshots.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numMaxScreenshots.Name = "numMaxScreenshots"
        numMaxScreenshots.Size = New Size(93, 31)
        numMaxScreenshots.TabIndex = 23
        numMaxScreenshots.Value = New Decimal(New Integer() {50, 0, 0, 0})
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI Semibold", 8F, FontStyle.Bold)
        Label8.Location = New Point(101, 624)
        Label8.Name = "Label8"
        Label8.Size = New Size(291, 21)
        Label8.TabIndex = 24
        Label8.Text = "Maximum Number of Images in Folder"
        ' 
        ' chkThermalWarning
        ' 
        chkThermalWarning.AutoSize = True
        chkThermalWarning.Font = New Font("Segoe UI", 9F)
        chkThermalWarning.Location = New Point(441, 121)
        chkThermalWarning.Name = "chkThermalWarning"
        chkThermalWarning.Size = New Size(22, 21)
        chkThermalWarning.TabIndex = 25
        chkThermalWarning.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Microsoft Sans Serif", 8F)
        Label9.Location = New Point(139, 116)
        Label9.Name = "Label9"
        Label9.Size = New Size(253, 20)
        Label9.TabIndex = 26
        Label9.Text = "Enable Thermal Throttling Warning"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Microsoft Sans Serif", 8F)
        Label10.Location = New Point(104, 236)
        Label10.Name = "Label10"
        Label10.Size = New Size(288, 20)
        Label10.TabIndex = 27
        Label10.Text = "WatchVegas visible in Auto screen shot"
        ' 
        ' chkAutoScreenshotVis
        ' 
        chkAutoScreenshotVis.AutoSize = True
        chkAutoScreenshotVis.Font = New Font("Segoe UI", 9F)
        chkAutoScreenshotVis.Location = New Point(441, 235)
        chkAutoScreenshotVis.Name = "chkAutoScreenshotVis"
        chkAutoScreenshotVis.Size = New Size(22, 21)
        chkAutoScreenshotVis.TabIndex = 28
        chkAutoScreenshotVis.UseVisualStyleBackColor = True
        ' 
        ' btnOpenPlugins
        ' 
        btnOpenPlugins.AutoSize = True
        btnOpenPlugins.BackgroundImage = CType(resources.GetObject("btnOpenPlugins.BackgroundImage"), Image)
        btnOpenPlugins.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        btnOpenPlugins.Location = New Point(441, 688)
        btnOpenPlugins.Name = "btnOpenPlugins"
        btnOpenPlugins.Size = New Size(123, 46)
        btnOpenPlugins.TabIndex = 29
        btnOpenPlugins.Text = "Open"
        btnOpenPlugins.UseVisualStyleBackColor = True
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI Semibold", 8F, FontStyle.Bold)
        Label11.Location = New Point(232, 699)
        Label11.Name = "Label11"
        Label11.Size = New Size(151, 21)
        Label11.TabIndex = 30
        Label11.Text = "Advanced Settings:"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Segoe UI Semibold", 8F, FontStyle.Bold)
        Label12.Location = New Point(594, 78)
        Label12.Name = "Label12"
        Label12.Size = New Size(194, 21)
        Label12.TabIndex = 31
        Label12.Text = "Enable Plugin Monitoring"
        Label12.Visible = False
        ' 
        ' chkPluginMonitoring
        ' 
        chkPluginMonitoring.AutoSize = True
        chkPluginMonitoring.Location = New Point(766, 51)
        chkPluginMonitoring.Name = "chkPluginMonitoring"
        chkPluginMonitoring.Size = New Size(22, 21)
        chkPluginMonitoring.TabIndex = 32
        chkPluginMonitoring.UseVisualStyleBackColor = True
        chkPluginMonitoring.Visible = False
        ' 
        ' chkUseAdaptiveCpuWarning
        ' 
        chkUseAdaptiveCpuWarning.AutoSize = True
        chkUseAdaptiveCpuWarning.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        chkUseAdaptiveCpuWarning.Location = New Point(441, 273)
        chkUseAdaptiveCpuWarning.Name = "chkUseAdaptiveCpuWarning"
        chkUseAdaptiveCpuWarning.Size = New Size(22, 21)
        chkUseAdaptiveCpuWarning.TabIndex = 33
        chkUseAdaptiveCpuWarning.UseVisualStyleBackColor = True
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Microsoft Sans Serif", 8F)
        Label13.Location = New Point(183, 275)
        Label13.Name = "Label13"
        Label13.Size = New Size(200, 20)
        Label13.TabIndex = 34
        Label13.Text = "Use Adaptive Cpu Warning"
        ' 
        ' frmSettings
        ' 
        AutoScaleDimensions = New SizeF(12F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(800, 857)
        ControlBox = False
        Controls.Add(Label13)
        Controls.Add(chkUseAdaptiveCpuWarning)
        Controls.Add(chkPluginMonitoring)
        Controls.Add(Label12)
        Controls.Add(Label11)
        Controls.Add(btnOpenPlugins)
        Controls.Add(chkAutoScreenshotVis)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(chkThermalWarning)
        Controls.Add(Label8)
        Controls.Add(numMaxScreenshots)
        Controls.Add(lblGpuValue)
        Controls.Add(Label7)
        Controls.Add(trkGpuThreshold)
        Controls.Add(Label6)
        Controls.Add(cmbTempThreshold)
        Controls.Add(Label3)
        Controls.Add(chkAutoScreenshot)
        Controls.Add(Label5)
        Controls.Add(btnopenLog)
        Controls.Add(rad60)
        Controls.Add(rad70)
        Controls.Add(rad90)
        Controls.Add(rad100)
        Controls.Add(Label4)
        Controls.Add(btnReset)
        Controls.Add(btnClose)
        Controls.Add(btnApply)
        Controls.Add(Label2)
        Controls.Add(chkAlwaysOnTop)
        Controls.Add(Label1)
        Controls.Add(numRefreshRate)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(822, 810)
        Name = "frmSettings"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Settings"
        TopMost = True
        CType(numRefreshRate, ComponentModel.ISupportInitialize).EndInit()
        CType(trkGpuThreshold, ComponentModel.ISupportInitialize).EndInit()
        CType(numMaxScreenshots, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents numRefreshRate As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents chkAlwaysOnTop As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnApply As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnReset As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents rad100 As RadioButton
    Friend WithEvents rad90 As RadioButton
    Friend WithEvents rad70 As RadioButton
    Friend WithEvents rad60 As RadioButton
    Friend WithEvents btnopenLog As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents chkAutoScreenshot As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbTempThreshold As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents trkGpuThreshold As TrackBar
    Friend WithEvents Label7 As Label
    Friend WithEvents lblGpuValue As Label
    Friend WithEvents numMaxScreenshots As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents chkThermalWarning As CheckBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents chkAutoScreenshotVis As CheckBox
    Friend WithEvents btnOpenPlugins As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents chkPluginMonitoring As CheckBox
    Friend WithEvents chkUseAdaptiveCpuWarning As CheckBox
    Friend WithEvents Label13 As Label
End Class
