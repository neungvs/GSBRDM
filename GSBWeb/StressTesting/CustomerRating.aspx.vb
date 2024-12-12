Imports System.Drawing
Imports Arsoft.Utility
Imports System.IO
Imports ClosedXML.Excel
Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class Customer_Rating
    Inherits System.Web.UI.Page
    Private _customerRatingBiz As New CustomerRatingBiz
    Private _timeBiz As New TimeBiz
    Private _valBiz As New ValidateBiz

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindDropDownListTime()
            tbCustomerRatingHeader.Visible = False
            gvCustomerRating.Visible = False
        End If
    End Sub

#Region "Sub Program"
    Private Sub BindDropDownListTime()
        Dim _lsTime As List(Of TimeEntity)
        Dim _html As New StringBuilder
        _lsTime = _timeBiz.GetDate()
        ddlTime.DataSource = _lsTime
        ddlTime.DataValueField = "TimeId"
        ddlTime.DataTextField = "TimeName"
        ddlTime.DataBind()
    End Sub
    Private Sub BindGridViewCustomerRating()
        gvCustomerRating.DataSource = _customerRatingBiz.GetByTime(ddlTime.SelectedValue)
        gvCustomerRating.DataBind()
        tbCustomerRatingHeader.Visible = True
        gvCustomerRating.Visible = True
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
#End Region

#Region "Search"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGridViewCustomerRating()
    End Sub
    Protected Sub btnExcelImport_Click(sender As Object, e As EventArgs) Handles btnExcelImport.Click
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()

        lblModalTitle.Text = "Import Excel"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        UpdModal.Update()
    End Sub
#End Region

#Region "Result"
    Protected Sub gvCustomerRating_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCustomerRating.PageIndexChanging
        gvCustomerRating.PageIndex = e.NewPageIndex
        BindGridViewCustomerRating()
    End Sub

#End Region

