Imports System.Data
Imports System.Text
Imports System.IO
Imports Arsoft.Utility

Public Class SQLServerDBService

    Public Shared Function Execute(ByVal sql As String, Optional ByVal param() As SQLServerDBParameter = Nothing) As Integer
        Dim result As Integer = 0
        Dim acc As SQLServerDBAccess = New SQLServerDBAccess
        Try
            acc.BeginTransaction()
            result = acc.ExecuteNonQuery(sql, param)
            acc.CommitTransaction()
        Catch ex As Exception
            acc.RollbackTransaction()
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            acc.Dispose()
            acc = Nothing
        End Try
        Return result
    End Function

    Public Shared Function Query(ByVal sql As String, Optional ByVal param() As SQLServerDBParameter = Nothing) As DataTable
        Dim table As DataTable = New DataTable
        Dim acc As SQLServerDBAccess = New SQLServerDBAccess
        Try
            table = acc.ExecuteAdapter(sql, param)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            acc.Dispose()
            acc = Nothing
        End Try
        Return table
    End Function

    Public Shared Function Query(ByVal sql As String, ByVal startRecord As Integer, ByVal maxRecords As Integer, Optional ByVal param() As SQLServerDBParameter = Nothing) As DataSet
        Dim table As DataSet = New DataSet
        Dim acc As SQLServerDBAccess = New SQLServerDBAccess
        Try
            table = acc.ExecuteAdapter(sql, param, startRecord, maxRecords)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            acc.Dispose()
            acc = Nothing
        End Try
        Return table
    End Function

    Public Shared Function Scalar(ByVal sql As String, Optional ByVal param() As SQLServerDBParameter = Nothing) As Object
        Dim result As Object = Nothing
        Dim acc As SQLServerDBAccess = New SQLServerDBAccess
        Try
            acc.BeginTransaction()
            result = acc.ExecuteScalar(sql, param)
            acc.CommitTransaction()
        Catch ex As Exception
            acc.RollbackTransaction()
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            acc.Dispose()
            acc = Nothing
        End Try
        Return result
    End Function

