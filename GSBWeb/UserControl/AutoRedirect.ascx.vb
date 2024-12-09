Imports System.Globalization

Public Class AutoRedirect
    Inherits System.Web.UI.UserControl

    Public LoginDate As String
    Public ExpressDate As String
    Public CKey As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check session is expire or timeout.

        If Session("UserName") Is Nothing Then

            Response.Redirect("~/Login.aspx")

        End If


        'Get user login time or last activity time.
        Dim _date As DateTime = DateTime.Now
        LoginDate = _date.ToString("u", DateTimeFormatInfo.InvariantInfo).Replace("Z", "")

        Dim sessionTimeout As Integer = Session.Timeout
        Dim dateExpress As DateTime = _date.AddMinutes(sessionTimeout)
        ExpressDate = dateExpress.ToString("u", DateTimeFormatInfo.InvariantInfo).Replace("Z", "")

        CKey = CStr(System.Configuration.ConfigurationManager.AppSettings("Ckey"))

    End Sub

End Class