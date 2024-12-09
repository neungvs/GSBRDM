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
Imports Arsoft.Utility

Public Class ModifyIndustryLimit
    Inherits System.Web.UI.Page

    Dim lmBiz As New IndustryBiz
    Dim gui As New Guid


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetData()
            HF_effectiveDate.Value = Convert.ToString(Session("effectiveDate"))
            SetHeadder()
            chkCheckbok()
            SetEffectiveDate()
        End If
    End Sub


    Protected Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Response.Redirect("~/IndustryLimit/IndustryLimit.aspx")
    End Sub

    Private Sub GetData()
        gvLimit.DataSource = lmBiz.GetSectorLimit(Convert.ToString(Session("effectiveDate"))).OrderBy(Function(i) If(i.LN_MKT_CODE = "" And i.LN_TYPE_CODE = "" And i.LN_SUB_TYPE = "", 1, 2)).ThenBy(Function(i) If(i.IndustryLimitPercentage = "", 2, 1)).ThenBy(Function(i) i.LN_TYPE_CODE).ThenBy(Function(i) i.LN_SUB_TYPE).ThenBy(Function(i) i.LN_MKT_CODE).ThenBy(Function(i) i.ISICCODE).ThenBy(Function(s) s.ISICCODESUBLEVEL).ToList()
        gvLimit.DataBind()
    End Sub



    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Not String.IsNullOrEmpty(txtisicCode.Text) Then
            Dim result As Boolean = False
            result = UtilsBiz.checkForSQLInjection(txtisicCode.Text.Trim())
            If result = True Then
                MessageBoxAlert("แจ้งเตือน", "ตรวจพบคำสั่ง sqlInjection ไม่สามารถค้นหาข้อมูลได้", "", "ปิด", False, True)
                Return
            End If
        End If

        If Not String.IsNullOrEmpty(txtisicSub.Text) Then
            Dim result As Boolean = False
            result = UtilsBiz.checkForSQLInjection(txtisicSub.Text.Trim())
            If result = True Then
                MessageBoxAlert("แจ้งเตือน", "ตรวจพบคำสั่ง sqlInjection ไม่สามารถค้นหาข้อมูลได้", "", "ปิด", False, True)
                Return
            End If
        End If

        Session("IsicCode") = txtisicCode.Text
        Session("IsicSub") = txtisicSub.Text
        gvLimit.PageIndex = 0
        SearchData()
    End Sub

    Private Sub SearchData()
        gvLimit.DataSource = lmBiz.GetSectorLimitCriteria(HF_effectiveDate.Value, Session("IsicCode").ToString, Session("IsicSub").ToString).OrderBy(Function(i) If(i.LN_MKT_CODE = "" And i.LN_TYPE_CODE = "" And i.LN_SUB_TYPE = "", 1, 2)).ThenBy(Function(i) If(i.IndustryLimitPercentage = "", 2, 1)).ThenBy(Function(i) i.LN_TYPE_CODE).ThenBy(Function(i) i.LN_SUB_TYPE).ThenBy(Function(i) i.LN_MKT_CODE).ThenBy(Function(i) i.ISICCODE).ThenBy(Function(s) s.ISICCODESUBLEVEL).ToList()
        gvLimit.DataBind()
    End Sub

    Private Sub SetControl()
        Dim liId As HtmlGenericControl = Page.Master.FindControl("liId")
        liId.Attributes.Add("class", "active")
    End Sub




    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim entHeader As New IndustyHeaderEntity
            entHeader = SetHeadderDetail()

            If String.IsNullOrEmpty(txtEffectiveDate1.Text) And String.IsNullOrEmpty(txtEffectiveDate2.Text) And String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาเลือก วันที่มีผลบังคับใช้", "", "ปิด", False, True)
                Return
            End If


            If chkIsAgree1.Checked = False And chkIsAgree2.Checked = False And chkIsAgree3.Checked = False And chkIsApprove1.Checked = False And chkIsApprove2.Checked = False And chkIsApprove3.Checked = False Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาเลือก เห็นชอบ หรือ อนุมัติ", "", "ปิด", False, True)
                Return
            End If


            If txtEffectiveDate1.Text <> HF_effectiveDate.Value And txtEffectiveDate2.Text <> HF_effectiveDate.Value And txtEffectiveDate3.Text <> HF_effectiveDate.Value Then
                Dim chkISIC As Boolean = True
                If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                    chkISIC = lmBiz.CheckEffectiveDate(txtEffectiveDate1.Text)
                ElseIf Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                    chkISIC = lmBiz.CheckEffectiveDate(txtEffectiveDate2.Text)
                ElseIf Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                    chkISIC = lmBiz.CheckEffectiveDate(txtEffectiveDate3.Text)
                End If

                If chkISIC = False Then
                    MessageBoxAlert("แจ้งเตือน", "ไม่สามารถบันทึกได้เนื่องจากมี EffectiveDate นี้อยู่่ในระบบแล้ว", "", "ปิด", False, True)
                    Return
                End If
            End If


            Dim newEffectiveDate As String
            newEffectiveDate = Nothing

            If txtEffectiveDate1.Text <> HF_effectiveDate.Value And txtEffectiveDate2.Text <> HF_effectiveDate.Value And txtEffectiveDate3.Text <> HF_effectiveDate.Value Then
                If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                    newEffectiveDate = txtEffectiveDate1.Text
                ElseIf Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                    newEffectiveDate = txtEffectiveDate2.Text
                ElseIf Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                    newEffectiveDate = txtEffectiveDate3.Text
                End If
            Else
                newEffectiveDate = Nothing

            End If


            lmBiz.UpdateHeadder(HF_effectiveDate.Value, entHeader, newEffectiveDate)
            MessageBoxAlert("Success", "บันทึกข้อมูลเรียบร้อยแล้ว", "ปิด", "", True, False)

            If txtEffectiveDate1.Text <> HF_effectiveDate.Value Then
                HF_effectiveDate.Value = txtEffectiveDate1.Text
            End If

        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("ModifyIndustryLimit", "btnSave_Click()", ex.Message)
        End Try

    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        SetHeadder()
        chkCheckbok()
        SetEffectiveDate()
    End Sub

    Private Sub SetEffectiveDate()
        If chkIsAgree1.Checked = False And chkIsApprove1.Checked = False Then
            txtEffectiveDate1.Text = ""
        Else
            txtEffectiveDate1.Text = HF_effectiveDate.Value
        End If

        If chkIsAgree2.Checked = False And chkIsApprove2.Checked = False Then
            txtEffectiveDate2.Text = ""
        Else
            txtEffectiveDate2.Text = HF_effectiveDate.Value
        End If


        If chkIsAgree3.Checked = False And chkIsApprove3.Checked = False Then
            txtEffectiveDate3.Text = ""
        Else
            txtEffectiveDate3.Text = HF_effectiveDate.Value
        End If
    End Sub

    Protected Sub gvLimit_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub




    Protected Sub gvLimit_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLimit.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        Dim lblIndustryLimitPercentage As Label = CType(e.Row.FindControl("lblIndustryLimitPercentage"), Label)
        Dim lblIndustryLimitAmount As Label = CType(e.Row.FindControl("lblIndustryLimitAmount"), Label)

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
            If String.IsNullOrEmpty(lblIndustryLimitPercentage.Text) = True Then
                lblIndustryLimitPercentage.Text = "-"
            End If
            If String.IsNullOrEmpty(lblIndustryLimitAmount.Text) = True Then
                lblIndustryLimitAmount.Text = "-"
                e.Row.Cells(4).CssClass = "TextCenter"
            Else
                e.Row.Cells(4).CssClass = "TextRight"
            End If
        End If
    End Sub

    Private Sub SetHeadder()
        Try
            Dim entHeader As New List(Of IndustyHeaderEntity)
            entHeader = lmBiz.GetHeadder(HF_effectiveDate.Value)
            'Set Approve Date  วันที่อนุมัติ
            If Not String.IsNullOrEmpty(entHeader(0).ApproveDate_1) Then
                txtApproveDate1.Text = entHeader(0).ApproveDate_1
            End If
            If Not String.IsNullOrEmpty(entHeader(0).ApproveDate_2) Then
                txtApproveDate2.Text = entHeader(0).ApproveDate_2
            End If
            If Not String.IsNullOrEmpty(entHeader(0).ApproveDate_3) Then
                txtApproveDate3.Text = entHeader(0).ApproveDate_3
            End If
            'Set ApproveID   ครั้งที่อนุมัติ
            If Not String.IsNullOrEmpty(entHeader(0).ApproveID_1) Then
                txtApproveID1.Text = entHeader(0).ApproveID_1
            End If
            If Not String.IsNullOrEmpty(entHeader(0).ApproveID_2) Then
                txtApproveID2.Text = entHeader(0).ApproveID_2
            End If
            If Not String.IsNullOrEmpty(entHeader(0).ApproveID_3) Then
                txtApproveID3.Text = entHeader(0).ApproveID_3
            End If
            'SetIsAgree  เห็นชอบ
            If Not String.IsNullOrEmpty(entHeader(0).IsAgree_1) Then
                chkIsAgree1.Checked = entHeader(0).IsAgree_1
            Else
                chkIsAgree1.Checked = False
            End If
            If Not String.IsNullOrEmpty(entHeader(0).IsAgree_2) Then
                chkIsAgree2.Checked = entHeader(0).IsAgree_2
            Else
                chkIsAgree2.Checked = False
            End If
            If Not String.IsNullOrEmpty(entHeader(0).IsAgree_3) Then
                chkIsAgree3.Checked = entHeader(0).IsAgree_3
            Else
                chkIsAgree3.Checked = False
            End If
            'SetIsApprove  อนุมัติ
            If Not String.IsNullOrEmpty(entHeader(0).IsApprove_1) Then
                chkIsApprove1.Checked = entHeader(0).IsApprove_1
            Else
                chkIsApprove1.Checked = False
            End If
            If Not String.IsNullOrEmpty(entHeader(0).IsApprove_2) Then
                chkIsApprove2.Checked = entHeader(0).IsApprove_2
            Else
                chkIsApprove2.Checked = False
            End If
            If Not String.IsNullOrEmpty(entHeader(0).IsApprove_3) Then
                chkIsApprove3.Checked = entHeader(0).IsApprove_3
            Else
                chkIsApprove3.Checked = False
            End If



        Catch ex As Exception

            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("ModifyIndustryLimit", "btnSave_Click()", ex.Message)
        End Try
    End Sub

    Private Function SetHeadderDetail() As IndustyHeaderEntity
        Dim ent As New IndustyHeaderEntity
        ent.ApproveDate_1 = txtApproveDate1.Text
        ent.ApproveDate_2 = txtApproveDate2.Text
        ent.ApproveDate_3 = txtApproveDate3.Text
        ent.ApproveID_1 = txtApproveID1.Text
        ent.ApproveID_2 = txtApproveID2.Text
        ent.ApproveID_3 = txtApproveID3.Text

        If chkIsAgree1.Checked = True Then
            ent.IsAgree_1 = 1
        Else
            ent.IsAgree_1 = 0
        End If

        If chkIsAgree2.Checked = True Then
            ent.IsAgree_2 = 1
        Else
            ent.IsAgree_2 = 0
        End If
        If chkIsAgree3.Checked = True Then
            ent.IsAgree_3 = 1
        Else
            ent.IsAgree_3 = 0
        End If
        If chkIsApprove1.Checked = True Then
            ent.IsApprove_1 = 1

        Else
            ent.IsApprove_1 = 0
        End If

        If chkIsApprove2.Checked = True Then
            ent.IsApprove_2 = 1

        Else
            ent.IsApprove_2 = 0
        End If

        If chkIsApprove3.Checked = True Then
            ent.IsApprove_3 = 1

        Else
            ent.IsApprove_3 = 0
        End If

        Return ent
    End Function



    Protected Sub gvLimit_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles gvLimit.SelectedIndexChanged
        Dim hdfID As HiddenField = CType(gvLimit.SelectedRow.FindControl("hdfID"), HiddenField)
        Session("hdfID") = hdfID.Value
        Session("effectiveDate") = HF_effectiveDate.Value
        Response.Redirect("~/IndustryLimit/Industry_Limit_Edit.aspx")
    End Sub


    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim hdfID As HiddenField = gvLimit.Rows(e.RowIndex).Cells(1).FindControl("hdfID")
            lmBiz.DeleteDetail(hdfID.Value)
            MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "ปิด", "", True, False)
            GetData()
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("ModifyIndustryLimit", "btnSave_Click()", ex.Message)
        End Try
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("~/IndustryLimit/Industry_Limit_Add.aspx")
    End Sub

    Protected Sub gvLimit_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvLimit.PageIndexChanging
        gvLimit.PageIndex = e.NewPageIndex
        If String.IsNullOrEmpty(Convert.ToString(Session("IsicCode"))) And String.IsNullOrEmpty(Convert.ToString(Session("IsicSub"))) Then
            GetData()
        Else
            SearchData()
        End If
    End Sub

    Protected Sub txtEffectiveDate1_TextChanged(sender As Object, e As EventArgs) Handles txtEffectiveDate1.TextChanged
        If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                txtEffectiveDate2.Text = txtEffectiveDate1.Text
            End If

            If Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                txtEffectiveDate3.Text = txtEffectiveDate1.Text
            End If
        End If
    End Sub

    Protected Sub txtEffectiveDate2_TextChanged(sender As Object, e As EventArgs) Handles txtEffectiveDate2.TextChanged
        If Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                txtEffectiveDate1.Text = txtEffectiveDate2.Text
            End If

            If Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                txtEffectiveDate3.Text = txtEffectiveDate2.Text
            End If
        End If
    End Sub

    Protected Sub txtEffectiveDate3_TextChanged(sender As Object, e As EventArgs) Handles txtEffectiveDate3.TextChanged
        If Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                txtEffectiveDate1.Text = txtEffectiveDate3.Text
            End If

            If Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                txtEffectiveDate2.Text = txtEffectiveDate3.Text
            End If
        End If
    End Sub



    ' CheckCheckBox
    Protected Sub chkIsAgree1_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsAgree1.CheckedChanged
        chkCheckbok()
        If String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                txtEffectiveDate1.Text = txtEffectiveDate2.Text
            End If
            If Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                txtEffectiveDate1.Text = txtEffectiveDate3.Text
            End If
        End If

        If chkIsAgree1.Checked = False And chkIsApprove1.Checked = False Then
            txtEffectiveDate1.Text = ""
        End If
    End Sub

    Protected Sub chkIsApprove1_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsApprove1.CheckedChanged
        chkCheckbok()
        If String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                txtEffectiveDate1.Text = txtEffectiveDate2.Text
            End If
            If Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                txtEffectiveDate1.Text = txtEffectiveDate3.Text

            End If
        End If

        If chkIsAgree1.Checked = False And chkIsApprove1.Checked = False Then
            txtEffectiveDate1.Text = ""
        End If
    End Sub

    Protected Sub chkIsAgree2_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsAgree2.CheckedChanged
        chkCheckbok()
        If String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                txtEffectiveDate2.Text = txtEffectiveDate1.Text
            End If
            If Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                txtEffectiveDate2.Text = txtEffectiveDate3.Text
            End If
        End If

        If chkIsAgree2.Checked = False And chkIsApprove2.Checked = False Then
            txtEffectiveDate2.Text = ""
        End If
    End Sub

    Protected Sub chkIsApprove2_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsApprove2.CheckedChanged
        chkCheckbok()
        If String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                txtEffectiveDate2.Text = txtEffectiveDate1.Text
            End If

            If Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                txtEffectiveDate2.Text = txtEffectiveDate3.Text
            End If
        End If
        If chkIsAgree2.Checked = False And chkIsApprove2.Checked = False Then
            txtEffectiveDate2.Text = ""
        End If
    End Sub

    Protected Sub chkIsAgree3_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsAgree3.CheckedChanged
        chkCheckbok()
        If String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                txtEffectiveDate3.Text = txtEffectiveDate1.Text
            End If
            If Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                txtEffectiveDate3.Text = txtEffectiveDate2.Text
            End If
        End If

        If chkIsAgree3.Checked = False And chkIsApprove3.Checked = False Then
            txtEffectiveDate3.Text = ""
        End If
    End Sub

    Protected Sub chkIsApprove3_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsApprove3.CheckedChanged
        chkCheckbok()
        If String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
            If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                txtEffectiveDate3.Text = txtEffectiveDate1.Text
            End If
            If Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                txtEffectiveDate3.Text = txtEffectiveDate2.Text
            End If
        End If

        If chkIsAgree3.Checked = False And chkIsApprove3.Checked = False Then
            txtEffectiveDate3.Text = ""
        End If
    End Sub

    Private Sub chkCheckbok()
        If chkIsAgree1.Checked = False And chkIsApprove1.Checked = False Then
            txtApproveID1.Enabled = False
            txtApproveDate1.CssClass = ""
            txtApproveID1.Text = ""
            txtApproveDate1.Text = ""
            txtEffectiveDate1.Text = ""
            txtEffectiveDate1.CssClass = ""
        Else
            txtApproveID1.Enabled = True
            txtApproveDate1.CssClass = "datePic"
            txtEffectiveDate1.CssClass = "datePic"
        End If

        If chkIsAgree2.Checked = False And chkIsApprove2.Checked = False Then
            txtApproveID2.Enabled = False
            txtApproveDate2.CssClass = ""
            txtApproveID2.Text = ""
            txtApproveDate2.Text = ""
            txtEffectiveDate2.Text = ""
            txtEffectiveDate2.CssClass = ""
        Else
            txtApproveID2.Enabled = True
            txtApproveDate2.CssClass = "datePic"
            txtEffectiveDate2.CssClass = "datePic"
        End If

        If chkIsAgree3.Checked = False And chkIsApprove3.Checked = False Then
            txtApproveID3.Enabled = False
            txtApproveDate3.CssClass = ""
            txtApproveID3.Text = ""
            txtApproveDate3.Text = ""
            txtEffectiveDate3.Text = ""
            txtEffectiveDate3.CssClass = ""
        Else
            txtApproveID3.Enabled = True
            txtApproveDate3.CssClass = "datePic"
            txtEffectiveDate3.CssClass = "datePic"
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


End Class