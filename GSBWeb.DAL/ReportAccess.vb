Imports System.Data.SqlClient
Imports Arsoft.Utility

Public Class ReportAccess

    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function GetReport() As DataTable
        Dim dt As New DataTable
        Try
            ' Connection string to your SQL Server database
            Dim connectionString As String = DBUtility.ReportConnectionString("ConnectionString_Report")
            ' Name of the stored procedure and parameters
            Dim storedProcedure As String = "sp_Report_Test"
            ' Create a connection and command
            Using conn As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(storedProcedure, conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    'cmd.Parameters.AddWithValue("@TIMEID", _timeId)
                    'cmd.Parameters.AddWithValue("@SCENARIO", _scenarioId)
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