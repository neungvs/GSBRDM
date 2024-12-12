Imports System.IO
Imports Arsoft.Utility
Imports ClosedXML.Excel
Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports WinSCP

Public Class Import_MEV
    Inherits System.Web.UI.Page
    Private _timeBiz As New TimeBiz
    Private _factorNameBiz As New FactorNameBiz
    Private _importMevBiz As New ImportMevBiz
    Private _valBiz As New ValidateBiz
    Private _dateBiz As New DateHelperUtil
    Private _stressScenarioBiz As New StressScenarioBiz
    Private _macroEconomicFactorBiz As New MacroEconomicFactorBiz

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            tbImportMevHeader.Visible = False
            tbFactorHeader.Visible = False
            gvImportMev.Visible = False
            BindDropDownListTimeFactor()
            BindThaiMonths(ddlAddEditMonth)
            tbFactorDetail.Visible = False
            tbBtnAdd.Visible = False
        End If
    End Sub

#Region "Sub Program"
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
    Private Sub BindThaiMonths(ByVal ddl As DropDownList)
        ' Create a dictionary of Thai month names and their IDs
        Dim thaiMonths As New Dictionary(Of Integer, String) From {
        {1, "มกราคม"},
        {2, "กุมภาพันธ์"},
        {3, "มีนาคม"},
        {4, "เมษายน"},
        {5, "พฤษภาคม"},
        {6, "มิถุนายน"},
        {7, "กรกฎาคม"},
        {8, "สิงหาคม"},
        {9, "กันยายน"},
        {10, "ตุลาคม"},
        {11, "พฤศจิกายน"},
        {12, "ธันวาคม"}
    }

        ' Bind the dictionary to the DropDownList
        ddl.DataSource = thaiMonths
        ddl.DataTextField = "Value"  ' Display Thai month names
        ddl.DataValueField = "Key"  ' Use month IDs as values
        ddl.DataBind()

        ' Optionally add a default "Please select" item
        ddl.Items.Insert(0, New ListItem("--เลือกเดือน--", ""))
    End Sub

#End Region

#Region "Function"
    Private Function LoadFactor() As List(Of FactorEntity)
        Dim timeId As String = GetLastDayOfMonth()
        Return _factorNameBiz.GetFactor(timeId)
    End Function

    Private Function GetLastDayOfMonth() As String
        Dim _timeId As String = ddlTime.SelectedValue
        Dim _year As String = _timeId.Substring(0, 4)
        Dim _month As String = _timeId.Substring(4, 2)
        _timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        Return _timeId
    End Function
#End Region

#Region "Search"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDropDownList()
        tbImportMevHeader.Visible = True
        tbFactorHeader.Visible = True
        gvImportMev.Visible = False
        tbFactorDetail.Visible = False
        tbBtnAdd.Visible = False
    End Sub

    Protected Sub btnSearchByFactor_Click(sender As Object, e As EventArgs) Handles btnSearchByFactor.Click
        BindGridViewImportMev()
    End Sub
#End Region

