Public Class DataSetMarketEntity

    Private _id As String
    Private _timeid As String
    Private _reportName As String

    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Public Property TimeID() As String
        Get
            Return _timeid
        End Get
        Set(ByVal value As String)
            _timeid = value
        End Set
    End Property

    Public Property ReportName() As String
        Get
            Return _reportName
        End Get
        Set(ByVal value As String)
            _reportName = value
        End Set
    End Property

End Class
