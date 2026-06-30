<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenshot
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
        btnCloseForm = New Button()
        btnPurge = New Button()
        btnOpenFolder = New Button()
        pbPreview = New PictureBox()
        lblFrameCount = New Label()
        lblStatus = New Label()
        btnstop = New Button()
        btnclear = New Button()
        btnSave = New Button()
        btnstart = New Button()
        CType(pbPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnCloseForm
        ' 
        btnCloseForm.Location = New Point(796, 697)
        btnCloseForm.Name = "btnCloseForm"
        btnCloseForm.Size = New Size(121, 48)
        btnCloseForm.TabIndex = 1
        btnCloseForm.Text = "&Exit"
        btnCloseForm.UseVisualStyleBackColor = True
        ' 
        ' btnPurge
        ' 
        btnPurge.Location = New Point(15, 685)
        btnPurge.Name = "btnPurge"
        btnPurge.Size = New Size(211, 60)
        btnPurge.TabIndex = 18
        btnPurge.Text = "Purge All Captures"
        btnPurge.UseVisualStyleBackColor = True
        ' 
        ' btnOpenFolder
        ' 
        btnOpenFolder.Location = New Point(12, 423)
        btnOpenFolder.Name = "btnOpenFolder"
        btnOpenFolder.Size = New Size(164, 60)
        btnOpenFolder.TabIndex = 17
        btnOpenFolder.Text = "Open Folder"
        btnOpenFolder.UseVisualStyleBackColor = True
        ' 
        ' pbPreview
        ' 
        pbPreview.BackColor = SystemColors.ActiveCaptionText
        pbPreview.Location = New Point(201, 53)
        pbPreview.Name = "pbPreview"
        pbPreview.Size = New Size(736, 444)
        pbPreview.SizeMode = PictureBoxSizeMode.Zoom
        pbPreview.TabIndex = 16
        pbPreview.TabStop = False
        ' 
        ' lblFrameCount
        ' 
        lblFrameCount.AutoSize = True
        lblFrameCount.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblFrameCount.Location = New Point(15, 541)
        lblFrameCount.Name = "lblFrameCount"
        lblFrameCount.Size = New Size(155, 32)
        lblFrameCount.TabIndex = 15
        lblFrameCount.Text = "Frame Count"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblStatus.Location = New Point(15, 593)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(81, 32)
        lblStatus.TabIndex = 14
        lblStatus.Text = "Status"
        ' 
        ' btnstop
        ' 
        btnstop.Location = New Point(12, 147)
        btnstop.Name = "btnstop"
        btnstop.Size = New Size(164, 60)
        btnstop.TabIndex = 13
        btnstop.Text = "Stop"
        btnstop.UseVisualStyleBackColor = True
        ' 
        ' btnclear
        ' 
        btnclear.Location = New Point(12, 239)
        btnclear.Name = "btnclear"
        btnclear.Size = New Size(164, 60)
        btnclear.TabIndex = 12
        btnclear.Text = "Clear"
        btnclear.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(12, 331)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(164, 60)
        btnSave.TabIndex = 11
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnstart
        ' 
        btnstart.Location = New Point(12, 55)
        btnstart.Name = "btnstart"
        btnstart.Size = New Size(164, 60)
        btnstart.TabIndex = 10
        btnstart.Text = "Start"
        btnstart.UseVisualStyleBackColor = True
        ' 
        ' frmScreenshot
        ' 
        AutoScaleDimensions = New SizeF(12F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(949, 772)
        ControlBox = False
        Controls.Add(btnPurge)
        Controls.Add(btnOpenFolder)
        Controls.Add(pbPreview)
        Controls.Add(lblFrameCount)
        Controls.Add(lblStatus)
        Controls.Add(btnstop)
        Controls.Add(btnclear)
        Controls.Add(btnSave)
        Controls.Add(btnstart)
        Controls.Add(btnCloseForm)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MaximumSize = New Size(971, 828)
        MinimumSize = New Size(971, 828)
        Name = "frmScreenshot"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Screenshot"
        CType(pbPreview, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents btnCloseForm As Button
    Friend WithEvents btnPurge As Button
    Friend WithEvents btnOpenFolder As Button
    Friend WithEvents pbPreview As PictureBox
    Friend WithEvents lblFrameCount As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnstop As Button
    Friend WithEvents btnclear As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnstart As Button
End Class
