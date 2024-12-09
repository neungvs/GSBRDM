Imports Arsoft.Utility
Imports GSBWeb.DAL

Public Class FactorNameBiz
    Dim _factorNameAcc As New FactorNameAccess

    Public Function GetFactorDate() As List(Of TimeEntity)
        Dim _result As List(Of TimeEntity)
        _result = _factorNameAcc.GetFactorDate().OrderByDescending(Function(x) x.TimeId).ToList()
        Return _result
    End Function

    Public Function GetByTime(_timeId As String) As List(Of FactorNameEntity)
        Dim _result As List(Of FactorNameEntity)
        _result = _factorNameAcc.GetDataByTimeId(_timeId)
        Return _result
    End Function

    Public Function SaveCreateNew(timeId As String, userId As String) As Boolean
        Try
            _factorNameAcc.CreateNew(timeId, userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameBiz", "SaveCreateNew", ex.Message)
        End Try
        Return False
    End Function

    Public Function SaveAdd(_timeId As String, _factorName As String, _factorDesc As String, _unit As String, _userId As Integer) As Boolean
        Try
            _factorNameAcc.Add(_timeId, _factorName, _factorDesc, _unit, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameBiz", "SaveAdd", ex.Message)
        End Try
        Return False
    End Function

    Public Function SaveUpdate(_timeId As String, _factorId As Integer, _senarioName As String, _senarioDes As String, _unit As String, _userId As Integer) As Boolean
        Try
            _factorNameAcc.Update(_timeId, _factorId, _senarioName, _senarioDes, _unit, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameBiz", "SaveUpdate", ex.Message)
        End Try
        Return False
    End Function

    Public Function Delete(_timeId As String, _factorId As Integer, _userId As Integer) As Boolean
        Try
            _factorNameAcc.Delete(_timeId, _factorId, _userId)
            Return True
        Catch ex As Exception
            UtilLogfile.writeToLog("FactorNameBiz", "Delete", ex.Message)
        End Try
        Return False
    End Function

    Public Function GetFactor(timeId As String) As List(Of FactorEntity)
        Dim _result As List(Of FactorEntity)
        _result = _factorNameAcc.GetFactorByTimeId(timeId)
        Return _result
    End Function

End Class