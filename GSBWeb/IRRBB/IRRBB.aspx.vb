Imports GSBWeb.DAL
Imports Arsoft.Utility
Imports System
Imports System.Web.Services
Imports System.Web.UI
Imports System.Web.Script.Services
Imports System.Web
Imports System.String
Imports System.Threading
Imports System.Globalization

Public Class IRRBB
    Inherits System.Web.UI.Page

    Dim _moduleid = "10800"
    Dim MessageBox_Result As Integer = -1
    Dim _command As Integer = -1
    Dim flag As Integer = 0
    Dim _userid = ""

    Public Shared SharedTextBox As TextBox


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load





        'Label1.Text = Session("ProcessCount").ToString()

        'ScriptManager.RegisterPostBackControl(Me.gvTable.FindControl("BthDownloadDiff"))

        _userid = Session("UserID")

        'ProcesTimer.Enabled = True

        'Dim ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "LoadUpdate", "startProcess()", True)



        If Not IsPostBack Then


            SetYear()
            SetMonth()
            SetMonthStatus()
            SetYearStatus()
            SetMonthNii()
            SetYearNii()
            SetReport()
            LoadParameter()
            LoadNii()
            SharedTextBox = TxtHeading
            ShowStatusAll()
            SetYearReport()

            Dim data As New IRRBBAccess
            Dim dtSearchUploadTemplate As New DataTable

            dtSearchUploadTemplate = data.SelectIRRBB_UploadTemplate_Onpageload()

            GVloadTemplate.DataSource = dtSearchUploadTemplate
            GVloadTemplate.DataBind()

        End If

        If flag = 0 Then

            If Session("Status") = "OK" Then

                MessageBoxAlert("Success", "บันทึกสำเร็จ", "", "ปิด", False, True)

                Dim dataAsof As Integer = Session("TIMEID")

                If dataAsof.ToString <> "" And dataAsof <> 0 Then

                    ddlMonthStatus.SelectedIndex = Convert.ToInt16(Mid(dataAsof.ToString, 5, 2))
                    ddlYearStatus.SelectedValue = Convert.ToString(Convert.ToInt16(Left(dataAsof.ToString, 4)))
                    ddlMonth.SelectedIndex = Convert.ToInt16(Mid(dataAsof.ToString, 5, 2))
                    ddlYear.SelectedValue = Convert.ToString(Convert.ToInt16(Left(dataAsof.ToString, 4)))

                End If

                Session("TIMEID") = 0
                ShowStatus()
                flag = 1
                Session("Status") = ""

            ElseIf Session("Status") = "PROCESSOK" Then
                MessageBoxAlert("Success", "ดำเนินการสำเร็จ", "", "ปิด", False, True)
                Session("TIMEID") = 0
                ShowStatus()
                flag = 1
                Session("Status") = ""
            ElseIf Session("Status") = "PROCESSERROR" Then
                MessageBoxAlert("Error", Session("StatusDesc"), "", "ปิด", False, True)
                Session("TIMEID") = 0
                ShowStatus()
                flag = 1
                Session("Status") = ""
                Session("StatusDesc") = ""
            End If

            If Request.Cookies.Count > 0 Then
                Dim cookiedata As String
                If Request.Cookies("finishdataRDM_Web") Is Nothing Then
                Else
                    cookiedata = ConvertBase64ToText(Request.Cookies("finishdataRDM_Web").Value)

                    If cookiedata.IndexOf("Complete") > -1 Then
                        MessageBoxAlert("Success", cookiedata, "", "ปิด", False, True)
                        ShowStatus()
                        flag = 1
                        Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    ElseIf cookiedata.IndexOf("กรอกข้อมูลในแถบต่อไปและคลิก Process เพื่อออกรายงาน") > -1 Then
                        MessageBoxAlert("Success", cookiedata, "", "ปิด", False, True)
                        ShowStatusAll()
                        flag = 1
                        Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    Else
                        MessageBoxAlert("Error", cookiedata, "", "ปิด", False, True)
                        flag = 1
                        Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    End If
                End If
            End If
        End If

    End Sub


    Protected Function ConvertBase64ToText(ByVal _base64str As String) As String
        Dim _results As String = ""
        Dim _bytes_ As Byte() = Convert.FromBase64String(_base64str)
        _results = Text.Encoding.UTF8.GetString(_bytes_)
        Return _results
    End Function

    Protected Function ConvertTextToBase64(ByVal _str As String) As String
        Dim _byt As Byte() = Encoding.UTF8.GetBytes(_str)
        Dim _base64 As String = Convert.ToBase64String(_byt)
        Return _base64
    End Function

    Private Sub SampleGV()

        Dim dt As DataTable = New DataTable
        dt.Columns.Add("ลำดับ")
        dt.Columns.Add("วันที่ Upload")
        dt.Columns.Add("รายงานประจำเดือน")
        dt.Columns.Add("ผู้ดำเนินการ")
        dt.Columns.Add("สถานะ")

        Dim dr As DataRow = dt.NewRow()
        dr("ลำดับ") = "1"
        dr("วันที่ Upload") = "01/10/2561"
        dr("รายงานประจำเดือน") = "กันยายน"
        dr("ผู้ดำเนินการ") = "วชิรา"
        dr("สถานะ") = "สำเร็จ"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ลำดับ") = "2"
        dr("วันที่ Upload") = "01/11/2561"
        dr("รายงานประจำเดือน") = "พฤศจิกายน"
        dr("ผู้ดำเนินการ") = "วชิรา"
        dr("สถานะ") = "สำเร็จ"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ลำดับ") = "3"
        dr("วันที่ Upload") = "01/12/2561"
        dr("รายงานประจำเดือน") = "ธันวาคม"
        dr("ผู้ดำเนินการ") = "วชิรา"
        dr("สถานะ") = "ไม่สำเร็จ"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ลำดับ") = "4"
        dr("วันที่ Upload") = "05/12/2562"
        dr("รายงานประจำเดือน") = "มกราคม"
        dr("ผู้ดำเนินการ") = "วชิรา"
        dr("สถานะ") = ""
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ลำดับ") = "5"
        dr("วันที่ Upload") = "01/12/2562"
        dr("รายงานประจำเดือน") = "กุมภาพันธ์"
        dr("ผู้ดำเนินการ") = "วชิรา"
        dr("สถานะ") = "สำเร็จ"
        dt.Rows.Add(dr)

        gvTable.DataSource = dt
        gvTable.DataBind()


    End Sub

    Private Sub LoadParameter()

        Dim IRRBBAccess As New IRRBBAccess
        Dim paramIRRBB As IRRBBEntity

        paramIRRBB = IRRBBAccess.GetIRRBBParam()

        If paramIRRBB.ReportUnit = 1000000 Then
            ddlCurrency.SelectedIndex = 0
        Else
            ddlCurrency.SelectedIndex = 1
        End If

        DdlMonthBack.SelectedIndex = paramIRRBB.GraphBackward - 1
        'TxtInput.Text = "xxxxx"
        TxtEffect.Text = paramIRRBB.NIIMonth.ToString
        TxtHeading.Text = paramIRRBB.ReportHeading


    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function ParaSave() As String

        Dim a As New IRRBBAccess


        Return "Complete"

    End Function




    Private Sub SetControl()
        'Dim ligl As HtmlGenericControl = Page.Master.FindControl("ligl")
        'ligl.Attributes.Add("class", "active")
    End Sub

    Private Sub SetReport()
        'Dim _userid = Session("UserID")
        'Dim _moduleacc As New ModuleAccess
        'Dim _lsmenu As List(Of ModuleEntity)
        'Dim _html As New StringBuilder
        'Dim li As New LiteralControl

        '_lsmenu = _moduleacc.GetUserModules(_userid, _moduleid, 1, 0)
        'For Each _menu As ModuleEntity In _lsmenu
        '    If _menu.LV > 0 Then
        '        '_html.Append(String.Format("<a href = ""{0}"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">{1}. {2}</a>", _menu.LinkPage, _menu.HeaderSeq, _menu.ModuleNameTH))
        '        _html.Append(String.Format("<a href = ""{0}"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;""><i class=""fa fa-caret-right""></i> {1}</a>", _menu.LinkPage, _menu.ModuleNameTH))
        '    End If
        'Next


        ''_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">1. Capital Fund (DS_CAP)</a>")
        ''_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">2. Operational Risk (DS_OPR)</a>")
        ''_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">3. Credit Risk Standardised Approach (DS_CRS)</a>")
        ''_html.Append("<a href = ""#"" class = ""list-group-item"" style=""text-align: left; padding-right: 10;"">4. Contingent Summary (DS_COS)</a>")

        'li.Text = _html.ToString()
        'pnlReport.Controls.Add(li)

    End Sub


    Private Sub SetYear()
        Dim curYear As Integer = Date.Today.Year
        Dim curThaiYear As Integer
        For indexYear As Integer = 1 To 10
            curThaiYear = curYear + 543
            ddlYear.Items.Add(New ListItem(curThaiYear.ToString, curYear.ToString))
            DdlYearStart.Items.Add(New ListItem(curThaiYear.ToString, curYear.ToString))
            DdlYearEnd.Items.Add(New ListItem(curThaiYear.ToString, curYear.ToString))
            curYear = curYear - 1
        Next
        ddlYear.Items.Insert(0, New ListItem("--เลือกปี--", ""))
        ddlYear.SelectedIndex = 1
        DdlYearStart.Items.Insert(0, New ListItem("--เลือกปี--", ""))
        DdlYearStart.SelectedIndex = 1
        DdlYearEnd.Items.Insert(0, New ListItem("--เลือกปี--", ""))
        DdlYearEnd.SelectedIndex = 1
    End Sub

    Private Sub SetYearStatus()
        Dim curYear As Integer = Date.Today.Year
        Dim curThaiYear As Integer
        For indexYear As Integer = 1 To 10
            curThaiYear = curYear + 543
            ddlYearStatus.Items.Add(New ListItem(curThaiYear.ToString, curYear.ToString))
            curYear = curYear - 1
        Next
        ddlYearStatus.Items.Insert(0, New ListItem("--เลือกปี--", ""))
        ddlYearStatus.SelectedIndex = 1
    End Sub

    Private Sub SetYearNii()
        Dim curYear As Integer = Date.Today.Year
        Dim curThaiYear As Integer
        For indexYear As Integer = 1 To 10
            curThaiYear = curYear + 543
            ddlYearnii.Items.Add(New ListItem(curThaiYear.ToString, curYear.ToString))
            curYear = curYear - 1
        Next
        ddlYearnii.Items.Insert(0, New ListItem("--เลือกปี--", ""))
        ddlYearnii.SelectedIndex = 1
    End Sub

    Private Sub SetYearReport()
        Dim curYear As Integer = Date.Today.Year
        Dim curThaiYear As Integer
        For indexYear As Integer = 1 To 10
            curThaiYear = curYear + 543
            DdlYearReport.Items.Add(New ListItem(curThaiYear.ToString, curYear.ToString))
            curYear = curYear - 1
        Next
        DdlYearReport.SelectedIndex = 0
    End Sub

    Private Sub SetMonth()
        Dim curMonth As Integer = Date.Today.Month
        ddlMonth.SelectedIndex = curMonth


    End Sub

    Private Sub SetMonthStatus()
        Dim curMonth As Integer = Date.Today.Month
        ddlMonthStatus.SelectedIndex = curMonth


    End Sub

    Private Sub SetMonthNii()
        Dim curMonth As Integer = Date.Today.Month
        ddlmonthnii.SelectedIndex = curMonth


    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearData()
    End Sub

    Private Sub ClearData()
        fuATM1.Dispose()
        fuATM2.Dispose()
        fuATM1Adjusted.Dispose()
        fuATM1Adjusted.Dispose()
        txtATM1.Text = ""
        txtATM2.Text = ""
        txtATM1Adjusted.Text = ""
        txtATM2Adjusted.Text = ""
        ddlYear.SelectedIndex = 1
        SetMonth()
    End Sub


    Public Function AnyDup(NumList As Array) As Boolean
        Dim a As Long, b As Long
        'Start the first loop

        If NumList(0) = NumList(1) Then
            AnyDup = True : Exit Function
        End If


        If NumList(2) = NumList(3) Then
            AnyDup = True : Exit Function
        End If

    End Function


    Protected Sub BthCancelInput_Click(sender As Object, e As EventArgs) Handles BthCancelInput.Click

        Response.Redirect("~/IRRBB/IRRBB.aspx")

    End Sub




    Protected Sub BthCancelNii_Click(sender As Object, e As EventArgs) Handles BthCancelNii.Click

        Response.Redirect("~/IRRBB/IRRBB.aspx")

    End Sub


    Protected Sub BthCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        Response.Redirect("~/IRRBB/IRRBB.aspx")

    End Sub




    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        'If String.IsNullOrEmpty(ddlMonth.SelectedValue) Then
        '    MessageBoxAlert("แจ้งเตือน", "กรุณาเพิ่มข้อมูลรายละเอียด IndustyLimit", "", "ปิด", False, True)
        '    Return
        'ElseIf String.IsNullOrEmpty(ddlYear.SelectedValue) Then

        'Else

        If ddlMonth.SelectedIndex <= 0 Then
            MessageBoxAlert("แจ้งเตือน", "กรุณาเลือก เดือน ปี ที่ต้องการ", "", "ปิด", False, True)
            ClearData()
            Return
        ElseIf ddlYear.SelectedIndex <= 0 Then
            MessageBoxAlert("แจ้งเตือน", "กรุณาเลือก เดือน ปี ที่ต้องการ", "", "ปิด", False, True)
            ClearData()
            Return
        ElseIf String.IsNullOrEmpty(fuATM1Adjusted.FileName) Or String.IsNullOrEmpty(fuATM2Adjusted.FileName) Then
            MessageBoxAlert("แจ้งเตือน", "กรุณาเลือกไฟล์รายงานที่มีการปรับยอดแล้ว", "", "ปิด", False, True)
            ClearData()
            Return
        End If

        Dim arrayfile() As String = {fuATM1.FileName, fuATM2.FileName, fuATM1Adjusted.FileName, fuATM2Adjusted.FileName}
        Dim checkDup As Boolean = AnyDup(arrayfile)

        If checkDup = True Then
            MessageBoxAlert("แจ้งเตือน", "ชื่อไฟล์ซ้ำ โปรดแก้ไข", "", "ปิด", False, True)
            Return
        End If

        Dim hfc As HttpFileCollection = Request.Files

        Dim month As Integer
        Dim year As Integer
        Dim nii As New IRRBBEntity

        month = ddlMonth.SelectedIndex
        year = Convert.ToInt16(ddlYear.SelectedValue)
        Dim intDataasof As Integer
        Dim strDataasof As String
        strDataasof = Format(year, "0000") + month.ToString("D2") + Format(Date.DaysInMonth(year, month), "00")
        intDataasof = Convert.ToInt32(strDataasof)

        Session("TIMEID") = intDataasof
        Session("hfc") = hfc

        'For i = 0 To hfc.Count - 1
        '    Dim hpf As HttpPostedFile = hfc(i)
        '    FileName = System.IO.Path.GetFileName(hpf.FileName)
        '    If hpf.ContentLength > 0 Then

        '        Dim Ext As String = System.IO.Path.GetExtension(hpf.FileName)
        '        hpf.SaveAs(Server.MapPath("~/IRRBB/Upload_IRRBB/" + FileName))
        '    End If

        'Next


        'ClearData()

        Response.Cookies("SetCommandData_GSBWebsite").Value = 1
        Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            MessageBoxAlert("Question", "ยืนยัน Upload File รายงานนี้ใช่หรือไม่", "ใช่", "ไม่ใช่", True, True)
        End If

    End Sub

    Protected Sub BthUploadInput_Click(sender As Object, e As EventArgs) Handles BthUploadInput.Click
        'If String.IsNullOrEmpty(ddlMonth.SelectedValue) Then
        '    MessageBoxAlert("แจ้งเตือน", "กรุณาเพิ่มข้อมูลรายละเอียด IndustyLimit", "", "ปิด", False, True)
        '    Return
        'ElseIf String.IsNullOrEmpty(ddlYear.SelectedValue) Then

        'Else

        Dim unit As Integer
        Dim graph As Integer
        Dim nii As Integer
        Dim heading As String
        Dim nameRef As String = "Test.pdf"

        If TxtEffect.Text = "" Or String.IsNullOrEmpty(TxtEffect.Text) Then

            MessageBoxAlert("แจ้งเตือน", "กรอกข้อมูลไม่ครบถ้วน", "", "ปิด", False, True)
            Return
        ElseIf SharedTextBox.Text = "" Or String.IsNullOrEmpty(SharedTextBox.Text) Then
            MessageBoxAlert("แจ้งเตือน", "กรอกข้อมูลไม่ครบถ้วน", "", "ปิด", False, True)
            Return
        End If




        If FUInput.FileName = nameRef Or String.IsNullOrEmpty(FUInput.FileName) Then

            Dim paramIRRBB As IRRBBEntity
            Dim IRRBBAccess As New IRRBBAccess

            paramIRRBB = IRRBBAccess.GetIRRBBParam()

            If FUInput.HasFile Then
                FUInput.PostedFile.SaveAs(paramIRRBB.PathReport + "/" + FUInput.FileName)
            End If

            Dim data As New IRRBBAccess

            If ddlCurrency.SelectedIndex = 0 Then
                unit = 1000000
            Else
                unit = 1
            End If

            graph = DdlMonthBack.SelectedIndex + 1
            nii = Val(TxtEffect.Text)
            heading = TxtHeading.Text


            data.UpdateIRRBBParam(unit, graph, nii, heading)


            MessageBoxAlert("Success", "บันทึกสำเร็จ", "", "ปิด", False, True)


        Else

            MessageBoxAlert("แจ้งเตือน", "ชื่อ File ไม่ถูกต้อง", "", "ปิด", False, True)

        End If



        'Dim hfc As HttpFileCollection = Request.Files

        'Session("hfc") = hfc

        'For i = 0 To hfc.Count - 1
        '    Dim hpf As HttpPostedFile = hfc(i)
        '    FileName = System.IO.Path.GetFileName(hpf.FileName)
        '    If hpf.ContentLength > 0 Then

        '        Dim Ext As String = System.IO.Path.GetExtension(hpf.FileName)
        '        hpf.SaveAs(Server.MapPath("~/IRRBB/Upload_IRRBB/" + FileName))
        '    End If

        'Next


        'ClearData()

        'Response.Cookies("SetCommandData_GSBWebsite").Value = 1
        'Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
        'If MessageBox_Result = -1 Then
        '    btn_OK.Attributes.Remove("data-dismiss")
        '    MessageBoxAlert("Question", "ยืนยัน Upload File รายงานนี้ใช่หรือไม่", "ใช่", "ไม่ใช่", True, True)
        'End If

    End Sub


    Protected Sub BthSaveNii_Click(sender As Object, e As EventArgs) Handles BthSaveNii.Click

        Dim monthT As Integer
        Dim yearT As Integer
        Dim monthTX As Integer
        Dim yearTX As Integer
        Dim intDataasofT As Integer
        Dim strDataasofT As String
        Dim intDataasofTX As Integer
        Dim strDataasofTX As String

        monthT = DdlMonthStart.SelectedIndex
        yearT = Convert.ToInt16(DdlYearStart.SelectedValue)
        monthTX = DdlMonthEnd.SelectedIndex
        yearTX = Convert.ToInt16(DdlYearEnd.SelectedValue)
        strDataasofT = Format(yearT, "0000") + monthT.ToString("D2") + Format(Date.DaysInMonth(yearT, monthT), "00")
        intDataasofT = Convert.ToInt32(strDataasofT)
        strDataasofTX = Format(yearTX, "0000") + monthTX.ToString("D2") + Format(Date.DaysInMonth(yearTX, monthTX), "00")
        intDataasofTX = Convert.ToInt32(strDataasofTX)

        If TxtAlertRisk.Text = "" Or String.IsNullOrEmpty(TxtAlertRisk.Text) Then
            MessageBoxAlert("แจ้งเตือน", "กรอกข้อมูลไม่ครบถ้วน", "", "ปิด", False, True)
            Return
        ElseIf TxtCeilingRisk.Text = "" Or String.IsNullOrEmpty(TxtCeilingRisk.Text) Then
            MessageBoxAlert("แจ้งเตือน", "กรอกข้อมูลไม่ครบถ้วน", "", "ปิด", False, True)
            Return
        ElseIf RbnCFO.Checked Then

            If TxtNumCFO.Text = "" Or String.IsNullOrEmpty(TxtNumCFO.Text) Then
                MessageBoxAlert("แจ้งเตือน", "กรอกข้อมูลไม่ครบถ้วน", "", "ปิด", False, True)
                Return
            End If
        ElseIf RbnOther.Checked Then
            If TxtNumOtherSource.Text = "" Or String.IsNullOrEmpty(TxtNumOtherSource.Text) Then
                MessageBoxAlert("แจ้งเตือน", "กรอกข้อมูลไม่ครบถ้วน", "", "ปิด", False, True)
                Return
            ElseIf TxtOtherSource.Text = "" Or String.IsNullOrEmpty(TxtOtherSource.Text) Then
                MessageBoxAlert("แจ้งเตือน", "กรอกข้อมูลไม่ครบถ้วน", "", "ปิด", False, True)
                Return
            End If
        End If

        If intDataasofTX > intDataasofT Then
            MessageBoxAlert("แจ้งเตือน", "(T-x) เดือนเปรียบเที่ยบมีค่ามากกว่าเดือนตั้งตั้น<br/>กรุณาเลือกเดือนให้ถูกต้อง", "", "ปิด", False, True)
            Return
        End If

        SaveNii()

        MessageBoxAlert("Success", "บันทึกสำเร็จ", "", "ปิด", False, True)



    End Sub

    Private Sub SaveNii()

        Dim month As Integer
        Dim year As Integer
        Dim monthT As Integer
        Dim yearT As Integer
        Dim monthTX As Integer
        Dim yearTX As Integer
        Dim intDataasof As Integer
        Dim strDataasof As String
        Dim intDataasofT As Integer
        Dim strDataasofT As String
        Dim intDataasofTX As Integer
        Dim strDataasofTX As String
        Dim alertrisk As Double
        Dim ceiingrisk As Double
        Dim typenii As String = ""
        Dim numnii As Double


        month = ddlmonthnii.SelectedIndex
        year = Convert.ToInt16(ddlYearnii.SelectedValue)

        monthT = DdlMonthStart.SelectedIndex
        yearT = Convert.ToInt16(DdlYearStart.SelectedValue)

        monthTX = DdlMonthEnd.SelectedIndex
        yearTX = Convert.ToInt16(DdlYearEnd.SelectedValue)

        alertrisk = Convert.ToDouble(TxtAlertRisk.Text)
        ceiingrisk = Convert.ToDouble(TxtCeilingRisk.Text)

        If RbnCFO.Checked Then
            typenii = "CFO"
            numnii = Convert.ToDouble(TxtNumCFO.Text)
        ElseIf RbnOther.Checked Then
            typenii = TxtOtherSource.Text
            numnii = Convert.ToDouble(TxtNumOtherSource.Text)
        End If

        strDataasof = Format(year, "0000") + month.ToString("D2") + Format(Date.DaysInMonth(year, month), "00")
        intDataasof = Convert.ToInt32(strDataasof)
        strDataasofT = Format(yearT, "0000") + monthT.ToString("D2") + Format(Date.DaysInMonth(yearT, monthT), "00")
        intDataasofT = Convert.ToInt32(strDataasofT)
        strDataasofTX = Format(yearTX, "0000") + monthTX.ToString("D2") + Format(Date.DaysInMonth(yearTX, monthTX), "00")
        intDataasofTX = Convert.ToInt32(strDataasofTX)


        Dim data As New IRRBBAccess
        data.DeleteNii(intDataasof)
        data.InsertNii(intDataasof, intDataasofT, intDataasofTX, alertrisk, ceiingrisk, numnii, typenii)

        Session("TIMEID") = intDataasof

    End Sub

    Private Sub LoadNii()

        Dim month As Integer
        Dim year As Integer
        Dim nii As New IRRBBEntity

        month = ddlmonthnii.SelectedIndex
        year = Convert.ToInt16(ddlYearnii.SelectedValue)
        Dim intDataasof As Integer
        Dim strDataasof As String
        strDataasof = Format(year, "0000") + month.ToString("D2") + Format(Date.DaysInMonth(year, month), "00")
        intDataasof = Convert.ToInt32(strDataasof)

        TxtAlertRisk.Text = ""
        TxtCeilingRisk.Text = ""
        TxtOtherSource.Text = ""
        TxtNumCFO.Text = ""
        TxtNumOtherSource.Text = ""
        RbnCFO.Checked = True
        RbnOther.Checked = False

        Dim data As New IRRBBAccess
        nii = data.SelectNii(intDataasof)

        TxtAlertRisk.Text = nii.AlertRiskPercent.ToString
        TxtCeilingRisk.Text = nii.CeilingRiskPercent.ToString

        Dim monthT As Integer = 0
        Dim yearT As Integer = 0
        Dim monthTX As Integer = 0
        Dim yearTX As Integer = 0

        If nii.DataAsOf_T > 0 Then
            monthT = Convert.ToInt16(nii.DataAsOf_T.ToString.Substring(4, 2))
            yearT = Convert.ToInt16(Left(nii.DataAsOf_T.ToString, 4))
        End If

        If nii.DataAsOf_TX > 0 Then
            monthTX = Convert.ToInt16(nii.DataAsOf_TX.ToString.Substring(4, 2))
            yearTX = Convert.ToInt16(Left(nii.DataAsOf_TX.ToString, 4))
        End If


        If monthT > 0 Then
            DdlMonthStart.SelectedValue = monthT.ToString
        Else
            DdlMonthStart.SelectedValue = month.ToString
        End If

        If yearT > 0 Then
            DdlYearStart.SelectedValue = yearT.ToString
        Else
            DdlYearStart.SelectedValue = year.ToString
        End If

        If monthTX > 0 Then
            DdlMonthEnd.SelectedValue = monthTX.ToString
        Else
            DdlMonthEnd.SelectedValue = month.ToString
        End If

        If yearTX > 0 Then
            DdlYearEnd.SelectedValue = yearTX.ToString
        Else
            DdlYearEnd.SelectedValue = year.ToString
        End If






        If nii.NiiSource = "CFO" Then
            RbnCFO.Checked = True
            RbnOther.Checked = False
            TxtNumCFO.Text = nii.NiiValue
        Else
            If Not String.IsNullOrEmpty(nii.NiiSource) Then
                RbnOther.Checked = True
                RbnCFO.Checked = False
                TxtOtherSource.Text = nii.NiiSource
                TxtNumOtherSource.Text = nii.NiiValue
            End If
        End If




    End Sub


    Private Sub Uploadfile()


        If MessageBox_Result > 0 Then

            'Try


            Dim IRRBBAccess As New IRRBBAccess
                Dim paramIRRBB As IRRBBEntity

                paramIRRBB = IRRBBAccess.GetIRRBBParam()

                Dim dataAsof As Integer = Session("TIMEID")

                Dim FileName As String = ""
                Dim i As Integer
                Dim hfc As HttpFileCollection = Session("hfc")
            Dim path(hfc.Count) As String



            For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    FileName = System.IO.Path.GetFileName(hpf.FileName)
                    If hpf.ContentLength > 0 Then
                        Dim Ext As String = System.IO.Path.GetExtension(hpf.FileName)
                    If i < 2 Then
                        path(i) = paramIRRBB.PathInput + "\" + FileName
                        hpf.SaveAs(path(i))
                    Else
                        path(i) = paramIRRBB.PathInputAdjust + "\" + FileName
                        hpf.SaveAs(path(i))
                        End If

                        If i = 0 Then Session("IRRBBUpload1") = path(i)
                        If i = 1 Then Session("IRRBBUpload2") = path(i)
                        If i = 2 Then Session("IRRBBUpload3") = path(i)
                        If i = 3 Then Session("IRRBBUpload4") = path(i)

                    End If

                Next


            MessageBoxAlert("Success", "กรอกข้อมูลในแถบต่อไปและคลิก Process เพื่อออกรายงาน", "", "ปิด", False, True)
            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("กรอกข้อมูลในแถบต่อไปและคลิก Process เพื่อออกรายงาน")
            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)

            'Catch ex As Exception
            '    UtilLogfile.writeToLog("IRRBB", "Uploadfile()", ex.Message)
            'End Try

        End If


    End Sub

    Private Sub ProcessBatch(ByVal dataAsof As String, ByVal dataAsofT As String, ByVal dataAsofTX As String, ByVal path() As String)

        Dim MyBatchFile As String = "D:\IRRBB\IRRBB_PROCESS.bat"

        Dim StreamReader1 As New IO.StreamReader(MyBatchFile)
        Dim bat As String = StreamReader1.ReadToEnd
        StreamReader1.Close()

        Dim myDelims As String() = New String() {"""/"}

        Dim spritForParam() As String = bat.Split(myDelims, StringSplitOptions.None)

        Dim cSprit As Integer = spritForParam.Length

        For i = 0 To cSprit - 1

            If Left(spritForParam(i), 6) = "param:" Then

                Dim param As String = Right(spritForParam(i), Len(spritForParam(i)) - 6)

                param = RTrim(param)
                If Left(param, 13) = "DATA_ASOF_T-X" Then
                    bat = bat.Replace(param, "DATA_ASOF_T-X=" + dataAsofTX.ToString() + """")
                ElseIf Left(param, 11) = "DATA_ASOF_T" Then
                    bat = bat.Replace(param, "DATA_ASOF_T=" + dataAsofT.ToString() + """")
                ElseIf Left(param, 9) = "DATA_ASOF" Then
                    bat = bat.Replace(param, "DATA_ASOF=" + dataAsof.ToString() + """")
                ElseIf Left(param, 15) = "FILE_7_1_ADJUST" Then
                    If path(2) <> "" Then
                        path(2) = path(2).Replace("/", "\")
                    End If
                    bat = bat.Replace(param, "FILE_7_1_ADJUST=" + path(2) + """")
                ElseIf Left(param, 15) = "FILE_7_2_ADJUST" Then
                    If path(3) <> "" Then
                        path(3) = path(3).Replace("/", "\")
                    End If
                    bat = bat.Replace(param, "FILE_7_2_ADJUST=" + path(3) + """")
                ElseIf Left(param, 8) = "FILE_7_1" Then
                    If path(0) <> "" Then
                        path(0) = path(0).Replace("/", "\")
                    End If
                    bat = bat.Replace(param, "FILE_7_1=" + path(0) + """")
                ElseIf Left(param, 8) = "FILE_7_2" Then
                    If path(1) <> "" Then
                        path(1) = path(1).Replace("/", "\")
                    End If
                    bat = bat.Replace(param, "FILE_7_2=" + path(1) + """")
                ElseIf Left(param, 11) = "PATH_REPORT" Then
                    bat = bat.Replace(param, "PATH_REPORT=D:\IRRBB\REPORT""")
                End If


            End If

        Next

        IO.File.WriteAllText(MyBatchFile, bat)


        Dim process As New Process

        process = Process.Start(MyBatchFile)

        Dim data As New IRRBBAccess

        data.Update_Status_Process("เริ่มการประมวลผล...", 0, Convert.ToInt32(dataAsof))



        Response.Redirect("~/IRRBB/WaitingPage.aspx")

        process.WaitForExit()


    End Sub




    Protected Sub AlertReport1(ByVal reportType As String)
        MessageReport.Text = reportType
        BtnExport.Attributes.Remove("data-dismiss")
        BtnReportCancel.Attributes.Remove("data-dismiss")
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Report", "$('#AlertReport').modal();", True)
    End Sub

    Private Sub ShowStatusAll()


        Dim data As New IRRBBAccess
        Dim tblStatus As New DataTable

        Dim month As Integer
        Dim year As Integer
        Dim nii As New IRRBBEntity

        month = ddlMonthStatus.SelectedIndex
        year = Convert.ToInt16(ddlYearStatus.SelectedValue)
        Dim intDataasof As Integer
        Dim strDataasof As String
        strDataasof = Format(year, "0000") + month.ToString("D2") + Format(Date.DaysInMonth(year, month), "00")
        intDataasof = Convert.ToInt32(strDataasof)

        tblStatus = data.SelectIRRBBStatusAll()

        gvTable.DataSource = tblStatus
        gvTable.DataBind()

    End Sub


    Private Sub ShowStatus()

        Dim data As New IRRBBAccess
        Dim tblStatus As New DataTable

        Dim month As Integer
        Dim year As Integer
        Dim nii As New IRRBBEntity

        month = ddlMonthStatus.SelectedIndex
        year = Convert.ToInt16(ddlYearStatus.SelectedValue)
        Dim intDataasof As Integer
        Dim strDataasof As String
        strDataasof = Format(year, "0000") + month.ToString("D2") + Format(Date.DaysInMonth(year, month), "00")
        intDataasof = Convert.ToInt32(strDataasof)

        tblStatus = data.SelectIRRBBStatus(intDataasof)

        gvTable.DataSource = tblStatus
        gvTable.DataBind()

    End Sub


    Protected Sub MessageBoxAlert(ByVal title As String, ByVal _message As String, ByVal BtnOKString As String, ByVal BtnNOString As String, ByVal YesbtnStatus As Boolean, ByVal NobtnStatus As Boolean)
        lbl_Title.Text = title
        If title = "Error" Then
            Symbol_Image.ImageUrl = "~/Images/NotCorrect.png"
        ElseIf title = "Success" Then
            Symbol_Image.ImageUrl = "~/Images/Correct.png"
        ElseIf title = "Question" Then
            Symbol_Image.ImageUrl = "~/Images/Question.png"
        ElseIf title = "แจ้งเตือน" Then
            Symbol_Image.ImageUrl = "~/Images/Warning.png"
        End If

        Messages.Text = _message
        btn_OK.Visible = YesbtnStatus
        btn_NO.Visible = NobtnStatus
        btn_OK.Text = BtnOKString
        btn_NO.Text = BtnNOString
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertBox", "$('#AlertBox').modal();", True)
        UpdModal.Update()
    End Sub

    Protected Sub Async_Upload_File(sender As Object, EventName As EventArgs)
        Dim HasFile As Boolean = fuATM1.HasFile
    End Sub

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click

        If btn_OK.Visible = True Then
            MessageBox_Result = 1
            _command = Response.Cookies("SetCommandData_GSBWebsite").Value
            _command = Request.Cookies("SetCommandData_GSBWebsite").Value
            Session("Status") = "OK"
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(-1)
            If _command = 1 Then
                btn_OK.Attributes.Add("data-dismiss", "modal")
                Uploadfile()
            End If

            If _command = 2 Then

                Session("Status") = "PROCESSOK"

                btn_OK.Attributes.Add("data-dismiss", "modal")

                Dim intDataasof As Integer
                Dim intDataasofT As Integer
                Dim intDataasofTX As Integer
                intDataasof = Session("TIMEID")
                intDataasofT = Session("TIMEID_T")
                intDataasofTX = Session("TIMEID_TX")

                Dim path(4) As String
                path(0) = Session("IRRBBUpload1")
                path(1) = Session("IRRBBUpload2")
                path(2) = Session("IRRBBUpload3")
                path(3) = Session("IRRBBUpload4")




                ProcessBatch(intDataasof, intDataasofT, intDataasofTX, path)

                'Dim data As New IRRBBAccess

                'data.Update_Status(Session("UserID").ToString, intDataasof)



            End If

            'MessageBox_Result = -1
        End If
        'Response.Redirect("~/IRRBB/IRRBB.aspx")


    End Sub

    Protected Sub BthShowNii_Click(sender As Object, e As EventArgs) Handles BthShowNii.Click


        LoadNii()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "Parameter2Click();", True)


    End Sub

    Protected Sub BthProcess_Click(sender As Object, e As EventArgs) Handles BthProcess.Click

        Dim Month As Integer
        Dim Year As Integer
        Month = ddlmonthnii.SelectedIndex
        Year = Convert.ToInt16(ddlYearnii.SelectedValue)
        Dim intDataasof As Integer
        Dim strDataasof As String
        strDataasof = Format(Year, "0000") + Month.ToString("D2") + Format(Date.DaysInMonth(Year, Month), "00")
        intDataasof = Convert.ToInt32(strDataasof)

        Dim MonthT As Integer
        Dim YearT As Integer
        MonthT = DdlMonthStart.SelectedIndex
        YearT = Convert.ToInt16(DdlYearStart.SelectedValue)
        Dim intDataasofT As Integer
        Dim strDataasofT As String
        strDataasofT = Format(YearT, "0000") + MonthT.ToString("D2") + Format(Date.DaysInMonth(YearT, MonthT), "00")
        intDataasofT = Convert.ToInt32(strDataasofT)

        Dim MonthTX As Integer
        Dim YearTX As Integer
        MonthTX = DdlMonthEnd.SelectedIndex
        YearTX = Convert.ToInt16(DdlYearEnd.SelectedValue)
        Dim intDataasofTX As Integer
        Dim strDataasofTX As String
        strDataasofTX = Format(YearTX, "0000") + MonthTX.ToString("D2") + Format(Date.DaysInMonth(YearTX, MonthTX), "00")
        intDataasofTX = Convert.ToInt32(strDataasofTX)

        Session("TIMEID") = intDataasof
        Session("TIMEID_T") = intDataasofT
        Session("TIMEID_TX") = intDataasofTX

        Response.Cookies("SetCommandData_GSBWebsite").Value = 2
        Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            MessageBoxAlert("Question", "ยืนยัน Process รายงานนี้ใช่หรือไม่", "ใช่", "ไม่ใช่", True, True)
        End If


    End Sub



    'Protected Sub BtnReport1_Click(sender As Object, e As EventArgs) Handles BtnReport1.Click
    '    Session("IRRBBReport") = "Report1"
    '    AlertReport1(BtnReport1.Text)
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    'End Sub

    'Protected Sub BtnReport2_1_Click(sender As Object, e As EventArgs) Handles BtnReport2_1.Click
    '    Session("IRRBBReport") = "Report2_1"
    '    AlertReport1(BtnReport2_1.Text)
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    'End Sub

    'Protected Sub BtnReport2_2_Click(sender As Object, e As EventArgs) Handles BtnReport2_2.Click
    '    Session("IRRBBReport") = "Report2_2"
    '    AlertReport1(BtnReport2_2.Text)
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    'End Sub

    'Protected Sub BtnReport3_Click(sender As Object, e As EventArgs) Handles BtnReport3.Click
    '    Session("IRRBBReport") = "Report3"
    '    AlertReport1(BtnReport3.Text)
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    'End Sub

    Protected Sub BthSearchStatus_Click(sender As Object, e As EventArgs) Handles BthSearchStatus.Click

        ShowStatus()

    End Sub

    Protected Sub BtnReportCancel_Click(sender As Object, e As EventArgs) Handles BtnReportCancel.Click
        Response.Redirect("~/IRRBB/IRRBB.aspx")
    End Sub


    Protected Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click

        Dim month As Integer
        Dim year As Integer
        Dim rptType As String = Session("IRRBBReport")

        month = DdlMonthReport.SelectedIndex + 1
        year = Convert.ToInt16(DdlYearReport.SelectedValue)

        Dim intDataasof As Integer
        Dim strDataasof As String
        strDataasof = Format(year, "0000") + month.ToString("D2") + Format(Date.DaysInMonth(year, month), "00")
        intDataasof = Convert.ToInt32(strDataasof)

        Dim tblStatus As New DataTable
        Dim tblParam As New IRRBBEntity

        Dim data As New IRRBBAccess

        tblStatus = data.SelectIRRBBStatus(intDataasof)

        tblParam = data.GetIRRBBParam

        Dim path As String = ""

        If tblStatus.Rows.Count > 0 Then

            If rptType = "Report1" Then

                If Not String.IsNullOrEmpty(tblStatus.Rows(0).Item("DIFF_FILE").ToString) Then
                    path = tblStatus.Rows(0).Item("DIFF_FILE").ToString()
                    DownloadExcel(path)
                    Response.Redirect("~/IRRBB/IRRBB.aspx")
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert('ไม่มีข้อมูล')", True)
                End If

            ElseIf rptType = "Report2_1" Then

                If Not String.IsNullOrEmpty(tblStatus.Rows(0).Item("ALM1_COMPARE_FILE").ToString) Then
                    path = tblStatus.Rows(0).Item("ALM1_COMPARE_FILE").ToString()
                    DownloadExcel(path)
                    Response.Redirect("~/IRRBB/IRRBB.aspx")
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert('ไม่มีข้อมูล')", True)
                End If

            ElseIf rptType = "Report2_2" Then

                If Not String.IsNullOrEmpty(tblStatus.Rows(0).Item("ALM2_COMPARE_FILE").ToString) Then
                    path = tblStatus.Rows(0).Item("ALM2_COMPARE_FILE").ToString()
                    DownloadExcel(path)
                    Response.Redirect("~/IRRBB/IRRBB.aspx")
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert('ไม่มีข้อมูล')", True)
                End If

            ElseIf rptType = "Report3" Then

                If Not String.IsNullOrEmpty(tblStatus.Rows(0).Item("REPORT_FILE").ToString) Then
                    path = tblStatus.Rows(0).Item("REPORT_FILE").ToString()
                    DownloadExcel(path)
                    Response.Redirect("~/IRRBB/IRRBB.aspx")
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert('ไม่มีข้อมูล')", True)
                End If

            ElseIf rptType = "Report4" Then

                If Not String.IsNullOrEmpty(tblParam.PathReport) Then
                    path = tblParam.PathReport + "\IRRBB_T-X_REPORT_" + intDataasof.ToString + ".xlsx"
                    DownloadExcel(path)
                    Response.Redirect("~/IRRBB/IRRBB.aspx")
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert('ไม่มีข้อมูล')", True)
                End If

            End If

        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert('ไม่มีข้อมูล')", True)

        End If





    End Sub


    Protected Sub gvTable_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTable.RowCommand

        If e.CommandName = "Download" Then

            Dim intDataasof As Integer = Convert.ToInt32(e.CommandArgument)

            Dim tblStatus As New DataTable

            Dim data As New IRRBBAccess

            tblStatus = data.SelectIRRBBStatus(intDataasof)

            Dim path = tblStatus.Rows(0).Item("DIFF_FILE").ToString()

            DownloadExcel(path)

        End If

    End Sub


    Protected Sub gvTable_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTable.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblProcessDate As Label = CType(e.Row.FindControl("lblProcessDate"), Label)
            If Not lblProcessDate Is Nothing Then
                Dim processDate As DateTime = Convert.ToDateTime(lblProcessDate.Text)
                Dim _cultureTHInfo As New Globalization.CultureInfo("th-TH")
                lblProcessDate.Text = processDate.ToString("dd MMMM yyyy", _cultureTHInfo)
            End If

            Dim lblDataasof As Label = CType(e.Row.FindControl("lblDataAsof"), Label)
            If Not lblDataasof Is Nothing Then

                Dim month As Integer = Convert.ToInt16(Mid(lblDataasof.Text.ToString, 5, 2))
                Dim year As Integer = Convert.ToInt16(Left(lblDataasof.Text.ToString, 4))
                Dim strmonth As String = ""
                Dim strYear As String

                year = year + 543

                If month = 1 Then
                    strmonth = "มกราคม"
                ElseIf month = 2 Then
                    strmonth = "กุมภาพันธ์"
                ElseIf month = 3 Then
                    strmonth = "มีนาคม"
                ElseIf month = 4 Then
                    strmonth = "เมษายน"
                ElseIf month = 5 Then
                    strmonth = "พฤษภาคม"
                ElseIf month = 6 Then
                    strmonth = "มิถุนายน"
                ElseIf month = 7 Then
                    strmonth = "กรกฎาคม"
                ElseIf month = 8 Then
                    strmonth = "สิงหาคม"
                ElseIf month = 9 Then
                    strmonth = "กันยายน"
                ElseIf month = 10 Then
                    strmonth = "ตุลาคม"
                ElseIf month = 11 Then
                    strmonth = "พฤศจิกายน"
                ElseIf month = 12 Then
                    strmonth = "ธันวาคม"
                End If

                strYear = year.ToString

                lblDataasof.Text = strmonth + " " + strYear


            End If

        End If

    End Sub

    Private Sub DownloadExcel(ByVal filename As String)

        Dim TargetFile As New System.IO.FileInfo(filename)
        Dim fs As System.IO.FileStream
        fs = System.IO.File.Open(filename, System.IO.FileMode.Open)
        Dim btFile(fs.Length) As Byte
        fs.Read(btFile, 0, Convert.ToInt32(fs.Length))
        fs.Close()
        Response.AddHeader("Content-disposition", "attachment; filename=" + TargetFile.Name)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.ContentType = "application/octet-stream"
        Response.BinaryWrite(btFile)
        Response.End()

    End Sub

    Protected Sub gvTable_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvTable.RowCreated
        'ImageButton btnAdd = (ImageButton)e.Row.Cells[0].FindControl("imagebuttonID");
        'If (btnAdd!= null) Then
        '            {
        '    ScriptManager.GetCurrent(this).RegisterPostBackControl(btnAdd);   
        '}

        If e.Row.Cells.Count >= 6 Then

            Dim bthDiff As Button = CType(e.Row.FindControl("BthDownloadDiff"), Button)

            If Not bthDiff Is Nothing Then

                ScriptManager.GetCurrent(Me).RegisterPostBackControl(bthDiff)

            End If

        End If



    End Sub

    Protected Sub TxtHeading_TextChanged(sender As Object, e As EventArgs) Handles TxtHeading.TextChanged
        SharedTextBox.Text = TxtHeading.Text
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Session("IRRBBReport") = "Report3"
        AlertReport1(LinkButton1.Text)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    End Sub

    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        Session("IRRBBReport") = "Report2_1"
        AlertReport1(LinkButton3.Text)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Session("IRRBBReport") = "Report2_2"
        AlertReport1(LinkButton2.Text)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    End Sub

    Protected Sub LinkButton4Click(sender As Object, e As EventArgs) Handles LinkButton4.Click
        Session("IRRBBReport") = "Report4"
        AlertReport1(LinkButton4.Text)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "ReportClick();", True)
    End Sub

    Protected Sub ProcesTimer_Tick(sender As Object, e As EventArgs) Handles ProcesTimer.Tick

        Session("ProcessCount") = Session("ProcessCount") + 1



        If Session("ProcessCount") = 10 Then

            'LblProcess.Text = Session("ProcessCount")

            ProcesTimer.Enabled = False
        End If

    End Sub

    Protected Sub BtnUploadTemplate_Click(sender As Object, e As EventArgs) Handles BtnUploadTemplate.Click

        Dim saveDir As String = "D:\IRRBB\Report\Template"

        If FUInput.HasFile Then

            Dim savePath As String = saveDir + "\Template IRRBB(Analysis Report).xlsx"
            FUInput.SaveAs(savePath)

            Dim data As New IRRBBAccess

            data.Insert_UploadTemplate(Now, Session("UserID"), FUInput.FileName)

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "TemplateClick();", True)
            MessageBoxAlert("Success", "Upload สำเร็จ", "", "ปิด", False, True)

            TxtInput.Text = ""

            Dim dtSearchUploadTemplate As New DataTable

            dtSearchUploadTemplate = data.SelectIRRBB_UploadTemplate_Onpageload()

            GVloadTemplate.DataSource = dtSearchUploadTemplate
            GVloadTemplate.DataBind()

        Else

            MessageBoxAlert("แจ้งเตือน", "ไม่สามารถ Upload File ได้", "", "ปิด", False, True)

        End If

    End Sub

    Protected Sub BtnSearchTemplate_Click(sender As Object, e As EventArgs) Handles BtnSearchTemplate.Click

        If Not String.IsNullOrEmpty(txt_StartDate.Text.Trim) And Not String.IsNullOrEmpty(txt_EndDate.Text.Trim) Then

            Dim _ConDT As DateTime = DateTime.ParseExact(txt_StartDate.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH"))
            Dim formatStartDate As String = _ConDT.ToString("yyyy-MM-dd 00:00:00.000")
            _ConDT = DateTime.ParseExact(txt_EndDate.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH"))
            Dim formatEndDate As String = _ConDT.ToString("yyyy-MM-dd 23:59:59.000")

            Dim data As New IRRBBAccess
            Dim dtSearchUploadTemplate As New DataTable

            dtSearchUploadTemplate = data.SelectIRRBB_UploadTemplate(formatStartDate, formatEndDate)

            GVloadTemplate.DataSource = dtSearchUploadTemplate
            GVloadTemplate.DataBind()


        Else

            MessageBoxAlert("แจ้งเตือน", "กรุณากรอกข้อมูลการค้นหาให้ครบถ้วน", "", "ปิด", False, True)

        End If

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "TemplateClick();", True)

    End Sub

    Protected Sub LnkTemplate01Click(sender As Object, e As EventArgs) Handles LnkTemplate01.Click
        DownloadExcel("D:\IRRBB\REPORT\Template\Template IRRBB(Analysis Report)_Original.xlsx")
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "TemplateClick();", True)
    End Sub

    Protected Sub LnkTemplate02Click(sender As Object, e As EventArgs) Handles LnkTemplate02.Click
        DownloadExcel("D:\IRRBB\REPORT\Template\Template IRRBB(Analysis Report).xlsx")
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "TemplateClick();", True)
    End Sub

    Protected Sub LnkTemplate03Click(sender As Object, e As EventArgs) Handles LnkTemplate03.Click
        DownloadExcel("D:\IRRBB\INPUT_ADJUST\Template_Adjusted.zip")
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "TemplateClick();", True)
    End Sub

End Class