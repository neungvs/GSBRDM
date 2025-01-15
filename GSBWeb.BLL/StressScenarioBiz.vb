Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class StressScenarioBiz
    Dim _stressScenarioAcc As New StressScenarioAccess

    Public Function GetScenarioByTimeId(timeId As String) As List(Of ScenarioEntity)
        Dim _result As List(Of ScenarioEntity)
        _result = _stressScenarioAcc.GetScenarioByTimeId(timeId)
        Return _result
    End Function

    Public Function GetDate() As List(Of TimeEntity)
        Dim _result As List(Of TimeEntity)
        _result = _stressScenarioAcc.GetDate().OrderByDescending(Function(x) x.TimeId).ToList()
        Return _result
    End Function

    Public Function GetByTime(_timeId As String) As List(Of StressScenarioEntity)
        Dim _result As List(Of StressScenarioEntity)
        _result = _stressScenarioAcc.GetDataByTimeId(_timeId)
        Return _result
    End Function

    Public Function SaveCreateNew(timeId As String, userId As String) As Boolean
        Try
            Return _stressScenarioAcc.CreateNew(timeId, userId)
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioBiz", "SaveCreateNew", ex.Message)
        End Try
        Return False
    End Function

    Public Function SaveAdd(_timeId As String, _senarioName As String, _senarioDes As String, _userId As String) As Boolean
        Try
            _stressScenarioAcc.Add(_timeId, _senarioName, _senarioDes, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioBiz", "SaveAdd", ex.Message)
        End Try
        Return False
    End Function

    Public Function SaveUpdate(_timeId As String, _senarioId As String, _senarioName As String, _senarioDes As String, _userId As String) As Boolean
        Try
            _stressScenarioAcc.Update(_timeId, _senarioId, _senarioName, _senarioDes, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioBiz", "SaveUpdate", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _senarioId As String, _userId As String) As Boolean
        Try
            Return _stressScenarioAcc.Delete(_timeId, _senarioId, _userId)
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioBiz", "Delete", ex.Message)
        End Try
        Return False
    End Function

End Class