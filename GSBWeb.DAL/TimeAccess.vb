Imports Arsoft.Utility

Public Class TimeAccess

    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function GetDate() As List(Of TimeEntity)
        Dim listTime As New List(Of TimeEntity)
        Try
            Dim _sql As String
            Dim _time As TimeEntity

            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_get_date")
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _time = New TimeEntity
                With _time
                    .TimeId = _dbaccess.GetItem("Date")
                    .TimeName = _dbaccess.GetItem("ShowDate")
                End With
                listTime.Add(_time)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("LGDAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listTime
    End Function

    Public Function GetScenarioByTimeId(timeId As String) As List(Of ScenarioEntity)
        Dim retData As New List(Of ScenarioEntity)
        Try
            Dim _sql As String
            Dim entitty As ScenarioEntity

            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_get_scenario_name  '{0}'", timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                entitty = New ScenarioEntity
                With entitty
                    .ScenarioId = _dbaccess.GetItem("SCENARIO")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                End With
                retData.Add(entitty)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("LGDAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return retData
    End Function

    Public Sub Dispose()
        If Not _dbaccess Is Nothing Then
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End If
    End Sub

End Class