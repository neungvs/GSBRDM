Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class LGDBiz
    Dim _lgdAcc As New LGDAccess

    Public Function GetByTime(_timeId As String) As List(Of LGDEntity)
        Dim _result As List(Of LGDEntity)
        _result = _lgdAcc.GetDataByTimeId(_timeId)
        Return _result
    End Function

    Public Function Save(listData As List(Of LGDEntity)) As Boolean
        Try
            For Each entity As LGDEntity In listData
                Dim Time As Integer = entity.TimeId
                Dim Year As String = entity.Year
                Dim Scenario As String = entity.Scenario
                Dim StressLgdScalar As String = entity.StressLgdScalar
                _lgdAcc.Insert(Time, Year, Scenario, StressLgdScalar)
            Next
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("LGDAccess", "Save", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of LGDEntity)
        Dim _result As List(Of LGDEntity)
        _result = _lgdAcc.GetTemplateByTime(_timeId)
        Return _result
    End Function

End Class