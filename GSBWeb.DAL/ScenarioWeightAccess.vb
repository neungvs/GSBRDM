Imports Arsoft.Utility

Public Class ScenarioWeightAccess

    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function GetDataByTimeId(_timeId As String) As List(Of ScenarioWeightEntity)
        Dim listEntiry As New List(Of ScenarioWeightEntity)
        Try
            Dim _sql As String
            Dim entity As ScenarioWeightEntity
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_WEIGHT_get  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                entity = New ScenarioWeightEntity
                With entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .ScenarioId = _dbaccess.GetItem("SCENARIO")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .Weight = BindingDecimalFormat(_dbaccess.GetItem("WEIGHT"))
                End With
                listEntiry.Add(entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "GetDataByTimeId", ex.Message)
        End Try
        Return listEntiry
    End Function

    Private Function BindingDecimalFormat(value As Decimal) As String
        Return If(value = Math.Floor(value), value.ToString("F2"), value.ToString("G").TrimEnd("0"c).TrimEnd("."c))
    End Function

    Public Function Update(_timeId As String, _scenarioId As Integer, _weight As Decimal) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_WEIGHT_update  '{0}',{1},{2}", _timeId, _scenarioId, _weight)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function Add(_timeId As String, _senarioId As Integer, _weight As Decimal) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_WEIGHT_insert  '{0}',{1},{2}", _timeId, _senarioId, _weight)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _scenarioId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_WEIGHT_delete  '{0}',{1}", _timeId, _scenarioId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    'Public Function GetTime() As List(Of TimeEntity)
    '    Dim listTime As New List(Of TimeEntity)
    '    Try
    '        Dim _sql As String
    '        Dim _time As TimeEntity

    '        _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_WEIGHT_NAME_get_date")
    '        _dbaccess.ExecuteReader(_sql)

    '        Do While _dbaccess.Read
    '            _time = New TimeEntity
    '            With _time
    '                .TimeId = _dbaccess.GetItem("Date")
    '                .TimeName = _dbaccess.GetItem("ShowDate")
    '            End With
    '            listTime.Add(_time)
    '        Loop
    '        _dbaccess.CloseReader()
    '    Catch ex As Exception
    '        UtilLogfile.writeToLog("LGDAccess", "GetDataByTimeId", ex.Message)
    '    End Try

    '    Return listTime
    'End Function

    Public Sub Dispose()
        If Not _dbaccess Is Nothing Then
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End If
    End Sub

End Class