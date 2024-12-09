Imports Microsoft.Reporting.WebForms

Partial Class _10DimensionSapproach
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

    Private Sub DlAssetType()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tboem = Nothing
        strSQL = "
            select AssetTypeDesc from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by AssetTypeDesc order by AssetTypeDesc"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlAssetType.DataSource = ds
        Me.DrlAssetType.DataBind()
        Me.DrlAssetType.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Private Sub DlCRMType()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))

        tboem = Nothing
        strSQL = "
            select CRMTYPEDESC from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by CRMTYPEDESC order by CRMTYPEDESC"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlCRMType.DataSource = ds
        Me.DrlCRMType.DataBind()
        Me.DrlCRMType.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Private Sub DlObligorType()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tboem = Nothing
        strSQL = "
            select MARKETPARTICIPANTTYPEDESC from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by MARKETPARTICIPANTTYPEDESC order by MARKETPARTICIPANTTYPEDESC"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlObligorType.DataSource = ds
        Me.DrlObligorType.DataBind()
        Me.DrlObligorType.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Private Sub DlArrearsStatus()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tboem = Nothing
        strSQL = "
            select ARREARSSTATUSDESC from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by ARREARSSTATUSDESC order by ARREARSSTATUSDESC"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlArrearsStatus.DataSource = ds
        Me.DrlArrearsStatus.DataBind()
        Me.DrlArrearsStatus.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Private Sub DlExternalCreditRating()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tboem = Nothing
        strSQL = "
            select EXTERNALCREDITRATINGTYPEDESC from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by EXTERNALCREDITRATINGTYPEDESC order by EXTERNALCREDITRATINGTYPEDESC"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlExternalCreditRating.DataSource = ds
        Me.DrlExternalCreditRating.DataBind()
        Me.DrlExternalCreditRating.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Private Sub DlSpecifiedApproach()

        'Dim strSQL As String
        'Dim ds As DataSet
        'Dim tboem As DataTable
        'Dim strTimeID As String = ""

        'Dim t1 As New TimeSearch

        'strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        'tboem = Nothing
        'strSQL = "
        '    select APPROACHDESC from [dbo].[STDCredit_WS_CRM_MAP]
        '    where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by APPROACHDESC order by APPROACHDESC"
        'ds = clsDB.QueryDataSet(strSQL)
        'Me.DrlSpecifiedApproach.DataSource = ds
        'Me.DrlSpecifiedApproach.DataBind()
        'Me.DrlSpecifiedApproach.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        'clsDB.Close()

    End Sub

    Private Sub DlExposureCurrency()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tboem = Nothing
        strSQL = "
            select CURRENCYDESC from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by CURRENCYDESC order by CURRENCYDESC"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlExposureCurrency.DataSource = ds
        Me.DrlExposureCurrency.DataBind()
        Me.DrlExposureCurrency.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Private Sub DlCountryOfOrigin()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tboem = Nothing
        strSQL = "
            select COUNTRYDESC from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by COUNTRYDESC order by COUNTRYDESC"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlCountryOfOrigin.DataSource = ds
        Me.DrlCountryOfOrigin.DataBind()
        Me.DrlCountryOfOrigin.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Private Sub DlObligorIndustryGroup()

        Dim strSQL As String
        Dim ds As DataSet
        Dim tboem As DataTable
        Dim strTimeID As String = ""

        Dim t1 As New TimeSearch

        strTimeID = t1.GetTimeID(DrlTimeYear.SelectedValue.ToString, Format((DrlTimeMonth.SelectedIndex + 1), "00"))
        tboem = Nothing
        strSQL = "
            select INDUSTRYGROUPDESC from [dbo].[STDCredit_WS_CRM_MAP]
            where timeid = " + strTimeID + " and  SCENARIOID = " + DrlScenario.SelectedItem.Value + " Group by INDUSTRYGROUPDESC order by INDUSTRYGROUPDESC"
        ds = clsDB.QueryDataSet(strSQL)
        Me.DrlObligorIndustryGroup.DataSource = ds
        Me.DrlObligorIndustryGroup.DataBind()
        Me.DrlObligorIndustryGroup.Items.Insert(0, New ListItem("--เลือกทั้งหมด--", "0"))
        clsDB.Close()

    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click

        Panel1.Visible = True

        DlAssetType()
        DlCRMType()
        DlObligorType()
        DlArrearsStatus()
        DlExternalCreditRating()
        DlSpecifiedApproach()
        DlExposureCurrency()
        DlCountryOfOrigin()
        DlObligorIndustryGroup()

    End Sub

    Protected Sub btnOkCondition_Click(sender As Object, e As EventArgs) Handles btnOkCondition.Click

        Dim pTimeID As New ReportParameter
        Dim pScenarioID As New ReportParameter
        Dim pSimpComp As New ReportParameter
        Dim pCemOem As New ReportParameter
        Dim pTop As New ReportParameter
        Dim pCondition As New ReportParameter
        Dim pSort As New ReportParameter
        Dim pNameSearch As New ReportParameter
        Dim strCondition As String = ""
        Dim nameCondition As String = ""
        Dim typeReport As String = ""

        'typeReport = HdfTypeReport.Value
        If rdoCounterparties.Checked Then
            typeReport = rdoCounterparties.Text
        ElseIf rdoExposure.Checked Then
            typeReport = rdoExposure.Text
        End If

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
        pTop.Name = "TOP"
        pTop.Values.Add(DrlTop.SelectedItem.Value)
        pSort.Name = "SORT"
        pSort.Values.Add("[" + DrlSort.SelectedItem.Value + "]")

        strCondition = " "

        If DrlAssetType.SelectedIndex > 0 Then

            strCondition = strCondition + " and AssetTypeDesc = '" + DrlAssetType.SelectedValue + "' "

        End If

        If DrlCRMType.SelectedIndex > 0 Then

            strCondition = strCondition + " and CRMTYPEDESC = '" + DrlCRMType.SelectedValue + "' "

        End If

        If DrlObligorType.SelectedIndex > 0 Then

            strCondition = strCondition + " and MARKETPARTICIPANTTYPEDESC = '" + DrlObligorType.SelectedValue + "' "

        End If

        If DrlArrearsStatus.SelectedIndex > 0 Then

            strCondition = strCondition + " and ARREARSSTATUSDESC = '" + DrlArrearsStatus.SelectedValue + "' "

        End If

        If DrlExternalCreditRating.SelectedIndex > 0 Then

            strCondition = strCondition + " and EXTERNALCREDITRATINGTYPEDESC = '" + DrlExternalCreditRating.SelectedValue + "' "

        End If


        'If DrlSpecifiedApproach.SelectedIndex > 0 Then

        '    strCondition = strCondition + " and APPROACHDESC = '" + DrlSpecifiedApproach.SelectedValue + "' "

        'End If


        If DrlExposureCurrency.SelectedIndex > 0 Then

            strCondition = strCondition + " and CURRENCYDESC = '" + DrlExposureCurrency.SelectedValue + "' "

        End If


        If DrlCountryOfOrigin.SelectedIndex > 0 Then

            strCondition = strCondition + " and COUNTRYDESC = '" + DrlCountryOfOrigin.SelectedValue + "' "

        End If


        If DrlObligorIndustryGroup.SelectedIndex > 0 Then

            strCondition = strCondition + " and INDUSTRYGROUPDESC = '" + DrlObligorIndustryGroup.SelectedValue + "' "

        End If

        pCondition.Name = "CONDITION"
        pCondition.Values.Add(strCondition)


        nameCondition = " "

        If typeReport = "Counterparties" Then

            If (Trim(TxtBegin.Text) <> "" Or Not String.IsNullOrEmpty(TxtBegin.Text)) And (Trim(TxtContain.Text) <> "" Or Not String.IsNullOrEmpty(TxtContain.Text)) Then
                nameCondition = " and (OBLIGORNAME like '" + TxtBegin.Text + "%' or OBLIGORNAME like '%" + TxtContain.Text + "%' ) "
            ElseIf Trim(TxtBegin.Text) <> "" Then
                nameCondition = " and (OBLIGORNAME like '" + TxtBegin.Text + "%' ) "
            ElseIf Trim(TxtContain.Text) <> "" Then
                nameCondition = " and (OBLIGORNAME like '%" + TxtContain.Text + "%' ) "
            End If

            pNameSearch.Name = "NAMESEARCH"
            pNameSearch.Values.Add(nameCondition)


            clsDB.CallReportPara(ReportViewer2, "Rpt_Counter_Party_List_Main", pTimeID, pScenarioID, pSimpComp, pCemOem, pTop, pCondition, pSort, pNameSearch)
        ElseIf typeReport = "Exposure" Then

            If (Trim(TxtBegin.Text) <> "" Or Not String.IsNullOrEmpty(TxtBegin.Text)) And (Trim(TxtContain.Text) <> "" Or Not String.IsNullOrEmpty(TxtContain.Text)) Then
                nameCondition = " and (ACCOUNTREFCD like '" + TxtBegin.Text + "%' or ACCOUNTREFCD like '%" + TxtContain.Text + "%' ) "
            ElseIf Trim(TxtBegin.Text) <> "" Then
                nameCondition = " and (ACCOUNTREFCD like '" + TxtBegin.Text + "%' ) "
            ElseIf Trim(TxtContain.Text) <> "" Then
                nameCondition = " and (ACCOUNTREFCD like '%" + TxtContain.Text + "%' ) "
            End If

            pNameSearch.Name = "NAMESEARCH"
            pNameSearch.Values.Add(nameCondition)

            clsDB.CallReportPara(ReportViewer2, "Rpt_Counter_Party_List_Exposure", pTimeID, pScenarioID, pSimpComp, pCemOem, pTop, pCondition, pSort, pNameSearch)
        End If

    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DrlScenario.SelectedIndex = 0
        Me.Drlsimcom.SelectedIndex = 0
        Me.Drloem.SelectedIndex = 0
        Me.ReportViewer2.Reset()

    End Sub


End Class

