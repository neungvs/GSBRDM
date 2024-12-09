Imports Arsoft.Utility
Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class Stress_Scenario
    Inherits System.Web.UI.Page
    Private _stressScenarioBiz As New StressScenarioBiz
    Private _timeBiz As New TimeBiz
    Dim MessageBox_Result As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("mode") = ""
            ViewState("TimeId") = ""
            ViewState("ScenarioId") = ""
            BindDropDownListTime()
            BindDropDownListTimeCreateNew()
            tbStressScenarioHeader.Visible = False
            gvStressScenario.Visible = False
        End If
    End Sub

#Region "Sub "
    Private Sub BindDropDownListTime()
        ddlTime.DataSource = _stressScenarioBiz.GetDate()
        ddlTime.DataValueField = "TimeId"
        ddlTime.DataTextField = "TimeName"
        ddlTime.DataBind()
    End Sub

    Private Sub BindDropDownListTimeCreateNew()
        Dim lstNewTime As List(Of TimeEntity)
        Dim lstExistingTime As List(Of TimeEntity)
        lstExistingTime = _stressScenarioBiz.GetDate()
        lstNewTime = _timeBiz.GetDateCreateNew()

        For Each te As TimeEntity In lstExistingTime
            For Each tn As TimeEntity In lstNewTime
                If (tn.TimeId = te.TimeId) Then
                    lstNewTime.RemoveAll(Function(p) p.TimeId = te.TimeId)
                    Exit For
                End If
            Next
        Next

        ddlTimeCreateNew.DataSource = lstNewTime
        ddlTimeCreateNew.DataValueField = "TimeId"
        ddlTimeCreateNew.DataTextField = "TimeName"
        ddlTimeCreateNew.DataBind()
    End Sub

    Protected Sub MessageBoxAlert(ByVal title As String, ByVal _message As String, ByVal BtnOKString As String, ByVal BtnNOString As String, ByVal YesbtnStatus As Boolean, ByVal NobtnStatus As Boolean)
        lbl_Title.Text = title
        If title = "Error" Then
            Symbol_Image.ImageUrl = "~/Images/NotCorrect.png"
        ElseIf title = "Success" Then
            Symbol_Image.ImageUrl = "~/Images/Correct.png"
        ElseIf title = "Question" Then
            Symbol_Image.ImageUrl = "~/Images/Question.png"
        ElseIf title = "Warning" Then
            Symbol_Image.ImageUrl = "~/Images/Warning.png"
        ElseIf title = "Information" Then
            Symbol_Image.ImageUrl = "~/Images/Infomation.png"
        End If
        Messages.Text = _message
        btn_OK.Visible = YesbtnStatus
        btn_NO.Visible = NobtnStatus
        btn_OK.Text = BtnOKString
        btn_NO.Text = BtnNOString
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertBox", "$('#AlertBox').modal();", True)
        UpdModal.Update()
    End Sub

    Protected Sub MessageBoxAlert2(ByVal title As String, ByVal _message As String, ByVal BtnOKString As String, ByVal BtnNOString As String, ByVal YesbtnStatus As Boolean, ByVal NobtnStatus As Boolean)
        lbl_Title.Text = title
        If title = "Error" Then
            Symbol_Image.ImageUrl = "~/Images/NotCorrect.png"
        ElseIf title = "Success" Then
            Symbol_Image.ImageUrl = "~/Images/Correct.png"
        ElseIf title = "Question" Then
            Symbol_Image.ImageUrl = "~/Images/Question.png"
        ElseIf title = "Warning" Then
            Symbol_Image.ImageUrl = "~/Images/Warning.png"
        ElseIf title = "Information" Then
            Symbol_Image.ImageUrl = "~/Images/Infomation.png"
        End If
        Messages.Text = _message
        btn_OK.Visible = YesbtnStatus
        btn_NO.Visible = NobtnStatus
        btn_OK.Text = BtnOKString
        btn_NO.Text = BtnNOString
        ''ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertBox", "$('#AlertBox').modal();", True)
        'UpdModal.Update()
    End Sub

#End Region

