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


Public Class IndustryLimit
    Inherits System.Web.UI.Page
    Dim lmBiz As New IndustryBiz
    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            LoadData()
            ClearSession()
        End If
    End Sub

    Private Sub LoadData()
        gvLimit.DataSource = lmBiz.GetDataIndustyLimit()
        gvLimit.DataBind()
    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim _result As Boolean = False
        Dim lblEffectiveDate As Label = gvLimit.Rows(e.RowIndex).Cells(1).FindControl("lblEffectiveDate")
        _result = lmBiz.DeleteByEffective(lblEffectiveDate.Text)

        Try
            If _result = True Then
                LoadData()
                MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
            End If
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("IndustryLimit.aspx", "OnRowDeleting()", ex.Message)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLimit.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
        End If
    End Sub

    Protected Sub gvLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLimit.SelectedIndexChanged
        Session("effectiveDate") = ""
        Dim lblEffectiveDate As Label = CType(gvLimit.SelectedRow.FindControl("lblEffectiveDate"), Label)

        Session("effectiveDate") = lblEffectiveDate.Text
        Response.Redirect("~/IndustryLimit/ModifyIndustryLimit.aspx")
    End Sub

    Private Sub SetControl()
        Dim liId As HtmlGenericControl = Page.Master.FindControl("liId")
        liId.Attributes.Add("class", "active")
    End Sub

    Protected Sub gvLimit_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvLimit.PageIndexChanging
        gvLimit.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ClearSession()
        Response.Redirect("~/IndustryLimit/EffectiveDate_Add.aspx")
    End Sub

    Private Sub ClearSession()
        Session("effectiveDate") = Nothing
        Session("HeaderList") = Nothing
        Session("DetailList") = Nothing
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