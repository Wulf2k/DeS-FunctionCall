<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PS3Connector
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
        Me.rdbTMAPI = New System.Windows.Forms.RadioButton()
        Me.rdbCCAPI = New System.Windows.Forms.RadioButton()
        Me.btnDeSfnccall = New System.Windows.Forms.Button()
        Me.txtPS3IP = New System.Windows.Forms.TextBox()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rdbTMAPI
        '
        Me.rdbTMAPI.AutoSize = True
        Me.rdbTMAPI.Checked = True
        Me.rdbTMAPI.ForeColor = System.Drawing.Color.Red
        Me.rdbTMAPI.Location = New System.Drawing.Point(176, 49)
        Me.rdbTMAPI.Name = "rdbTMAPI"
        Me.rdbTMAPI.Size = New System.Drawing.Size(58, 17)
        Me.rdbTMAPI.TabIndex = 19
        Me.rdbTMAPI.TabStop = True
        Me.rdbTMAPI.Text = "TMAPI"
        Me.rdbTMAPI.UseVisualStyleBackColor = True
        '
        'rdbCCAPI
        '
        Me.rdbCCAPI.AutoSize = True
        Me.rdbCCAPI.ForeColor = System.Drawing.Color.Red
        Me.rdbCCAPI.Location = New System.Drawing.Point(96, 49)
        Me.rdbCCAPI.Name = "rdbCCAPI"
        Me.rdbCCAPI.Size = New System.Drawing.Size(56, 17)
        Me.rdbCCAPI.TabIndex = 18
        Me.rdbCCAPI.Text = "CCAPI"
        Me.rdbCCAPI.UseVisualStyleBackColor = True
        '
        'btnDeSfnccall
        '
        Me.btnDeSfnccall.BackColor = System.Drawing.Color.Black
        Me.btnDeSfnccall.Enabled = False
        Me.btnDeSfnccall.ForeColor = System.Drawing.Color.Red
        Me.btnDeSfnccall.Location = New System.Drawing.Point(85, 114)
        Me.btnDeSfnccall.Name = "btnDeSfnccall"
        Me.btnDeSfnccall.Size = New System.Drawing.Size(161, 23)
        Me.btnDeSfnccall.TabIndex = 17
        Me.btnDeSfnccall.Text = "Demon's Souls Function Caller"
        Me.btnDeSfnccall.UseVisualStyleBackColor = False
        '
        'txtPS3IP
        '
        Me.txtPS3IP.BackColor = System.Drawing.Color.Black
        Me.txtPS3IP.ForeColor = System.Drawing.Color.Red
        Me.txtPS3IP.Location = New System.Drawing.Point(110, 12)
        Me.txtPS3IP.Name = "txtPS3IP"
        Me.txtPS3IP.Size = New System.Drawing.Size(107, 20)
        Me.txtPS3IP.TabIndex = 16
        Me.txtPS3IP.Text = "192.168.1.232"
        '
        'btnDisconnect
        '
        Me.btnDisconnect.BackColor = System.Drawing.Color.Black
        Me.btnDisconnect.Enabled = False
        Me.btnDisconnect.ForeColor = System.Drawing.Color.Red
        Me.btnDisconnect.Location = New System.Drawing.Point(176, 83)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(157, 25)
        Me.btnDisconnect.TabIndex = 15
        Me.btnDisconnect.Text = "Disconnect"
        Me.btnDisconnect.UseVisualStyleBackColor = False
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.Black
        Me.btnConnect.ForeColor = System.Drawing.Color.Red
        Me.btnConnect.Location = New System.Drawing.Point(6, 83)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(164, 25)
        Me.btnConnect.TabIndex = 14
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'PS3Connector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(344, 152)
        Me.Controls.Add(Me.rdbTMAPI)
        Me.Controls.Add(Me.rdbCCAPI)
        Me.Controls.Add(Me.btnDeSfnccall)
        Me.Controls.Add(Me.txtPS3IP)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.btnConnect)
        Me.Name = "PS3Connector"
        Me.Text = "PS3 Connector"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdbTMAPI As System.Windows.Forms.RadioButton
    Friend WithEvents rdbCCAPI As System.Windows.Forms.RadioButton
    Friend WithEvents btnDeSfnccall As System.Windows.Forms.Button
    Friend WithEvents txtPS3IP As System.Windows.Forms.TextBox
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents btnConnect As System.Windows.Forms.Button

End Class