#Region "Function "

    Private Function Vaidate()
        Dim errMsgList As New List(Of String)
        Dim scenarioName As String = txtScenarioName.Text
        Dim timeId As String = ddlTime.SelectedValue
        Dim lstScenario As List(Of ScenarioEntity) = _stressScenarioBiz.GetScenarioByTimeId(timeId)
        'สถานการณ์ภาวะวิกฤต (กรอกได้ทั้ง text,ตัวเลข และต้องห้ามซ้ำตาม list Scenario)
        If ViewState("mode") = "add" Then
            If (scenarioName = "") Then
                errMsgList.Add("กรุณากรอกชื่อสถานการณ์")
            ElseIf (IsExistingScenario(scenarioName, lstScenario) = True) Then
                errMsgList.Add("ชื่อสถานการณ์นี้มีอยู่แล้วในระบบ")
            End If
        ElseIf ViewState("mode") = "edit" Then
            If (scenarioName = "") Then
                errMsgList.Add("กรุณากรอกชื่อสถานการณ์")
            End If
        End If

        Return errMsgList
    End Function

    Function IsExistingScenario(ScenarioName As String, lstEntity As List(Of ScenarioEntity)) As Boolean
        For Each entity As ScenarioEntity In lstEntity
            If (ScenarioName = entity.ScenarioName) Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region

#Region "Search"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGridViewStressScenario()
    End Sub

    Protected Sub btnCreateNew_Click(sender As Object, e As EventArgs) Handles btnCreateNew.Click
        lblModalTitle.Text = "สร้าง (New)"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalCreateNew", "$('#myModalCreateNew').modal();", True)
        UpdModal.Update()
    End Sub
#End Region

#Region "Result"
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ViewState("mode") = "add"
        lblMessage.Visible = False
        lblModalTitle.Text = "เพิ่ม (Add)"
        txtScenarioDesc.Text = ""
        txtScenarioName.Text = ""
        txtScenarioName.Enabled = True
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        UpdModal.Update()
    End Sub

    Private Sub BindGridViewStressScenario()
        Dim _timeId As String = ddlTime.SelectedValue
        gvStressScenario.DataSource = _stressScenarioBiz.GetByTime(_timeId)
        gvStressScenario.DataBind()
        tbStressScenarioHeader.Visible = True
        gvStressScenario.Visible = True
    End Sub

    Protected Sub gvStressScenario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvStressScenario.RowCommand
        If e.CommandName = "Edit" Then
            ViewState("mode") = "edit"
            ' Get the row index from CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            ' Get the GridView row
            Dim row As GridViewRow = gvStressScenario.Rows(rowIndex)
            ' Find the Label control and get its value
            Dim lbScenarioName As Label = CType(row.FindControl("lbScenarioName"), Label)
            Dim lbScenarioDesc As Label = CType(row.FindControl("lbScenarioDesc"), Label)
            Dim lbTimeId As Label = CType(row.FindControl("lbTimeId"), Label)
            Dim lbScenarioId As Label = CType(row.FindControl("lbScenarioId"), Label)
            ViewState("TimeId") = lbTimeId.Text
            ViewState("ScenarioId") = lbScenarioId.Text
            lblModalTitle.Text = "แก้ไข (Edit)"
            txtScenarioName.Text = lbScenarioName.Text
            txtScenarioDesc.Text = lbScenarioDesc.Text
            txtScenarioName.Enabled = False
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            UpdModal.Update()
        End If
    End Sub

    Protected Sub gvStressScenario_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvStressScenario.RowEditing

    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim result As Boolean
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim lbScenarioId As Label = gvStressScenario.Rows(e.RowIndex).Cells(1).FindControl("lbScenarioId")
        Dim timeId As String = ddlTime.SelectedValue
        Dim scenarioId As String = lbScenarioId.Text

        result = _stressScenarioBiz.Delete(timeId, scenarioId, userId)
        Try
            If result = True Then
                BindGridViewStressScenario()
                MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
            End If
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("StressScenario.aspx", "OnRowDeleting()", ex.Message)
        End Try

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvStressScenario.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
        End If

        'If (e.Row.RowType = DataControlRowType.DataRow) Then
        '    Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        '    If btnDelete IsNot Nothing Then
        '        Dim timeId As String = DataBinder.Eval(e.Row.DataItem, "TimeId").ToString()
        '        Dim scenarioId As String = DataBinder.Eval(e.Row.DataItem, "ScenarioId").ToString()
        '        ViewState("mode") = "delete"
        '        ViewState("timeId") = timeId
        '        ViewState("scenarioId") = scenarioId
        '        'btn_OK.Attributes.Remove("data-dismiss")
        '        btnDelete.OnClientClick = String.Format("showModal('{0}','{1}','{2}','{3}','{4}','{5}'); return false;", "Question", "ต้องการลบรายการนี้ ใช่หรือไม่", "ใช่", "ไม่ใช่", True, True)
        '    End If
        'End If
    End Sub

