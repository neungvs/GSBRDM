Imports Arsoft.Utility

Public Class FactorNameAccess
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function GetDataByTimeId(_timeId As String) As List(Of FactorNameEntity)
        Dim listEntiry As New List(Of FactorNameEntity)
        Try
            Dim _sql As String
            Dim entity As FactorNameEntity
            _sql = String.Format("EXEC sp_REF_STRESS_FACTOR_get  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                entity = New FactorNameEntity
                With entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .FactorId = _dbaccess.GetItem("FACTORID")
                    .FactorName = _dbaccess.GetItem("FACTORID_NAME")
                    .FactorUnit = _dbaccess.GetItem("UNIT")
                    .FactorDesc = _dbaccess.GetItem("FACTORID_DESC")
                End With
                listEntiry.Add(entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameAccess", "GetDataByTimeId", ex.Message)
        End Try
        Return listEntiry
    End Function

    Public Function CreateNew(_timeId As String, _userId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_FACTOR_new_date  '{0}',{1}", _timeId, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameAccess", "CreateNew", ex.Message)
        End Try
        Return False
    End Function

    Public Function Update(_timeId As String, _factorId As Integer, _factorName As String, _factorDesc As String, _unit As String, _userId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_FACTOR_update  '{0}',{1},'{2}','{3}','{4}',{5}", _timeId, _factorId, _factorName, _factorDesc, _unit, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameAccess", "Update", ex.Message)
        End Try
        Return False
    End Function

    Public Function Add(_timeId As String, _factorName As String, _factorDesc As String, _unit As String, _userId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_FACTOR_insert  '{0}','{1}','{2}','{3}',{4}", _timeId, _factorName, _factorDesc, _unit, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameAccess", "Add", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _factorId As Integer, _userId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_FACTOR_delete  '{0}',{1},{2}", _timeId, _factorId, _userId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameAccess", "Delete", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetFactorDate() As List(Of TimeEntity)
        Dim listTime As New List(Of TimeEntity)
        Try
            Dim _sql As String
            Dim _time As TimeEntity

            _sql = String.Format("EXEC sp_REF_STRESS_FACTOR_get_date  ")
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

    Public Function GetFactorByTimeId(timeId As String) As List(Of FactorEntity)
        Dim retData As New List(Of FactorEntity)
        Try
            Dim _sql As String
            Dim entitty As FactorEntity
            _sql = String.Format("EXEC sp_REF_STRESS_FACTOR_get_factor_name  '{0}'", timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                entitty = New FactorEntity
                With entitty
                    .FactorId = _dbaccess.GetItem("FACTORID")
                    .FactorName = _dbaccess.GetItem("FACTORID_NAME")
                    .FactorDesc = _dbaccess.GetItem("FACTORID_DESC")
                    .FactorUnit = _dbaccess.GetItem("UNIT")
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