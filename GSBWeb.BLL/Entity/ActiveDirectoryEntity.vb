Public Class ActiveDirectoryEntity

#Region "Atributes"
    Private _employeeid As String
    Private _nameen As String
    Private _surnameen As String
    Private _namelo As String
    Private _surnamelo As String
    Private _title As String
    Private _company As String
    Private _pager As String
    Private _postalcode As String
    Private _organizationunit As String
    Private _memberof As String
    Private _deptcode As String
    Private _msg As String
    Private _displayname As String

#End Region

#Region "Methods"

    Public Property EmployeeID As String
        Get
            Return _employeeid
        End Get
        Set(value As String)
            _employeeid = value
        End Set
    End Property

    Public Property NameEn As String
        Get
            Return _nameen
        End Get
        Set(value As String)
            _nameen = value
        End Set
    End Property

    Public Property SurnameEn As String
        Get
            Return _surnameen
        End Get
        Set(value As String)
            _surnameen = value
        End Set
    End Property

    Public Property NameLo As String
        Get
            Return _namelo
        End Get
        Set(value As String)
            _namelo = value
        End Set
    End Property

    Public Property SurnameLo As String
        Get
            Return _surnamelo
        End Get
        Set(value As String)
            _surnamelo = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return _title
        End Get
        Set(value As String)
            _title = value
        End Set
    End Property

    Public Property Company As String
        Get
            Return _company
        End Get
        Set(value As String)
            _company = value
        End Set
    End Property

    Public Property Pager As String
        Get
            Return _pager
        End Get
        Set(value As String)
            _pager = value
        End Set
    End Property

    Public Property PostalCode As String
        Get
            Return _postalcode
        End Get
        Set(value As String)
            _postalcode = value
        End Set
    End Property

    Public Property OrganizationUnit As String
        Get
            Return _organizationunit
        End Get
        Set(value As String)
            _organizationunit = value
        End Set
    End Property

    Public Property MemberOf As String
        Get
            Return _memberof
        End Get
        Set(value As String)
            _memberof = value
        End Set
    End Property

    Public Property DeptCode As String
        Get
            Return _deptcode
        End Get
        Set(value As String)
            _deptcode = value
        End Set
    End Property


    Public Property Msg As String
        Get
            Return _msg
        End Get
        Set(value As String)
            _msg = value
        End Set
    End Property

    Public Property DisplayName As String
        Get
            Return _displayname
        End Get
        Set(value As String)
            _displayname = value
        End Set
    End Property

#End Region

End Class
