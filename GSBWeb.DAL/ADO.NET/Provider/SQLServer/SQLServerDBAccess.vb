Option Explicit On 

Imports System.Data.SqlClient

Public Class SQLServerDBAccess

#Region "Attibutes"
    Dim conn As SqlConnection
    Dim comm As SqlCommand
    Dim tran As SqlTransaction
    Dim reader As SqlDataReader
    Dim setprepare As Boolean = False
#End Region

#Region "Method"

    Public Sub New()
        Try
            conn = New SqlConnection(SQLServerDBConfiguration.ConnectionString)
            conn.Open()
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Public Function Open() As Boolean
        Dim result As Boolean = False
        If conn.State = ConnectionState.Open Then
            result = True
        End If
        Return result
    End Function

    Public Sub OpenConnection()
        If Not conn Is Nothing Then
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Else
            conn = New SqlConnection(SQLServerDBConfiguration.ConnectionString)
            conn.Open()
        End If
    End Sub

    Public Sub CloseConnection()
        If Not conn Is Nothing Then
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
        End If
    End Sub

    Public Sub Dispose()
        Try
            Me.RollbackTransaction()
            Me.CloseReader()
            Me.CloseConnection()
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            reader = Nothing
            tran = Nothing
            comm = Nothing
            conn = Nothing
        End Try
        GC.SuppressFinalize(Me)
    End Sub

#Region "ExecuteNonQuery"

    Public Function ExecuteNonQuery(ByVal sql As String) As Integer
        Return Me.ExecuteNonQuery(sql, Nothing)
    End Function

    Public Function ExecuteNonQuery(ByVal sql As String, ByVal param() As SQLServerDBParameter) As Integer
        Dim result As Integer = 0
        Try
            OpenConnection()
            comm = New SqlCommand(sql, conn, tran)
            comm.CommandTimeout = 0
            ExtractParameter(param)
            result = comm.ExecuteNonQuery()
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return result
    End Function

#End Region

#Region "ExecuteScalar"

    Public Function ExecuteScalar(ByVal sql As String) As Object
        Return Me.ExecuteScalar(sql, Nothing)
    End Function

    Public Function ExecuteScalar(ByVal sql As String, ByVal param As SQLServerDBParameter()) As Object
        Dim result As Object = Nothing
        Try
            OpenConnection()
            comm = New SqlCommand(sql, conn, tran)
            comm.CommandTimeout = 0
            ExtractParameter(param)
            result = comm.ExecuteScalar()
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return result
    End Function

    Private Function testParameter(ByVal p() As SQLServerDBParameter) As SqlParameter()
        Dim param(p.Length - 1) As SqlParameter
        Dim i As Integer
        For i = 0 To param.Length - 1
            param(i) = New SqlParameter(p(i).Name, p(i).Values)
        Next
        Return param
    End Function

#End Region

#Region "ExecuteReader"

    Public Sub ExecuteReader(ByVal sql As String)
        Me.ExecuteReader(sql, Nothing)
    End Sub

    Public Sub ExecuteReader(ByVal sql As String, ByVal param() As SQLServerDBParameter)
        Try
            OpenConnection()
            comm = New SqlCommand(sql, conn)
            comm.CommandTimeout = 0
            Me.ExtractParameter(param)
            reader = comm.ExecuteReader
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Public Sub CommandExecuteReader(ByVal sql As String, ByVal commandstype As CommandType, Optional ByVal param() As SQLServerDBParameter = Nothing)
        Try
            OpenConnection()
            comm = New SqlCommand(sql, conn)
            comm.CommandTimeout = 0
            comm.CommandType = commandstype
            Me.ExtractParameter(param)
            reader = comm.ExecuteReader
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Public Function Read() As Boolean
        Dim result As Boolean = False
        If Not reader Is Nothing And Not reader.IsClosed Then
            result = reader.Read()
        End If
        Return result
    End Function

    Public Function GetItem(ByVal index As Integer) As Object
        If IsDBNull(reader(index)) Then
            Return Nothing
        Else
            Return reader(index)
        End If
    End Function

    Public Function GetItem(ByVal filename As String) As Object
        If IsDBNull(reader(filename)) Then
            Return Nothing
        Else
            Return reader(filename)
        End If
    End Function

    Public Sub CloseReader()
        If Not reader Is Nothing Then
            If Not reader.IsClosed Then
                reader.Close()
            End If
        End If
        reader = Nothing
        If Not comm Is Nothing Then comm.Dispose()
        comm = Nothing
    End Sub

    Public Function FieldCount() As Integer
        Dim fcount As Integer = 0
        If Not reader Is Nothing Then
            If Not reader.IsClosed Then
                fcount = reader.FieldCount
            End If
        End If
        Return fcount
    End Function



#End Region

