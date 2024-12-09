Imports Microsoft.Reporting.WebForms
Imports System.Web.UI.WebControls

Partial Class _16ExposureSTDcomp
    Inherits System.Web.UI.Page
    Dim clsDB As New clsboldb

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadtime()
            dlSCENARIOID()

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


    Protected Sub BtnPercentilesSelect_Click(sender As Object, e As EventArgs) Handles BtnPercentilesSelect.Click

        Dim message As String = ""
        Dim cItem As Integer = 0



        For Each item As ListItem In lstPercentiles.Items
            If item.Selected Then
                If cItem = 0 Then
                    message = Replace(item.Text, "%", "")
                Else
                    message = message + "," + Replace(item.Text, "%", "")
                End If
                cItem = cItem + 1
            End If
        Next

        If cItem = 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('กรุณาเลือก Percentiles');", True)
            Exit Sub
        End If

        Dim pTimeID As New ReportParameter
        Dim pScenarioID As New ReportParameter
        Dim pCondition As New ReportParameter
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))

        pTimeID.Name = "TIMEID"
        pTimeID.Values.Add(strTimeID)
        pScenarioID.Name = "SCENARIOID"
        pScenarioID.Values.Add(DrlScenario.SelectedItem.Value)
        pCondition.Name = "CONDITION"
        pCondition.Values.Add(message)

        clsDB.CallReportPara(ReportViewer2, "Rpt_Economic_Capital_by_Confidence_Levels", pTimeID, pScenarioID, pCondition)

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        Me.DrlScenario.SelectedIndex = 0
        Me.ReportViewer2.Reset()

        For Each a As ListItem In Me.lstPercentiles.Items
            If a.Selected Then a.Selected = False
        Next



    End Sub


End Class

