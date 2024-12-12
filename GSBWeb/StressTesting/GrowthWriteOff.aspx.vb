Imports System.Drawing
Imports Arsoft.Utility
Imports System.IO
Imports ClosedXML.Excel
Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class GrowthWriteOff
    Inherits System.Web.UI.Page
    Private _growthWriteOffBiz As New GrowthWriteOffBiz
    Private _timeBiz As New TimeBiz
    Private _valBiz As New ValidateBiz

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadDownDownTime()
            tbGrowthWriteOffHeader.Visible = False
            gvGrowthWriteOff.Visible = False
        End If
    End Sub

#Region "Sub Program"
    Private Sub LoadGvGrowthWriteOff()
        Dim _lsEntity As List(Of GrowthWriteOffEntity)
        _lsEntity = _growthWriteOffBiz.GetByTime(ddlTime.SelectedValue)
        gvGrowthWriteOff.DataSource = _lsEntity
        gvGrowthWriteOff.DataBind()
    End Sub
    Private Sub LoadDownDownTime()
        Dim _lsTime As List(Of TimeEntity)
        _lsTime = _timeBiz.GetDate()
        ddlTime.DataSource = _lsTime
        ddlTime.DataValueField = "TimeId"
        ddlTime.DataTextField = "TimeName"
        ddlTime.DataBind()
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
#End Region

#Region "Function"
    Private Function LoadScenario() As List(Of ScenarioEntity)
        Return _timeBiz.GetScenario(ddlTime.SelectedValue)
    End Function
    Private Function GetInvalidData(dt As DataTable) As DataTable
        Dim retDt As DataTable = New DataTable()
        For Each col In dt.Columns
            retDt.Columns.Add(col.ToString())
        Next
        Dim lstScenario As List(Of ScenarioEntity)
        lstScenario = LoadScenario()

        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim retError As List(Of String) = CheckValidData(row, lstScenario)
                If retError.Count > 0 Then
                    row("ErrorDetail") = String.Join(",", retError.ToArray())
                    retDt.ImportRow(row)
                End If
            Next
        End If
        Return retDt
    End Function
    Private Function CheckValidData(row As DataRow, lstScenario As List(Of ScenarioEntity)) As List(Of String)
        Dim Time As String = row("วันที่ของข้อมูล").ToString()
        Dim PdSegment As String = row("PD_SEGMENT").ToString()
        Dim Year As String = row("ปีที่ทดสอบภาวะวิกฤต").ToString()
        Dim Scenario As String = row("สถานการณ์ภาวะวิกฤต").ToString()
        Dim LoanGrowth As String = row("Loan Growth").ToString()
        Dim WriteOff As String = row("Write Off").ToString()
        Dim retData As New List(Of String)
        Dim errMsgList As New List(Of String)

        errMsgList = (_valBiz.CheckValidByDataType("Time", Time))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("PdSegment", Year))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("Year", Year))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("Scenario", Scenario, lstScenario))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("LoanGrowth", LoanGrowth))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("WriteOff", WriteOff))
        retData.AddRange(errMsgList)

        Return retData
    End Function
    Private Function BindingData() As List(Of GrowthWriteOffEntity)
        Dim dt As DataTable = New DataTable()
        Dim dateUtil As New DateHelperUtil
        Dim _listEntity As New List(Of GrowthWriteOffEntity)
        dt = ViewState("dtImportRawData")
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim _entity As New GrowthWriteOffEntity
                Dim Time As String = row("วันที่ของข้อมูล").ToString()
                Dim PdSegment As String = row("PD_SEGMENT").ToString()
                Dim Year As String = row("ปีที่ทดสอบภาวะวิกฤต").ToString()
                Dim Scenario As String = row("สถานการณ์ภาวะวิกฤต").ToString()
                Dim LoanGrowth As String = row("Loan Growth").ToString()
                Dim WriteOff As String = row("Write Off").ToString()

                _entity.TimeId = dateUtil.GetLastDayOfMonth(Time)
                _entity.PdSegment = PdSegment
                _entity.Year = Year
                _entity.ScenarioName = Scenario
                _entity.LoanGrowthPerc = LoanGrowth
                _entity.WriteOffPerc = WriteOff
                _listEntity.Add(_entity)
            Next
        End If
        Return _listEntity
    End Function
#End Region

#Region "Button Main"
    Protected Sub btnExcelImport_Click(sender As Object, e As EventArgs) Handles btnExcelImport.Click
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()

        lblModalTitle.Text = "Import Excel"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        UpdModal.Update()
    End Sub
#End Region

