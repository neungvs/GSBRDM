Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class StressScenarioWeightBiz
    Dim _stressScenarioWeightAcc As New StressScenarioWeightAccess

    Public Function GetTime() As List(Of TimeEntity)
        Dim _result As List(Of TimeEntity)
        _result = _stressScenarioWeightAcc.GetTime()
        Return _result
    End Function


    Public Function GetByTime(_timeId As String) As List(Of StressScenarioEntity)
        Dim _result As List(Of StressScenarioEntity)
        _result = _stressScenarioWeightAcc.GetDataByTimeId(_timeId)
        Return _result
    End Function

    Public Function GetScenarioByTime(_timeId As String) As List(Of ScenarioEntity)
        Dim _result As List(Of ScenarioEntity)
        _result = _stressScenarioWeightAcc.GetScenarioByTimeId(_timeId)
        Return _result
    End Function



    Public Function SaveCreateNew(timeId As String, lastestTimeId As String, userId As String) As Boolean
        Try
            Return _stressScenarioWeightAcc.CreateNew(timeId, lastestTimeId, userId)
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioWeightBiz", "SaveCreateNew", ex.Message)
        End Try
        Return False
    End Function

    Public Function SaveAdd(_timeId As String, _senarioName As String, _senarioDes As String, _userId As String) As Boolean
        Try
            _stressScenarioWeightAcc.Add(_timeId, _senarioName, _senarioDes, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioWeightBiz", "SaveAdd", ex.Message)
        End Try
        Return False
    End Function

    Public Function SaveUpdate(_timeId As String, _senarioId As String, _senarioName As String, _senarioDes As String, _userId As String) As Boolean
        Try
            _stressScenarioWeightAcc.Update(_timeId, _senarioId, _senarioName, _senarioDes, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioWeightBiz", "SaveUpdate", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _senarioId As String, _userId As String) As Boolean
        Try
            _stressScenarioWeightAcc.Delete(_timeId, _senarioId, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioWeightBiz", "Delete", ex.Message)
        End Try
        Return False
    End Function

End Class