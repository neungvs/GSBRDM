Imports Arsoft.Utility
Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class ScenarioWeightName
    Inherits System.Web.UI.Page
    Private _stressScenarioWeightBiz As New StressScenarioWeightBiz
    Private _timeBiz As New TimeBiz
    Private _valBiz As New ValidateBiz
    Private _dateBiz As New DateHelperUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("mode") = ""
            ViewState("TimeId") = ""
            ViewState("ScenarioId") = ""
            LoadTime()
            LoadTimeCreateNew()
            tbStressScenarioHeader.Visible = False
            gvStressScenario.Visible = False
        End If
    End Sub

    Private Sub LoadTime()
        Dim _lsTime As List(Of TimeEntity)
        _lsTime = _stressScenarioWeightBiz.GetTime()
        If (_lsTime.Count = 0) Then
            cb_List_Time.Items.Add(New ListItem("ไม่มีข้อมูล", String.Empty))
        Else
            cb_List_Time.DataSource = _lsTime
            cb_List_Time.DataValueField = "TimeId"
            cb_List_Time.DataTextField = "TimeName"
            cb_List_Time.DataBind()
        End If
    End Sub

    Private Sub LoadTimeCreateNew()
        Dim _lsValidTime As New List(Of TimeEntity)
        Dim _lsNewTime As List(Of TimeEntity)
        Dim _lstExistingTime As List(Of TimeEntity)
        _lstExistingTime = _stressScenarioWeightBiz.GetTime()
        _lsNewTime = _timeBiz.GetDateCreateNew()

        For Each te As TimeEntity In _lstExistingTime
            For Each tn As TimeEntity In _lsNewTime
                If (tn.TimeId = te.TimeId) Then
                    _lsNewTime.RemoveAll(Function(p) p.TimeId = te.TimeId)
                    Exit For
                End If
            Next
        Next

        ddlTimeCreateNew.DataSource = _lsNewTime
        ddlTimeCreateNew.DataValueField = "TimeId"
        ddlTimeCreateNew.DataTextField = "TimeName"
        ddlTimeCreateNew.DataBind()
    End Sub

    Protected Sub btn_Searchr_Click(sender As Object, e As EventArgs) Handles btn_Searchr.Click
        LoadData()
    End Sub

    Private Function GetLastDayOfMonth() As String
        Dim _timeId As String = cb_List_Time.SelectedValue
        Dim _year As String = _timeId.Substring(0, 4)
        Dim _month As String = _timeId.Substring(4, 2)
        _timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        Return _timeId
    End Function

    Private Sub LoadData()
        Dim listData As List(Of StressScenarioEntity)
        Dim _timeId As String = GetLastDayOfMonth()
        listData = _stressScenarioWeightBiz.GetByTime(_timeId)
        gvStressScenario.DataSource = listData
        gvStressScenario.DataBind()
        tbStressScenarioHeader.Visible = True
        gvStressScenario.Visible = True
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ViewState("mode") = "add"
        lblMessage.Visible = False
        lblModalTitle.Text = "เพิ่ม (Add)"
        txtScenarioDesc.Text = ""
        txtScenarioName.Text = ""
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        UpdModal.Update()
    End Sub

    Protected Sub btnCreateNew_Click(sender As Object, e As EventArgs) Handles btnCreateNew.Click
        lblModalTitle.Text = "สร้าง (New)"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalCreateNew", "$('#myModalCreateNew').modal();", True)
        UpdModal.Update()
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

    Protected Sub btnSaveCreateNew_Click(sender As Object, e As EventArgs) Handles btnSaveCreateNew.Click
        If SaveCreateNewTime() Then
            LoadTime()
            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
        Else
            MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
        End If
    End Sub

    Private Function LoadScenario() As List(Of ScenarioEntity)
        Dim _timeId As String = GetLastDayOfMonth()
        Return _timeBiz.GetScenario(_timeId)
    End Function

    Private Function VaidateScenarioName()
        Dim errMsgList As New List(Of String)
        Dim scenarioName As String = txtScenarioName.Text
        Dim lstScenario As List(Of ScenarioEntity) = LoadScenario()
        'สถานการณ์ภาวะวิกฤต = กรอกได้ทั้ง text,ตัวเลข และต้องห้ามซ้ำตาม list Scenario
        If (scenarioName = "") Then
            errMsgList.Add("กรุณากรอกชื่อสถานการณ์")
        ElseIf (IsExistingScenario(scenarioName, lstScenario) = True) Then
            errMsgList.Add("ชื่อสถานการณ์นี้มีอยู่แล้วในระบบ")
        End If
        Return errMsgList
    End Function

    Function IsExistingScenario(Scenario As String, lstScenario As List(Of ScenarioEntity)) As Boolean
        For Each _senario As ScenarioEntity In lstScenario
            If (Scenario = _senario.ScenarioName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub btnSaveAdd_Click(sender As Object, e As EventArgs) Handles btnSaveAdd.Click
        If ViewState("mode") = "add" Then
            Dim errMsgList As List(Of String) = VaidateScenarioName()
            If errMsgList.Count > 0 Then
                lblMessage.Visible = True
                lblMessage.Text = String.Join(",", errMsgList.ToArray())
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: true});", True)
                UpdModal.Update()
            Else
                lblMessage.Visible = False
                If SaveAdd() Then
                    LoadData()
                    MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                End If
            End If
        ElseIf ViewState("mode") = "edit" Then
            Dim timeId As String = ViewState("TimeId")
            Dim scenarioId As String = ViewState("ScenarioId")
            Dim userId As Integer = Convert.ToInt16(Session("UserID"))
            If _stressScenarioWeightBiz.SaveUpdate(timeId, scenarioId, txtScenarioName.Text, txtScenarioDesc.Text, userId) Then
                LoadData()
                MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
            End If
        End If
    End Sub

    Private Function SaveCreateNewTime() As Boolean
        Dim userId As Integer = Convert.ToInt16(Session("UserID"))
        Return _stressScenarioWeightBiz.SaveCreateNew(ddlTimeCreateNew.SelectedValue, userId)
    End Function

    Private Function SaveAdd() As Boolean
        Dim _scenarioName As String = txtScenarioName.Text
        Dim _scenarioDesc As String = txtScenarioDesc.Text
        Dim _userId As Integer = Convert.ToInt16(Session("UserID"))
        Dim _timeId As String = GetLastDayOfMonth()
        Return _stressScenarioWeightBiz.SaveAdd(_timeId, _scenarioName, txtScenarioDesc.Text, _userId)
    End Function

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim _result As Boolean = False
        Dim userId As Integer = Convert.ToInt16(Session("UserID"))
        Dim lbScenarioId As Label = gvStressScenario.Rows(e.RowIndex).Cells(1).FindControl("lbScenarioId")
        Dim _timeId As String = GetLastDayOfMonth()
        _result = _stressScenarioWeightBiz.Delete(_timeId, lbScenarioId.Text, userId)
        Try
            If _result = True Then
                LoadData()
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            UpdModal.Update()
        End If
    End Sub

    Protected Sub gvStressScenario_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvStressScenario.RowEditing

    End Sub

End Class