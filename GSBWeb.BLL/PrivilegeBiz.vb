Imports GSBWeb.DAL

Public Class PrivilegeBiz
    Dim _privilegeacc As New PrivilegeAccess

    Public Function SetGroupPrivilege(ByVal _datalist As List(Of PrivilegeEntity)) As Boolean
        Dim _result As Boolean
        _result = _privilegeacc.SetGroupPrivilege(_datalist)

        Return _result
    End Function

    Public Function SetUserPrivilege(ByVal _datalist As List(Of PrivilegeEntity)) As Boolean
        Dim _result As Boolean
        _result = _privilegeacc.SetUserPrivilege(_datalist)

        Return _result
    End Function

    Public Sub Dispost()
        _privilegeacc.Dispose()

        If Not _privilegeacc Is Nothing Then
            _privilegeacc = Nothing
        End If

    End Sub

End Class
