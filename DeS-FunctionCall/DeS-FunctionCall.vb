﻿Imports System.Math
Imports PS3Lib

Public Class DeS_FunctionCall

    Dim PS3 As PS3API = PS3Connector.PS3
    Dim api As String = PS3Connector.api
    Dim funcLoc As UInteger = &H170D944&

    Dim funcDesc() As String = {}

    Dim codeopen As String = "7c0802a6" & "38800000" & "90830000"
    Dim codeclose As String = "7c0903a6" & "4e800421" & "7c0803a6" & "4970d516"

    Dim param1 As String
    Dim param2 As String
    Dim param3 As String
    Dim param4 As String
    Dim param5 As String

    Dim jumploc1 As String
    Dim jumploc2 As String

    Dim code As Byte()

    Private Function getMemByteArray(ByRef loc As UInteger, ByVal count As Integer) As Byte()
        Dim byt(count - 1) As Byte
        PS3.GetMemory(loc, byt)
        Return byt
    End Function
    Private Function GetStr(ByVal loc As UInteger) As String
        Dim buildstr As String = ""
        Dim barr(80) As Byte

        barr = getMemByteArray(loc, 81)

        For i = 0 To 80
            If barr(i) > 0 Then
                buildstr = buildstr & Convert.ToChar(barr(i))
            Else
                Exit For
            End If
        Next
        Return buildstr
    End Function
    Private Function GetUnicodeStr(ByVal loc As UInteger) As String
        Dim buildstr As String = ""
        Dim barr(160) As Byte

        barr = getMemByteArray(loc, 161)

        For i = 0 To 159
            If barr(i) > 0 Then
                buildstr = buildstr & Convert.ToChar(barr(i))
            Else
                If barr(i + 1) = 0 Then Exit For
            End If
        Next
        Return buildstr
    End Function
    Private Function ReverseFourBytes(ByVal bytes() As Byte)
        Return {bytes(3), bytes(2), bytes(1), bytes(0)}
    End Function
    Private Function ReverseTwoBytes(ByVal bytes() As Byte)
        Return {bytes(1), bytes(0)}
    End Function
    Private Function OneByteAnd(ByVal loc As UInteger, ByVal cmp As Integer)
        Dim b As Byte
        b = getMemByteArray(loc, 1)(0)
        Return b And cmp
    End Function
    Private Function Four2UInteger(ByVal loc As UInteger) As UInteger
        Dim bytes(3) As Byte
        bytes = getMemByteArray(loc, 4)
        Return BitConverter.ToUInt32(ReverseFourBytes(bytes), 0)
    End Function
    Private Function Four2SInteger(ByVal loc As UInteger) As Long
        Dim bytes(3) As Byte
        bytes = getMemByteArray(loc, 4)
        Return BitConverter.ToInt32(ReverseFourBytes(bytes), 0)
    End Function
    Private Function Four2Float(ByVal loc As UInteger) As Single
        Dim bytes(3) As Byte
        bytes = getMemByteArray(loc, 4)
        Return BitConverter.ToSingle(ReverseFourBytes(bytes), 0)
    End Function
    Private Function Two2UInteger(ByVal loc As UInteger) As UInt16
        Dim bytes(1) As Byte
        bytes = getMemByteArray(loc, 2)
        Return BitConverter.ToUInt16(ReverseTwoBytes(bytes), 0)
    End Function
    Private Function TXTSingle2Hex(ByVal val As String) As Byte()
        If IsNumeric(val) Then
            Return ReverseFourBytes(BitConverter.GetBytes(Convert.ToSingle(val)))
        Else
            Return {0, 0, 0, 0}
        End If
    End Function
    Private Sub TXTHex2Four(ByVal loc As UInteger, val As String)
        If val.Length > 0 Then
            PS3.SetMemory(loc, ReverseFourBytes(BitConverter.GetBytes(Convert.ToInt32(val, 16))))
        End If
    End Sub
    Private Sub TXTUInteger2Four(ByVal loc As UInteger, ByRef txt As String)
        Dim txtval As UInteger = Val(txt)
        Dim byt(3) As Byte
        byt(0) = Convert.ToByte((Math.Floor(txtval / &H1000000)) Mod &H100)
        byt(1) = Convert.ToByte((Math.Floor(txtval / &H10000)) Mod &H100)
        byt(2) = Convert.ToByte((Math.Floor(txtval / &H100)) Mod &H100)
        byt(3) = Convert.ToByte(txtval Mod &H100)
        PS3.SetMemory(loc, byt)
    End Sub
    Private Sub TXTSInteger2Four(ByVal loc As UInteger, ByRef txt As String)
        Dim txtval As Integer = Val(txt)
        Dim byt(3) As Byte
        byt(0) = Convert.ToByte((Math.Floor(txtval / &H1000000)) Mod &H100)
        byt(1) = Convert.ToByte((Math.Floor(txtval / &H10000)) Mod &H100)
        byt(2) = Convert.ToByte((Math.Floor(txtval / &H100)) Mod &H100)
        byt(3) = Convert.ToByte(txtval Mod &H100)
        PS3.SetMemory(loc, byt)
    End Sub
    Private Function padHex(ByVal hex As String, ByVal chars As Integer) As String
        For i = 0 To chars
            If hex.Length < chars Then hex = "0" & hex
        Next
        Return hex
    End Function

    Private Function HexToBArr(ByVal hexstring As String) As Byte()
        Dim length As Integer = hexString.Length
        Dim upperBound As Integer = length \ 2
        If length Mod 2 = 0 Then
            upperBound -= 1
        Else
            hexString = "0" & hexString
        End If
        Dim bytes(upperBound) As Byte
        For i As Integer = 0 To upperBound
            bytes(i) = Convert.ToByte(hexString.Substring(i * 2, 2), 16)
        Next
        Return bytes
    End Function
    Private Sub AddDesc(ByVal name As String, ByVal desc As String, ByVal params As String)
        Array.Resize(funcDesc, funcDesc.Length + 2)

        funcDesc(funcDesc.Length - 2) = name
        funcDesc(funcDesc.Length - 1) = desc & Environment.NewLine & Environment.NewLine & params
        cmbFunc.Items.Add(name)
    End Sub


    Sub initFuncDesc()
        AddDesc("DisableHpGauge", "Turn off floating HP bar.", "Parameters - Creature ID, 0/1.")
        AddDesc("DisableMapHit", "Turn off Map collisions for creature.", "Parameters - Creature ID, 0/1.")
        AddDesc("EraseEventSpecialEffect_2", "Remove special effect from creature.", "Parameters - Creature ID, Special Effect ID.")
        AddDesc("PlayAnimation", "Force selected creature into specific animation.", "Parameters - Creature ID, Animation ID.")
        AddDesc("ReturnMapSelect", "Return to main menu.", "No parameters.")
        AddDesc("SaveRequest", "Save the game.", "No Parameters.")
        AddDesc("SaveRequest_Profile", "Save the profile.  (Unsure if different from regular SaveRequest).", "No Parameters.")
        AddDesc("SetDeadMode", "Prevent creature from dying.", "Parameters - Creature ID, 0/1.")
        AddDesc("SetDisableGravity", "Enable or disable gravity for target creature.", "Parameters - Creature ID, 0/1.")
        AddDesc("SetEventSpecialEffect_2", "Apply special effect to creature.", "Parameters - Creature ID, Special Effect ID.")
        AddDesc("Warp", "Warp creature to area.", "Parameters - Creature ID, Warp ID.")
        AddDesc("WarpNextStage", "Warp player to new map.", "Parameters - World ID, Level ID, Area ID, Subarea ID, Warp ID")
    End Sub

    Private Sub DeS_FunctionCall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        initFuncDesc()
    End Sub
    Private Sub DeS_FunctionCall_Close(sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PS3Connector.Close()
    End Sub

    Private Sub btnHook_Click(sender As Object, e As EventArgs) Handles btnHook.Click
        code = HexToBArr("f821ff81" & _
            "7c0802a6" & _
            "f8c10038" & _
            "f8e10030" & _
            "f9010028" & _
            "f9410020" & _
            "f9810018" & _
            "f9210040" & _
            "f8010048" & _
            "f8410050" & _
            "f8610058" & _
            "f8810060" & _
            "f8a10068" & _
            "fbc10070" & _
            "fbe10078" & _
            "3c600170" & _
            "6063d940" & _
            "80830000" & _
            "2f840000" & _
            "419e0008" & _
            "48000439" & _
            "e8010048" & _
            "ebe10078" & _
            "ebc10070" & _
            "e8a10068" & _
            "e8810060" & _
            "e8610058" & _
            "e8410050" & _
            "7c0803a6" & _
            "e9210040" & _
            "e9810018" & _
            "e9410020" & _
            "e9010028" & _
            "e8e10030" & _
            "e8c10038" & _
            "38210080" & _
            "4e800020")
        PS3.SetMemory(&H170D4C0, code)

        code = HexToBArr("49467129")
        PS3.SetMemory(&H2A6398, code)

    End Sub
    Private Sub SetJump(ByVal jumploc As String)
        jumploc1 = Microsoft.VisualBasic.Left(jumploc, 4)
        jumploc2 = Microsoft.VisualBasic.Right(jumploc, 4)
    End Sub
    Private Sub triggerFunc()
        PS3.SetMemory(&H170D943, {1})
    End Sub
    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click

        param1 = Hex(Convert.ToInt16(Val(txtParam1.Text)))
        param1 = padHex(param1, 4)
        param2 = Hex(Convert.ToInt16(Val(txtParam2.Text)))
        param2 = padHex(param2, 4)
        param3 = Hex(Convert.ToInt16(Val(txtParam3.Text)))
        param3 = padHex(param3, 4)
        param4 = Hex(Convert.ToInt16(Val(txtParam4.Text)))
        param4 = padHex(param4, 4)
        param5 = Hex(Convert.ToInt16(Val(txtParam5.Text)))
        param5 = padHex(param5, 4)

        Select Case cmbFunc.Text
            Case "DisableHpGauge"
                SetJump("004443d8")
            Case "DisableMapHit"
                SetJump("004430d0")
            Case "EraseEventSpecialEffect_2"
                SetJump("00446b58")
            Case "PlayAnimation"
                SetJump("00443da0")
            Case "ReturnMapSelect"
                SetJump("0043f980")
            Case "SaveRequest"
                SetJump("004418f0")
            Case "SaveRequest_Profile"
                SetJump("004418b8")
            Case "SetDeadMode"
                SetJump("0044ab30")
            Case "SetDisableGravity"
                SetJump("00446a48")
            Case "SetEventSpecialEffect_2"
                SetJump("00446c10")
            Case "Warp"
                SetJump("00443f40")
            Case "WarpNextStage"
                SetJump("00451430")
        End Select

        code = HexToBArr(codeopen & _
         "3880" & param1 & _
         "38a0" & param2 & _
         "38c0" & param3 & _
         "38e0" & param4 & _
         "3900" & param5 & _
         "3c00" & jumploc1 & _
         "6000" & jumploc2 & _
         codeclose)

        PS3.SetMemory(funcLoc, code)
        triggerFunc()
    End Sub
    Private Sub cmbFunc_Change(sender As Object, e As EventArgs) Handles cmbFunc.TextChanged
        Dim index = Array.IndexOf(funcDesc, cmbFunc.Text)

        If index > -1 Then
            txtDescription.Text = funcDesc(index + 1)
        Else
            txtDescription.Text = ""
        End If

    End Sub
End Class