#End Region

#Region "Create New"
    Protected Sub btnSaveCreateNew_Click(sender As Object, e As EventArgs) Handles btnSaveCreateNew.Click
        If SaveCreateNewTime() Then
            BindDropDownListTime()
            ddlTime.SelectedValue = ViewState("CreateNewTimeId")
            BindGridViewStressScenario()
            BindDropDownListTimeCreateNew()
            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
        End If
    End Sub

    Private Function SaveCreateNewTime() As Boolean
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim timeId As Integer = ddlTimeCreateNew.SelectedValue
        ViewState("CreateNewTimeId") = timeId
        Return _stressScenarioBiz.SaveCreateNew(timeId, userId)
    End Function
#End Region

#Region "Add/Edit"
    Protected Sub btnSaveAdd_Click(sender As Object, e As EventArgs) Handles btnSaveAdd.Click
        Dim errMsgList As List(Of String) = Vaidate()
        If errMsgList.Count > 0 Then
            lblMessage.Visible = True
            lblMessage.Text = String.Join(",", errMsgList.ToArray())
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: true});", True)
            UpdModal.Update()
        Else
            If ViewState("mode") = "add" Then
                lblMessage.Visible = False
                If SaveAdd() Then
                    BindGridViewStressScenario()
                    MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                End If
            ElseIf ViewState("mode") = "edit" Then
                Dim timeId As String = ViewState("TimeId")
                Dim scenarioId As String = ViewState("ScenarioId")
                Dim userId As Integer = Convert.ToInt32(Session("UserID"))
                Dim scenarioName As String = txtScenarioName.Text
                Dim scenarioDesc As String = txtScenarioDesc.Text
                If _stressScenarioBiz.SaveUpdate(timeId, scenarioId, scenarioName, scenarioDesc, userId) Then
                    BindGridViewStressScenario()
                    MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                End If
            End If
        End If

        'If ViewState("mode") = "add" Then
        '    Dim errMsgList As List(Of String) = Vaidate()
        '    If errMsgList.Count > 0 Then
        '        lblMessage.Visible = True
        '        lblMessage.Text = String.Join(",", errMsgList.ToArray())
        '        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: true});", True)
        '        UpdModal.Update()
        '    Else
        '        lblMessage.Visible = False
        '        If SaveAdd() Then
        '            BindGridViewStressScenario()
        '            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
        '        Else
        '            MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
        '        End If
        '    End If
        'ElseIf ViewState("mode") = "edit" Then
        '    Dim timeId As String = ViewState("TimeId")
        '    Dim scenarioId As String = ViewState("ScenarioId")
        '    Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        '    Dim scenarioName As String = txtScenarioName.Text
        '    Dim scenarioDesc As String = txtScenarioDesc.Text
        '    If _stressScenarioBiz.SaveUpdate(timeId, scenarioId, scenarioName, scenarioDesc, userId) Then
        '        BindGridViewStressScenario()
        '        MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
        '    Else
        '        MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
        '    End If
        'End If
    End Sub

    Private Function SaveAdd() As Boolean
        Dim scenarioName As String = txtScenarioName.Text
        Dim scenarioDesc As String = txtScenarioDesc.Text
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim timeId As String = ddlTime.SelectedValue
        Return _stressScenarioBiz.SaveAdd(timeId, scenarioName, scenarioDesc, userId)
    End Function

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        'Dim result As Boolean
        'Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        'Dim timeId As String = ViewState("timeId")
        'Dim scenarioId As String = ViewState("scenarioId")

        'result = _stressScenarioBiz.Delete(timeId, scenarioId, userId)
        'Try
        '    If result = True Then
        '        BindGridViewStressScenario()
        '        MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
        '        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertBox", "$('#AlertBox').modal({backdrop: true});", True)
        '        UpdModal.Update()
        '    Else
        '        MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
        '    End If
        'Catch ex As Exception
        '    MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
        '    UtilLogfile.writeToLog("StressScenario.aspx", "OnRowDeleting()", ex.Message)
        'End Try

    End Sub

#End Region

End Class