#Region "Result"
    Protected Sub gvImportMev_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvImportMev.RowCommand
        If e.CommandName = "Edit" Then
            ViewState("mode") = "edit"
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvImportMev.Rows(rowIndex)
            ' Find the Label control and get its value
            Dim lbTimeId As Label = CType(row.FindControl("lbTimeId"), Label)
            Dim lbFactorId As Label = CType(row.FindControl("lbFactorId"), Label)
            Dim lbScenarioId As Label = CType(row.FindControl("lbScenarioId"), Label)
            Dim lbStressMonth As Label = CType(row.FindControl("lbStressMonth"), Label)
            Dim lbStressYear As Label = CType(row.FindControl("lbStressYear"), Label)
            Dim lbFactorValue As Label = CType(row.FindControl("lbFactorValue"), Label)

            ddlAddEditFactor.SelectedValue() = lbFactorId.Text
            ddlAddEditScenario.SelectedValue() = lbScenarioId.Text
            ddlAddEditMonth.SelectedValue() = lbStressMonth.Text

            ViewState("TimeId") = lbTimeId.Text
            ViewState("FactorId") = lbFactorId.Text
            ViewState("ScenarioId") = lbScenarioId.Text
            ViewState("Month") = lbStressMonth.Text
            ViewState("Year") = lbStressYear.Text

            lblModalTitle.Text = "แก้ไข (Edit)"

            ddlAddEditFactor.Enabled = False
            ddlAddEditMonth.Enabled = False
            ddlAddEditScenario.Enabled = False
            txtAddEditFactorValue.Enabled = True
            txtAddEditYear.Enabled = False

            txtAddEditYear.Text = lbStressYear.Text + 543
            txtAddEditFactorValue.Text = lbFactorValue.Text
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myAddEditModal", "$('#myAddEditModal').modal();", True)
            UpdModal.Update()
        End If
    End Sub

    Protected Sub gvImportMev_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvImportMev.RowEditing

    End Sub

    Protected Sub gvImportMev_DataBound(sender As Object, e As EventArgs) Handles gvImportMev.DataBound

    End Sub

    Private Sub BindGridViewImportMev(Optional TimeId As String = Nothing, Optional FactorId As String = Nothing)
        Dim _TimeId As String
        Dim _FactorId As String

        If TimeId <> Nothing And FactorId <> Nothing Then
            _TimeId = TimeId
            _FactorId = FactorId
        Else
            _TimeId = ddlTime.SelectedValue
            _FactorId = ddlFactor.SelectedValue
        End If

        Dim lstFactor As List(Of FactorEntity)
        lstFactor = LoadFactor()
        Dim factor As FactorEntity = lstFactor.SingleOrDefault(Function(c) c.FactorId = _FactorId)
        'Factor Detail
        lbHeaderFactorDesc.Text = factor.FactorDesc
        lbHeaderFactorName.Text = factor.FactorName
        lbHeaderFactorUnit.Text = factor.FactorUnit
        tbFactorDetail.Visible = True
        tbBtnAdd.Visible = True
        Dim listImportMev As List(Of ImportMevEntity) = _importMevBiz.GetByTimeAndFactor(_TimeId, _FactorId)
        gvImportMev.Visible = True
        gvImportMev.DataSource = listImportMev
        gvImportMev.DataBind()
    End Sub

    Protected Sub gvImportMev_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvImportMev.PageIndexChanging
        gvImportMev.PageIndex = e.NewPageIndex
        BindDropDownList()
        tbImportMevHeader.Visible = True
        tbFactorHeader.Visible = True
        gvImportMev.Visible = True
    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim result As Boolean
        Dim userId As Integer = Convert.ToInt16(Session("UserID"))
        Dim lbTimeId As Label = gvImportMev.Rows(e.RowIndex).Cells(1).FindControl("lbTimeId")
        Dim lbFactorId As Label = gvImportMev.Rows(e.RowIndex).Cells(1).FindControl("lbFactorId")
        Dim lbScenarioId As Label = gvImportMev.Rows(e.RowIndex).Cells(1).FindControl("lbScenarioId")
        Dim lbStressMonth As Label = gvImportMev.Rows(e.RowIndex).Cells(1).FindControl("lbStressMonth")
        Dim lbStressYear As Label = gvImportMev.Rows(e.RowIndex).Cells(1).FindControl("lbStressYear")
        Dim timeId As String = GetLastDayOfMonth()
        Dim scenarioId As String = lbScenarioId.Text
        Dim factorId As String = lbFactorId.Text
        Dim stressYear As String = lbStressYear.Text
        Dim stressMonth As String = lbStressMonth.Text

        result = _importMevBiz.Delete(timeId, scenarioId, stressYear, stressMonth, factorId)
        Try
            If result = True Then
                BindDropDownList()
                'BindGridViewImportMev()
                BindGridViewImportMev(timeId, factorId)
                ddlFactor.SelectedValue = factorId
                MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
            End If
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("ImportMEV.aspx", "OnRowDeleting()", ex.Message)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvImportMev.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
        End If
    End Sub
#End Region

