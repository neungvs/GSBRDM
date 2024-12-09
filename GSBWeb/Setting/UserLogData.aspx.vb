Imports GSBWeb.DAL
Imports GSBWeb.BLL
Imports System
Imports System.Globalization
Imports System.Configuration
Imports System.Web.HttpApplicationState



Public Class UserManagement
    Inherits System.Web.UI.Page
    Dim _usermodule As New UserModuleBiz
    Dim MessageBox_Result As Integer = -1
    Dim _command As Integer = -1
    Dim flag As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CreateDropDown("กำหนดกลุ่มงาน", 1, 0)
            CreateDropDown("กำหนดกลุ่มงานกับเมนูงาน", 2, 1)
            CreateDropDown("กำหนดผู้ใช้งานเข้ากับกลุ่มงาน", 3, 2)
            CreateDropDown("กำหนดเมนู", 4, 3)
            CreateDropDown("กำหนดผู้ใช้", 5, 4)
            Dim _data As New List(Of UserEntity)
            gv_Logdata.DataSource = _data
            gv_Logdata.DataBind()
            dd_Activation.Focus()
        End If
        If flag = 0 Then
            If Request.Cookies.Count > 0 Then
                If Request.Cookies("finishdataRDM_Web") Is Nothing Then
                Else
                    Dim cookiedata As String = ConvertBase64ToText(Request.Cookies("finishdataRDM_Web").Value)
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    If cookiedata.IndexOf("สำเร็จ") > -1 Or cookiedata.IndexOf("เสร็จสิ้น") > -1 Then
                        MessageBoxAlert("Success", cookiedata.ToString(), "", "ปิด", False, True)
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub LoadData()
        Dim _groupLogData As New List(Of UserEntity)
        Dim _ConDT As DateTime = DateTime.ParseExact(txt_StartDate.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH"))
        Dim formatStartDate As String = _ConDT.ToString("yyyy-MM-dd 00:00:00.000")
        _ConDT = DateTime.ParseExact(txt_EndDate.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH"))
        Dim formatEndDate As String = _ConDT.ToString("yyyy-MM-dd 23:59:59.000")
        _groupLogData = _usermodule.SelectGroupLogBetweenDateTime(dd_Activation.SelectedValue, formatStartDate, formatEndDate)
        lbl_Header_Ativity.Text = dd_Activation.SelectedItem.Text

        gv_Logdata.DataSource = _groupLogData
        gv_Logdata.DataBind()
    End Sub
    Protected Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            MessageBoxAlert("Question", "ต้องการยกเลิกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            ClearData()
            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ยกเลิกรายการเสร็จสิ้น")
            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
        End If
    End Sub

    Protected Sub btn_Submit_Click(sender As Object, e As EventArgs) Handles btn_Submit.Click
        If (txt_StartDate.Text <> Nothing And txt_EndDate.Text <> Nothing) And (txt_StartDate.Text <> "" And txt_EndDate.Text <> "") Then
            Dim _StartDateCheck As DateTime = DateTime.ParseExact(txt_StartDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture)
            Dim _EndDateCheck As DateTime = DateTime.ParseExact(txt_EndDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture)
            If _StartDateCheck.Date <= _EndDateCheck.Date Then
                LoadData()
                'If gv_Logdata.Rows.Count = 0 Then
                '    MessageBoxAlert("Information", "ไม่มีข้อมูล", "", "ปิด", False, True)
                'Else
                '    MessageBoxAlert("Information", "ค้นหาข้อมูลสำเร็จ", "", "ปิด", False, True)
                'End If
            Else
                MessageBoxAlert("Error", "กรอกข้อมูลไม่ถูกต้อง กรุณากรอกใหม่", "", "ปิด", False, True)
            End If
        Else
            MessageBoxAlert("Error", "กรอกข้อมูลไม่ครบ ไม่สามารถดำเนินรายการได้<br />กรุณาทำรายการใหม่", "", "ปิด", False, True)
        End If
    End Sub
    Protected Sub CreateDropDown(ByVal _data As String, ByVal _values As Integer, ByVal _items As Integer)
        dd_Activation.Items.Add(1)
        dd_Activation.Items(_items).Text = _data : dd_Activation.Items(_items).Value = _values
    End Sub
    Protected Sub ClearData()
        dd_Activation.SelectedIndex = 0
        txt_StartDate.Text = ""
        txt_EndDate.Text = ""
    End Sub

    Protected Sub btn_Print_Click(sender As Object, e As EventArgs) Handles btn_Print.Click
        If gv_Logdata.Rows.Count > 0 Then
            CreateArea()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "div_panel", "printing()", True)
        Else
            MessageBoxAlert("Error", "ไม่มีข้อมูล", "", "ปิด", False, True)
        End If
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
            btn_Cancel_Click(sender, e)
        End If
        Response.Redirect("~/setting/UserLogData.aspx")
    End Sub

    Protected Sub CreateArea()
        Dim CreateTag As String
        Dim countAll As Integer = gv_Logdata.Rows.Count
        Dim AllPageVal As Integer
        Dim countmoth As Integer
        Dim countPageValue As Integer = 1
        If countAll < 20 Then
            AllPageVal = 1
        Else
            AllPageVal = countAll / 20
            countmoth = countAll Mod 20
            If countmoth > 0 Then
                AllPageVal += 1
            End If
        End If
        Dim Header As String = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงาน Log การทำงาน</td></tr></table>"
        Header &= "<table align='center' width='80%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
        Header &= "<table align='center' width='80%' border='0'><tr><td>วันที่เริ่มค้นหา : " + txt_StartDate.Text + "   วันที่สิ้นสุดการค้นหา : " + txt_EndDate.Text + "</td></tr></table>"
        Header &= "<table align='center' width='80%' style='border: solid 1px #000;' style='background-color:#FF388C;color:#FFF;'><tr><td>" + dd_Activation.SelectedItem.Text + "</td></tr></table>"
        Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;'>ลำดับ</td><td align='center' style='background-color:#FF388C;color:#FFF;'>วันและเวลา</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ผู้ใช้งาน</td><td align='center' style='background-color:#FF388C;color:#FFF;'>กิจกรรม</td></tr>"
        countPageValue += 1
        div_Panel.InnerHtml = Header
        Dim _color As String = ""
        Dim count As Integer = 1

        For i As Integer = 0 To gv_Logdata.Rows.Count - 1
            Dim _NumberValue As LinkButton = CType(gv_Logdata.Rows(i).FindControl("lb_No"), LinkButton)
            Dim _DateTimeValue As LinkButton = CType(gv_Logdata.Rows(i).FindControl("lb_DateTime"), LinkButton)
            Dim _UserCode As LinkButton = CType(gv_Logdata.Rows(i).FindControl("lb_User_Code"), LinkButton)
            Dim _UserValue As LinkButton = CType(gv_Logdata.Rows(i).FindControl("lb_User_Name"), LinkButton)
            Dim _Activity As LinkButton = CType(gv_Logdata.Rows(i).FindControl("lb_Activity"), LinkButton)

            If _color = "" Then
                _color = "#FFE8EE"
            ElseIf _color = "#FFE8EE" Then
                _color = "#FFCEDB"
            Else
                _color = "#FFE8EE"
            End If

            If count = 20 And i <> gv_Logdata.Rows.Count - 1 Then
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td align='center'>" + _NumberValue.Text + "</td><td>" + _DateTimeValue.Text + "</td><td>" + _UserCode.Text + "-" + _UserValue.Text + "</td><td>" + _Activity.Text + "</td></tr>"
                Header = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='80%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>รายงาน Log การทำงาน</td></tr></table>"
                Header &= "<table align='center' width='80%' border='0'><tr><td>&nbsp;</tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table>"
                Header &= "<table align='center' width='80%' border='0'><tr><td>วันที่เริ่มค้นหา : " + txt_StartDate.Text + "   วันที่สิ้นสุดการค้นหา : " + txt_EndDate.Text + "</td></tr></table>"
                Header &= "<table align='center' width='80%' style='border: solid 1px #000;'><tr><td>" + dd_Activation.Text + "</td></tr></table>"
                Header &= "<table align='center' width='80%' style='border: solid 1px #000;' style='background-color:#FF388C;color:#FFF;'><tr ><td align='center' style='background-color:#FF388C;color:#FFF;'>ลำดับ</td><td align='center' style='background-color:#FF388C;color:#FFF;'>วันและเวลา</td><td align='center' style='background-color:#FF388C;color:#FFF;'>ผู้ใช้งาน</td><td align='center' style='background-color:#FF388C;color:#FFF;'>กิจกรรม</td></tr>"
                CreateTag = CreateTag & "</table>" & "<div style='height:4.8cm;'></div>" & Header
                div_Panel.InnerHtml &= CreateTag
                count = 1
                countPageValue += 1
            Else
                CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td align='center'>" + _NumberValue.Text + "</td><td>" + _DateTimeValue.Text + "</td><td>" + _UserCode.Text + "-" + _UserValue.Text + "</td><td>" + _Activity.Text + "</td></tr>"
                div_Panel.InnerHtml &= CreateTag
                count += 1
            End If

            
        Next
    End Sub

    Protected Sub gv_Logdata_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv_Logdata.PageIndexChanging
        gv_Logdata.PageIndex = e.NewPageIndex
        LoadData()
    End Sub
End Class