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




Public Class WF52_RejEdit

    Inherits System.Web.UI.Page

    Dim wfBiz As New WF52Biz

    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            Dim cifNumber As String = Session("CifNumber").ToString()
            Dim accNO As String = Session("ACCNO").ToString()
            Dim tempId As String = Session("TMPID").ToString()

            lblCifNumber.Text = cifNumber
            lblAccno.Text = accNO

            GetRejData(tempId)

            'SetControl()

        End If

    End Sub


    Private Sub GetRejData(tempId As String)

        gvWF52.DataSource = wfBiz.GetRejData(tempId)
        gvWF52.DataBind()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            If Not String.IsNullOrEmpty(txtEditValue.Text) Then

                Dim result As Boolean = False

                result = UtilsBiz.checkForSQLInjection(txtEditValue.Text.Trim())

                If result = True Then

                    UtilsBiz.CreateMessageAlert(Page, "ตรวจพบคำสั่ง sqlInjection ไม่สามารถบันทึกข้อมูลได้", gui)
                    Return

                End If


            End If





            wfBiz.Save_edit(Session("TMPID").ToString(), HF_ColumnValue.Value, txtEditValue.Text)


            UtilsBiz.CreateMessageAlert(Page, "บันทึกเรียบร้อย", gui)


            GetRejData(Session("TMPID").ToString())

            txtEditValue.Text = String.Empty
            pnlEdit.Visible = False


        Catch ex As Exception

            UtilsBiz.CreateMessageAlert(Page, ex.Message, gui)

        End Try

    End Sub

    Protected Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click

        txtSeq.Text = String.Empty
        txtColumnName.Text = String.Empty
        txtValue.Text = String.Empty
        txtEditValue.Text = String.Empty


        pnlEdit.Visible = False

    End Sub

    Protected Sub gvWF52_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvWF52.SelectedIndexChanged

        Dim HF_DataRecordID As HiddenField = CType(gvWF52.SelectedRow.FindControl("HF_DataRecordID"), HiddenField)

        Dim lblColumnName As Label = CType(gvWF52.SelectedRow.FindControl("lblColumnName"), Label)

        Dim lblColumnValue As Label = CType(gvWF52.SelectedRow.FindControl("lblColumnValue"), Label)

        Dim lblRejectReason As Label = CType(gvWF52.SelectedRow.FindControl("lblRejectReason"), Label)

        Dim lblSeq As Label = CType(gvWF52.SelectedRow.FindControl("lblSeq"), Label)


        Dim HF_ColumnName As HiddenField = CType(gvWF52.SelectedRow.FindControl("HF_ColumnName"), HiddenField)



        SetValidation(HF_ColumnName.Value)


        txtEditValue.Text = String.Empty


        HF_ColumnValue.Value = HF_ColumnName.Value

        txtSeq.Text = lblSeq.Text
        txtColumnName.Text = lblColumnName.Text
        txtValue.Text = lblColumnValue.Text
        pnlEdit.Visible = True



    End Sub


    Private Sub SetValidation(columnName As String)

        If (New String() {"MATURITY_DATE", "ORIGINATION_DATE", "SETTLEMENT_DATE", "TRADE_DATE"}).Contains(columnName) Then

            txtEditValue.MaxLength = 8

            rgLenght.ValidationExpression = "^(\d{8})?$"
            rgLenght.ErrorMessage = "ข้อมูลต้องเป็นตัวเลข 8 หลัก"
            lblrgEx.Text = "ข้อมูลต้องเป็นตัวเลข 8 หลัก"
            rgLenght.Enabled = True


            rgFormat.ValidationExpression = "(0[1-9]|[12][0-9]|3[01])(0[1-9]|1[012])(19|20)\d\d"
            rgFormat.ErrorMessage = "ข้อมูลต้องอยู่ในรูปแบบ ddMMyyyy โดยที่ yyyy คือ ปี ค.ศ"
            rgFormat.Enabled = True


        ElseIf (New String() {"GL_ACCOUNT_ID"}).Contains(columnName) Then

            txtEditValue.MaxLength = 8
            rgLenght.ValidationExpression = "^(\d{8})?$"
            rgLenght.ErrorMessage = "ข้อมูลต้องเป็นตัวเลข 8 หลัก"
            lblrgEx.Text = "ข้อมูลต้องเป็นตัวเลข 8 หลัก"
            rgLenght.Enabled = True

            rgFormat.ValidationExpression = "^[0-9]*$"
            rgFormat.ErrorMessage = "ข้อมูลต้องอยู่ในรูปแบบของตัวเลข 0-9 เท่านั้น"
            rgFormat.Enabled = True



        ElseIf (New String() {"CUR_GROSS_RATE"}).Contains(columnName) Then

            txtEditValue.MaxLength = 50
            rgLenght.Enabled = False


            rgFormat.ValidationExpression = "^(?:[1-9][0-9]{0,9}|(?![0-9.]{22})(?:0\.[0-9]*[1-9][0-9]*|[1-9][0-9]*\.[0-9]+))$"
            rgFormat.ErrorMessage = "ข้อมูลต้องอยู่ในรูปแบบของตัวเลข 0-9 เท่านั้น และมีขนาดไม่เกิน 10 หลัก (ไม่รวมทศนิยม)"
            rgFormat.Enabled = True


        Else

            txtEditValue.MaxLength = 50
            rgLenght.Enabled = False
            rgFormat.Enabled = False

        End If

    End Sub


    Protected Sub gvWF52_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvWF52.RowDataBound


        Dim HF_ColumnName As HiddenField = CType(e.Row.FindControl("HF_ColumnName"), HiddenField)

        Dim lblColumnName As Label = CType(e.Row.FindControl("lblColumnName"), Label)

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            If HF_ColumnName.Value = "GL_ACCOUNT_ID" Then

                lblColumnName.Text = "รหัสบัญชีของธนาคาร (GL_ACCOUNT_ID)"

            ElseIf HF_ColumnName.Value = "CUR_GROSS_RATE" Then
                lblColumnName.Text = "อัตราดอกเบี้ยปัจจุบันที่ลูกค้าจ่าย (CUR_GROSS_RATE)"

            ElseIf HF_ColumnName.Value = "SETTLEMENT_DATE" Then
                lblColumnName.Text = "วันที่ครบกำหนดส่งมอบ (SETTLEMENT_DATE)"

            ElseIf HF_ColumnName.Value = "ORIGINATION_DATE" Then
                lblColumnName.Text = "วันที่เริ่มดำเนินการกับธนาคารของแต่ละบัญชี (ORIGINATION_DATE)"

            End If


        End If

    End Sub

    Protected Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click

        lblModalTitle.Text = "คำอธิบายรูปแบบข้อมูลยอดคงเหลือหลักทรัพย์ของ Repo ขากู้ -> WF52"
        'lblModalBody.Text = "This is modal body"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()

    End Sub


    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Response.Redirect("WF52_RejList.aspx")

    End Sub

    Private Sub SetControl()

        Dim liAj As HtmlGenericControl = Page.Master.FindControl("liAj")
        liAj.Attributes.Add("class", "active")

    End Sub







End Class