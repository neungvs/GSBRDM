Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports Arsoft.Utility

Public Class UserManagement1
    Inherits System.Web.UI.Page
    Dim DataBizz As New UserModuleBiz
    Dim MessageBox_Result As Integer = -1
    Dim _command As Integer = -1
    Dim _userentityforlog As UserEntity
    Dim flag As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()

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
    End Sub

    Protected Sub btn_AddData_Click(sender As Object, e As EventArgs) Handles btn_AddData.Click

        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 1
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการบันทึกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If

        If MessageBox_Result > 0 Then
            Dim counting As Integer = 0
            If Username.Text = "" Then
                counting += 1
            End If
            If EmployeeID.Text = "" Then
                counting += 1
            End If
            If FirstnameEN.Text = "" Then
                counting += 1
            End If
            If LastnameEN.Text = "" Then
                counting += 1
            End If
            If FirstnameTH.Text = "" Then
                counting += 1
            End If
            If LastnameTH.Text = "" Then
                counting += 1
            End If
            If counting = 0 Then
                GetUIData()
                Dim _results As Boolean = False
                Dim _results2 As Boolean = False
                Dim UInfor As New List(Of UserEntity)
                UInfor = DataBizz.SelectUserDataFilterbyUsername(Username.Text)
                If UInfor.Count = 0 Then
                    _results = DataBizz.Insert(_userentityforlog)
                Else
                    _userentityforlog.UserID = UInfor(0).UserID
                    _results = DataBizz.Update(_userentityforlog)
                End If
                Dim _sendprivilage As New List(Of PrivilegeEntity)
                Dim _setprivilage As New PrivilegeEntity
                _userentityforlog = DataBizz.GetUserInfo(_userentityforlog.UserName)
                With _setprivilage
                    .UserID = _userentityforlog.UserID
                    '.GroupID = GroupData.SelectedValue
                    .GroupCode = "NULL"
                End With
                _sendprivilage.Add(_setprivilage)
                '_results2 = DataBizz.SaveGroupUserDataPrivilage(_sendprivilage)
                If _results = True Then
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกสำเร็จ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                Else
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกล้มเหลว กรุณาทำการบันทึกอีกรอบ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                End If
            Else
                MessageBoxAlert("Error", "คุณกรอกข้อมูลไม่ครบ กรุณากรอกข้อมูลให้ครบ", "ปิด", "", True, False)
                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("คุณกรอกข้อมูลไม่ครบ กรุณากรอกข้อมูลให้ครบ")
                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                Exit Sub
            End If
            _userentityforlog = New UserEntity
            _userentityforlog = DataBizz.GetUserInfo(Session("UserName"))
            Dim _GroupData As String = DataBizz.GetGroupDataforLogData(_userentityforlog.UserID)
            With _userentityforlog
                .GroupID = 5
                .GroupName_TH = "กำหนดผู้ใช้"
                .UserActivity = "เพิ่มผู้ใช้" & Username.Text
            End With
            DataBizz.AddLogdata(_userentityforlog)
        End If
    End Sub

    Protected Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 3
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการยกเลิกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ยกเลิกรายการสำเร็จ")
            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)

            Username.Text = ""
            FirstnameEN.Text = ""
            LastnameEN.Text = ""
            FirstnameTH.Text = ""

            btn_Delete.Visible = False
            btn_AddData.Visible = False
            btn_Cancel.Visible = False
            EmployeeID.Enabled = True

            '_userentityforlog = New UserEntity
            '_userentityforlog = DataBizz.GetUserInfo(Session("UserName"))
            'Dim _GroupData As String = DataBizz.GetGroupDataforLogData(_userentityforlog.UserID)
            'With _userentityforlog
            '    .GroupID = 5
            '    .GroupName_TH = "กำหนดผู้ใช้"
            '    .UserActivity = "ยกเลิก" & _GroupData
            'End With
            'DataBizz.AddLogdata(_userentityforlog)
        End If
    End Sub

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If btn_NO.Visible = True Then
            MessageBox_Result = 1
            _command = Request.Cookies("SetCommandData_GSBWebsite").Value
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(-1)
            If _command = 1 Then
                btn_AddData_Click(sender, e)
            ElseIf _command = 2 Then
                btn_Delete_Click(sender, e)
            Else
                btn_Cancel_Click(sender, e)
            End If
        End If
        Response.Redirect("~/setting/UserManagement.aspx")
    End Sub

    Protected Sub LoadData()
        'Dim _datas As New List(Of UserEntity)
        '_datas = DataBizz.GetWorkgroup("m")
        'For Each _group As UserEntity In _datas
        '    Dim ListItem = New ListItem(_group.GroupName_TH, _group.GroupID)
        '    GroupData.Items.Add(ListItem)
        'Next
        Username.Focus()

        gv_User.DataSource = DataBizz.GetAllUserInfo()
        gv_User.DataBind()
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

    Protected Sub GetUIData()
        _userentityforlog = New UserEntity
        With _userentityforlog
            .UserName = Username.Text
            .Password = "NULL"
            .EmployeeID = EmployeeID.Text
            .FirstNameEN = FirstnameEN.Text
            .LastNameEN = LastnameEN.Text
            .FirstNameTH = FirstnameTH.Text
            .LastNameTH = LastnameTH.Text
            '.GroupID = GroupData.SelectedValue.ToString()
        End With
    End Sub

    Protected Sub btn_Search_User_Click(sender As Object, e As EventArgs) Handles btn_Search_User.Click
        Dim _activeBiz As New BLL.ActiveDirectoryManagement
        Dim _listLDAP As New List(Of String)
        Dim _UserEntityData As New UserEntity
        Dim _aduser As GSBWeb.BLL.ActiveDirectoryEntity
        Dim _msg As String = ""

        If Not EmployeeID.Text Is Nothing Then

            If IsNumeric(EmployeeID.Text) Then
                _UserEntityData = DataBizz.GetUserRDMDataByEmpidStr(EmployeeID.Text)
            Else
                Username.Text = ""
                FirstnameEN.Text = ""
                LastnameEN.Text = ""
                FirstnameTH.Text = ""
                LastnameTH.Text = ""

                btn_Delete.Visible = False
                btn_AddData.Visible = False
                btn_Cancel.Visible = False

                MessageBoxAlert("Warning", "ไม่พบข้อมูล", "ปิด", "", True, False)

                Exit Sub
            End If


            If _UserEntityData.UserID = 0 Then

                _listLDAP.Add(CStr(System.Configuration.ConfigurationManager.AppSettings("Local_LDAPDirectory")))
                _listLDAP.Add(CStr(System.Configuration.ConfigurationManager.AppSettings("GSB_LDAPDirectory")))

                Dim strPwd As String()

                strPwd = Session("Password").ToString.Split(";")


                For Each _ldapDirectory As String In _listLDAP



                    _aduser = _activeBiz.GetUserADByEmployeeID(EmployeeID.Text.Trim(), Session("UserName"), strPwd(0), _ldapDirectory)
                    _msg = _aduser.Msg

                    If _msg = "true" Then
                        Exit For
                    End If
                Next

                If _msg = "true" Then

                    Username.Text = _aduser.DisplayName
                    FirstnameEN.Text = _aduser.NameEn
                    LastnameEN.Text = _aduser.SurnameEn
                    FirstnameTH.Text = _aduser.NameLo
                    LastnameTH.Text = _aduser.SurnameLo
                    'GroupData.SelectedIndex = 0
                    'btn_Save.Visible = False
                    btn_Delete.Visible = False
                    btn_AddData.Visible = True
                    btn_Cancel.Visible = True
                    btn_AddData.Text = "เพิ่ม"
                    EmployeeID.Enabled = False

                Else

                    Username.Text = ""
                    FirstnameEN.Text = ""
                    LastnameEN.Text = ""
                    FirstnameTH.Text = ""
                    LastnameTH.Text = ""

                    btn_Delete.Visible = False
                    btn_AddData.Visible = False
                    btn_Cancel.Visible = False


                    MessageBoxAlert("Warning", "ไม่มีผู้ใช้ในธนาคาร", "ปิด", "", True, False)

                    Exit Sub

                End If


            End If

        End If

        If _UserEntityData.UserName <> Nothing And _UserEntityData.UserName <> "" Then
            Username.Text = _UserEntityData.UserName
            FirstnameEN.Text = _UserEntityData.FirstNameEN
            LastnameEN.Text = _UserEntityData.LastNameEN
            FirstnameTH.Text = _UserEntityData.FirstNameTH
            LastnameTH.Text = _UserEntityData.LastNameTH
            'GroupData.SelectedValue = _UserEntityData.GroupID
            'btn_Save.Visible = True
            'btn_AddData.Text = "บันทึก"
            btn_Delete.Visible = True
            btn_Cancel.Visible = True
            btn_AddData.Visible = False

        End If
    End Sub

    Protected Sub btn_Delete_Click(sender As Object, e As EventArgs) Handles btn_Delete.Click

        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 2
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการลบรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result = 1 Then
            Dim _UserEntityData As New UserEntity
            _UserEntityData = DataBizz.GetUserRDMDataByEmpidStr(EmployeeID.Text)
            Dim Result As Boolean = DataBizz.Delete(_UserEntityData.UserID.ToString())
            If Result = True Then
                MessageBoxAlert("Success", "ลบผู้ใช้สำเร็จ", "", "ปิด", False, True)
                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบผู้ใช้สำเร็จ")
                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            Else
                MessageBoxAlert("Error", "ลบผู้ใช้ล้มเหลวเนื่องจากไม่มีผู้ใช้คนนี้หรือเลือกผู้ใช้ผิดคน กรุณาทำรายการใหม่", "", "ปิด", False, True)
                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบผู้ใช้ล้มเหลวเนื่องจากไม่มีผู้ใช้คนนี้หรือเลือกผู้ใช้ผิดคน กรุณาทำรายการใหม่")
                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            End If
            _userentityforlog = New UserEntity
            _userentityforlog = DataBizz.GetUserInfo(Session("UserName"))
            Dim _GroupData As String = DataBizz.GetGroupDataforLogData(_userentityforlog.UserID)
            With _userentityforlog
                .GroupID = 5
                .GroupName_TH = "กำหนดผู้ใช้"
                .UserActivity = "ลบผู้ใช้" & Username.Text
            End With
            DataBizz.AddLogdata(_userentityforlog)
            EmployeeID.Enabled = True
        End If
    End Sub

    Protected Sub btn_Print_Click(sender As Object, e As EventArgs) Handles btn_Print.Click
        CreateArea()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "div_panel", "printing()", True)
    End Sub

    Protected Sub CreateArea()
        Dim CreateTag As String
        Dim countAll As Integer = gv_User.Rows.Count
        Dim AllPageVal As Integer
        Dim countmoth As Integer
        Dim numOnPage As Integer = 38
        Dim countPageValue As Integer = 1
        If countAll < numOnPage Then
            AllPageVal = 1
        Else
            AllPageVal = countAll / numOnPage
            countmoth = countAll Mod numOnPage
            If countmoth > 0 Then
                AllPageVal += 1
            End If
        End If
        Dim Header As String = "<table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดผู้ใช้</td></tr></table>"
        Header &= "<table width='80%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
        Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;'>รหัสพนักงาน</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อ</td><td align='center' style='background-color:#FF388C;color:#FFF;'>นามสกุล</td></tr>"
        countPageValue += 1
        div_Panel.InnerHtml = Header
        Dim _color As String = ""
        Dim count As Integer = 1

        For i As Integer = 0 To gv_User.Rows.Count - 1
            Dim _groupcodeData As LinkButton = CType(gv_User.Rows.Item(i).FindControl("EmployeeID"), LinkButton)
            Dim _groupname_th As LinkButton = CType(gv_User.Rows.Item(i).FindControl("FirstNameTH"), LinkButton)
            Dim _grouplastname_th As LinkButton = CType(gv_User.Rows.Item(i).FindControl("LastNameTH"), LinkButton)



            If _color = "" Then
                _color = "#FFE8EE"
            ElseIf _color = "#FFE8EE" Then
                _color = "#FFCEDB"
            Else
                _color = "#FFE8EE"
            End If

            'If count = numOnPage Then
            '    CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td style='padding-left:30px;'>" & _groupcodeData.Text & "</td><td style='padding-left:30px;'>" & _groupname_th.Text & "</td></tr>"
            '    Header = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr><tr style='size:30pt;'><td align='center'>รายงานการกำหนดผู้ใช้งานเข้ากับกลุ่มงาน</td></tr>"
            '    Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr><td align='center' >รหัสกลุ่มงาน</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อกลุ่มงาน</td></tr>"
            '    CreateTag = CreateTag & "</table>" & "<div style='height:4.8cm;'></div>" & Header
            '    div_Panel.InnerHtml &= CreateTag
            '    count = 1
            '    countPageValue += 1
            'End If
            CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td align='left' style='padding-left:30px;'>" & _groupcodeData.Text & "</td><td align='left' style='padding-left:30px;'>" & _groupname_th.Text & "</td><td align='left' style='padding-left:30px;'>" & _grouplastname_th.Text & "</td></tr>"
            div_Panel.InnerHtml &= CreateTag
            count += 1
        Next
    End Sub

    Protected Sub gv_User_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_User.PageIndexChanging
        gv_User.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub gv_User_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_User.SelectedIndexChanged
        Dim data As String
        Dim _username As LinkButton = CType(gv_User.SelectedRow.FindControl("UserName"), LinkButton)
        data = _username.Text

        Dim user As New UserEntity
        user = DataBizz.GetUserInfo(data)

        EmployeeID.Text = user.EmployeeID
        Username.Text = user.UserName
        FirstnameEN.Text = user.FirstNameEN
        LastnameEN.Text = user.LastNameEN
        FirstnameTH.Text = user.FirstNameTH
        LastnameTH.Text = user.LastNameTH
        'GroupData.SelectedValue = user.GroupID.ToString()

        'btn_Save.Visible = True
        btn_Delete.Visible = True
        btn_Cancel.Visible = True
        btn_AddData.Visible = False
        'btn_AddData.Text = "บันทึก"
        EmployeeID.Enabled = True

    End Sub

End Class