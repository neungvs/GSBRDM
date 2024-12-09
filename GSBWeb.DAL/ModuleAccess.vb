Imports Arsoft.Utility

Public Class ModuleAccess
    'Dim dbCon As New DBclass("ConnectionString_Report")
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString("ConnectionString_Report")
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function GetUserModules(_userid As String, _moduleid As String, _levelid As Integer, _ismenu As Integer) As List(Of ModuleEntity)
        Dim _sql As String
        Dim _module As ModuleEntity
        'Dim dt As DataTable

        _sql = String.Format("EXEC sp_UserPrivilege {0},'{1}',{2},{3}", _userid, _moduleid, _levelid, _ismenu)
        _dbaccess.ExecuteReader(_sql)

        Dim lsModules As New List(Of ModuleEntity)
        Do While _dbaccess.Read
            _module = New ModuleEntity
            With _module
                .ModuleID = _dbaccess.GetItem("moduleid")
                .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                .ParentID = _dbaccess.GetItem("parentid")
                .LevelID = _dbaccess.GetItem("levelid")
                .MenuSeq = _dbaccess.GetItem("menuseq")
                .HeaderSeq = _dbaccess.GetItem("headerseq")
                .IsHeaderSeq = _dbaccess.GetItem("isheaderseq")
                .LinkPage = _dbaccess.GetItem("linkpage")
                .LV = _dbaccess.GetItem("lv")
            End With
            lsModules.Add(_module)
        Loop
        _dbaccess.CloseReader()

        Return lsModules
    End Function

    Public Function GetUserInfo(ByVal _username As String) As UserEntity
        Dim _result As UserEntity = Nothing
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT *,IIF(timefail is NULL, 0, DATEDIFF(second,timefail,GETDATE())) as checktime FROM rdmsys_users  WHERE disabled=0 AND username=@username"

            _param(0) = New SQLServerDBParameter("@username", _username)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read
                _result = New UserEntity
                With _result
                    .UserID = _dbaccess.GetItem("userid")
                    .UserName = _dbaccess.GetItem("username")
                    .Password = _dbaccess.GetItem("password")
                    .FirstNameEN = _dbaccess.GetItem("firstnameen")
                    .LastNameEN = _dbaccess.GetItem("lastnameen")
                    .FirstNameTH = _dbaccess.GetItem("firstnameth")
                    .LastNameTH = _dbaccess.GetItem("lastnameth")
                    .CountFail = _dbaccess.GetItem("countfail")
                    .TimeFail = _dbaccess.GetItem("timefail")
                    .CheckTime = _dbaccess.GetItem("checktime")
                    .UserFlag = _dbaccess.GetItem("userflag")
                    .EmployeeID = _dbaccess.GetItem("EmployeeID")
                    .GroupID = _dbaccess.GetItem("groupid")
                End With
            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function IsPrivilage(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _user As Integer = 0
        'Dim _dbaccess As SQLServerDBAccess
        Dim _param(1) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            _sql = "SELECT COUNT(*) FROM rdmsys_users WHERE disabled=0 AND userid=@userid"

            _param(0) = New SQLServerDBParameter("@userid", _userid)
            _user = _dbaccess.ExecuteScalar(_sql, _param)

            If _user > 0 Then
                _result = True
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "IsPrivilage()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Function IsLogin(ByVal _userid As String, _ispass As Boolean) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String
        Dim _user As Integer = 0
        'Dim _dbaccess As SQLServerDBAccess
        Dim _param(1) As SQLServerDBParameter

        'SQLServerDBConfiguration.ConnectionString = DBUtility.ReportConnectionString
        '_dbaccess = New SQLServerDBAccess

        Try
            If _ispass Then
                _sql = "UPDATE rdmsys_users SET countfail=0,timefail=NULL WHERE userid=@userid"
            Else
                _sql = "UPDATE rdmsys_users SET countfail=countfail+1,timefail=IIF(countfail>1 or countfail>3 ,GETDATE(),timefail) WHERE userid=@userid"
            End If

            _param(0) = New SQLServerDBParameter("@userid", _userid)
            _dbaccess.ExecuteScalar(_sql, _param)

            _result = True

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "IsLogin()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            'If Not _dbaccess Is Nothing Then
            '    _dbaccess.Dispose()
            '    _dbaccess = Nothing
            'End If
        End Try

        Return _result
    End Function

    Public Sub Dispose()
        If Not _dbaccess Is Nothing Then
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End If
    End Sub

    Public Function GetDataFromparam() As List(Of ModuleEntity)
        Dim _result As ModuleEntity
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter
        Dim IsModules As New List(Of ModuleEntity)

        Try
            _sql = "SELECT moduleid,ISNULL( modulename_en,' ') modulename_en,ISNULL( modulename_th,' ') modulename_th,levelid,ISNULL(parentid,0) parentid,ISNULL(menuseq,0) menuseq,ISNULL(headerseq,0) headerseq,isheaderseq,ISNULL(linkpage,' ') linkpage FROM rdmsys_module WHERE isactive=@isactive"

            _param(0) = New SQLServerDBParameter("@isactive", 1)

            _dbaccess.ExecuteReader(_sql, _param)



            Do While _dbaccess.Read
                _result = New ModuleEntity
                With _result
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                    .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    .LevelID = _dbaccess.GetItem("levelid")
                    .ParentID = _dbaccess.GetItem("parentid")
                    .MenuSeq = _dbaccess.GetItem("menuseq")
                    .HeaderSeq = _dbaccess.GetItem("headerseq")
                    .IsHeaderSeq = _dbaccess.GetItem("isheaderseq")
                    .LinkPage = _dbaccess.GetItem("linkpage")
                End With
                IsModules.Add(_result)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return IsModules

    End Function
    Public Function GetDatagroupModulePrivilage(ByVal _data As String) As List(Of ModuleEntity)

        Dim _module As ModuleEntity
        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        _sql = "SELECT rdmgp.moduleid,rdmgp.groupid,rdmm.levelid,rdmm.modulename_th,rdmm.modulename_en FROM rdmsys_groupmodule_privilege rdmgp " &
               "LEFT JOIN rdmsys_module rdmm ON rdmgp.moduleid = rdmm.moduleid  WHERE groupid = @groupid order by rdmm.moduleid"
        _param(0) = New SQLServerDBParameter("groupid", _data)
        _dbaccess.ExecuteReader(_sql, _param)
        Dim ReturnData As New List(Of ModuleEntity)
        Try
            Do While _dbaccess.Read
                _module = New ModuleEntity
                With _module
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .GroupID = _dbaccess.GetItem("groupid")
                    .LevelID = _dbaccess.GetItem("levelid")
                    .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                End With
                ReturnData.Add(_module)
            Loop
            _dbaccess.CloseReader()

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return ReturnData

    End Function

    Public Function GetModuleLevelID() As List(Of ModuleEntity)
        Dim _module As ModuleEntity
        Dim _sql As String

        _sql = "SELECT levelid  FROM rdmsys_module GROUP BY levelid;"
        _dbaccess.ExecuteReader(_sql)
        Dim returndata As New List(Of ModuleEntity)
        Try
            Do While _dbaccess.Read
                _module = New ModuleEntity
                With _module
                    .LevelID = _dbaccess.GetItem("levelid")
                End With
                returndata.Add(_module)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return returndata
    End Function

    Public Function GetModuleLevel(ByVal _level As Integer) As List(Of ModuleEntity)
        Dim _module As ModuleEntity
        Dim _sql As String
        Dim _ReturnData As New List(Of ModuleEntity)
        Dim _param(6) As SQLServerDBParameter

        If _level = 0 Then
            Try
                _sql = "SELECT  moduleid,modulename_en,ISNULL(modulename_th,' ') modulename_th,menuseq FROM rdmsys_module WHERE levelid = @levelid AND isactive=@isactive "
                _param(0) = New SQLServerDBParameter("@levelid", _level)
                _param(1) = New SQLServerDBParameter("@isactive", 1)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try

        ElseIf _level = 1 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid as moduleid,tb2.modulename_en ,tb2.modulename_th ,tb2.menuseq FROM rdmsys_module as tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb2.isactive = @isactive;"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 1)
                _param(1) = New SQLServerDBParameter("@levelid2", _level)
                _param(2) = New SQLServerDBParameter("@isactive", 1)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        ElseIf _level = 2 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid moduleid_level1,tb2.modulename_en modulename_en_level1,tb2.modulename_th modulename_th_level1,tb3.moduleid as moduleid,"
                _sql &= "tb3.modulename_en,tb3.modulename_th,tb3.menuseq FROM rdmsys_module as tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb3 ON tb2.moduleid = tb3.parentid "
                _sql &= "WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb3.levelid = @levelid3 AND tb3.isactive=@isactive"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 2)
                _param(1) = New SQLServerDBParameter("@levelid2", _level - 1)
                _param(2) = New SQLServerDBParameter("@levelid3", _level)
                _param(3) = New SQLServerDBParameter("@isactive", 1)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID_Level1 = _dbaccess.GetItem("moduleid_level1")
                        .ModuleName_EN_Level1 = _dbaccess.GetItem("modulename_en_level1")
                        .ModuleName_TH_Level1 = _dbaccess.GetItem("modulename_th_level1")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try

        ElseIf _level = 3 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid moduleid_level1,tb2.modulename_en modulename_en_level1,tb2.modulename_th modulename_th_level1,"
                _sql &= "tb3.moduleid moduleid_level2,tb3.modulename_en modulename_en_level2,tb3.modulename_th modulename_th_level2,"
                _sql &= "tb4.moduleid as moduleid,tb4.modulename_en,tb4.modulename_th,tb4.menuseq "
                _sql &= "FROM rdmsys_module AS tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb3 ON tb2.moduleid = tb3.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb4 ON tb3.moduleid = tb4.parentid "
                _sql &= "WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb3.levelid = @levelid3 AND tb4.levelid = @levelid4 AND tb4.isactive = @isactive"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 3)
                _param(1) = New SQLServerDBParameter("@levelid2", _level - 2)
                _param(2) = New SQLServerDBParameter("@levelid3", _level - 1)
                _param(3) = New SQLServerDBParameter("@levelid4", _level)
                _param(4) = New SQLServerDBParameter("@isactive", 1)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID_Level1 = _dbaccess.GetItem("moduleid_level1")
                        .ModuleName_EN_Level1 = _dbaccess.GetItem("modulename_en_level1")
                        .ModuleName_TH_Level1 = _dbaccess.GetItem("modulename_th_level1")
                        .ModuleID_Level2 = _dbaccess.GetItem("moduleid_level2")
                        .ModuleName_EN_Level2 = _dbaccess.GetItem("modulename_en_level2")
                        .ModuleName_TH_Level2 = _dbaccess.GetItem("modulename_th_level2")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try

        ElseIf _level = 4 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid moduleid_level1,tb2.modulename_en modulename_en_level1,tb2.modulename_th modulename_th_level1,"
                _sql &= "tb3.moduleid moduleid_level2,tb3.modulename_en modulename_en_level2,tb3.modulename_th modulename_th_level2,"
                _sql &= "tb4.moduleid moduleid_level3,tb4.modulename_en modulename_en_level3,tb4.modulename_th modulename_th_level3,"
                _sql &= "tb5.moduleid as moduleid,tb5.modulename_en,tb5.modulename_th,tb5.menuseq "
                _sql &= "FROM rdmsys_module AS tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb3 ON tb2.moduleid = tb3.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb4 ON tb3.moduleid = tb4.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb5 ON tb4.moduleid = tb5.parentid "
                _sql &= "WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb3.levelid = @levelid3 AND tb4.levelid = @levelid4 AND tb5.levelid = @levleidt5 AND tb5.isactive = @isactive;"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 4)
                _param(1) = New SQLServerDBParameter("@levelid2", _level - 3)
                _param(2) = New SQLServerDBParameter("@levelid3", _level - 2)
                _param(3) = New SQLServerDBParameter("@levelid4", _level - 1)
                _param(4) = New SQLServerDBParameter("@levleidt5", _level)
                _param(5) = New SQLServerDBParameter("@isactive", 1)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID_Level1 = _dbaccess.GetItem("moduleid_level1")
                        .ModuleName_EN_Level1 = _dbaccess.GetItem("modulename_en_level1")
                        .ModuleName_TH_Level1 = _dbaccess.GetItem("modulename_th_level1")
                        .ModuleID_Level2 = _dbaccess.GetItem("moduleid_level2")
                        .ModuleName_EN_Level2 = _dbaccess.GetItem("modulename_en_level2")
                        .ModuleName_TH_Level2 = _dbaccess.GetItem("modulename_th_level2")
                        .ModuleID_Level3 = _dbaccess.GetItem("moduleid_level3")
                        .ModuleName_EN_Level3 = _dbaccess.GetItem("modulename_en_level3")
                        .ModuleName_TH_Level3 = _dbaccess.GetItem("modulename_th_level3")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        End If
        _dbaccess.CloseReader()
        Return _ReturnData
    End Function

    Public Function GetModuleLevelByParent(ByVal _level As Integer, ByVal _parent As Integer) As List(Of ModuleEntity)
        Dim _module As ModuleEntity
        Dim _sql As String
        Dim _ReturnData As New List(Of ModuleEntity)
        Dim _param(6) As SQLServerDBParameter

        If _level = 1 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid as moduleid,tb2.modulename_en ,tb2.modulename_th ,tb2.menuseq FROM rdmsys_module as tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb2.isactive = @isactive and tb2.parentid=@parentid;"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 1)
                _param(1) = New SQLServerDBParameter("@levelid2", _level)
                _param(2) = New SQLServerDBParameter("@isactive", 1)
                _param(3) = New SQLServerDBParameter("@parentid", _parent)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        ElseIf _level = 2 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid moduleid_level1,tb2.modulename_en modulename_en_level1,tb2.modulename_th modulename_th_level1,tb3.moduleid as moduleid,"
                _sql &= "tb3.modulename_en,tb3.modulename_th,tb3.menuseq FROM rdmsys_module as tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb3 ON tb2.moduleid = tb3.parentid "
                _sql &= "WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb3.levelid = @levelid3 AND tb3.isactive=@isactive and tb3.parentid=@parentid"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 2)
                _param(1) = New SQLServerDBParameter("@levelid2", _level - 1)
                _param(2) = New SQLServerDBParameter("@levelid3", _level)
                _param(3) = New SQLServerDBParameter("@isactive", 1)
                _param(4) = New SQLServerDBParameter("@parentid", _parent)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID_Level1 = _dbaccess.GetItem("moduleid_level1")
                        .ModuleName_EN_Level1 = _dbaccess.GetItem("modulename_en_level1")
                        .ModuleName_TH_Level1 = _dbaccess.GetItem("modulename_th_level1")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try

        ElseIf _level = 3 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid moduleid_level1,tb2.modulename_en modulename_en_level1,tb2.modulename_th modulename_th_level1,"
                _sql &= "tb3.moduleid moduleid_level2,tb3.modulename_en modulename_en_level2,tb3.modulename_th modulename_th_level2,"
                _sql &= "tb4.moduleid as moduleid,tb4.modulename_en,tb4.modulename_th,tb4.menuseq "
                _sql &= "FROM rdmsys_module AS tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb3 ON tb2.moduleid = tb3.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb4 ON tb3.moduleid = tb4.parentid "
                _sql &= "WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb3.levelid = @levelid3 AND tb4.levelid = @levelid4 AND tb4.isactive = @isactive and tb4.parentid=@parentid"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 3)
                _param(1) = New SQLServerDBParameter("@levelid2", _level - 2)
                _param(2) = New SQLServerDBParameter("@levelid3", _level - 1)
                _param(3) = New SQLServerDBParameter("@levelid4", _level)
                _param(4) = New SQLServerDBParameter("@isactive", 1)
                _param(5) = New SQLServerDBParameter("@parentid", _parent)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID_Level1 = _dbaccess.GetItem("moduleid_level1")
                        .ModuleName_EN_Level1 = _dbaccess.GetItem("modulename_en_level1")
                        .ModuleName_TH_Level1 = _dbaccess.GetItem("modulename_th_level1")
                        .ModuleID_Level2 = _dbaccess.GetItem("moduleid_level2")
                        .ModuleName_EN_Level2 = _dbaccess.GetItem("modulename_en_level2")
                        .ModuleName_TH_Level2 = _dbaccess.GetItem("modulename_th_level2")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try

        ElseIf _level = 4 Then
            Try
                _sql = "SELECT tb1.moduleid moduleid_level0,tb1.modulename_en modulename_en_level0,ISNULL(tb1.modulename_th,' ') modulename_th_level0,"
                _sql &= "tb2.moduleid moduleid_level1,tb2.modulename_en modulename_en_level1,tb2.modulename_th modulename_th_level1,"
                _sql &= "tb3.moduleid moduleid_level2,tb3.modulename_en modulename_en_level2,tb3.modulename_th modulename_th_level2,"
                _sql &= "tb4.moduleid moduleid_level3,tb4.modulename_en modulename_en_level3,tb4.modulename_th modulename_th_level3,"
                _sql &= "tb5.moduleid as moduleid,tb5.modulename_en,tb5.modulename_th,tb5.menuseq "
                _sql &= "FROM rdmsys_module AS tb1 "
                _sql &= "LEFT JOIN rdmsys_module AS tb2 ON tb1.moduleid = tb2.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb3 ON tb2.moduleid = tb3.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb4 ON tb3.moduleid = tb4.parentid "
                _sql &= "LEFT JOIN rdmsys_module AS tb5 ON tb4.moduleid = tb5.parentid "
                _sql &= "WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb3.levelid = @levelid3 AND tb4.levelid = @levelid4 AND tb5.levelid = @levleidt5 AND tb5.isactive = @isactive and tb5.parentid=@parentid;"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 4)
                _param(1) = New SQLServerDBParameter("@levelid2", _level - 3)
                _param(2) = New SQLServerDBParameter("@levelid3", _level - 2)
                _param(3) = New SQLServerDBParameter("@levelid4", _level - 1)
                _param(4) = New SQLServerDBParameter("@levleidt5", _level)
                _param(5) = New SQLServerDBParameter("@isactive", 1)
                _param(6) = New SQLServerDBParameter("@parentid", _parent)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read
                    _module = New ModuleEntity
                    With _module
                        .ModuleID_Level0 = _dbaccess.GetItem("moduleid_level0")
                        .ModuleName_EN_Level0 = _dbaccess.GetItem("modulename_en_level0")
                        .ModuleName_TH_Level0 = _dbaccess.GetItem("modulename_th_level0")
                        .ModuleID_Level1 = _dbaccess.GetItem("moduleid_level1")
                        .ModuleName_EN_Level1 = _dbaccess.GetItem("modulename_en_level1")
                        .ModuleName_TH_Level1 = _dbaccess.GetItem("modulename_th_level1")
                        .ModuleID_Level2 = _dbaccess.GetItem("moduleid_level2")
                        .ModuleName_EN_Level2 = _dbaccess.GetItem("modulename_en_level2")
                        .ModuleName_TH_Level2 = _dbaccess.GetItem("modulename_th_level2")
                        .ModuleID_Level3 = _dbaccess.GetItem("moduleid_level3")
                        .ModuleName_EN_Level3 = _dbaccess.GetItem("modulename_en_level3")
                        .ModuleName_TH_Level3 = _dbaccess.GetItem("modulename_th_level3")
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                        .MenuSeq = _dbaccess.GetItem("menuseq")
                    End With
                    _ReturnData.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        End If
        _dbaccess.CloseReader()
        Return _ReturnData
    End Function

    Public Function SelectDataFilteFromModuleid(ByVal _moduleid As String, ByVal _level As Integer) As List(Of ModuleEntity)
        Dim _Return As New List(Of ModuleEntity)
        Dim _module As ModuleEntity
        Dim _sql As String
        Dim _param(7) As SQLServerDBParameter
        If _level = 0 Then
            Try
                _sql = "SELECT tb1.moduleid as moduleid,tb1.modulename_en,tb1.modulename_th FROM rdmsys_module tb1 WHERE tb1.levelid = @levelid AND tb1.isactive = @isactive order by menuseq"
                _param(0) = New SQLServerDBParameter("@levelid", _level)
                _param(1) = New SQLServerDBParameter("@isactive", 1)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read()
                    _module = New ModuleEntity
                    With _module
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    End With
                    _Return.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        Else
            Try
                _sql = "SELECT tb2.moduleid as moduleid,tb2.modulename_en,tb2.modulename_th FROM rdmsys_module tb1 " &
                       "LEFT JOIN rdmsys_module as tb2 ON tb1.moduleid = tb2.parentid " &
                       "WHERE tb1.levelid = @levelid1 AND tb2.levelid = @levelid2 AND tb2.isactive = @isactive AND  tb1.moduleid = @moduleid"
                _param(0) = New SQLServerDBParameter("@levelid1", _level - 1)
                _param(1) = New SQLServerDBParameter("@levelid2", _level)
                _param(2) = New SQLServerDBParameter("@isactive", 1)
                _param(3) = New SQLServerDBParameter("@moduleid", _moduleid)
                _dbaccess.ExecuteReader(_sql, _param)
                Do While _dbaccess.Read()
                    _module = New ModuleEntity
                    With _module
                        .ModuleID = _dbaccess.GetItem("moduleid")
                        .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                        .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    End With
                    _Return.Add(_module)
                Loop
            Catch ex As Exception
                UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        End If
        _dbaccess.CloseReader()
        Return _Return
    End Function

    Public Function InserModule(ByVal _data As ModuleEntity) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _param(10) As SQLServerDBParameter

        Try

            _dbaccess.BeginTransaction()
            If _data.ParentID = Nothing And _data.HeaderSeq = Nothing Then
                _sql = "INSERT INTO rdmsys_module(modulename_en,modulename_th,levelid,isheaderseq,linkpage,isactive,moduleid)" &
                  "VALUES (@modulename_en,@modulename_en,@levelid,@isheaderseq,@linkpage,@isactive,@moduleid)"
            ElseIf _data.ParentID <> Nothing And _data.HeaderSeq = Nothing Then
                _sql = "INSERT INTO rdmsys_module(modulename_en,modulename_th,levelid,parentid,menuseq,isheaderseq,linkpage,isactive,moduleid)" &
                "VALUES (@modulename_th,@modulename_th,@levelid,@parentid,@menuseq,@isheaderseq,@linkpage,@isactive,@moduleid)"
            ElseIf _data.ParentID <> Nothing And _data.HeaderSeq = Nothing Then
                _sql = "INSERT INTO rdmsys_module(modulename_en,modulename_th,levelid,parentid,menuseq,isheaderseq,linkpage,isactive,moduleid)" &
                "VALUES (@modulename_th,@modulename_th,@levelid,@parentid,@menuseq,@isheaderseq,@linkpage,@isactive,@moduleid)"
            Else
                _sql = "INSERT INTO rdmsys_module(modulename_en,modulename_th,levelid,parentid,menuseq,headerseq,isheaderseq,linkpage,isactive,moduleid)" &
                  "VALUES (@modulename_th,@modulename_th,@levelid,@parentid,@menuseq,@headerseq,@isheaderseq,@linkpage,@isactive,@moduleid)"
            End If


            With _data
                If .ModuleNameEN <> Nothing Then
                    _param(0) = New SQLServerDBParameter("@modulename_en", .ModuleNameEN)
                End If

                _param(1) = New SQLServerDBParameter("@modulename_th", .ModuleNameTH)
                _param(2) = New SQLServerDBParameter("@levelid", .LevelID)
                If .ParentID <> Nothing Then
                    _param(3) = New SQLServerDBParameter("@parentid", .ParentID)
                End If
                If .MenuSeq <> Nothing Then
                    _param(4) = New SQLServerDBParameter("@menuseq", .MenuSeq)
                End If
                If .HeaderSeq <> Nothing Then
                    _param(5) = New SQLServerDBParameter("@headerseq", .HeaderSeq)
                End If
                _param(6) = New SQLServerDBParameter("@isheaderseq", 1)

                _param(7) = New SQLServerDBParameter("@linkpage", .LinkPage)

                _param(8) = New SQLServerDBParameter("@isactive", 1)
                _param(9) = New SQLServerDBParameter("@moduleid", .ModuleID)
            End With
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function
    Public Function DeleteModule(ByVal _moduleid As String) As Boolean
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _param(2) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()
            _sql = "UPDATE rdmsys_module SET isactive=@isactive WHERE moduleid=@moduleid;"
            _param(0) = New SQLServerDBParameter("@isactive", 0)
            _param(1) = New SQLServerDBParameter("@moduleid", _moduleid)
            _dbaccess.ExecuteNonQuery(_sql, _param)
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _result
    End Function

    Public Function UpdateModule(ByVal _data As ModuleEntity, ByVal _level As Integer)
        Dim _result As Boolean = False
        Dim _sql As String = ""
        Dim _param(9) As SQLServerDBParameter

        Try
            _dbaccess.BeginTransaction()
            If _level = 0 Then
                If _data.LinkPage = Nothing Then
                    _sql = "UPDATE rdmsys_module SET modulename_en=@modulename_en ,modulename_th=@modulename_th ,levelid=@levelid,isactive=@isactive " &
                    "WHERE moduleid=@moduleid;"
                Else
                    _sql = "UPDATE rdmsys_module SET modulename_en=@modulename_en ,modulename_th=@modulename_th ,levelid=@levelid ,linkpage=@linkpage ,isactive=@isactive " &
                      "WHERE moduleid=@moduleid;"
                End If


                With _data
                    _param(0) = New SQLServerDBParameter("@modulename_en", .ModuleNameEN)
                    _param(1) = New SQLServerDBParameter("@modulename_th", .ModuleNameEN)
                    _param(2) = New SQLServerDBParameter("@levelid", .LevelID)
                    If .LinkPage <> Nothing Then
                        _param(3) = New SQLServerDBParameter("@linkpage", .LinkPage)
                    End If
                    _param(4) = New SQLServerDBParameter("@moduleid", .ModuleID)
                    _param(5) = New SQLServerDBParameter("@isactive", 1)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            Else
                _sql = "UPDATE rdmsys_module SET modulename_en=@modulename_en ,modulename_th=@modulename_th , levelid=@levelid ," &
                       "linkpage=@linkpage ,menuseq=@menuseq, parentid=@parentid, isactive=@isactive WHERE moduleid=@moduleid"
                With _data
                    _param(0) = New SQLServerDBParameter("@modulename_en", .ModuleNameTH)
                    _param(1) = New SQLServerDBParameter("@modulename_th", .ModuleNameTH)
                    _param(2) = New SQLServerDBParameter("@levelid", .LevelID)
                    _param(3) = New SQLServerDBParameter("@parentid", .ParentID)
                    _param(4) = New SQLServerDBParameter("@menuseq", .MenuSeq)
                    _param(5) = New SQLServerDBParameter("@linkpage", .LinkPage)
                    _param(6) = New SQLServerDBParameter("@moduleid", .ModuleID)
                    _param(7) = New SQLServerDBParameter("@isactive", 1)
                End With
                _dbaccess.ExecuteNonQuery(_sql, _param)
            End If
            _dbaccess.CommitTransaction()
            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        Return _result
    End Function

    Public Function CheckDataActive(ByVal _moduleid As String) As Boolean
        Dim _sql As String
        Dim _Returns As Boolean
        Dim _param(1) As SQLServerDBParameter
        Dim _Return As New List(Of ModuleEntity)
        Dim _module As ModuleEntity
        Try
            _sql = "SELECT moduleid FROM rdmsys_module WHERE moduleid=@moduleid"
            _param(0) = New SQLServerDBParameter("@moduleid", _moduleid)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                _module = New ModuleEntity
                With _module
                    .ID = _dbaccess.GetItem("moduleid")
                End With
                _Return.Add(_module)
            Loop

            _dbaccess.CloseReader()
            If _Return.Count = 0 Then
                _Returns = True
            End If
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _Returns
    End Function

    Public Function CountItemAllModule() As Integer
        Dim _sql As String
        Dim _result As Integer
        Dim _param(1) As SQLServerDBParameter
        Return _result
    End Function

    Public Function GetModulelevel0FromID(ByVal _moduleid As Integer) As ModuleEntity
        Dim _return As New ModuleEntity
        Dim _param(1) As SQLServerDBParameter
        Dim _sql As String = ""
        Try
            _sql = "SELECT t1.moduleid as moduleid,ISNULL( t1.modulename_en,'') modulename_en,ISNULL(t1.modulename_th,'') modulename_th,ISNULL(t1.levelid,'') levelid,ISNULL(t1.linkpage,'') linkpage, menuseq,moduleid" &
                   " FROM rdmsys_module t1" &
                   " WHERE t1.moduleid = @moduleid and t1.isactive = @isactive"

            _param(0) = New SQLServerDBParameter("@moduleid", _moduleid)
            _param(1) = New SQLServerDBParameter("@isactive", 1)

            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                With _return
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                    .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    .LevelID = _dbaccess.GetItem("levelid")
                    .LinkPage = _dbaccess.GetItem("linkpage")
                    .MenuSeq = _dbaccess.GetItem("menuseq")
                End With
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return _return
    End Function

    Public Function GetModulelevel1FromID(ByVal _moduleid As Integer) As ModuleEntity
        Dim _return As New ModuleEntity
        Dim _param(1) As SQLServerDBParameter
        Dim _sql As String = ""
        Try
            _sql = "SELECT t1.moduleid as moduleid,ISNULL(t1.modulename_en,'') modulename_en,ISNULL(t1.modulename_th,'') modulename_th,ISNULL(t1.levelid,'') levelid," &
                   "ISNULL(t1.parentid,'') parentid,ISNULL(t1.menuseq,'') menuseq,ISNULL(t1.linkpage,'')linkpage " &
                   "FROM rdmsys_module t1 " &
                   "LEFT JOIN rdmsys_module t2 on t1.parentid = t2.moduleid " &
                   "WHERE t1.moduleid=@moduleid and t1.isactive = @isactive"

            _param(0) = New SQLServerDBParameter("@moduleid", _moduleid)
            _param(1) = New SQLServerDBParameter("@isactive", 1)

            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                With _return
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                    .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    .LevelID = _dbaccess.GetItem("levelid")
                    .ParentID = _dbaccess.GetItem("parentid")
                    .MenuSeq = _dbaccess.GetItem("menuseq")
                    .LinkPage = _dbaccess.GetItem("linkpage")
                End With
            Loop

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        _dbaccess.CloseReader()
        Return _return
    End Function

    Public Function GetModulelevel2FromID(ByVal _moduleid As Integer) As ModuleEntity
        Dim _return As New ModuleEntity
        Dim _param(1) As SQLServerDBParameter
        Dim _sql As String = ""
        Try
            _sql = "SELECT t1.moduleid as moduleid,ISNULL(t1.modulename_en,'') modulename_en,ISNULL(t1.modulename_th,'') modulename_th,ISNULL(t1.levelid,0) levelid," &
                   "ISNULL(t2.parentid,0) parentid_lv1,ISNULL(t1.parentid,'') parentid,ISNULL(t1.menuseq,0) menuseq,ISNULL(t1.linkpage,'') linkpage " &
                   "FROM rdmsys_module t1 " &
                   "LEFT JOIN rdmsys_module t2 on t1.parentid = t2.moduleid " &
                   "LEFT JOIN rdmsys_module t3 on t2.parentid = t3.moduleid " &
                   "WHERE t1.moduleid = @moduleid and t1.isactive = @isactive"

            _param(0) = New SQLServerDBParameter("@moduleid", _moduleid)
            _param(1) = New SQLServerDBParameter("@isactive", 1)
            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read
                With _return
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                    .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    .LevelID = _dbaccess.GetItem("levelid")
                    .ParentID_Level1 = _dbaccess.GetItem("parentid_lv1")
                    .ParentID = _dbaccess.GetItem("parentid")
                    .MenuSeq = _dbaccess.GetItem("menuseq")
                    .LinkPage = _dbaccess.GetItem("linkpage")
                End With
            Loop

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        _dbaccess.CloseReader()
        Return _return
    End Function

    Public Function GetModulelevel3FromID(ByVal _moduleid As Integer) As ModuleEntity
        Dim _return As New ModuleEntity
        Dim _param(1) As SQLServerDBParameter
        Dim _sql As String = ""
        Try
            _sql = "SELECT t1.moduleid as moduleid,ISNULL(t1.modulename_en,'') modulename_en,ISNULL(t1.modulename_th,'') modulename_th,ISNULL(t1.levelid,0) levelid," &
                   "ISNULL(t3.parentid,'') parentid_lv1,ISNULL(t2.parentid,'') parentid_lv2,ISNULL(t1.parentid,'') parentid,ISNULL(t1.menuseq,0) menuseq," &
                   "ISNULL(t1.linkpage,'') linkpage " &
                   "FROM rdmsys_module t1 " &
                   "LEFT JOIN rdmsys_module t2 on t1.parentid = t2.moduleid " &
                   "LEFT JOIN rdmsys_module t3 on t2.parentid = t3.moduleid " &
                   "LEFT JOIN rdmsys_module t4 on t3.parentid = t4.moduleid " &
                   "WHERE t1.moduleid = @moduleid and t1.isactive = @isactive"

            _param(0) = New SQLServerDBParameter("@moduleid", _moduleid)
            _param(1) = New SQLServerDBParameter("@isactive", 1)

            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                With _return
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                    .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    .LevelID = _dbaccess.GetItem("levelid")
                    .ParentID_Level1 = _dbaccess.GetItem("parentid_lv1")
                    .ParentID_Level2 = _dbaccess.GetItem("parentid_lv2")
                    .ParentID = _dbaccess.GetItem("parentid")
                    .MenuSeq = _dbaccess.GetItem("menuseq")
                    .LinkPage = _dbaccess.GetItem("linkpage")
                End With
            Loop

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        _dbaccess.CloseReader()
        Return _return
    End Function

    Public Function GetModulelevel4FromID(ByVal _moduleid As Integer) As ModuleEntity
        Dim _return As New ModuleEntity
        Dim _param(1) As SQLServerDBParameter
        Dim _sql As String = ""
        Try
            _sql = "SELECT t1.moduleid as moduleid,ISNULL(t1.modulename_en,'') modulename_en,ISNULL(t1.modulename_th,'') modulename_th,ISNULL(t1.levelid,0) levelid," &
                   "ISNULL(t4.parentid,'') parentid_lv1,ISNULL(t3.parentid,'') parentid_lv2,ISNULL(t2.parentid,'') parentid_lv3,ISNULL(t1.parentid,'') parentid," &
                   "ISNULL(t1.menuseq,'') menuseq,ISNULL(t1.linkpage,'') linkpage " &
                   "FROM rdmsys_module t1 " &
                   "LEFT JOIN rdmsys_module t2 on t1.parentid = t2.moduleid " &
                   "LEFT JOIN rdmsys_module t3 on t2.parentid = t3.moduleid " &
                   "LEFT JOIN rdmsys_module t4 on t3.parentid = t4.moduleid " &
                   "LEFT JOIN rdmsys_module t5 on t4.parentid = t5.moduleid " &
                   "WHERE t1.moduleid = @moduleid and t1.isactive = @isactive"

            _param(0) = New SQLServerDBParameter("@moduleid", _moduleid)
            _param(1) = New SQLServerDBParameter("@isactive", 1)

            _dbaccess.ExecuteReader(_sql, _param)

            Do While _dbaccess.Read()
                With _return
                    .ModuleID = _dbaccess.GetItem("moduleid")
                    .ModuleNameEN = _dbaccess.GetItem("modulename_en")
                    .ModuleNameTH = _dbaccess.GetItem("modulename_th")
                    .LevelID = _dbaccess.GetItem("levelid")
                    .ParentID_Level1 = _dbaccess.GetItem("parentid_lv1")
                    .ParentID_Level2 = _dbaccess.GetItem("parentid_lv2")
                    .ParentID_Level3 = _dbaccess.GetItem("parentid_lv3")
                    .ParentID = _dbaccess.GetItem("parentid")
                    .MenuSeq = _dbaccess.GetItem("menuseq")
                    .LinkPage = _dbaccess.GetItem("linkpage")
                End With
            Loop

        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        _dbaccess.CloseReader()
        Return _return
    End Function
    'Public Function GetGroupModule(_datagroup As String) As List(Of ModuleEntity)
    '    Dim _module As ModuleEntity
    '    Dim _sql As String
    '    Dim _param(1) As SQLServerDBParameter
    '    Dim _dbaccess As SQLServerDBAccess

    '    _sql = "SELECT groupid,groupname FROM rdmsys_groupuser WHERE groupid <> @isheaderseq"
    '    _param(0) = New SQLServerDBParameter("@isheaderseq", _datagroup)
    '    _dbaccess.ExecuteReader(_sql, _param)

    '    Dim IsModules As New List(Of ModuleEntity)

    '    Try
    '        Do While _dbaccess.Read
    '            _module = New ModuleEntity
    '            With _module
    '                .IsHeaderSeq = _dbaccess.GetItem("isheaderseq")
    '            End With
    '            IsModules.Add(_module)
    '        Loop
    '        _dbaccess.CloseReader()
    '    Catch ex As Exception
    '        UtilLogfile.writeToLog("ModuleAccess", "GetUserInfo()", ex.Message)
    '        Throw New Exception(ex.Message, ex.InnerException)
    '    End Try
    '    Return IsModules

    'End Function
    'Public Function Insert(_data As BranchEntity) As Boolean
    '    Dim _result As Boolean = False
    '    Dim _sql As String
    '    Dim _param(8) As SQLServerDBParameter
    '    Dim _dbaccess As SQLServerDBAccess

    '    SQLServerDBConfiguration.ConnectionString = DBUtility.GSBBankruptcyConnectionString
    '    _dbaccess = New SQLServerDBAccess

    '    Try

    '        _sql = "INSERT INTO GSBBRC_Branch(deptid,deptcode,deptname,deptpid,deptpcode,deptlevel,deptgroup,divisionid,regionid) "
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



End Class
