Imports System.Security.Cryptography
Imports System.Text

Public Class RSACryptographyKeys

#Region "Attributes"
    Dim xmlKeys As String 'A combination of both the public and     'private keys  
    Dim xmlPublicKey As String 'The public key only
#End Region

#Region "Methods"

    Public Sub New()
        'This creates both the public and the private keys
        Dim rsa As New RSACryptoServiceProvider()
        RSACryptoServiceProvider.UseMachineKeyStore = True

        'Hold both keys in a global variable that will be used in the decryption procedure   
        xmlKeys = rsa.ToXmlString(True)

        'Hold the public key to be used in the encryption procedure
        xmlPublicKey = rsa.ToXmlString(False)

    End Sub

    'Public Shared Property UseMachineKeyStore() As Boolean
    '    Get

    '    End Get
    '    Set(ByVal Value As Boolean)

    '    End Set
    'End Property

    Public ReadOnly Property PrivateKey() As String
        Get
            Return xmlKeys
        End Get
    End Property

    Public ReadOnly Property PublicKey() As String
        Get
            Return xmlPublicKey
        End Get
    End Property

    Public Function Encrypt(ByVal _txtdata As String, ByVal publickeys As String) As String
        Dim rsa As New RSACryptoServiceProvider

        'get the public key so you can encrypt the message:
        rsa.FromXmlString(publickeys)

        'get the message
        Dim message As String = _txtdata

        If message.Length > 58 Then
            MsgBox("You must use fewer than 59 characters")
            Return ""
            Exit Function
        End If


        'The plaintext message in a byte array
        Dim PlainTextBArray As Byte()
        'transform message string into a byte array:
        PlainTextBArray = (New UnicodeEncoding).GetBytes(message)

        'The cyphertext message in a byte array
        Dim CypherTextBArray As Byte()
        ' Encrypt 
        CypherTextBArray = rsa.Encrypt(PlainTextBArray, False)

        'view the cyphertext in txtcrypt:
        Dim txtencrypt As String = Convert.ToBase64String(CypherTextBArray)
        'txtencrypt = ""
        'For i As Integer = 0 To CypherTextBArray.Length - 1
        '    txtencrypt &= Chr(CypherTextBArray(i))
        'Next i

        'see the lengths of the plaintext, and the cyphertext
        'Me.Text = "Plaintext Length (Unicode): " & PlainTextBArray.Length & " Cyphertext Length: " & CypherTextBArray.Length
        Return txtencrypt
    End Function

    Public Function Decrypt(ByVal txtencrypt As String, ByVal privatekeys As String) As String
        Dim rsa As New RSACryptoServiceProvider

        'get the keys, thereby creating an RSA object that's identical
        ' to the one used in the Form_Load event when the keys were first built
        rsa.FromXmlString(privatekeys)

        Dim CypherTextBArray() As Byte = Convert.FromBase64String(txtencrypt)

        'create a byte array and then put the decrypted plaintext into it
        Dim RestoredPlainText() As Byte = rsa.Decrypt(CypherTextBArray, False)

        Dim txtdata As String '= Encoding.ASCII.GetString(RestoredPlainText)
        txtdata = ""
        'Step through the two-byte unicode plaintext, displaying the restored plaintext message
        For i As Integer = 0 To (RestoredPlainText.Length - 1) Step 2
            txtdata &= Chr(RestoredPlainText(i))
        Next i
        Return txtdata
    End Function

#End Region

End Class