#Region "Import Excel"
    Protected Sub btnCancelImport_Click(sender As Object, e As EventArgs) Handles btnCancelImport.Click
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: false});", True)
    End Sub
    Private Sub grvImportExcel_DataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvImportExcel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim TimeId As String = DataBinder.Eval(e.Row.DataItem, "TIMEID").ToString()
            Dim ScenarioId As String = DataBinder.Eval(e.Row.DataItem, "ScenarioID").ToString()
            Dim StressYear As String = DataBinder.Eval(e.Row.DataItem, "Stress_Year").ToString()
            Dim StressMonth As String = DataBinder.Eval(e.Row.DataItem, "Stress_Month").ToString()
            Dim FactorId As String = DataBinder.Eval(e.Row.DataItem, "FactorID").ToString()
            Dim FactorValue As String = DataBinder.Eval(e.Row.DataItem, "FactorValue").ToString()
            Dim ErrorDetail As String = DataBinder.Eval(e.Row.DataItem, "ErrorDetail").ToString()

            Dim lbTimeId As Label = CType(e.Row.FindControl("lbTimeId"), Label)
            Dim lbScenarioId As Label = CType(e.Row.FindControl("lbScenarioId"), Label)
            Dim lbStressYear As Label = CType(e.Row.FindControl("lbStressYear"), Label)
            Dim lbStressMonth As Label = CType(e.Row.FindControl("lbStressMonth"), Label)
            Dim lbFactorId As Label = CType(e.Row.FindControl("lbFactorId"), Label)
            Dim lbFactorValue As Label = CType(e.Row.FindControl("lbFactorValue"), Label)
            Dim lbErrorDetail As Label = CType(e.Row.FindControl("lbErrorDetail"), Label)

            lbTimeId.Text = TimeId
            lbScenarioId.Text = ScenarioId
            lbStressYear.Text = StressYear
            lbStressMonth.Text = StressMonth
            lbFactorId.Text = FactorId
            lbFactorValue.Text = FactorValue
            lbErrorDetail.Text = ErrorDetail

            e.Row.ForeColor = System.Drawing.Color.Red
        End If
    End Sub
    Protected Sub btnExcelImport_Click(sender As Object, e As EventArgs) Handles btnExcelImport.Click
        lblModalTitle.Text = "Import Excel"
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        UpdModal.Update()
    End Sub
    Protected Sub grvImportExcel_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvImportExcel.PageIndexChanging
        grvImportExcel.PageIndex = e.NewPageIndex
        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: false});", True)
        grvImportExcel.DataSource = ViewState("dtInvalidData")
        grvImportExcel.DataBind()
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        If fileUpload.HasFile Then
            Try
                Dim dt As New DataTable
                Using fileStream = fileUpload.PostedFile.InputStream
                    Dim workbook = New XLWorkbook(fileStream)
                    Dim worksheet = workbook.Worksheet(1)
                    Dim firstRow As Boolean = True
                    For Each row As IXLRow In worksheet.Rows()
                        'Use the first row to add columns to DataTable.
                        If firstRow Then
                            For Each cell As IXLCell In row.Cells()
                                dt.Columns.Add(cell.Value.ToString())
                            Next
                            firstRow = False
                        Else
                            'Add rows to DataTable.
                            dt.Rows.Add()
                            Dim i As Integer = 0
                            For Each cell As IXLCell In row.Cells()
                                dt.Rows(dt.Rows.Count - 1)(i) = cell.Value.ToString()
                                i += 1
                            Next
                        End If
                    Next
                    dt.Columns.Add("ErrorDetail", GetType(String))
                    ViewState("dtImportRawData") = dt
                End Using

                Dim dtInvalidData As DataTable = New DataTable()
                dtInvalidData = GetInvalidData(dt)

                If (dtInvalidData.Rows.Count = 0) Then
                    If SaveImport() Then
                        BindDropDownList()
                        BindGridViewImportMev()
                        tbImportMevHeader.Visible = True
                        tbFactorHeader.Visible = True
                        gvImportMev.Visible = True
                        grvImportExcel.DataSource = Nothing
                        grvImportExcel.DataBind()
                        ViewState("dtImportRawData") = Nothing
                        MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                    End If
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                    ViewState("dtInvalidData") = dtInvalidData
                    grvImportExcel.DataSource = dtInvalidData
                    grvImportExcel.DataBind()
                    Dim submitButton As Button = CType(sender, Button)
                    submitButton.Attributes.Remove("data-dismiss")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                    btnCancelImport.Visible = True
                End If
                btnCancelImport.Visible = True
            Catch ex As Exception
                lblMessage.Text = "Error: " & ex.Message
            End Try
        Else
            lblMessage.Text = "Please upload a file."
        End If
    End Sub

