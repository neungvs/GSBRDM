Imports Microsoft.Reporting.WebForms

Partial Class _19RiskTree
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

        Dim t1 As New TimeSearch

        DrlTimeYear.DataSource = t1.GetYear()
        DrlTimeYear.DataBind()

        DrlTimeMonth.DataSource = t1.GetMonth()
        DrlTimeMonth.DataBind()

        'Dim strSQL As String
        'Dim ds As DataSet
        'Dim tbloadtime As DataTable
        'tbloadtime = Nothing
        'strSQL = "select timeid from STDCredit_Dimension_Report_Arrears_Status_Asset_Type where timeid != 1 group by timeid "
        'ds = clsDB.QueryDataSet(strSQL)
        'Me.DrlTime.DataSource = ds
        'Me.DrlTime.DataBind()
        'Me.DrlTime.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", " "))
        'clsDB.Close()
    End Sub
    Private Sub dlSCENARIOID()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tbSCENARIOID As DataTable
        tbSCENARIOID = Nothing
        strSQL = "select SCENARIODESC,SCENARIOID from SL_SCENARIO b group by SCENARIODESC,SCENARIOID "
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlScenario.DataSource = ds
        Me.DrlScenario.DataBind()
        'Me.DrlScenario.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", " "))
        clsDB.Close()
    End Sub
    Private Sub dlsimpCom()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tbsimpCom As DataTable
        tbsimpCom = Nothing
        strSQL = "select APPROACHDESC  as SIMP_COMP from SL_APPROACH where APPROACHID in (30,40) order by APPROACHID "
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
        strSQL = "select OEM_CEM from [dbo].[STDCredit_Dimension_Report_Arrears_Status_Asset_Type] where SIMP_COMP != '1' and OEM_CEM is not null group by OEM_CEM  "
        ds = clsDB.QueryDataSet(strSQL)
        Me.Drloem.DataSource = ds
        Me.Drloem.DataBind()
        Me.Drloem.Items.Insert(0, New ListItem("OEM", "OEM"))
        Me.Drloem.Items.Insert(1, New ListItem("CEM", "CEM"))
        clsDB.Close()
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        'clsDB.CallReport(ReportViewer2, "Rpt_Dimension_Report_Arrears_Status")
        Dim pTimeID As New ReportParameter
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))

        pTimeID.Name = "TIMEID"
        pTimeID.Values.Add(strTimeID)

        Dim paraScenarioId As New ReportParameter()
        paraScenarioId.Name = "SCENARIOID"
        paraScenarioId.Values.Add(DrlScenario.SelectedValue)

        Dim paraSimpComp As New ReportParameter()
        paraSimpComp.Name = "SIMPCOMP"
        paraSimpComp.Values.Add(Drlsimcom.SelectedValue)

        Dim paraOemCem As New ReportParameter()
        paraOemCem.Name = "OEMCEM"
        paraOemCem.Values.Add(Drloem.SelectedValue)

        clsDB.CallReportPara(ReportViewer2, "Rpt_Risk_Tree_Overview", pTimeID, paraScenarioId, paraSimpComp, paraOemCem)
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DrlScenario.SelectedIndex = 0
        Me.Drlsimcom.SelectedIndex = 0
        Me.Drloem.SelectedIndex = 0
        Me.ReportViewer2.Reset()
    End Sub
End Class

