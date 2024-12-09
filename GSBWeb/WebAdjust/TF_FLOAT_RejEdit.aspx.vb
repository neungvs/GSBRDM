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





Public Class TF_FLOAT_RejEdit
    Inherits System.Web.UI.Page

    Dim tfBiz As New TF_FLOATBiz

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

        gvTF_FLOAT.DataSource = tfBiz.GetRejData(tempId)
        gvTF_FLOAT.DataBind()

    End Sub



    Protected Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click

        txtSeq.Text = String.Empty
        txtColumnName.Text = String.Empty
        txtValue.Text = String.Empty
        txtEditValue.Text = String.Empty

        pnlEdit.Visible = False


    End Sub




    Protected Sub gvTF_FLOAT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTF_FLOAT.SelectedIndexChanged

        Dim HF_DataRecordID As HiddenField = CType(gvTF_FLOAT.SelectedRow.FindControl("HF_DataRecordID"), HiddenField)

        Dim lblColumnName As Label = CType(gvTF_FLOAT.SelectedRow.FindControl("lblColumnName"), Label)

        Dim lblColumnValue As Label = CType(gvTF_FLOAT.SelectedRow.FindControl("lblColumnValue"), Label)

        Dim lblRejectReason As Label = CType(gvTF_FLOAT.SelectedRow.FindControl("lblRejectReason"), Label)

        Dim lblSeq As Label = CType(gvTF_FLOAT.SelectedRow.FindControl("lblSeq"), Label)


        Dim HF_ColumnName As HiddenField = CType(gvTF_FLOAT.SelectedRow.FindControl("HF_ColumnName"), HiddenField)



        SetValidation(HF_ColumnName.Value)


        txtEditValue.Text = String.Empty


        HF_ColumnValue.Value = HF_ColumnName.Value

        txtSeq.Text = lblSeq.Text
        txtColumnName.Text = lblColumnName.Text
        txtValue.Text = lblColumnValue.Text
        pnlEdit.Visible = True



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

            tfBiz.Save_edit(Session("TMPID").ToString(), HF_ColumnValue.Value, txtEditValue.Text)


            UtilsBiz.CreateMessageAlert(Page, "บันทึกเรียบร้อย", gui)


            GetRejData(Session("TMPID").ToString())

            txtEditValue.Text = String.Empty
            pnlEdit.Visible = False

        Catch ex As Exception

            UtilsBiz.CreateMessageAlert(Page, ex.Message, gui)

        End Try


    End Sub

    Private Sub SetValidation(columnName As String)

        If (New String() {"CREATIONDATE", "StartDate", "EndDate"}).Contains(columnName) Then

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


        ElseIf (New String() {"AccruedInterest", "PrincipalAmount", "PrincipalOutstanding", "OutstandingInFCY"}).Contains(columnName) Then

            txtEditValue.MaxLength = 50
            rgLenght.Enabled = False


            rgFormat.ValidationExpression = "^(?:[1-9][0-9]{0,17}|(?![0-9.]{24})(?:0\.[0-9]*[1-9][0-9]*|[1-9][0-9]*\.[0-9]+))$"
            rgFormat.ErrorMessage = "ข้อมูลต้องอยู่ในรูปแบบของตัวเลข 0-9 เท่านั้น และมีขนาดไม่เกิน 18 หลัก (ไม่รวมทศนิยม)"
            rgFormat.Enabled = True


        ElseIf (New String() {"Coupon_Rate"}).Contains(columnName) Then

            txtEditValue.MaxLength = 50
            rgLenght.Enabled = False


            rgFormat.ValidationExpression = "^(?:[1-9][0-9]{0,3}|(?![0-9.]{10})(?:0\.[0-9]*[1-9][0-9]*|[1-9][0-9]*\.[0-9]+))$"
            rgFormat.ErrorMessage = "ข้อมูลต้องอยู่ในรูปแบบของตัวเลข 0-9 เท่านั้น และมีขนาดไม่เกิน 4 หลัก (ไม่รวมทศนิยม)"
            rgFormat.Enabled = True


        ElseIf (New String() {"Currency_Original", "PositionID", "ASSET_CLASS_TYPE"}).Contains(columnName) Then

            txtEditValue.MaxLength = 50

            rgLenght.Enabled = False

            rgFormat.Enabled = False


        Else

            txtEditValue.MaxLength = 50
            rgLenght.Enabled = False
            rgFormat.Enabled = False

        End If


    End Sub




    Protected Sub gvTF_FLOAT_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTF_FLOAT.RowDataBound

        Dim HF_ColumnName As HiddenField = CType(e.Row.FindControl("HF_ColumnName"), HiddenField)

        Dim lblColumnName As Label = CType(e.Row.FindControl("lblColumnName"), Label)

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            If HF_ColumnName.Value = "GL_ACCOUNT" Then

                lblColumnName.Text = "รหัสบัญชี (GL_ACCOUNT)"

            ElseIf HF_ColumnName.Value = "StartDate" Then
                lblColumnName.Text = "วันจ่ายเงินกู้ (StartDate)"

            ElseIf HF_ColumnName.Value = "EndDate" Then
                lblColumnName.Text = "วันสิ้นสุดสัญญา (EndDate)"

            ElseIf HF_ColumnName.Value = "Currency_Original" Then
                lblColumnName.Text = "สกุลเงิน (Currency_Original)"

            ElseIf HF_ColumnName.Value = "ASSET_CLASS_TYPE" Then
                lblColumnName.Text = "ระดับการจัดชั้นหนี้ตามยอดค้างชำระ (ASSET_CLASS_TYPE)"

            End If


        End If

    End Sub

    Protected Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click

        lblModalTitle.Text = "คำอธิบายรูปแบบข้อมูลบัญชีเงินกู้ในธุรกิจ Trade Finance ที่มีอัตราดอกเบี้ยลอยตัว -> TF_FLOAT"
        'lblModalBody.Text = "This is modal body"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()

    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Response.Redirect("TF_FLOAT_RejList.aspx")
    End Sub

    Private Sub SetControl()

        Dim liAj As HtmlGenericControl = Page.Master.FindControl("liAj")
        liAj.Attributes.Add("class", "active")

    End Sub

End Class