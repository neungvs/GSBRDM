Imports System.Diagnostics
Imports System.Threading
Imports System.Data
Imports System.IO
Imports GSBWeb.DAL
Imports GSBWeb.BLL
Imports Arsoft.Utility
Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Web.HttpApplicationState

Public Class UserGroupManagement
    Inherits System.Web.UI.Page
    Dim _lumb As New UserModuleBiz
    Dim gui As New Guid
    Dim listuser As New List(Of UserEntity)
    Dim MessageBox_Result As Integer = -1
    Dim _command As Integer = -1
    Dim flag As Integer = 0
    Dim _result As Boolean
    Dim _userentityforlog As UserEntity

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadData()
            GroupID.Focus()
            Enable_False()
        End If
        Progress.Visible = True
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

    Protected Sub Submit_Click(sender As Object, e As EventArgs) Handles Submit.Click
        Response.Cookies("SetCommandData_GSBWebsite").Value = 1
        Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)

        If GroupID.Text <> "" Or GroupID.Text <> Nothing Then
            If Group_Name_th.Text <> "" Or Group_Name_th.Text <> Nothing Then
                Dim GetGroupDatas As New UserEntity
                GetGroupDatas = _lumb.SearchGroupDataByGroupNameTH(Group_Name_th.Text)
                If GetGroupDatas.GroupName_TH Is Nothing Then

                    Dim data As New UserEntity
                    Dim counting As Integer = 0
                    Dim res As Boolean
                    listuser = _lumb.GetWorkgroup("m")
                    For Each Col As UserEntity In listuser
                        If Col.GroupCode = GroupID.Text Then
                            With data
                                .GroupID = Col.GroupID
                                .GroupCode = GroupID.Text
                                .GroupName_EN = Group_name_en.Text
                                .GroupName_TH = Group_Name_th.Text
                            End With
                            counting += 1
                            Exit For
                        End If
                    Next

                    Dim _GroupDatas As UserEntity = _lumb.GetGroupUserrows(GroupID.Text)
                    If _GroupDatas.GroupID <> 0 Then
                        With data
                            .GroupID = _GroupDatas.GroupID
                            .GroupCode = GroupID.Text
                            .GroupName_TH = Group_Name_th.Text
                            .GroupName_EN = Group_name_en.Text
                        End With
                        counting += 1
                    End If

                    If MessageBox_Result = -1 Then
                        btn_OK.Attributes.Remove("data-dismiss")
                        MessageBoxAlert("Question", "ต้องการบันทึกรายการนี้ ใช่หรือไม่", "ใช่", "ไม่", True, True)
                    Else
                        btn_OK.Attributes.Add("data-dismiss", "modal")
                    End If
                    If MessageBox_Result > 0 Then
                        If counting > 0 Then
                            res = _lumb.EditGroupUser(data)

                            If res = True Then
                                MessageBoxAlert("Success", "บันทึกรายการสำเร็จ", "", "ปิด", False, True)
                                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกรายการสำเร็จ")
                                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                            Else
                                MessageBoxAlert("Error", "บันทึกรายการล้มเหลว กรุณาทำรายการใหม่", "", "ปิด", False, True)
                                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกรายการล้มเหลว กรุณาทำรายการใหม่")
                                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                            End If

                        Else
                            With data
                                .GroupCode = GroupID.Text
                                .GroupName_EN = Group_name_en.Text
                                .GroupName_TH = Group_Name_th.Text
                            End With
                            res = _lumb.SaveGroupUser(data)
                            If res = True Then
                                MessageBoxAlert("Success", "บันทึกรายการสำเร็จ", "", "ปิด", False, True)
                                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกรายการสำเร็จ")
                                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                            Else
                                MessageBoxAlert("Error", "บันทึกรายการล้มเหลว กรุณาทำรายการใหม่", "", "ปิด", False, True)
                                Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกรายการล้มเหลว กรุณาทำรายการใหม่")
                                Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                            End If
                        End If
                        _userentityforlog = New UserEntity
                        _userentityforlog = _lumb.GetUserInfo(Session("UserName"))
                        Dim _GroupData As String = _lumb.GetGroupDataforLogData(_userentityforlog.UserID)

                        With _userentityforlog
                            .GroupID = 1
                            .GroupName_TH = "กำหนดกลุ่มงาน"
                            .UserActivity = "บันทึกกลุ่มงาน" & GroupID.Text
                        End With
                        _result = _lumb.AddLogdata(_userentityforlog)
                    End If

                Else

                    MessageBoxAlert("Error", "พบชื่อกลุ่มนี้แล้ว กรุณากรอกชื่อกลุ่มภาษาไทยใหม่", "", "ปิด", False, True)

                End If

            Else
                    MessageBoxAlert("Error", "กรุณากรอกชื่อกลุ่มภาษาไทย", "", "ปิด", False, True)
            End If
        Else
            MessageBoxAlert("Error", "กรุณากรอกรหัสกลุ่ม", "", "ปิด", False, True)
        End If
        
    End Sub

    Protected Sub Delete_Click(sender As Object, e As EventArgs) Handles Delete.Click
        Dim GroupsID As String = GroupID.Text
        Dim res As Boolean
        Response.Cookies("SetCommandData_GSBWebsite").Value = 2
        Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
        If GroupID.Text <> Nothing Or GroupID.Text <> "" Then
            If MessageBox_Result = -1 Then
                btn_OK.Attributes.Remove("data-dismiss")
                MessageBoxAlert("Question", "ต้องการลบรายการนี้ ใช่หรือไม่", "ใช่", "ไม่", True, True)
            Else
                btn_OK.Attributes.Add("data-dismiss", "modal")
            End If
            If MessageBox_Result > 0 Then
                res = _lumb.DeleteGroupUser(GroupsID)
                If res = True Then
                    MessageBoxAlert("Success", "ทำรายการสำเร็จ", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ทำรายการสำเร็จ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                Else
                    MessageBoxAlert("Error", "ทำรายการล้มเหลว กรุณาทำรายการใหม่", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ทำรายการล้มเหลว กรุณาทำรายการใหม่")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                End If
            End If
        Else
            MessageBoxAlert("Error", "กรุณากรอกรหัสกลุ่ม", "", "ปิด", False, True)
        End If
        _userentityforlog = New UserEntity
        _userentityforlog = _lumb.GetUserInfo(Session("UserName"))
        Dim _GroupData As String = _lumb.GetGroupDataforLogData(_userentityforlog.UserID)

        With _userentityforlog
            .GroupID = 1
            .GroupName_TH = "กำหนดกลุ่มงาน"
            .UserActivity = "ลบกลุ่มงาน" & GroupID.Text
        End With
        _result = _lumb.AddLogdata(_userentityforlog)
    End Sub

    Private Sub LoadData()
        gv_GroupUser.DataSource = _lumb.GetWorkgroup("m")
        gv_GroupUser.DataBind()
        'Dim Headertable As String
        'Headertable = "<table width='100%' border id='datatable' class='' name='datatable'><tr valign='middle' height='35px'><td align='center' width='100px'>รหัสกลุ่มงาน</td><td align='center'>ชื่อกลุ่มงาน</td></tr>"
        'Dim Footertable As String = "<tr style='display:none;'><td colspan='2'>ไม่มีข้อมูล</td></tr></table>"
        'Dim BodyTable As String = ""
        'Dim user As New List(Of UserEntity)
        'user = _lumb.GetWorkgroup()
        'If user.Count > 0 Then
        '    For Each col As UserEntity In user
        '        BodyTable &= "<tr onclick='' style='cursor:pointer;' height='35px' valign='middle'>"
        '        BodyTable &= "<td>" & col.GroupCode.ToString() & "</td>"
        '        BodyTable &= "<td align='left' style='padding-left:30px;'>" & col.GroupName_TH.ToString() & "</td>"
        '        BodyTable &= "</tr>"
        '    Next
        '    groupUserdata.InnerHtml = Headertable & BodyTable & Footertable
        'Else
        '    Footertable = "<tr ><td colspan='2'>ไม่มีข้อมูล</td></tr></table>"
        '    groupUserdata.InnerHtml = Headertable & Footertable
        'End If

    End Sub

    Protected Sub gv_GroupUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv_GroupUser.SelectedIndexChanged
        Dim data As String
        data = gv_GroupUser.SelectedRow.Cells(0).Text
        Dim _GroupCode As LinkButton = CType(gv_GroupUser.SelectedRow.FindControl("GroupCodedata"), LinkButton)
        data = _GroupCode.Text
        listuser = _lumb.GetWorkgroup("m")
        For i As Integer = 0 To listuser.Count - 1
            If listuser.Item(i).GroupCode = data Then
                GroupID.Text = listuser.Item(i).GroupCode
                Group_Name_th.Text = listuser.Item(i).GroupName_TH
                Group_name_en.Text = listuser.Item(i).GroupName_EN
            End If
        Next
        GroupID.Focus()
        Enable_True()
    End Sub

    Protected Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 3
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการยกเลิกกลุ่มงาน ใช่หรือไม่?", "ใช่", "ไม่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            ClearAllData()
            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ยกเลิกรายการสำเร็จ")
            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            '_userentityforlog = New UserEntity
            '_userentityforlog = _lumb.GetUserInfo(Session("UserName"))
            'Dim _GroupData As String = _lumb.GetGroupDataforLogData(_userentityforlog.UserID)

            'With _userentityforlog
            '    .GroupID = 1
            '    .GroupName_TH = "กำหนดกลุ่มงาน"
            '    .UserActivity = "ยกเลิกกลุ่มงาน " & _GroupData
            'End With
            '_result = _lumb.AddLogdata(_userentityforlog)
        End If
    End Sub

    Protected Sub ClearAllData()
        GroupID.Text = ""
        Group_Name_th.Text = ""
        Group_name_en.Text = ""
    End Sub

    Protected Sub GroupID_TextChanged(sender As Object, e As EventArgs) Handles GroupID.TextChanged
        'If GroupID.Text.Length = 5 Then
        '    listuser = _lumb.GetWorkgroup()
        '    For i As Integer = 0 To listuser.Count - 1
        '        If listuser.Item(i).GroupCode = GroupID.Text Then
        '            Group_Name_th.Text = listuser.Item(i).GroupName_TH.ToString()
        '            Group_name_en.Text = listuser.Item(i).GroupName_EN.ToString()
        '        End If
        '    Next
        'End If
    End Sub

    Protected Sub btn_Search_Click(sender As Object, e As EventArgs) Handles btn_Search.Click

        Dim GetGroupDatas As New UserEntity
        GetGroupDatas = _lumb.SearchGroupDataByGroupNameTH(Group_Name_th.Text)
        If GetGroupDatas.GroupID <> Nothing Then
            GroupID.Text = GetGroupDatas.GroupCode
            Group_Name_th.Text = GetGroupDatas.GroupName_TH
            Group_name_en.Text = GetGroupDatas.GroupName_EN
            Group_Name_th.Focus()
        Else
            MessageBoxAlert("Error", "ไม่มีกลุ่มงานนี้", "", "ปิด", False, True)
            Group_name_en.Text = ""
            GroupID.Text = ""
            Group_Name_th.Focus()
        End If
        '_userentityforlog = New UserEntity
        '_userentityforlog = _lumb.GetUserInfo(Session("UserName"))
        'Dim _GroupData As String = _lumb.GetGroupDataforLogData(_userentityforlog.UserID)

        'With _userentityforlog
        '    .GroupID = 1
        '    .GroupName_TH = "กำหนดกลุ่มงาน"
        '    .UserActivity = "ค้นหาผู้ใช้ " & _GroupData
        'End With
        '_result = _lumb.AddLogdata(_userentityforlog)
        Enable_True()
    End Sub

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If btn_OK.Visible = True Then
            MessageBox_Result = 1
            _command = Request.Cookies("SetCommandData_GSBWebsite").Value
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(-1)
            If _command = 1 Then
                Submit_Click(sender, e)
            ElseIf _command = 2 Then
                Delete_Click(sender, e)
            ElseIf _command = 3 Then
                Cancel_Click(sender, e)
            End If
            MessageBox_Result = -1
        End If
        Response.Redirect("~/setting/UserGroupManagement.aspx")
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

    Protected Sub btn_Print_Click(sender As Object, e As EventArgs) Handles btn_Print.Click
        CreateArea()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "div_panel", "printing()", True)
        '_userentityforlog = New UserEntity
        '_userentityforlog = _lumb.GetUserInfo(Session("UserName"))
        'Dim _GroupData As String = _lumb.GetGroupDataforLogData(_userentityforlog.UserID)

        'With _userentityforlog
        '    .GroupID = 1
        '    .GroupName_TH = "กำหนดกลุ่มงาน"
        '    .UserActivity = "พิมพ์กลุ่มงาน " & _GroupData
        'End With
        '_result = _lumb.AddLogdata(_userentityforlog)
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
    Protected Sub CreateArea()
        Dim CreateTag As String
        Dim countAll As Integer = gv_GroupUser.Rows.Count
        Dim AllPageVal As Integer
        Dim countmoth As Integer
        Dim countPageValue As Integer = 1
        If countAll < 32 Then
            AllPageVal = 1
        Else
            AllPageVal = countAll / 32
            countmoth = countAll Mod 32
            If countmoth > 0 Then
                AllPageVal += 1
            End If
        End If
        Dim Header As String = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงานการกำหนดกลุ่มงาน</td></tr></table>"
        Header &= "<table width='80%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
        Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;'>รหัสกลุ่มงาน</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อกลุ่มงาน</td></tr>"
        countPageValue += 1
        div_Panel.InnerHtml = Header
        Dim _color As String = ""
        Dim count As Integer = 1

        For i As Integer = 0 To gv_GroupUser.Rows.Count - 1
            Dim _groupcodeData As LinkButton = CType(gv_GroupUser.Rows.Item(i).FindControl("GroupCodedata"), LinkButton)
            Dim _groupname_th As LinkButton = CType(gv_GroupUser.Rows.Item(i).FindControl("GroupName_TH"), LinkButton)

            If _color = "" Then
                _color = "#FFE8EE"
            ElseIf _color = "#FFE8EE" Then
                _color = "#FFCEDB"
            Else
                _color = "#FFE8EE"
            End If

            If count = 32 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td style='padding-left:30px;'>" & _groupcodeData.Text & "</td><td style='padding-left:30px;'>" & _groupname_th.Text & "</td></tr>"
                Header = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr><tr style='size:30pt;'><td align='center'>รายงานการกำหนดผู้ใช้งานเข้ากับกลุ่มงาน</td></tr>"
                Header &= "<tr style='size:15pt;'><td align='right'>พิมพ์รายงานโดย : " + Session("UserName").ToString() + "</td></tr>"
                Header &= "<tr style='align:right;size:15pt;'><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
                Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr><td align='center' >รหัสกลุ่มงาน</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ชื่อกลุ่มงาน</td></tr>"
                CreateTag = CreateTag & "</table>" & "<div style='height:4.8cm;'></div>" & Header
                div_Panel.InnerHtml &= CreateTag
                count = 1
                countPageValue += 1
            End If
            CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td align='left' style='padding-left:30px;'>" & _groupcodeData.Text & "</td><td align='left' style='padding-left:30px;'>" & _groupname_th.Text & "</td></tr>"
            div_Panel.InnerHtml &= CreateTag
            count += 1
        Next
    End Sub

    Protected Sub gv_GroupUser_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_GroupUser.PageIndexChanging
        gv_GroupUser.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub Enable_False()
        Group_name_en.Enabled = False
        Group_Name_th.Enabled = True
    End Sub

    Protected Sub Enable_True()
        Group_name_en.Enabled = True
        Group_Name_th.Enabled = True
    End Sub

    Protected Sub btn_Add_Click(sender As Object, e As EventArgs) Handles btn_Add.Click
        Dim GetLastIndex As Integer = Convert.ToInt32(_lumb.GetMaxGroupCode())

        GroupID.Text = "U" & (GetLastIndex + 1).ToString("D4")
        GroupID.Enabled = False
        Group_Name_th.Enabled = True
        Group_name_en.Enabled = True
    End Sub
End Class