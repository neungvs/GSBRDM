Imports Microsoft.Reporting.WebForms

Partial Class _17EconomicLevel
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
        'Me.DrlTime.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        'clsDB.Close()
    End Sub
    Private Sub dlSCENARIOID()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tbSCENARIOID As DataTable
        tbSCENARIOID = Nothing
        strSQL = "select b.SCENARIODESC,a.SCENARIOID from [dbo].[STDCredit_Dimension_Report_Arrears_Status_Asset_Type] a inner join SL_SCENARIO b on a.SCENARIOID = b.SCENARIOID  where a.SIMP_COMP != '1' group by b.SCENARIODESC,a.SCENARIOID "
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlScenario.DataSource = ds
        Me.DrlScenario.DataBind()
        'Me.DrlScenario.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()
    End Sub
    Private Sub dlsimpCom()
        Dim strSQL As String
        Dim ds As DataSet
        Dim tbsimpCom As DataTable
        tbsimpCom = Nothing
        strSQL = "select SIMP_COMP from [dbo].[STDCredit_Dimension_Report_Arrears_Status_Asset_Type] where SIMP_COMP != '1' group by SIMP_COMP order by SIMP_COMP desc"
        ds = clsDB.QueryDataSet(strSQL)
        Me.Drlsimcom.DataSource = ds
        Me.Drlsimcom.DataBind()
        Me.Drlsimcom.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
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

        Panel1.Visible = True
        'clsDB.CallReport(ReportViewer2, "Rpt_Dimension_Report_Arrears_Status")

    End Sub

    Protected Sub BthAccountRefCDcSch_Click(sender As Object, e As EventArgs) Handles BthAccountRefCDcSch.Click


        Dim strSQL As String
        Dim ds As DataSet
        Dim tbsimpCom As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))

        tbsimpCom = Nothing
        strSQL = "select ACCOUNTREFCD from [dbo].[STDCredit_WS_CRM_MAP]
                where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " and ACCOUNTREFCD like '" + TxtAccountRefCDSch.Text + "%'
                Group by ACCOUNTREFCD order by ACCOUNTREFCD"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlAccountRefCD.DataSource = ds
        Me.DrlAccountRefCD.DataBind()
        clsDB.Close()


    End Sub

    Protected Sub btnOkCondition_Click(sender As Object, e As EventArgs) Handles btnOkCondition.Click

        Dim pTimeID As New ReportParameter
        Dim pScenarioID As New ReportParameter
        Dim pSimpComp As New ReportParameter
        Dim pCemOem As New ReportParameter
        Dim pAccountRefCD As New ReportParameter
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))

        pTimeID.Name = "TIMEID"
        pTimeID.Values.Add(strTimeID)
        pScenarioID.Name = "SCENARIOID"
        pScenarioID.Values.Add(DrlScenario.SelectedItem.Value)
        pSimpComp.Name = "SIMPCOMP"
        pSimpComp.Values.Add(Drlsimcom.SelectedItem.Value)
        pCemOem.Name = "OEMCEM"
        pCemOem.Values.Add(Drloem.SelectedItem.Value)
        pAccountRefCD.Name = "ACCOUNTREFCD"
        pAccountRefCD.Values.Add(DrlAccountRefCD.SelectedItem.Value)

        clsDB.CallReportPara(ReportViewer2, "Rpt_Exposure_Statement_Tranche", pTimeID, pScenarioID, pSimpComp, pCemOem, pAccountRefCD)

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DrlScenario.SelectedIndex = 0
        Me.Drlsimcom.SelectedIndex = 0
        Me.Drloem.SelectedIndex = 0
        Me.ReportViewer2.Reset()
    End Sub
End Class

