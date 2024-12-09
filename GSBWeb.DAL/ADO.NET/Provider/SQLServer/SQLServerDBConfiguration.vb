Public Class SQLServerDBConfiguration

#Region "Attibutes"
    Private Shared _connectionstring As String
#End Region

    Public Shared Property ConnectionString() As String
        Get
            '"User Id=" & _uid & ";Password=" & _password & ";Host=" & _host & ";Server=" & _server & ";Service=" & _service & ";Database=" & _database
            '_connectionstring = ConfigurationManager.ConnectionStrings("ProductionConnection").ConnectionString.ToString
            Return _connectionstring
        End Get
        Set(ByVal Value As String)
            _connectionstring = Value
        End Set
    End Property

End Class
