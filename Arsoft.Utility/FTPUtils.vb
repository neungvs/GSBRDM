Imports WinSCP

Public Class FTPUtils

    Public Function DatasetMarketDownload(_ftpinfo As FTPInfo, _period As String, _ftppath As String, _localpath As String) As Boolean
        Dim _result As Boolean = False

        Try
            ' Setup session options
            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = IIf(_ftpinfo.Protocol = "22", Protocol.Sftp, Protocol.Ftp)
                .HostName = _ftpinfo.HostName
                .UserName = _ftpinfo.UserName
                .Password = _ftpinfo.Password
                .SshHostKeyFingerprint = IIf(_ftpinfo.Protocol = "22", _ftpinfo.SSHKey, Nothing)
            End With

            Using session As New Session

                'As WinSCP .NET assembly has to be stored in GAC to be used with SSIS,
                'you need to set path to WinSCP.exe explicitly, if using non-default location.
                session.ExecutablePath = _ftpinfo.ExecutablePath

                ' Connect
                session.Open(sessionOptions)

                ' Download files
                Dim transferOptions As New TransferOptions
                transferOptions.TransferMode = TransferMode.Binary

                Dim _ftpdata As String = ""
                If Not String.IsNullOrEmpty(_ftppath) Then
                    _ftpdata = String.Format("/{0}/*{1}*", _ftppath, _period)
                Else
                    _ftpdata = String.Format("/*{0}*", _period)
                End If

                Dim transferResult As TransferOperationResult
                transferResult = session.GetFiles(_ftpdata, _localpath, False, transferOptions)

                ' Throw on any error
                transferResult.Check()

                ' Print results
                For Each transfer In transferResult.Transfers
                    Console.WriteLine("Download of {0} succeeded", transfer.FileName)
                Next

            End Using

        Catch ex As Exception
            UtilLogfile.writeToLog("FTPUtils", "DatasetMarketDownload", ex.Message)
        End Try


        Return _result
    End Function

End Class


Public Class FTPInfo
    Private _protocol As String
    Private _hostname As String
    Private _username As String
    Private _pw As String
    Private _sshkey As String
    Private _executablepath As String

    Public Property Protocol() As String
        Get
            Return _protocol
        End Get
        Set(ByVal value As String)
            _protocol = value
        End Set
    End Property

    Public Property HostName() As String
        Get
            Return _hostname
        End Get
        Set(ByVal value As String)
            _hostname = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _pw
        End Get
        Set(ByVal value As String)
            _pw = value
        End Set
    End Property

    Public Property SSHKey() As String
        Get
            Return _sshkey
        End Get
        Set(ByVal value As String)
            _sshkey = value
        End Set
    End Property

    Public Property ExecutablePath() As String
        Get
            Return _executablepath
        End Get
        Set(ByVal value As String)
            _executablepath = value
        End Set
    End Property
End Class
