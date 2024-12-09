Public Class IRRBBEntity

#Region "Attributes"

    Private _reportunit As Integer
    Private _graphbackward As Integer
    Private _pathreport As String
    Private _niimonth As Integer
    Private _reportheading As String
    Private _dataasof As Integer
    Private _alertriskpercent As Double
    Private _ceilingriskpercent As Double
    Private _niivalue As Double
    Private _niisource As String
    Private _pathinput As String
    Private _pathinputadjust As String
    Private _dataasofT As Integer
    Private _dataasofTX As Integer

#End Region

#Region "Methods"

    Public Property ReportUnit As Integer
        Get
            Return _reportunit
        End Get
        Set(value As Integer)
            _reportunit = value
        End Set
    End Property

    Public Property GraphBackward As Integer
        Get
            Return _graphbackward
        End Get
        Set(value As Integer)
            _graphbackward = value
        End Set
    End Property

    Public Property PathReport As String
        Get
            Return _pathreport
        End Get
        Set(value As String)
            _pathreport = value
        End Set
    End Property

    Public Property NIIMonth As Integer
        Get
            Return _niimonth
        End Get
        Set(value As Integer)
            _niimonth = value
        End Set
    End Property


    Public Property ReportHeading As String
        Get
            Return _reportheading
        End Get
        Set(value As String)
            _reportheading = value
        End Set
    End Property

    Public Property DataAsOf As Integer
        Get
            Return _dataasof
        End Get
        Set(value As Integer)
            _dataasof = value
        End Set
    End Property

    Public Property AlertRiskPercent As Double
        Get
            Return _alertriskpercent
        End Get
        Set(value As Double)
            _alertriskpercent = value
        End Set
    End Property

    Public Property CeilingRiskPercent As Double
        Get
            Return _ceilingriskpercent
        End Get
        Set(value As Double)
            _ceilingriskpercent = value
        End Set
    End Property

    Public Property NiiValue As Double
        Get
            Return _niivalue
        End Get
        Set(value As Double)
            _niivalue = value
        End Set
    End Property

    Public Property NiiSource As String
        Get
            Return _niisource
        End Get
        Set(value As String)
            _niisource = value
        End Set
    End Property

    Public Property PathInput As String
        Get
            Return _pathinput
        End Get
        Set(value As String)
            _pathinput = value
        End Set
    End Property

    Public Property PathInputAdjust As String
        Get
            Return _pathinputadjust
        End Get
        Set(value As String)
            _pathinputadjust = value
        End Set
    End Property

    Public Property DataAsOf_T As Integer
        Get
            Return _dataasofT
        End Get
        Set(value As Integer)
            _dataasofT = value
        End Set
    End Property

    Public Property DataAsOf_TX As Integer
        Get
            Return _dataasofTX
        End Get
        Set(value As Integer)
            _dataasofTX = value
        End Set
    End Property

#End Region

End Class
