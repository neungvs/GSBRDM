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

Public Class Industry_Limit_Edit
    Inherits System.Web.UI.Page

    Dim lmBiz As New IndustryBiz
    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            hdfID.Value = Convert.ToString(Session("hdfID"))
            HF_effectiveDate.Value = Convert.ToString(Session("effectiveDate"))
            GetData(hdfID.Value)
        End If
    End Sub

    Protected Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Session("effectiveDate") = HF_effectiveDate.Value
        Response.Redirect("~/IndustryLimit/ModifyIndustryLimit.aspx")
    End Sub



    Private Sub GetData(sectorID As String)
        Dim detailEnt As New IndustyDetailEntity

        detailEnt = lmBiz.GetEditData(hdfID.Value)

        If detailEnt.Type = 1 Then
            lblISICCODE.Text = detailEnt.ISICCODE
            lblISICCODESUBLEVEL.Text = detailEnt.ISICCODESUBLEVEL
            lblIndustry.Text = detailEnt.Industry
            trISICCODE.Visible = True
            trISICSUB.Visible = True
            trIndustry.Visible = True
            trLN_TYPE_CODE.Visible = False
            trLN_SUB_TYPE.Visible = False
            trMKT_CODE.Visible = False
            trLoanType.Visible = False
            lblNameType.Text = "ข้อมูลภาคธุรกิจ"
        Else
            lblLN_TYPE_CODE.Text = detailEnt.LN_TYPE_CODE
            lblLN_SUB_TYPE.Text = detailEnt.LN_SUB_TYPE
            lblMKT_CODE.Text = detailEnt.LN_MKT_CODE
            lblLoanType.Text = detailEnt.LoanType
            trISICCODE.Visible = False
            trISICSUB.Visible = False
            trIndustry.Visible = False
            trLN_TYPE_CODE.Visible = True
            trLN_SUB_TYPE.Visible = True
            trMKT_CODE.Visible = True
            trLoanType.Visible = True
            lblNameType.Text = "ข้อมูลประเภทสินเชื่อ"
        End If

        If detailEnt.IndustryLimitAmount = "-" Then
            txtIndustryLimitAmount.Text = ""
        Else
            txtIndustryLimitAmount.Text = detailEnt.IndustryLimitAmount
        End If

        If detailEnt.IndustryLimitPercentage = "-" Then
            txtIndustryLimitPercentage.Text = ""
        Else
            txtIndustryLimitPercentage.Text = detailEnt.IndustryLimitPercentage
        End If

        CheckTextBox()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim IndustryLimitAmount As Double? = 0
            Dim IndustryLimitPercentage As Double? = 0
            Dim entDetail As New IndustyDetailEntity

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


            If (IndustryLimitPercentage Is Nothing And IndustryLimitAmount Is Nothing) Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาบันทึกข้อมูล เปอร์เซ็น หรือ วงเงินอนุมัติ อย่างใดอย่างหนึ่ง", "", "ปิด", False, True)
                Return
            End If

            If (IndustryLimitPercentage = 0 And IndustryLimitAmount = 0) Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาบันทึกข้อมูล เปอร์เซ็น หรือ วงเงินอนุมัติ อย่างใดอย่างหนึ่ง", "", "ปิด", False, True)
                Return
            End If

            If (IndustryLimitPercentage <> 0 And IndustryLimitAmount <> 0) Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาบันทึกข้อมูล เปอร์เซ็น หรือ วงเงินอนุมัติ อย่างใดอย่างหนึ่ง", "", "ปิด", False, True)
                Return
            Else
                lmBiz.UpdateDetail(hdfID.Value, IndustryLimitPercentage, IndustryLimitAmount)
                Session("effectiveDate") = HF_effectiveDate.Value
                MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "ปิด", "", True, False)
                btn_OK.Attributes.Remove("data-dismiss")
            End If

        Catch ex As Exception
            UtilsBiz.CreateMessageAlert(Page, ex.Message, gui)
        End Try

    End Sub

    Protected Sub txtIndustryLimitAmount_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryLimitAmount.TextChanged
        If String.IsNullOrEmpty(txtIndustryLimitAmount.Text) = True Then
            txtIndustryLimitPercentage.Enabled = True
        Else
            txtIndustryLimitPercentage.Enabled = False
        End If
    End Sub

    Protected Sub txtIndustryLimitPercentage_TextChanged(sender As Object, e As EventArgs) Handles txtIndustryLimitPercentage.TextChanged
        If String.IsNullOrEmpty(txtIndustryLimitPercentage.Text) = True Then
            txtIndustryLimitAmount.Enabled = True
        Else
            txtIndustryLimitAmount.Enabled = False
        End If
    End Sub

    Private Sub CheckTextBox()
        If String.IsNullOrEmpty(txtIndustryLimitAmount.Text) = True Then
            txtIndustryLimitAmount.Enabled = False
        Else
            txtIndustryLimitAmount.Enabled = True
        End If

        If String.IsNullOrEmpty(txtIndustryLimitPercentage.Text) = True Then
            txtIndustryLimitPercentage.Enabled = False
        Else
            txtIndustryLimitPercentage.Enabled = True
        End If
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
        Response.Redirect("~/IndustryLimit/ModifyIndustryLimit.aspx")
    End Sub
End Class