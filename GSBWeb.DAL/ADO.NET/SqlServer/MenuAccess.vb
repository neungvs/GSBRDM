Public Class MenuAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    'Public Function GetInfo(_deptcode As String) As BranchEntity
    '    Dim _result As New BranchEntity
    '    Dim _sql As String
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess

    '    Try
    '        'DeptID,DeptCode,DeptName,DeptpID,DeptpCode,DeptLevel,DeptGroup,DivisionID,RegionID
    '        _sql = "SELECT * FROM gsbrc_branch WHERE deptcode='" & _deptcode & "'"
    '        _dbaccess.ExecuteReader(_sql)
    '        Do While _dbaccess.Read
    '            With _result
    '                .DeptID = _dbaccess.GetItem("deptid")
    '                .DeptCode = _dbaccess.GetItem("deptcode")
    '                .DeptName = _dbaccess.GetItem("deptname")
    '                .DeptpID = _dbaccess.GetItem("deptpid")
    '                .DeptpCode = _dbaccess.GetItem("deptpcode")
    '                .DeptLevel = _dbaccess.GetItem("deptlevel")
    '                .DeptGroup = _dbaccess.GetItem("deptgroup")
    '                .DivisionID = _dbaccess.GetItem("divisionid")
    '                .RegionID = _dbaccess.GetItem("regionid")
    '            End With
    '        Loop
    '        _dbaccess.CloseReader()
    '    Catch ex As Exception
    '        UtilLogfile.writeToLog("BranchAccess", "GetInfoLists()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    Finally
    '        If Not _dbaccess Is Nothing Then
    '            _dbaccess.Dispose()
    '            _dbaccess = Nothing
    '        End If
    '    End Try
    '    Return _result
    'End Function

    Public Function GetInfoLists() As MenuLists
        Dim _result As New MenuLists
        Dim _menu As MenuEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            'DeptID,DeptCode,DeptName,DeptpID,DeptpCode,DeptLevel,DeptGroup,DivisionID,RegionID
            _sql = "SELECT * FROM MenuTest"
            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _menu = New MenuEntity
                With _menu
                    .MenuID = _dbaccess.GetItem("MenuID")
                    .ParentId = _dbaccess.GetItem("parent_id")
                    .Name = _dbaccess.GetItem("Name")
                    .Url = _dbaccess.GetItem("Url")

                End With
                _result.Add(_menu)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("MenuAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    'Public Function Insert(_data As BranchEntity) As Boolean
    '    Dim _result As Boolean = False
    '    Dim _sql As String
    '    Dim _param(8) As SQLServerDBParameter
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess

    '    Try
    '        _sql = "INSERT INTO GSBRC_Branch(deptid,deptcode,deptname,deptpid,deptpcode,deptlevel,deptgroup,divisionid,regionid) "
    '        _sql = _sql & "VALUES(@deptid,@deptcode,@deptname,@deptpid,@deptpcode,@deptlevel,@deptgroup,@divisionid,@regionid)"

    '        _dbaccess.BeginTransaction()
    '        With _data
    '            _param(0) = New SQLServerDBParameter("@deptid", .DeptID)
    '            _param(1) = New SQLServerDBParameter("@deptcode", .DeptCode)
    '            _param(2) = New SQLServerDBParameter("@deptname", .DeptName)
    '            _param(3) = New SQLServerDBParameter("@deptpid", .DeptpID)
    '            _param(4) = New SQLServerDBParameter("@deptpcode", .DeptpCode)
    '            _param(5) = New SQLServerDBParameter("@deptlevel", .DeptLevel)
    '            _param(6) = New SQLServerDBParameter("@deptgroup", .DeptGroup)
    '            _param(7) = New SQLServerDBParameter("@divisionid", .DivisionID)
    '            _param(8) = New SQLServerDBParameter("@regionid", .RegionID)
    '        End With
    '        _dbaccess.ExecuteNonQuery(_sql, _param)
    '        _dbaccess.CommitTransaction()
    '        _result = True
    '    Catch ex As Exception
    '        _dbaccess.RollbackTransaction()
    '        UtilLogfile.writeToLog("BranchAccess", "Insert()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    Finally
    '        If Not _dbaccess Is Nothing Then
    '            _dbaccess.Dispose()
    '            _dbaccess = Nothing
    '        End If
    '    End Try

    '    Return _result
    'End Function

    'Public Function Update(_data As BranchEntity) As Boolean
    '    Dim _result As Boolean = False
    '    Dim _sql As String
    '    Dim _param(8) As SQLServerDBParameter
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess

    '    Try
    '        _sql = "UPDATE GSBRC_Branch SET "
    '        _sql = _sql & "deptcode=@deptcode,deptname=@deptname,deptpid=@deptpid,deptpcode=@deptpcode,"
    '        _sql = _sql & "deptlevel=@deptlevel,deptgroup=@deptgroup,divisionid=@divisionid,regionid=@regionid "
    '        _sql = _sql & "WHERE deptid=@deptid"

    '        _dbaccess.BeginTransaction()
    '        With _data
    '            _param(0) = New SQLServerDBParameter("@deptcode", .DeptCode)
    '            _param(1) = New SQLServerDBParameter("@deptname", .DeptName)
    '            _param(2) = New SQLServerDBParameter("@deptpid", .DeptpID)
    '            _param(3) = New SQLServerDBParameter("@deptpcode", .DeptpCode)
    '            _param(4) = New SQLServerDBParameter("@deptlevel", .DeptLevel)
    '            _param(5) = New SQLServerDBParameter("@deptgroup", .DeptGroup)
    '            _param(6) = New SQLServerDBParameter("@divisionid", .DivisionID)
    '            _param(7) = New SQLServerDBParameter("@regionid", .RegionID)
    '            _param(8) = New SQLServerDBParameter("@deptid", .DeptID)
    '        End With
    '        _dbaccess.ExecuteNonQuery(_sql, _param)
    '        _dbaccess.CommitTransaction()
    '        _result = True
    '    Catch ex As Exception
    '        _dbaccess.RollbackTransaction()
    '        UtilLogfile.writeToLog("BranchAccess", "Update()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    Finally
    '        If Not _dbaccess Is Nothing Then
    '            _dbaccess.Dispose()
    '            _dbaccess = Nothing
    '        End If
    '    End Try

    '    Return _result
    'End Function

    'Public Function Delete(ByVal _branceid As String) As Boolean
    '    Dim _result As Boolean = False
    '    Dim _sql As String
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess

    '    Try
    '        _sql = "DELETE FROM GSBRC_Branch WHERE documentid=" & _branceid
    '        _dbaccess.ExecuteNonQuery(_sql)
    '        _result = True
    '    Catch ex As Exception
    '        UtilLogfile.writeToLog("BranchAccess", "Delete()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    Finally
    '        If Not _dbaccess Is Nothing Then
    '            _dbaccess.Dispose()
    '            _dbaccess = Nothing
    '        End If
    '    End Try
    '    Return _result
    'End Function

#End Region

End Class
