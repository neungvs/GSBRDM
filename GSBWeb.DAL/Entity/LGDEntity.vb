Public Class LGDEntity

#Region "Attributes"

    Private _timeId As String
    Private _year As String
    Private _scenario As String
    Private _stressLgdScalar As String
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

    Public Property Year As String
        Get
            Return _year
        End Get
        Set(value As String)
            _year = value
        End Set
    End Property

    Public Property Scenario As String
        Get
            Return _scenario
        End Get
        Set(value As String)
            _scenario = value
        End Set
    End Property

    Public Property StressLgdScalar As String
        Get
            Return _stressLgdScalar
        End Get
        Set(value As String)
            _stressLgdScalar = value
        End Set
    End Property

#End Region

End Class
