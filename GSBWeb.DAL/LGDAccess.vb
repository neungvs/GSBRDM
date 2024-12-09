Imports Arsoft.Utility

Public Class LGDAccess
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function Insert(_timeId As String, _year As Integer, _scenario As String, _stressLgdScalar As Decimal) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_Ref_STRESS_LGD_SCALAR_insert  '{0}',{1},'{2}',{3}", _timeId, _year, _scenario, _stressLgdScalar)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("LGDAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetDataByTimeId(_timeId As String) As List(Of LGDEntity)
        Dim listLgd As New List(Of LGDEntity)
        Try
            Dim _sql As String
            Dim _lgd As LGDEntity
            _sql = String.Format("EXEC sp_Ref_STRESS_LGD_SCALAR_get  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _lgd = New LGDEntity
                With _lgd
                    .TimeId = _dbaccess.GetItem("TimeId")
                    .Year = _dbaccess.GetItem("Year")
                    .Scenario = _dbaccess.GetItem("SCENARIO_NAME")
                    .StressLgdScalar = _dbaccess.GetItem("Stress_Lgd_Scalar")
                End With
                listLgd.Add(_lgd)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("LGDAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listLgd
    End Function


    Public Function GetTemplateByTime(_timeId As String) As List(Of LGDEntity)
        Dim listEntity As New List(Of LGDEntity)
        Try
            Dim _sql As String
            Dim _entity As LGDEntity
            _sql = String.Format("EXEC sp_Ref_STRESS_LGD_SCALAR_get_template  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New LGDEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .Year = _dbaccess.GetItem("STRESS_YEAR")
                    .Scenario = _dbaccess.GetItem("SCENARIO_NAME")
                    .StressLgdScalar = _dbaccess.GetItem("STRESS_LGD_SCALAR")
                End With
                listEntity.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "GetDataByTimeId", ex.Message)
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