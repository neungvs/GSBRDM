Imports System.Globalization
Imports System.Threading
Imports System.Data
Imports System.IO
Imports GSBWeb.BLL
Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Web.HttpApplicationState
Imports GSBWeb.DAL




Public Class WF52_RejList

    Inherits System.Web.UI.Page

    Dim wfBiz As New WF52Biz

    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            LoadData()
            ClearSession()
            'SetControl()

        End If

    End Sub


    Protected Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click

        Response.Redirect("~/WebAdjust/DataAdjustment.aspx")

    End Sub



    Private Sub LoadData()

        gvWF52.DataSource = wfBiz.GetDataWF52()
        gvWF52.DataBind()

    End Sub



    Protected Sub gvWF52_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvWF52.RowDataBound

        Dim hpACCNO As LinkButton = CType(e.Row.FindControl("hpACCNO"), LinkButton)
        Dim HF_TMP_WF52ID As HiddenField = CType(e.Row.FindControl("HF_TMP_WF52ID"), HiddenField)

        Dim lblCifNumber As Label = CType(e.Row.FindControl("lblCifNumber"), Label)

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            hpACCNO.Text = DataBinder.Eval(e.Row.DataItem, "ACCNO").ToString()
            HF_TMP_WF52ID.Value = DataBinder.Eval(e.Row.DataItem, "TMP_WF52ID").ToString()

        End If

    End Sub


    Protected Sub gvWF52_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvWF52.PageIndexChanging

        gvWF52.PageIndex = e.NewPageIndex

        If String.IsNullOrEmpty(Session("CifNum")) And String.IsNullOrEmpty(Session("AccNo")) And String.IsNullOrEmpty(Session("Name")) And String.IsNullOrEmpty(Session("SurName")) Then
            LoadData()
        Else
            SearchData()
        End If

    End Sub


    Protected Sub gvWF52_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvWF52.SelectedIndexChanged

        Dim lblCifNumber As Label = CType(gvWF52.SelectedRow.FindControl("lblCifNumber"), Label)
        Dim hpACCNO As LinkButton = CType(gvWF52.SelectedRow.FindControl("hpACCNO"), LinkButton)
        Dim HF_TMP_WF52ID As HiddenField = CType(gvWF52.SelectedRow.FindControl("HF_TMP_WF52ID"), HiddenField)

        ClearSession()
        Session("CifNumber") = lblCifNumber.Text
        Session("ACCNO") = hpACCNO.Text
        Session("TMPID") = HF_TMP_WF52ID.Value

        Response.Redirect("WF52_RejEdit.aspx")

    End Sub



    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click


        If Not String.IsNullOrEmpty(txtAccNo.Text) Then

            Dim result As Boolean = False

            result = UtilsBiz.checkForSQLInjection(txtAccNo.Text.Trim())

            If result = True Then

                UtilsBiz.CreateMessageAlert(Page, "ตรวจพบคำสั่ง sqlInjection ไม่สามารถค้นหาข้อมูลได้", gui)
                Return

            End If


        End If

        If Not String.IsNullOrEmpty(txtCifNum.Text) Then

            Dim result As Boolean = False

            result = UtilsBiz.checkForSQLInjection(txtCifNum.Text.Trim())

            If result = True Then

                UtilsBiz.CreateMessageAlert(Page, "ตรวจพบคำสั่ง sqlInjection ไม่สามารถค้นหาข้อมูลได้", gui)
                Return

            End If


        End If


        If Not String.IsNullOrEmpty(txtCifName.Text) Then

            Dim result As Boolean = False

            result = UtilsBiz.checkForSQLInjection(txtCifName.Text.Trim())

            If result = True Then

                UtilsBiz.CreateMessageAlert(Page, "ตรวจพบคำสั่ง sqlInjection ไม่สามารถค้นหาข้อมูลได้", gui)
                Return

            End If


        End If




        If Not String.IsNullOrEmpty(txtCifSurName.Text) Then

            Dim result As Boolean = False

            result = UtilsBiz.checkForSQLInjection(txtCifSurName.Text.Trim())

            If result = True Then

                UtilsBiz.CreateMessageAlert(Page, "ตรวจพบคำสั่ง sqlInjection ไม่สามารถค้นหาข้อมูลได้", gui)
                Return

            End If


        End If

        ClearSession()


        Session("CifNum") = txtCifNum.Text
        Session("AccNo") = txtAccNo.Text
        Session("Name") = txtCifName.Text
        Session("SurName") = txtCifSurName.Text

        gvWF52.PageIndex = 0

        SearchData()

    End Sub

    Private Sub SearchData()

        gvWF52.DataSource = wfBiz.GetWF52ByCriteria(Convert.ToString(Session("CifNum")), Convert.ToString(Session("AccNo")), Convert.ToString(Session("Name")), Convert.ToString(Session("SurName")))
        gvWF52.DataBind()

    End Sub




    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click

        Dim rds As New ReportDataSource
        rds.Name = "DataSet1"

        Dim lsRej As New List(Of RejListReportEntity)

        lsRej = wfBiz.GetRejData_Report()

        For Each item As RejListReportEntity In lsRej

            If item.ColumnName = "GL_ACCOUNT_ID" Then

                item.ColumnName = "รหัสบัญชีของธนาคาร (GL_ACCOUNT_ID)"

            ElseIf item.ColumnName = "CUR_GROSS_RATE" Then
                item.ColumnName = "อัตราดอกเบี้ยปัจจุบันที่ลูกค้าจ่าย (CUR_GROSS_RATE)"

            ElseIf item.ColumnName = "SETTLEMENT_DATE" Then
                item.ColumnName = "วันที่ครบกำหนดส่งมอบ (SETTLEMENT_DATE)"

            ElseIf item.ColumnName = "ORIGINATION_DATE" Then
                item.ColumnName = "วันที่เริ่มดำเนินการกับธนาคารของแต่ละบัญชี (ORIGINATION_DATE)"

            End If

        Next

        rds.Value = lsRej



        Dim viewer As New ReportViewer
        viewer.ProcessingMode = ProcessingMode.Local
        viewer.LocalReport.ReportPath = Server.MapPath("~/Reports/WF52_RejReport.rdlc")
        viewer.LocalReport.DataSources.Add(rds)




        'Variables
        Dim warnings() As Warning
        Dim strStreams() As String
        Dim MIMETYPE As String = String.Empty
        Dim encoding As String = String.Empty
        Dim extension As String = String.Empty
        Dim strDeviceInfo As String = ""
        Dim deviceInfo As String = ""


        Dim bytes() As Byte = viewer.LocalReport.Render("PDF", strDeviceInfo, MIMETYPE, encoding, extension, strStreams, warnings)

        Dim fileName As String = "WF52_RejectDetail_" + String.Format("{0:ddMMyyyy}", Date.Now) + ".pdf"


        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.Close()
        Response.End()


    End Sub

    Private Sub SetControl()

        Dim liAj As HtmlGenericControl = Page.Master.FindControl("liAj")
        liAj.Attributes.Add("class", "active")

    End Sub



    Private Sub ClearSession()

        Session("CifNum") = Nothing
        Session("AccNo") = Nothing
        Session("Name") = Nothing
        Session("SurName") = Nothing
        Session("TMPID") = Nothing
        Session("CifNumber") = Nothing
        Session("ACCNO") = Nothing

    End Sub


End Class