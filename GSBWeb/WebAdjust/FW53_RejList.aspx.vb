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



Public Class FW53_RejList
    Inherits System.Web.UI.Page

    Dim fwBiz As New FW53Biz

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

        gvFW53.DataSource = fwBiz.GetDataFW53()
        gvFW53.DataBind()

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

        gvFW53.PageIndex = 0

        SearchData()

    End Sub


    Protected Sub gvFW53_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFW53.SelectedIndexChanged

        Dim lblCifNumber As Label = CType(gvFW53.SelectedRow.FindControl("lblCifNumber"), Label)
        Dim hpACCNO As LinkButton = CType(gvFW53.SelectedRow.FindControl("hpACCNO"), LinkButton)
        Dim HF_TMP_FW53ID As HiddenField = CType(gvFW53.SelectedRow.FindControl("HF_TMP_FW53ID"), HiddenField)

        ClearSession()
        Session("CifNumber") = lblCifNumber.Text
        Session("ACCNO") = hpACCNO.Text
        Session("TMPID") = HF_TMP_FW53ID.Value

        Response.Redirect("FW53_RejEdit.aspx")


    End Sub


    Protected Sub gvFW53_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFW53.PageIndexChanging

        gvFW53.PageIndex = e.NewPageIndex

        If String.IsNullOrEmpty(Session("CifNum")) And String.IsNullOrEmpty(Session("AccNo")) And String.IsNullOrEmpty(Session("Name")) And String.IsNullOrEmpty(Session("SurName")) Then
            LoadData()
        Else
            SearchData()
        End If

    End Sub

    Protected Sub gvFW53_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFW53.RowDataBound

        Dim hpACCNO As LinkButton = CType(e.Row.FindControl("hpACCNO"), LinkButton)
        Dim HF_TMP_FW53ID As HiddenField = CType(e.Row.FindControl("HF_TMP_FW53ID"), HiddenField)


        Dim lblCifNumber As Label = CType(e.Row.FindControl("lblCifNumber"), Label)


        If (e.Row.RowType = DataControlRowType.DataRow) Then

            hpACCNO.Text = DataBinder.Eval(e.Row.DataItem, "ACCNO").ToString()
            HF_TMP_FW53ID.Value = DataBinder.Eval(e.Row.DataItem, "TMP_FW53ID").ToString()

        End If



    End Sub

    Private Sub SearchData()

        gvFW53.DataSource = fwBiz.GetFW53ByCriteria(Convert.ToString(Session("CifNum")), Convert.ToString(Session("AccNo")), Convert.ToString(Session("Name")), Convert.ToString(Session("SurName")))
        gvFW53.DataBind()

    End Sub





    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click

        Dim rds As New ReportDataSource
        rds.Name = "DataSet1"

        Dim lsRej As New List(Of RejListReportEntity)

        lsRej = fwBiz.GetRejData_Report()

        For Each item As RejListReportEntity In lsRej

            If item.ColumnName = "GL_ACCOUNT_ID" Then

                item.ColumnName = "รหัสบัญชีของธนาคาร (GL_ACCOUNT_ID)"

            ElseIf item.ColumnName = "AS_OF_DATE" Then
                item.ColumnName = "วันที่ของข้อมูล (AS_OF_DATE)"

            ElseIf item.ColumnName = "ORIGINATION_DATE" Then
                item.ColumnName = "วันที่เริ่มต้นของรายการ (ORIGINATION_DATE)"

            ElseIf item.ColumnName = "MATURITY_DATE" Then
                item.ColumnName = "เป็นข้อมูลวันที่ครบกำหนดสัญญา (MATURITY_DATE)"

            End If

        Next

        rds.Value = lsRej
 

        Dim viewer As New ReportViewer
        viewer.ProcessingMode = ProcessingMode.Local
        viewer.LocalReport.ReportPath = Server.MapPath("~/Reports/FW53_RejReport.rdlc")
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

        Dim fileName As String = "FW53_RejectDetail_" + String.Format("{0:ddMMyyyy}", Date.Now) + ".pdf"


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