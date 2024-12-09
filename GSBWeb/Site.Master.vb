Imports GSBWeb.BLL
Imports GSBWeb.DAL
Imports System.Configuration
Imports System.Web.Configuration


Public Class Site

    Inherits System.Web.UI.MasterPage
    Dim gui As New Guid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString("ConnectionString_Report")

            If Session("UserName") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            Else
                'lblUsername.Text = Session("UserName").ToString()
            End If
        End If
    End Sub


  
End Class