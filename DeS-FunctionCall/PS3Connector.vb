Imports PS3Lib

Public Class PS3Connector

    Public Shared PS3 As New PS3API
    Public Shared api As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PS3.ChangeAPI(SelectAPI.TargetManager)
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        If api = "CCAPI" Then
            PS3.ChangeAPI(SelectAPI.ControlConsole)

            If PS3.ConnectTarget(txtPS3IP.Text) Then
                PS3.CCAPI.Notify(11, "Connected")
                If PS3.AttachProcess() Then
                    PS3.CCAPI.Notify(11, "Attached")
                    btnConnect.Enabled = False
                    btnDisconnect.Enabled = True
                    btnDeSfnccall.Enabled = True
                    txtPS3IP.Enabled = False
                Else
                    MsgBox("Failed to attach")
                End If
            Else
                MsgBox("No connection")
            End If
        End If

        If api = "TMAPI" Then
            PS3.ChangeAPI(SelectAPI.TargetManager)
            If PS3.TMAPI.ConnectTarget(txtPS3IP.Text) Then
                If PS3.AttachProcess() Then
                    btnConnect.Enabled = False
                    btnDisconnect.Enabled = True
                    btnDeSfnccall.Enabled = True
                    txtPS3IP.Enabled = False
                Else
                    MsgBox("Failed to attach")
                End If
            Else
                MsgBox("No connection")
            End If
        End If

    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        PS3.DisconnectTarget()
        btnConnect.Enabled = True
        btnDisconnect.Enabled = False
        btnDeSfnccall.Enabled = False
        txtPS3IP.Enabled = True
    End Sub

    Private Sub btnDeSfnccall_Click(sender As Object, e As EventArgs) Handles btnDeSfnccall.Click
        Dim DeS As DeS_FunctionCall
        DeS = New DeS_FunctionCall
        DeS.Show()
        DeS = Nothing
        Me.Hide()

    End Sub

    Private Sub rdbTMAPI_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTMAPI.CheckedChanged
        If rdbTMAPI.Checked Then
            api = "TMAPI"
        End If

    End Sub
    Private Sub rdbCCAPI_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCCAPI.CheckedChanged
        If rdbCCAPI.Checked Then api = "CCAPI"
    End Sub
End Class