#Region "Import"

    Private Function BindingData() As List(Of CustomerRatingEntity)
        Dim dt As DataTable = New DataTable()
        Dim _listData As New List(Of CustomerRatingEntity)
        Dim dateUtil As New DateHelperUtil
        dt = ViewState("dtImportRawData")
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim entity As New CustomerRatingEntity
                Dim Time As String = row("วันที่ของข้อมูล").ToString()
                Dim CIFNumber As String = row("CIF Number").ToString()
                Dim Year As String = row("ปีที่ทดสอบภาวะวิกฤต").ToString()
                Dim Scenario As String = row("สถานการณ์ภาวะวิกฤต").ToString()
                Dim OldPdSegment As String = row("pd_segment เก่า").ToString()
                Dim NewPdSegment As String = row("pd_segment ใหม่").ToString()

                entity.TimeId = dateUtil.GetLastDayOfMonth(Time)
                entity.CustomerNr = CIFNumber
                entity.Year = Year
                entity.ScenarioName = Scenario
                entity.OldPdSegment = OldPdSegment
                entity.NewPdSegment = NewPdSegment
                _listData.Add(entity)
            Next
        End If
        Return _listData
    End Function
    Private Function CheckValidData(row As DataRow, lstScenario As List(Of ScenarioEntity)) As List(Of String)
        Dim Time As String = row("วันที่ของข้อมูล").ToString()
        Dim CIFNumber As String = row("CIF Number").ToString()
        Dim Scenario As String = row("สถานการณ์ภาวะวิกฤต").ToString()
        Dim Year As String = row("ปีที่ทดสอบภาวะวิกฤต").ToString()
        Dim OldPdSegment As String = row("pd_segment เก่า").ToString()
        Dim NewPdSegment As String = row("pd_segment ใหม่").ToString()
        Dim retData As New List(Of String)
        Dim errMsgList As New List(Of String)

        errMsgList = (_valBiz.CheckValidCustomerRatingByDataType("Time", Time))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidCustomerRatingByDataType("CIFNumber", CIFNumber))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidCustomerRatingByDataType("Scenario", Scenario, lstScenario))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidCustomerRatingByDataType("Year", Year))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidCustomerRatingByDataType("OldPdSegment", OldPdSegment))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidCustomerRatingByDataType("NewPdSegment", NewPdSegment))
        retData.AddRange(errMsgList)

        Return retData
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
                        BindGridViewCustomerRating()
                        grvImportExcel.DataSource = Nothing
                        grvImportExcel.DataBind()
                        ViewState("dtImportRawData") = Nothing
                        MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                    End If
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
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

    Private Function SaveImport() As Boolean
        Dim timeId As String = ddlTime.SelectedValue
        If _customerRatingBiz.DeleteByTimeId(timeId) Then
            Dim _listData As List(Of CustomerRatingEntity) = BindingData()
            Return _customerRatingBiz.SaveInsertImportExcel(_listData)
        End If
        Return False
    End Function

    Protected Sub btnCancelImport_Click(sender As Object, e As EventArgs) Handles btnCancelImport.Click
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
    End Sub

    Protected Sub grvImportExcel_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvImportExcel.PageIndexChanging
        grvImportExcel.PageIndex = e.NewPageIndex
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: false});", True)
        grvImportExcel.DataSource = ViewState("dtImportRawData")
        grvImportExcel.DataBind()
    End Sub

    Private Sub grvImportExcel_DataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvImportExcel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'วันที่ของข้อมูล CIF Number	สถานการณ์ภาวะวิกฤต	ปีที่ทดสอบภาวะวิกฤต	pd_segment เก่า	pd_segment ใหม่
            Dim Time As String = DataBinder.Eval(e.Row.DataItem, "วันที่ของข้อมูล").ToString()
            Dim CIFNumber As String = DataBinder.Eval(e.Row.DataItem, "CIF Number").ToString()
            Dim Scenario As String = DataBinder.Eval(e.Row.DataItem, "สถานการณ์ภาวะวิกฤต").ToString()
            Dim Year As String = DataBinder.Eval(e.Row.DataItem, "ปีที่ทดสอบภาวะวิกฤต").ToString()
            Dim OldPdSegment As String = DataBinder.Eval(e.Row.DataItem, "pd_segment เก่า").ToString()
            Dim NewPdSegment As String = DataBinder.Eval(e.Row.DataItem, "pd_segment ใหม่").ToString()

            Dim ErrorDetail As String = DataBinder.Eval(e.Row.DataItem, "ErrorDetail").ToString()
            Dim lbTime As Label = CType(e.Row.FindControl("lbTime"), Label)
            Dim lbCIFNumber As Label = CType(e.Row.FindControl("lbCIFNumber"), Label)
            Dim lbScenario As Label = CType(e.Row.FindControl("lbScenario"), Label)
            Dim lbYear As Label = CType(e.Row.FindControl("lbYear"), Label)
            Dim lbOldPdSegment As Label = CType(e.Row.FindControl("lbOldPdSegment"), Label)
            Dim lbNewPdSegment As Label = CType(e.Row.FindControl("lbNewPdSegment"), Label)
            Dim lbErrorDetail As Label = CType(e.Row.FindControl("lbErrorDetail"), Label)
            lbTime.Text = Time
            lbCIFNumber.Text = CIFNumber
            lbScenario.Text = Scenario
            lbYear.Text = Year
            lbOldPdSegment.Text = OldPdSegment
            lbNewPdSegment.Text = NewPdSegment
            lbErrorDetail.Text = ErrorDetail
            e.Row.ForeColor = Color.Red
        End If
    End Sub

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Try
            Dim timeId As String = ddlTime.SelectedValue
            Dim listEntity As List(Of CustomerRatingEntity) = _customerRatingBiz.GetTemplateByTime(timeId)

            ' Create a new Excel workbook
            Using workbook As New XLWorkbook()
                Dim worksheet = workbook.Worksheets.Add("CustomerRating")

                ' Add headers to the worksheet
                worksheet.Cell(1, 1).Value = "วันที่ของข้อมูล"
                worksheet.Cell(1, 2).Value = "CIF Number"
                worksheet.Cell(1, 3).Value = "สถานการณ์ภาวะวิกฤต"
                worksheet.Cell(1, 4).Value = "ปีที่ทดสอบภาวะวิกฤต"
                worksheet.Cell(1, 5).Value = "pd_segment เก่า"
                worksheet.Cell(1, 6).Value = "pd_segment ใหม่"

                Dim rowNum As Integer = 2
                For Each entity As CustomerRatingEntity In listEntity
                    worksheet.Cell(rowNum, 1).Value = entity.TimeId.Trim()
                    worksheet.Cell(rowNum, 2).Value = entity.CustomerNr.Trim()
                    worksheet.Cell(rowNum, 3).Value = entity.ScenarioName.Trim()
                    worksheet.Cell(rowNum, 4).Value = entity.Year.Trim()
                    worksheet.Cell(rowNum, 5).Value = entity.OldPdSegment.Trim()
                    worksheet.Cell(rowNum, 6).Value = entity.NewPdSegment.Trim()
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
                    Response.AddHeader("Content-Disposition", "attachment; filename=CustomerRating_Template.xlsx")
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
            UtilLogfile.writeToLog("CustomerRating.aspx", "btnDownloadTemplate_Click()", ex.Message)
            'Response.Write("An error occurred: " & ex.Message)
            Response.End()
        End Try
    End Sub

#End Region

#Region "Create New"

#End Region

#Region "Add/Edit"

#End Region





    'Function IsValidScenario(Scenario As String, lstScenario As List(Of ScenarioEntity)) As Boolean
    '    For Each _senario As ScenarioEntity In lstScenario
    '        If (Scenario = _senario.ScenarioName) Then
    '            Return True
    '        End If
    '    Next
    '    Return False
    'End Function

    'Function IsValidNumber(value As String) As Boolean
    '    Return IsNumeric(value)
    'End Function

End Class