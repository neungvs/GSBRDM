Imports Arsoft.Utility

Public Class CustomerRatingAccess
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub


    Public Function InsertImportExcel(_timeId As String, _customerNr As String, _year As String, _scenario As String, _old_pd_segment As String, _new_pd_segment As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_CUSTOMER_RATING_import_excel  '{0}','{1}',{2},'{3}','{4}','{5}' ", _timeId, _customerNr, _year, _scenario, _old_pd_segment, _new_pd_segment)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("CustomerRatingAccess", "InsertImportExcel", ex.Message)
        End Try
        Return False
    End Function

    Public Function DeleteByTimeId(_timeId As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_CUSTOMER_RATING_delete_timeid  '{0}'", _timeId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("CustomerRatingAccess", "DeleteByTimeId", ex.Message)
        End Try
        Return False
    End Function

    Public Function Insert(_timeId As String, _customerNr As String, _year As String, _scenario As String, _old_pd_segment As String, _new_pd_segment As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_CUSTOMER_RATING_insert  '{0}','{1}',{2},'{3}','{4}','{5}' ", _timeId, _customerNr, _year, _scenario, _old_pd_segment, _new_pd_segment)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("CustomerRatingAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetDataByTimeId(_timeId As String) As List(Of CustomerRatingEntity)
        Dim listEntity As New List(Of CustomerRatingEntity)
        Try
            Dim _sql As String
            Dim entity As CustomerRatingEntity

            _sql = String.Format("EXEC sp_REF_STRESS_CUSTOMER_RATING_get  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                entity = New CustomerRatingEntity
                With entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .CustomerNr = _dbaccess.GetItem("CUSTOMER_NR")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .Year = _dbaccess.GetItem("YEAR")
                    .OldPdSegment = _dbaccess.GetItem("OLD_PD_SEGMENT")
                    .NewPdSegment = _dbaccess.GetItem("NEW_PD_SEGMENT")
                End With
                listEntity.Add(entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CustomerRatingAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listEntity
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of CustomerRatingEntity)
        Dim listEntity As New List(Of CustomerRatingEntity)
        Try
            Dim _sql As String
            Dim _entity As CustomerRatingEntity
            _sql = String.Format("EXEC sp_REF_STRESS_CUSTOMER_RATING_get_template  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New CustomerRatingEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .CustomerNr = _dbaccess.GetItem("CUSTOMER_NR")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .Year = _dbaccess.GetItem("STRESS_YEAR")
                    .OldPdSegment = _dbaccess.GetItem("OLD_PD_SEGMENT")
                    .NewPdSegment = _dbaccess.GetItem("NEW_PD_SEGMENT")
                End With
                listEntity.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("CustomerRatingAccess", "GetTemplateByTime", ex.Message)
        End Try

        Return listEntity
    End Function

    Public Sub Dispose()
        If Not _dbaccess Is Nothing Then
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End If
    End Sub

End Class