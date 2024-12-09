Imports GSBWeb.DAL

Public Class UserModuleBiz
    Dim _moduleacc As New ModuleAccess
    Dim _useracc As New UserAccess
    Dim _privilageacc As New PrivilegeAccess
    Dim _ActiveDirMange As New ActiveDirectoryManagement

    Public Function GetUserModules(_userid As String, _moduleid As String, _levelid As Integer, _ismenu As Integer) As List(Of ModuleEntity)
        Dim _result As List(Of ModuleEntity)

        _result = _moduleacc.GetUserModules(_userid, _moduleid, _levelid, _ismenu)

        Return _result
    End Function

    Public Function GetUserInfo(ByVal _username As String) As UserEntity
        Dim _result As UserEntity

        _result = _moduleacc.GetUserInfo(_username)

        Return _result
    End Function

    Public Function IsPrivilage(ByVal _userid As String) As Boolean
        Dim _result As Boolean

        _result = _moduleacc.IsPrivilage(_userid)

        Return _result
    End Function

    Public Function IsLogin(ByVal _userid As String, _ispass As Boolean) As Boolean
        Dim _result As Boolean
        _result = _moduleacc.IsLogin(_userid, _ispass)

        Return _result
    End Function

    Public Function GetUserID(_username As String) As Integer
        Dim _result As Integer

        _result = _useracc.GetUserID(_username)

        Return _result

    End Function

    Public Function SetUserInfo(ByVal _user As UserEntity) As Boolean
        Dim _result As Boolean = False

        _result = _useracc.SetUserInfo(_user)

        Return _result
    End Function

    Public Function GetAllUserInfo() As List(Of UserEntity)
        Dim _result As New List(Of UserEntity)

        _result = _useracc.GetAllUserInfo()

        Return _result
    End Function

    Public Function Insert(_data As UserEntity) As Boolean
        Dim _result As Boolean = False

        _result = _useracc.Insert(_data)

        Return _result
    End Function

    Public Function Update(_data As UserEntity) As Boolean
        Dim _result As Boolean = False

        _result = _useracc.Update(_data)

        Return _result
    End Function

    Public Function Delete(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False

        _result = _useracc.Delete(_userid)

        Return _result
    End Function

    Public Function Disabled(ByVal _userid As String) As Boolean
        Dim _result As Boolean = False

        _result = _useracc.Disabled(_userid)

        Return _result
    End Function

    Public Function SearchGroup() As List(Of ModuleEntity)
        Dim _result As List(Of ModuleEntity)
        _result = _moduleacc.GetDataFromparam()
        Return _result
    End Function

    Public Function GetWorkgroup(ByVal type As String) As List(Of UserEntity)
        Dim _result As List(Of UserEntity)
        _result = _useracc.GetGroupUser(" ", type)
        Return _result
    End Function

    Public Function GetGroupUserrows(ByVal _data As String) As UserEntity
        Dim _result As New UserEntity
        _result = _useracc.GetGroupInfo(_data)
        Return _result
    End Function

    Public Function GetGroupData(ByVal _groupid As String) As List(Of ModuleEntity)
        Dim _result As List(Of ModuleEntity)
        _result = _moduleacc.GetDatagroupModulePrivilage(_groupid)
        Return _result
    End Function

    Public Function SaveUserModulePrivilage(ByVal _datalist As List(Of PrivilegeEntity)) As Boolean
        Dim _result As Boolean
        _result = _privilageacc.SetUserPrivilege(_datalist)
        Return _result
    End Function

    Public Function SaveGroupUser(ByVal data As UserEntity) As Boolean
        Dim _result As Boolean
        _result = _useracc.InsertGroupUser(data)
        Return _result
    End Function

    Public Function EditGroupUser(ByVal data As UserEntity) As Boolean
        Dim _result As Boolean
        _result = _useracc.UpdateGroupUser(data)
        Return _result
    End Function

    Public Function DeleteGroupUser(ByVal data As String) As Boolean
        Dim _result As Boolean
        _result = _useracc.DeleteGroupuser(data)
        Return _result
    End Function

    Public Function GetLevelID() As List(Of ModuleEntity)
        Dim _result As New List(Of ModuleEntity)
        _result = _moduleacc.GetModuleLevelID()
        Return _result
    End Function
    Public Function GetDataModuleLevel(ByVal _level As Integer) As List(Of ModuleEntity)
        Dim _result As New List(Of ModuleEntity)
        _result = _moduleacc.GetModuleLevel(_level)
        Return _result
    End Function

    Public Function GetDataModuleLevelByParent(ByVal _level As Integer, ByVal _parent As Integer) As List(Of ModuleEntity)
        Dim _result As New List(Of ModuleEntity)
        _result = _moduleacc.GetModuleLevelByParent(_level, _parent)
        Return _result
    End Function

    Public Function CheckModuledata(ByVal _moduleid As String) As Boolean
        Dim _result As Boolean
        _result = _moduleacc.CheckDataActive(_moduleid)
        Return _result
    End Function

    Public Function SaveDataModule(ByVal _data As ModuleEntity) As Boolean
        Dim _result As Boolean
        _result = _moduleacc.InserModule(_data)
        Return _result
    End Function

    Public Function UpdateDataModule(ByVal _data As ModuleEntity, ByVal _level As Integer) As Boolean
        Dim _result As Boolean
        _result = _moduleacc.UpdateModule(_data, _level)
        Return _result
    End Function

    Public Function DeleteDataModule(ByVal _moduleid As String) As Boolean
        Dim _result As Boolean
        _result = _moduleacc.DeleteModule(_moduleid)
        Return _result
    End Function

    Public Function SelectDataforDropdownlist(ByVal _moduleid As String, ByVal _level As Integer) As List(Of ModuleEntity)
        Dim _result As New List(Of ModuleEntity)
        _result = _moduleacc.SelectDataFilteFromModuleid(_moduleid, _level)
        Return _result
    End Function

    Public Function SelectUserDataFilterbyUsername(ByVal _username As String) As List(Of UserEntity)
        Dim _result As New List(Of UserEntity)
        _result = _useracc.GetUserdataByUsername(_username)
        Return _result
    End Function

    Public Function SelectUserDataFilterbyEmployeeID(ByVal _employeeid As String) As List(Of UserEntity)
        Dim _result As New List(Of UserEntity)
        _result = _useracc.GetUserdataByEmployeeID(_employeeid)
        Return _result
    End Function

    Public Function SaveGroupUserDataPrivilage(ByVal _data As List(Of PrivilegeEntity)) As Boolean
        Dim _result As Boolean
        _result = _privilageacc.SetGroupUserPrivilage(_data)
        Return _result
    End Function
    Public Function SeletGroupUserPrivilage(ByVal _data As Integer) As List(Of PrivilegeEntity)
        Dim _result As List(Of PrivilegeEntity)
        _result = _privilageacc.GetGroupUserPrivilage(_data)
        Return _result
    End Function
    Public Function SelectGroupCodeFromGroupID(ByVal _data As Integer) As String
        Dim _result As String
        _result = _useracc.GetGroupCodeFromGroupID(_data)
        Return _result
    End Function
    Public Function DeleteGroupUserPrivilage(ByVal _data As Integer) As Boolean
        Dim _result As Boolean
        _result = _privilageacc.DeleteGroupUserPrivilage(_data)
        Return _result
    End Function

    Public Function SearchGroupData(ByVal _data As String) As UserEntity
        Dim _result As New UserEntity
        _result = _useracc.SearchGroup(_data)
        Return _result
    End Function

    Public Function SearchGroupDataByGroupNameTH(ByVal _data As String) As UserEntity
        Dim _result As New UserEntity
        _result = _useracc.SearchGroupByGroupNameTH(_data)
        Return _result
    End Function

    Public Function DeleteGroupModule(ByVal _data As Integer) As Boolean
        Dim _result As Boolean
        _result = _privilageacc.DeleteGroupModulePrivilege(_data)
        Return _result
    End Function

    Public Function SaveGroupModulePrivilage(ByVal _data As List(Of PrivilegeEntity)) As Boolean
        Dim _result As Boolean
        _result = _privilageacc.SetGroupPrivilege(_data)
        Return _result
    End Function

    Public Function CountAllModule() As Integer
        Dim _result As Integer
        _result = 0
        Return _result
    End Function

    Public Function SelectGroupLogBetweenDateTime(ByVal _groupid As Integer, ByVal _startdate As String, ByVal _enddate As String) As List(Of UserEntity)
        Dim _result As New List(Of UserEntity)
        _result = _useracc.SelectLogData(_groupid, _startdate, _enddate)
        Return _result
    End Function

    Public Function AddLogdata(ByVal _LogData As UserEntity) As Boolean
        Dim _result As Boolean
        _result = _useracc.InsertLogdata(_LogData)
        Return _result
    End Function

    Public Function GetGroupDataforLogData(ByVal _userid As Integer) As String
        Dim _result As String
        _result = _useracc.GetGroupDataforCreateLogData(_userid)
        Return _result
    End Function

    Public Function GetLevel0Info(ByVal _moduleid As Integer) As ModuleEntity
        Dim _result As New ModuleEntity
        _result = _moduleacc.GetModulelevel0FromID(_moduleid)
        Return _result
    End Function

    Public Function GetLevel1Info(ByVal _moduleid As Integer) As ModuleEntity
        Dim _result As New ModuleEntity
        _result = _moduleacc.GetModulelevel1FromID(_moduleid)
        Return _result
    End Function

    Public Function GetLevel2Info(ByVal _moduleid As Integer) As ModuleEntity
        Dim _result As New ModuleEntity
        _result = _moduleacc.GetModulelevel2FromID(_moduleid)
        Return _result
    End Function

    Public Function GetLevel3Info(ByVal _moduleid As Integer) As ModuleEntity
        Dim _result As New ModuleEntity
        _result = _moduleacc.GetModulelevel3FromID(_moduleid)
        Return _result
    End Function

    Public Function GetLevel4Info(ByVal _moduleid As Integer) As ModuleEntity
        Dim _result As New ModuleEntity
        _result = _moduleacc.GetModulelevel4FromID(_moduleid)
        Return _result
    End Function

    Public Function GetMaxGroupCode() As Integer
        Dim _result As Integer = 0
        _result = _useracc.GetLastGroupCode()
        Return _result
    End Function

    Public Function GetUserRDMData(ByVal _employeeid As Integer) As UserEntity
        Dim _result As New UserEntity
        _result = _useracc.GetUserRDMData(_employeeid)
        Return _result
    End Function


    Public Function GetUserRDMDataByEmpidStr(ByVal _employeeid As String) As UserEntity
        Dim _result As New UserEntity
        _result = _useracc.GetUserRDMDataByEmpIDStr(_employeeid)
        Return _result
    End Function

    Public Function GetADData(ByVal _username As String, ByVal _ldapdirectory As String, ByVal _password As String, ByVal _employeeid As Integer) As UserEntity
        Dim _result As New UserEntity
        _result = _ActiveDirMange.GetUserFromAD(_username, _password, _ldapdirectory, _employeeid)
        Return _result
    End Function
    Public Sub Dispost()
        _useracc.Dispose()
        _moduleacc.Dispose()
    End Sub

    Public Function GenerateModuleID(ByVal _parrentid As Integer, ByVal _levelid As Integer) As Integer
        Dim _result As Integer = 0
        _result = _useracc.GenerateModuleID(_parrentid, _levelid)
        Return _result
    End Function

End Class