#Region "Generator Object Class"

    Public Shared Function ListTable() As DataTable
        Dim _result As DataTable
        Dim _sql As String
        Dim _dbaccess As New SQLServerDBAccess
        Try
            _sql = "select * from sysobjects where xtype='U' order by name"
            _result = _dbaccess.ExecuteAdapter(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("DBService", "ListTable()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End Try
        Return _result
    End Function

    Public Shared Function ListFieldOfTable(ByVal _id As String) As DataTable
        Dim _result As DataTable
        Dim _sql As String
        Dim _dbaccess As New SQLServerDBAccess
        Try
            _sql = "select a.name as field ,b.name as type "
            _sql = _sql & "from syscolumns a, systypes b "
            _sql = _sql & "where a.xtype=b.xtype and a.id='" & _id & "' "
            _sql = _sql & "order by a.colorder"
            _result = _dbaccess.ExecuteAdapter(_sql)
        Catch ex As Exception
            UtilLogfile.writeToLog("DBService", "ListFieldOfTable()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End Try
        Return _result
    End Function

    Public Shared Function GenerateBusinessObjectClass(ByVal _id As String, ByVal _path As String) As Boolean
        Dim _result As Boolean = False
        Dim _fname As String
        Dim _tablename As String = ""
        Dim _fieldname As String
        Dim _fieldtype As String
        Dim _str As New StringBuilder
        Dim _sql As String

        Dim _attributeslist As New ArrayList
        Dim _propertrylist As New ArrayList

        Dim _dbaccess As New SQLServerDBAccess
        Try
            _sql = "select a.name as tablename, b.name as fieldname, c.name as fieldtype "
            _sql = _sql & "from sysobjects a,syscolumns b, systypes c "
            _sql = _sql & "where a.id=b.id and b.xtype=c.xtype and a.id='" & _id & "' "
            _sql = _sql & "order by b.colorder"
            _dbaccess.ExecuteReader(_sql)

            Dim _attributes As String
            Dim _methods As String
            Do While _dbaccess.Read
                _tablename = _dbaccess.GetItem("tablename")
                _fieldname = _dbaccess.GetItem("fieldname")
                _fieldtype = _dbaccess.GetItem("fieldtype")
                _attributes = CreateAttributes(_fieldname, _fieldtype)
                _attributeslist.Add(_attributes)
                _methods = CreateProperty(_fieldname, _fieldtype)
                _propertrylist.Add(_methods)
            Loop
            _dbaccess.CloseReader()


            Dim i As Integer
            With _str
                .Append("Public Class " & _tablename).Append(ControlChars.NewLine).Append(ControlChars.NewLine)
                .Append("#Region ""Attributes""").Append(ControlChars.NewLine)
                For i = 0 To _attributeslist.Count - 1
                    .Append(_attributeslist(i)).Append(ControlChars.NewLine)
                Next
                .Append("#End Region").Append(ControlChars.NewLine)

                .Append("#Region ""Methods""").Append(ControlChars.NewLine).Append(ControlChars.NewLine)
                For i = 0 To _propertrylist.Count - 1
                    .Append(_propertrylist(i)).Append(ControlChars.NewLine)
                Next
                .Append("#End Region").Append(ControlChars.NewLine).Append(ControlChars.NewLine)
                .Append("End Class").Append(ControlChars.NewLine)
            End With

            Dim _fout As StreamWriter
            _fname = _path & "\" & _tablename & ".vb"
            _fout = New StreamWriter(_fname, False, System.Text.Encoding.Default)
            _fout.Write(_str.ToString)
            _fout.Close()

            _result = True
        Catch ex As Exception
            UtilLogfile.writeToLog("DBService", "GenerateBusinessObjectClass()", ex.Message)
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            _dbaccess.Dispose()
            _dbaccess = Nothing
        End Try
        Return _result
    End Function

    Private Shared Function CreateAttributes(ByVal _fieldname As String, ByVal _fieldtype As String) As String
        Dim _result As String
        _result = "Dim _" & _fieldname.Trim.ToLower & " As " & FieldType(_fieldtype)
        Return _result
    End Function

    Private Shared Function CreateProperty(ByVal _fieldname As String, ByVal _fieldtype As String) As String
        Dim _result As String = ""
        Dim _typename As String
        Dim _str As New StringBuilder
        _typename = FieldType(_fieldtype)
        With _str
            .Append("Public Property ").Append(_fieldname.Trim.ToUpper).Append("() As ").Append(_typename).Append(ControlChars.NewLine)
            .Append("Get").Append(ControlChars.NewLine)
            .Append("Return _").Append(_fieldname.Trim.ToLower).Append(ControlChars.NewLine)
            .Append("End Get").Append(ControlChars.NewLine)
            .Append("Set (ByVal value As ").Append(_typename & ")").Append(ControlChars.NewLine)
            .Append("_" & _fieldname.Trim.ToLower & " = value").Append(ControlChars.NewLine)
            .Append("End Set").Append(ControlChars.NewLine)
            .Append("End Property").Append(ControlChars.NewLine)
        End With
        _result = _str.ToString
        Return _result
    End Function

    Private Shared Function FieldType(ByVal _fieldtype As String) As String
        Dim _result As String = ""
        Select Case _fieldtype.ToLower
            Case "bigint" : _result = "Long"
            Case "binary" : _result = "Binary"
            Case "bit" : _result = "Integer"
            Case "char" : _result = "String"
            Case "datetime" : _result = "Date"
            Case "decimal" : _result = "Decimal"
            Case "float" : _result = "Float"
            Case "image" : _result = "Binary"
            Case "int" : _result = "Integer"
            Case "money" : _result = "Double"
            Case "nchar" : _result = "String"
            Case "ntext" : _result = "String"
            Case "numeric" : _result = "Decimal"
            Case "nvarchar" : _result = "String"
            Case "real" : _result = "Byte"
            Case "smalldatetime" : _result = "Date"
            Case "smallint" : _result = "Integer"
            Case "smallmoney" : _result = "Double"
            Case "sql_variant" : _result = "Object"
            Case "sysname" : _result = "String"
            Case "text" : _result = "string"
            Case "timestamp" : _result = "Binary"
            Case "tinyint" : _result = "Byte"
            Case "uniqueidentifier" : _result = "Object"
            Case "varbinary" : _result = "Binary"
            Case "varchar" : _result = "String"
        End Select
        Return _result
    End Function

#End Region

End Class
