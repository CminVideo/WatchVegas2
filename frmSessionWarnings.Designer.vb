<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSessionWarnings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSessionWarnings))
        btnCopyWarnings = New Button()
        btnCloseWarnings = New Button()
        txtWarnings = New RichTextBox()
        SuspendLayout()
        ' 
        ' btnCopyWarnings
        ' 
        btnCopyWarnings.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCopyWarnings.AutoSizeMode = AutoSizeMode.GrowAndShrink
        btnCopyWarnings.BackColor = Color.Tan
        btnCopyWarnings.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCopyWarnings.Image = CType(resources.GetObject("btnCopyWarnings.Image"), Image)
        btnCopyWarnings.Location = New Point(16, 880)
        btnCopyWarnings.Name = "btnCopyWarnings"
        btnCopyWarnings.Size = New Size(263, 53)
        btnCopyWarnings.TabIndex = 25
        btnCopyWarnings.Text = "Copy Data to Clipboard"
        btnCopyWarnings.UseVisualStyleBackColor = False
        ' 
        ' btnCloseWarnings
        ' 
        btnCloseWarnings.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCloseWarnings.AutoSizeMode = AutoSizeMode.GrowAndShrink
        btnCloseWarnings.BackColor = Color.FromArgb(CByte(255), CByte(128), CByte(128))
        btnCloseWarnings.Font = New Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCloseWarnings.Image = CType(resources.GetObject("btnCloseWarnings.Image"), Image)
        btnCloseWarnings.Location = New Point(775, 881)
        btnCloseWarnings.Name = "btnCloseWarnings"
        btnCloseWarnings.Size = New Size(169, 53)
        btnCloseWarnings.TabIndex = 26
        btnCloseWarnings.Text = "Close"
        btnCloseWarnings.UseVisualStyleBackColor = False
        ' 
        ' txtWarnings
        ' 
        txtWarnings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtWarnings.Location = New Point(16, 25)
        txtWarnings.Name = "txtWarnings"
        txtWarnings.ScrollBars = RichTextBoxScrollBars.Vertical
        txtWarnings.Size = New Size(923, 822)
        txtWarnings.TabIndex = 27
        txtWarnings.Text = ""
        ' 
        ' frmSessionWarnings
        ' 
        AutoScaleDimensions = New SizeF(12F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaption
        ClientSize = New Size(965, 959)
        Controls.Add(txtWarnings)
        Controls.Add(btnCloseWarnings)
        Controls.Add(btnCopyWarnings)
        MinimumSize = New Size(987, 1015)
        Name = "frmSessionWarnings"
        StartPosition = FormStartPosition.CenterScreen
        Text = "frmSessionWarnings"
        TopMost = True
        ResumeLayout(False)
    End Sub
    Friend WithEvents btnCopyWarnings As Button
    Friend WithEvents btnCloseWarnings As Button
    Friend WithEvents txtWarnings As RichTextBox
End Class
