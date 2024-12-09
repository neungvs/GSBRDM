Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports Arsoft.Utility
Imports System.Xml

Public Class ModuleManagement
    Inherits System.Web.UI.Page
    Dim _usermodule As New UserModuleBiz
    Dim _DataModule As ModuleEntity
    Dim _ThaiNoData As String = "ไม่มีข้อมูล"
    Dim flaging As Integer = -1
    Dim MessageBox_Result As Integer = -1
    Dim _command As Integer = -1
    Dim flag As Integer = 0
    Dim _userentityforlog As UserEntity
    Dim datas As String = ""
    Dim _GroupCode As LinkButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Loaddata(0)
            If Request.Cookies("finishOnLevel") Is Nothing Then
            Else
                Dim _leveldata As String = ConvertBase64ToText(Request.Cookies("finishOnLevel").Value)
                Response.Cookies("finishOnLevel").Expires = DateTime.Now.AddDays(-1)
                Loaddata(Convert.ToInt32(_leveldata))
                SwitchLevel.SelectedValue = Convert.ToInt32(_leveldata)
                SwitchLevel_SelectedIndexChanged(sender, e)
            End If
        End If
        If flag = 0 Then
            If Request.Cookies.Count > 0 Then
                Dim cookiedata As String
                If Request.Cookies("finishdataRDM_Web") Is Nothing Then
                Else
                    cookiedata = ConvertBase64ToText(Request.Cookies("finishdataRDM_Web").Value)
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    If cookiedata.IndexOf("สำเร็จ") > -1 Then
                        MessageBoxAlert("Success", cookiedata, "", "ปิด", False, True)
                    Else
                        MessageBoxAlert("Error", cookiedata, "", "ปิด", False, True)
                    End If
                End If
            End If
        End If
        'gv_level0.Visible = True
        'gv_level1.Visible = True
        'gv_level2.Visible = True
        'gv_level3.Visible = True
        'gv_level4.Visible = True
    End Sub
    Private Sub Loaddata(ByVal _level As Integer)
        Dim _data As New List(Of ModuleEntity)
        Dim _textdata As String = ""
        _data = _usermodule.GetLevelID()
        With SwitchLevel
            .DataSource = _data
            .DataValueField = "LevelID"
            .DataBind()
        End With
        For i As Integer = 0 To _data.Count - 1
            Dim createtext As String = "Level " & _data.Item(i).LevelID.ToString()
            With SwitchLevel
                .Items(i).Text = createtext
            End With
        Next
        SetSwitchLevel(_level)
        Page_Header_Content.InnerHtml = "กำหนดเมนูระดับ " & SwitchLevel.SelectedValue
        SwitchLevel.Focus()
    End Sub
    'Private Sub CreateModulelevel(ByVal _count As Integer, ByVal _valuelevel As Integer)
    '    Dim Headtable As String = "<table width='1000%' border=0>"
    '    Dim Bodytable As String = ""
    '    Dim Footertab As String = "</table>"
    '    Dim processcount As Integer = _count - 1
    '    For i As Integer = 0 To processcount - 1
    '        Dim createID As String = "LV" & i.ToString() & "ID"
    '        Dim createmenuid As String = "menulevelID" + i.ToString()
    '        Bodytable &= "<tr id=" & createmenuid & "><td align=""right"">เมนูระดับ " & i.ToString() & "</td><td align=""center"" width=""10px"">:</td><td align=""left"">"
    '        Bodytable &= "<asp:DropDownList ID=" & createID & " runat=""server"" AutoPostBack=""True""></asp:DropDownList></td></tr>"
    '    Next
    '    Bodytable &= "<tr><td align=""right"">รหัสเมนูระดับ " & _valuelevel & "</td><td align=""center"" width=""10px"">:</td><td align=""left""><input type=""text"" id=""LVAID"" name=""LVAID"" runat=""server"" style=""width:50px;""></td></tr>"
    '    Bodytable &= "<tr><td align=""right"">ชื่อเมนูภาษาไทยระดับ " & _valuelevel & "</td><td align=""center"" width=""10px"">:</td><td align=""left""><input type=""text"" id=""LVAName_th"" name=""LVAName_th"" runat=""server"" style=""width:250px;""></td></tr>"
    '    Bodytable &= "<tr><td align=""right"">ชื่อเมนูภาษาอังกฤษระดับ " & _valuelevel & "</td><td align=""center"" width=""10px"">:</td><td align=""left""><input type=""text"" id=""LVAName_en"" name=""LVAName_en"" runat=""server"" style=""width:250px;""></td></tr>"
    '    Bodytable &= "<tr><td align=""right"">Path โปรแกรม" & _valuelevel & "</td><td align=""center"" width=""10px"">:</td><td align=""left""><input type=""text"" id=""LVpath"" name=""LVpath"" runat=""server"" style=""width:250px;""></td></tr>"
    '    Bodytable &= "<tr><td align=""right"">ลำดับที่แสดง" & _valuelevel & "</td><td align=""center"" width=""10px"">:</td><td align=""left""><input type=""text"" id=""LVpath"" name=""LVpath"" runat=""server"" style=""width:250px;""></td></tr>"
    '    modulelv.InnerHtml = Headtable & Bodytable & Footertab
    'End Sub

    Protected Sub SwitchLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SwitchLevel.SelectedIndexChanged
        SetSwitchLevel(SwitchLevel.SelectedValue)
        ShowGridByLevel()

        Page_Header_Content.InnerHtml = "กำหนดเมนูระดับ " & SwitchLevel.SelectedValue
    End Sub

    Private Sub SetSwitchLevel(ByVal _valuelevel As Integer)
        Dim _data As List(Of ModuleEntity)
        ClearData()
        ClearGrid()
        ClearError()
        'lvaid.InnerHtml = "<font color=""#FF0000"">*</font>รหัสเมนูระดับ " & _valuelevel.ToString()
        lvaname_th.InnerHtml = "<font color=""#FF0000"">*</font>ชื่อเมนูระดับ " & _valuelevel.ToString()
        'lvaname_en.InnerHtml = "ชื่อเมนูภาษาอังกฤษระดับ " & _valuelevel.ToString()
        If _valuelevel = 0 Then
            menulevel0.Style.Clear() : menulevel0.Style.Add("display", "none")
            menulevel1.Style.Clear() : menulevel1.Style.Add("display", "none")
            menulevel2.Style.Clear() : menulevel2.Style.Add("display", "none")
            menulevel3.Style.Clear() : menulevel3.Style.Add("display", "none")
            lvshow.Style.Add("display", "none")
            gv_level0.Visible = True : gv_level1.Visible = False : gv_level2.Visible = False : gv_level3.Visible = False : gv_level4.Visible = False
            UnLockControl()
            Starbox_Show()

        ElseIf _valuelevel = 1 Then
            menulevel0.Style.Clear()
            menulevel1.Style.Clear() : menulevel1.Style.Add("display", "none")
            menulevel2.Style.Clear() : menulevel2.Style.Add("display", "none")
            menulevel3.Style.Clear() : menulevel3.Style.Add("display", "none")
            lvshow.Style.Clear()
            gv_level0.Visible = False : gv_level1.Visible = True : gv_level2.Visible = False : gv_level3.Visible = False : gv_level4.Visible = False
            UnLockControl()
            Starbox_Show()
        ElseIf _valuelevel = 2 Then
            menulevel0.Style.Clear()
            menulevel1.Style.Clear()
            menulevel2.Style.Clear() : menulevel2.Style.Add("display", "none")
            menulevel3.Style.Clear() : menulevel3.Style.Add("display", "none")
            lvshow.Style.Clear()
            gv_level0.Visible = False : gv_level1.Visible = False : gv_level2.Visible = True : gv_level3.Visible = False : gv_level4.Visible = False
            UnLockControl()
            Starbox_Show()
        ElseIf _valuelevel = 3 Then
            menulevel0.Style.Clear()
            menulevel1.Style.Clear()
            menulevel2.Style.Clear()
            menulevel3.Style.Clear() : menulevel3.Style.Add("display", "none")
            lvshow.Style.Clear()
            gv_level0.Visible = False : gv_level1.Visible = False : gv_level2.Visible = False : gv_level3.Visible = True : gv_level4.Visible = False
            UnLockControl()
            Starbox_Show()
        ElseIf _valuelevel = 4 Then
            menulevel0.Style.Clear()
            menulevel1.Style.Clear()
            menulevel2.Style.Clear()
            menulevel3.Style.Clear()
            lvshow.Style.Clear()
            gv_level0.Visible = False : gv_level1.Visible = False : gv_level2.Visible = False : gv_level3.Visible = False : gv_level4.Visible = True
            UnLockControl()
            Starbox_Show()
        End If
        If _valuelevel >= 1 Then
            dd_Menulevel_0.Items.Clear()
            _data = _usermodule.SelectDataforDropdownlist(" ", 0)
            With dd_Menulevel_0
                .DataSource = _data
                .DataTextField = "ModuleNameEN"
                .DataValueField = "ModuleID"
                .DataBind()
            End With
            If _valuelevel >= 2 Then
                dd_Menulevel_1.Items.Clear()
                If dd_Menulevel_0.SelectedValue <> 0 Then
                    With dd_Menulevel_1
                        .DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_0.SelectedValue, 1)
                        .DataTextField = "ModuleNameTH"
                        .DataValueField = "ModuleID"
                        .DataBind()
                        UnLockControl()
                        Starbox_Show()
                    End With
                    If dd_Menulevel_1.Items.Count = 0 Then
                        dd_Menulevel_1.Items.Add(1)
                        dd_Menulevel_1.Items(0).Text = _ThaiNoData
                        dd_Menulevel_1.Items(0).Value = ""
                        LockControl()
                        Starbox_Hide()
                    End If
                End If
            End If
            If _valuelevel >= 3 Then
                dd_Menulevel_2.Items.Clear()
                If dd_Menulevel_1.SelectedValue <> 0 Then
                    With dd_Menulevel_2
                        .DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_1.SelectedValue, 2)
                        .DataTextField = "ModuleNameTH"
                        .DataValueField = "ModuleID"
                        .DataBind()
                        UnLockControl()
                        Starbox_Show()
                    End With
                    If dd_Menulevel_2.Items.Count = 0 Then
                        dd_Menulevel_2.Items.Add(1)
                        dd_Menulevel_2.Items(0).Text = _ThaiNoData
                        dd_Menulevel_2.Items(0).Value = ""
                        LockControl()
                        Starbox_Hide()
                    End If
                End If
            End If
            If _valuelevel = 4 Then
                dd_Menulevel_3.Items.Clear()
                If dd_Menulevel_2.SelectedValue Then
                    With dd_Menulevel_3
                        .DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_2.SelectedValue, 2)
                        .DataTextField = "ModuleNameTH"
                        .DataValueField = "ModuleID"
                        .DataBind()
                        UnLockControl()
                        Starbox_Show()
                    End With
                    If dd_Menulevel_3.Items.Count = 0 Then
                        dd_Menulevel_3.Items.Add(1)
                        dd_Menulevel_3.Items(0).Text = _ThaiNoData
                        dd_Menulevel_3.Items(0).Value = ""
                        menu_star3.Visible = False
                        Starbox_Hide()
                        LockControl()
                    End If
                End If
            End If
        End If
        LoadDataLevelPutinGridView()
    End Sub

    Private Sub LoadDataLevelPutinGridView()
        If gv_level0.Visible = True Then
            gv_level0.DataSource = _usermodule.GetDataModuleLevel(0)
            gv_level0.DataBind()
        End If

        If gv_level1.Visible = True Then
            gv_level1.DataSource = _usermodule.GetDataModuleLevel(1)
            gv_level1.DataBind()
        End If

        If gv_level2.Visible = True Then
            gv_level2.DataSource = _usermodule.GetDataModuleLevel(2)
            gv_level2.DataBind()
        End If

        If gv_level3.Visible = True Then
            gv_level3.DataSource = _usermodule.GetDataModuleLevel(3)
            gv_level3.DataBind()
        End If

        If gv_level4.Visible = True Then
            gv_level4.DataSource = _usermodule.GetDataModuleLevel(4)
            gv_level4.DataBind()
        End If
    End Sub

    Private Sub GetDataFromControlAllLevel(ByVal _level As Integer)
        _DataModule = New ModuleEntity
        If _level = 0 Then

            With _DataModule
                .ModuleID = LVIID.Text
                .ModuleNameTH = LVIName_th.Value
                .ModuleNameEN = LVIName_th.Value
                .MenuSeq = LVIMenuseq.Text
                If LVIPath.Value = "NULL" Or LVIPath.Value = "null" Or LVIPath.Value = "Null" Then
                    .LinkPage = Nothing
                Else
                    .LinkPage = LVIPath.Value
                End If

                .LevelID = SwitchLevel.SelectedValue
            End With
        Else
            With _DataModule
                If _level = 1 Then
                    .ParentID = dd_Menulevel_0.SelectedValue
                ElseIf _level = 2 Then
                    .ParentID = dd_Menulevel_1.SelectedValue
                ElseIf _level = 3 Then
                    .ParentID = dd_Menulevel_2.SelectedValue
                ElseIf _level = 4 Then
                    .ParentID = dd_Menulevel_3.SelectedValue
                End If
                .ModuleID = LVIID.Text
                '.ModuleNameEN = LVIName_en.Value
                .ModuleNameTH = LVIName_th.Value
                If LVIPath.Value = "NULL" Or LVIPath.Value = "null" Or LVIPath.Value = "Null" Then
                    .LinkPage = Nothing
                Else
                    .LinkPage = LVIPath.Value
                End If
                .MenuSeq = LVIMenuseq.Text
                .LevelID = SwitchLevel.SelectedValue
            End With
        End If
    End Sub


    Protected Sub Sav_Click(sender As Object, e As EventArgs) Handles Sav.Click
        Dim checklist_fail As Integer
        Dim mark = New String() {"", "", "", "", "", "", "", "", "", "", "", ""}
        Dim Create_Fail_Message As String = ""
        If SwitchLevel.SelectedValue = 0 Then
            If Trim(LVIName_th.Value) = "" Then
                checklist_fail += 1
            End If
            If checklist_fail > 0 Then
                For i As Integer = 0 To mark.Length - 1
                    If mark(i) <> "" Then
                        If mark(i + 1) = "" Then
                            Create_Fail_Message &= mark(i).ToString()
                        Else
                            Create_Fail_Message &= mark(i).ToString() & " และ "
                        End If
                    ElseIf mark(i) = "" Then
                        Exit For
                    End If
                Next
                MessageBoxAlert("Error", "กรุณากรอกข้อมูลให้ครบถ้วน", "", "ปิด", False, True)
            End If
        Else
            If LVIName_th.Value = "" Then
                checklist_fail += 1
                If mark(0) = "" Then
                    mark(0) = "ชื่อภาษาไทยระดับ" & SwitchLevel.SelectedValue.ToString()
                Else
                    mark(1) = "ชื่อภาษาไทยระดับ" & SwitchLevel.SelectedValue.ToString()
                End If
            End If
            If LVIMenuseq.Text = "" Then
                checklist_fail += 1
                If mark(0) = "" Then
                    mark(0) = "ลำดับที่แสดง"
                Else
                    If mark(1) = "" Then
                        mark(1) = "ลำดับที่แสดง"
                    Else
                        If mark(2) = "" Then
                            mark(2) = "ลำดับที่แสดง"
                        Else
                            mark(3) = "ลำดับที่แสดง"
                        End If
                    End If
                End If
            End If
            If checklist_fail > 0 Then
                For i As Integer = 0 To mark.Length - 1
                    If mark(i) <> "" Then
                        If mark(i + 1) = "" Then
                            If Create_Fail_Message = "" Then
                                Create_Fail_Message = mark(i)
                            Else
                                Create_Fail_Message = Create_Fail_Message.Substring(0, Create_Fail_Message.LastIndexOf(",")) & " และ " & mark(i)
                            End If
                        Else
                            Create_Fail_Message &= mark(i).ToString() & ", "
                        End If
                    ElseIf mark(i) = "" Then
                        Exit For
                    End If
                Next
                MessageBoxAlert("Error", "กรุณากรอก " & Create_Fail_Message & " ให้ครบถ้วน", "", "ปิด", False, True)
            End If
        End If
        If checklist_fail = 0 Then
            If MessageBox_Result = -1 Then
                btn_OK.Attributes.Remove("data-dismiss")
                MessageBoxAlert("Question", "ต้องการบันทึกรายการนี้ ใช่หรือไม่ ", "ใช่", "ไม่ใช่", True, True)
                Response.Cookies("SetCommandData_GSBWebsite").Value = 1
                Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            Else
                btn_OK.Attributes.Add("data-dismiss", "modal")
            End If
            If MessageBox_Result > 0 Then
                Dim _switchdata As Integer = SwitchLevel.SelectedValue
                GetDataFromControlAllLevel(_switchdata)
                Dim _result As Boolean
                _result = _usermodule.CheckModuledata(_DataModule.ModuleID)
                If _result = True Then
                    If IsNothing(_DataModule.ParentID) Then
                        _DataModule.ModuleID = _usermodule.GenerateModuleID(0, 0)
                    Else
                        _DataModule.ModuleID = _usermodule.GenerateModuleID(_DataModule.ParentID, _DataModule.LevelID)
                    End If
                    _result = _usermodule.SaveDataModule(_DataModule)
                    Else
                        _result = _usermodule.UpdateDataModule(_DataModule, _switchdata)
                End If
                If _result = True Then
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกรายการสำเร็จ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)

                Else
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกรายการล้มเหลว กรุณาทำรายการใหม่")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                End If
                _userentityforlog = New UserEntity
                _userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
                Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

                With _userentityforlog
                    .GroupID = 4
                    .GroupName_TH = "กำหนดเมนู"
                    .UserActivity = "บันทึกเมนู " & _GroupData
                End With
                _usermodule.AddLogdata(_userentityforlog)
            End If
        End If
        Response.Cookies("finishOnLevel").Value = ConvertTextToBase64(SwitchLevel.SelectedValue.ToString())
        Response.Cookies("finishOnLevel").Expires = DateTime.Now.AddDays(1)
    End Sub

    Protected Sub Delete_Click(sender As Object, e As EventArgs) Handles Delete.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            MessageBoxAlert("Question", "ต้องการลบรายการนี้ ใช่หรือไม่", "ใช่", "ไม่ใช่", True, True)
            Response.Cookies("SetCommandData_GSBWebsite").Value = 2
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            Dim _switchdata As Integer = SwitchLevel.SelectedValue
            GetDataFromControlAllLevel(_switchdata)
            Dim _result As Boolean
            _result = _usermodule.DeleteDataModule(_DataModule.ModuleID)
            If _result = True Then
                MessageBoxAlert("Success", "ลบรายการสำเร็จ", "", "ปิด", False, True)
                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบรายการสำเร็จ")
                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            Else
                MessageBoxAlert("Error", "ลบรายการล้มเหลว", "", "ปิด", False, True)
                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบรายการสำเร็จ")
                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            End If
            _userentityforlog = New UserEntity
            _userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
            Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

            With _userentityforlog
                .GroupID = 4
                .GroupName_TH = "กำหนดเมนู"
                .UserActivity = "ลบเมนู " & _GroupData
            End With
            _usermodule.AddLogdata(_userentityforlog)
        End If
        Response.Cookies("finishOnLevel").Value = ConvertTextToBase64(SwitchLevel.SelectedValue.ToString())
        Response.Cookies("finishOnLevel").Expires = DateTime.Now.AddDays(1)
    End Sub

    Protected Sub dd_Menulevel_0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dd_Menulevel_0.SelectedIndexChanged
        If SwitchLevel.SelectedValue >= 2 Then
            With dd_Menulevel_1
                .Items.Clear()
                .DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_0.SelectedValue, 1)
                .DataTextField = "ModuleNameTH"
                .DataValueField = "ModuleID"
                .DataBind()
                Set_item_level2()
                UnLockControl()
                menu_star1.Visible = True
                If dd_Menulevel_2.Items.Count > 0 Then
                    menu_star2.Visible = True
                End If
                Starbox_Show()
            End With
        End If

        ShowGridByLevel()

        If SwitchLevel.SelectedValue = 2 And dd_Menulevel_1.Items.Count = 0 Then
            dd_Menulevel_1.Items.Clear()
            dd_Menulevel_1.Items.Add(1)
            dd_Menulevel_1.Items(0).Text = _ThaiNoData

            LockControl()
            menu_star1.Visible = False : menu_star2.Visible = False : menu_star3.Visible = False
            Starbox_Hide()

        ElseIf SwitchLevel.SelectedValue = 3 And (dd_Menulevel_1.Items.Count = 0 Or dd_Menulevel_2.Items.Count = 0) Then
            If dd_Menulevel_1.Items.Count = 0 Then
                dd_Menulevel_1.Items.Clear()
                dd_Menulevel_1.Items.Add(1)
                dd_Menulevel_1.Items(0).Text = _ThaiNoData
                dd_Menulevel_1.Items(0).Value = ""
            End If
            If dd_Menulevel_2.Items.Count = 0 Then
                dd_Menulevel_2.Items.Clear()
                dd_Menulevel_2.Items.Add(1)
                dd_Menulevel_2.Items(0).Text = _ThaiNoData
                dd_Menulevel_2.Items(0).Value = ""
            End If
            If dd_Menulevel_2.Items.Count = 1 And dd_Menulevel_2.Items(0).Text = _ThaiNoData Then
                dd_Menulevel_2.Items(0).Value = ""
            End If
            If dd_Menulevel_1.Items(0).Value = "" And dd_Menulevel_2.Items(0).Value = "" Then
                menu_star1.Visible = False : menu_star2.Visible = False
            ElseIf dd_Menulevel_1.Items(0).Value = "" And dd_Menulevel_2.Items(0).Value <> "" Then
                menu_star1.Visible = False
            ElseIf dd_Menulevel_1.Items(0).Value <> "" And dd_Menulevel_2.Items(0).Value = "" Then
                menu_star2.Visible = False
            End If
            LockControl()
            Starbox_Hide()
        ElseIf SwitchLevel.SelectedValue = 4 And (dd_Menulevel_1.Items.Count = 0 Or dd_Menulevel_2.Items.Count = 0 Or dd_Menulevel_3.Items.Count = 0) Then
            If dd_Menulevel_1.Items.Count = 0 Then
                dd_Menulevel_1.Items.Clear()
                dd_Menulevel_1.Items.Add(1)
                dd_Menulevel_1.Items(0).Text = _ThaiNoData
                dd_Menulevel_1.Items(0).Value = ""
            End If
            If dd_Menulevel_2.Items.Count = 0 Then
                dd_Menulevel_2.Items.Clear()
                dd_Menulevel_2.Items.Add(1)
                dd_Menulevel_2.Items(0).Text = _ThaiNoData
                dd_Menulevel_2.Items(0).Value = ""
            End If
            If dd_Menulevel_3.Items.Count = 0 Then
                dd_Menulevel_3.Items.Clear()
                dd_Menulevel_3.Items.Add(1)
                dd_Menulevel_3.Items(0).Text = _ThaiNoData
                dd_Menulevel_3.Items(0).Value = ""
            End If
            If dd_Menulevel_1.Items(0).Value = "" And dd_Menulevel_2.Items(0).Value = "" And dd_Menulevel_3.Items(0).Value = "" Then
                menu_star1.Visible = False : menu_star2.Visible = False : menu_star3.Visible = False
            ElseIf dd_Menulevel_1.Items(0).Value <> "" And dd_Menulevel_2.Items(0).Value = "" And dd_Menulevel_3.Items(0).Value = "" Then
                menu_star1.Visible = False : menu_star2.Visible = False
            ElseIf dd_Menulevel_1.Items(0).Value <> "" And dd_Menulevel_2.Items(0).Value <> "" And dd_Menulevel_3.Items(0).Value = "" Then
                menu_star1.Visible = False : menu_star3.Visible = False
            End If
            LockControl()
            Starbox_Hide()
        End If
    End Sub

    Protected Sub dd_Menulevel_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dd_Menulevel_1.SelectedIndexChanged
        dd_Menulevel_2.Items.Clear()
        If SwitchLevel.SelectedValue >= 3 Or dd_Menulevel_1.Items.Count > 0 Then
            With dd_Menulevel_2
                .Items.Clear()
                .DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_1.SelectedValue.ToString(), 2)

                .DataTextField = "ModuleNameTH"
                .DataValueField = "ModuleID"
                .DataBind()
                'If SwitchLevel.SelectedValue = 3 And dd_Menulevel_1.Items.Count = 0 And dd_Menulevel_2.Items.Count = 0 Then
                '    SetSwitchLevel(SwitchLevel.SelectedValue)
                'End If
                UnLockControl()
                Set_item_level3(dd_Menulevel_2.SelectedValue.ToString())
                menu_star2.Visible = True
                Starbox_Show()
            End With

        End If

        ShowGridByLevel()

        If SwitchLevel.SelectedValue = 3 And dd_Menulevel_2.Items.Count = 0 Then
            dd_Menulevel_2.Items.Clear()
            dd_Menulevel_2.Items.Add(1)
            dd_Menulevel_2.Items(0).Text = _ThaiNoData
            dd_Menulevel_2.Items(0).Value = ""
            LockControl()
            menu_star2.Visible = False
            Starbox_Hide()
        ElseIf SwitchLevel.SelectedValue = 4 And (dd_Menulevel_2.Items.Count = 0 Or dd_Menulevel_3.Items.Count = 0 Or dd_Menulevel_3.Items.Count = 1) Then
            If dd_Menulevel_2.Items.Count = 0 Then
                dd_Menulevel_2.Items.Clear()
                dd_Menulevel_2.Items.Add(1)
                dd_Menulevel_2.Items(0).Text = _ThaiNoData
                dd_Menulevel_2.Items(0).Value = ""
            End If
            If dd_Menulevel_3.Items.Count = 0 Then
                dd_Menulevel_3.Items.Clear() : dd_Menulevel_3.Items.Add(1) : dd_Menulevel_3.Items.Item(0).Text = _ThaiNoData : dd_Menulevel_3.Items(0).Value = ""
            End If
            If dd_Menulevel_2.Items(0).Value = "" And dd_Menulevel_3.Items(0).Value = "" Then
                menu_star2.Visible = False : menu_star3.Visible = False
            ElseIf dd_Menulevel_2.Items(0).Value = "" And dd_Menulevel_3.Items(0).Value <> "" Then
                menu_star2.Visible = False
            ElseIf dd_Menulevel_2.Items(0).Value <> "" And dd_Menulevel_3.Items(0).Value = "" Then
                menu_star3.Visible = False
            End If

            Starbox_Hide()
            LockControl()
        End If
    End Sub

    Protected Sub dd_Menulevel_2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dd_Menulevel_2.SelectedIndexChanged
        dd_Menulevel_3.Items.Clear()
        If SwitchLevel.SelectedValue > 3 Or dd_Menulevel_2.Items.Count > 0 Then
            With dd_Menulevel_3
                .Items.Clear()
                .DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_2.SelectedValue, 3)
                .DataTextField = "ModuleNameTH"
                .DataValueField = "ModuleID"
                .DataBind()
                If dd_Menulevel_3.Items.Count = 0 Then
                    Set_item_level3(dd_Menulevel_2.SelectedValue)
                End If
                UnLockControl()
                menu_star3.Visible = True
                Starbox_Show()
            End With

        End If

        ShowGridByLevel()

        If SwitchLevel.SelectedValue = 4 And dd_Menulevel_3.Items.Count = 0 Then
            dd_Menulevel_3.Items.Clear()
            dd_Menulevel_3.Items.Add(1)
            dd_Menulevel_3.Items(0).Text = _ThaiNoData
            LockControl()
            menu_star3.Visible = False
            Starbox_Hide()
        End If
    End Sub

    Protected Sub LockControl()
        LVIID.Enabled = False
        'LVIName_en.Disabled = True
        LVIName_th.Disabled = True
        LVIPath.Disabled = True
        LVIMenuseq.Enabled = False
    End Sub

    Protected Sub UnLockControl()
        LVIID.Enabled = True
        'LVIName_en.Disabled = False
        LVIName_th.Disabled = False
        LVIPath.Disabled = False
        LVIMenuseq.Enabled = True
    End Sub

    Protected Sub Starbox_Show()
        lvaid.InnerHtml = "<font color=""#FF0000"">*</font>รหัสเมนูระดับ " & SwitchLevel.SelectedValue.ToString()
        lvaname_th.InnerHtml = "<font color=""#FF0000"">*</font>ชื่อเมนูระดับ " & SwitchLevel.SelectedValue.ToString()
        'lvaname_en.InnerHtml = "<font color=""#FF0000"">*</font>ชื่อเมนูภาษาอังกฤษระดับ " & SwitchLevel.SelectedValue.ToString()
        menu_starpath.Visible = True
        menu_starseq.Visible = True
    End Sub

    Protected Sub Starbox_Hide()
        lvaid.InnerHtml = "รหัสเมนูระดับ " & SwitchLevel.SelectedValue.ToString()
        lvaname_th.InnerHtml = "ชื่อเมนูระดับ " & SwitchLevel.SelectedValue.ToString()
        menu_starpath.Visible = False
        menu_starseq.Visible = False
    End Sub

    Protected Sub dd_Menulevel_1_DataBinding(sender As Object, e As EventArgs) Handles dd_Menulevel_1.DataBinding
        If dd_Menulevel_1.Items.Count > 0 Then
            If dd_Menulevel_2.Items.Count > 0 Then
                If dd_Menulevel_3.Items.Count > 0 Then
                    UnLockControl()
                Else
                    LockControl()
                End If
            Else
                LockControl()
            End If
        End If
    End Sub

    Protected Sub dd_Menulevel_2_DataBinding(sender As Object, e As EventArgs) Handles dd_Menulevel_2.DataBinding
        If dd_Menulevel_2.Items.Count > 0 Then
            If dd_Menulevel_3.Items.Count > 0 Then
                UnLockControl()
            End If
        End If
    End Sub

    Protected Sub dd_Menulevel_3_DataBinding(sender As Object, e As EventArgs) Handles dd_Menulevel_3.DataBinding
        If dd_Menulevel_3.Items.Count > 0 Then
            UnLockControl()
        End If
    End Sub

    Protected Sub Set_item_level2()
        Dim level0_now As Integer = dd_Menulevel_0.SelectedValue
        Dim level1_select As List(Of ModuleEntity) = _usermodule.SelectDataforDropdownlist(level0_now, 1)
        If level1_select.Count > 0 Then
            If dd_Menulevel_1.Items.Count > 0 Then
                If dd_Menulevel_1.Items(0).Text = "" Then
                    dd_Menulevel_2.DataSource = _usermodule.SelectDataforDropdownlist(level1_select.Item(0).ModuleID, 2)
                    dd_Menulevel_2.DataBind()
                Else
                    dd_Menulevel_2.DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_1.SelectedValue, 2)
                    dd_Menulevel_2.DataBind()
                End If
            End If
            If SwitchLevel.SelectedValue = 4 Then
                If dd_Menulevel_2.Items.Count > 0 Then
                    Set_item_level3(dd_Menulevel_2.Items(0).Value)
                Else
                    Set_item_level3("")
                End If
            End If
        End If
    End Sub

    Protected Sub Set_item_level3(ByVal _Select_LV2 As String)
        Dim level0_now As Integer = dd_Menulevel_0.SelectedValue
        Dim level1_select As List(Of ModuleEntity) = _usermodule.SelectDataforDropdownlist(level0_now, 1)
        Dim level2_select As New List(Of ModuleEntity)
        If _Select_LV2 <> "" Then
            dd_Menulevel_3.DataSource = _usermodule.SelectDataforDropdownlist(dd_Menulevel_2.SelectedValue, 3)
            dd_Menulevel_3.DataBind()
        ElseIf _Select_LV2 = "ไม่มีข้อมูล" Then
            dd_Menulevel_3.DataSource = level2_select
            dd_Menulevel_3.DataBind()
        Else
            level2_select = _usermodule.SelectDataforDropdownlist(level1_select.Item(0).ModuleID, 2)
            If level2_select.Count <> 0 Then
                dd_Menulevel_3.DataSource = _usermodule.SelectDataforDropdownlist(level2_select.Item(0).ModuleID, 3)
                dd_Menulevel_3.DataBind()
            End If
        End If
        'If dd_Menulevel_1.Items.Count > 0 Then
        '    level2_select = _usermodule.SelectDataforDropdownlist(dd_Menulevel_1.SelectedValue, 2)
        '    If dd_Menulevel_2.SelectedValue <> "" Then

        '    Else
        '        dd_Menulevel_3.DataSource = _usermodule.SelectDataforDropdownlist(level2_select.Item(0).ModuleID, 3)
        '        dd_Menulevel_3.DataBind()
        '    End If
        'Else
        '    level2_select = _usermodule.SelectDataforDropdownlist(level1_select.Item(0).ModuleID, 2)
        '    If level2_select.Count > 0 Then
        '        dd_Menulevel_3.DataSource = _usermodule.SelectDataforDropdownlist(level2_select.Item(0).ModuleID, 3)
        '        dd_Menulevel_3.DataBind()
        '    End If
        'End If
    End Sub

    Protected Sub MessageBoxAlert(ByVal title As String, ByVal _message As String, ByVal BtnOKString As String, ByVal BtnNOString As String, ByVal YesbtnStatus As Boolean, ByVal NobtnStatus As Boolean)
        lbl_Title.Text = title
        If title = "Error" Then
            Symbol_Image.ImageUrl = "~/Images/NotCorrect.png"
        ElseIf title = "Success" Then
            Symbol_Image.ImageUrl = "~/Images/Correct.png"
        ElseIf title = "Question" Then
            Symbol_Image.ImageUrl = "~/Images/Question.png"
        ElseIf title = "Warning" Then
            Symbol_Image.ImageUrl = "~/Images/Warning.png"
        ElseIf title = "Information" Then
            Symbol_Image.ImageUrl = "~/Images/Infomation.png"
        End If
        Messages.Text = _message
        btn_OK.Visible = YesbtnStatus
        btn_NO.Visible = NobtnStatus
        btn_OK.Text = BtnOKString
        btn_NO.Text = BtnNOString
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertBox", "$('#AlertBox').modal();", True)
        UpdModal.Update()
    End Sub

    Protected Function ConvertTextToBase64(ByVal _str As String) As String
        Dim _byt As Byte() = Encoding.UTF8.GetBytes(_str)
        Dim _base64 As String = Convert.ToBase64String(_byt)
        Return _base64
    End Function

    Protected Function ConvertBase64ToText(ByVal _base64str As String) As String
        Dim _results As String
        Dim _bytes_ As Byte() = Convert.FromBase64String(_base64str)
        _results = Text.Encoding.UTF8.GetString(_bytes_)
        Return _results
    End Function

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If btn_NO.Visible = True Then
            MessageBox_Result = 1
            _command = Request.Cookies("SetCommandData_GSBWebsite").Value
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(-1)
            If _command = 1 Then
                Sav_Click(sender, e)
            ElseIf _command = 2 Then
                Delete_Click(sender, e)
            ElseIf _command = 3 Then
                Cancel_Click(sender, e)
            End If
            MessageBox_Result = -1
        End If
        Response.Redirect("~/setting/ModuleManagement.aspx")
    End Sub

    Protected Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 3
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการยกเลิกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            ClearContent()
            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ยกเลิกรายการสำเร็จ")
            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            '_userentityforlog = New UserEntity
            '_userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
            'Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

            'With _userentityforlog
            '    .GroupID = 4
            '    .GroupName_TH = "กำหนดเมนู"
            '    .UserActivity = "ยกเลิกเมนู " & _GroupData

            'End With
            '_usermodule.AddLogdata(_userentityforlog)
        End If
        Response.Cookies("finishOnLevel").Value = ConvertTextToBase64(SwitchLevel.SelectedValue.ToString())
        Response.Cookies("finishOnLevel").Expires = DateTime.Now.AddDays(1)
    End Sub

    Protected Sub ClearContent()
        LVIID.Text = ""
        LVIName_th.Value = ""
        'LVIName_en.Value = ""
        LVIPath.Value = ""
        LVIMenuseq.Text = ""
    End Sub

    Protected Sub btn_Prints_Click(sender As Object, e As EventArgs) Handles btn_Prints.Click
        Dim _Data As New List(Of ModuleEntity)
        If SwitchLevel.SelectedValue = 0 Then
            _Data = GetDataFromGridview(SwitchLevel.SelectedValue)
        ElseIf SwitchLevel.SelectedValue = 1 Then
            _Data = GetDataFromGridview(SwitchLevel.SelectedValue)
        ElseIf SwitchLevel.SelectedValue = 2 Then
            _Data = GetDataFromGridview(SwitchLevel.SelectedValue)
        ElseIf SwitchLevel.SelectedValue = 3 Then
            _Data = GetDataFromGridview(SwitchLevel.SelectedValue)
        ElseIf SwitchLevel.SelectedValue = 4 Then
            _Data = GetDataFromGridview(SwitchLevel.SelectedValue)
        End If
        Create_DataForm(_Data)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "div_panel", "printing()", True)
        '_userentityforlog = New UserEntity
        '_userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
        'Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

        'With _userentityforlog
        '    .GroupID = 4
        '    .GroupName_TH = "กำหนดเมนู"
        '    .UserActivity = "พิมพ์เมนู " & _GroupData
        'End With
        '_usermodule.AddLogdata(_userentityforlog)
    End Sub

    Protected Function GetDataFromGridview(ByVal _levels As Integer) As List(Of ModuleEntity)

        Dim _return As New List(Of ModuleEntity)
        Dim _moduleitem As ModuleEntity
        Dim _moduleid As LinkButton
        Dim _modulename_en As LinkButton
        Dim _moduleid_lv0 As LinkButton
        Dim _moduleid_lv1 As LinkButton
        Dim _moduleid_lv2 As LinkButton
        Dim _moduleid_lv3 As LinkButton
        Dim _Sequenceitem As LinkButton

        If _levels = 0 Then
            For i As Integer = 0 To gv_level0.Rows.Count - 1
                _moduleid = CType(gv_level0.Rows(i).FindControl("lbGroupCode_level0"), LinkButton)
                _modulename_en = CType(gv_level0.Rows(i).FindControl("lbGroupname_EN_level0"), LinkButton)
                _Sequenceitem = CType(gv_level0.Rows(i).FindControl("Number_Level0"), LinkButton)
                _moduleitem = New ModuleEntity
                With _moduleitem

                    .ModuleID = _moduleid.Text
                    .ModuleNameEN = _modulename_en.Text
                    .MenuSeq = _Sequenceitem.Text
                End With
                _return.Add(_moduleitem)
            Next
        ElseIf _levels = 1 Then
            For i As Integer = 0 To gv_level1.Rows.Count - 1
                _moduleid_lv0 = CType(gv_level1.Rows(i).FindControl("lbGroupCode0_level1"), LinkButton)
                _moduleid = CType(gv_level1.Rows(i).FindControl("lbGroupCode1_level1"), LinkButton)
                _modulename_en = CType(gv_level1.Rows(i).FindControl("lbGroupname_TH_level1"), LinkButton)
                _Sequenceitem = CType(gv_level1.Rows(i).FindControl("Number_Level1"), LinkButton)
                _moduleitem = New ModuleEntity
                With _moduleitem
                    .ModuleID_Level0 = _moduleid_lv0.Text
                    .ModuleID = _moduleid.Text
                    .ModuleNameEN = _modulename_en.Text
                    .MenuSeq = _Sequenceitem.Text
                End With
                _return.Add(_moduleitem)
            Next
        ElseIf _levels = 2 Then
            For i As Integer = 0 To gv_level2.Rows.Count - 1
                _moduleid_lv0 = CType(gv_level2.Rows(i).FindControl("lbGroupCode0_level2"), LinkButton)
                _moduleid_lv1 = CType(gv_level2.Rows(i).FindControl("lbGroupCode1_level2"), LinkButton)
                _moduleid = CType(gv_level2.Rows(i).FindControl("lbGroupCode2_level2"), LinkButton)
                _modulename_en = CType(gv_level2.Rows(i).FindControl("lbGroupName_TH_level2"), LinkButton)
                _Sequenceitem = CType(gv_level2.Rows(i).FindControl("Number_Level2"), LinkButton)
                _moduleitem = New ModuleEntity
                With _moduleitem
                    .ModuleID_Level0 = _moduleid_lv0.Text
                    .ModuleID_Level1 = _moduleid_lv1.Text
                    .ModuleID = _moduleid.Text
                    .ModuleNameTH = _modulename_en.Text
                    .MenuSeq = _Sequenceitem.Text
                End With
                _return.Add(_moduleitem)
            Next
        ElseIf _levels = 3 Then
            For i As Integer = 0 To gv_level3.Rows.Count - 1
                _moduleid_lv0 = CType(gv_level3.Rows(i).FindControl("lbGroupCode0_level3"), LinkButton)
                _moduleid_lv1 = CType(gv_level3.Rows(i).FindControl("lbGroupCode1_level3"), LinkButton)
                _moduleid_lv2 = CType(gv_level3.Rows(i).FindControl("lbGroupCode2_level3"), LinkButton)
                _moduleid = CType(gv_level3.Rows(i).FindControl("lbGroupCode3_level3"), LinkButton)
                _modulename_en = CType(gv_level3.Rows(i).FindControl("lbGroupName_TH_level3"), LinkButton)
                _Sequenceitem = CType(gv_level3.Rows(i).FindControl("Number_Level3"), LinkButton)
                _moduleitem = New ModuleEntity
                With _moduleitem
                    .ModuleID_Level0 = _moduleid_lv0.Text
                    .ModuleID_Level1 = _moduleid_lv1.Text
                    .ModuleID_Level2 = _moduleid_lv2.Text
                    .ModuleID = _moduleid.Text
                    .ModuleNameTH = _modulename_en.Text
                    .MenuSeq = _Sequenceitem.Text
                End With
                _return.Add(_moduleitem)
            Next
        ElseIf _levels = 4 Then
            For i As Integer = 0 To gv_level4.Rows.Count - 1
                _moduleid_lv0 = CType(gv_level4.Rows(i).FindControl("lbGroupCode0_level4"), LinkButton)
                _moduleid_lv1 = CType(gv_level4.Rows(i).FindControl("lbGroupCode1_level4"), LinkButton)
                _moduleid_lv2 = CType(gv_level4.Rows(i).FindControl("lbGroupCode2_level4"), LinkButton)
                _moduleid_lv3 = CType(gv_level4.Rows(i).FindControl("lbGroupCode3_level4"), LinkButton)
                _moduleid = CType(gv_level4.Rows(i).FindControl("lbGroupCode4_level4"), LinkButton)
                _modulename_en = CType(gv_level4.Rows(i).FindControl("lbGroupName_TH_level4"), LinkButton)
                _Sequenceitem = CType(gv_level4.Rows(i).FindControl("Number_Level4"), LinkButton)
                _moduleitem = New ModuleEntity
                With _moduleitem
                    .ModuleID_Level0 = _moduleid_lv0.Text
                    .ModuleID_Level1 = _moduleid_lv1.Text
                    .ModuleID_Level2 = _moduleid_lv2.Text
                    .ModuleID_Level3 = _moduleid_lv3.Text
                    .ModuleID = _moduleid.Text
                    .ModuleNameTH = _modulename_en.Text
                    .MenuSeq = _Sequenceitem.Text
                End With
                _return.Add(_moduleitem)
            Next
        End If

        Return _return
    End Function

    Protected Sub Create_DataForm(ByVal _listdata As List(Of ModuleEntity))
        Dim CreateTag As String = ""
        Dim countAll As Integer
        Dim AllPageVal As Integer
        Dim countmoth As Integer
        Dim _RowCount As Integer = 0
        Dim countPageValue As Integer = 1
        If SwitchLevel.SelectedValue = 0 Then
            countAll = gv_level0.Rows.Count
            _RowCount = 32
        ElseIf SwitchLevel.SelectedValue = 1 Then
            countAll = gv_level1.Rows.Count
            _RowCount = 32
        ElseIf SwitchLevel.SelectedValue = 2 Then
            countAll = gv_level2.Rows.Count
            _RowCount = 25
        ElseIf SwitchLevel.SelectedValue = 3 Then
            countAll = gv_level3.Rows.Count
            _RowCount = 19
        ElseIf SwitchLevel.SelectedValue = 4 Then
            countAll = gv_level4.Rows.Count
            _RowCount = 32
        End If
        If countAll < _RowCount Then
            AllPageVal = 1
        Else
            AllPageVal = countAll / _RowCount
            countmoth = countAll Mod _RowCount
            If countmoth > 0 Then
                AllPageVal += 1
            End If
        End If
        Dim Header As String = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดเมนูระดับ " & SwitchLevel.SelectedValue.ToString() & "</td></tr></table>"
        If SwitchLevel.SelectedValue > 1 Then
            Header &= "<table align='center' width='100%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
        Else
            Header &= "<table align='center' width='80%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"

        End If
        If SwitchLevel.SelectedValue = 0 Then
            Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูภาษาอังกฤษ</td></tr>"
        ElseIf SwitchLevel.SelectedValue = 1 Then
            Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>ลำดับที่แสดง</td></tr>"
        ElseIf SwitchLevel.SelectedValue = 2 Then
            Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>ลำดับที่แสดง</td></tr>"
        ElseIf SwitchLevel.SelectedValue = 3 Then
            Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 3</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 3</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>ลำดับที่แสดง</td></tr>"
        ElseIf SwitchLevel.SelectedValue = 4 Then
            Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 3</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 4</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 4</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>ลำดับที่แสดง</td></tr>"
        End If
        countPageValue += 1
        div_Panel.InnerHtml = Header
        Dim _color As String = ""
        Dim count As Integer = 1
        For Each _item As ModuleEntity In _listdata
            If _color = "" Then
                _color = "#FFE8EE"
            ElseIf _color = "#FFE8EE" Then
                _color = "#FFCEDB"
            Else
                _color = "#FFE8EE"
            End If
            If count = _RowCount Then
               
                Header = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดเมนูระดับ " & SwitchLevel.SelectedValue.ToString() & "</td></tr></table>"
                If SwitchLevel.SelectedValue > 1 Then
                    Header &= "<table align='center' width='100%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
                Else
                    Header &= "<table align='center' width='80%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
                End If
                If SwitchLevel.SelectedValue = 0 Then
                    Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูภาษาอังกฤษ</td></tr>"
                ElseIf SwitchLevel.SelectedValue = 1 Then
                    Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:120px;'>ลำดับที่แสดง</td></tr>"
                ElseIf SwitchLevel.SelectedValue = 2 Then
                    Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>ลำดับที่แสดง</td></tr>"
                ElseIf SwitchLevel.SelectedValue = 3 Then
                    Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>รหัสเมนูระดับ 3</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 3</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>ลำดับที่แสดง</td></tr>"
                ElseIf SwitchLevel.SelectedValue = 4 Then
                    Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 0</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 1</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 2</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 3</td><td align='center' style='background-color:#FF388C;color:#FFF;width:100px;'>รหัสเมนูระดับ 4</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อเมนูระดับ 4</td><td align='center' style='background-color:#FF388C;color:#FFF;width:110px;'>ลำดับที่แสดง</td></tr>"
                End If
                If SwitchLevel.SelectedValue = 2 Then
                    If countPageValue > 1 Then
                        If countPageValue = 2 Then
                            CreateTag = CreateTag & "</table>"
                        Else
                            CreateTag = CreateTag & "</table>" & "<div style='height:1cm;'>&nbsp;</div>" & Header
                        End If
                        If countPageValue > 2 Then
                            _RowCount = 17

                        ElseIf countPageValue > 3 Then
                            _RowCount = 25
                        Else
                            _RowCount = 20
                            AllPageVal = countAll / _RowCount
                            countmoth = countAll Mod _RowCount
                            If countmoth > 0 Then
                                AllPageVal += 1
                            End If
                            Dim htmlstring = div_Panel.InnerHtml.Replace("</table>", "</table>,")
                            Dim _xmlarr() As String = htmlstring.Split(",")
                            Dim index As Integer = 0
                            Dim _createData As String = ""
                            For Each _point As String In _xmlarr
                                _createData = "/" & Convert.ToString(AllPageVal - 1)
                                If _point.IndexOf(_createData) > -1 Then

                                    div_Panel.InnerHtml = _point.Replace(_createData, "/" & AllPageVal)

                                Else
                                    div_Panel.InnerHtml &= _point
                                End If
                                index += 1
                            Next
                            htmlstring = Header.Replace("</table>", "</table>,")
                            _xmlarr = htmlstring.Split(",")
                            For Each _point As String In _xmlarr
                                _createData = "/" & Convert.ToString(AllPageVal - 1)
                                If _point.IndexOf(_createData) > -1 Then
                                    Header = _point.Replace(_createData, "/" & AllPageVal)
                                Else
                                    Header &= _point
                                End If
                            Next
                            CreateTag &= "<div style='height:1cm;'>&nbsp;</div>" & Header
                        End If
                    Else
                        CreateTag = CreateTag & "</table>" & "<div style='height:2cm;'>&nbsp;</div>" & Header
                    End If
                ElseIf SwitchLevel.SelectedValue = 3 Then
                    If countPageValue = 2 Then
                        _RowCount = 17
                        CreateTag = CreateTag & "</table>" & "<div style='height:0.5cm;'>&nbsp;</div>" & Header
                    ElseIf countPageValue = 3 Then
                        _RowCount = 22
                        CreateTag = CreateTag & "</table>" & "<div style='height:1.5cm;'>&nbsp;</div>" & Header
                    ElseIf countPageValue = 4 Then
                        _RowCount = 19
                        CreateTag = CreateTag & "</table>" & "<div style='height:0.5cm;'>&nbsp;</div>" & Header
                    ElseIf countPageValue = 5 Then
                        CreateTag = CreateTag & "</table>" & "<div style='height:1.5cm;'>&nbsp;</div>" & Header
                    Else
                        CreateTag = CreateTag & "</table>" & "<div style='heihgt:4.8cm;'>&nbsp;</div>" & Header
                    End If

                Else
                    CreateTag = CreateTag & "</table>" & "<div style='height:4.8cm;'>&nbsp;</div>" & Header
                End If
                div_Panel.InnerHtml &= CreateTag
                count = 1
                countPageValue += 1
            End If
            If SwitchLevel.SelectedValue = 0 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td align='center' >" & _item.ModuleID.ToString() & "</td><td align='left' style='padding-left:30px;'>" & _item.ModuleNameEN.ToString() & "</td></tr>"
            ElseIf SwitchLevel.SelectedValue = 1 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'>"
                CreateTag &= "<td align='center' style='padding-left:30px;'>" & _item.ModuleID_Level0.ToString() & "</td>"
                CreateTag &= "<td align='center'>" & _item.ModuleID.ToString() & "</td>"
                CreateTag &= "<td align='left' style='margin-left:30px;'>" & _item.ModuleNameEN.ToString() & "</td>"
                CreateTag &= "<td aling='center'>" & _item.MenuSeq.ToString() & "</td></tr>"
            ElseIf SwitchLevel.SelectedValue = 2 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'>"
                CreateTag &= "<td align='center'>" & _item.ModuleID_Level0.ToString() & "</td>"
                CreateTag &= "<td aling='center'>" & _item.ModuleID_Level1.ToString() & "</td>"
                CreateTag &= "<td align='center'>" & _item.ModuleID.ToString() & "</td>"
                CreateTag &= "<td align='left' style='margin-left:30px;'>" & _item.ModuleNameTH.ToString() & "</td>"
                CreateTag &= "<td align='center'>" & _item.MenuSeq.ToString() & "</td></tr>"
            ElseIf SwitchLevel.SelectedValue = 3 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'>"
                CreateTag &= "<td align='center'>" & _item.ModuleID_Level0.ToString() & "</td>"
                CreateTag &= "<td aling='center'>" & _item.ModuleID_Level1.ToString() & "</td>"
                CreateTag &= "<td aling='center'>" & _item.ModuleID_Level2.ToString() & "</td>"
                CreateTag &= "<td align='center'>" & _item.ModuleID.ToString() & "</td>"
                CreateTag &= "<td align='left' style='margin-left:30px;'>" & _item.ModuleNameTH.ToString() & "</td>"
                CreateTag &= "<td align='center'>" & _item.MenuSeq.ToString() & "</td></tr>"
            ElseIf SwitchLevel.SelectedValue = 4 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'>"
                CreateTag &= "<td align='center'>" & _item.ModuleID_Level0.ToString() & "</td>"
                CreateTag &= "<td aling='center'>" & _item.ModuleID_Level1.ToString() & "</td>"
                CreateTag &= "<td aling='center'>" & _item.ModuleID_Level2.ToString() & "</td>"
                CreateTag &= "<td aling='center'>" & _item.ModuleID_Level3.ToString() & "</td>"
                CreateTag &= "<td align='center'>" & _item.ModuleID.ToString() & "</td>"
                CreateTag &= "<td align='left' style='margin-left:30px;'>" & _item.ModuleNameTH.ToString() & "</td>"
                CreateTag &= "<td align='center'>" & _item.MenuSeq.ToString() & "</td></tr>"
            End If
            div_Panel.InnerHtml &= CreateTag
            count += 1
        Next
    End Sub

    Protected Sub gv_level0_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_level0.PageIndexChanging
        gv_level0.PageIndex = e.NewPageIndex
        Loaddata(0)
    End Sub

    Protected Sub gv_level1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_level1.PageIndexChanging
        gv_level1.PageIndex = e.NewPageIndex
        Loaddata(1)
    End Sub

    Protected Sub gv_level2_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_level2.PageIndexChanging
        gv_level2.PageIndex = e.NewPageIndex
        Loaddata(2)
    End Sub

    Protected Sub gv_level3_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_level3.PageIndexChanging
        gv_level3.PageIndex = e.NewPageIndex
        Loaddata(3)
    End Sub

    Protected Sub gv_level4_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_level4.PageIndexChanging
        gv_level4.PageIndex = e.NewPageIndex
        Loaddata(4)
    End Sub

    Protected Sub LVIID_TextChanged(sender As Object, e As EventArgs) Handles LVIID.TextChanged
        If LVIID.Text <> "" Then
            If IsNumeric(LVIID.Text) Then
                ClearError()
            Else
                Error_InputLVIID.Style.Add("color", "#FF0000")
                Error_InputLVIID.InnerHtml = "คุณกรอกข้อมูลผิดรูปแบบ กรุณากรอกข้อมูลอีกรอบ"
                LVIID.Text = "0"
            End If
        End If

    End Sub

    Protected Sub LVIMenuseq_TextChanged(sender As Object, e As EventArgs) Handles LVIMenuseq.TextChanged
        If LVIMenuseq.Text <> "" Then
            If Regex.IsMatch(LVIMenuseq.Text, "^[0-9]*$") Then
                ClearError()
            Else
                Error_InputLVIMenuseq.Style.Add("color", "#FF0000")
                Error_InputLVIMenuseq.InnerHtml = "คุณกรอกข้อมูลผิดรูปแบบ กรุณากรอกข้อมูลอีกรอบ"
                LVIMenuseq.Text = "0"
            End If
        End If
    End Sub

    Protected Sub gv_level0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_level0.SelectedIndexChanged
        datas = gv_level0.SelectedRow.Cells(0).Text
        _GroupCode = CType(gv_level0.SelectedRow.FindControl("lbGroupCode_level0"), LinkButton)
        datas = _GroupCode.Text
        _DataModule = _usermodule.GetLevel0Info(Convert.ToInt32(datas))
        LVIID.Text = _DataModule.ModuleID
        LVIName_th.Value = _DataModule.ModuleNameEN
        'LVIName_en.Value = _DataModule.ModuleNameEN
        LVIPath.Value = _DataModule.LinkPage
        LVIMenuseq.Text = _DataModule.MenuSeq
        LVIID.Focus()
    End Sub

   

    Protected Sub gv_level2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_level2.SelectedIndexChanged
        datas = gv_level2.SelectedRow.Cells(0).Text
        _GroupCode = CType(gv_level2.SelectedRow.FindControl("lbGroupCode2_level2"), LinkButton)
        datas = _GroupCode.Text
        _DataModule = _usermodule.GetLevel2Info(Convert.ToInt32(datas))
        LVIID.Text = _DataModule.ModuleID
        LVIName_th.Value = _DataModule.ModuleNameTH
        'LVIName_en.Value = _DataModule.ModuleNameEN
        LVIPath.Value = _DataModule.LinkPage
        LVIMenuseq.Text = _DataModule.MenuSeq
        dd_Menulevel_0.SelectedValue = _DataModule.ParentID_Level1
        dd_Menulevel_0_SelectedIndexChanged(sender, e)
        dd_Menulevel_1.SelectedValue = _DataModule.ParentID
        LVIID.Focus()
    End Sub

    Protected Sub gv_level3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_level3.SelectedIndexChanged
        datas = gv_level3.SelectedRow.Cells(0).Text
        _GroupCode = CType(gv_level3.SelectedRow.FindControl("lbGroupCode3_level3"), LinkButton)
        datas = _GroupCode.Text
        _DataModule = _usermodule.GetLevel3Info(Convert.ToInt32(datas))
        LVIID.Text = _DataModule.ModuleID
        LVIName_th.Value = _DataModule.ModuleNameTH
        'LVIName_en.Value = _DataModule.ModuleNameEN
        LVIPath.Value = _DataModule.LinkPage
        LVIMenuseq.Text = _DataModule.MenuSeq
        dd_Menulevel_0.SelectedValue = _DataModule.ParentID_Level1
        dd_Menulevel_0_SelectedIndexChanged(sender, e)
        dd_Menulevel_1.SelectedValue = _DataModule.ParentID_Level2
        dd_Menulevel_1_SelectedIndexChanged(sender, e)
        dd_Menulevel_2.SelectedValue = _DataModule.ParentID
        LVIID.Focus()
    End Sub

   
    Protected Sub gv_level4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_level4.SelectedIndexChanged
        datas = gv_level4.SelectedRow.Cells(0).Text
        datas = gv_level4.SelectedRow.RowIndex.ToString()
        _GroupCode = CType(gv_level4.SelectedRow.FindControl("lbGroupCode4_level4"), LinkButton)
        datas = _GroupCode.Text
        _DataModule = _usermodule.GetLevel4Info(Convert.ToInt32(datas))
        LVIID.Text = _DataModule.ModuleID
        LVIName_th.Value = _DataModule.ModuleNameTH
        'LVIName_en.Value = _DataModule.ModuleNameEN
        LVIPath.Value = _DataModule.LinkPage
        LVIMenuseq.Text = _DataModule.MenuSeq
        dd_Menulevel_0.SelectedValue = _DataModule.ParentID_Level1
        dd_Menulevel_0_SelectedIndexChanged(sender, e)
        dd_Menulevel_1.SelectedValue = _DataModule.ParentID_Level2
        dd_Menulevel_1_SelectedIndexChanged(sender, e)
        dd_Menulevel_2.SelectedValue = _DataModule.ParentID_Level3
        dd_Menulevel_2_SelectedIndexChanged(sender, e)
        dd_Menulevel_3.SelectedValue = _DataModule.ParentID
        LVIID.Focus()
    End Sub

    'Protected Sub gv_level1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_level1.SelectedIndexChanged
    '    datas = gv_level1.SelectedRow.Cells(0).Text
    '    _GroupCode = CType(gv_level1.SelectedRow.FindControl("lbGroupCode1_level1"), LinkButton)
    '    datas = _GroupCode.Text
    '    _DataModule = _usermodule.GetLevel1Info(Convert.ToInt32(datas))
    '    LVIID.Text = _DataModule.ModuleID
    '    LVIName_th.Value = _DataModule.ModuleNameTH
    '    LVIName_en.Value = _DataModule.ModuleNameEN
    '    LVIPath.Value = _DataModule.LinkPage
    '    LVIMenuseq.Text = _DataModule.MenuSeq
    '    LVIID.Focus()
    'End Sub

    Protected Sub ClearData()
        LVIID.Text = ""
        'LVIName_en.Value = ""
        LVIName_th.Value = ""
        LVIPath.Value = ""
        LVIMenuseq.Text = ""
    End Sub

    Protected Sub ClearError()
        Error_InputLVIID.Style.Remove("background-color")
        Error_InputLVIID.Style.Remove("color")
        Error_InputLVIID.InnerHtml = ""
        Error_InputLVIMenuseq.Style.Remove("background-color")
        Error_InputLVIMenuseq.Style.Remove("color")
        Error_InputLVIMenuseq.InnerHtml = ""
    End Sub

    Protected Sub gv_level1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_level1.SelectedIndexChanged
        datas = gv_level1.SelectedRow.Cells(0).Text
        _GroupCode = CType(gv_level1.SelectedRow.FindControl("lbGroupCode1_level1"), LinkButton)
        datas = _GroupCode.Text
        _DataModule = _usermodule.GetLevel1Info(Convert.ToInt32(datas))
        LVIID.Text = _DataModule.ModuleID
        LVIName_th.Value = _DataModule.ModuleNameTH
        'LVIName_en.Value = _DataModule.ModuleNameEN
        LVIPath.Value = _DataModule.LinkPage
        LVIMenuseq.Text = _DataModule.MenuSeq
        dd_Menulevel_0.SelectedValue = _DataModule.ParentID
        LVIID.Focus()


    End Sub
    Protected Sub ClearGrid()
        gv_level0.SelectedIndex = -1 : gv_level1.SelectedIndex = -1 : gv_level2.SelectedIndex = -1 : gv_level3.SelectedIndex = -1 : gv_level4.SelectedIndex = -1
    End Sub

    Protected Sub ShowGridByLevel()

        If SwitchLevel.SelectedValue = 1 Then
            gv_level1.DataSource = _usermodule.GetDataModuleLevelByParent(1, dd_Menulevel_0.SelectedValue)
            gv_level1.DataBind()
        ElseIf SwitchLevel.SelectedValue = 2 Then
            If IsNothing(dd_Menulevel_1.SelectedValue) Or dd_Menulevel_1.SelectedValue = "" Then
                gv_level2.DataSource = _usermodule.GetDataModuleLevelByParent(2, -1)
                gv_level2.DataBind()
            Else
                gv_level2.DataSource = _usermodule.GetDataModuleLevelByParent(2, dd_Menulevel_1.SelectedValue)
                gv_level2.DataBind()
            End If
        ElseIf SwitchLevel.SelectedValue = 3 Then
            If IsNothing(dd_Menulevel_2.SelectedValue) Or dd_Menulevel_2.SelectedValue = "" Then
                gv_level3.DataSource = _usermodule.GetDataModuleLevelByParent(3, -1)
                gv_level3.DataBind()
            Else
                gv_level3.DataSource = _usermodule.GetDataModuleLevelByParent(3, dd_Menulevel_2.SelectedValue)
                gv_level3.DataBind()
            End If
        ElseIf SwitchLevel.SelectedValue = 4 Then
            If IsNothing(dd_Menulevel_3.SelectedValue) Or dd_Menulevel_3.SelectedValue = "" Then
                gv_level4.DataSource = _usermodule.GetDataModuleLevelByParent(4, -1)
                gv_level4.DataBind()
            Else
                gv_level4.DataSource = _usermodule.GetDataModuleLevelByParent(4, dd_Menulevel_3.SelectedValue)
                gv_level4.DataBind()
            End If

        End If

    End Sub

End Class