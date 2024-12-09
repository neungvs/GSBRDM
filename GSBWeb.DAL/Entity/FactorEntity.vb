Public Class FactorEntity

#Region "Attributes"
    Private _factorId As String
    Private _factorName As String
    Private _factorDesc As String
    Private _factorUnit As String

#End Region

#Region "Property"
    Public Property FactorId As String
        Get
            Return _factorId
        End Get
        Set(value As String)
            _factorId = value
        End Set
    End Property
    Public Property FactorName As String
        Get
            Return _factorName
        End Get
        Set(value As String)
            _factorName = value
        End Set
    End Property

    Public Property FactorDesc As String
        Get
            Return _factorDesc
        End Get
        Set(value As String)
            _factorDesc = value
        End Set
    End Property

    Public Property FactorUnit As String
        Get
            Return _factorUnit
        End Get
        Set(value As String)
            _factorUnit = value
        End Set
    End Property


#End Region

End Class
