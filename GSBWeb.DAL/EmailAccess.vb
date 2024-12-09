Imports System.Data.SqlClient
Imports System.Configuration
Imports Arsoft.Utility
Imports System.Globalization





Public Class EmailAccess

    Dim _dbaccess As SQLServerDBAccess
    Dim _dbCon As New DBclass("ConnectionString_Report")

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub


    Public Function GetDataReceiveMail(ByVal _ReportID As String) As DataTable
        Dim _sql As String
        Dim _result As DataTable
        Try
            _sql = "select ROW_NUMBER()  over (order by Email) RowNumber "
            _sql += ",Email "
            _sql += "From RDM_Report..Cpm_ReceiveMail "
            _sql += "WHERE ReportID = '" + _ReportID + "' "
            _sql += "order by Email"

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("EmailAcess", "GetDataReceiveMail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function GetDataConfigEmail(ByVal _ConfigName As String) As DataTable
        Dim _sql As String
        Dim _result As DataTable
        Try
            _sql = "select ConfigName"
            _sql += ",value "
            _sql += "From RDM_Report..Cpm_ConfigMail "
            _sql += "where ConfigName = '" + _ConfigName + "' "

            _result = _dbCon.ExecuteReader(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("EmailAcess", "GetDataConfigEmail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function



    Public Function CheckEmail(ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean = True
        Dim _sql As String = String.Empty
        Dim _param(2) As SQLServerDBParameter
        Dim _cntEmail As Integer = 0

        Try
            _sql = "select COUNT(*) "
            _sql += "from Cpm_ReceiveMail "
            _sql += "where Email  = @Email "
            _sql += "and ReportID = @ReportID"

            _param(0) = New SQLServerDBParameter("@Email", RTrim(LTrim(_Email)))
            _param(1) = New SQLServerDBParameter("@ReportID", _ReportID)
            _cntEmail = _dbaccess.ExecuteScalar(_sql, _param)

            If _cntEmail > 0 Then
                _result = False
            Else
                _result = True
            End If

        Catch ex As Exception
            UtilLogfile.writeToLog("EmailAccess", "CheckEmail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function InsertEmail(ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = String.Empty
        Dim _param(2) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()

            _sql = "INSERT INTO [RDM_Report]..[Cpm_ReceiveMail] "
            _sql += "(Email,ReportID) "
            _sql += "VALUES (@Email,@ReportID) "

            _param(0) = New SQLServerDBParameter("@Email", RTrim(LTrim(_Email)))
            _param(1) = New SQLServerDBParameter("@ReportID", _ReportID)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("EmailAccess", "InsertEmail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function


    Public Function DeleteEmail(ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = String.Empty
        Dim _param(2) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()

            _sql = "delete from  [RDM_Report]..[Cpm_ReceiveMail]  where [Email] = @Eamil and ReportID = @ReportID"

            _param(0) = New SQLServerDBParameter("@Eamil ", RTrim(LTrim(_Email)))
            _param(1) = New SQLServerDBParameter("@ReportID", _ReportID)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("EmailAccess", "DeleteEmail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function UpdateEmail(ByVal _EmailUpdate As String, ByVal _Email As String, ByVal _ReportID As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _param(3) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()
            _sql = "UPDATE [RDM_Report]..[Cpm_ReceiveMail] SET Email=@EmailUpdate,Updatedate =GETDATE() WHERE ReportID=@ReportID and Email = @Email;"
            _param(0) = New SQLServerDBParameter("@EmailUpdate", RTrim(LTrim(_EmailUpdate)))
            _param(1) = New SQLServerDBParameter("@ReportID", _ReportID)
            _param(3) = New SQLServerDBParameter("@Email", RTrim(LTrim(_Email)))
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("EmailAccess", "UpdateEmail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function UpdateConfigMail(ByVal _ConfigName As String, ByVal _value As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _param(2) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()
            _sql = "UPDATE [RDM_Report]..[Cpm_ConfigMail] SET value=@value,Updatedate =GETDATE() WHERE ConfigName=@ConfigName;"
            _param(0) = New SQLServerDBParameter("@value", _value)
            _param(1) = New SQLServerDBParameter("@ConfigName", _ConfigName)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("EmailAccess", "UpdateConfigMail()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function



End Class
