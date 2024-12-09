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
Imports System.Web.Services
Imports Arsoft.Utility


Public Class EffectiveDateDetail_Add
    Inherits System.Web.UI.Page
    Dim lmBiz As New IndustryBiz
    Dim DtIndustryLimi As DataTable
    Dim DtLntypecode As DataTable
    Dim DtLnsubtype As DataTable
    Dim DtLnmktcode As DataTable
    Dim gui As New Guid
    Shared detailList As New List(Of IndustyDetailEntity)
    Shared IndustyList As New List(Of IndustyDetailEntity)
    Shared entDetail As New IndustyDetailEntity

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetControl()
            Dim tlist = TryCast(CObj(Session("DetailList")), List(Of IndustyDetailEntity))
            detailList = tlist
        End If
    End Sub

    Private Sub SetControl()
        rdbIssicType.Checked = True
        GetDDL_ISICCODE()
        GetDDL_ISICCODESUBLEVEL(ddlISICCODE.SelectedItem.ToString())
        ddlLNSUBTYPE.Items.Clear()
        ddlLNTYPECODE.Items.Clear()
        ddlMKTCODE.Items.Clear()
        ddlLNSUBTYPE.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        ddlLNTYPECODE.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        ddlMKTCODE.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        ddlISICCODE.BackColor = Drawing.Color.White
        ddlISICSUB.BackColor = Drawing.Color.White
        btnLoandTypeAdd.Visible = False
        btnIsicTypeAdd.Visible = True
        lblLoanType.Text = ""
        lblIndustry.Text = ""
        trLoanType.Visible = False
        trLNSUBTYPE.Visible = False
        trLNTYPECODE.Visible = False
        trMKT_CODE.Visible = False
    End Sub

    Protected Sub ddlISICCODE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlISICCODE.SelectedIndexChanged
        lblIndustry.Text = ""
        GetDDL_ISICCODESUBLEVEL(ddlISICCODE.SelectedItem.Text)

        If ddlISICCODE.SelectedIndex <> 0 Then
            GetDDL_ISICCODESUBLEVEL(ddlISICCODE.SelectedItem.Text)
            Dim Dt_HeightLevel As New DataTable()
            Dt_HeightLevel = lmBiz.Check_HeightLevel(ddlISICCODE.SelectedItem.Text)
            If (Dt_HeightLevel.Rows.Count > 0) Then
                DtIndustryLimi = New DataTable()
                DtIndustryLimi = lmBiz.Load_Industry(Dt_HeightLevel.Rows(0)(0).ToString())
                If (DtIndustryLimi.Rows.Count > 0) Then
                    lblIndustry.Text = DtIndustryLimi.Rows(0)("CBT_DESC_LEV1").ToString()
                End If
            End If
        End If

    End Sub

    Protected Sub ddlISICSUB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlISICSUB.SelectedIndexChanged
        If ddlISICSUB.SelectedIndex <> 0 Then
            DtIndustryLimi = New DataTable()
            DtIndustryLimi = lmBiz.Load_Industry(ddlISICSUB.SelectedValue)
            If (DtIndustryLimi.Rows.Count > 0) Then
                lblIndustry.Text = DtIndustryLimi.Rows(0)("CBT_DESC_LEV1").ToString()
            End If
        Else
            Dim Dt_HeightLevel As New DataTable()
            Dt_HeightLevel = lmBiz.Check_HeightLevel(ddlISICCODE.SelectedItem.Text)
            If (Dt_HeightLevel.Rows.Count > 0) Then
                DtIndustryLimi = New DataTable()
                DtIndustryLimi = lmBiz.Load_Industry(Dt_HeightLevel.Rows(0)(0).ToString())
                If (DtIndustryLimi.Rows.Count > 0) Then
                    lblIndustry.Text = DtIndustryLimi.Rows(0)("CBT_DESC_LEV1").ToString()
                End If
            End If
        End If
    End Sub

    Private Sub GetDDL_ISICCODE()
        Dim dt As DataTable = lmBiz.LoadDDL_ISICCODE()
        If dt.Rows.Count > 0 Then
            ddlISICCODE.Items.Clear()
            ddlISICCODE.DataSource = dt
            ddlISICCODE.DataValueField = "Row"
            ddlISICCODE.DataTextField = "ISICCODE"
            ddlISICCODE.DataBind()
            ddlISICCODE.Items.Insert(0, New ListItem("<---ISICCODE--->", "0"))
        Else
            ddlISICCODE.Items.Clear()
            ddlISICCODE.Items.Insert(0, New ListItem("<---ISICCODE--->", "0"))
        End If
    End Sub

    Private Sub GetDDL_ISICCODESUBLEVEL(ByVal ISICCODE As String)
        Dim dt As DataTable = lmBiz.LoadDDL_ISICCODESUBLEVEL(ISICCODE)

        If dt.Rows.Count > 0 Then
            ddlISICSUB.Items.Clear()
            ddlISICSUB.DataSource = dt
            ddlISICSUB.DataValueField = "ISICCODESUBLEVEL"
            ddlISICSUB.DataTextField = "ISICCODESUBLEVEL"
            ddlISICSUB.DataBind()
            ddlISICSUB.Items.Insert(0, New ListItem("<---ISICCODESUBLEVEL--->", "0"))
        Else
            ddlISICSUB.Items.Clear()
            ddlISICSUB.Items.Insert(0, New ListItem("<---ISICCODESUBLEVEL--->", "0"))
        End If
    End Sub

    Private Sub GetDDL_LNTYPECODE()
        Dim dt As DataTable = lmBiz.LoadDDL_LNTYPECODE()

        If dt.Rows.Count > 0 Then
            ddlLNTYPECODE.Items.Clear()
            ddlLNTYPECODE.DataSource = dt
            ddlLNTYPECODE.DataValueField = "TYPE_DESC"
            ddlLNTYPECODE.DataTextField = "LN_TYPE_CODE"
            ddlLNTYPECODE.DataBind()
            ddlLNTYPECODE.Items.Insert(0, New ListItem("<---LN_TYPE_CODE--->", "0"))
        Else
            ddlLNTYPECODE.Items.Clear()
            ddlLNTYPECODE.Items.Insert(0, New ListItem("<---LN_TYPE_CODE--->", "0"))
        End If
    End Sub

    Private Sub GetDDL_LNSUBTYPE(ByVal LNTYPECODE As String)
        Dim dt As DataTable

        If LNTYPECODE <> "" Then
            dt = lmBiz.GetLnsubtypeByLntypecode(LNTYPECODE)
        Else
            dt = lmBiz.LoadDDL_LNSUBTYPE()
        End If

        If dt.Rows.Count > 0 Then
            ddlLNSUBTYPE.Items.Clear()
            ddlLNSUBTYPE.DataSource = dt
            ddlLNSUBTYPE.DataValueField = "SUBDESC"
            ddlLNSUBTYPE.DataTextField = "LNSUBTYPEID"
            ddlLNSUBTYPE.DataBind()
            ddlLNSUBTYPE.Items.Insert(0, New ListItem("<---LNSUBTYPE--->", "0"))
        Else
            ddlLNSUBTYPE.Items.Clear()
            ddlLNSUBTYPE.Items.Insert(0, New ListItem("<---LNSUBTYPE--->", "0"))
        End If
    End Sub

    Private Sub GetDDL_LNMKTCODE(ByVal LNSUBTYPE As String)
        Dim dt As DataTable

        If LNSUBTYPE <> "" Then
            dt = lmBiz.GetLnmktcodeByLnsubtype(LNSUBTYPE)
        Else
            dt = lmBiz.LoadDDL_LNMKTCODE()
        End If

        If dt.Rows.Count > 0 Then
            ddlMKTCODE.Items.Clear()
            ddlMKTCODE.DataSource = dt
            ddlMKTCODE.DataValueField = "MKT_CODE"
            ddlMKTCODE.DataTextField = "LN_MKT_CODE"
            ddlMKTCODE.DataBind()
            ddlMKTCODE.Items.Insert(0, New ListItem("<---LNMKTCODE--->", "0"))
        Else
            ddlMKTCODE.Items.Clear()
            ddlMKTCODE.Items.Insert(0, New ListItem("<---LNMKTCODE--->", "0"))
        End If
    End Sub

    Private Sub GetDDL_LNMKTCODEBYLNTYPECODE(ByVal LNTYPECODE As String)
        Dim dt As DataTable

        dt = lmBiz.GetLnmktcodeByLntypecode(LNTYPECODE)

        If dt.Rows.Count > 0 Then
            ddlMKTCODE.Items.Clear()
            ddlMKTCODE.DataSource = dt
            ddlMKTCODE.DataValueField = "MKT_CODE"
            ddlMKTCODE.DataTextField = "LN_MKT_CODE"
            ddlMKTCODE.DataBind()
            ddlMKTCODE.Items.Insert(0, New ListItem("<---LNMKTCODE--->", "0"))
        Else
            ddlMKTCODE.Items.Clear()
            ddlMKTCODE.Items.Insert(0, New ListItem("<---LNMKTCODE--->", "0"))
        End If
    End Sub

    Protected Sub txtIndustryLimitPercentage_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryLimitPercentage.TextChanged
        If String.IsNullOrEmpty(txtIndustryLimitPercentage.Text) = True Then
            txtIndustryLimitAmount.Enabled = True
        Else
            txtIndustryLimitAmount.Enabled = False
        End If
    End Sub

    Protected Sub rdbIssicType_CheckedChanged(sender As Object, e As EventArgs) Handles rdbIssicType.CheckedChanged
        ClearDetail()
        ddlLNSUBTYPE.Items.Clear()
        ddlLNTYPECODE.Items.Clear()
        ddlMKTCODE.Items.Clear()
        ddlLNSUBTYPE.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        ddlLNTYPECODE.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        ddlMKTCODE.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        ddlISICCODE.BackColor = Drawing.Color.White
        ddlISICSUB.BackColor = Drawing.Color.White
        GetDDL_ISICCODE()
        GetDDL_ISICCODESUBLEVEL(ddlISICCODE.SelectedItem.ToString())
        lblLoanType.Text = ""
        btnLoandTypeAdd.Visible = False
        btnIsicTypeAdd.Visible = True
        trLoanType.Visible = False
        trLNSUBTYPE.Visible = False
        trLNTYPECODE.Visible = False
        trMKT_CODE.Visible = False
        trIsiccode.Visible = True
        trIsicsub.Visible = True
        trIndustry.Visible = True
    End Sub

    Protected Sub rdbLoanType_CheckedChanged(sender As Object, e As EventArgs) Handles rdbLoanType.CheckedChanged
        ClearDetail()
        ddlISICCODE.Items.Clear()
        ddlISICSUB.Items.Clear()
        GetDDL_LNTYPECODE()
        GetDDL_LNSUBTYPE("")
        GetDDL_LNMKTCODE("")
        ddlLNSUBTYPE.BackColor = Drawing.Color.White
        ddlLNTYPECODE.BackColor = Drawing.Color.White
        ddlMKTCODE.BackColor = Drawing.Color.White
        ddlISICCODE.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        ddlISICSUB.BackColor = System.Drawing.ColorTranslator.FromHtml("#EBEBE4")
        lblIndustry.Text = ""
        btnLoandTypeAdd.Visible = True
        btnIsicTypeAdd.Visible = False
        trLoanType.Visible = True
        trLNSUBTYPE.Visible = True
        trLNTYPECODE.Visible = True
        trMKT_CODE.Visible = True
        trIsiccode.Visible = False
        trIsicsub.Visible = False
        trIndustry.Visible = False
    End Sub

    Protected Sub ddlLNTYPECODE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLNTYPECODE.SelectedIndexChanged
        If ddlLNTYPECODE.SelectedIndex <> 0 Then
            DtLntypecode = New DataTable()
            DtLntypecode = lmBiz.Load_Lntypecode(ddlLNTYPECODE.SelectedItem.Text)

            If (DtLntypecode.Rows.Count > 0) Then
                lblLoanType.Text = DtLntypecode.Rows(0)("TYPE_DESC").ToString()
            End If

            GetDDL_LNSUBTYPE(ddlLNTYPECODE.SelectedItem.Text)
            ddlMKTCODE.Items.Clear()
            ddlMKTCODE.Items.Insert(0, New ListItem("<---LNMKTCODE--->", "0"))
        Else
            GetDDL_LNSUBTYPE("")
            GetDDL_LNMKTCODE("")
            lblLoanType.Text = ""
        End If
    End Sub


    Protected Sub ddlLNSUBTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLNSUBTYPE.SelectedIndexChanged
        If ddlLNSUBTYPE.SelectedIndex <> 0 Then
            DtLnsubtype = New DataTable()
            DtLnsubtype = lmBiz.Load_Lnsubtype(ddlLNSUBTYPE.SelectedItem.Text)

            If (DtLnsubtype.Rows.Count > 0) Then
                lblLoanType.Text = DtLnsubtype.Rows(0)("SUBDESC").ToString()
            End If

            GetDDL_LNMKTCODE(ddlLNSUBTYPE.SelectedItem.Text)
        Else
            If ddlLNTYPECODE.SelectedIndex <> 0 Then
                GetDDL_LNSUBTYPE(ddlLNTYPECODE.SelectedItem.Text)
            Else
                GetDDL_LNSUBTYPE("")
            End If

            If ddlLNTYPECODE.SelectedIndex <> 0 Then
                GetDDL_LNMKTCODE(ddlLNSUBTYPE.SelectedItem.Text)
            Else
                GetDDL_LNMKTCODE("")
            End If

            If ddlLNTYPECODE.SelectedIndex <> 0 Then
                DtLntypecode = New DataTable()
                DtLntypecode = lmBiz.Load_Lntypecode(ddlLNTYPECODE.SelectedItem.Text)
                If (DtLntypecode.Rows.Count > 0) Then
                    lblLoanType.Text = DtLntypecode.Rows(0)("TYPE_DESC").ToString()
                End If
            End If

            If ddlLNTYPECODE.SelectedIndex = 0 And ddlLNSUBTYPE.SelectedIndex = 0 And ddlMKTCODE.SelectedIndex = 0 Then
                lblLoanType.Text = ""
            End If
        End If
    End Sub

    Protected Sub ddlMKTCODE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMKTCODE.SelectedIndexChanged

        If ddlMKTCODE.SelectedIndex <> 0 Then
            DtLnmktcode = New DataTable()
            DtLnmktcode = lmBiz.Load_Lnmktcode(ddlMKTCODE.SelectedItem.Text)
            If (DtLnmktcode.Rows.Count > 0) Then
                lblLoanType.Text = DtLnmktcode.Rows(0)("MKT_CODE").ToString()
            End If
        Else
            If ddlLNSUBTYPE.SelectedIndex <> 0 Then
                DtLnsubtype = New DataTable()
                DtLnsubtype = lmBiz.Load_Lnsubtype(ddlLNSUBTYPE.SelectedItem.Text)
                If (DtLnsubtype.Rows.Count > 0) Then
                    lblLoanType.Text = DtLnsubtype.Rows(0)("SUBDESC").ToString()
                End If
                GetDDL_LNMKTCODE(ddlLNSUBTYPE.SelectedItem.Text)
            Else
                If ddlLNTYPECODE.SelectedIndex <> 0 Then
                    DtLntypecode = New DataTable()
                    DtLntypecode = lmBiz.Load_Lntypecode(ddlLNTYPECODE.SelectedItem.Text)
                    If (DtLntypecode.Rows.Count > 0) Then
                        lblLoanType.Text = DtLntypecode.Rows(0)("TYPE_DESC").ToString()
                    End If
                    GetDDL_LNMKTCODEBYLNTYPECODE(ddlLNTYPECODE.SelectedItem.Text)
                End If
            End If

            If ddlLNTYPECODE.SelectedIndex = 0 And ddlLNSUBTYPE.SelectedIndex = 0 And ddlMKTCODE.SelectedIndex = 0 Then
                lblLoanType.Text = ""
            End If
        End If
    End Sub



    Protected Sub btnIsicTypeAdd_Click(sender As Object, e As EventArgs) Handles btnIsicTypeAdd.Click

        Try
    
            Dim value As String
            Dim chkDup As Boolean
            chkDup = True

            'Check EmptryData
            If lblIndustry.Text = "" Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาเลือกข้อมูลภาคธุรกิจ", "", "ปิด", False, True)
                Return
            End If

            If ddlISICSUB.SelectedIndex <> 0 Then
                value = ddlISICSUB.SelectedItem.ToString()
            Else
                value = ddlISICCODE.SelectedItem.ToString()
            End If

            'checkDuplicateData
            chkDup = CheckDupDataIsic(value)
            If chkDup = False Then
                MessageBoxAlert("แจ้งเตือน", "ไม่สามารถบันทึกข้อมูลได้เนื่องมีข้อมูลภาคธุรกิจอยู่แล้ว", "", "ปิด", False, True)
                Return
            End If

            If ddlISICSUB.SelectedIndex <> 0 Then
                If lblIsiccodesubDetail.Text = "" Then
                    lblIsiccodesubDetail.Text = value
                Else
                    lblIsiccodesubDetail.Text = lblIsiccodesubDetail.Text + "," + value
                End If
            Else
                If ddlISICCODE.SelectedIndex <> 0 Then
                    If lblIsiccodeDetail.Text = "" Then
                        lblIsiccodeDetail.Text = value
                    Else
                        lblIsiccodeDetail.Text = lblIsiccodeDetail.Text + "," + value
                    End If
                End If
            End If

            If lblIndustryDetail.Text = "" Then
                lblIndustryDetail.Text = lblIndustry.Text
            Else
                lblIndustryDetail.Text = lblIndustryDetail.Text + "," + lblIndustry.Text
            End If

            hdfType.Value = 1
            lblIndustry.Text = ""

            GetDDL_ISICCODE()
            GetDDL_ISICCODESUBLEVEL(ddlISICCODE.SelectedItem.ToString())

            'Insert Data Detail
            entDetail.Industry = lblIndustryDetail.Text
            entDetail.ISICCODE = lblIsiccodeDetail.Text
            entDetail.ISICCODESUBLEVEL = lblIsiccodesubDetail.Text
            entDetail.Type = hdfType.Value
            entDetail.IndustryLimitAmount = txtIndustryLimitAmount.Text
            entDetail.IndustryLimitPercentage = txtIndustryLimitPercentage.Text

        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("EffectiveDateDetail_Add", "IsAuthen()", ex.Message)
        End Try
    End Sub

    Protected Sub btnLoandTypeAdd_Click(sender As Object, e As EventArgs) Handles btnLoandTypeAdd.Click
        Try
            Dim value As String
            Dim chkDup As Boolean
            chkDup = True

            'Check EmptryData
            If lblLoanType.Text = "" Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาเลือกข้อมูลประเภทสินเชื่อ", "", "ปิด", False, True)
                Return
            End If

            If ddlMKTCODE.SelectedIndex <> 0 Then
                value = ddlMKTCODE.SelectedItem.ToString()
            ElseIf ddlLNSUBTYPE.SelectedIndex <> 0 Then
                value = ddlLNSUBTYPE.SelectedItem.ToString()
            ElseIf ddlLNTYPECODE.SelectedIndex <> 0 Then
                value = ddlLNTYPECODE.SelectedItem.ToString()
            End If

            'checkDuplicateData
            chkDup = CheckDupDataLoanType(value)
            If chkDup = False Then
                MessageBoxAlert("แจ้งเตือน", "ไม่สามารถบันทึกข้อมูลได้เนื่องมีข้อมูลประเภทสินเชื่อนี้อยู่แล้ว", "", "ปิด", False, True)
                Return
            End If

            If lblLoanTypeDetail.Text = "" Then
                lblLoanTypeDetail.Text = lblLoanType.Text
            Else
                lblLoanTypeDetail.Text = lblLoanTypeDetail.Text + "," + lblLoanType.Text
            End If

            If ddlMKTCODE.SelectedIndex <> 0 Then
                If lblLnmktcodeDetail.Text = "" Then
                    lblLnmktcodeDetail.Text = ddlMKTCODE.SelectedItem.ToString()
                Else
                    lblLnmktcodeDetail.Text = lblLnmktcodeDetail.Text + "," + ddlMKTCODE.SelectedItem.ToString()
                End If
            Else
                If lblLnmktcodeDetail.Text = "" Then
                    lblLnmktcodeDetail.Text = "-"
                Else
                    lblLnmktcodeDetail.Text = lblLnmktcodeDetail.Text + "," + "-"
                End If
            End If



            If ddlLNSUBTYPE.SelectedIndex <> 0 Then
                If lblLnsubtypeDetail.Text = "" Then
                    lblLnsubtypeDetail.Text = ddlLNSUBTYPE.SelectedItem.ToString()
                Else
                    lblLnsubtypeDetail.Text = lblLnsubtypeDetail.Text + "," + ddlLNSUBTYPE.SelectedItem.ToString()
                End If
            Else
                If lblLnsubtypeDetail.Text = "" Then
                    lblLnsubtypeDetail.Text = "-"
                Else
                    lblLnsubtypeDetail.Text = lblLnsubtypeDetail.Text + "," + "-"
                End If
            End If



            If ddlLNTYPECODE.SelectedIndex <> 0 Then
                If lblLntypecodeDetail.Text = "" Then
                    lblLntypecodeDetail.Text = ddlLNTYPECODE.SelectedItem.ToString()
                Else
                    lblLntypecodeDetail.Text = lblLntypecodeDetail.Text + "," + ddlLNTYPECODE.SelectedItem.ToString()
                End If
            Else
                If lblLntypecodeDetail.Text = "" Then
                    lblLntypecodeDetail.Text = "-"
                Else
                    lblLntypecodeDetail.Text = lblLntypecodeDetail.Text + "," + "-"
                End If
            End If

            hdfType.Value = 2
            lblLoanType.Text = ""

            GetDDL_LNTYPECODE()
            GetDDL_LNSUBTYPE("")
            GetDDL_LNMKTCODE("")

            entDetail.LoanType = lblLoanTypeDetail.Text
            entDetail.LN_TYPE_CODE = lblLntypecodeDetail.Text
            entDetail.LN_SUB_TYPE = lblLnsubtypeDetail.Text
            entDetail.LN_MKT_CODE = lblLnmktcodeDetail.Text
            entDetail.Type = hdfType.Value
            entDetail.IndustryLimitAmount = txtIndustryLimitAmount.Text
            entDetail.IndustryLimitPercentage = txtIndustryLimitPercentage.Text

        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("EffectiveDateDetail_Add", "IsAuthen()", ex.Message)
        End Try

    End Sub

    Protected Sub MessageBoxAlert(ByVal title As String, ByVal _message As String, ByVal BtnOKString As String, ByVal BtnNOString As String, ByVal YesbtnStatus As Boolean, ByVal NobtnStatus As Boolean)
        lbl_Title.Text = title
        If title = "Error" Then
            Symbol_Image.ImageUrl = "~/Images/NotCorrect.png"
        ElseIf title = "Success" Then
            Symbol_Image.ImageUrl = "~/Images/Correct.png"
        ElseIf title = "Question" Then
            Symbol_Image.ImageUrl = "~/Images/Question.png"
        ElseIf title = "แจ้งเตือน" Then
            Symbol_Image.ImageUrl = "~/Images/Warning.png"
        End If

        Messages.Text = _message
        btn_OK.Visible = YesbtnStatus
        btn_NO.Visible = NobtnStatus
        btn_OK.Text = BtnOKString
        btn_NO.Text = BtnNOString

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertBox", "$('#AlertBox').modal();", True)
        UpdModal.Update()
    End Sub

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Response.Redirect("~/IndustryLimit/EffectiveDate_Add.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim ISICCODESUBLEVEL As String = ""
            Dim IndustryLimitAmount As Double? = 0
            Dim IndustryLimitPercentage As Double? = 0
            Dim chkEmpty As Boolean
            chkEmpty = True

            'Dim entDetail As New IndustyDetailEntity
            If (Not String.IsNullOrEmpty(txtIndustryLimitPercentage.Text)) Then
                IndustryLimitPercentage = Convert.ToDouble(txtIndustryLimitPercentage.Text)
            Else
                IndustryLimitPercentage = Nothing
            End If

            If (Not String.IsNullOrEmpty(txtIndustryLimitAmount.Text)) Then
                IndustryLimitAmount = Convert.ToDouble(txtIndustryLimitAmount.Text)
            Else
                IndustryLimitAmount = Nothing
            End If

            'Check EmptyData
            chkEmpty = CheckEmptyData(IndustryLimitPercentage, IndustryLimitAmount)
            If chkEmpty = False Then
                Return
            End If

            entDetail = New IndustyDetailEntity

            If hdfType.Value = 1 Then
                entDetail.ISICCODE = lblIsiccodeDetail.Text
                entDetail.ISICCODESUBLEVEL = lblIsiccodesubDetail.Text
                entDetail.Industry = lblIndustryDetail.Text
            Else
                entDetail.LN_TYPE_CODE = lblLntypecodeDetail.Text
                entDetail.LN_SUB_TYPE = lblLnsubtypeDetail.Text
                entDetail.LN_MKT_CODE = lblLnmktcodeDetail.Text
                entDetail.LoanType = lblLoanTypeDetail.Text
            End If

            If Not IndustryLimitAmount Is Nothing Then
                entDetail.IndustryLimitAmount = String.Format("{0:N2}", IndustryLimitAmount)
            Else
                entDetail.IndustryLimitAmount = Convert.ToString(IndustryLimitAmount)
            End If

            If Not IndustryLimitPercentage Is Nothing Then
                entDetail.IndustryLimitPercentage = String.Format("{0:N2}", IndustryLimitPercentage)
            Else
                entDetail.IndustryLimitPercentage = Convert.ToString(IndustryLimitPercentage)
            End If

            If detailList Is Nothing Then
                detailList = New List(Of IndustyDetailEntity)
            End If

            entDetail.Type = hdfType.Value

            detailList.Add(entDetail)
            Session("DetailList") = detailList
            entDetail = New IndustyDetailEntity

            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "ปิด", "", True, False)
            btn_OK.Attributes.Remove("data-dismiss")

        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("EffectiveDateDetail_Add", "IsAuthen()", ex.Message)
        End Try
    End Sub

    Private Sub ClearDetail()
        lblIsiccodeDetail.Text = String.Empty
        lblIsiccodesubDetail.Text = String.Empty
        lblLntypecodeDetail.Text = String.Empty
        lblLnsubtypeDetail.Text = String.Empty
        lblLnmktcodeDetail.Text = String.Empty
        lblIndustryDetail.Text = String.Empty
        lblLoanTypeDetail.Text = String.Empty
        txtIndustryLimitAmount.Text = String.Empty
        txtIndustryLimitPercentage.Text = String.Empty
        txtIndustryLimitAmount.Enabled = True
        txtIndustryLimitPercentage.Enabled = True
        hdfType.Value = String.Empty
        entDetail = New IndustyDetailEntity
    End Sub

    Protected Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        ClearDetail()
        Response.Redirect("~/IndustryLimit/EffectiveDate_Add.aspx")
    End Sub

    Protected Sub txtIndustryLimitAmount_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryLimitAmount.TextChanged
        If String.IsNullOrEmpty(txtIndustryLimitAmount.Text) = True Then
            txtIndustryLimitPercentage.Enabled = True
        Else
            txtIndustryLimitPercentage.Enabled = False
        End If
    End Sub

    Private Function CheckEmptyData(ByVal IndustryLimitAmount As Double?, ByVal IndustryLimitPercentage As Double?) As Boolean
        If hdfType.Value = String.Empty Then
            MessageBoxAlert("แจ้งเตือน", "กรุณาเลือกข้อมูลภาคธุรกิจหรือข้อมูลประเภทสินเชื่อ", "", "ปิด", False, True)
            Return False
        End If

        'Check Empty Amount and Percentage
        If txtIndustryLimitAmount.Text = String.Empty And txtIndustryLimitPercentage.Text = String.Empty Then
            MessageBoxAlert("แจ้งเตือน", "กรุณาบันทึกข้อมูล เปอร์เซ็น หรือ วงเงินอนุมัติ อย่างใดอย่างหนึ่ง", "", "ปิด", False, True)
            Return False
        End If

        If IndustryLimitPercentage = 0 And IndustryLimitAmount = 0 Then
            MessageBoxAlert("แจ้งเตือน", "กรุณาบันทึกข้อมูล เปอร์เซ็น หรือ วงเงินอนุมัติ อย่างใดอย่างหนึ่ง", "", "ปิด", False, True)
            Return False
        End If

        If (IndustryLimitPercentage Is Nothing And IndustryLimitAmount Is Nothing) Then
            MessageBoxAlert("แจ้งเตือน", "กรุณาบันทึกข้อมูล เปอร์เซ็น หรือ วงเงินอนุมัติ อย่างใดอย่างหนึ่ง", "", "ปิด", False, True)
            Return False
        End If
        Return True
    End Function

    Private Function CheckDupDataIsic(ByVal value As String) As Boolean
        Dim stData As String
        Dim xd As New List(Of String)

        If Not String.IsNullOrEmpty(entDetail.Industry) Then
            If ddlISICSUB.SelectedIndex <> 0 Then
                stData = entDetail.ISICCODESUBLEVEL
            ElseIf ddlISICCODE.SelectedIndex <> 0 Then
                stData = entDetail.ISICCODE
            End If

            If Not String.IsNullOrEmpty(stData) Then
                xd.AddRange(stData.Split(",").ToList)
            End If
            Dim chkList = xd.Where(Function(x) x = value).Count()
            If chkList <> 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Private Function CheckDupDataLoanType(ByVal value As String) As Boolean
        Dim stData As String
        Dim xd As New List(Of String)

        If Not String.IsNullOrEmpty(entDetail.LoanType) Then
            If ddlMKTCODE.SelectedIndex <> 0 Then
                stData = entDetail.LN_MKT_CODE
            ElseIf ddlLNSUBTYPE.SelectedIndex <> 0 Then
                stData = entDetail.LN_SUB_TYPE
            ElseIf ddlLNTYPECODE.SelectedIndex <> 0 Then
                stData = entDetail.LN_TYPE_CODE
            End If

            If Not String.IsNullOrEmpty(stData) Then
                xd.AddRange(stData.Split(",").ToList)
            End If

            Dim chkList = xd.Where(Function(x) x = value).Count()
            If chkList <> 0 Then
                Return False
            End If
        End If
        Return True
    End Function

End Class