Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class ImportMevBiz
    Dim _importMevAcc As New ImportMevAccess

    Public Function DeleteByTimeId(_timeId As String) As Boolean
        Try
            _importMevAcc.Delete(_timeId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevBiz", "Delete", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _scenarioId As Integer, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer) As Boolean
        Try
            _importMevAcc.Delete(_timeId, _scenarioId, _stressYear, _stressMonth, _factorId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevBiz", "Delete", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetByTimeAndFactor(_timeId As String, _factorId As Integer) As List(Of ImportMevEntity)
        Dim _result As List(Of ImportMevEntity)
        _result = _importMevAcc.GetDataByTimeAndFactor(_timeId, _factorId)
        Return _result
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of ImportMevEntity)
        Dim _result As List(Of ImportMevEntity)
        _result = _importMevAcc.GetTemplateByTime(_timeId)
        Return _result
    End Function

    Public Function GetForecastReport(_timeId As String, _scenorioId As Integer) As DataTable
        Dim _result As New DataTable
        _result = _importMevAcc.GetForecastReport(_timeId, _scenorioId)
        Return _result
    End Function

    Public Function Save(_userId As Integer, entity As ImportMevEntity) As Boolean
        Try
            Dim TimeId As String = entity.TimeId
            Dim ScenarioId As Integer = entity.ScenarioId
            Dim StressYear As Integer = entity.StressYear
            Dim StressMonth As Integer = entity.StressMonth
            Dim FactorId As Integer = entity.FactorId
            Dim FactorValue As Decimal = entity.FactorValue
            _importMevAcc.Insert(TimeId, ScenarioId, StressYear, StressMonth, FactorId, FactorValue, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevBiz", "Save", ex.Message)
        End Try
        Return False
    End Function

    'Public Function Save(_userId As Integer, listData As List(Of ImportMevEntity)) As Boolean
    '    Try
    '        For Each entity As ImportMevEntity In listData
    '            Dim TimeId As String = entity.TimeId
    '            Dim ScenarioId As Integer = entity.ScenarioId
    '            Dim StressYear As Integer = entity.StressYear
    '            Dim StressMonth As Integer = entity.StressMonth
    '            Dim FactorId As Integer = entity.FactorId
    '            Dim FactorValue As Decimal = entity.FactorValue
    '            _importMevAcc.InsertExcel(TimeId, ScenarioId, StressYear, StressMonth, FactorId, FactorValue, _userId)
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        UtilLogfile.writeToLog("ImportMevBiz", "Save", ex.Message)
    '    End Try
    '    Return False
    'End Function

    Public Function SaveInsertExcel(_userId As Integer, listData As List(Of ImportMevEntity)) As Boolean
        Try
            For Each entity As ImportMevEntity In listData
                Dim TimeId As String = entity.TimeId
                Dim ScenarioId As Integer = entity.ScenarioId
                Dim StressYear As Integer = entity.StressYear
                Dim StressMonth As Integer = entity.StressMonth
                Dim FactorId As Integer = entity.FactorId
                Dim FactorValue As Decimal = entity.FactorValue
                _importMevAcc.InsertExcel(TimeId, ScenarioId, StressYear, StressMonth, FactorId, FactorValue, _userId)
            Next
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevBiz", "Save", ex.Message)
        End Try
        Return False
    End Function


End Class