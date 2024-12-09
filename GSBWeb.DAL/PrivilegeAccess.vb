Imports Arsoft.Utility

Public Class PrivilegeAccess
    'Dim dbCon As New DBclass("ConnectionString_Report")
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString("ConnectionString_Report")
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function SetGroupPrivilege(ByVal _datalist As List(Of PrivilegeEntity)) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

            _sql = "DELETE FROM rdmsys_groupmodule_privilege WHERE groupid=" & _datalist(0).GroupID
            _dbaccess.ExecuteNonQuery(_sql)

            _sql = "INSERT INTO rdmsys_groupmodule_privilege(moduleid,groupid) "
            _sql = _sql & "VALUES(@moduleid,@groupid)"

            For Each _data In _datalist
                With _data
                    _param(0) = New SQLServerDBParameter("@moduleid", .ModuleID)
                    _param(1) = New SQLServerDBParameter("@groupid", .GroupID)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _result = True

        Catch ex As Exception
            UtilLogfile.writeToLog("PrivilegeAccess", "SetGroupPrivilege()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function SetGroupUserPrivilage(ByVal _datalist As List(Of PrivilegeEntity)) As Boolean
        Dim _Return As Boolean = False
        Dim _sql As String
        Dim _sql_selectGroupid As String
        Dim _param(2) As SQLServerDBParameter

        Try
            _dbaccess.CommitTransaction()

            _sql = "DELETE FROM rdmsys_groupuser_privilege where userid=@userid"
            _param(0) = New SQLServerDBParameter("@userid", _datalist.Item(0).UserID)
            _dbaccess.ExecuteNonQuery(_sql, _param)

            _sql = "INSERT INTO rdmsys_groupuser_privilege(groupid,userid)" & _
                   "VALUES (@groupid,@userid)"
            For Each _data In _datalist
                Dim _realgroupid As Integer
                With _data
                    If .GroupCode = "NULL" Then
                        _param(0) = New SQLServerDBParameter("@groupid", .GroupID)
                    Else
                        _sql_selectGroupid = "SELECT groupid FROM rdmsys_groupuser where groupcode = @groupcode"
                        _param(0) = New SQLServerDBParameter("@groupcode", .GroupCode)
                        _realgroupid = _dbaccess.ExecuteScalar(_sql_selectGroupid, _param)
                        _param(0) = New SQLServerDBParameter("@groupid", _realgroupid)

                    End If
                    _param(1) = New SQLServerDBParameter("@userid", .UserID)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _Return = True
        Catch ex As Exception
            UtilLogfile.writeToLog("PrivilegeAccess", "SetUserPrivilege()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _Return
    End Function

    Public Function GetGroupUserPrivilage(ByVal _userid As Integer) As List(Of PrivilegeEntity)
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim _RebackData As New List(Of PrivilegeEntity)
        Dim _GetData As PrivilegeEntity

        Try
            _sql = "SELECT groupid,userid FROM rdmsys_groupuser_privilege WHERE userid = @userid"
            _param(0) = New SQLServerDBParameter("@userid", _userid)

            _dbaccess.ExecuteReader(_sql, _param)
            Do While _dbaccess.Read()
                _GetData = New PrivilegeEntity()
                With _GetData
                    .GroupID = _dbaccess.GetItem("groupid")
                    .UserID = _dbaccess.GetItem("userid")
                End With
                _RebackData.Add(_GetData)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("PrivilegeAccess", "SetGroupPrivilege()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _RebackData
    End Function

    Public Function DeleteGroupUserPrivilage(ByVal _userid As Integer) As Boolean
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter
        Dim _Returndata As Boolean = False

        Try
            _dbaccess.BeginTransaction()
            _sql = "DELETE FROM rdmsys_groupuser_privilege WHERE userid=@userid"
            _param(0) = New SQLServerDBParameter("@userid", _userid)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _Returndata = True
        Catch ex As Exception
            UtilLogfile.writeToLog("PrivilegeAccess", "SetUserPrivilege()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _Returndata
    End Function
    
    Public Function SetUserPrivilege(ByVal _datalist As List(Of PrivilegeEntity)) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            _dbaccess.BeginTransaction()

            _sql = "DELETE FROM rdmsys_usermodule_privilege WHERE userid=" & _datalist(0).UserID
            _dbaccess.ExecuteNonQuery(_sql)

            _sql = "INSERT INTO rdmsys_usermodule_privilege(moduleid,userid) "
            _sql = _sql & "VALUES(@moduleid,@userid)"

            For Each _data In _datalist
                With _data
                    _param(0) = New SQLServerDBParameter("@moduleid", .ModuleID)
                    _param(1) = New SQLServerDBParameter("@userid", .UserID)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Next

            _dbaccess.CommitTransaction()
            _result = True

        Catch ex As Exception
            UtilLogfile.writeToLog("PrivilegeAccess", "SetUserPrivilege()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function DeleteGroupModulePrivilege(ByVal _groupid As Integer) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _param(2) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()
            _sql = "DELETE FROM rdmsys_groupmodule_privilege where groupid=@groupid"
            _param(0) = New SQLServerDBParameter("@groupid", _groupid)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("PrivilegeAccess", "SetUserPrivilege()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Sub Dispose()
        If Not _dbaccess Is Nothing Then
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End If
    End Sub

End Class
