Public Class ScenarioWeightEntity

#Region "Attributes"

    Private _timeId As String
    Private _scenarioId As String
    Private _scenarioName As String
    Private _weight As String

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

    Public Property Weight As String
        Get
            Return _weight
        End Get
        Set(value As String)
            _weight = value
        End Set
    End Property

#End Region

End Class