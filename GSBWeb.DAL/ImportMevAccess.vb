Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing
Imports Arsoft.Utility

Public Class ImportMevAccess
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function Insert(_timeId As String, _scenarioId As Integer, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer, _factorValue As Decimal, _createBy As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_insert  '{0}',{1},{2},{3},{4},{5}", _timeId, _scenarioId, _stressYear, _stressMonth, _factorId, _factorValue)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function InsertExcel(_timeId As String, _scenarioId As Integer, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer, _factorValue As Decimal, _createBy As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_import_excel  '{0}',{1},{2},{3},{4},{5}", _timeId, _scenarioId, _stressYear, _stressMonth, _factorId, _factorValue)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function


    Public Function GetDataByTimeAndFactor(_timeId As String, _factorId As Integer) As List(Of ImportMevEntity)
        Dim listLgd As New List(Of ImportMevEntity)
        Try
            Dim _sql As String
            Dim _entity As ImportMevEntity
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_get  '{0}',{1}", _timeId, _factorId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New ImportMevEntity
                With _entity
                    Dim factorValue As Decimal = Convert.ToDecimal(_dbaccess.GetItem("FACTOR_VALUE"))
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .ScenarioId = _dbaccess.GetItem("SCENARIO_ID")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .StressYear = _dbaccess.GetItem("STRESS_YEAR")
                    .StressMonth = _dbaccess.GetItem("STRESS_MONTH")
                    .FactorId = _dbaccess.GetItem("FACTOR_ID")
                    .FactorValue = If(factorValue = Math.Floor(factorValue), factorValue.ToString("F2"), factorValue.ToString("G").TrimEnd("0"c).TrimEnd("."c))
                    .Year = _dbaccess.GetItem("YEAR")
                    .Month = _dbaccess.GetItem("MONTH")
                End With
                listLgd.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listLgd
    End Function

    Public Function FormatNumberWithAtLeastTwoDecimals(input As Decimal) As String
        Dim formatted As String = input.ToString("0.##")
        If formatted.Contains(".") AndAlso formatted.Split("."c)(1).Length < 2 Then
            formatted &= "0" ' Append a zero if there’s only one decimal place
        ElseIf Not formatted.Contains(".") Then
            formatted &= ".00" ' Ensure at least two decimal places if there's no decimal
        End If
        Return formatted
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of ImportMevEntity)
        Dim listLgd As New List(Of ImportMevEntity)
        Try
            Dim _sql As String
            Dim _entity As ImportMevEntity
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_get_template  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New ImportMevEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    .ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .StressYear = _dbaccess.GetItem("STRESS_YEAR")
                    .StressMonth = _dbaccess.GetItem("STRESS_MONTH")
                    .FactorName = _dbaccess.GetItem("FACTORID_NAME")
                    .FactorValue = _dbaccess.GetItem("FACTOR_VALUE")
                End With
                listLgd.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listLgd
    End Function

    Public Function Delete(_timeId As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_delete_timeid  '{0}'", _timeId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "Delete", ex.Message)
        End Try
        Return False
    End Function


    Public Function Delete(_timeId As String, _scenarioId As Integer, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_delete  '{0}',{1},{2},{3},{4}", _timeId, _scenarioId, _stressYear, _stressMonth, _factorId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "Delete", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetForecastReport(_timeId As String, _scenarioId As Integer) As DataTable
        Dim dt As New DataTable
        Try
            ' Connection string to your SQL Server database
            Dim connectionString As String = DBUtility.ReportConnectionString("ConnectionString_Report")
            ' Name of the stored procedure and parameters
            Dim storedProcedure As String = "sp_REF_STRESS_MEV_FORECAST_REPORT"
            ' Create a connection and command
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(storedProcedure, conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@TIMEID", _timeId)
                    cmd.Parameters.AddWithValue("@SCENARIO", _scenarioId)
                    ' Open the connection
                    conn.Open()
                    ' Execute the stored procedure and get the SqlDataReader
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ' Load the SqlDataReader into the DataTable
                        dt.Load(reader)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            UtilLogfile.writeToLog("ImportMevAccess", "GetForecastReport", ex.Message)
        End Try

        Return dt
    End Function

    Public Sub Dispose()
        If Not _dbaccess Is Nothing Then
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End If
    End Sub

End Class