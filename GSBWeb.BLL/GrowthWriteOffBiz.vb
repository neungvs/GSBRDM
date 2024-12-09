Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class GrowthWriteOffBiz
    Dim _growthWriteOffAcc As New GrowthWriteOffAccess

    Public Function GetByTime(_timeId As String) As List(Of GrowthWriteOffEntity)
        Dim _result As List(Of GrowthWriteOffEntity)
        _result = _growthWriteOffAcc.GetDataByTimeId(_timeId)
        Return _result
    End Function

    Public Function Save(listEntity As List(Of GrowthWriteOffEntity)) As Boolean
        Try
            For Each entity As GrowthWriteOffEntity In listEntity
                Dim Time As String = entity.TimeId
                Dim PdSegment As String = entity.PdSegment
                Dim Scenario As String = entity.ScenarioName
                Dim Year As Integer = entity.Year
                Dim LoanGrowthPerc As Decimal = Convert.ToDecimal(entity.LoanGrowthPerc)
                Dim WriteOffPerc As Decimal = Convert.ToDecimal(entity.WriteOffPerc)
                _growthWriteOffAcc.Insert(Time, PdSegment, Scenario, Year, LoanGrowthPerc, WriteOffPerc)
            Next
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("GrowthWriteOffBiz", "Save", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of GrowthWriteOffEntity)
        Dim _result As List(Of GrowthWriteOffEntity)
        _result = _growthWriteOffAcc.GetTemplateByTime(_timeId)
        Return _result
    End Function
End Class