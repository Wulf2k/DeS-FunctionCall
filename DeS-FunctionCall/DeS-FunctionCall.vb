﻿Imports System.Math
Imports PS3Lib

Public Class DeS_FunctionCall

    Dim PS3 As PS3API = PS3Connector.PS3
    Dim api As String = PS3Connector.api
    Dim funcLoc As UInteger = &H170D958&

    Dim funcDesc() As String = {}

    Dim codeopen As String = "7c0802a6" & "38800000" & "90830000"
    Dim codeclose As String = "7c0903a6" & "4e800421" & "3c800170" & "6084d950" & "90640000" & "7c0803a6" & "4970d52a"

    Dim cllTxtParam() As TextBox
    Dim cllLblParam() As Label
    Dim cllIntParam() As String
    Dim cllFltParam() As String

    Dim intParam1, intParam2, intParam3, intParam4, intParam5 As String
    Dim fltParam1, fltParam2, fltParam3, fltParam4, fltParam5 As String

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
    Private Function BArrToStr(ByVal bytes() As Byte)
        Dim tmpstr As String = ""
        For i = 0 To bytes.Length - 1
            tmpstr = tmpstr + padHex(Hex(bytes(i)), 2)
        Next
        Return tmpstr
    End Function


    Private Function HexToBArr(ByVal hexstring As String) As Byte()
        Dim length As Integer = hexstring.Length
        Dim upperBound As Integer = length \ 2
        If length Mod 2 = 0 Then
            upperBound -= 1
        Else
            hexstring = "0" & hexstring
        End If
        Dim bytes(upperBound) As Byte
        For i As Integer = 0 To upperBound
            bytes(i) = Convert.ToByte(hexstring.Substring(i * 2, 2), 16)
        Next
        Return bytes
    End Function
    Private Sub AddDesc(ByVal name As String, ByVal desc As String, ByVal params As String, numParams As String)
        Array.Resize(funcDesc, funcDesc.Length + 3)

        funcDesc(funcDesc.Length - 3) = name
        funcDesc(funcDesc.Length - 2) = desc & Environment.NewLine & Environment.NewLine & params
        funcDesc(funcDesc.Length - 1) = numParams
        cmbFunc.Items.Add(name)
    End Sub
    Private Sub EnableParams(ByVal numParams As String)
        For i = 0 To 4
            If i <= numParams.Length - 1 Then

                cllTxtParam(i).Visible = True
                cllLblParam(i).Visible = True
                Select Case numParams(i)
                    Case "s"
                        cllLblParam(i).Text = "s32"
                    Case "f"
                        cllLblParam(i).Text = "f32"
                End Select
            Else
                cllTxtParam(i).Visible = False
                cllLblParam(i).Visible = False
            End If
        Next



    End Sub

    Sub initFuncDesc()
        AddDesc("AddClearCount", "Increases NG+ count.", "No Parameters.", "")
        AddDesc("AddDeathCount", "Increases death count.", "No Parameters.", "")
        AddDesc("AddHelpWhiteGhost", "Increases white ghosts helped count.", "No Parameters.", "")
        AddDesc("AddInventoryItem", "Add an item to the player's inventory.", "Parameters - Item ID, Category, Count.  (0 = Weapons, 268435456 = Armor, 536870912 = Accessories, 1073741824 = Goods.)", "sss")
        AddDesc("AddKillBlackGhost", "Increases number of black phantoms killed.", "No Parameters.", "")
        AddDesc("AddTrueDeathCount", "Increases your number of TrueDeaths (inactive feature).", "No Parameters.", "")
        AddDesc("CamReset", "Reset camera to default.", "Creature ID, 0/1.", "ss")
        AddDesc("ChangeModel", "Unknown Function", "Parameters - Object ID, Model ID", "ss")
        AddDesc("ClearBossGauge", "Remove large boss HP bars.", "No Parameters.", "")
        AddDesc("CloseMenu", "Close the Start menu.", "No Parameters.", "")
        AddDesc("CreateCamSfx", "Unknown function.", "Parameters - Unknown ID, Unknown value (0 in examples).", "ss")
        AddDesc("DisableHpGauge", "Turn off floating HP bar.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("DisableMapHit", "Turn off map collisions for creature.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("DisableMove", "Disable creature movement.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("EnableLogic", "Enable or disable logic on target creature.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("EraseEventSpecialEffect_2", "Remove special effect from creature.", "Parameters - Creature ID, Special Effect ID.", "ss")
        AddDesc("ForceDead", "Force a creature to die.", "Parameters - Creature ID.", "s")
        AddDesc("GetBlockId", "Return current area's block ID.", "No Parameters.", "")
        AddDesc("GetBossCount", "Unsure on usage.", "No Parameters.", "")
        AddDesc("GetClearCount", "Return number of times game has been cleared.", "No Parameters.", "")
        AddDesc("GetFloorMaterial", "Return the material type of floor under target creature.", "Parameters - Creature ID", "s")
        AddDesc("GetGlobalQWC", "Return queued QWC value.", "Parameters - Unknown ID (0-2)", "s")
        AddDesc("GetHostPlayerNo", "Get Host's Creature ID.", "No Parameters.", "")
        AddDesc("GetItem", "Give item to player.  (Appears non-functional.)", "Item ID, Category.", "ss")
        AddDesc("GetLocalPlayerChrType", "Gets local player's ghost type.", "No Parameters.", "")
        AddDesc("GetLocalPlayerId", "Gets ID of local player.", "No Parameters.", "")
        AddDesc("GetLocalPlayerSoulLv", "Gets soul level of local player.", "No Parameters.", "")
        AddDesc("GetLocalQWC", "Gets local tendency of target area.", "Parameters - Unknown ID.", "s")
        AddDesc("GetTargetChrID", "Gets target creature ID for specified creature.", "Parameters - Creature ID", "s")
        AddDesc("GetQWC", "Gets the specified tendency.", "Parameters - Unsure (0-2).", "s")
        AddDesc("IsOnline", "Returns online status.", "No Parameters.", "")
        AddDesc("IsOnlineMode", "Returns status of online mode.", "No Parameters.", "")
        AddDesc("LockSession", "Locks session, disabling MP.", "No Parameters", "")
        AddDesc("NotNetMessage_begin", "Unknown", "No Parameters.", "")
        AddDesc("NotNetMessage_end", "Unknown", "No Parameters.", "")
        AddDesc("OpenCampMenu", "Open the Start menu.", "No Parameters.", "")
        AddDesc("PauseTutorial", "Unknown Function.", "No Parameters.", "")
        AddDesc("PlayAnimation", "Force selected creature into specific animation.", "Parameters - Creature ID, Animation ID.", "ss")
        AddDesc("PlayLoopAnimation", "Force selected creature into a loop of a specific animation", "Parameters - CreatureID, AnimationID", "ss")
        AddDesc("RecvGlobalQwc", "Set queued tendencies to be active.", "No Parameters.", "")
        AddDesc("RequestUnlockTrophy", "Unlocks specified trophy.", "Parameters - Trophy ID.", "s")
        AddDesc("ReturnMapSelect", "Return to main menu.", "No parameters.", "")
        AddDesc("SaveRequest", "Save the game.", "No Parameters.", "")
        AddDesc("SaveRequest_Profile", "Save the profile.  (Unsure if different from regular SaveRequest).", "No Parameters.", "")
        AddDesc("SetBallista", "Unknown function.", "Parameters - Unknown ID, Creature ID.", "ss")
        AddDesc("SetBossGauge", "Assign creature's HP bar to large boss health gauge.", "Parameters - Creature ID, Bar #, Name ID.", "sss")
        AddDesc("SetColiEnable", "Enable or disable creature collision for target object or creature.", "Parameters - Object/Creature ID, 0/1.", "ss")
        AddDesc("SetDeadMode", "Prevent creature from dying.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("SetDisable", "Remove target creature.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("SetDisableBackread_forEvent", "Part of disabling a creature, exact function uncertain.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("SetDisableGravity", "Enable or disable gravity for target creature.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("SetDrawEnable", "Enable or disable drawing target object or creature.", "Parameters - Object/Creature ID, 0/1.", "ss")
        AddDesc("SetEventSpecialEffect_2", "Apply special effect to creature.", "Parameters - Creature ID, Special Effect ID.", "ss")
        AddDesc("SetHp", "Set creature's current HP.", "Parameters - Creature ID, 0-1 percentage of HP.", "sf")
        AddDesc("SetIgnoreHit", "Disable all of creature's collision.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("SetMenuBrake", "Disable the start menu.  (Is immediately overridden.)", "No Parameters.", "")
        AddDesc("SetPartyRestrictNum", "Sets maximum party size.", "Parameters - Max players.", "ss")
        AddDesc("SetSubMenuBrake", "Disable Select menu in multiplayer.", "Parameters - 0/1.", "ss")
        AddDesc("SetSuperArmor", "Enable Super Armor on creature.", "Parameters - Creature ID, 0/1.", "ss")
        AddDesc("SetTeamType", "Set creature's team type.", "Parameters - Creature ID, Team ID.", "ss")
        AddDesc("SetTextEffect", "Display selcted text banner across screen.", "Parameters - TextEffect ID.", "s")
        AddDesc("ShowGenDialog", "Show Dialog Box.", "Parameters - Message ID, Prompt ID, Prompt Type, Unknown.", "ssss")
        AddDesc("StopPlayer", "Disable player movement.  Use EnableLogic to re-enable.", "No Parameters.", "")
        AddDesc("SummonBlackRequest", "Believed to request a black invader, like Old Monk.", "Parameters - Area ID.", "s")
        AddDesc("SummonedMapReload", "Unknown Effect.", "No Parameters.", "")
        AddDesc("Tutorial_begin", "Enter Tutorial mode.  Disable autosaving.", "No Parameters.", "")
        AddDesc("Tutorial_end", "Exit Tutorial mode.  Does not seem to re-enable autosaving.", "No Parameters.", "")
        AddDesc("UnLockSession", "Re-enable MP capabilities.", "No Parameters.", "")
        AddDesc("Warp", "Warp creature to area.", "Parameters - Creature ID, Warp ID.", "ss")
        AddDesc("WarpSelfBloodMark", "Warp yourself to the map containing your bloodstain.  Parameter appears to be ignored.", "Parameters - 0/1.", "s")
        AddDesc("WarpNextStage", "Warp player to new map.", "Parameters - World ID, Level ID, Area ID, Subarea ID, Warp ID", "sssss")
        AddDesc("WarpRestart", "Warp creature to area.", "Parameters - Creature ID, Unknown ID.", "ss")
    End Sub

    Private Sub DeS_FunctionCall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cllTxtParam = {txtParam1, txtParam2, txtParam3, txtParam4, txtParam5}
        cllLblParam = {lblParam1, lblParam2, lblParam3, lblParam4, lblParam5}
        cllIntParam = {intParam1, intParam2, intParam3, intParam4, intParam5}
        cllFltParam = {fltParam1, fltParam2, fltParam3, fltParam4, fltParam5}
        initFuncDesc()
    End Sub
    Private Sub DeS_FunctionCall_Close(sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PS3Connector.Close()
    End Sub

    Private Sub btnHook_Click(sender As Object, e As EventArgs) Handles btnHook.Click
        code = HexToBArr("f821ff61" & _
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
            "d8210080" & _
            "d8410088" & _
            "d8610090" & _
            "d8810098" & _
              "3c600170" & _
              "60000000" & _
            "6063d940" & _
            "80830000" & _
            "2f840000" & _
            "419e0008" & _
            "48000435" & _
            "c8210080" & _
            "c8410088" & _
            "c8610090" & _
            "c8810098" & _
              "e8010048" & _
              "60000000" & _
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
            "382100a0" & _
            "4e800020")
        PS3.SetMemory(&H170D4C0, code)
        REM "d8a100a0" & _
        REM "c8a100a0" & _
        REM Nopped above due to stack issues


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
        Select Case cmbFunc.Text
            Case "AddClearCount"
                SetJump("00444c88")
            Case "AddDeathCount"
                SetJump("00444298")
            Case "AddHelpWhiteGhost"
                SetJump("0043f8a8")
            Case "AddInventoryItem"
                SetJump("00447748")
            Case "AddKillBlackGhost"
                SetJump("0043f880")
            Case "AddTrueDeathCount"
                SetJump("00444250")
            Case "CamReset"
                SetJump("004432e8")
            Case "ChangeModel"
                SetJump("0044a960")
            Case "ClearBossGauge"
                SetJump("0043e490")
            Case "CloseMenu"
                SetJump("0043f8d0")
            Case "CreateCamSfx"
                SetJump("00441ee8")
            Case "DisableHpGauge"
                SetJump("004443d8")
            Case "DisableMapHit"
                SetJump("004430d0")
            Case "EnableLogic"
                SetJump("00443838")
            Case "DisableMove"
                SetJump("00443128")
            Case "EraseEventSpecialEffect_2"
                SetJump("00446b58")
            Case "ForceDead"
                SetJump("00446478")
            Case "GetBlockId"
                SetJump("0043ed60")
            Case "GetBossCount"
                SetJump("0043e5b8")
            Case "GetClearCount"
                SetJump("00444cd0")
            Case "GetFloorMaterial"
                SetJump("00441340")
            Case "GetGlobalQWC"
                SetJump("004427e8")
            Case "GetHostPlayerNo"
                SetJump("00443f90")
            Case "GetItem"
                SetJump("0043fc58")
            Case "GetLocalPlayerChrType"
                SetJump("0043e660")
            Case "GetLocalPlayerID"
                SetJump("00439160")
            Case "GetLocalPlayerSoulLv"
                SetJump("00441710")
            Case "GetLocalQWC"
                SetJump("00442840")
            Case "GetTargetChrID"
                SetJump("00446ed0")
            Case "GetQWC"
                SetJump("00442898")
            Case "IsOnline"
                SetJump("00447330")
            Case "IsOnlineMode"
                SetJump("0043e3e8")
            Case "LockSession"
                SetJump("00441838")
            Case "NotNetMessage_begin"
                SetJump("00439198")
            Case "NotNetMessage_end"
                SetJump("004391a8")
            Case "OpenCampMenu"
                SetJump("0043ece8")
            Case "PauseTutorial"
                SetJump("00441e70")
            Case "PlayAnimation"
                SetJump("00443da0")
            Case "PlayLoopAnimation"
                SetJump("00443c40")
            Case "RecvGlobalQwc"
                SetJump("00441308")
            Case "RequestUnlockTrophy"
                SetJump("004446d8")
            Case "ReturnMapSelect"
                SetJump("0043f980")
            Case "SaveRequest"
                SetJump("004418f0")
            Case "SaveRequest_Profile"
                SetJump("004418b8")
            Case "SetBallista"
                SetJump("00447dc0")
            Case "SetBossGauge"
                SetJump("00441928")
            Case "SetColiEnable"
                SetJump("00444030")
            Case "SetDeadMode"
                SetJump("0044ab30")
            Case "SetDisable"
                SetJump("00446788")
            Case "SetDisableBackread_forEvent"
                SetJump("004449a8")
            Case "SetDisableGravity"
                SetJump("00446a48")
            Case "SetDrawEnable"
                SetJump("004440d8")
            Case "SetEventSpecialEffect_2"
                SetJump("00446c10")
            Case "SetHp"
                SetJump("004477b8")
            Case "SetIgnoreHit"
                SetJump("00443080")
            Case "SetMenuBrake"
                SetJump("0043f8f8")
            Case "SetPartyRestrictNum"
                SetJump("00444910")
            Case "SetSubMenuBrake"
                SetJump("0043e300")
            Case "SetSuperArmor"
                SetJump("00442fd8")
            Case "SetTeamType"
                SetJump("00444ba0")
            Case "SetTextEffect"
                SetJump("0043e4e0")
            Case "ShowGenDialog"
                SetJump("0043e800")
            Case "StopPlayer"
                SetJump("00441dc8")
            Case "SummonBlackRequest"
                SetJump("00444770")
            Case "SummonedMapReload"
                SetJump("00441ab8")
            Case "Tutorial_begin"
                SetJump("0043ed38")
            Case "UnLockSession"
                SetJump("0044a2b8")
            Case "Warp"
                SetJump("00443f40")
            Case "WarpSelfBloodMark"
                SetJump("00451518")
            Case "WarpNextStage"
                SetJump("00451430")
            Case "WarpRestart"
                SetJump("00443e98")
        End Select

        Dim intNum = 0
        Dim fltNum = 0

        For i = 0 To 4
            cllIntParam(i) = "00000000"
            cllFltParam(i) = "00000000"
            Select Case cllLblParam(i).Text
                Case "s32"
                    cllIntParam(intNum) = Hex(Convert.ToInt32(Val(cllTxtParam(i).Text)))
                    cllIntParam(intNum) = padHex(cllIntParam(intNum), 8)
                    intNum += 1
                Case "f32"
                    cllFltParam(fltNum) = BArrToStr(ReverseFourBytes(BitConverter.GetBytes(Convert.ToSingle(cllTxtParam(i).Text))))
                    fltNum += 1
            End Select
        Next

        Dim tmpcode As String = ""

        REM
        For i = 0 To 3
            tmpcode = tmpcode & "3c80" & Microsoft.VisualBasic.Left(cllFltParam(i), 4) &
                "6084" & Microsoft.VisualBasic.Right(cllFltParam(i), 4) & _
                "9083" & padHex(Hex(i * 4 + 4), 4) & _
                "c0" & Hex(i * 2 + 2) & "3" & padHex(Hex(i * 4 + 4), 4)
        Next

        For i = 0 To 4
            tmpcode = tmpcode & "3" & Hex(&HC8 + (i * 2)) & "0" & Microsoft.VisualBasic.Left(cllIntParam(i), 4) & _
                "6" & padHex(Hex(8 + i * 2), 2) & Hex(i + 4) & Microsoft.VisualBasic.Right(cllIntParam(i), 4)
        Next

        code = HexToBArr(codeopen & _
                         tmpcode & _
                         "3c00" & jumploc1 & _
                         "6000" & jumploc2 & _
                         codeclose)
        PS3.SetMemory(funcLoc, code)
        triggerFunc()

        Threading.Thread.Sleep(500)

        txtResult.Text = Four2SInteger(&H170D950&)
        TXTSInteger2Four(&H170D950&, 0)
    End Sub
    Private Sub cmbFunc_Change(sender As Object, e As EventArgs) Handles cmbFunc.TextChanged
        Dim index = Array.IndexOf(funcDesc, cmbFunc.Text)

        If index > -1 Then
            txtDescription.Text = funcDesc(index + 1)
            EnableParams(funcDesc(index + 2))
        Else
            txtDescription.Text = ""
        End If

    End Sub
End Class