#Region "ExecuteAdapter"

    Public Function ExecuteAdapter(ByVal sql As String) As DataTable
        Return Me.ExecuteAdapter(sql, Nothing)
    End Function

    Public Function ExecuteAdapter(ByVal sql As String, ByVal param() As SQLServerDBParameter) As DataTable
        Dim table As DataTable = New DataTable
        Try
            OpenConnection()
            comm = New SqlCommand(sql, conn)
            comm.CommandTimeout = 0
            Me.ExtractParameter(param)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(comm)
            adapter.Fill(table)
            adapter.Dispose()
            adapter = Nothing
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return table
    End Function

    Public Function ExecuteAdapter(ByVal sql As String, ByVal startRecord As Integer, ByVal maxRecords As Integer) As DataSet
        Return Me.ExecuteAdapter(sql, Nothing, startRecord, maxRecords)
    End Function

    Public Function ExecuteAdapter(ByVal sql As String, ByVal param() As SQLServerDBParameter, ByVal startRecord As Integer, ByVal maxRecords As Integer) As DataSet
        Dim table As DataSet = New DataSet
        Try
            OpenConnection()
            comm = New SqlCommand(sql, conn)
            comm.CommandTimeout = 0
            Me.ExtractParameter(param)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(comm)
            adapter.Fill(table, startRecord, maxRecords, "resulttable")
            adapter.Dispose()
            adapter = Nothing
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return table
    End Function

#End Region

#Region "Transaction"

    Public Sub BeginTransaction()
        If tran Is Nothing Then
            tran = conn.BeginTransaction
        End If
    End Sub

    Public Sub RollbackTransaction()
        If Not tran Is Nothing Then
            tran.Rollback()
            tran = Nothing
        End If
    End Sub

    Public Sub CommitTransaction()
        If Not tran Is Nothing Then
            tran.Commit()
            tran = Nothing
        End If
    End Sub

#End Region

#Region "Prepare"


    Public Sub BeginPrepare(ByVal sql As String)
        Try
            OpenConnection()
            comm = New SqlCommand(sql, conn, tran) 'use Begin Transaction
            'comm = New SqlCommand(sql, conn)
            comm.CommandTimeout = 0
            setprepare = False
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
    End Sub

    Public Function ExecutePrepare(ByVal param() As SQLServerDBParameter) As Integer
        Dim result As Integer = 0
        Try
            ExtractParameterPrepare(param)
            If Not setprepare Then
                comm.Prepare()
                setprepare = True
            End If
            result = comm.ExecuteNonQuery()
        Catch ex As SqlException
            Throw New Exception(ex.Message, ex.InnerException)
        End Try
        Return result
    End Function

    Private Sub ExtractParameterPrepare(ByVal param() As SQLServerDBParameter)
        If Not param Is Nothing Then
            Dim p As SQLServerDBParameter
            Dim parameter As SqlParameter
            If Not setprepare Then
                For Each p In param
                    If Not p Is Nothing Then
                        parameter = New SqlParameter(p.Name, p.DbType)
                        With parameter
                            '.IsNullable = True
                            '.Direction = ParameterDirection.Output
                            '.Value = p.Values
                            .Size = p.Size
                            .Precision = p.Precision
                            .Scale = p.Scale
                            'Select Case p.DbType
                            '    Case SqlDbType.Decimal
                            '        .Precision = 18
                            '        .Scale = 4
                            'End Select
                        End With
                        comm.Parameters.Add(parameter)
                    End If
                Next
            End If

            Dim i As Integer = 0
            For Each _param In comm.Parameters
                CType(_param, SqlParameter).Value = param(i).Values
                i = i + 1
            Next
            p = Nothing
        End If
    End Sub

    Private Sub SqlCommandPrepareEx(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = New SqlCommand("", connection)

            ' Create and prepare an SQL statement.
            command.CommandText = _
               "INSERT INTO Region (RegionID, RegionDescription) " & _
               "VALUES (@id, @desc)"
            Dim idParam As SqlParameter = _
                New SqlParameter("@id", SqlDbType.Int, 0)
            Dim descParam As SqlParameter = _
                New SqlParameter("@desc", SqlDbType.Decimal, 0)
            'New SqlParameter("@desc", SqlDbType.Text, 100)
            idParam.Value = 20
            descParam.Value = "First Region"
            command.Parameters.Add(idParam)
            command.Parameters.Add(descParam)

            ' Call Prepare after setting the Commandtext and Parameters.
            command.Prepare()
            command.ExecuteNonQuery()

            ' Change parameter values and call ExecuteNonQuery.
            command.Parameters(0).Value = 21
            command.Parameters(1).Value = "Second Region"
            command.ExecuteNonQuery()
        End Using
    End Sub


#End Region

#Region "Internal Method"

    Private Sub ExtractParameter(ByVal param() As SQLServerDBParameter)
        If Not param Is Nothing Then
            Dim p As SQLServerDBParameter
            For Each p In param
                If Not p Is Nothing Then
                    comm.Parameters.Add(New SqlParameter(p.Name, p.Values))
                End If
            Next
            p = Nothing
        End If
    End Sub

#End Region

#End Region

End Class
