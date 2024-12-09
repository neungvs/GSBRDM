Imports GSBWeb.BLL
Imports Arsoft.Utility

Public Class SettingReceiveMail
    Inherits System.Web.UI.Page

    Dim emBiz As New EmailBiz
    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LoadData()
        End If
    End Sub

    Private Sub LoadData()
        gvEmail.DataSource = emBiz.GetDataReceiveMail("CPM 13.3")
        gvEmail.DataBind()
    End Sub

    Private Sub ClearData()
        txtEmail.Text = ""
        hdEmail.Value = ""
        LoadData()
    End Sub


    Protected Sub gvEmail_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvEmail.PageIndexChanging
        gvEmail.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        txtEmail.Text = ""
        hdEmail.Value = ""
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim _chkEmail As Boolean = True
            Dim _result As Boolean
            _result = False

            If Not String.IsNullOrEmpty(txtEmail.Text) Then
                _chkEmail = emBiz.CheckEmail(txtEmail.Text, "CPM 13.3")
            End If

            If _chkEmail = False Then
                MessageBoxAlert("แจ้งเตือน", "ไม่สามารถบันทึกได้เนื่องจากมี Email นี้อยู่่ในระบบแล้ว", "", "ปิด", False, True)
                Return
            End If

            If Not String.IsNullOrEmpty(hdEmail.Value) Then
                emBiz.UpdateEmail(txtEmail.Text, hdEmail.Value, "CPM 13.3")
            Else
                emBiz.InsertEmail(txtEmail.Text, "CPM 13.3")
            End If


            ClearData()
            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "ปิด", "", True, False)
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("SettingReceiveMail", "btnSave_Click()", ex.Message)
        End Try
        'reqtxtEmail.va()
    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim _result As Boolean = False
        Dim lblEmail As Label = gvEmail.Rows(e.RowIndex).FindControl("lblEmail")

        _result = emBiz.DeleteEmail(lblEmail.Text, "CPM 13.3")

        Try
            If _result = True Then
                MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
                LoadData()
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
            End If
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("SettingReceiveMail", "OnRowDeleting()", ex.Message)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvEmail.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
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



    Protected Sub gvEmail_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)

    End Sub

    Protected Sub gvEmail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEmail.SelectedIndexChanged
        'Dim lblID As Label = CType(gvEmail.SelectedRow.FindControl("lblID"), Label)
        Dim lblEmail As Label = CType(gvEmail.SelectedRow.FindControl("lblEmail"), Label)

        txtEmail.Text = lblEmail.Text
        hdEmail.Value = lblEmail.Text

    End Sub
End Class