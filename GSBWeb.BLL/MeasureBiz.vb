Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class MeasureBiz
    Dim _measureAcc As New MeasureAccess

    Public Function GetByTime(_timeId As String) As List(Of MeasureEntity)
        Dim _result As List(Of MeasureEntity)
        _result = _measureAcc.GetDataByTimeId(_timeId)
        Return _result
    End Function

    Public Function Save(listData As List(Of MeasureEntity)) As Boolean
        Try
            For Each entity As MeasureEntity In listData
                Dim Time As Integer = entity.TimeId
                Dim MainMeasure As String = entity.MainMeasure
                Dim SubMeasure As String = entity.SubMeasure
                Dim AccountNumber As String = entity.AccountNumber
                _measureAcc.Insert(Time, MainMeasure, SubMeasure, AccountNumber)
            Next
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MeasureBiz", "Save", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of MeasureEntity)
        Dim _result As List(Of MeasureEntity)
        _result = _measureAcc.GetTemplateByTime(_timeId)
        Return _result
    End Function

End Class