#End Region

#Region "Add/Edit"
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ViewState("mode") = "add"
        lblMessage.Visible = False
        lblModalTitle.Text = "เพิ่ม (Add)"
        txtAddEditFactorValue.Text = ""
        ddlAddEditMonth.ClearSelection()
        ddlAddEditFactor.SelectedValue = ddlFactor.SelectedValue
        txtAddEditYear.Text = ""

        txtAddEditFactorValue.Enabled = True
        txtAddEditYear.Enabled = True
        ddlAddEditFactor.Enabled = True
        ddlAddEditMonth.Enabled = True
        ddlAddEditScenario.Enabled = True

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myAddEditModal", "$('#myAddEditModal').modal();", True)
        UpdModal.Update()
    End Sub

    Function IsValidNumber(value As String) As Boolean
        Return IsNumeric(value)
    End Function

    Function IsValidBuddhistYear(year As String) As Boolean
        year = Convert.ToInt16(year)
        Return year >= 2500
    End Function

    Function IsValidDecimal(input As String, maxDecimals As Integer) As Boolean
        Dim number As Decimal

        ' Try to convert the input to a decimal
        If Decimal.TryParse(input, number) Then
            ' Check the number of decimal places
            Dim decimalPlaces As Integer = BitConverter.GetBytes(Decimal.GetBits(number)(3))(2)
            Return decimalPlaces <= maxDecimals
        Else
            ' Invalid number format
            Return False
        End If
    End Function

    Private Function Vaidate()
        Dim errMsgList As New List(Of String)
        Dim factorId As String = ddlAddEditFactor.SelectedValue
        Dim scenarioId As String = ddlAddEditScenario.SelectedValue
        Dim year As String = txtAddEditYear.Text
        Dim month As String = ddlAddEditMonth.SelectedValue
        Dim factorValue As String = txtAddEditFactorValue.Text
        Dim timeId As String = ddlTime.SelectedValue
        Dim maxDecimals As Integer = 15

        If ViewState("mode") = "add" Then
            If (year <> "" And month <> "" And scenarioId <> "") Then
                Dim listImportMev As List(Of ImportMevEntity) = _importMevBiz.GetByTimeAndFactor(timeId, factorId)
                For Each ds As ImportMevEntity In listImportMev
                    If (ds.StressYear = year - 543 And ds.StressMonth = month And ds.ScenarioId = scenarioId) Then
                        errMsgList.Add("ข้อมูลซ้ำ")
                    End If
                Next
            End If

            If (year = "") Then
                errMsgList.Add("กรุณาเลือกปีที่ทดสอบภาวะวิกฤต")
            ElseIf (IsValidNumber(year) = False) Then
                errMsgList.Add("ปีที่ทดสอบภาวะวิกฤตต้องเป็นตัวเลขเท่านั้น")
            ElseIf (IsValidBuddhistYear(year) = False) Then
                errMsgList.Add("ปีที่ทดสอบภาวะวิกฤต กรอกพ.ศ.เท่านั้น")
            End If

            If (month = "") Then
                errMsgList.Add("กรุณาเลือกเดือนที่ทดสอบภาวะวิกฤต")
            End If

            If (factorValue = "") Then
                errMsgList.Add("กรุณากรอกค่า Factor Value")
            ElseIf (IsValidNumber(factorValue) = False) Then
                errMsgList.Add("ค่า Factor Value ต้องเป็นตัวเลขเท่านั้น")
            ElseIf (IsValidDecimal(factorValue, maxDecimals) = False) Then
                errMsgList.Add("ค่า Factor Value ทศยนืยมไม่เดิน " & maxDecimals & " หลัก")
            End If

        ElseIf ViewState("mode") = "edit" Then
            If (factorValue = "") Then
                errMsgList.Add("กรุณากรอกค่า Factor Value")
            ElseIf (IsValidNumber(factorValue) = False) Then
                errMsgList.Add("ค่า Factor Value ต้องเป็นตัวเลขเท่านั้น")
            ElseIf (IsValidDecimal(factorValue, maxDecimals) = False) Then
                errMsgList.Add("ค่า Factor Value ทศยนืยมไม่เดิน " & maxDecimals & " หลัก")
            End If
        End If

        Return errMsgList
    End Function

    Protected Sub btnSaveAdd_Click(sender As Object, e As EventArgs) Handles btnSaveAdd.Click

        Dim errMsgList As List(Of String) = Vaidate()
        If errMsgList.Count > 0 Then
            lblMessageErrAdd.Visible = True
            lblMessageErrAdd.Text = String.Join(",", errMsgList.ToArray())
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myAddEditModal", "$('#myAddEditModal').modal({backdrop: true});", True)
            UpdModal.Update()
        Else
            'If ViewState("mode") = "add" Or ViewState("mode") = "edit" Then
            lblMessageErrAdd.Visible = False
            If SaveAdd() Then
                'BindDropDownList()
                BindGridViewImportMev(ViewState("timeId"), ViewState("factorId"))
                MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
            End If
            'ElseIf ViewState("mode") = "edit" Then
            '    Dim timeId As String = ViewState("TimeId")
            '    Dim factorId As String = ViewState("FactorId")
            '    Dim scenarioId As String = ViewState("ScenarioId")
            '    Dim month As String = ViewState("Month")
            '    Dim year As String = ViewState("Year")

            '    'Dim userId As Integer = Convert.ToInt16(Session("UserID"))
            '    'If _factorNameBiz.SaveUpdate(timeId, factorId, txtFactorName.Text, txtFactorDesc.Text, txtFactorUnit.Text, userId) Then
            '    '    LoadData()
            '    '    MessageBoxAlert("Success", "Add Data สำเร็จ", "", "ปิด", False, True)
            '    'Else
            '    '    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
            '    'End If
            'End If
        End If


    End Sub

