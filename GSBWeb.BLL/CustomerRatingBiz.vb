Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class CustomerRatingBiz
    Dim _customerRatingAcc As New CustomerRatingAccess

    Public Function GetByTime(_timeId As String) As List(Of CustomerRatingEntity)
        Try
            Dim _result As List(Of CustomerRatingEntity)
            _result = _customerRatingAcc.GetDataByTimeId(_timeId)
            Return _result
        Catch ex As Exception
            UtilLogfile.writeToLog("CustomerRatingBiz", "GetByTime", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function Save(listData As List(Of CustomerRatingEntity)) As Boolean
        Try
            For Each ds As CustomerRatingEntity In listData
                Dim Time As Integer = ds.TimeId
                Dim Year As String = ds.Year
                Dim CustomerNr As String = ds.CustomerNr
                Dim ScenarioName As String = ds.ScenarioName
                Dim OldPdSegment As String = ds.OldPdSegment
                Dim NewPdSegment As String = ds.NewPdSegment
                _customerRatingAcc.Insert(Time, CustomerNr, Year, ScenarioName, OldPdSegment, NewPdSegment)
            Next
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("CustomerRatingBiz", "Save", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of CustomerRatingEntity)
        Dim _result As List(Of CustomerRatingEntity)
        _result = _customerRatingAcc.GetTemplateByTime(_timeId)
        Return _result
    End Function

End Class