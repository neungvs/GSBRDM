Public Class IRRBBAccess

    Dim _dbaccess As SQLServerDBAccess

    Public Sub New()
        _dbaccess = New SQLServerDBAccess
    End Sub

    Public Function GetIRRBBParam() As IRRBBEntity

        Dim _param As New IRRBBEntity

        Dim _sql As String

        Try

            _sql = "SELECT * FROM IRRBB_SYS_PARAMETER"
            _dbaccess.ExecuteReader(_sql)

            If _dbaccess.Read Then

                _param.ReportUnit = _dbaccess.GetItem("REPORT_UNIT")
                _param.GraphBackward = _dbaccess.GetItem("GRAPH_BACKWARD")
                _param.PathReport = _dbaccess.GetItem("PATH_REPORT")
                _param.NIIMonth = _dbaccess.GetItem("NII_MONTH")
                _param.ReportHeading = _dbaccess.GetItem("REPORT_HEADING")
                _param.PathInput = _dbaccess.GetItem("PATH_INPUT_FILE")
                _param.PathInputAdjust = _dbaccess.GetItem("PATH_INPUT_FILE_ADJ")

            End If

            _dbaccess.CloseReader()

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

        Return _param

    End Function

    Public Sub UpdateIRRBBParam(ByVal unit As Integer, ByVal graph As Integer, ByVal month As Integer, ByVal heading As String)

        Dim _sql As String
        Dim _param(4) As SQLServerDBParameter

        Try

            _sql = "Update  IRRBB_SYS_PARAMETER set REPORT_UNIT = @unit,GRAPH_BACKWARD = @graph,NII_MONTH = @month,REPORT_HEADING = @heading"

            _param(0) = New SQLServerDBParameter("@unit", unit)
            _param(1) = New SQLServerDBParameter("@graph", graph)
            _param(2) = New SQLServerDBParameter("@month", month)
            _param(3) = New SQLServerDBParameter("@heading", heading)


            _dbaccess.ExecuteNonQuery(_sql, _param)


        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

    End Sub

    Public Sub DeleteNii(ByVal dataasof As Integer)

        Dim _sql As String
        Dim _param(1) As SQLServerDBParameter

        Try

            _sql = "Delete  IRRBB_NII where DATA_ASOF = @dataasof"

            _param(0) = New SQLServerDBParameter("@dataasof", dataasof)

            _dbaccess.ExecuteNonQuery(_sql, _param)


        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

    End Sub

    Public Sub InsertNii(ByVal dataAsOf As Integer, ByVal dataAsOfT As Integer, ByVal dataAsOfTX As Integer, ByVal alert As Double, ByVal ceiling As Double, ByVal niiValue As Double, ByVal niiSource As String)


        Dim _sql As String
        Dim _param(8) As SQLServerDBParameter

        Try

            _sql = "insert into  IRRBB_NII values(@dataAsOf,@dataAsOfT,@dataAsOfTX,@niiSource,@niiValue,@alert,@ceiling,@update)"

            _param(0) = New SQLServerDBParameter("@dataAsOf", dataAsOf)
            _param(1) = New SQLServerDBParameter("@dataAsOfT", dataAsOfT)
            _param(2) = New SQLServerDBParameter("@dataAsOfTX", dataAsOfTX)
            _param(3) = New SQLServerDBParameter("@niiSource", niiSource)
            _param(4) = New SQLServerDBParameter("@niiValue", niiValue)
            _param(5) = New SQLServerDBParameter("@alert", alert)
            _param(6) = New SQLServerDBParameter("@ceiling", ceiling)
            _param(7) = New SQLServerDBParameter("@update", Now)

            _dbaccess.ExecuteNonQuery(_sql, _param)


        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

    End Sub

    Public Function SelectNii(ByVal dataasof As Integer) As IRRBBEntity

        Dim _param As New IRRBBEntity
        Dim _sql As String

        Try

            _sql = "SELECT * FROM IRRBB_NII where DATA_ASOF = " + dataasof.ToString
            _dbaccess.ExecuteReader(_sql)

            If _dbaccess.Read Then

                _param.AlertRiskPercent = _dbaccess.GetItem("ALERT_RISK_PERCENT")
                _param.CeilingRiskPercent = _dbaccess.GetItem("CEILING_RISK_PERCENT")
                _param.NiiSource = _dbaccess.GetItem("NII_SOURCE")
                _param.NiiValue = _dbaccess.GetItem("NII_VALUE")
                _param.DataAsOf_T = _dbaccess.GetItem("DATA_ASOF_T")
                _param.DataAsOf_TX = _dbaccess.GetItem("DATA_ASOF_TX")
            End If

            _dbaccess.CloseReader()

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

        Return _param

    End Function

    Public Function SelectIRRBBStatus(ByVal dataasof As Integer) As DataTable

        Dim _param As New DataTable
        Dim _sql As String

        Try

            _sql = "SELECT a.*,b.firstnameth+' '+isnull(b.lastnameth,'') as PROCESS_BYNAME FROM IRRBB_Status a left join rdmsys_users b on PROCESS_BY = userid where DATA_ASOF = " + dataasof.ToString
            _param = _dbaccess.ExecuteAdapter(_sql)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

        Return _param

    End Function

    Public Function SelectIRRBBStatusAll() As DataTable

        Dim _param As New DataTable
        Dim _sql As String

        Try

            _sql = "SELECT a.*,b.firstnameth+' '+isnull(b.lastnameth,'') as PROCESS_BYNAME FROM IRRBB_Status a left join rdmsys_users b on PROCESS_BY = userid order by DATA_ASOF desc"
            _param = _dbaccess.ExecuteAdapter(_sql)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

        Return _param

    End Function


    Public Function SelectIRRBBStatusDIFF(ByVal dataasof As Integer) As DataTable

        Dim _param As New DataTable
        Dim _sql As String

        Try

            _sql = "SELECT [PROCESS_DATE],[DATA_ASOF],[PROCESS_BY],[PROCESS_STATUS],[DIFF_FILE] FROM IRRBB_Status where DIFF_FILE is not null and  DATA_ASOF = " + dataasof.ToString
            _param = _dbaccess.ExecuteAdapter(_sql)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

        Return _param

    End Function

    Public Sub Insert_UploadTemplate(ByVal _uploaddate As DateTime, ByVal _user As String, ByVal _filename As String)

        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter

        Try

            _sql = "insert into [IRRBB_UPLOAD_TEMPLATE] values(@uploadDate,@FileName,@UploadBy)"
            _param(0) = New SQLServerDBParameter("@uploadDate", _uploaddate)
            _param(1) = New SQLServerDBParameter("@FileName", _filename)
            _param(2) = New SQLServerDBParameter("@UploadBy", _user)
            _dbaccess.ExecuteNonQuery(_sql, _param)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

    End Sub

    Public Function SelectIRRBB_UploadTemplate(ByVal _startdate As String, ByVal _enddate As String) As DataTable

        Dim _ret As New DataTable
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter

        Try

            _sql = "select a.upload_date,a.filename,b.firstnameth+' '+isnull(b.lastnameth,'') as uploadby from IRRBB_UPLOAD_TEMPLATE a left join rdmsys_users b on UPLOADBY = userid where a.upload_date BETWEEN @startdate AND @enddate order by a.upload_date desc"
            _param(0) = New SQLServerDBParameter("@startdate", _startdate)
            _param(1) = New SQLServerDBParameter("@enddate", _enddate)
            _ret = _dbaccess.ExecuteAdapter(_sql, _param)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

        Return _ret

    End Function

    Public Function SelectIRRBB_UploadTemplate_Onpageload() As DataTable

        Dim _ret As New DataTable
        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter

        Try

            _sql = "select top 10 a.upload_date,a.filename,b.firstnameth+' '+isnull(b.lastnameth,'') as uploadby from IRRBB_UPLOAD_TEMPLATE a left join rdmsys_users b on UPLOADBY = userid order by a.upload_date desc"
            _ret = _dbaccess.ExecuteAdapter(_sql)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try

        Return _ret

    End Function

    Public Sub Update_Status(ByVal userid As String, ByVal dataasof As Integer)

        Dim _sql As String
        Dim _param(2) As SQLServerDBParameter

        Try

            _sql = "update [IRRBB_STATUS] set [PROCESS_BY] = @userid where [DATA_ASOF] = @dataasof"
            _param(0) = New SQLServerDBParameter("@userid", userid)
            _param(1) = New SQLServerDBParameter("@dataasof", dataasof)
            _dbaccess.ExecuteNonQuery(_sql, _param)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try
    End Sub

    Public Sub Update_Status_Process(ByVal status As String, ByVal statusid As Integer, ByVal dataasof As Integer)

        Dim _sql As String
        Dim _param(3) As SQLServerDBParameter

        Try

            _sql = "update [IRRBB_STATUS] set [PROCESS_STATUS] = @status,[PROCESS_STATUS_ID] = @statusid where [DATA_ASOF] = @dataasof"
            _param(0) = New SQLServerDBParameter("@status", status)
            _param(1) = New SQLServerDBParameter("@statusid", statusid)
            _param(2) = New SQLServerDBParameter("@dataasof", dataasof)
            _dbaccess.ExecuteNonQuery(_sql, _param)

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        End Try
    End Sub


End Class
