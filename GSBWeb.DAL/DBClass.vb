Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Configuration
Imports System.Data
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports GSBWeb.DAL


Public Class DBclass
    Private Com As SqlCommand
    Private Con As SqlConnection
    Private SQL As String
    Dim en As Object

    Public Sub New()
        Me.Con = New SqlConnection
        Me.Com = New SqlCommand
        Me.Con.ConnectionString = ConfigurationManager.ConnectionStrings.Item("ConnectionString_Staging").ConnectionString
    End Sub


    Public Sub New(ByRef conName As String)

        Me.Con = New SqlConnection
        Me.Com = New SqlCommand
        Me.Con.ConnectionString = ConfigurationManager.ConnectionStrings.Item(conName).ConnectionString
    End Sub


    'Public Sub ExecuteNonQuery(ByVal SQL As String)
    '    Try
    '        Me.Con.Open()
    '        Using conn As New SqlConnection(Me.Con.ConnectionString)
    '            Dim trans As SqlTransaction = conn.BeginTransaction
    '            Me.Com.Connection = Me.Con
    '            Me.Com.Transaction = trans
    '            Try
    '                Me.Com.CommandType = CommandType.Text
    '                Me.Com.CommandText = SQL
    '                Me.Com.ExecuteNonQuery()
    '                trans.Commit()
    '            Catch ex As Exception
    '                trans.Rollback()
    '            Finally
    '                Me.Con.Close()
    '                Me.Com.Dispose()
    '            End Try
    '        End Using
    '    Catch exception1 As SqlException
    '        Me.Con.Close()
    '        Me.Com.Dispose()
    '    Finally
    '        Me.Con.Close()
    '        Me.Com.Dispose()
    '    End Try
    'End Sub

    Public Sub ExecuteNonQuery(ByVal SQL As String)
        Try
            Me.Con.Open()
            Me.Com.Connection = Me.Con
            Me.Com.CommandType = CommandType.Text
            Me.Com.CommandText = SQL
            Me.Com.ExecuteNonQuery()
        Catch exception1 As SqlException
            Me.Con.Close()
            Me.Com.Dispose()
        End Try
        Me.Con.Close()
        Me.Com.Dispose()
    End Sub

    Public Function ExecuteReader(ByVal SQL As String) As DataTable
        Dim table As New DataTable
        Try
            Me.Con.Open()
            Me.Com.Connection = Me.Con
            Me.Com.CommandType = CommandType.Text
            Me.Com.CommandText = SQL
            table.Load(Me.Com.ExecuteReader)
        Catch exception1 As SqlException
            Me.Con.Close()
            Me.Com.Dispose()
            Return table
        Finally
            Me.Con.Close()
            Me.Com.Dispose()
        End Try

        Return table
    End Function

    Public Function ExecuteNonQueryScalar(ByVal SQL As String) As Object
        Dim id As New Object
        Try
            Me.Con.Open()
            Me.Com.Connection = Me.Con
            Me.Com.CommandType = CommandType.Text
            Me.Com.CommandText = SQL
            id = Me.Com.ExecuteScalar()
        Catch exception1 As SqlException
            Me.Con.Close()
            Me.Com.Dispose()
        End Try
        Me.Con.Close()
        Me.Com.Dispose()
        Return id
    End Function

End Class







