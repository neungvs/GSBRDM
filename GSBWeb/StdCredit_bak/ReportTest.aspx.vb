Imports Microsoft.Reporting.WebForms

Partial Class ReportTest
    Inherits System.Web.UI.Page
    Dim clsDB As New clsboldb
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadtime()
            dlSCENARIOID()
            dlsimpCom()
            dloem()
        End If
    End Sub
    Private Sub loadtime()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tbloadtime As DataTable
        tbloadtime = Nothing
        strSQL = "select timeid from STDCredit_Dimension_Report_Arrears_Status_Asset_Type where timeid != 1 group by timeid "
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlTime.DataSource = ds
        Me.DrlTime.DataBind()
        Me.DrlTime.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", " "))
        clsDB.Close()
    End Sub
    Private Sub dlSCENARIOID()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tbSCENARIOID As DataTable
        tbSCENARIOID = Nothing
        strSQL = "select SCENARIOID from [dbo].[STDCredit_Dimension_Report_Arrears_Status_Asset_Type] where timeid != 1 group by SCENARIOID "
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlScenario.DataSource = ds
        Me.DrlScenario.DataBind()
        Me.DrlScenario.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", " "))
        clsDB.Close()
    End Sub
    Private Sub dlsimpCom()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tbsimpCom As DataTable
        tbsimpCom = Nothing
        strSQL = "select SIMP_COMP from [dbo].[STDCredit_Dimension_Report_Arrears_Status_Asset_Type] where timeid != 1 group by SIMP_COMP "
        ds = clsDB.QueryDataSet(strSQL)
        Me.Drlsimcom.DataSource = ds
        Me.Drlsimcom.DataBind()
        Me.Drlsimcom.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", " "))
        clsDB.Close()
    End Sub
    Private Sub dloem()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        tboem = Nothing
        strSQL = "select OEM_CEM from [dbo].[STDCredit_Dimension_Report_Arrears_Status_Asset_Type] where timeid != 1 group by OEM_CEM "
        ds = clsDB.QueryDataSet(strSQL)
        Me.Drloem.DataSource = ds
        Me.Drloem.DataBind()
        Me.Drloem.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", " "))
        clsDB.Close()
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        clsDB.CallReport(ReportViewer2, "Report6")
        'clsDB.CallReport(ReportViewer2, "Rpt_Counter_Party_Exposure")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DrlTime.SelectedIndex = 0
        Me.DrlScenario.SelectedIndex = 0
        Me.Drlsimcom.SelectedIndex = 0
        Me.Drloem.SelectedIndex = 0
        Me.ReportViewer2.Reset()

    End Sub

    Protected Sub btnBack_Click1(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("../index.aspx")
    End Sub
End Class
