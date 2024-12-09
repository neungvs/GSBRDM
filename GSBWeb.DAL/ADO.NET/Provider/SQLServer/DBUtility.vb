Imports Microsoft.VisualBasic
Imports System.Configuration
Imports Arsoft.Utility

Public Class DBUtility

#Region "Attributes"
    Private Shared _sqlserverconnection As String
#End Region

#Region "Methods"

    Public Shared Function GetString(ByVal _data As Object) As Object
        Dim _value As Object
        Try
            If IsDBNull(_data) Or (_data Is Nothing) Or (_data = "") Then
                _value = DBNull.Value
            Else
                'Dim _type As String
                '_type = _value.GetType.ToString

                'Select Case LCase(_type)
                '    Case "system.datetime"
                '        _value.ToString()
                '    Case "system.int32"
                'End Select
                _value = _data.ToString
            End If
            Return _value
        Catch ex As Exception
            UtilLogfile.writeToLog("DBUtil", "GetString", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function

    Public Shared Function GetDate(ByVal _data As Object) As Object
        Dim _value As Object
        Try
            If IsDBNull(_data) Or (_data Is Nothing) Or (_data = "") Then
                Return DBNull.Value
            Else
                _value = CDate(_data)
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("DBUtil", "GetDate", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _value
    End Function

    Public Shared Function GetNumeric(ByVal _data As Object) As Object
        Dim _value As Object
        Dim _ck As Boolean = True
        Try
            If IsDBNull(_data) Then
                _ck = False
            Else
                If _data Is Nothing Or Trim(_data) = "" Then
                    _ck = False
                Else
                    If Not IsNumeric(_data) Then
                        _ck = False
                    End If
                End If
            End If

            If _ck Then
                _value = CDbl(_data)
            Else
                Return DBNull.Value
            End If

        Catch ex As Exception
            UtilLogfile.writeToLog("DBUtil", "GetNumeric", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _value
    End Function

    Public Shared Function GetBoolean(ByVal _data As Object) As Object
        Dim _value As Object

        If (_data Is Nothing) Or IsDBNull(_data) Then
            _value = DBNull.Value
        Else
            Select Case CBool(_data)
                Case False
                    _value = 0
                Case True
                    _value = 1
                Case Else
                    _value = DBNull.Value
            End Select
        End If
        Return _value
    End Function

    Public Shared Function NullToString(ByVal _data As Object) As String
        Dim _value As String
        Try
            If IsDBNull(_data) Or (_data Is Nothing) Then
                _value = ""
            Else
                _value = _data.ToString
            End If
            Return _value
        Catch ex As Exception
            UtilLogfile.writeToLog("DBUtil", "NullToString", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Function

    Public Shared Function ReportConnectionString() As String
        If IsNothing(_sqlserverconnection) Then
            _sqlserverconnection = ConfigurationManager.ConnectionStrings("ConnectionString_Report").ToString
        End If
        Return _sqlserverconnection
    End Function

    Public Shared Function ReportConnectionString(constr As String) As String
        'If IsNothing(_sqlserverconnection) Then
        _sqlserverconnection = ConfigurationManager.ConnectionStrings(constr).ToString
        'End If
        Return _sqlserverconnection
    End Function

#End Region
End Class
