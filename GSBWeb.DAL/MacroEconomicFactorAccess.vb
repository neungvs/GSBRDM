Imports System.Data.SqlClient
Imports Arsoft.Utility

Public Class MacroEconomicFactorAccess
    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function InsertImportExcel(_timeId As String, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer, _factorValue As Decimal) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_HIS_import_excel  '{0}',{1},{2},{3},{4}", _timeId, _stressYear, _stressMonth, _factorId, _factorValue)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function Insert(_timeId As String, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer, _factorValue As Decimal) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_HIS_insert  '{0}',{1},{2},{3},{4}", _timeId, _stressYear, _stressMonth, _factorId, _factorValue)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorAccess", "Insert", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetDataByTimeId(_timeId As String, _factorId As Integer) As List(Of MacroEconomicFactorEntity)
        Dim listLgd As New List(Of MacroEconomicFactorEntity)
        Try
            Dim _sql As String
            Dim _entity As MacroEconomicFactorEntity
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_HIS_get  '{0}',{1}", _timeId, _factorId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New MacroEconomicFactorEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    '.ScenarioId = _dbaccess.GetItem("SCENARIO_ID")
                    .StressYear = _dbaccess.GetItem("STRESS_YEAR")
                    .StressMonth = _dbaccess.GetItem("STRESS_MONTH")
                    .FactorId = _dbaccess.GetItem("FACTORID")
                    .FactorValue = _dbaccess.GetItem("FACTOR_VALUE")
                    '.Year = _dbaccess.GetItem("YEAR")
                    '.Month = _dbaccess.GetItem("MONTH")
                End With
                listLgd.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listLgd
    End Function

    Public Function DeleteByTimeId(_timeId As String) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_HIS_delete_timeid  '{0}'", _timeId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorAccess", "Delete", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _stressYear As Integer, _stressMonth As Integer, _factorId As Integer) As Boolean
        Try
            Dim _sql As String
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_HIS_delete  '{0}',{1},{2},{3}", _timeId, _stressYear, _stressMonth, _factorId)
            _dbaccess.ExecuteNonQuery(_sql)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorAccess", "Delete", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetTemplateByTime(_timeId As String) As List(Of MacroEconomicFactorEntity)
        Dim listLgd As New List(Of MacroEconomicFactorEntity)
        Try
            Dim _sql As String
            Dim _entity As MacroEconomicFactorEntity
            _sql = String.Format("EXEC sp_REF_STRESS_MEV_HIS_get_template  '{0}'", _timeId)
            _dbaccess.ExecuteReader(_sql)

            Do While _dbaccess.Read
                _entity = New MacroEconomicFactorEntity
                With _entity
                    .TimeId = _dbaccess.GetItem("TIMEID")
                    '.ScenarioName = _dbaccess.GetItem("SCENARIO_NAME")
                    .StressYear = _dbaccess.GetItem("STRESS_YEAR")
                    .StressMonth = _dbaccess.GetItem("STRESS_MONTH")
                    .FactorName = _dbaccess.GetItem("FACTORID_NAME")
                    .FactorValue = _dbaccess.GetItem("FACTOR_VALUE")
                End With
                listLgd.Add(_entity)
            Loop
            _dbaccess.CloseReader()
        Catch ex As Exception
            UtilLogfile.writeToLog("MacroEconomicFactorAccess", "GetDataByTimeId", ex.Message)
        End Try

        Return listLgd
    End Function

    Public Function GetForecastReport(_timeId As String) As DataTable
        Dim dt As New DataTable
        Try
            ' Connection string to your SQL Server database
            Dim connectionString As String = DBUtility.ReportConnectionString("ConnectionString_Report")
            ' Name of the stored procedure and parameters
            Dim storedProcedure As String = "sp_REF_STRESS_MEV_HIS_FORECAST_REPORT"
            ' Create a connection and command
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(storedProcedure, conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@TIMEID", _timeId)
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
            UtilLogfile.writeToLog("MacroEconomicFactorAccess", "GetForecastReport", ex.Message)
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