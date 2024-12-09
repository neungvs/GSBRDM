Public Class TimeEntity

#Region "Attributes"

    Private _timeId As String
    Private _timeName As String

#End Region

#Region "Property"

    Public Property TimeId As String
        Get
            Return _timeId
        End Get
        Set(value As String)
            _timeId = value
        End Set
    End Property

    Public Property TimeName As String
        Get
            Return _timeName
        End Get
        Set(value As String)
            _timeName = value
        End Set
    End Property

#End Region

End Class
