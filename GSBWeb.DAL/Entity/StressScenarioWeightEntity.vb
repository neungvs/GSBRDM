Public Class StressScenarioWeightEntity

#Region "Attributes"

    Private _TimeId As String
    Private _ScenarioId As String
    Private _ScenarioName As String
    Private _ScenarioDesc As String

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

    Public Property ScenarioId As String
        Get
            Return _ScenarioId
        End Get
        Set(value As String)
            _ScenarioId = value
        End Set
    End Property

    Public Property ScenarioName As String
        Get
            Return _ScenarioName
        End Get
        Set(value As String)
            _ScenarioName = value
        End Set
    End Property

    Public Property ScenarioDesc As String
        Get
            Return _ScenarioDesc
        End Get
        Set(value As String)
            _ScenarioDesc = value
        End Set
    End Property

#End Region

End Class
