Public Class BranchAccess

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_deptcode As String) As BranchEntity
        Dim _result As New BranchEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            'DeptID,DeptCode,DeptName,DeptpID,DeptpCode,DeptLevel,DeptGroup,DivisionID,RegionID

            _sql = "SELECT * FROM gsbbrc_branch WHERE deptcode='" & _deptcode & "'"

            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .DeptID = _dbaccess.GetItem("deptid")
                    .DeptCode = _dbaccess.GetItem("deptcode")
                    .DeptName = _dbaccess.GetItem("deptname")
                    .DeptpID = _dbaccess.GetItem("deptpid")
                    .DeptpCode = _dbaccess.GetItem("deptpcode")
                    .DeptLevel = _dbaccess.GetItem("deptlevel")
                    .DeptGroup = _dbaccess.GetItem("deptgroup")
                    .DivisionID = _dbaccess.GetItem("divisionid")
                    .RegionID = _dbaccess.GetItem("regionid")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("BranchAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoByID(_deptid As String) As BranchEntity
        Dim _result As New BranchEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            'DeptID,DeptCode,DeptName,DeptpID,DeptpCode,DeptLevel,DeptGroup,DivisionID,RegionID

            _sql = "SELECT * FROM gsbbrc_branch WHERE deptid=" & _deptid

            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                With _result
                    .DeptID = _dbaccess.GetItem("deptid")
                    .DeptCode = _dbaccess.GetItem("deptcode")
                    .DeptName = _dbaccess.GetItem("deptname")
                    .DeptpID = _dbaccess.GetItem("deptpid")
                    .DeptpCode = _dbaccess.GetItem("deptpcode")
                    .DeptLevel = _dbaccess.GetItem("deptlevel")
                    .DeptGroup = _dbaccess.GetItem("deptgroup")
                    .DivisionID = _dbaccess.GetItem("divisionid")
                    .RegionID = _dbaccess.GetItem("regionid")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("BranchAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As BranchLists
        Dim _result As New BranchLists
        Dim _branch As BranchEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            'DeptID,DeptCode,DeptName,DeptpID,DeptpCode,DeptLevel,DeptGroup,DivisionID,RegionID

            _sql = "SELECT * FROM gsbbrc_branch ORDER BY deptcode"

            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _branch = New BranchEntity
                With _branch
                    .DeptID = _dbaccess.GetItem("deptid")
                    .DeptCode = _dbaccess.GetItem("deptcode")
                    .DeptName = _dbaccess.GetItem("deptname")
                    .DeptpID = _dbaccess.GetItem("deptpid")
                    .DeptpCode = _dbaccess.GetItem("deptpcode")
                    .DeptLevel = _dbaccess.GetItem("deptlevel")
                    .DeptGroup = _dbaccess.GetItem("deptgroup")
                    .DivisionID = _dbaccess.GetItem("divisionid")
                    .RegionID = _dbaccess.GetItem("regionid")
                End With
                _result.Add(_branch)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("BranchAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetInfoLists(_levelid As String, Optional _regionid As String = "", Optional _divisionid As String = "", Optional _parentid As String = "") As BranchLists
        Dim _result As New BranchLists
        Dim _branch As BranchEntity
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            'DeptID,DeptCode,DeptName,DeptpID,DeptpCode,DeptLevel,DeptGroup,DivisionID,RegionID

            _sql = "SELECT * FROM gsbbrc_branch "
            _sql = _sql & "WHERE deptlevel=" & _levelid
            If _regionid <> "" Then
                _sql = _sql & " AND regionid=" & _regionid
            End If
            If _divisionid <> "" Then
                _sql = _sql & " AND divisionid=" & _divisionid
            End If
            If _levelid = 2 Then
                If _parentid <> "" Then
                    _sql = _sql & " AND deptpid=" & _parentid
                End If
            End If

            Select Case _levelid
                Case "1", "2"
                    _sql = _sql & " ORDER BY right(rtrim(deptname),2)"
                Case Else
                    _sql = _sql & " ORDER BY deptname,deptcode"
            End Select


            _dbaccess.ExecuteReader(_sql)
            Do While _dbaccess.Read
                _branch = New BranchEntity
                With _branch
                    .DeptID = _dbaccess.GetItem("deptid")
                    .DeptCode = _dbaccess.GetItem("deptcode")
                    .DeptName = _dbaccess.GetItem("deptname")
                    .DeptpID = _dbaccess.GetItem("deptpid")
                    .DeptpCode = _dbaccess.GetItem("deptpcode")
                    .DeptLevel = _dbaccess.GetItem("deptlevel")
                    .DeptGroup = _dbaccess.GetItem("deptgroup")
                    .DivisionID = _dbaccess.GetItem("divisionid")
                    .RegionID = _dbaccess.GetItem("regionid")
                    '_dbaccess.GetItem("deptcode") & "-" &
                End With
                _result.Add(_branch)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("BranchAccess", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function Insert(_data As BranchEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(8) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try

            _sql = "INSERT INTO GSBBRC_Branch(deptid,deptcode,deptname,deptpid,deptpcode,deptlevel,deptgroup,divisionid,regionid) "
            _sql = _sql & "VALUES(@deptid,@deptcode,@deptname,@deptpid,@deptpcode,@deptlevel,@deptgroup,@divisionid,@regionid)"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@deptid", .DeptID)
                _param(1) = New SQLServerDBParameter("@deptcode", .DeptCode)
                _param(2) = New SQLServerDBParameter("@deptname", .DeptName)
                _param(3) = New SQLServerDBParameter("@deptpid", .DeptpID)
                _param(4) = New SQLServerDBParameter("@deptpcode", .DeptpCode)
                _param(5) = New SQLServerDBParameter("@deptlevel", .DeptLevel)
                _param(6) = New SQLServerDBParameter("@deptgroup", .DeptGroup)
                _param(7) = New SQLServerDBParameter("@divisionid", .DivisionID)
                _param(8) = New SQLServerDBParameter("@regionid", .RegionID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("BranchAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Insert(_data As BranchEntity, _parentid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(8) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        Dim _deptid As String = ""
        Dim _deptpid As String = ""
        Dim _deptpcode As String = ""
        Dim _divisionid As String = ""
        Dim _regionid As String = ""

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try

            If _parentid.Trim.Length > 0 Then
                _sql = "SELECT deptid,deptcode,deptlevel,divisionid,regionid "
                _sql = _sql & "FROM GSBBRC_Branch a "
                _sql = _sql & "WHERE deptid=" & _parentid
                _dbaccess.ExecuteReader(_sql)

                If _dbaccess.Read Then
                    _deptpid = _dbaccess.GetItem("deptid")
                    _deptpcode = _dbaccess.GetItem("deptcode")
                    _divisionid = _dbaccess.GetItem("divisionid")
                    _regionid = _dbaccess.GetItem("regionid")
                End If
                _dbaccess.CloseReader()
            End If

            _dbaccess.BeginTransaction()
            _sql = "SELECT MAX(deptid)+1 as deptid FROM GSBBRC_Branch "
            _deptid = _dbaccess.ExecuteScalar(_sql)

            _sql = "INSERT INTO GSBBRC_Branch(deptid,deptcode,deptname,deptpid,deptpcode,deptlevel,deptgroup,divisionid,regionid) "
            _sql = _sql & "VALUES(@deptid,@deptcode,@deptname,@deptpid,@deptpcode,@deptlevel,@deptgroup,@divisionid,@regionid)"

            With _data
                _param(0) = New SQLServerDBParameter("@deptid", _deptid)
                _param(1) = New SQLServerDBParameter("@deptcode", .DeptCode)
                _param(2) = New SQLServerDBParameter("@deptname", .DeptName)
                _param(3) = New SQLServerDBParameter("@deptpid", DBUtility.GetNumeric(_deptpid))
                _param(4) = New SQLServerDBParameter("@deptpcode", DBUtility.GetString(_deptpcode))
                _param(5) = New SQLServerDBParameter("@deptlevel", .DeptLevel)
                _param(6) = New SQLServerDBParameter("@deptgroup", DBUtility.GetString(.DeptGroup))
                If .DeptLevel = 3 Then
                    _divisionid = _deptid
                    _regionid = _deptpid
                End If
                _param(7) = New SQLServerDBParameter("@divisionid", DBUtility.GetNumeric(_divisionid))
                _param(8) = New SQLServerDBParameter("@regionid", DBUtility.GetNumeric(_regionid))
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)

            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("BranchAccess", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Update(_data As BranchEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _param(8) As SQLServerDBParameter
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "UPDATE GSBBRC_Branch SET "
            _sql = _sql & "deptcode=@deptcode,deptname=@deptname,deptpid=@deptpid,deptpcode=@deptpcode,"
            _sql = _sql & "deptlevel=@deptlevel,deptgroup=@deptgroup,divisionid=@divisionid,regionid=@regionid "
            _sql = _sql & "WHERE deptid=@deptid"

            _dbaccess.BeginTransaction()
            With _data
                _param(0) = New SQLServerDBParameter("@deptcode", .DeptCode)
                _param(1) = New SQLServerDBParameter("@deptname", .DeptName)
                _param(2) = New SQLServerDBParameter("@deptpid", DBUtility.GetNumeric(.DeptpID))
                _param(3) = New SQLServerDBParameter("@deptpcode", DBUtility.GetString(.DeptpCode))
                _param(4) = New SQLServerDBParameter("@deptlevel", .DeptLevel)
                _param(5) = New SQLServerDBParameter("@deptgroup", DBUtility.GetString(.DeptGroup))
                _param(6) = New SQLServerDBParameter("@divisionid", DBUtility.GetNumeric(.DivisionID))
                _param(7) = New SQLServerDBParameter("@regionid", DBUtility.GetNumeric(.RegionID))
                _param(8) = New SQLServerDBParameter("@deptid", .DeptID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            _dbaccess.RollbackTransaction()
            UtilLogfile.writeToLog("BranchAccess", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            If Not _dbaccess Is Nothing Then
                _dbaccess.Dispose()
                _dbaccess = Nothing
            End If
        End Try

        Return _result
    End Function

    Public Function Delete(ByVal _branceid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _dbaccess As SQLServerDBAccess

        SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
        _dbaccess = New SQLServerDBAccess

        Try
            _sql = "DELETE FROM GSBBRC_Branch WHERE deptid=" & _branceid
            _dbaccess.ExecuteNonQuery(_sql)
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("BranchAccess", "Delete()", ex.Message)
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
