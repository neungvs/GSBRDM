Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class ScenarioWeightBiz
    Dim _scenarioWeightAcc As New ScenarioWeightAccess

    'Public Function GetFactorDate() As List(Of TimeEntity)
    '    Dim _result As List(Of TimeEntity)
    '    _result = _scenarioWeightAcc.GetFactorDate().OrderByDescending(Function(x) x.TimeId).ToList()
    '    Return _result
    'End Function

    Public Function GetByTime(_timeId As String) As List(Of ScenarioWeightEntity)
        Dim _result As List(Of ScenarioWeightEntity)
        _result = _scenarioWeightAcc.GetDataByTimeId(_timeId)
        Return _result
    End Function

    'Public Function SaveCreateNew(timeId As String, userId As String) As Boolean
    '    Try
    '        _scenarioWeightAcc.CreateNew(timeId, userId)
    '        Return True
    '    Catch ex As Exception
    '        UtilLogfile.writeToLog("FactorNameBiz", "SaveCreateNew", ex.Message)
    '    End Try
    '    Return False
    'End Function

    Public Function SaveAdd(_timeId As String, _scenarioId As Integer, _weight As Decimal) As Boolean
        Try
            _scenarioWeightAcc.Add(_timeId, _scenarioId, _weight)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameBiz", "SaveAdd", ex.Message)
        End Try
        Return False
    End Function

    Public Function SaveUpdate(_timeId As String, _scenarioId As Integer, _weight As Decimal) As Boolean
        Try
            _scenarioWeightAcc.Update(_timeId, _scenarioId, _weight)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameBiz", "SaveUpdate", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _scenarioId As Integer) As Boolean
        Try
            _scenarioWeightAcc.Delete(_timeId, _scenarioId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameBiz", "Delete", ex.Message)
        End Try
        Return False
    End Function

    'Public Function GetFactor(timeId As String) As List(Of FactorEntity)
    '    Dim _result As List(Of FactorEntity)
    '    _result = _scenarioWeightAcc.GetFactorByTimeId(timeId)
    '    Return _result
    'End Function

End Class