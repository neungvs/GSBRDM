Imports Arsoft.Utility
Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class Scenario_Weight
    Inherits System.Web.UI.Page

    Private _scenarioWeightBiz As New ScenarioWeightBiz
    Private _scenarioScenarioWeightBiz As New StressScenarioWeightBiz
    Private _timeBiz As New TimeBiz
    Private _valBiz As New ValidateBiz
    Private _dateBiz As New DateHelperUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("mode") = ""
            ViewState("TimeId") = ""
            ViewState("FactorId") = ""
            LoadTime()
            LoadScenario()
            tbScenarioWeightHeader.Visible = False
            gvScenarioWeight.Visible = False
        End If
    End Sub

    Private Sub LoadTime()
        Dim _lsTime As List(Of TimeEntity)
        _lsTime = _scenarioScenarioWeightBiz.GetTime()
        ddlTime.DataSource = _lsTime
        ddlTime.DataValueField = "TimeId"
        ddlTime.DataTextField = "TimeName"
        ddlTime.DataBind()
    End Sub

    Private Sub LoadScenario()
        Dim _timeId As String = ddlTime.SelectedValue
        Dim _lstScenario As List(Of ScenarioEntity)
        _lstScenario = _scenarioScenarioWeightBiz.GetScenarioByTime(_timeId)
        ddlAddEditScenario.DataSource = _lstScenario
        ddlAddEditScenario.DataValueField = "ScenarioId"
        ddlAddEditScenario.DataTextField = "ScenarioName"
        ddlAddEditScenario.DataBind()
    End Sub

    Protected Sub btn_Searchr_Click(sender As Object, e As EventArgs) Handles btn_Searchr.Click
        BindGridData()
        LoadScenario()
    End Sub

    Private Function GetLastDayOfMonth() As String
        Dim _timeId As String = ddlTime.SelectedValue
        Dim _year As String = _timeId.Substring(0, 4)
        Dim _month As String = _timeId.Substring(4, 2)
        _timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        Return _timeId
    End Function

    Private Function GetCreateNewLastDayOfMonth() As String
        Dim _timeId As String = ddlTime.SelectedValue
        Dim _year As String = _timeId.Substring(0, 4)
        Dim _month As String = _timeId.Substring(4, 2)
        _timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        Return _timeId
    End Function

    Private Sub BindGridData()
        Dim listData As List(Of ScenarioWeightEntity)
        Dim _html As New StringBuilder
        Dim _timeId As String = GetLastDayOfMonth()
        listData = _scenarioWeightBiz.GetByTime(_timeId)
        gvScenarioWeight.DataSource = listData
        gvScenarioWeight.DataBind()
        tbScenarioWeightHeader.Visible = True
        gvScenarioWeight.Visible = True
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ViewState("mode") = "add"
        lblMessage.Visible = False
        lblModalTitle.Text = "เพิ่ม (Add)"
        txtAddEditWeight.Text = ""
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        UpdModal.Update()
    End Sub

    'Protected Sub btnCreateNew_Click(sender As Object, e As EventArgs) Handles btnCreateNew.Click
    '    lblModalTitle.Text = "สร้าง (New)"
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalCreateNew", "$('#myModalCreateNew').modal();", True)
    '    UpdModal.Update()
    'End Sub

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

    'Protected Sub btnSaveCreateNew_Click(sender As Object, e As EventArgs) Handles btnSaveCreateNew.Click
    '    If SaveCreateNewTime() Then
    '        LoadTime()
    '        MessageBoxAlert("Success", "Save Data สำเร็จ", "", "ปิด", False, True)
    '    End If
    'End Sub

    'Private Function LoadFactor() As List(Of FactorEntity)
    '    'Dim _timeId As String = GetLastDayOfMonth()
    '    'Return _factorNameBiz.GetFactor(_timeId)
    'End Function

    Private Function Vaidate()
        Dim errMsgList As New List(Of String)
        Dim weight As String = txtAddEditWeight.Text
        'Dim lstFactor As List(Of FactorEntity) = LoadFactor()
        'สถานการณ์ภาวะวิกฤต = กรอกได้ทั้ง text,ตัวเลข และต้องห้ามซ้ำตาม list Scenario
        If (weight = "") Then
            errMsgList.Add("Weight ห้ามว่าง")
            'ElseIf (IsExistingFactorName(factorName, lstFactor) = True) Then
            '    errMsgList.Add("ชื่อตัวแปรทางเศรษฐกิจ ห้ามซ้ำ")
        End If
        Return errMsgList
    End Function

    Function IsExistingFactorName(factorName As String, lstFactor As List(Of FactorEntity)) As Boolean
        For Each entity As FactorEntity In lstFactor
            If (factorName = entity.FactorName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub btnSaveAdd_Click(sender As Object, e As EventArgs) Handles btnSaveAdd.Click
        If ViewState("mode") = "add" Then
            Dim errMsgList As List(Of String) = Vaidate()
            If errMsgList.Count > 0 Then
                lblMessage.Visible = True
                lblMessage.Text = String.Join(",", errMsgList.ToArray())
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: true});", True)
                UpdModal.Update()
            Else
                lblMessage.Visible = False
                If SaveAdd() Then
                    BindGridData()
                    MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                End If
            End If
        ElseIf ViewState("mode") = "edit" Then
            Dim timeId As String = ViewState("TimeId")
            Dim scenarioId As String = ViewState("ScenarioId")
            Dim weight As String = txtAddEditWeight.Text
            If _scenarioWeightBiz.SaveUpdate(timeId, scenarioId, weight) Then
                BindGridData()
                MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
            End If
        End If
    End Sub

    'Private Function SaveCreateNewTime() As Boolean
    '    Dim _userId As Integer = Convert.ToInt16(Session("UserID"))
    '    Dim _timeId As String = GetCreateNewLastDayOfMonth()
    '    Return _factorNameBiz.SaveCreateNew(_timeId, _userId)
    'End Function

    Private Function SaveAdd() As Boolean
        Dim _timeId As String = GetLastDayOfMonth()
        Dim _scenarioId As String = ddlAddEditScenario.SelectedValue
        Dim _weight As String = txtAddEditWeight.Text
        Return _scenarioWeightBiz.SaveAdd(_timeId, _scenarioId, _weight)
    End Function

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim _result As Boolean = False
        Dim lbScenarioId As Label = gvScenarioWeight.Rows(e.RowIndex).Cells(1).FindControl("lbScenarioId")
        Dim _timeId As String = GetLastDayOfMonth()
        _result = _scenarioWeightBiz.Delete(_timeId, lbScenarioId.Text)
        Try
            If _result = True Then
                BindGridData()
                MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
            End If
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("StressScenario.aspx", "OnRowDeleting()", ex.Message)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvScenarioWeight.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
        End If
    End Sub

    Protected Sub gvFactorName_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvScenarioWeight.RowCommand
        If e.CommandName = "Edit" Then
            ViewState("mode") = "edit"
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvScenarioWeight.Rows(rowIndex)
            ' Find the Label control and get its value
            'Dim lbFactorName As Label = CType(row.FindControl("lbFactorName"), Label)
            'Dim lbFactorDesc As Label = CType(row.FindControl("lbFactorDesc"), Label)
            'Dim lbFactorUnit As Label = CType(row.FindControl("lbFactorUnit"), Label)
            Dim lbTimeId As Label = CType(row.FindControl("lbTimeId"), Label)
            Dim lbScenarioId As Label = CType(row.FindControl("lbScenarioId"), Label)
            Dim lbWeight As Label = CType(row.FindControl("lbWeight"), Label)
            ViewState("TimeId") = lbTimeId.Text
            ViewState("ScenarioId") = lbScenarioId.Text
            lblModalTitle.Text = "แก้ไข (Edit)"
            'txtFactorName.Text = lbTimeId.Text
            ddlAddEditScenario.SelectedValue = lbScenarioId.Text
            txtAddEditWeight.Text = lbWeight.Text
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            UpdModal.Update()
        End If
    End Sub

    Protected Sub gvFactorName_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvScenarioWeight.RowEditing

    End Sub

    Protected Sub gvFactorName_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvScenarioWeight.PageIndexChanging
        gvScenarioWeight.PageIndex = e.NewPageIndex
        BindGridData()
    End Sub

End Class