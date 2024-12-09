Public Class UtilOfValidate

#Region "Attributes"

#End Region

#Region "Methods"

    Enum BooleanType
        eString = 0
        eInteger = 1
    End Enum

    Public Shared Function CheckString(ByVal _data As Object) As Object
        Dim _rt As String = ""
        If _data Is Nothing Then
            Return _rt
        Else
            Return _data
        End If
    End Function

    Public Shared Function CheckNumeric(ByVal _data As Object) As Object
        Dim _rt As Integer = 0
        If _data Is Nothing Then
            Return _rt
        Else
            If IsNumeric(_data) Then
                Return _data
            End If
        End If
        Return _rt
    End Function

    Public Shared Function CheckBoolean(ByVal _data As Object, _type As BooleanType) As String
        Dim _rt As String = ""
        If (_data Is Nothing) Or IsDBNull(_data) Then
            Return _rt
        Else
            Select Case _type
                Case BooleanType.eInteger
                    Select Case _data
                        Case "False", 0
                            _rt = 0
                        Case "True", 1
                            _rt = 1
                        Case Else
                            _rt = -1
                    End Select
                Case BooleanType.eString
                    Select Case _data
                        Case "False", 0
                            _rt = "Fasle"
                        Case "True", 1
                            _rt = "True"
                        Case Else
                            _rt = ""
                    End Select
            End Select
        End If
        Return _rt
    End Function

#End Region

End Class
