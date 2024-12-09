Public Class ModuleManagement

#Region "Attributes"
    '
#End Region

#Region "Methods"

    Public Function GetInfo(_moduleid As String) As ModuleEntity
        Dim _result As ModuleEntity
        Dim _module As New ModuleAccess
        Try
            _result = _module.GetInfo(_moduleid)
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleManagement", "GetInfo()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _module = Nothing
        End Try
        Return _result
    End Function

    Public Function GetInfoLists() As ModuleLists
        Dim _result As ModuleLists
        Dim _module As New ModuleAccess
        Try
            _result = _module.GetInfoLists
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleManagement", "GetInfoLists()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _module = Nothing
        End Try
        Return _result
    End Function

    Public Function Insert(_data As ModuleEntity) As Boolean
        Dim _result As Boolean = False
        Dim _module As New ModuleAccess
        Try
            _result = _module.Insert(_data)
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleManagement", "Insert()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _module = Nothing
        End Try
        Return _result
    End Function

    Public Function Update(_data As ModuleEntity) As Boolean
        Dim _result As Boolean = False
        Dim _module As New ModuleAccess
        Try
            _result = _module.Update(_data)
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleManagement", "Update()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _module = Nothing
        End Try
        Return _result
    End Function

    Public Function Delete(ByVal _moduleid As String) As Boolean
        Dim _result As Boolean = False
        Dim _module As New ModuleAccess
        Try
            _result = _module.Delete(_moduleid)
        Catch ex As Exception
            UtilLogfile.writeToLog("ModuleManagement", "Delete()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _module = Nothing
        End Try
        Return _result
    End Function

#End Region

End Class
