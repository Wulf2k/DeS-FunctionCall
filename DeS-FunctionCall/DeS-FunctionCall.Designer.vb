﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeS_FunctionCall
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
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.cmbFunc = New System.Windows.Forms.ComboBox()
        Me.btnHook = New System.Windows.Forms.Button()
        Me.txtParam1 = New System.Windows.Forms.TextBox()
        Me.txtParam2 = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtParam4 = New System.Windows.Forms.TextBox()
        Me.txtParam3 = New System.Windows.Forms.TextBox()
        Me.txtParam5 = New System.Windows.Forms.TextBox()
        Me.lblParam1 = New System.Windows.Forms.Label()
        Me.lblParam2 = New System.Windows.Forms.Label()
        Me.lblParam3 = New System.Windows.Forms.Label()
        Me.lblParam4 = New System.Windows.Forms.Label()
        Me.lblParam5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(442, 132)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(75, 23)
        Me.btnExecute.TabIndex = 0
        Me.btnExecute.Text = "Execute"
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        'cmbFunc
        '
        Me.cmbFunc.FormattingEnabled = True
        Me.cmbFunc.Location = New System.Drawing.Point(13, 12)
        Me.cmbFunc.Name = "cmbFunc"
        Me.cmbFunc.Size = New System.Drawing.Size(210, 21)
        Me.cmbFunc.TabIndex = 2
        '
        'btnHook
        '
        Me.btnHook.Location = New System.Drawing.Point(13, 132)
        Me.btnHook.Name = "btnHook"
        Me.btnHook.Size = New System.Drawing.Size(75, 23)
        Me.btnHook.TabIndex = 1
        Me.btnHook.Text = "Insert Hook"
        Me.btnHook.UseVisualStyleBackColor = True
        '
        'txtParam1
        '
        Me.txtParam1.Location = New System.Drawing.Point(243, 12)
        Me.txtParam1.Name = "txtParam1"
        Me.txtParam1.Size = New System.Drawing.Size(50, 20)
        Me.txtParam1.TabIndex = 3
        Me.txtParam1.Visible = False
        '
        'txtParam2
        '
        Me.txtParam2.Location = New System.Drawing.Point(299, 12)
        Me.txtParam2.Name = "txtParam2"
        Me.txtParam2.Size = New System.Drawing.Size(50, 20)
        Me.txtParam2.TabIndex = 4
        Me.txtParam2.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(13, 56)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(504, 70)
        Me.txtDescription.TabIndex = 8
        '
        'txtParam4
        '
        Me.txtParam4.Location = New System.Drawing.Point(411, 12)
        Me.txtParam4.Name = "txtParam4"
        Me.txtParam4.Size = New System.Drawing.Size(50, 20)
        Me.txtParam4.TabIndex = 6
        Me.txtParam4.Visible = False
        '
        'txtParam3
        '
        Me.txtParam3.Location = New System.Drawing.Point(355, 12)
        Me.txtParam3.Name = "txtParam3"
        Me.txtParam3.Size = New System.Drawing.Size(50, 20)
        Me.txtParam3.TabIndex = 5
        Me.txtParam3.Visible = False
        '
        'txtParam5
        '
        Me.txtParam5.Location = New System.Drawing.Point(467, 12)
        Me.txtParam5.Name = "txtParam5"
        Me.txtParam5.Size = New System.Drawing.Size(50, 20)
        Me.txtParam5.TabIndex = 7
        Me.txtParam5.Visible = False
        '
        'lblParam1
        '
        Me.lblParam1.AutoSize = True
        Me.lblParam1.Location = New System.Drawing.Point(256, 35)
        Me.lblParam1.Name = "lblParam1"
        Me.lblParam1.Size = New System.Drawing.Size(10, 13)
        Me.lblParam1.TabIndex = 9
        Me.lblParam1.Text = "."
        Me.lblParam1.Visible = False
        '
        'lblParam2
        '
        Me.lblParam2.AutoSize = True
        Me.lblParam2.Location = New System.Drawing.Point(312, 35)
        Me.lblParam2.Name = "lblParam2"
        Me.lblParam2.Size = New System.Drawing.Size(10, 13)
        Me.lblParam2.TabIndex = 10
        Me.lblParam2.Text = "."
        Me.lblParam2.Visible = False
        '
        'lblParam3
        '
        Me.lblParam3.AutoSize = True
        Me.lblParam3.Location = New System.Drawing.Point(368, 35)
        Me.lblParam3.Name = "lblParam3"
        Me.lblParam3.Size = New System.Drawing.Size(10, 13)
        Me.lblParam3.TabIndex = 11
        Me.lblParam3.Text = "."
        Me.lblParam3.Visible = False
        '
        'lblParam4
        '
        Me.lblParam4.AutoSize = True
        Me.lblParam4.Location = New System.Drawing.Point(425, 35)
        Me.lblParam4.Name = "lblParam4"
        Me.lblParam4.Size = New System.Drawing.Size(10, 13)
        Me.lblParam4.TabIndex = 12
        Me.lblParam4.Text = "."
        Me.lblParam4.Visible = False
        '
        'lblParam5
        '
        Me.lblParam5.AutoSize = True
        Me.lblParam5.Location = New System.Drawing.Point(480, 35)
        Me.lblParam5.Name = "lblParam5"
        Me.lblParam5.Size = New System.Drawing.Size(10, 13)
        Me.lblParam5.TabIndex = 13
        Me.lblParam5.Text = "."
        Me.lblParam5.Visible = False
        '
        'DeS_FunctionCall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 160)
        Me.Controls.Add(Me.lblParam5)
        Me.Controls.Add(Me.lblParam4)
        Me.Controls.Add(Me.lblParam3)
        Me.Controls.Add(Me.lblParam2)
        Me.Controls.Add(Me.lblParam1)
        Me.Controls.Add(Me.txtParam5)
        Me.Controls.Add(Me.txtParam4)
        Me.Controls.Add(Me.txtParam3)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.txtParam2)
        Me.Controls.Add(Me.txtParam1)
        Me.Controls.Add(Me.btnHook)
        Me.Controls.Add(Me.cmbFunc)
        Me.Controls.Add(Me.btnExecute)
        Me.Name = "DeS_FunctionCall"
        Me.Text = "Wulf's Demon's Souls Function Caller"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents cmbFunc As System.Windows.Forms.ComboBox
    Friend WithEvents btnHook As System.Windows.Forms.Button
    Friend WithEvents txtParam1 As System.Windows.Forms.TextBox
    Friend WithEvents txtParam2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtParam4 As System.Windows.Forms.TextBox
    Friend WithEvents txtParam3 As System.Windows.Forms.TextBox
    Friend WithEvents txtParam5 As System.Windows.Forms.TextBox
    Friend WithEvents lblParam1 As System.Windows.Forms.Label
    Friend WithEvents lblParam2 As System.Windows.Forms.Label
    Friend WithEvents lblParam3 As System.Windows.Forms.Label
    Friend WithEvents lblParam4 As System.Windows.Forms.Label
    Friend WithEvents lblParam5 As System.Windows.Forms.Label
End Class