#Region "Modal Import Excel"
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim dt As DataTable = New DataTable()

        If fileUpload.HasFile Then
            Try
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
                        LoadGvGrowthWriteOff()
                        tbGrowthWriteOffHeader.Visible = True
                        gvGrowthWriteOff.Visible = True
                        grvImportExcel.DataSource = Nothing
                        grvImportExcel.DataBind()
                        ViewState("dtImportRawData") = Nothing
                        MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                    Else
                        MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
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
    Protected Sub grvImportExcel_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvImportExcel.PageIndexChanging
        grvImportExcel.PageIndex = e.NewPageIndex
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: false});", True)
        grvImportExcel.DataSource = ViewState("dtImportRawData")
        grvImportExcel.DataBind()
    End Sub
    Private Sub grvImportExcel_DataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvImportExcel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Time As String = DataBinder.Eval(e.Row.DataItem, "วันที่ของข้อมูล").ToString()
            Dim PdSegment As String = DataBinder.Eval(e.Row.DataItem, "PD_SEGMENT").ToString()
            Dim Year As String = DataBinder.Eval(e.Row.DataItem, "ปีที่ทดสอบภาวะวิกฤต").ToString()
            Dim Scenario As String = DataBinder.Eval(e.Row.DataItem, "สถานการณ์ภาวะวิกฤต").ToString()
            Dim LoanGrowth As String = DataBinder.Eval(e.Row.DataItem, "Loan Growth").ToString()
            Dim WriteOff As String = DataBinder.Eval(e.Row.DataItem, "Write Off").ToString()
            Dim ErrorDetail As String = DataBinder.Eval(e.Row.DataItem, "ErrorDetail").ToString()

            Dim lbTime As Label = CType(e.Row.FindControl("lbTime"), Label)
            Dim lbPdSegment As Label = CType(e.Row.FindControl("lbPdSegment"), Label)
            Dim lbYear As Label = CType(e.Row.FindControl("lbYear"), Label)
            Dim lbScenario As Label = CType(e.Row.FindControl("lbScenario"), Label)
            Dim lbLoanGrowth As Label = CType(e.Row.FindControl("lbLoanGrowth"), Label)
            Dim lbWriteOff As Label = CType(e.Row.FindControl("lbWriteOff"), Label)
            Dim lbErrorDetail As Label = CType(e.Row.FindControl("lbErrorDetail"), Label)

            lbTime.Text = Time
            lbPdSegment.Text = PdSegment
            lbYear.Text = Year
            lbScenario.Text = Scenario
            lbLoanGrowth.Text = LoanGrowth
            lbWriteOff.Text = WriteOff
            lbErrorDetail.Text = ErrorDetail
            e.Row.ForeColor = Color.Red
        End If
    End Sub
    Private Function SaveImport() As Boolean
        Dim timeId As String = ddlTime.SelectedValue
        If _growthWriteOffBiz.DeleteByTimeId(timeId) Then
            Dim _listEntity As List(Of GrowthWriteOffEntity) = BindingData()
            Return _growthWriteOffBiz.SaveInsertImportExcel(_listEntity)
        End If
        Return False
    End Function
    Protected Sub btnCancelImport_Click(sender As Object, e As EventArgs) Handles btnCancelImport.Click
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
    End Sub

#End Region

#Region "gvGrowthWriteOff"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadGvGrowthWriteOff()
        tbGrowthWriteOffHeader.Visible = True
        gvGrowthWriteOff.Visible = True
    End Sub

    Protected Sub gvGrowthWriteOff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvGrowthWriteOff.PageIndexChanging
        gvGrowthWriteOff.PageIndex = e.NewPageIndex
        LoadGvGrowthWriteOff()
        tbGrowthWriteOffHeader.Visible = True
        gvGrowthWriteOff.Visible = True
    End Sub

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Try
            Dim timeId As String = ddlTime.SelectedValue
            Dim listEntity As List(Of GrowthWriteOffEntity) = _growthWriteOffBiz.GetTemplateByTime(timeId)

            ' Create a new Excel workbook
            Using workbook As New XLWorkbook()
                Dim worksheet = workbook.Worksheets.Add("GrowthWriteOff")

                ' Add headers to the worksheet
                worksheet.Cell(1, 1).Value = "วันที่ของข้อมูล"
                worksheet.Cell(1, 2).Value = "PD_SEGMENT"
                worksheet.Cell(1, 3).Value = "ปีที่ทดสอบภาวะวิกฤต"
                worksheet.Cell(1, 4).Value = "สถานการณ์ภาวะวิกฤต"
                worksheet.Cell(1, 5).Value = "Loan Growth"
                worksheet.Cell(1, 6).Value = "Write Off"

                Dim rowNum As Integer = 2
                For Each entity As GrowthWriteOffEntity In listEntity
                    worksheet.Cell(rowNum, 1).Value = entity.TimeId.Trim()
                    worksheet.Cell(rowNum, 2).Value = entity.PdSegment.Trim()
                    worksheet.Cell(rowNum, 3).Value = entity.Year.Trim()
                    worksheet.Cell(rowNum, 4).Value = entity.ScenarioName.Trim()
                    worksheet.Cell(rowNum, 5).Value = entity.LoanGrowthPerc.Trim()
                    worksheet.Cell(rowNum, 6).Value = entity.WriteOffPerc.Trim()
                    rowNum = rowNum + 1
                Next

                'Adjust column widthsy
                worksheet.Columns().AdjustToContents()

                ' Save the workbook to memory stream
                Using memoryStream As New MemoryStream()
                    workbook.SaveAs(memoryStream)
                    memoryStream.Seek(0, SeekOrigin.Begin)
                    ' Clear the response
                    Response.Clear()
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("Content-Disposition", "attachment; filename=GrowthWriteOff_Template.xlsx")
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
            UtilLogfile.writeToLog("GrowthWriteOff.aspx", "btnDownloadTemplate_Click()", ex.Message)
            'Response.Write("An error occurred: " & ex.Message)
            Response.End()
        End Try
    End Sub
#End Region


End Class