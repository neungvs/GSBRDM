Imports System.IO
Imports System.IO.FileSystemInfo
Imports System.Configuration

Public Class UtilLogfile

#Region "Attributes"
    Private Shared logfilename As String
    Private Shared logfile As StreamWriter
    Private Shared folderfile As String = ConfigurationManager.AppSettings("logfile").ToString
#End Region

#Region "Methods"

    Public Shared Function GetAppPath() As String
        Dim _path As String
        _path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()(0).FullyQualifiedName)
        If _path.Substring(_path.Length - 1, 1) <> "\" Then
            _path += "\"
        End If
        Return _path
    End Function

    Public Shared Sub writeToLog(ByRef verrordescription As String)
        If logfile Is Nothing Then
            logfilename = "error-" & Format(Now, "yyyyMMdd") & ".log"
            logfile = New StreamWriter(folderfile & logfilename, True)
            logfile.AutoFlush = True
        ElseIf Not logfilename.Equals("error-" & Format(Now, "yyyyMMdd") & ".log") Then
            logfile.Close()
            logfilename = ""

            logfilename = "error-" & Format(Now, "yyyyMMdd") & ".log"
            logfile = New StreamWriter(folderfile & logfilename, True)
            logfile.AutoFlush = True
        End If

        SyncLock (logfile)
            logfile.WriteLine("Internal" & " | " & Now & " | " & verrordescription)
        End SyncLock
    End Sub

    Public Shared Sub writeToLog(ByRef vsessionid As String, ByRef verrordescription As String)
        If logfile Is Nothing Then
            logfilename = "error-" & Format(Now, "yyyyMMdd") & ".log"
            logfile = New StreamWriter(folderfile & logfilename, True)
            logfile.AutoFlush = True
        ElseIf Not logfilename.Equals("error-" & Format(Now, "yyyyMMdd") & ".log") Then
            logfile.Close()
            logfilename = ""

            logfilename = "error-" & Format(Now, "yyyyMMdd") & ".log"
            logfile = New StreamWriter(folderfile & logfilename, True)
            logfile.AutoFlush = True
        End If

        SyncLock (logfile)
            logfile.WriteLine(vsessionid & " | " & Now & " | " & verrordescription)
        End SyncLock
    End Sub

    Public Shared Sub writeToLog(ByRef _class As String, ByRef _function As String, ByRef _description As String)
        If logfile Is Nothing Then
            logfilename = "error-" & Format(Now, "yyyyMMdd") & ".log"
            logfile = New StreamWriter(folderfile & logfilename, True)
            logfile.AutoFlush = True
        ElseIf Not logfilename.Equals("error-" & Format(Now, "yyyyMMdd") & ".log") Then
            logfile.Close()
            logfilename = ""

            logfilename = "error-" & Format(Now, "yyyyMMdd") & ".log"
            logfile = New StreamWriter(folderfile & logfilename, True)
            logfile.AutoFlush = True
        End If

        SyncLock (logfile)
            logfile.WriteLine(Now & " | " & _class & " | " & _function & " | " & _description)
        End SyncLock
    End Sub

#End Region

End Class
