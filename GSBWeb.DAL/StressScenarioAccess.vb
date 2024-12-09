﻿Imports Arsoft.Utility

Public Class StressScenarioAccess

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

    Public Function GetDataByTimeId(_timeId As String) As List(Of StressScenarioEntity)
        Dim listEntiry As New List(Of StressScenarioEntity)
        Try
            Dim _sql As String
            Dim entity As StressScenarioEntity
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_get  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                entity = New StressScenarioEntity
                With entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .ScenarioId = _dbaccess.GetItem("SCENARIO")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .ScenarioDesc = _dbaccess.GetItem("SCENARIO_DESC")
                End With
                listEntiry.Add(entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "GetDataByTimeId", ex.Message)
        End Try
        Return listEntiry
    End Function

    Public Function CreateNew(_timeId As String, _userId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_new_date  '{0}',{1}", _timeId, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function Update(_timeId As String, _scenarioId As Integer, _scenarioName As String, _scenarioDes As String, _userId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_update  '{0}',{1},'{2}','{3}',{4}", _timeId, _scenarioId, _scenarioName, _scenarioDes, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function Add(_timeId As String, _senarioName As String, _scenarioDes As String, _userId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_insert  '{0}','{1}','{2}',{3}", _timeId, _senarioName, _scenarioDes, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _scenarioId As Integer, _userId As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_SCENARIO_delete  '{0}',{1},{2}", _timeId, _scenarioId, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("StressScenarioAccess", "Insert", ex.Message)
        End Try
        Return False
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