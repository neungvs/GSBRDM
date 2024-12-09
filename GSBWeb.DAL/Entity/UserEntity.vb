Public Class UserEntity
#Region "Attributes"

    Private _userid As Integer
    Private _username As String
    Private _password As String
    Private _employeeid As String
    Private _firstnameen As String
    Private _lastnameen As String
    Private _firstnameth As String
    Private _lastnameth As String
    Private _positionid As String
    Private _companyid As String
    Private _ou As String
    Private _groupid As Integer
    Private _groupcode As String
    Private _groupname_en As String
    Private _groupname_th As String
    Private _deptid As Integer
    Private _disabled As Boolean
    Private _userflag As Integer
    Private _countfail As Integer
    Private _timefail As DateTime
    Private _checktime As Integer
    Private _useraction As String
    Private _username_th As String
    Private _dtmstamp As DateTime
    Private _number As Integer
    '_createddate] [datetime] NULL,
    '_modifieddate] [datetime] NULL,
#End Region

#Region "Methods"

    Public Property UserID As Integer
        Get
            Return _userid
        End Get
        Set(value As Integer)
            _userid = value
        End Set
    End Property

    Public Property UserName As String
        Get
            Return _username
        End Get
        Set(value As String)
            _username = value
        End Set
    End Property

    Public Property Password As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Public Property EmployeeID As String
        Get
            Return _employeeid
        End Get
        Set(value As String)
            _employeeid = value
        End Set
    End Property

    Public Property FirstNameEN As String
        Get
            Return _firstnameen
        End Get
        Set(value As String)
            _firstnameen = value
        End Set
    End Property

    Public Property LastNameEN As String
        Get
            Return _lastnameen
        End Get
        Set(value As String)
            _lastnameen = value
        End Set
    End Property

    Public Property FirstNameTH As String
        Get
            Return _firstnameth
        End Get
        Set(value As String)
            _firstnameth = value
        End Set
    End Property

    Public Property LastNameTH As String
        Get
            Return _lastnameth
        End Get
        Set(value As String)
            _lastnameth = value
        End Set
    End Property

    Public Property PositionID As String
        Get
            Return _positionid
        End Get
        Set(value As String)
            _positionid = value
        End Set
    End Property

    Public Property CompanyID As String
        Get
            Return _companyid
        End Get
        Set(value As String)
            _companyid = value
        End Set
    End Property

    Public Property OU As String
        Get
            Return _ou
        End Get
        Set(value As String)
            _ou = value
        End Set
    End Property

    Public Property GroupID As Integer
        Get
            Return _groupid
        End Get
        Set(value As Integer)
            _groupid = value
        End Set
    End Property

    Public Property GroupCode As String
        Get
            Return _groupcode
        End Get
        Set(value As String)
            _groupcode = value
        End Set
    End Property

    Public Property GroupName_EN As String
        Get
            Return _groupname_en
        End Get
        Set(value As String)
            _groupname_en = value
        End Set
    End Property

    Public Property GroupName_TH As String
        Get
            Return _groupname_th
        End Get
        Set(value As String)
            _groupname_th = value
        End Set
    End Property

    Public Property DeptID As Integer
        Get
            Return _deptid
        End Get
        Set(value As Integer)
            _deptid = value
        End Set
    End Property

    Public Property Disabled As Boolean
        Get
            Return _disabled
        End Get
        Set(value As Boolean)
            _disabled = value
        End Set
    End Property

    Public Property UserFlag As Integer
        Get
            Return _userflag
        End Get
        Set(value As Integer)
            _userflag = value
        End Set
    End Property

    Public Property CountFail As Integer
        Get
            Return _countfail
        End Get
        Set(value As Integer)
            _countfail = value
        End Set
    End Property

    Public Property TimeFail As DateTime
        Get
            Return _timefail
        End Get
        Set(value As DateTime)
            _timefail = value
        End Set
    End Property

    Public Property CheckTime As Integer
        Get
            Return _checktime
        End Get
        Set(value As Integer)
            _checktime = value
        End Set
    End Property

    Public Property UserActivity As String
        Get
            Return _useraction
        End Get
        Set(value As String)
            _useraction = value
        End Set
    End Property

    Public Property UserName_TH As String
        Get
            Return _username_th
        End Get
        Set(value As String)
            _username_th = value
        End Set
    End Property

    Public Property DTmStamp As DateTime
        Get
            Return _dtmstamp
        End Get
        Set(value As DateTime)
            _dtmstamp = value
        End Set
    End Property

    Public Property Number As Integer
        Get
            Return _number
        End Get
        Set(value As Integer)
            _number = value
        End Set
    End Property

#End Region

End Class
