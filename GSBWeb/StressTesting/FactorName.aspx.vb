Imports Arsoft.Utility
Imports GSBWeb.BLL
Imports GSBWeb.DAL

Public Class Factor_Name
    Inherits System.Web.UI.Page

    Private _factorNameBiz As New FactorNameBiz
    Private _timeBiz As New TimeBiz
    Private _valBiz As New ValidateBiz
    Private _dateBiz As New DateHelperUtil

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("mode") = ""
            ViewState("TimeId") = ""
            ViewState("FactorId") = ""
            BindDropDownListTime()
            BindDropDownListTimeCreateNew()
            tbFactorNameHeader.Visible = False
            gvFactorName.Visible = False
        End If
    End Sub

#Region "Sub Program"
    Private Sub BindDropDownListTime()
        Dim _lsTime As List(Of TimeEntity)
        _lsTime = _factorNameBiz.GetFactorDate()
        cbListTime.DataSource = _lsTime
        cbListTime.DataValueField = "TimeId"
        cbListTime.DataTextField = "TimeName"
        cbListTime.DataBind()
    End Sub

    Private Sub BindDropDownListTimeCreateNew()
        Dim lstNewTime As List(Of TimeEntity)
        Dim lstExistingTime As List(Of TimeEntity)
        lstExistingTime = _factorNameBiz.GetFactorDate()
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

    Private Sub BindGridViewFactorName()
        Dim timeId As String = GetLastDayOfMonth()
        gvFactorName.DataSource = _factorNameBiz.GetByTime(timeId)
        gvFactorName.DataBind()
        tbFactorNameHeader.Visible = True
        gvFactorName.Visible = True
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

#End Region

#Region "Function"
    Private Function GetLastDayOfMonth() As String
        Dim timeId As String = cbListTime.SelectedValue
        Dim _year As String = timeId.Substring(0, 4)
        Dim _month As String = timeId.Substring(4, 2)
        timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        Return timeId
    End Function

    Private Function GetCreateNewLastDayOfMonth() As String
        Dim timeId As String = ddlTimeCreateNew.SelectedValue
        Dim _year As String = timeId.Substring(0, 4)
        Dim _month As String = timeId.Substring(4, 2)
        timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        ViewState("CreateNewTimeId") = timeId
        Return timeId
    End Function

    Private Function LoadFactor() As List(Of FactorEntity)
        Dim timeId As String = GetLastDayOfMonth()
        Return _factorNameBiz.GetFactor(timeId)
    End Function

    Private Function Vaidate()
        Dim errMsgList As New List(Of String)
        Dim factorName As String = txtFactorName.Text
        Dim factorUnit As String = txtFactorUnit.Text
        Dim lstFactor As List(Of FactorEntity) = LoadFactor()
        'สถานการณ์ภาวะวิกฤต = กรอกได้ทั้ง text,ตัวเลข และต้องห้ามซ้ำตาม list Scenario

        If ViewState("mode") = "add" Then
            If (factorUnit = "") Then
                errMsgList.Add("กรุณากรอกชื่อหน่วย")
            ElseIf (factorName = "") Then
                errMsgList.Add("กรุณากรอกชื่อตัวแปรทางเศรษฐกิจ")
            ElseIf (IsExistingFactorName(factorName, lstFactor) = True) Then
                errMsgList.Add("ชื่อตัวแปรทางเศรษฐกิจนี้มีอยู่แล้วในระบบ")
            End If
        Else ViewState("mode") = "edit"
            If (factorUnit = "") Then
                errMsgList.Add("กรุณากรอกชื่อหน่วย")
            ElseIf (factorName = "") Then
                errMsgList.Add("กรุณากรอกชื่อตัวแปรทางเศรษฐกิจ")
            End If
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
#End Region

#Region "Search"
    Protected Sub btn_Searchr_Click(sender As Object, e As EventArgs) Handles btn_Searchr.Click
        BindGridViewFactorName()
    End Sub
#End Region

