Option Explicit On

Imports System.Data.SqlClient

Public Class SQLServerDBParameter
    Dim _name As String
    Dim _value As Object
    Dim _type As SqlDbType
    Dim _size As Integer
    Dim _precision As Integer = 0
    Dim _scale As Integer = 0


    Public Sub New()
    End Sub

    Public Sub New(ByVal name As String, ByVal value As Object)
        _name = name
        _value = value
    End Sub

    Public Sub New(ByVal name As String, type As SqlDbType, size As Integer, Optional precision As Integer = 0, Optional scale As Integer = 0)
        _name = name
        _type = type
        _size = size
        _precision = precision
        _scale = scale
    End Sub

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal Value As String)
            _name = Value
        End Set
    End Property

    Public Property Values() As Object
        Get
            Return _value
        End Get
        Set(ByVal Value As Object)
            _value = Value
        End Set
    End Property

    Public Property DbType As SqlDbType
        Get
            Return _type
        End Get
        Set(value As SqlDbType)
            _type = value
        End Set
    End Property

    Public Property Size As Integer
        Get
            Return _size
        End Get
        Set(value As Integer)
            _size = value
        End Set
    End Property

    Public Property Precision As Integer
        Get
            Return _precision
        End Get
        Set(value As Integer)
            _precision = value
        End Set
    End Property

    Public Property Scale As Integer
        Get
            Return _scale
        End Get
        Set(value As Integer)
            _scale = value
        End Set
    End Property

End Class
