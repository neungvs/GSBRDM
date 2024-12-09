Imports System.Net
Imports System.IO
Imports System.Text

Public Class UtilOfFTP

    Public Sub SetFTPSetting(_ip As String, _userid As String, _password As String)
        FTPSettings.IP = _ip '"Domain Name"
        FTPSettings.UserID = _userid '"User ID"
        FTPSettings.Password = _password '"Password"
    End Sub

    Public Function IsFTPConnection() As Boolean
        Dim _result As Boolean = False
        Dim _reqftp As FtpWebRequest = Nothing
        _reqftp = DirectCast(FtpWebRequest.Create(New Uri("ftp://" + FTPSettings.IP)), FtpWebRequest)
        _reqftp.UseBinary = True
        _reqftp.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)

        Dim _resftp As Net.FtpWebResponse = CType(_reqftp.GetResponse, Net.FtpWebResponse)

        If (_resftp.StatusCode = FtpStatusCode.ClosingData Or _resftp.StatusCode = FtpStatusCode.ClosingControl Or _resftp.StatusCode = FtpStatusCode.ConnectionClosed Or _resftp.StatusCode = FtpStatusCode.FileStatus) Then
            _result = True
        Else
            _result = False
        End If

        Return _result

    End Function

    Public Function ValidFtpConnection() As Boolean
        'Dim siteUri As New Uri("ftp://ftp.servage.com")
        Dim request As FtpWebRequest = DirectCast(FtpWebRequest.Create(New Uri("ftp://" + FTPSettings.IP)), FtpWebRequest)
        request.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)

        request.Method = WebRequestMethods.Ftp.GetDateTimestamp
        request.UsePassive = True
        request.UseBinary = True
        request.KeepAlive = False
        Try
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Return True
        Catch e As WebException
            If e.Status = WebExceptionStatus.ProtocolError Then
                Return True
            ElseIf e.Status = WebExceptionStatus.ConnectFailure Then
                Return False
            Else
                'MessageBox.Show(e.Message)
                Return False
            End If
        End Try
    End Function

    Public Sub Download(_filepath As String, _filename As String)
        Dim _reqftp As FtpWebRequest = Nothing
        Dim _ftpStream As Stream = Nothing

        Try
            Dim outputStream As New FileStream(_filepath + "\" + _filename, FileMode.Create)
            _reqftp = DirectCast(FtpWebRequest.Create(New Uri("ftp://" + FTPSettings.IP + "/" + _filename)), FtpWebRequest)
            _reqftp.Method = WebRequestMethods.Ftp.DownloadFile
            _reqftp.UseBinary = True
            _reqftp.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)
            Dim response As FtpWebResponse = DirectCast(_reqftp.GetResponse(), FtpWebResponse)
            _ftpStream = response.GetResponseStream()

            Dim cl As Long = response.ContentLength
            Dim bufferSize As Integer = 2048
            Dim readCount As Integer
            Dim buffer As Byte() = New Byte(bufferSize - 1) {}

            readCount = _ftpStream.Read(buffer, 0, bufferSize)
            While readCount > 0
                outputStream.Write(buffer, 0, readCount)
                readCount = _ftpStream.Read(buffer, 0, bufferSize)
            End While

            _ftpStream.Close()
            outputStream.Close()
            response.Close()
        Catch ex As Exception
            If _ftpStream IsNot Nothing Then
                _ftpStream.Close()
                _ftpStream.Dispose()
            End If
            Throw New Exception(ex.Message.ToString())
        End Try
    End Sub

    Public Function UploadFile(_pathfile As String, _filename As String) As String
        Dim _result As String = ""
        Dim _ftpWebRequest As FtpWebRequest
        Dim _ftpWebResponse As FtpWebResponse
        Dim _ftpStreamWriter As StreamWriter

        _ftpWebRequest = WebRequest.Create("ftp://" & FTPSettings.IP & "/" & _filename)
        _ftpWebRequest.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)

        _ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile
        _ftpWebRequest.UseBinary = True

        _ftpStreamWriter = New StreamWriter(_ftpWebRequest.GetRequestStream())
        '_ftpStreamWriter.Write(New StreamReader(Server.MapPath("filename.ext")).ReadToEnd)
        _ftpStreamWriter.Write(New StreamReader(_pathfile & _filename).ReadToEnd)
        _ftpStreamWriter.Close()

        _ftpWebResponse = _ftpWebRequest.GetResponse()
        _result = _ftpWebResponse.StatusDescription
        _ftpWebResponse.Close()

        Return _result
    End Function

    Public Function DownloadFile(_pathfile As String, _filename As String) As String
        Dim _result As String = ""
        Dim _ftpWebRequest As FtpWebRequest
        Dim _ftpWebResponse As FtpWebResponse
        Dim _ftpStreamWriter As StreamWriter

        'myFtpWebRequest = WebRequest.Create("ftp://ftp_server_name/filename.ext")
        'myFtpWebRequest.Credentials = New NetworkCredential("username", "password")
        _ftpWebRequest = WebRequest.Create("ftp://" & FTPSettings.IP & "/" & _filename)
        _ftpWebRequest.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)

        _ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile
        _ftpWebRequest.UseBinary = True

        _ftpWebResponse = _ftpWebRequest.GetResponse()

        '_ftpStreamWriter = New StreamWriter(Server.MapPath("filename.ext"))
        _ftpStreamWriter = New StreamWriter(_pathfile & _filename)
        _ftpStreamWriter.Write(New StreamReader(_ftpWebResponse.GetResponseStream()).ReadToEnd)
        _ftpStreamWriter.Close()
        _result = _ftpWebResponse.StatusDescription
        _ftpWebResponse.Close()
        Return _result
    End Function

    Public Function DeleteFile(_filename As String) As String
        Dim _result As String = ""
        Dim _ftpWebRequest As FtpWebRequest
        Dim _ftpWebResponse As FtpWebResponse

        '_ftpWebRequest = WebRequest.Create("ftp://ftp_server_name/filename.ext")
        _ftpWebRequest = WebRequest.Create("ftp://" & FTPSettings.IP & "/" & _filename)
        _ftpWebRequest.Credentials = New NetworkCredential(FTPSettings.UserID, FTPSettings.Password)

        _ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile
        _ftpWebResponse = _ftpWebRequest.GetResponse()
        _result = _ftpWebResponse.StatusDescription
        _ftpWebResponse.Close()
        Return _result
    End Function


End Class

Public Class FTPSettings

#Region "Attributes"
    Private Shared _ip As String
    Private Shared _userid As String
    Private Shared _password As String
#End Region

#Region "Methodes"

    Public Shared Property IP As String
        Get
            Return _ip
        End Get
        Set(value As String)
            _ip = value
        End Set
    End Property

    Public Shared Property UserID As String
        Get
            Return _userid
        End Get
        Set(value As String)
            _userid = value
        End Set
    End Property

    Public Shared Property Password As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

#End Region

End Class

