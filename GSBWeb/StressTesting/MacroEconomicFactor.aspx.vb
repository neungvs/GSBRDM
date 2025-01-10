Imports System.Globalization
Imports System.IO
Imports Arsoft.Utility
Imports ClosedXML.Excel
Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class MacroEconomicFactor
    Inherits System.Web.UI.Page
    Private _macroEconomicFactorBiz As New MacroEconomicFactorBiz
    Private _timeBiz As New TimeBiz
    Private _factorNameBiz As New FactorNameBiz
    Private _valBiz As New ValidateBiz
    Private _dateBiz As New DateHelperUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            tbImportMevHeader.Visible = False
            tbFactorHeader.Visible = False
            gvMacroEconomicFactor.Visible = False
            LoadTimeFactor()
            BindThaiMonths(ddlAddEditMonth)
            tbFactorDetail.Visible = False
            btnAdd.Visible = False
        End If
    End Sub

    Private Sub LoadData()
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
            btnAdd.Visible = True
        Else
            tbFactorDetail.Visible = False
            btnAdd.Visible = False
        End If
    End Sub

    Private Sub BindGridView()
        Dim timeId As String
        Dim factorId As String
        If (ViewState("timeId") <> Nothing) And (ViewState("factorId") <> Nothing) Then
            timeId = ViewState("timeId")
            factorId = ViewState("factorId")
        Else
            timeId = ddlTime.SelectedValue
            factorId = ddlFactor.SelectedValue
        End If

        Dim lstFactor As List(Of FactorEntity)
        lstFactor = LoadFactor()
        Dim factor As FactorEntity = lstFactor.SingleOrDefault(Function(c) c.FactorId = factorId)
        lbHeaderFactorDesc.Text = factor.FactorDesc
        lbHeaderFactorName.Text = factor.FactorName
        lbHeaderFactorUnit.Text = factor.FactorUnit
        tbFactorDetail.Visible = True
        btnAdd.Visible = True
        Dim listEntity As List(Of MacroEconomicFactorEntity) = _macroEconomicFactorBiz.GetByTimeAndFactor(timeId, factorId)
        gvMacroEconomicFactor.Visible = True
        gvMacroEconomicFactor.DataSource = listEntity
        gvMacroEconomicFactor.DataBind()
    End Sub

    Private Function LoadScenario() As List(Of ScenarioEntity)
        Dim _timeId As String = ddlTime.SelectedValue
        Return _timeBiz.GetScenario(_timeId)
    End Function

    Private Sub LoadTimeFactor()
        Dim _listEntity As List(Of TimeEntity)
        _listEntity = _factorNameBiz.GetFactorDate()
        ddlTime.DataSource = _listEntity
        ddlTime.DataValueField = "TimeId"
        ddlTime.DataTextField = "TimeName"
        ddlTime.DataBind()
    End Sub

    Private Sub LoadTimeStress()
        Dim _lsTime As List(Of TimeEntity)
        _lsTime = _timeBiz.GetDate()
        Session("LoadTimeStress") = _lsTime
        'ddlTime.DataSource = _lsTime
        'ddlTime.DataValueField = "TimeId"
        'ddlTime.DataTextField = "TimeName"
        'ddlTime.DataBind()
    End Sub

    Protected Sub btn_Excel_Import_Click(sender As Object, e As EventArgs) Handles btn_Excel_Import.Click
        lblModalTitle.Text = "Import Excel"
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        LoadTimeStress()
        UpdModal.Update()
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
                        LoadData()
                        tbImportMevHeader.Visible = True
                        tbFactorHeader.Visible = True
                        gvMacroEconomicFactor.Visible = True
                        grvImportExcel.DataSource = Nothing
                        grvImportExcel.DataBind()
                        ViewState("dtImportRawData") = Nothing
                        BindGridView()
                        MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                    End If
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                    'ViewState("dtImportLgdRawData") = dtInvalidData
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
        lstTime = Session("LoadTimeStress")

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

        errMsgList = (_valBiz.CheckValidByDataType("FactorName", FactorName, lstScenario, lstFactor))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("FactorValue", FactorValue))
        retData.AddRange(errMsgList)

        Return retData
    End Function

    Protected Sub grvImportExcel_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvImportExcel.PageIndexChanging
        grvImportExcel.PageIndex = e.NewPageIndex
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: false});", True)
        grvImportExcel.DataSource = ViewState("dtImportLgdRawData")
        grvImportExcel.DataBind()
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

    Private Sub grvImportExcel_DataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvImportExcel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim TimeId As String = DataBinder.Eval(e.Row.DataItem, "TimeId").ToString()
            Dim StressYear As String = DataBinder.Eval(e.Row.DataItem, "Stress_Year").ToString()
            Dim StressMonth As String = DataBinder.Eval(e.Row.DataItem, "Stress_Month").ToString()
            Dim FactorId As String = DataBinder.Eval(e.Row.DataItem, "FactorID").ToString()
            Dim FactorValue As String = DataBinder.Eval(e.Row.DataItem, "FactorValue").ToString()
            Dim ErrorDetail As String = DataBinder.Eval(e.Row.DataItem, "ErrorDetail").ToString()

            Dim lbTimeId As Label = CType(e.Row.FindControl("lbTimeId"), Label)
            Dim lbStressYear As Label = CType(e.Row.FindControl("lbStressYear"), Label)
            Dim lbStressMonth As Label = CType(e.Row.FindControl("lbStressMonth"), Label)
            Dim lbFactorId As Label = CType(e.Row.FindControl("lbFactorId"), Label)
            Dim lbFactorValue As Label = CType(e.Row.FindControl("lbFactorValue"), Label)
            Dim lbErrorDetail As Label = CType(e.Row.FindControl("lbErrorDetail"), Label)

            lbTimeId.Text = TimeId
            lbStressYear.Text = StressYear
            lbStressMonth.Text = StressMonth
            lbFactorId.Text = FactorId
            lbFactorValue.Text = FactorValue
            lbErrorDetail.Text = ErrorDetail

            e.Row.ForeColor = System.Drawing.Color.Red
        End If
    End Sub

    Private Function Binding() As List(Of MacroEconomicFactorEntity)
        Dim dt As DataTable = New DataTable()
        Dim _listEntity As New List(Of MacroEconomicFactorEntity)
        Dim dateUtil As New DateHelperUtil

        dt = ViewState("dtImportRawData")
        If dt.Rows.Count > 0 Then

            Dim lstScenario As List(Of ScenarioEntity)
            lstScenario = LoadScenario()

            Dim lstFactor As List(Of FactorEntity)
            lstFactor = LoadFactor()

            For Each row As DataRow In dt.Rows
                Dim entity As New MacroEconomicFactorEntity
                Dim TimeId As String = row("TIMEID").ToString()
                Dim StressYear As String = row("Stress_Year").ToString()
                Dim StressMonth As String = row("Stress_Month").ToString()
                Dim FactorName As String = row("FactorID").ToString()
                Dim FactorValue As String = row("FactorValue").ToString()

                entity.TimeId = dateUtil.GetLastDayOfMonth(TimeId)
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
        Dim _listEntity As List(Of MacroEconomicFactorEntity) = Binding()
        Dim _userId As Integer = Convert.ToInt16(Session("UserID"))
        Dim timeId As String = ddlTime.SelectedValue
        If _macroEconomicFactorBiz.DeleteByTimeId(timeId) Then
            Return _macroEconomicFactorBiz.SaveImportExcel(_userId, _listEntity)
        End If
        Return False
    End Function

    Protected Sub btn_Searchr_Click(sender As Object, e As EventArgs) Handles btn_Searchr.Click
        LoadData()
        tbImportMevHeader.Visible = True
        tbFactorHeader.Visible = True
        gvMacroEconomicFactor.Visible = False
        tbFactorDetail.Visible = False
        btnAdd.Visible = False
    End Sub

    Protected Sub btnCancelImport_Click(sender As Object, e As EventArgs) Handles btnCancelImport.Click
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
    End Sub

    Protected Sub gvMacroEconomicFactor_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvMacroEconomicFactor.PageIndexChanging
        gvMacroEconomicFactor.PageIndex = e.NewPageIndex
        'LoadData()
        BindGridView()
        tbImportMevHeader.Visible = True
        tbFactorHeader.Visible = True
        gvMacroEconomicFactor.Visible = True
    End Sub

    Private Function LoadFactor() As List(Of FactorEntity)
        Dim _timeId As String = GetLastDayOfMonth()
        'Dim _timeId As String = cb_List_Time.SelectedValue
        Return _factorNameBiz.GetFactor(_timeId)
    End Function

    Private Function GetLastDayOfMonth() As String
        Dim _timeId As String = ddlTime.SelectedValue
        Dim _year As String = _timeId.Substring(0, 4)
        Dim _month As String = _timeId.Substring(4, 2)
        _timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        Return _timeId
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

    Protected Sub btnSearchByFactor_Click(sender As Object, e As EventArgs) Handles btnSearchByFactor.Click
        BindGridView()
    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim _result As Boolean = False
        Dim userId As Integer = Convert.ToInt16(Session("UserID"))
        Dim lbTimeId As Label = gvMacroEconomicFactor.Rows(e.RowIndex).Cells(1).FindControl("lbTimeId")
        Dim lbFactorId As Label = gvMacroEconomicFactor.Rows(e.RowIndex).Cells(1).FindControl("lbFactorId")
        Dim lbStressMonth As Label = gvMacroEconomicFactor.Rows(e.RowIndex).Cells(1).FindControl("lbStressMonth")
        Dim lbStressYear As Label = gvMacroEconomicFactor.Rows(e.RowIndex).Cells(1).FindControl("lbStressYear")
        ViewState("timeId") = lbTimeId.Text
        ViewState("factorId") = lbFactorId.Text
        _result = _macroEconomicFactorBiz.Delete(lbTimeId.Text, lbStressYear.Text - 543, lbStressMonth.Text, lbFactorId.Text)
        Try
            If _result = True Then
                'LoadData()
                BindGridView()
                ViewState("timeId") = Nothing
                ViewState("factorId") = Nothing
                MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
            End If
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("MacroEconomicFactor.aspx", "OnRowDeleting()", ex.Message)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMacroEconomicFactor.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lbStressYear As Label = CType(e.Row.FindControl("lbStressYear"), Label)
            Dim StressYear As String = DataBinder.Eval(e.Row.DataItem, "StressYear").ToString()
            Dim StressMonth As Integer = DataBinder.Eval(e.Row.DataItem, "StressMonth").ToString()
            Dim lbMonth As Label = CType(e.Row.FindControl("lbMonth"), Label)
            'Dim monthId As Integer = 3 ' Example: March
            Dim thaiCulture As New CultureInfo("th-TH")
            Dim thaiMonthName As String = thaiCulture.DateTimeFormat.MonthNames(StressMonth - 1) ' Subtract 1 as the array is 0-based
            lbMonth.Text = thaiMonthName
            lbStressYear.Text = StressYear
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ViewState("mode") = "add"
        lblMessage.Visible = False
        lblModalTitle.Text = "เพิ่ม (Add)"
        txtAddEditFactorValue.Text = ""
        'txtAddEditMonth.Text = ""
        txtAddEditYear.Text = ""

        ddlAddEditMonth.Enabled = True
        ddlAddEditFactor.Enabled = True
        txtAddEditYear.Enabled = True
        txtAddEditFactorValue.Enabled = True

        ddlAddEditMonth.ClearSelection()
        ddlAddEditFactor.SelectedValue = ddlFactor.SelectedValue

        lbAddEditErrorMessage.Visible = False

        ViewState("timeId") = Nothing
        ViewState("factorId") = Nothing

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myAddEditModal", "$('#myAddEditModal').modal();", True)
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

    Private Function Vaidate()
        Dim errMsgList As New List(Of String)
        Dim factorId As String = ddlAddEditFactor.SelectedValue
        Dim year As String = txtAddEditYear.Text
        Dim month As String = ddlAddEditMonth.SelectedValue
        Dim factorValue As String = txtAddEditFactorValue.Text
        Dim timeId As String = ddlTime.SelectedValue
        Dim maxDecimals As Integer = 15

        If ViewState("mode") = "add" Then
            If (year <> "" And month <> "" And factorId <> "") Then
                Dim listEntity As List(Of MacroEconomicFactorEntity) = _macroEconomicFactorBiz.GetByTimeAndFactor(timeId, factorId)
                For Each ds As MacroEconomicFactorEntity In listEntity
                    If (ds.StressYear = year And ds.StressMonth = month And ds.FactorId = factorId) Then
                        errMsgList.Add("ข้อมูลซ้ำ")
                    End If
                Next
            End If

            If (year = "") Then
                errMsgList.Add("กรุณาเลือกปีที่ทดสอบภาวะวิกฤต")
            ElseIf (_valBiz.IsValidNumber(year) = False) Then
                errMsgList.Add("ปีที่ทดสอบภาวะวิกฤตต้องเป็นตัวเลขเท่านั้น")
            ElseIf (_valBiz.IsValidBuddhistYear(year) = False) Then
                errMsgList.Add("ปีที่ทดสอบภาวะวิกฤต กรอกพ.ศ.เท่านั้น")
            End If

            If (month = "") Then
                errMsgList.Add("กรุณาเลือกเดือนที่ทดสอบภาวะวิกฤต")
            End If

            If (factorValue = "") Then
                errMsgList.Add("กรุณากรอกค่า Factor Value")
            ElseIf (_valBiz.IsValidNumber(factorValue) = False) Then
                errMsgList.Add("ค่า Factor Value ต้องเป็นตัวเลขเท่านั้น")
            ElseIf (_valBiz.IsValidDecimal(factorValue, maxDecimals) = False) Then
                errMsgList.Add("ค่า Factor Value ทศยนืยมไม่เดิน " & maxDecimals & " หลัก")
            End If

        ElseIf ViewState("mode") = "edit" Then
            If (factorValue = "") Then
                errMsgList.Add("กรุณากรอกค่า Factor Value")
            ElseIf (_valBiz.IsValidNumber(factorValue) = False) Then
                errMsgList.Add("ค่า Factor Value ต้องเป็นตัวเลขเท่านั้น")
            ElseIf (_valBiz.IsValidDecimal(factorValue, maxDecimals) = False) Then
                errMsgList.Add("ค่า Factor Value ทศยนืยมไม่เดิน " & maxDecimals & " หลัก")
            End If
        End If

        Return errMsgList
    End Function

    Protected Sub btnSaveAdd_Click(sender As Object, e As EventArgs) Handles btnSaveAdd.Click
        Dim errMsgList As List(Of String) = Vaidate()
        If errMsgList.Count > 0 Then
            lbAddEditErrorMessage.Visible = True
            lbAddEditErrorMessage.Text = String.Join(",", errMsgList.ToArray())
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: true});", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myAddEditModal", "$('#myAddEditModal').modal({backdrop: true});", True)
            UpdModal.Update()
        Else
            If ViewState("mode") = "add" Then
                lbAddEditErrorMessage.Visible = False
                If SaveAddEdit() Then
                    'LoadData()
                    BindGridView()
                    ViewState("timeId") = Nothing
                    ViewState("factorId") = Nothing
                    MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                End If
            ElseIf ViewState("mode") = "edit" Then
                Dim timeId As String = ViewState("TimeId")
                Dim factorId As String = ViewState("FactorId")
                Dim scenarioId As String = ViewState("ScenarioId")
                Dim month As String = ViewState("Month")
                Dim year As String = ViewState("Year")
                Dim userId As Integer = Convert.ToInt16(Session("UserID"))
                If SaveAddEdit() Then
                    'LoadData()
                    BindGridView()
                    ViewState("timeId") = Nothing
                    ViewState("factorId") = Nothing
                    MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                End If
            End If
        End If
    End Sub

    Private Function SaveAddEdit() As Boolean

        'Dim _listEntity As New List(Of MacroEconomicFactorEntity)
        Dim _entity As New MacroEconomicFactorEntity
        _entity.TimeId = ddlTime.SelectedValue
        _entity.FactorValue = txtAddEditFactorValue.Text
        _entity.StressMonth = ddlAddEditMonth.SelectedValue
        _entity.StressYear = txtAddEditYear.Text - 543
        _entity.FactorId = ddlAddEditFactor.SelectedValue
        '_listEntity.Add(_entity)

        Dim _userId As Integer = Convert.ToInt16(Session("UserID"))

        ViewState("timeId") = ddlTime.SelectedValue
        ViewState("factorId") = ddlAddEditFactor.SelectedValue

        Return _macroEconomicFactorBiz.Save(_userId, _entity)
    End Function

    Protected Sub gvImportMev_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMacroEconomicFactor.RowCommand
        If e.CommandName = "Edit" Then
            ViewState("mode") = "edit"
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvMacroEconomicFactor.Rows(rowIndex)
            ' Find the Label control and get its value
            Dim lbTimeId As Label = CType(row.FindControl("lbTimeId"), Label)
            Dim lbFactorId As Label = CType(row.FindControl("lbFactorId"), Label)
            'Dim lbScenarioId As Label = CType(row.FindControl("lbScenarioId"), Label)
            Dim lbStressMonth As Label = CType(row.FindControl("lbStressMonth"), Label)
            Dim lbStressYear As Label = CType(row.FindControl("lbStressYear"), Label)
            Dim lbFactorValue As Label = CType(row.FindControl("lbFactorValue"), Label)

            ddlAddEditFactor.SelectedValue() = lbFactorId.Text
            'ddlAddEditScenario.SelectedValue() = lbScenarioId.Text

            ViewState("TimeId") = lbTimeId.Text
            ViewState("FactorId") = lbFactorId.Text
            'ViewState("ScenarioId") = lbScenarioId.Text
            ViewState("Month") = lbStressMonth.Text
            ViewState("Year") = lbStressYear.Text

            lblModalTitle.Text = "แก้ไข (Edit)"

            txtAddEditYear.Text = lbStressYear.Text
            ddlAddEditMonth.SelectedValue = lbStressMonth.Text
            txtAddEditFactorValue.Text = lbFactorValue.Text

            ddlAddEditMonth.Enabled = False
            ddlAddEditFactor.Enabled = False
            txtAddEditYear.Enabled = False
            txtAddEditFactorValue.Enabled = True

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myAddEditModal", "$('#myAddEditModal').modal();", True)
            UpdModal.Update()
        End If
    End Sub

    Protected Sub gvImportMev_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvMacroEconomicFactor.RowEditing

    End Sub

    Protected Sub gvMacroEconomicFactor_DataBound(sender As Object, e As EventArgs) Handles gvMacroEconomicFactor.DataBound

    End Sub

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Try
            Dim timeId As String = ddlTime.SelectedValue
            Dim listEntity As List(Of MacroEconomicFactorEntity) = _macroEconomicFactorBiz.GetTemplateByTime(timeId)

            ' Create a new Excel workbook
            Using workbook As New XLWorkbook()
                Dim worksheet = workbook.Worksheets.Add("MacroEconomicFactor")

                ' Add headers to the worksheet
                worksheet.Cell(1, 1).Value = "TIMEID"
                'worksheet.Cell(1, 2).Value = "ScenarioID"
                worksheet.Cell(1, 2).Value = "Stress_Year"
                worksheet.Cell(1, 3).Value = "Stress_Month"
                worksheet.Cell(1, 4).Value = "FactorID"
                worksheet.Cell(1, 5).Value = "FactorValue"

                Dim rowNum As Integer = 2
                For Each entity As MacroEconomicFactorEntity In listEntity
                    worksheet.Cell(rowNum, 1).Value = entity.TimeId.Trim()
                    'worksheet.Cell(rowNum, 2).Value = entity.ScenarioName.Trim()
                    worksheet.Cell(rowNum, 2).Value = entity.StressYear.Trim()
                    worksheet.Cell(rowNum, 3).Value = entity.StressMonth.Trim()
                    worksheet.Cell(rowNum, 4).Value = entity.FactorName.Trim()
                    worksheet.Cell(rowNum, 5).Value = entity.FactorValue.Trim()
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
                    Response.AddHeader("Content-Disposition", "attachment; filename=MacroEconomicFactor_Template.xlsx")
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
            UtilLogfile.writeToLog("MacroEconomicFactor.aspx", "btnDownloadTemplate_Click()", ex.Message)
            'Response.Write("An error occurred: " & ex.Message)
            Response.End()
        End Try
    End Sub
End Class