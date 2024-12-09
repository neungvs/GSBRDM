Public Class PrivilegeEntity
#Region "Attributes"
    Private _moduleid As String
    Private _groupid As String
    Private _userid As String
    Private _groupcode As String
#End Region

#Region "Property"

    Public Property ModuleID() As String
        Get
            Return _moduleid
        End Get
        Set(ByVal value As String)
            _moduleid = value
        End Set
    End Property

    Public Property GroupID() As String
        Get
            Return _groupid
        End Get
        Set(ByVal value As String)
            _groupid = value
        End Set
    End Property

    Public Property UserID() As String
        Get
            Return _userid
        End Get
        Set(ByVal value As String)
            _userid = value
        End Set
    End Property

    Public Property GroupCode() As String
        Get
            Return _groupcode
        End Get
        Set(value As String)
            _groupcode = value
        End Set
    End Property
#End Region

End Class
