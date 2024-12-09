Imports Microsoft.Reporting.WebForms

Public Class InternalEx
    Inherits System.Web.UI.Page
    Dim clsDB As New clsboldb
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            loadtime()

        End If
        'clsDB.CallReportPara(ReportViewer2, "Rpt_Dimension_Report_Exposure_Currency", pTimeID, pScenarioID, pSimpComp, pCemOem)
    End Sub
    Private Sub loadtime()
        Dim t1 As New TimeSearch

        DrlTimeYear.DataSource = t1.GetYear()
        DrlTimeYear.DataBind()

        DrlTimeMonth.DataSource = t1.GetMonth()
        DrlTimeMonth.DataBind()
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        'clsDB.CallReport(ReportViewer2, "InternalExternal")
        Dim pCount As New ReportParameter
        Dim pTimeID As New ReportParameter
        Dim pMonth As New ReportParameter
        'Dim strTimeID As String = ""

        'Dim t1 As New TimeSearch

        'strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))

        pTimeID.Name = "YEAR"
        pTimeID.Values.Add(DrlTimeYear.SelectedItem.Value - 543)

        pMonth.Name = "Month"
        pMonth.Values.Add(DrlTimeMonth.SelectedIndex + 1)

        pCount.Name = "Unit"
        pCount.Values.Add(Drl_count.SelectedItem.Value)

        clsDB.CallReportPara(ReportViewer2, "InternalExternal", pTimeID, pCount, pMonth)
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.ReportViewer2.Reset()
    End Sub
End Class