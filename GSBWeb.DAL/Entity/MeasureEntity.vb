Public Class MeasureEntity

#Region "Attributes"

    Private _timeId As String
    Private _main_measure As String
    Private _sub_measure As String
    Private _account_number As String
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

    Public Property MainMeasure As String
        Get
            Return _main_measure
        End Get
        Set(value As String)
            _main_measure = value
        End Set
    End Property

    Public Property SubMeasure As String
        Get
            Return _sub_measure
        End Get
        Set(value As String)
            _sub_measure = value
        End Set
    End Property

    Public Property AccountNumber As String
        Get
            Return _account_number
        End Get
        Set(value As String)
            _account_number = value
        End Set
    End Property

#End Region

End Class
