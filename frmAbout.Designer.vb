<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        btnClose = New Button()
        Label1 = New Label()
        lblVersion = New Label()
        lblCopyright = New Label()
        lblAppName = New Label()
        PictureBox1 = New PictureBox()
        RTBHistory = New RichTextBox()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(617, 883)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(126, 53)
        btnClose.TabIndex = 0
        btnClose.Text = "&Close"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(119, 36)
        Label1.Name = "Label1"
        Label1.Size = New Size(353, 30)
        Label1.TabIndex = 1
        Label1.Text = "App Name: WatchVegas Monitor"
        ' 
        ' lblVersion
        ' 
        lblVersion.AutoSize = True
        lblVersion.Font = New Font("Microsoft Sans Serif", 8F)
        lblVersion.Location = New Point(128, 85)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(0, 20)
        lblVersion.TabIndex = 2
        ' 
        ' lblCopyright
        ' 
        lblCopyright.AutoSize = True
        lblCopyright.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblCopyright.ForeColor = Color.Black
        lblCopyright.Location = New Point(130, 122)
        lblCopyright.Name = "lblCopyright"
        lblCopyright.Size = New Size(66, 25)
        lblCopyright.TabIndex = 3
        lblCopyright.Text = "Label2"
        ' 
        ' lblAppName
        ' 
        lblAppName.AutoSize = True
        lblAppName.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblAppName.Location = New Point(132, 166)
        lblAppName.Name = "lblAppName"
        lblAppName.Size = New Size(66, 25)
        lblAppName.TabIndex = 4
        lblAppName.Text = "Label2"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(28, 12)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(76, 77)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' RTBHistory
        ' 
        RTBHistory.Location = New Point(12, 216)
        RTBHistory.Name = "RTBHistory"
        RTBHistory.Size = New Size(744, 632)
        RTBHistory.TabIndex = 6
        RTBHistory.Text = ""
        ' 
        ' frmAbout
        ' 
        AutoScaleDimensions = New SizeF(12F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(768, 948)
        ControlBox = False
        Controls.Add(RTBHistory)
        Controls.Add(PictureBox1)
        Controls.Add(lblAppName)
        Controls.Add(lblCopyright)
        Controls.Add(lblVersion)
        Controls.Add(Label1)
        Controls.Add(btnClose)
        Name = "frmAbout"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "frmAbout"
        TopMost = True
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents lblCopyright As Label
    Friend WithEvents lblAppName As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents RTBHistory As RichTextBox
End Class
