Imports System.Security.Cryptography
Imports System.Text

Public Class RSACryptographyCsp

#Region "Attributes"
    Dim param As CspParameters
    Dim RSA As New RSACryptoServiceProvider
#End Region

#Region "Methods"

    Public Sub New()
        'Set Flag and Initialization RSA
        param = New CspParameters
        param.Flags = CspProviderFlags.UseMachineKeyStore
        RSA = New RSACryptoServiceProvider(param)
    End Sub

    Public Function EncrytionRSA(ByVal txtdata As String) As String
        'Encription
        Dim data() As Byte = Encoding.ASCII.GetBytes(txtdata)
        Dim encryptedData() As Byte = RSA.Encrypt(data, False)
        Return Convert.ToBase64String(encryptedData)
    End Function

    Public Function DecryptionRSA(ByVal txtencrypt As String) As String
        'Decrytion
        Dim data() As Byte = Convert.FromBase64String(txtencrypt)
        Dim decrytedData() As Byte = RSA.Decrypt(data, False)
        Return Encoding.ASCII.GetString(decrytedData)
    End Function

#End Region

End Class
