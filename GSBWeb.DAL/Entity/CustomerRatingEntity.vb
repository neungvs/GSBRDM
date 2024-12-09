Public Class CustomerRatingEntity

#Region "Attributes"

    Private _timeId As String
    Private _customer_nr As String
    Private _scenario_name As String
    Private _year As String
    Private _old_pd_segment As String
    Private _new_pd_segment As String
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

    Public Property CustomerNr As String
        Get
            Return _customer_nr
        End Get
        Set(value As String)
            _customer_nr = value
        End Set
    End Property

    Public Property ScenarioName As String
        Get
            Return _scenario_name
        End Get
        Set(value As String)
            _scenario_name = value
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

    Public Property OldPdSegment As String
        Get
            Return _old_pd_segment
        End Get
        Set(value As String)
            _old_pd_segment = value
        End Set
    End Property

    Public Property NewPdSegment As String
        Get
            Return _new_pd_segment
        End Get
        Set(value As String)
            _new_pd_segment = value
        End Set
    End Property

#End Region

End Class
