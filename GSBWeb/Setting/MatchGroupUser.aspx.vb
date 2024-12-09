Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports Arsoft.Utility
Imports System
Imports System.IO


Public Class MatchGroupUser
    Inherits System.Web.UI.Page
    Dim _moduleAcc As New ModuleAccess
    Dim _userAcc As New UserAccess
    Dim _priAcc As New PrivilegeAccess
    Dim _userID As Integer
    Dim _usernames As String
    Dim _logmessage As String
    Dim _usermodule As New UserModuleBiz
    Dim MessageBox_Result As Integer = -1
    Dim flag As Integer = 0
    Dim _command As Integer = -1
    Dim _userentityforlog As UserEntity

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
        End If
        If flag = 0 Then
            If Request.Cookies.Keys.Count > 0 Then
                Dim cookiedata As String
                If Request.Cookies("finishdataRDM_Web") Is Nothing Then

                Else
                    cookiedata = ConvertBase64ToText(Request.Cookies("finishdataRDM_Web").Value)
                    If cookiedata.IndexOf("สำเร็จ") > -1 Then
                        MessageBoxAlert("Success", cookiedata, "", "ปิด", False, True)
                        flag = 1
                        Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    Else
                        MessageBoxAlert("Error", cookiedata, "", "ปิด", False, True)
                        flag = 1
                        Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    End If
                End If
                If Request.Cookies("BackupDataUserIDRDM_Web") Is Nothing Then
                Else
                    cookiedata = ConvertBase64ToText(Request.Cookies("BackupDataUserIDRDM_Web").Value)
                    User_ID.Value = cookiedata
                    Response.Cookies("BackupDataUserIDRDM_Web").Expires = DateTime.Now.AddDays(-1)
                End If
                If Request.Cookies("BackupDataUsernameRDM_Web") Is Nothing Then
                Else
                    cookiedata = ConvertBase64ToText(Request.Cookies("BackupDataUsernameRDM_Web").Value)
                    username.Value = cookiedata
                    Response.Cookies("BackupDataUsernameRDM_Web").Expires = DateTime.Now.AddDays(-1)
                End If

            End If
        End If
    End Sub

    Protected Sub LoadData()
        gv_GroupUser.DataSource = _usermodule.GetWorkgroup("u")
        gv_GroupUser.DataBind()
        User_ID.Focus()
    End Sub

    Protected Sub gv_GroupUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_GroupUser.SelectedIndexChanged

        Dim _Checkbox As ImageButton = CType(gv_GroupUser.SelectedRow.FindControl("ib_Checkbox"), ImageButton)
        If _Checkbox.ImageUrl = "~/Images/checkbox _UnChecked.png" Then
            _Checkbox.ImageUrl = "~/Images/checkbox_Checked.png"
        Else
            _Checkbox.ImageUrl = "~/Images/checkbox _UnChecked.png"
        End If
        'Dim _SelectedGroupCode As Label = CType(gv_GroupUser.SelectedRow.FindControl("GroupcodeData"), Label)
        'For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
        '    _Checkbox = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
        '    Dim _NormalGroupCode As Label = CType(gv_GroupUser.Rows(i).FindControl("GroupcodeData"), Label)
        '    If _NormalGroupCode.Text <> _SelectedGroupCode.Text Then
        '        _Checkbox.ImageUrl = "~/Images/checkbox _UnChecked.png"
        '    End If
        'Next
        '_Checkbox.Focus()
    End Sub

    Protected Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Dim count As Integer = 0
        Dim _setdata As New List(Of PrivilegeEntity)
        Dim _setsubdata As PrivilegeEntity
        Dim Results As Boolean
        Dim EntitySync As New List(Of UserEntity)
        EntitySync = _usermodule.SelectUserDataFilterbyEmployeeID(User_ID.Value)
        If EntitySync.Count > 0 Then
            _command = 1
            Response.Cookies("SetCommandData_GSBWebsite").Value = _command
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            If MessageBox_Result = -1 Then
                btn_OK.Attributes.Remove("data-dismiss")
                MessageBoxAlert("Question", "ต้องการบันทึกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
            Else
                btn_OK.Attributes.Add("data-dismiss", "modal")
            End If
            If MessageBox_Result > 0 Then

                For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
                    Dim _checkbox As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
                    If _checkbox.ImageUrl = "~/Images/checkbox_Checked.png" Then
                        count += 1
                    End If
                Next
                If username.Value <> "" And User_ID.Value <> "" Or User_ID.Value <> Nothing Or username.Value <> Nothing Then
                    If count > 0 Then
                        Dim _realuserid As Integer = _usermodule.GetUserID(User_ID.Value)
                        For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
                            Dim _checkbox As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
                            If _checkbox.ImageUrl = "~/Images/checkbox_Checked.png" Then

                                Dim _CodeData As Label = CType(gv_GroupUser.Rows(i).FindControl("GroupcodeData"), Label)
                                _setsubdata = New PrivilegeEntity
                                With _setsubdata
                                    .GroupCode = _CodeData.Text
                                    .UserID = _realuserid
                                End With
                                _setdata.Add(_setsubdata)
                            End If
                        Next
                        Results = _usermodule.SaveGroupUserDataPrivilage(_setdata)
                        If Results = True Then
                            MessageBoxAlert("Success", "บันทึกสำเร็จ", "", "ปิด", False, True)
                            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกสำเร็จ")
                            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                        Else
                            MessageBoxAlert("Error", "บันทึกล้มเหลว กรุณาทำรายการใหม่", "", "ปิด", False, True)
                            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกล้มเหลว กรุณาทำรายการใหม่")
                            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                        End If
                    Else
                        If User_ID.Value = "" And username.Value = "" Or User_ID.Value = Nothing Or username.Value = Nothing Then
                            MessageBoxAlert("Error", "กรุณากรอกรหัสผู้ใช้และทำการค้นหาผู้ใช้", "", "ปิด", False, True)
                            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("กรุณากรอกรหัสผู้ใช้และทำการค้นหาผู้ใช้")
                            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                        Else
                            MessageBoxAlert("Error", "กรุณาเลือกกลุ่มให้ผู้ใช้", "", "ปิด", False, True)
                            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("กรุณาเลือกกลุ่มให้ผู้ใช้") : Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                            Response.Cookies("BackupDataUserIDRDM_Web").Value = User_ID.Value : Response.Cookies("BackupDataUserIDRDM_Web").Expires = DateTime.Now.AddDays(1)
                            Response.Cookies("BackupDataUsernameRDM_Web").Value = username.Value : Response.Cookies("BackupDataUsernameRDM_Web").Expires = DateTime.Now.AddDays(1)
                        End If
                    End If
                End If
            End If
        Else
            MessageBoxAlert("Error", "บันทึกล้มเหลว กรุณาทำรายการใหม่", "", "ปิด", False, True)
        End If
        
        _userentityforlog = New UserEntity
        _userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
        Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

        With _userentityforlog
            .GroupID = 3
            .GroupName_TH = "กำหนดผู้ใช้งานเข้ากับกลุ่มงาน"
            .UserActivity = "บันทึกกลุ่มงาน " & _GroupData
        End With
        _usermodule.AddLogdata(_userentityforlog)
    End Sub

    Protected Sub Submit_Click(sender As Object, e As EventArgs) Handles Submit.Click
        Dim EntitySync As New List(Of UserEntity)
        If User_ID.Value <> "" Or User_ID.Value <> Nothing Then
            EntitySync = _usermodule.SelectUserDataFilterbyEmployeeID(User_ID.Value)
            If EntitySync.Count = 0 Then
                'CancelManagement(0)
                username.Value = ""
                MessageBoxAlert("Error", "กรอกรหัสผู้ใช้ผิด กรุณากรอกรหัสผู้ใช้ใหม่", "", "ปิด", False, True)
            Else
                username.Value = EntitySync.Item(0).FirstNameTH
                CancelManagement(1)
                Dim _realuserid As Integer = _usermodule.GetUserID(User_ID.Value)
                Dim _GetGroupPrivilage As New List(Of PrivilegeEntity)
                _GetGroupPrivilage = _usermodule.SeletGroupUserPrivilage(_realuserid)
                For i As Integer = 0 To _GetGroupPrivilage.Count - 1
                    Dim _checkbox As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
                    Dim _groupcode As Label = CType(gv_GroupUser.Rows(i).FindControl("GroupcodeData"), Label)
                    Dim GetGroupcode As String = _usermodule.SelectGroupCodeFromGroupID(_GetGroupPrivilage.Item(i).GroupID)
                    If _groupcode.Text = GetGroupcode.ToString() Then
                        _checkbox.ImageUrl = "~/Images/checkbox_Checked.png"
                    Else
                        For j As Integer = 0 To gv_GroupUser.Rows.Count - 1
                            Dim _groupcode2 As Label = CType(gv_GroupUser.Rows(j).FindControl("GroupcodeData"), Label)
                            Dim _checkbox2 As ImageButton = CType(gv_GroupUser.Rows(j).FindControl("ib_Checkbox"), ImageButton)
                            If _groupcode2.Text = GetGroupcode.ToString() Then
                                _checkbox2.ImageUrl = "~/Images/checkbox_Checked.png"
                            End If
                        Next
                    End If
                Next
            End If
        Else
            MessageBoxAlert("Error", "กรุณากรอกรหัสผู้ใช้", "", "ปิด", False, True)
        End If
        '_userentityforlog = New UserEntity
        '_userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
        'Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

        'With _userentityforlog
        '    .GroupID = 3
        '    .GroupName_TH = "กำหนดผู้ใช้งานเข้ากับกลุ่มงาน"
        '    .UserActivity = "ค้นหาผู้ใช้ " & _GroupData
        'End With
        '_usermodule.AddLogdata(_userentityforlog)
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

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If btn_OK.Visible = True Then
            MessageBox_Result = 1
            _command = Request.Cookies("SetCommandData_GSBWebsite").Value
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(-1)
            If _command = 1 Then
                btn_Save_Click(sender, e)
            ElseIf _command = 2 Then
                btn_Delete_User_Click(sender, e)
            ElseIf _command = 3 Then
                btn_Delete_Group_Click(sender, e)
            ElseIf _command = 4 Then
                btn_Cancel_User_Click(sender, e)
            ElseIf _command = 5 Then
                btn_Cancel_Group_Click(sender, e)
            End If
            MessageBox_Result = -1
            'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "$('#AlertBox').modal('hide');display:none", True)
            'UpdModal.Update()

        End If
        Response.Redirect("~/Setting/MatchGroupUser.aspx")
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

    Protected Sub btn_Delete_User_Click(sender As Object, e As EventArgs) Handles btn_Delete_User.Click
        If User_ID.Value <> "" Then
            _command = 2
            Response.Cookies("SetCommandData_GSBWebsite").Value = _command
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            Dim Result As Boolean
            If MessageBox_Result = -1 Then
                btn_OK.Attributes.Remove("data-dismiss")
                MessageBoxAlert("Question", "ต้องการลบผู้ใช้นี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
            End If
            If MessageBox_Result = 1 Then
                Result = _usermodule.Delete(User_ID.Value)
                If Result = True Then
                    MessageBoxAlert("Success", "ลบผู้ใช้สำเร็จ", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบผู้ใช้สำเร็จ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                Else
                    MessageBoxAlert("Error", "ลบผู้ใช้ล้มเหลวเนื่องจากไม่มีผู้ใช้คนนี้หรือเลือกผู้ใช้ผิดคน กรุณาทำรายการใหม่", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบผู้ใช้ล้มเหลวเนื่องจากไม่มีผู้ใช้คนนี้หรือเลือกผู้ใช้ผิดคน กรุณาทำรายการใหม่")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                End If
            End If
        Else
        MessageBoxAlert("Error", "กรุณากรอกรหัสผู้ใช้งานก่อนทำการลบ", "", "ปิด", False, True)
        End If
        _userentityforlog = New UserEntity
        _userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
        Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

        With _userentityforlog
            .GroupID = 3
            .GroupName_TH = "กำหนดผู้ใช้งานเข้ากับกลุ่มงาน"
            .UserActivity = "ลบผู้ใช้ " & _GroupData
        End With
        _usermodule.AddLogdata(_userentityforlog)
    End Sub

    Protected Sub btn_Cancel_User_Click(sender As Object, e As EventArgs) Handles btn_Cancel_User.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 4
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการยกเลิกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            Dim counting As Integer
            For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
                Dim _checkbox As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
                If _checkbox.ImageUrl = "~/Images/checkbox_Checked.png" Then
                    counting += 1
                End If
            Next
            If counting > 0 Then
                CancelManagement(-1)
            Else
                CancelManagement(0)
            End If
            'Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ยกเลิกรายการสำเร็จ")
            'Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            '_userentityforlog = New UserEntity
            '_userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
            'Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

            'With _userentityforlog
            '    .GroupID = 3
            '    .GroupName_TH = "กำหนดผู้ใช้งานเข้ากับกลุ่มงาน"
            '    .UserActivity = "ยกเลิกทำรายการผู้ใช้ " & _GroupData
            'End With
            '_usermodule.AddLogdata(_userentityforlog)
        End If
       
    End Sub

    Protected Sub btn_Delete_Group_Click(sender As Object, e As EventArgs) Handles btn_Delete_Group.Click
        Dim re1 As Integer
        For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
            Dim _check As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
            If _check.ImageUrl = "~/Images/checkbox_Checked.png" Then
                re1 += 1
            End If
        Next
        If re1 > 0 Then
            _command = 3
            Response.Cookies("SetCommandData_GSBWebsite").Value = _command
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            Dim Results As Boolean
            If MessageBox_Result = -1 Then
                btn_OK.Attributes.Remove("data-dismiss")
                MessageBoxAlert("Question", "ต้องการลบรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
            End If
            If MessageBox_Result = 1 Then
                For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
                    Dim _checkbox As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
                    Dim counting As Integer
                    If _checkbox.ImageUrl = "~/Images/checkbox_Checked.png" Then
                        counting += 1
                    End If
                    If counting > 0 Then
                        Dim _realuserid As Integer = _usermodule.GetUserID(User_ID.Value)
                        Results = _usermodule.DeleteGroupUserPrivilage(_realuserid)
                    End If
                Next
                If Results = True Then
                    MessageBoxAlert("Success", "ลบสำเร็จ", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบสำเร็จ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                Else
                    MessageBoxAlert("Success", "ลบล้มเหลว", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบล้มเหลว")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                End If
            End If
        Else
            MessageBoxAlert("Error", "กรุณาเลือกกลุ่มงานที่ต้องการลบ", "", "ปิด", False, True)
        End If
        _userentityforlog = New UserEntity
        _userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
        Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

        With _userentityforlog
            .GroupID = 3
            .GroupName_TH = "กำหนดผู้ใช้งานเข้ากับกลุ่มงาน"
            .UserActivity = "ลบกลุ่มงาน " & _GroupData
        End With
        _usermodule.AddLogdata(_userentityforlog)
    End Sub

    Protected Sub btn_Cancel_Group_Click(sender As Object, e As EventArgs) Handles btn_Cancel_Group.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 5
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการยกเลิกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            CancelManagement(1)
            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ยกเลิกรายการสำเร็จ")
            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            Response.Cookies("BackupDataUserIDRDM_Web").Value = ConvertTextToBase64(User_ID.Value)
            Response.Cookies("BackupDataUserIDRDM_Web").Expires = DateTime.Now.AddDays(1)
            Response.Cookies("BackupDataUsernameRDM_Web").Value = ConvertTextToBase64(username.Value)
            Response.Cookies("BackupDataUsernameRDM_Web").Expires = DateTime.Now.AddDays(1)
            '_userentityforlog = New UserEntity
            '_userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
            'Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

            'With _userentityforlog
            '    .GroupID = 3
            '    .GroupName_TH = "กำหนดผู้ใช้งานเข้ากับกลุ่มงาน"
            '    .UserActivity = "ยกเลิกกลุ่มงาน " & _GroupData
            'End With
            '_usermodule.AddLogdata(_userentityforlog)
        End If
    End Sub

    Protected Sub CancelManagement(ByVal _types As Integer)
        If _types = 0 Then
            User_ID.Value = ""
            username.Value = ""
        ElseIf _types = 1 Then
            For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
                Dim _Checkbox As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
                _Checkbox.ImageUrl = "~/Images/checkbox _UnChecked.png"
            Next
        ElseIf _types = -1 Then
            ClearALL()
        End If
    End Sub
    Protected Sub ClearALL()
        User_ID.Value = ""
        username.Value = ""
        For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
            Dim _Checkbox As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
            _Checkbox.ImageUrl = "~/Images/checkbox _UnChecked.png"
        Next
    End Sub

    Protected Sub btn_Print_Group_Click(sender As Object, e As EventArgs) Handles btn_Print_Group.Click

        CreatePrintArea()
        '_userentityforlog = New UserEntity
        '_userentityforlog = _usermodule.GetUserInfo(Session("UserName"))
        'Dim _GroupData As String = _usermodule.GetGroupDataforLogData(_userentityforlog.UserID)

        'With _userentityforlog
        '    .GroupID = 3
        '    .GroupName_TH = "กำหนดผู้ใช้งานเข้ากับกลุ่มงาน"
        '    .UserActivity = "พิมพ์รายการ " & _GroupData
        'End With
        '_usermodule.AddLogdata(_userentityforlog)
    End Sub

    Protected Sub CreatePrintArea()
        'Dim _ScriptPrint As String
        '_ScriptPrint = "<script type='text/javascript'>"
        '_ScriptPrint += "function(){"
        '_ScriptPrint += "var printWindow = window.open('', '', 'height=600,width=800');"
        '_ScriptPrint += "printWindow.document.write('<html><body>');"
        '_ScriptPrint += "printWindow.document.write('</head><body >');"

        CreateArea()

        '_ScriptPrint += "printWindow.document.write('" + div_Panel.InnerHtml + "');"
        '_ScriptPrint += "printWindow.document.write('</body></html>');"
        '_ScriptPrint += "printWindow.document.close();"
        '_ScriptPrint += "printWindow.print();"
        '_ScriptPrint += "</script>"



        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "div_panel", "printing()", True)

    End Sub
    Protected Sub btn_Excel_Click(sender As Object, e As EventArgs) Handles btn_Excel.Click
        CreatePrintAreaExcel()
    End Sub

    Protected Sub CreatePrintAreaExcel()
        Dim CreateTag As String
        Dim countAll As Integer = gv_GroupUser.Rows.Count
        Dim AllPageVal As Integer
        Dim countmoth As Integer
        Dim countPageValue As Integer = 1
        Dim xxx As String
        If countAll < 36 Then
            AllPageVal = 1
        Else
            AllPageVal = countAll / 32
            countmoth = countAll Mod 32
            If countmoth > 0 Then
                AllPageVal += 1
            End If
        End If
        Dim Header As String = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดผู้ใช้งานเข้ากับกลุ่มงาน</td></tr>"
        Header &= "<tr><td colspan='2'>&nbsp;</td></tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr>"
        Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr style='background-color:#FF388C;color:#FFF;'><td align='left'>กลุ่มงาน</td></tr>"
        countPageValue += 1
        div_Panel.InnerHtml = Header
        Dim _color As String = ""
        Dim count As Integer = 1

        For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
            Dim _check As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
            Dim _gcode As Label = CType(gv_GroupUser.Rows(i).FindControl("GroupcodeData"), Label)
            Dim _gname As Label = CType(gv_GroupUser.Rows(i).FindControl("GroupnameData"), Label)

            If _color = "" Then
                _color = "#FFE8EE"
            ElseIf _color = "#FFE8EE" Then
                _color = "#FFCEDB"
            Else
                _color = "#FFE8EE"
            End If

            If count = 32 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td style='padding-left:30px;'>" & _gcode.Text & " - " & _gname.Text & "</td></tr></table>"
                Header = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดผู้ใช้งานเข้ากับกลุ่มงาน</td></tr>"
                Header &= "<tr><td colspan='2'>&nbsp;</td></tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr>"
                Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr style='background-color:#FF388C;color:#FFF;'><td align='left'>กลุ่มงาน</td></tr>"
                CreateTag = CreateTag & "</table>" & "<div style='height:4.8cm;'></div>" & Header
                div_Panel.InnerHtml &= CreateTag
                count = 1
                countPageValue += 1
            End If

            If _check.ImageUrl = "~/Images/checkbox_Checked.png" Then  'Check data parameter export data
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td style='padding-left:30px;'>" & _gcode.Text & " - " & _gname.Text & "</td></tr>"
                Dim _groupLogData As New List(Of UserEntity)
                _groupLogData = _userAcc.GetUserdataByGroupID(_gcode.Text)

                For x As Integer = 0 To _groupLogData.Count - 1
                    CreateTag += "<tr style='background-color:#FDEDEC;color:#000;text-indent: 2.0em;'><td style='padding-left:30px;'>" & _groupLogData.Item(x).FirstNameTH & " " & _groupLogData.Item(x).LastNameTH & "</td></tr>"
                Next
                div_Panel.InnerHtml &= CreateTag
            End If
            count += 1

            xxx = "<html><body>" + div_Panel.InnerHtml + "</table></body></html>"
        Next

        Response.ContentType = "application/x-msexcel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ExcelFile.xls")
        Response.ContentEncoding = Encoding.UTF8
        Response.Write(xxx)
        Response.End()

    End Sub

    Protected Sub CreateArea()
        Dim CreateTag As String
        Dim countAll As Integer = gv_GroupUser.Rows.Count
        Dim AllPageVal As Integer
        Dim countmoth As Integer
        Dim countPageValue As Integer = 1
        If countAll < 36 Then
            AllPageVal = 1
        Else
            AllPageVal = countAll / 32
            countmoth = countAll Mod 32
            If countmoth > 0 Then
                AllPageVal += 1
            End If
        End If
        Dim Header As String = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดผู้ใช้งานเข้ากับกลุ่มงาน</td></tr>"
        Header &= "<tr><td colspan='2'>&nbsp;</td></tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr>"
        Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr style='background-color:#FF388C;color:#FFF;'><td align='left'>กลุ่มงาน</td></tr>"
        countPageValue += 1
        div_Panel.InnerHtml = Header
        Dim _color As String = ""
        Dim count As Integer = 1

        For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
            Dim _check As ImageButton = CType(gv_GroupUser.Rows(i).FindControl("ib_Checkbox"), ImageButton)
            Dim _gcode As Label = CType(gv_GroupUser.Rows(i).FindControl("GroupcodeData"), Label)
            Dim _gname As Label = CType(gv_GroupUser.Rows(i).FindControl("GroupnameData"), Label)

            If _color = "" Then
                _color = "#FFE8EE"
            ElseIf _color = "#FFE8EE" Then
                _color = "#FFCEDB"
            Else
                _color = "#FFE8EE"
            End If

            If count = 32 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td style='padding-left:30px;'>" & _gcode.Text & " - " & _gname.Text & "</td></tr></table>"
                Header = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดผู้ใช้งานเข้ากับกลุ่มงาน</td></tr>"
                Header &= "<tr><td colspan='2'>&nbsp;</td></tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr>"
                Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr style='background-color:#FF388C;color:#FFF;'><td align='left'>กลุ่มงาน</td></tr>"
                CreateTag = CreateTag & "</table>" & "<div style='height:4.8cm;'></div>" & Header
                div_Panel.InnerHtml &= CreateTag
                count = 1
                countPageValue += 1
            End If
            'Ekk comment 18/04/2019
            'CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td style='padding-left:30px;'>" & _gcode.Text & " - " & _gname.Text & "</td></tr>"

            If _check.ImageUrl = "~/Images/checkbox_Checked.png" Then  'Check data parameter export data
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td style='padding-left:30px;'>" & _gcode.Text & " - " & _gname.Text & "</td></tr>"
                Dim _groupLogData As New List(Of UserEntity)
                _groupLogData = _userAcc.GetUserdataInGroupByGroupID(_gcode.Text)

                For x As Integer = 0 To _groupLogData.Count - 1
                    CreateTag += "<tr style='background-color:#FDEDEC;color:#000;text-indent: 2.0em;'><td style='padding-left:30px;'>" & _groupLogData.Item(x).FirstNameTH & " " & _groupLogData.Item(x).LastNameTH & "</td></tr>"
                Next
                div_Panel.InnerHtml &= CreateTag
            End If
            count += 1
        Next

    End Sub


    Protected Sub gv_GroupUser_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_GroupUser.PageIndexChanging
        gv_GroupUser.PageIndex = e.NewPageIndex
        LoadData()
    End Sub


End Class