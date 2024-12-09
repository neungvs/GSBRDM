Public Class FactorNameEntity

#Region "Attributes"

    Private _TimeId As String
    Private _FactorId As String
    Private _FactorName As String
    Private _FactorUnit As String
    Private _FactorDesc As String

#End Region

#Region "Property"

    Public Property TimeId As String
        Get
            Return _TimeId
        End Get
        Set(value As String)
            _TimeId = value
        End Set
    End Property

    Public Property FactorId As String
        Get
            Return _FactorId
        End Get
        Set(value As String)
            _FactorId = value
        End Set
    End Property

    Public Property FactorName As String
        Get
            Return _FactorName
        End Get
        Set(value As String)
            _FactorName = value
        End Set
    End Property

    Public Property FactorUnit As String
        Get
            Return _FactorUnit
        End Get
        Set(value As String)
            _FactorUnit = value
        End Set
    End Property

    Public Property FactorDesc As String
        Get
            Return _FactorDesc
        End Get
        Set(value As String)
            _FactorDesc = value
        End Set
    End Property

#End Region

End Class