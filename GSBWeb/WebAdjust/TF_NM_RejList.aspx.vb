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


Public Class TF_NM_RejList

    Inherits System.Web.UI.Page

    Dim tfBiz As New TF_NMBiz

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

        gvTF_NM.DataSource = tfBiz.GetDataTF_NM()
        gvTF_NM.DataBind()

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        If Not String.IsNullOrEmpty(txtPositionId.Text) Then

            Dim result As Boolean = False

            result = UtilsBiz.checkForSQLInjection(txtPositionId.Text.Trim())

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
        Session("AccNo") = txtPositionId.Text
        Session("Name") = txtCifName.Text
        Session("SurName") = txtCifSurName.Text
        gvTF_NM.PageIndex = 0

        SearchData()


    End Sub

    Private Sub SearchData()

        gvTF_NM.DataSource = tfBiz.GetTF_NM_ByCriteria(Convert.ToString(Session("CifNum")), Convert.ToString(Session("AccNo")), Convert.ToString(Session("Name")), Convert.ToString(Session("SurName")))
        gvTF_NM.DataBind()

    End Sub



    Protected Sub gvTF_NM_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTF_NM.PageIndexChanging

        gvTF_NM.PageIndex = e.NewPageIndex

        If String.IsNullOrEmpty(Session("CifNum")) And String.IsNullOrEmpty(Session("AccNo")) And String.IsNullOrEmpty(Session("Name")) And String.IsNullOrEmpty(Session("SurName")) Then
            LoadData()
        Else
            SearchData()
        End If

    End Sub


    Protected Sub gvTF_NM_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTF_NM.RowDataBound

        Dim hpPositionId As LinkButton = CType(e.Row.FindControl("hpPositionId"), LinkButton)
        Dim HF_TMP_TF_NMID As HiddenField = CType(e.Row.FindControl("HF_TMP_TF_NMID"), HiddenField)
        Dim HF_AS_OF_DATE As HiddenField = CType(e.Row.FindControl("HF_AS_OF_DATE"), HiddenField)

        Dim lblCifNumber As Label = CType(e.Row.FindControl("lblCifNumber"), Label)

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            hpPositionId.Text = DataBinder.Eval(e.Row.DataItem, "PositionID").ToString()
            HF_TMP_TF_NMID.Value = DataBinder.Eval(e.Row.DataItem, "TMP_TF_NMID").ToString()
            HF_AS_OF_DATE.Value = DataBinder.Eval(e.Row.DataItem, "AS_OF_DATE").ToString()

        End If

    End Sub



    Protected Sub gvTF_NM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTF_NM.SelectedIndexChanged


        Dim lblCifNumber As Label = CType(gvTF_NM.SelectedRow.FindControl("lblCifNumber"), Label)
        Dim hpPositionId As LinkButton = CType(gvTF_NM.SelectedRow.FindControl("hpPositionId"), LinkButton)
        Dim HF_TMP_TF_NMID As HiddenField = CType(gvTF_NM.SelectedRow.FindControl("HF_TMP_TF_NMID"), HiddenField)

        ClearSession()
        Session("CifNumber") = lblCifNumber.Text
        Session("ACCNO") = hpPositionId.Text
        Session("TMPID") = HF_TMP_TF_NMID.Value

        Response.Redirect("TF_NM_RejEdit.aspx")

    End Sub


    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click

        Dim rds As New ReportDataSource
        rds.Name = "DataSet1"

        Dim lsRej As New List(Of RejListReportEntity)

        lsRej = tfBiz.GetRejData_Report()


        For Each item As RejListReportEntity In lsRej


            If item.ColumnName = "GL_ACCOUNT" Then

                item.ColumnName = "รหัสบัญชี (GL_ACCOUNT)"

            ElseIf item.ColumnName = "StartDate" Then
                item.ColumnName = "วันจ่ายเงินกู้ (StartDate)"

            ElseIf item.ColumnName = "EndDate" Then
                item.ColumnName = "วันสิ้นสุดสัญญา (EndDate)"

            ElseIf item.ColumnName = "Currency_Original" Then
                item.ColumnName = "สกุลเงิน (Currency_Original)"

            ElseIf item.ColumnName = "ASSET_CLASS_TYPE" Then
                item.ColumnName = "ระดับการจัดชั้นหนี้ตามยอดค้างชำระ (ASSET_CLASS_TYPE)"

            End If

        Next

        rds.Value = lsRej



        Dim viewer As New ReportViewer
        viewer.ProcessingMode = ProcessingMode.Local
        viewer.LocalReport.ReportPath = Server.MapPath("~/Reports/TF_NM_RejReport.rdlc")
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

        Dim fileName As String = "TF_NM_RejDetail_" + String.Format("{0:ddMMyyyy}", Date.Now) + ".pdf"


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