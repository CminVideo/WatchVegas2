<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCaptureOne
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCaptureOne))
        btnClose = New Button()
        btnStartCap = New Button()
        btnStopCap = New Button()
        btnPurgeCap = New Button()
        btnOpenFolder = New Button()
        lblStatus = New Label()
        lblCountImages = New Label()
        Timer1 = New Timer(components)
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' btnClose
        ' 
        btnClose.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        btnClose.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnClose.Image = CType(resources.GetObject("btnClose.Image"), Image)
        btnClose.Location = New Point(400, 415)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(159, 56)
        btnClose.TabIndex = 37
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' btnStartCap
        ' 
        btnStartCap.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        btnStartCap.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnStartCap.Image = CType(resources.GetObject("btnStartCap.Image"), Image)
        btnStartCap.Location = New Point(26, 56)
        btnStartCap.Name = "btnStartCap"
        btnStartCap.Size = New Size(159, 56)
        btnStartCap.TabIndex = 38
        btnStartCap.Text = "Start Capture"
        btnStartCap.UseVisualStyleBackColor = False
        ' 
        ' btnStopCap
        ' 
        btnStopCap.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        btnStopCap.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnStopCap.Image = CType(resources.GetObject("btnStopCap.Image"), Image)
        btnStopCap.Location = New Point(26, 139)
        btnStopCap.Name = "btnStopCap"
        btnStopCap.Size = New Size(159, 56)
        btnStopCap.TabIndex = 39
        btnStopCap.Text = "Stop Capture"
        btnStopCap.UseVisualStyleBackColor = False
        ' 
        ' btnPurgeCap
        ' 
        btnPurgeCap.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        btnPurgeCap.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPurgeCap.Image = CType(resources.GetObject("btnPurgeCap.Image"), Image)
        btnPurgeCap.Location = New Point(26, 220)
        btnPurgeCap.Name = "btnPurgeCap"
        btnPurgeCap.Size = New Size(159, 56)
        btnPurgeCap.TabIndex = 40
        btnPurgeCap.Text = "Purge Folder"
        btnPurgeCap.UseVisualStyleBackColor = False
        ' 
        ' btnOpenFolder
        ' 
        btnOpenFolder.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        btnOpenFolder.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnOpenFolder.Image = CType(resources.GetObject("btnOpenFolder.Image"), Image)
        btnOpenFolder.Location = New Point(26, 306)
        btnOpenFolder.Name = "btnOpenFolder"
        btnOpenFolder.Size = New Size(159, 56)
        btnOpenFolder.TabIndex = 41
        btnOpenFolder.Text = "Open Folder"
        btnOpenFolder.UseVisualStyleBackColor = False
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblStatus.Location = New Point(210, 87)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(209, 25)
        lblStatus.TabIndex = 42
        lblStatus.Text = "Status ready to capture:"
        ' 
        ' lblCountImages
        ' 
        lblCountImages.AutoSize = True
        lblCountImages.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblCountImages.Location = New Point(442, 322)
        lblCountImages.Name = "lblCountImages"
        lblCountImages.Size = New Size(32, 25)
        lblCountImages.TabIndex = 43
        lblCountImages.Text = "00"
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 2000
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(221, 322)
        Label1.Name = "Label1"
        Label1.Size = New Size(215, 25)
        Label1.TabIndex = 44
        Label1.Text = "Count (Images in folder)"
        ' 
        ' frmCaptureOne
        ' 
        AutoScaleDimensions = New SizeF(12F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(582, 494)
        Controls.Add(Label1)
        Controls.Add(lblCountImages)
        Controls.Add(lblStatus)
        Controls.Add(btnOpenFolder)
        Controls.Add(btnPurgeCap)
        Controls.Add(btnStopCap)
        Controls.Add(btnStartCap)
        Controls.Add(btnClose)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCaptureOne"
        Text = "Screen Capture"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents btnStartCap As Button
    Friend WithEvents btnStopCap As Button
    Friend WithEvents btnPurgeCap As Button
    Friend WithEvents btnOpenFolder As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblCountImages As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label1 As Label
End Class
