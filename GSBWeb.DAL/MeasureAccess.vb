Imports Arsoft.Utility

Public Class MeasureAccess
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub


    Public Function InsertImportExcel(_timeId As String, _main_measure As String, _sub_measure As String, _account_number As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEASURE_import_excel  '{0}','{1}','{2}','{3}'", _timeId, _main_measure, _sub_measure, _account_number)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MeasureAccess", "InsertImportExcel", ex.Message)
        End Try
        Return False
    End Function

    Public Function DeleteByTimeId(_timeId As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEASURE_delete_timeid  '{0}'", _timeId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MeasureAccess", "DeleteByTimeId", ex.Message)
        End Try
        Return False
    End Function

    Public Function Insert(_timeId As String, _main_measure As String, _sub_measure As String, _account_number As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEASURE_insert  '{0}','{1}','{2}','{3}'", _timeId, _main_measure, _sub_measure, _account_number)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MeasureAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetDataByTimeId(_timeId As String) As List(Of MeasureEntity)
        Dim listEntity As New List(Of MeasureEntity)
        Try
            Dim _sql As String
            Dim _entity As MeasureEntity
            _sql = String.Format("EXEC sp_REF_STRESS_MEASURE_get  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New MeasureEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TimeId")
                    .AccountNumber = _dbaccess.GetItem("AccountNumber")
                    .MainMeasure = _dbaccess.GetItem("MainMeasure")
                    .SubMeasure = _dbaccess.GetItem("SubMeasure")
                End With
                listEntity.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("MeasureAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listEntity
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of MeasureEntity)
        Dim listEntity As New List(Of MeasureEntity)
        Try
            Dim _sql As String
            Dim _entity As MeasureEntity
            _sql = String.Format("EXEC sp_REF_STRESS_MEASURE_get_template  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New MeasureEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TimeId")
                    .AccountNumber = _dbaccess.GetItem("AccountNumber")
                    .MainMeasure = _dbaccess.GetItem("MainMeasure")
                    .SubMeasure = _dbaccess.GetItem("SubMeasure")
                End With
                listEntity.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("MeasureAccess", "GetDataByTimeId", ex.Message)
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