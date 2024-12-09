Imports GSBWeb.DAL
Imports GSBWeb.BLL
Imports Arsoft.Utility
Imports System.Configuration
Imports System.Web.Configuration

Public Class Login
    Inherits System.Web.UI.Page
    'Dim chkPermis As New BLL.PermissionReportBiz

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Page.IsPostBack) Then
            SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString("ConnectionString_Report")
            Session.Abandon()
            txtUser.Focus()
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim userName As String
        Dim script As String
        userName = txtUser.Text.Trim
        'userName = "RDMAPP01"

        Dim _usermodule As New UserModuleBiz
        Dim _userentity = _usermodule.GetUserInfo(userName)
        Dim _pw As String = ""
        Dim _msg As String = ""
        Dim _crypt As New CryptographyControl
        Dim _isvalid As Boolean = True
        Dim _findex As Integer = 0
        Dim _length As Integer = 0

        If IsNothing(_userentity) Then
            script = "alert('เข้าสู่ระบบล้มเหลว, ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง !!!')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
            Exit Sub
        Else
            If _userentity.UserFlag = 0 Then ' User Application
                _pw = _crypt.EncryptedMD5(txtPassword.Text.Trim)
                If Not _userentity.Password.Trim = _pw Then
                    script = "alert('เข้าสู่ระบบล้มเหลว, ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง !!!')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
                    Exit Sub
                Else
                    If Not _usermodule.IsLogin(_userentity.UserID, False) Then
                        _msg = "เกิดข้อผิดพลาดในฐานข้อมูล !!!"
                        script = "alert('" + _msg + " !!!')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
                        Exit Sub
                    End If
                End If
            Else
                '//============== Check active directory and login fail ==========

                'Dim _ldapDirectory As String = ""
                Dim _activeBiz As New BLL.ActiveDirectoryManagement
                'Dim _msg As String = ""
                Dim _listLDAP As New List(Of String)

                _listLDAP.Add(CStr(System.Configuration.ConfigurationManager.AppSettings("Local_LDAPDirectory")))
                _listLDAP.Add(CStr(System.Configuration.ConfigurationManager.AppSettings("GSB_LDAPDirectory")))

                'If _userentity.CountFail >= 3 And _userentity.CheckTime < 300 Then

                'Dim timediff As TimeSpan

                'If Not IsNothing(Session("datetimeerror")) Then
                '    timediff = DateTime.Now.Subtract(Session("datetimeerror"))
                'End If

                'If IsNothing(Session("CountError")) Then
                '    Session("CountError") = 0
                'End If

                If _userentity.CountFail >= 3 And _userentity.CheckTime < 300 Then
                    Dim delay As String = (Math.Ceiling((300 - CInt(_userentity.CheckTime)) / 60)).ToString()
                    'Dim delay As Integer = 5 - timediff.Minutes
                    script = "alert('ใส่ Password ผิดเกิน 3 ครั้ง จะสามารถ login ได้ในอีก " + delay.ToString() + " นาที !!!')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
                    Exit Sub
                Else

                    For Each _ldapDirectory As String In _listLDAP
                        _msg = _activeBiz.GetUserId(txtUser.Text.Trim(), txtPassword.Text.Trim(), _ldapDirectory)
                        If _msg = "true" Then
                            Exit For
                        End If
                    Next


                    If _msg = "true" Then
                        Session("CountError") = 0
                        If Not _usermodule.IsLogin(_userentity.UserID, True) Then
                            _msg = "เกิดข้อผิดพลาดในฐานข้อมูล !!!"
                            script = "alert('" + _msg + " !!!')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
                            Exit Sub
                        End If
                    Else

                        _usermodule.IsLogin(_userentity.UserID, False)

                        'Session("CountError") = Session("CountError") + 1

                        'If Session("CountError") = 1 Or Session("CountError") > 3 Then
                        '    Session("datetimeerror") = DateTime.Now()
                        'End If




                        'If Then

                        '    Dim delay As Integer = 2 - timediff.Minutes
                        '    script = "alert('ใส่ Password ผิดเกิน 3 ครั้ง จะสามารถ login ได้ในอีก " + delay.ToString() + " นาที !!!')"
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
                        '    Exit Sub

                        'Else

                        script = String.Format("alert('{0} !!!')", _msg.Replace(vbCr, "").Replace(vbLf, ""))
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
                        Exit Sub


                        'End If



                    End If

                    '_msg = _activeBiz.GetUserId(txtUser.Text.Trim(), txtPassword.Text.Trim(), _ldapDirectory)
                    'If _msg = "true" Then
                    '    If Not _usermodule.IsLogin(_userentity.UserID, True) Then
                    '        _msg = "เกิดข้อผิดพลาดในฐานข้อมูล !!!"
                    '        script = "alert('" + _msg + " !!!')"
                    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
                    '        Exit Sub
                    '    End If
                    'Else
                    '    'If Not _usermodule.IsLogin(userName, False) Then
                    '    script = String.Format("alert('{0} !!!')", _msg.Replace(vbCr, "").Replace(vbLf, ""))

                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)

                    '    Exit Sub
                    '    'End If
                    'End If

                End If

                End If
            End If

        '//===============================================================
        Session("UserID") = _userentity.UserID
        Session("Password") = txtPassword.Text.Trim() + ";"
        Session("UserName") = _userentity.UserName
        Session("UserInfo") = _userentity
        Session("Flag") = 0

        _usermodule.Dispost()
        Response.Redirect("~/Defult.aspx")


        'Dim LDAPDirectory As String = CStr(System.Configuration.ConfigurationManager.AppSettings("LDAPDirectory"))
        'Dim ActiveManag As New BLL.ActiveDirectoryManagement

        'Dim Error01 As String
        'Dim userName As String
        'Dim script As String
        'userName = txtUser.Text.Trim
        'userName = "RDMAPP01"

        'Dim UserObjects = chkPermis.GetDataCheckUser(userName)
        'If UserObjects.Count > 0 Then
        '    Dim item As PermissionReportEnntity
        '    item = UserObjects(0)

        '    Session("UserName") = userName.Trim()
        '    Response.Redirect("~/RDM_Reports/RDM_Report.aspx")
        '    'If item.CountFail >= 3 And item.chkTime < 300 Then
        '    '    Dim delay As String = (Math.Ceiling((300 - CInt(item.chkTime)) / 60)).ToString()
        '    '    script = "alert('ใส่ Password ผิดเกิน 3 ครั้ง จะสามารถ login ได้ในอีก " + delay + " นาที !!!')"
        '    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
        '    'Else
        '    '    If ActiveManag.GetUserId(txtUser.Text.Trim(), txtPassword.Text.Trim(), LDAPDirectory) = "true" Then
        '    '        If Not chkPermis.UpdateUser(userName, True) Then
        '    '            Error01 = "เกิดข้อผิดพลาดในฐานข้อมูล !!!"
        '    '            script = "alert('" + Error01 + " !!!')"
        '    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
        '    '        Else
        '    '            Session("UserName") = userName.Trim()
        '    '            Response.Redirect("~/RDM_Reports/RDM_Report.aspx")
        '    '        End If
        '    '    Else
        '    '        If Not chkPermis.UpdateUser(userName, False) Then
        '    '            Error01 = "เกิดข้อผิดพลาดในฐานข้อมูล !!!"
        '    '            script = "alert('" + Error01 + " !!!')"
        '    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
        '    '        Else
        '    '            script = "alert('รหัสผ่านไม่ถูกต้อง !!!')"
        '    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
        '    '        End If
        '    '    End If
        '    'End If
        'Else
        '    script = "alert('ไม่มีชื่อผู้ใช้นี้ในระบบ !!!')"
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
        'End If

    End Sub

End Class