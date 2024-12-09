Public Class MacroEconomicFactorEntity

#Region "Attributes"

    Private _timeId As String
    Private _scenarioId As String
    Private _scenarioName As String
    Private _stressYear As String
    Private _stressMonth As String
    Private _factorId As String
    Private _factorName As String
    Private _factorValue As String
    Private _year As String
    Private _month As String

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

    Public Property ScenarioId As String
        Get
            Return _scenarioId
        End Get
        Set(value As String)
            _scenarioId = value
        End Set
    End Property

    Public Property ScenarioName As String
        Get
            Return _scenarioName
        End Get
        Set(value As String)
            _scenarioName = value
        End Set
    End Property


    Public Property StressYear As String
        Get
            Return _stressYear
        End Get
        Set(value As String)
            _stressYear = value
        End Set
    End Property

    Public Property StressMonth As String
        Get
            Return _stressMonth
        End Get
        Set(value As String)
            _stressMonth = value
        End Set
    End Property

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

    Public Property FactorValue As String
        Get
            Return _factorValue
        End Get
        Set(value As String)
            _factorValue = value
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

    Public Property Month As String
        Get
            Return _month
        End Get
        Set(value As String)
            _month = value
        End Set
    End Property

#End Region

End Class
