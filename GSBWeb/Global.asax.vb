Imports System.Web.SessionState
Imports GSBWeb.DAL

Public Class Global_asax
    Inherits System.Web.HttpApplication



    Public Shared chkEndSession As String

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)

        ' Fires when the application is started

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)

        ' Fires when the session is started

    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    

    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)


        ' Fires when an error occurs

        'Dim ex As Exception = Server.GetLastError()


        'If ex.[GetType]() = GetType(HttpException) Then

        '    Dim httpEx As HttpException = DirectCast(ex, HttpException)
        '    If httpEx.GetHttpCode() = 404 Then
        '        Response.Redirect("~/ErrorPage/ErrorPage404.aspx")
        '    Else
        '        Response.Redirect("~/ErrorPage/ErrorPageOther.aspx")
        '    End If

        'Else

        '    Response.Redirect("~/ErrorPage/ErrorPageOther.aspx")
        'End If



    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)


    End Sub



    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub




  
 

End Class