
Imports Arsoft.Utility
Imports System.IO
Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Spreadsheet
Imports GSBWeb.BLL
Imports GSBWeb.DAL
Public Class LGD

    Inherits System.Web.UI.Page
    Private _lgdBiz As New LGDBiz
    Private _timeBiz As New TimeBiz
    Private _valBiz As New ValidateBiz

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            tbLgdHeader.Visible = False
            gvLgd.Visible = False
            LoadDropDownListTime()
        End If
    End Sub

    Private Sub BindGridviewLgd()
        Dim _listEntity As List(Of LGDEntity)
        _listEntity = _lgdBiz.GetByTime(cb_List_Time.SelectedValue)
        gvLgd.DataSource = _listEntity
        gvLgd.DataBind()
    End Sub

    Private Function LoadScenario() As List(Of ScenarioEntity)
        Return _timeBiz.GetScenario(cb_List_Time.SelectedValue)
    End Function

    Private Sub LoadDropDownListTime()
        Dim _listEntity As List(Of TimeEntity)
        _listEntity = _timeBiz.GetDate()
        cb_List_Time.DataSource = _listEntity
        cb_List_Time.DataValueField = "TimeId"
        cb_List_Time.DataTextField = "TimeName"
        cb_List_Time.DataBind()
    End Sub

    Protected Sub btn_Excel_Import_Click(sender As Object, e As EventArgs) Handles btn_Excel_Import.Click
        lblModalTitle.Text = "Import Excel"
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
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
                        BindGridviewLgd()
                        tbLgdHeader.Visible = True
                        gvLgd.Visible = True
                        grvImportExcel.DataSource = Nothing
                        grvImportExcel.DataBind()
                        ViewState("dtImportRawData") = Nothing
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


        Dim lstTime As List(Of TimeEntity)
        lstTime = _timeBiz.GetDate()


        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim retError As List(Of String) = CheckValidData(row, lstScenario, lstTime)
                If retError.Count > 0 Then
                    row("ErrorDetail") = String.Join(",", retError.ToArray())
                    retDt.ImportRow(row)
                End If
            Next
        End If
        Return retDt
    End Function

    Private Function CheckValidData(row As DataRow, lstScenario As List(Of ScenarioEntity), lstTime As List(Of TimeEntity)) As List(Of String)
        Dim Time As String = row("วันที่ของข้อมูล").ToString()
        Dim Year As String = row("ปีที่ทดสอบภาวะวิกฤต").ToString()
        Dim Scenario As String = row("สถาณการณ์ภาวะวิกฤต").ToString()
        Dim StressLgdScalar As String = row("ค่า LGD Scalar").ToString()
        Dim retData As New List(Of String)
        Dim errMsgList As New List(Of String)

        errMsgList = (_valBiz.CheckValidByDataType("Time", Time, Nothing, Nothing, lstTime))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("Year", Year))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("Scenario", Scenario, lstScenario))
        retData.AddRange(errMsgList)

        errMsgList = (_valBiz.CheckValidByDataType("StressLgdScalar", StressLgdScalar))
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
            Dim Time As String = DataBinder.Eval(e.Row.DataItem, "วันที่ของข้อมูล").ToString()
            Dim Year As String = DataBinder.Eval(e.Row.DataItem, "ปีที่ทดสอบภาวะวิกฤต").ToString()
            Dim Scenario As String = DataBinder.Eval(e.Row.DataItem, "สถาณการณ์ภาวะวิกฤต").ToString()
            Dim StressLgdScalar As String = DataBinder.Eval(e.Row.DataItem, "ค่า LGD Scalar").ToString()
            Dim ErrorDetail As String = DataBinder.Eval(e.Row.DataItem, "ErrorDetail").ToString()
            Dim lbTime As Label = CType(e.Row.FindControl("lbTime"), Label)
            Dim lbYear As Label = CType(e.Row.FindControl("lbYear"), Label)
            Dim lbScenario As Label = CType(e.Row.FindControl("lbScenario"), Label)
            Dim lbStressLgdScalar As Label = CType(e.Row.FindControl("lbStressLgdScalar"), Label)
            Dim lbErrorDetail As Label = CType(e.Row.FindControl("lbErrorDetail"), Label)
            lbTime.Text = Time
            lbYear.Text = Year
            lbScenario.Text = Scenario
            lbStressLgdScalar.Text = StressLgdScalar
            lbErrorDetail.Text = ErrorDetail
            e.Row.ForeColor = System.Drawing.Color.Red
        End If
    End Sub

    Private Function BindLgdList() As List(Of LGDEntity)
        Dim dt As DataTable = New DataTable()
        Dim _listEntity As New List(Of LGDEntity)
        dt = ViewState("dtImportRawData")
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim entity As New LGDEntity
                Dim Time As String = row("วันที่ของข้อมูล").ToString()
                Dim Year As String = row("ปีที่ทดสอบภาวะวิกฤต").ToString()
                Dim Scenario As String = row("สถาณการณ์ภาวะวิกฤต").ToString()
                Dim StressLgdScalar As String = row("ค่า LGD Scalar").ToString()
                entity.TimeId = Time
                entity.Year = Year
                entity.Scenario = Scenario
                entity.StressLgdScalar = StressLgdScalar
                _listEntity.Add(entity)
            Next
        End If
        Return _listEntity
    End Function

    Private Function SaveImport() As Boolean
        Dim _listEntity As List(Of LGDEntity) = BindLgdList()
        Return _lgdBiz.Save(_listEntity)
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGridviewLgd()
        tbLgdHeader.Visible = True
        gvLgd.Visible = True
    End Sub

    Protected Sub btnCancelImport_Click(sender As Object, e As EventArgs) Handles btnCancelImport.Click
        grvImportExcel.DataSource = Nothing
        grvImportExcel.DataBind()
    End Sub

    Protected Sub gv_Lgd_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLgd.PageIndexChanging
        gvLgd.PageIndex = e.NewPageIndex
        BindGridviewLgd()
        tbLgdHeader.Visible = True
        gvLgd.Visible = True
    End Sub

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Try
            Dim timeId As String = cb_List_Time.SelectedValue
            Dim listEntity As List(Of LGDEntity) = _lgdBiz.GetTemplateByTime(timeId)

            ' Create a new Excel workbook
            Using workbook As New XLWorkbook()
                Dim worksheet = workbook.Worksheets.Add("LGD")

                ' Add headers to the worksheet
                worksheet.Cell(1, 1).Value = "วันที่ของข้อมูล"
                worksheet.Cell(1, 2).Value = "ปีที่ทดสอบภาวะวิกฤต"
                worksheet.Cell(1, 3).Value = "สถาณการณ์ภาวะวิกฤต"
                worksheet.Cell(1, 4).Value = "ค่า LGD Scalar"

                Dim rowNum As Integer = 2
                For Each entity As LGDEntity In listEntity
                    worksheet.Cell(rowNum, 1).Value = entity.TimeId.Trim()
                    worksheet.Cell(rowNum, 2).Value = entity.Year.Trim()
                    worksheet.Cell(rowNum, 3).Value = entity.Scenario.Trim()
                    worksheet.Cell(rowNum, 4).Value = entity.StressLgdScalar.Trim()
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
                    Response.AddHeader("Content-Disposition", "attachment; filename=LGD_Template.xlsx")
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
            UtilLogfile.writeToLog("LGD.aspx", "btnDownloadTemplate_Click()", ex.Message)
            'Response.Write("An error occurred: " & ex.Message)
            Response.End()
        End Try
    End Sub
End Class