#End Region

    Private Sub BindDropDownList()
        Dim lstFactor As List(Of FactorEntity) = LoadFactor()
        ddlFactor.DataSource = lstFactor
        ddlFactor.DataValueField = "FactorId"
        ddlFactor.DataTextField = "FactorName"
        ddlFactor.DataBind()

        ddlAddEditFactor.DataSource = lstFactor
        ddlAddEditFactor.DataValueField = "FactorId"
        ddlAddEditFactor.DataTextField = "FactorName"
        ddlAddEditFactor.DataBind()

        If (lstFactor.Count > 1) Then
            tbFactorDetail.Visible = True
            tbBtnAdd.Visible = True
        Else
            tbFactorDetail.Visible = False
            tbBtnAdd.Visible = False
        End If

        Dim lstScenario As List(Of ScenarioEntity)
        lstScenario = LoadScenario()
        ddlAddEditScenario.DataSource = lstScenario
        ddlAddEditScenario.DataValueField = "ScenarioId"
        ddlAddEditScenario.DataTextField = "ScenarioName"
        ddlAddEditScenario.DataBind()
    End Sub

    Private Function LoadScenario() As List(Of ScenarioEntity)
        Dim _timeId As String = ddlTime.SelectedValue
        Return _timeBiz.GetScenario(_timeId)
    End Function

    Private Sub BindDropDownListTimeFactor()
        Dim _listEntity As List(Of TimeEntity)
        _listEntity = _factorNameBiz.GetFactorDate()
        ddlTime.DataSource = _listEntity
        ddlTime.DataValueField = "TimeId"
        ddlTime.DataTextField = "TimeName"
        ddlTime.DataBind()
    End Sub

    Private Function LoadTimeStress() As List(Of TimeEntity)
        Dim _lsTime As List(Of TimeEntity)
        _lsTime = _stressScenarioBiz.GetDate()
        Return _lsTime
    End Function

    Private Function GetInvalidData(dt As DataTable) As DataTable
        Dim retDt As DataTable = New DataTable()
        For Each col In dt.Columns
            retDt.Columns.Add(col.ToString())
        Next

        Dim lstScenario As List(Of ScenarioEntity)
        lstScenario = LoadScenario()

        Dim lstFactor As List(Of FactorEntity)
        lstFactor = LoadFactor()

        Dim lstTime As List(Of TimeEntity)
        lstTime = LoadTimeStress()

        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim retError As List(Of String) = CheckValidData(row, lstScenario, lstFactor, lstTime)
                If retError.Count > 0 Then
                    row("ErrorDetail") = String.Join(",", retError.ToArray())
                    retDt.ImportRow(row)
                End If
            Next
        End If
        Return retDt
    End Function

    Private Function CheckValidData(row As DataRow, lstScenario As List(Of ScenarioEntity), lstFactor As List(Of FactorEntity), lstTime As List(Of TimeEntity)) As List(Of String)
        Dim TimeId As String = row("TIMEID").ToString()
        Dim ScenarioName As String = row("ScenarioID").ToString()
        Dim StressYear As String = row("Stress_Year").ToString()
        Dim StressMonth As String = row("Stress_Month").ToString()
        Dim FactorName As String = row("FactorID").ToString()
        Dim FactorValue As String = row("FactorValue").ToString()

        Dim retData As New List(Of String)
        Dim errMsgList As New List(Of String)

        errMsgList = (_valBiz.CheckValidByDataType("Time", TimeId, lstScenario, lstFactor, lstTime))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("StressYear", StressYear))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("StressMonth", StressMonth))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("Scenario", ScenarioName, lstScenario, lstFactor))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("FactorName", FactorName, lstScenario, lstFactor))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("FactorValue", FactorValue))
        retData.AddRange(errMsgList)

        Return retData
    End Function

    Private Function Binding() As List(Of ImportMevEntity)
        Dim dt As DataTable = New DataTable()
        Dim _listEntity As New List(Of ImportMevEntity)
        Dim dateUtil As New DateHelperUtil

        dt = ViewState("dtImportRawData")
        If dt.Rows.Count > 0 Then

            Dim lstScenario As List(Of ScenarioEntity)
            lstScenario = LoadScenario()

            Dim lstFactor As List(Of FactorEntity)
            lstFactor = LoadFactor()

            For Each row As DataRow In dt.Rows
                Dim entity As New ImportMevEntity
                Dim TimeId As String = row("TIMEID").ToString()
                Dim ScenarioName As String = row("ScenarioID").ToString()
                Dim StressYear As String = row("Stress_Year").ToString()
                Dim StressMonth As String = row("Stress_Month").ToString()
                Dim FactorName As String = row("FactorID").ToString()
                Dim FactorValue As String = row("FactorValue").ToString()

                entity.TimeId = dateUtil.GetLastDayOfMonth(TimeId)
                entity.ScenarioId = GetScenarioId(ScenarioName, lstScenario)
                entity.StressYear = StressYear - 543
                entity.StressMonth = dateUtil.GetMonthIdByMonthName(StressMonth)
                entity.FactorId = GetFactorId(FactorName, lstFactor)
                entity.FactorValue = FactorValue

                _listEntity.Add(entity)
            Next
        End If
        Return _listEntity
    End Function

    Private Function SaveImport() As Boolean
        Dim _listEntity As List(Of ImportMevEntity) = Binding()
        Dim _userId As Integer = Convert.ToInt16(Session("UserID"))
        Dim timeId As String = ddlTime.SelectedValue
        If _importMevBiz.DeleteByTimeId(timeId) Then
            Return _importMevBiz.SaveInsertExcel(_userId, _listEntity)
        End If
        Return False
    End Function

    Private Function GetScenarioId(Scenario As String, lstScenario As List(Of ScenarioEntity)) As String
        For Each _senario As ScenarioEntity In lstScenario
            If (Scenario = _senario.ScenarioName) Then
                Return _senario.ScenarioId
            End If
        Next
        Return ""
    End Function

    Private Function GetFactorId(factorName As String, lstFactor As List(Of FactorEntity)) As String
        For Each _factor As FactorEntity In lstFactor
            If (factorName = _factor.FactorName) Then
                Return _factor.FactorId
            End If
        Next
        Return ""
    End Function

    Private Function SaveAdd() As Boolean
        Dim timeId As String = GetLastDayOfMonth()
        Dim factorId As String = ddlAddEditFactor.SelectedValue
        Dim scenarioId As String = ddlAddEditScenario.SelectedValue
        Dim stressYear As String = txtAddEditYear.Text
        Dim stressMonth As String = ddlAddEditMonth.SelectedValue
        Dim factorValue As String = txtAddEditFactorValue.Text
        Dim userId As Integer = Convert.ToInt16(Session("UserID"))

        Dim entity As New ImportMevEntity
        entity.TimeId = timeId
        entity.ScenarioId = scenarioId
        entity.StressYear = stressYear - 543
        entity.StressMonth = stressMonth
        entity.FactorId = factorId
        entity.FactorValue = factorValue
        'Dim _listEntity As New List(Of ImportMevEntity)
        '_listEntity.Add(entity)

        ViewState("timeId") = timeId
        ViewState("factorId") = factorId

        Return _importMevBiz.Save(userId, entity)

    End Function

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Try
            Dim timeId As String = ddlTime.SelectedValue
            Dim listImportMev As List(Of ImportMevEntity) = _importMevBiz.GetTemplateByTime(timeId)

            ' Create a new Excel workbook
            Using workbook As New XLWorkbook()
                Dim worksheet = workbook.Worksheets.Add("ImportMev")

                ' Add headers to the worksheet
                worksheet.Cell(1, 1).Value = "TIMEID"
                worksheet.Cell(1, 2).Value = "ScenarioID"
                worksheet.Cell(1, 3).Value = "Stress_Year"
                worksheet.Cell(1, 4).Value = "Stress_Month"
                worksheet.Cell(1, 5).Value = "FactorID"
                worksheet.Cell(1, 6).Value = "FactorValue"

                Dim rowNum As Integer = 2
                For Each entity As ImportMevEntity In listImportMev
                    worksheet.Cell(rowNum, 1).Value = entity.TimeId.Trim()
                    worksheet.Cell(rowNum, 2).Value = entity.ScenarioName.Trim()
                    worksheet.Cell(rowNum, 3).Value = entity.StressYear.Trim()
                    worksheet.Cell(rowNum, 4).Value = entity.StressMonth.Trim()
                    worksheet.Cell(rowNum, 5).Value = entity.FactorName.Trim()
                    worksheet.Cell(rowNum, 6).Value = entity.FactorValue.Trim()
                    rowNum = rowNum + 1
                Next

                'Adjust column widths
                worksheet.Columns().AdjustToContents()

                ' Save the workbook to memory stream
                Using memoryStream As New MemoryStream()
                    workbook.SaveAs(memoryStream)
                    memoryStream.Seek(0, SeekOrigin.Begin)
                    ' Clear the response
                    Response.Clear()
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("Content-Disposition", "attachment; filename=ImportMev_Template.xlsx")
                    ' Write the file to the response
                    Response.BinaryWrite(memoryStream.ToArray())
                    Response.Flush()
                    ' End the response and send the file to the client
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End Using
            End Using
        Catch ex As Exception
            ' Handle any errors
            Response.Clear()
            UtilLogfile.writeToLog("ImportMEV.aspx", "btnDownloadTemplate_Click()", ex.Message)
            'Response.Write("An error occurred: " & ex.Message)
            Response.End()
        End Try
    End Sub

