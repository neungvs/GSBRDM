Public Class GroupAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_groupid As Integer) As GroupUserEntity
        Dim _result As New GroupUserEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_GroupUser WHERE groupid=" & _groupid
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupName = _dbaccess.GetItem("groupname")
                    .Parent = _dbaccess.GetItem("parent")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As GroupUserLists
        Dim _result As New GroupUserLists
        Dim _groupuser As GroupUserEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_GroupUser ORDER BY groupid"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _groupuser = New GroupUserEntity
                With _groupuser
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupName = _dbaccess.GetItem("groupname")
                    .Parent = _dbaccess.GetItem("parent")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
                _result.Add(_groupuser)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetParentInfoLists() As GroupUserLists
        Dim _result As New GroupUserLists
        Dim _groupuser As GroupUserEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_GroupUser WHERE parent IS NULL ORDER BY groupid"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _groupuser = New GroupUserEntity
                With _groupuser
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupName = _dbaccess.GetItem("groupname")
                    .Parent = _dbaccess.GetItem("parent")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
                _result.Add(_groupuser)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetSubInfoLists(_parentid As Integer) As GroupUserLists
        Dim _result As New GroupUserLists
        Dim _groupuser As GroupUserEntity
        'Dim _param(1) As SQLServerDBParameter
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT * FROM GSBBRC_GroupUser "
            If _parentid = 0 Then
                _sql = _sql & "WHERE parent IS NULL"
            Else
                _sql = _sql & "WHERE parent=" & _parentid
            End If
            _sql = _sql & " ORDER BY groupid"

            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _groupuser = New GroupUserEntity
                With _groupuser
                    .GroupID = _dbaccess.GetItem("groupid")
                    .GroupName = _dbaccess.GetItem("groupname")
                    .Parent = _dbaccess.GetItem("parent")
                    .CreatedDate = _dbaccess.GetItem("createddate")
                    .ModifiedDate = _dbaccess.GetItem("modifieddate")
                End With
                _result.Add(_groupuser)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetGroupID(_groupname As String) As Integer
        Dim _result As Integer
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT top 1 groupid FROM GSBBRC_GroupUser WHERE groupname like '" & _groupname & "%'"
            _result = _dbaccess.ExecuteScalar(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("GroupAccess", "GetGroupID()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetGroupID(_groupname As String, _parentname As String) As Integer
        Dim _result As Integer = 0
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT a.groupid FROM GSBBRC_GroupUser a WHERE a.groupname like '" & _groupname & "%' "
            _sql = _sql & "AND a.parent =(SELECT groupid FROM GSBBRC_GroupUser WHERE groupname like '" & _parentname & "%')"
            _result = _dbaccess.ExecuteScalar(_sql)

        Catch ex As Exception
            UtilLogfile.writeToLog("GroupAccess", "GetGroupID()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result

    End Function

    Public Function Insert(_data As GroupUserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "INSERT INTO GSBBRC_GroupUser(groupid,groupname,parent,createddate,modifieddate) "
            _sql = _sql & "VALUES(@groupid,@groupname,@parent,getdate(),getdate())"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@groupid", .GroupID)
                _param(1) = New SQLServerDBParameter("@groupname", .GroupName)
                _param(2) = New SQLServerDBParameter("@parent", DBUtility.GetNumeric(.Parent))
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("GroupAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As GroupUserEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_GroupUser SET "
            _sql = _sql & "groupname=@groupname,parent=@parent,modifieddate=getdate() "
            _sql = _sql & "WHERE groupid=@groupid"

            'groupid,groupname,parent,createddate,modifieddate
            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@groupname", .GroupName)
                _param(1) = New SQLServerDBParameter("@parent", .Parent)
                _param(2) = New SQLServerDBParameter("@groupid", .GroupID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("GroupAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _groupid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_GroupUser WHERE groupid=" & _groupid
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("GroupAccess", "Delete()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

#End Region

End Class
