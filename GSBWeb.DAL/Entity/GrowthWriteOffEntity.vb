Public Class GrowthWriteOffEntity

#Region "Attributes"

    Private _timeId As String
    Private _pd_segment As String
    Private _scenario_name As String
    Private _year As String
    Private _loan_growth_perc As String
    Private _write_off_perc As String
    Private _import_date As String

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

    Public Property PdSegment As String
        Get
            Return _pd_segment
        End Get
        Set(value As String)
            _pd_segment = value
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

    Public Property LoanGrowthPerc As String
        Get
            Return _loan_growth_perc
        End Get
        Set(value As String)
            _loan_growth_perc = value
        End Set
    End Property

    Public Property WriteOffPerc As String
        Get
            Return _write_off_perc
        End Get
        Set(value As String)
            _write_off_perc = value
        End Set
    End Property

    Public Property ImportDate As String
        Get
            Return _import_date
        End Get
        Set(value As String)
            _import_date = value
        End Set
    End Property

#End Region

End Class