#Region "Result"
    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim result As Boolean
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim lbFactorId As Label = gvFactorName.Rows(e.RowIndex).Cells(1).FindControl("lbFactorId")
        Dim timeId As String = GetLastDayOfMonth()

        result = _factorNameBiz.Delete(timeId, lbFactorId.Text, userId)
        Try
            If result = True Then
                BindGridViewFactorName()
                MessageBoxAlert("Success", "ลบข้อมูลเรียบร้อยแล้ว", "", "ปิด", False, True)
            Else
                MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถลบข้อมูลได้", "", "ปิด", False, True)
            End If
        Catch ex As Exception
            MessageBoxAlert("Error", ex.Message, "", "ปิด", False, True)
            UtilLogfile.writeToLog("StressScenario.aspx", "OnRowDeleting()", ex.Message)
        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFactorName.RowDataBound
        Dim btnDelete As LinkButton = CType(e.Row.FindControl("btnDelete"), LinkButton)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            btnDelete.Attributes("onclick") = "if(!confirm('คุณต้องการลบข้อมูลใช่หรือไม่ ?')){ return false; };"
        End If
    End Sub

    Protected Sub gvFactorName_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFactorName.RowCommand
        If e.CommandName = "Edit" Then
            ViewState("mode") = "edit"

            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvFactorName.Rows(rowIndex)
            ' Find the Label control and get its value
            Dim lbFactorName As Label = CType(row.FindControl("lbFactorName"), Label)
            Dim lbFactorDesc As Label = CType(row.FindControl("lbFactorDesc"), Label)
            Dim lbFactorUnit As Label = CType(row.FindControl("lbFactorUnit"), Label)
            Dim lbTimeId As Label = CType(row.FindControl("lbTimeId"), Label)
            Dim lbFactorId As Label = CType(row.FindControl("lbFactorId"), Label)
            ViewState("TimeId") = lbTimeId.Text
            ViewState("FactorId") = lbFactorId.Text
            lblModalTitle.Text = "แก้ไข (Edit)"
            txtFactorName.Text = lbFactorName.Text
            txtFactorDesc.Text = lbFactorDesc.Text
            txtFactorUnit.Text = lbFactorUnit.Text
            txtFactorName.Enabled = False
            lblMessage.Text = ""
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            UpdModal.Update()
        End If
    End Sub

    Protected Sub gvFactorName_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFactorName.RowEditing

    End Sub

    Protected Sub gvFactorName_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFactorName.PageIndexChanging
        gvFactorName.PageIndex = e.NewPageIndex
        BindGridViewFactorName()
    End Sub
#End Region

#Region "Create New"
    Protected Sub btnCreateNew_Click(sender As Object, e As EventArgs) Handles btnCreateNew.Click
        lblModalTitle.Text = "สร้าง (New)"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalCreateNew", "$('#myModalCreateNew').modal();", True)
        UpdModal.Update()
    End Sub

    Protected Sub btnSaveCreateNew_Click(sender As Object, e As EventArgs) Handles btnSaveCreateNew.Click
        If SaveCreateNewTime() Then
            BindDropDownListTime()
            cbListTime.SelectedValue = ViewState("CreateNewTimeId")
            BindGridViewFactorName()
            BindDropDownListTimeCreateNew()
            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
        End If
    End Sub

    Private Function SaveCreateNewTime() As Boolean
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim timeId As String = GetCreateNewLastDayOfMonth()
        Return _factorNameBiz.SaveCreateNew(timeId, userId)
    End Function
#End Region

#Region "Add/Edit"
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ViewState("mode") = "add"
        lblMessage.Visible = False
        lblModalTitle.Text = "เพิ่ม (Add)"
        txtFactorDesc.Text = ""
        txtFactorName.Text = ""
        txtFactorUnit.Text = ""
        txtFactorName.Enabled = True
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        UpdModal.Update()
    End Sub
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
                    BindGridViewFactorName()
                    MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
                Else
                    MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
                End If
            ElseIf ViewState("mode") = "edit" Then
                Dim timeId As String = ViewState("TimeId")
                Dim factorId As String = ViewState("FactorId")
                Dim userId As Integer = Convert.ToInt32(Session("UserID"))
                If _factorNameBiz.SaveUpdate(timeId, factorId, txtFactorName.Text, txtFactorDesc.Text, txtFactorUnit.Text, userId) Then
                    BindGridViewFactorName()
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
        '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal({backdrop: true});", True)
        '        UpdModal.Update()
        '    Else
        '        lblMessage.Visible = False
        '        If SaveAdd() Then
        '            BindGridViewFactorName()
        '            MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
        '        Else
        '            MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
        '        End If
        '    End If
        'ElseIf ViewState("mode") = "edit" Then
        '    Dim timeId As String = ViewState("TimeId")
        '    Dim factorId As String = ViewState("FactorId")
        '    Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        '    If _factorNameBiz.SaveUpdate(timeId, factorId, txtFactorName.Text, txtFactorDesc.Text, txtFactorUnit.Text, userId) Then
        '        BindGridViewFactorName()
        '        MessageBoxAlert("Success", "บันทึกข้อมูลสำเร็จ", "", "ปิด", False, True)
        '    Else
        '        MessageBoxAlert("Error", "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้", "", "ปิด", False, True)
        '    End If
        'End If
    End Sub
    Private Function SaveAdd() As Boolean
        Dim factorName As String = txtFactorName.Text
        Dim factorDesc As String = txtFactorDesc.Text
        Dim unit As String = txtFactorUnit.Text
        Dim userId As Integer = Convert.ToInt32(Session("UserID"))
        Dim timeId As String = GetLastDayOfMonth()
        Return _factorNameBiz.SaveAdd(timeId, factorName, factorDesc, unit, userId)
    End Function
#End Region

End Class