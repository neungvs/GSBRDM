Imports Microsoft.Reporting.WebForms

Partial Class _11CouterpartyList
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
        strSQL = "select APPROACHDESC as SIMP_COMP from SL_APPROACH where APPROACHID in (30,40) order by APPROACHID "
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
        Me.Drloem.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        Me.Drloem.Items.Insert(1, New ListItem("OEM", "OEM"))
        Me.Drloem.Items.Insert(2, New ListItem("CEM", "CEM"))
        clsDB.Close()
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Panel1.Visible = True
        'clsDB.CallReport(ReportViewer2, "Rpt_Counter_Party_Exposure")
    End Sub

    Protected Sub BthObligorNameSch_Click(sender As Object, e As EventArgs) Handles BthObligorNameSch.Click

        Dim strSQL As String
        Dim ds As DataSet
        Dim tbsimpCom As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tbsimpCom = Nothing
        strSQL = "select OBLIGORNAME from [dbo].[STDCredit_WS_CRM_MAP]
                where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " and OBLIGORNAME like '" + TxtObligorNameSch.Text + "%'
                Group by OBLIGORNAME order by OBLIGORNAME"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlObligorName.DataSource = ds
        Me.DrlObligorName.DataBind()
        clsDB.Close()


    End Sub

    Protected Sub btnOkCondition_Click(sender As Object, e As EventArgs) Handles btnOkCondition.Click

        Dim pTimeID As New ReportParameter
        Dim pScenarioID As New ReportParameter
            Dim pSimpComp As New ReportParameter
            Dim pCemOem As New ReportParameter
            Dim pObligorName As New ReportParameter
            Dim strTimeID As String = ""

            Dim t1 As New TimeSearch

            strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))

            pTimeID.Name = "TIMEID"
            pTimeID.Values.Add(strTimeID)
            pScenarioID.Name = "SCENARIOID"
            pScenarioID.Values.Add(DrlScenario.SelectedItem.Value)
            pSimpComp.Name = "SIMP_COMP"
            pSimpComp.Values.Add(Drlsimcom.SelectedItem.Value)
            pCemOem.Name = "CEM_OEM"
            pCemOem.Values.Add(Drloem.SelectedItem.Value)
            pObligorName.Name = "OBLIGORNAME"
            pObligorName.Values.Add(DrlObligorName.SelectedItem.Value)

        clsDB.CallReportPara(ReportViewer2, "Rpt_Counter_Party_Exposure", pTimeID, pScenarioID, pSimpComp, pCemOem, pObligorName)

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DrlScenario.SelectedIndex = 0
        Me.Drlsimcom.SelectedIndex = 0
        Me.Drloem.SelectedIndex = 0
        Me.ReportViewer2.Reset()
    End Sub
End Class
