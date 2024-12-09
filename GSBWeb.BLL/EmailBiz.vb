Imports GSBWeb.DAL
Imports System.Web.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Web.HttpApplicationState
Imports System.Web.HttpServerUtility
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Globalization
Imports System.Threading
Imports System.Data
Imports Arsoft.Utility


Public Class EmailBiz


    Dim _Emailacc As New EmailAccess

    Public Sub New()
    End Sub

    Public Function GetDataReceiveMail(ByVal _ReportID As String) As DataTable
        Dim _result As DataTable
        _result = _Emailacc.GetDataReceiveMail(_ReportID)
        Return _result
    End Function

    Public Function GetDataConfigEmail(ByVal _ConfigName As String) As DataTable
        Dim _result As DataTable
        _result = _Emailacc.GetDataConfigEmail(_ConfigName)
        Return _result
    End Function

    Public Function CheckEmail(ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean = False
        _result = _Emailacc.CheckEmail(_Email, _ReportID)
        Return _result
    End Function

    Public Function InsertEmail(ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean
        _result = _Emailacc.InsertEmail(_Email, _ReportID)
        Return _result
    End Function

    Public Function DeleteEmail(ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean = False
        _result = _Emailacc.DeleteEmail(_Email, _ReportID)
        Return _result
    End Function

    Public Function UpdateConfigMail(ByVal _ID As String, ByVal _Email As String) As Boolean
        Dim _result As Boolean = False
        _result = _Emailacc.UpdateConfigMail(_ID, _Email)
        Return _result
    End Function

    Public Function UpdateEmail(ByVal _EmailUpdate As String, ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean = False
        _result = _Emailacc.UpdateEmail(_EmailUpdate, _Email, _ReportID)
        Return _result
    End Function

End Class
