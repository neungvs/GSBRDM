Public Class ScenarioEntity

#Region "Attributes"

    Private _scenarioId As String
    Private _scenarioName As String

#End Region

#Region "Property"
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

#End Region

End Class
