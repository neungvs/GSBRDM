Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class MacroEconomicFactorBiz
    Dim _macroEconomicFactorAcc As New MacroEconomicFactorAccess

    Public Function GetByTimeAndFactor(_timeId As String, _factorId As Integer) As List(Of MacroEconomicFactorEntity)
        Dim _result As List(Of MacroEconomicFactorEntity)
        _result = _macroEconomicFactorAcc.GetDataByTimeId(_timeId, _factorId)
        Return _result
    End Function

    Public Function SaveImportExcel(_userId As Integer, listEntity As List(Of MacroEconomicFactorEntity)) As Boolean
        Try
            For Each entity As MacroEconomicFactorEntity In listEntity
                Dim TimeId As String = entity.TimeId
                Dim ScenarioId As Integer = entity.ScenarioId
                Dim StressYear As Integer = entity.StressYear
                Dim StressMonth As Integer = entity.StressMonth
                Dim FactorId As Integer = entity.FactorId
                Dim FactorValue As Decimal = entity.FactorValue
                If _macroEconomicFactorAcc.InsertImportExcel(TimeId, StressYear, StressMonth, FactorId, FactorValue) = False Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorBiz", "SaveImportExcel", ex.Message)
        End Try
        Return False
    End Function

    Public Function Save(_userId As Integer, entity As MacroEconomicFactorEntity) As Boolean
        Try
            Dim TimeId As String = entity.TimeId
            Dim ScenarioId As Integer = entity.ScenarioId
            Dim StressYear As Integer = entity.StressYear
            Dim StressMonth As Integer = entity.StressMonth
            Dim FactorId As Integer = entity.FactorId
            Dim FactorValue As Decimal = entity.FactorValue
            If _macroEconomicFactorAcc.Insert(TimeId, StressYear, StressMonth, FactorId, FactorValue) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorBiz", "Save", ex.Message)
        End Try
        Return False
    End Function

    Public Function DeleteByTimeId(_timeId As String) As Boolean
        Try
            _macroEconomicFactorAcc.DeleteByTimeId(_timeId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorBiz", "DeleteByTimeId", ex.Message)
        End Try
        Return False
    End Function


    Public Function Delete(_timeId As String, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer) As Boolean
        Try
            _macroEconomicFactorAcc.Delete(_timeId, _stressYear, _stressMonth, _factorId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorBiz", "Delete", ex.Message)
        End Try
        Return False
    End Function


    Public Function GetTemplateByTime(_timeId As String) As List(Of MacroEconomicFactorEntity)
        Dim _result As List(Of MacroEconomicFactorEntity)
        _result = _macroEconomicFactorAcc.GetTemplateByTime(_timeId)
        Return _result
    End Function

    Public Function GetForecastReport(_timeId As String) As DataTable
        Dim _result As New DataTable
        _result = _macroEconomicFactorAcc.GetForecastReport(_timeId)
        Return _result
    End Function

End Class