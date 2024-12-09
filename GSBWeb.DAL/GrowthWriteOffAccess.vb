Imports Arsoft.Utility

Public Class GrowthWriteOffAccess
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function Insert(_timeId As String, _pd_segment As String, _scenario As String, _year As Integer, _loan_growth_perc As Decimal, _write_off_perc As Decimal) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_GROWTH_WRITE_OFF_insert  '{0}','{1}','{2}',{3},{4},{5} ", _timeId, _pd_segment, _scenario, _year, _loan_growth_perc, _write_off_perc)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("GrowthWriteOffAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetDataByTimeId(_timeId As String) As List(Of GrowthWriteOffEntity)
        Dim listEntity As New List(Of GrowthWriteOffEntity)
        Try
            Dim _sql As String
            Dim entity As GrowthWriteOffEntity

            _sql = String.Format("EXEC sp_REF_STRESS_GROWTH_WRITE_OFF_get  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                entity = New GrowthWriteOffEntity
                With entity
                    Dim loan_growth_perc As Decimal = Convert.ToDecimal(_dbaccess.GetItem("LOAN_GROWTH_PERC"))
                    Dim write_off_perc As Decimal = Convert.ToDecimal(_dbaccess.GetItem("WRITE_OFF_PERC"))
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .PdSegment = _dbaccess.GetItem("PD_SEGMENT")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .Year = _dbaccess.GetItem("YEAR")
                    .LoanGrowthPerc = BindingDecimalFormat(loan_growth_perc)
                    .WriteOffPerc = BindingDecimalFormat(write_off_perc)
                End With
                listEntity.Add(entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("GrowthWriteOffAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listEntity
    End Function

    Private Function BindingDecimalFormat(value As Decimal) As String
        Return If(value = Math.Floor(value), value.ToString("F2"), value.ToString("G").TrimEnd("0"c).TrimEnd("."c))
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of GrowthWriteOffEntity)
        Dim listEntity As New List(Of GrowthWriteOffEntity)
        Try
            Dim _sql As String
            Dim _entity As GrowthWriteOffEntity
            _sql = String.Format("EXEC sp_REF_STRESS_GROWTH_WRITE_OFF_get_template  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New GrowthWriteOffEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .PdSegment = _dbaccess.GetItem("PD_SEGMENT")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .Year = _dbaccess.GetItem("STRESS_YEAR")
                    .LoanGrowthPerc = _dbaccess.GetItem("LOAN_GROWTH_PERC")
                    .WriteOffPerc = _dbaccess.GetItem("WRITE_OFF_PERC")
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