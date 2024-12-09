Imports System.Globalization
Imports System.Threading
Imports System.Data
Imports System.IO
Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Web.HttpApplicationState
Imports Arsoft.Utility

Public Class EffectiveDate_Add
    Inherits System.Web.UI.Page
    Dim lmBiz As New IndustryBiz
    Dim gui As New Guid
    Shared headEntity As New IndustyHeaderEntity


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Session("HeaderList") Is Nothing Then
                SetHeaderDetial()
            End If
            If Not Session("DetailList") Is Nothing Then
                SetDetail()
            Else
                GetDetialData()
            End If
            chkCheckbok()
        End If
    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim lsDetial As New List(Of IndustyDetailEntity)
        lsDetial = Session("DetailList")
        lsDetial.RemoveAt(e.RowIndex)
        Session("DetailList") = lsDetial
        gvLimit.DataSource = lsDetial
        gvLimit.DataBind()
    End Sub

    Protected Sub gvLimit_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvLimit.PageIndexChanging
        gvLimit.PageIndex = e.NewPageIndex
        Dim ls = TryCast(CObj(Session("DetailList")), List(Of IndustyDetailEntity))
        ls = ls.OrderBy(Function(i) If(i.LN_MKT_CODE = "" And i.LN_TYPE_CODE = "" And i.LN_SUB_TYPE = "", 1, 2)).ThenBy(Function(i) If(i.IndustryLimitPercentage = "", 2, 1)).ThenBy(Function(i) i.LN_TYPE_CODE).ThenBy(Function(i) i.LN_SUB_TYPE).ThenBy(Function(i) i.LN_MKT_CODE).ThenBy(Function(i) i.ISICCODE).ThenBy(Function(s) s.ISICCODESUBLEVEL).ToList()
        gvLimit.DataSource = ls
        gvLimit.DataBind()
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        ClearSession()
        Response.Redirect("~/IndustryLimit/IndustryLimit.aspx")
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

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        headEntity = GetHeadderDetail()
        Dim lsDetial As New List(Of IndustyDetailEntity)
        Session("HeaderList") = headEntity
        Response.Redirect("~/IndustryLimit/EffectiveDateDetail_Add.aspx")
    End Sub

    Private Function GetHeadderDetail() As IndustyHeaderEntity
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

        If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
            ent.EffectiveDate = txtEffectiveDate1.Text
        ElseIf Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
            ent.EffectiveDate = txtEffectiveDate2.Text
        ElseIf Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
            ent.EffectiveDate = txtEffectiveDate3.Text
        End If

        Return ent
    End Function

    Private Sub SetHeaderDetial()
        Dim entHeader = TryCast(CObj(Session("HeaderList")), IndustyHeaderEntity)
        'Set Approve Date  วันที่อนุมัติ
        If Not String.IsNullOrEmpty(entHeader.ApproveDate_1) Then
            txtApproveDate1.Text = entHeader.ApproveDate_1
        End If
        If Not String.IsNullOrEmpty(entHeader.ApproveDate_2) Then
            txtApproveDate2.Text = entHeader.ApproveDate_2
        End If
        If Not String.IsNullOrEmpty(entHeader.ApproveDate_3) Then
            txtApproveDate3.Text = entHeader.ApproveDate_3
        End If
        'Set ApproveID   ครั้งที่อนุมัติ
        If Not String.IsNullOrEmpty(entHeader.ApproveID_1) Then
            txtApproveID1.Text = entHeader.ApproveID_1
        End If
        If Not String.IsNullOrEmpty(entHeader.ApproveID_2) Then
            txtApproveID2.Text = entHeader.ApproveID_2
        End If
        If Not String.IsNullOrEmpty(entHeader.ApproveID_3) Then
            txtApproveID3.Text = entHeader.ApproveID_3
        End If

        'SetIsAgree  เห็นชอบ
        If Not String.IsNullOrEmpty(entHeader.IsAgree_1) Then
            chkIsAgree1.Checked = entHeader.IsAgree_1
        Else
            chkIsAgree1.Checked = False
        End If
        If Not String.IsNullOrEmpty(entHeader.IsAgree_2) Then
            chkIsAgree2.Checked = entHeader.IsAgree_2
        Else
            chkIsAgree2.Checked = False
        End If
        If Not String.IsNullOrEmpty(entHeader.IsAgree_3) Then
            chkIsAgree3.Checked = entHeader.IsAgree_3
        Else
            chkIsAgree3.Checked = False
        End If

        'SetIsApprove  อนุมัติ
        If Not String.IsNullOrEmpty(entHeader.IsApprove_1) Then
            chkIsApprove1.Checked = entHeader.IsApprove_1
        Else
            chkIsApprove1.Checked = False
        End If
        If Not String.IsNullOrEmpty(entHeader.IsApprove_2) Then
            chkIsApprove2.Checked = entHeader.IsApprove_2
        Else
            chkIsApprove2.Checked = False
        End If
        If Not String.IsNullOrEmpty(entHeader.IsApprove_3) Then
            chkIsApprove3.Checked = entHeader.IsApprove_3
        Else
            chkIsApprove3.Checked = False
        End If

        txtEffectiveDate1.Text = entHeader.EffectiveDate
        txtEffectiveDate2.Text = entHeader.EffectiveDate
        txtEffectiveDate3.Text = entHeader.EffectiveDate
    End Sub

    Private Sub SetDetail()
        Dim ls = TryCast(CObj(Session("DetailList")), List(Of IndustyDetailEntity))
        ls = ls.OrderBy(Function(i) If(i.LN_MKT_CODE = "" And i.LN_TYPE_CODE = "" And i.LN_SUB_TYPE = "", 1, 2)).ThenBy(Function(i) If(i.IndustryLimitPercentage = "", 2, 1)).ThenBy(Function(i) i.LN_TYPE_CODE).ThenBy(Function(i) i.LN_SUB_TYPE).ThenBy(Function(i) i.LN_MKT_CODE).ThenBy(Function(i) i.ISICCODE).ThenBy(Function(s) s.ISICCODESUBLEVEL).ToList()
        gvLimit.DataSource = ls
        gvLimit.DataBind()
    End Sub

    Private Sub GetDetialData()
        gvLimit.DataSource = New List(Of String)
        gvLimit.DataBind()
    End Sub

    Protected Sub gvLimit_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLimit.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        Dim lblIndustryLimitPercentage As Label = CType(e.Row.FindControl("lblIndustryLimitPercentage"), Label)
        Dim lblIndustryLimitAmount As Label = CType(e.Row.FindControl("lblIndustryLimitAmount"), Label)

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
            If String.IsNullOrEmpty(lblIndustryLimitPercentage.Text) = True Or lblIndustryLimitPercentage.Text = "-" Then
                lblIndustryLimitPercentage.Text = "-"
            End If
            If String.IsNullOrEmpty(lblIndustryLimitAmount.Text) = True Or lblIndustryLimitAmount.Text = "-" Then
                lblIndustryLimitAmount.Text = "-"
                e.Row.Cells(4).CssClass = "TextCenter"
            Else
                e.Row.Cells(4).CssClass = "TextRight"
            End If
        End If
    End Sub

    Protected Sub gvLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLimit.SelectedIndexChanged
        Dim lblISICCODE As Label = CType(gvLimit.SelectedRow.FindControl("lblISICCODE"), Label)
        Dim lblISICCODESUBLEVEL As Label = CType(gvLimit.SelectedRow.FindControl("lblISICCODESUBLEVEL"), Label)
        Dim lsDetial As New List(Of IndustyDetailEntity)

        lsDetial = Session("DetailList")
        Dim index As Integer = lsDetial.FindIndex(Function(c) c.ISICCODE = lblISICCODE.Text And c.ISICCODESUBLEVEL = lblISICCODESUBLEVEL.Text)

        Session("rowIndex") = index
        Session("DetailList") = lsDetial
        headEntity = GetHeadderDetail()
        Session("HeaderList") = headEntity
        Response.Redirect("~/IndustryLimit/EffectiveDateDetail_Edit.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim _chkISIC As Boolean = True
            Dim _result As Boolean
            _result = False

            'Validate Data
            If String.IsNullOrEmpty(txtEffectiveDate1.Text) And String.IsNullOrEmpty(txtEffectiveDate2.Text) And String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาเลือก วันที่มีผลบังคับใช้", "", "ปิด", False, True)
                Return
            End If

            If Not String.IsNullOrEmpty(txtEffectiveDate1.Text) Then
                _chkISIC = lmBiz.CheckEffectiveDate(txtEffectiveDate1.Text)
            ElseIf Not String.IsNullOrEmpty(txtEffectiveDate2.Text) Then
                _chkISIC = lmBiz.CheckEffectiveDate(txtEffectiveDate2.Text)
            ElseIf Not String.IsNullOrEmpty(txtEffectiveDate3.Text) Then
                _chkISIC = lmBiz.CheckEffectiveDate(txtEffectiveDate3.Text)
            End If

            If chkIsAgree1.Checked = False And chkIsAgree2.Checked = False And chkIsAgree3.Checked = False And chkIsApprove1.Checked = False And chkIsApprove2.Checked = False And chkIsApprove3.Checked = False Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาเลือก เห็นชอบ หรือ อนุมัติ", "", "ปิด", False, True)
                Return
            End If
            If _chkISIC = False Then
                MessageBoxAlert("แจ้งเตือน", "ไม่สามารถบันทึกได้เนื่องจากมี EffectiveDate นี้อยู่่ในระบบแล้ว", "", "ปิด", False, True)
                Return
            End If
            If gvLimit.Rows.Count = 0 Then
                MessageBoxAlert("แจ้งเตือน", "กรุณาเพิ่มข้อมูลรายละเอียด IndustyLimit", "", "ปิด", False, True)
                Return
            End If

            'Get Header
            headEntity = GetHeadderDetail()
            Session("HeaderList") = headEntity

            'Insert Header
            Dim _entHeader = TryCast(CObj(Session("HeaderList")), IndustyHeaderEntity)
            _result = lmBiz.AddNewHeaderIndustryLimit(_entHeader)

            If _result = False Then
                MessageBoxAlert("Error", "บันทึกข้อมูลไม่สำเร็จ", "", "ปิด", False, True)
            End If

            'Get Detail
            Dim _entDetail As List(Of IndustyLimitDetailEntity)
            Dim _efDate As DateTime
            Dim _frDate As String
            _efDate = DateTime.ParseExact(_entHeader.EffectiveDate, "dd/MM/yyyy", Nothing)
            _efDate = _efDate.AddYears(-543)
            _frDate = String.Format("{0:yyyy-MM-dd}", _efDate)

            _entDetail = GetDataDetail(_frDate)

            _result = lmBiz.AddDetailIndustryLimit(_entDetail)

            If _result = False Then
                MessageBoxAlert("Error", "บันทึกข้อมูลไม่สำเร็จ", "", "ปิด", False, True)
            End If

            ClearSession()

            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "ปิด", "", True, False)
            btn_OK.Attributes.Remove("data-dismiss")
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("EffectiveDateDetail_Add", "btnSave_Click()", ex.Message)
        End Try
    End Sub

    Private Sub ClearSession()
        Session("HeaderList") = Nothing
        Session("DetailList") = Nothing
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
        Response.Redirect("~/IndustryLimit/IndustryLimit.aspx")
    End Sub

    Private Function GetDataDetail(ByVal _effectiveDate As String) As List(Of IndustyLimitDetailEntity)

        Dim _Result As New List(Of IndustyLimitDetailEntity)
        Dim _detailList = TryCast(CObj(Session("DetailList")), List(Of IndustyDetailEntity))

        For Each ent In _detailList
            Dim entDetail As New IndustyLimitDetailEntity

            If ent.Type = 1 Then
                entDetail.Level1 = ent.ISICCODE
                entDetail.Level2 = ent.ISICCODESUBLEVEL
                entDetail.Level3 = ""
                entDetail.IndustryDesc = ent.Industry
                entDetail.LoantypeDesc = ""
            Else
                entDetail.Level1 = ent.LN_TYPE_CODE
                entDetail.Level2 = ent.LN_SUB_TYPE
                entDetail.Level3 = ent.LN_MKT_CODE
                entDetail.IndustryDesc = ""
                entDetail.LoantypeDesc = ent.LoanType
            End If

            entDetail.Type = ent.Type
            entDetail.IndustryLimitAmount = ent.IndustryLimitAmount
            entDetail.IndustryLimitPercentage = ent.IndustryLimitPercentage
            entDetail.EffectiveDate = _effectiveDate

            _Result.Add(entDetail)
        Next

        Return _Result
    End Function

End Class