#Region "Upload"
    Protected Sub linkButtonUpload_Click(sender As Object, e As EventArgs) Handles linkButtonUpload.Click
        UploadMevForecast()
        UploadMevHisForecast()
    End Sub

    Public Sub UploadMevForecast()
        Dim _stressScenarioBiz As New StressScenarioBiz
        Dim timeId As String = ddlTime.SelectedValue
        Dim lstScenario As List(Of ScenarioEntity) = _stressScenarioBiz.GetScenarioByTimeId(timeId)
        For Each scenario As ScenarioEntity In lstScenario
            Dim scenarioName As String = scenario.ScenarioName
            Dim scenarioId As String = scenario.ScenarioId
            Dim dtBaseLine As New DataTable
            Dim fileNamePrefix As String = "TH_MEV_Forecast"
            Dim fileName As String = fileNamePrefix & "_" & scenarioName
            Dim dt As DataTable = _importMevBiz.GetForecastReport(timeId, scenarioId)
            If dt.Rows.Count > 0 Then
                ExportDataTableToExcelAndUploadFile(dt, fileName, scenarioName)
            End If
        Next
    End Sub

    Public Sub UploadMevHisForecast()
        Dim timeId As String = ddlTime.SelectedValue
        Dim fileNameHisPrefix As String = "TH_MEV_HIS_Forecast"
        Dim fileNameHis As String = fileNameHisPrefix
        Dim dtHis As DataTable = _macroEconomicFactorBiz.GetForecastReport(timeId)
        If dtHis.Rows.Count > 0 Then
            ExportDataTableToExcelAndUploadFile(dtHis, fileNameHis, "MacroEconomicFactor")
        End If
    End Sub

    Public Sub ExportDataTableToExcelAndUploadFile(dataTable As DataTable, fileName As String, Optional sheetName As String = "")
        'Define the folder path relative to the web app
        Dim folderTempExportExcelPath As String = HttpContext.Current.Server.MapPath("~/TempExportExcel/")
        Dim folderUploadedExcelPath As String = HttpContext.Current.Server.MapPath("~/UploadedExcel/")
        Dim fileNameExcel As String = fileName & ".xlsx"
        Dim tempFilePath As String = Path.Combine(folderTempExportExcelPath, fileNameExcel)
        ' Generate a new file name (e.g., append timestamp to the original file name)
        Dim newFileName As String = Path.GetFileNameWithoutExtension(fileNameExcel) & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & Path.GetExtension(fileNameExcel)
        Dim backupFilePath As String = Path.Combine(folderUploadedExcelPath, newFileName)

        'Ensure the folder exists
        If Not Directory.Exists(folderTempExportExcelPath) Then
            Directory.CreateDirectory(folderTempExportExcelPath)
        End If

        If Not Directory.Exists(folderUploadedExcelPath) Then
            Directory.CreateDirectory(folderUploadedExcelPath)
        End If

        Try
            'Create a new workbook and add a worksheet
            Using workbook As New XLWorkbook()
                Dim worksheet = workbook.Worksheets.Add(dataTable, sheetName)
                ' Save the workbook to the specified folder
                workbook.SaveAs(tempFilePath)
            End Using

            'Upload the file to an SFTP server using WinSCP
            Dim sessionOptions As New SessionOptions With {
                .Protocol = Protocol.Sftp,
                .HostName = "10.22.50.202",
                .UserName = "rdmftp",
                .SshPrivateKeyPath = HttpContext.Current.Server.MapPath("~/App_Data/Priv.ppk"),
                .SshHostKeyFingerprint = "ssh-rsa 1024 e4:dd:11:2e:82:34:ab:62:59:1c:c8:62:1d:4b:48:99"
            }

            Using session As New Session()
                session.Open(sessionOptions)

                ' Define the remote path on the SFTP server
                Dim remotePath As String = "/Stress_TFRS9/" & fileNameExcel

                ' Upload the file
                Dim transferOptions As New TransferOptions With {
                    .TransferMode = TransferMode.Binary
                }

                Dim transferResult As TransferOperationResult = session.PutFiles(tempFilePath, remotePath, False, transferOptions)

                ' Throw if there are any issues
                transferResult.Check()

                MessageBoxAlert("Success", "Upload ไฟล์เรียบร้อยแล้ว", "", "ปิด", False, True)
                ' Success message
                'Console.WriteLine("File uploaded successfully to " & remotePath)
            End Using
        Catch ex As Exception
            ' Log or handle errors
            UtilLogfile.writeToLog("ImportMEV.aspx", "ExportDataTableToExcelAndUploadFile()", ex.Message)
        Finally
            ' Clean up - delete the temporary file
            If File.Exists(tempFilePath) Then
                ' Check if the file exists in the temp folder
                If File.Exists(tempFilePath) Then
                    ' Move the file from temp folder to backup folder
                    File.Move(tempFilePath, backupFilePath)
                    'Console.WriteLine("File successfully moved to backup folder.")
                    File.Delete(tempFilePath)
                Else
                    'Console.WriteLine("File does not exist in the temporary folder.")
                End If
            End If
        End Try
    End Sub
#End Region

End Class