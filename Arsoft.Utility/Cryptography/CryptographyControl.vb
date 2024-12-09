Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
'Imports System.Text.Encoding
Imports System.ComponentModel


Public Class CryptographyControl

#Region "Attributes"
    'RSA Cryptograhy
    Dim xmlKeys As String 'A combination of both the public and private keys  
    Dim xmlPublicKey As String 'The public key only

    'File names to be used for public and private keys
    Private Const KEY_PUBLIC As String = "public.key"
    Private Const KEY_PRIVATE As String = "private.key"

#End Region

    Public Sub New()
        'This creates both the public and the private keys
        'RSACryptoServiceProvider.UseMachineKeyStore = true;
        Dim _cspparam As New CspParameters
        _cspparam.Flags = CspProviderFlags.UseMachineKeyStore
        Dim rsa As New RSACryptoServiceProvider(_cspparam)
        RSACryptoServiceProvider.UseMachineKeyStore = True
        'Hold both keys in a global variable that will be used in the decryption procedure   
        xmlKeys = rsa.ToXmlString(True)
        'Hold the public key to be used in the encryption procedure
        xmlPublicKey = rsa.ToXmlString(False)
    End Sub

#Region "Symmetric (Secret-Key) Cryptography"
    '//**************
    'จะใช้คีย์ลับ (Secret key) เพียงคีย์เดียวในการเข้ารหัสและถอดรหัสข้อมูล)
    'Algorithms     Key             IV(Initialization Vector)
    'RC2            5,6,7 ตัวอักษร         8 ตัว
    'DES            8 ตัวอักษร                 8 ตัว
    'Rijindeal      16,24,32 ตัวอักษร   16 ตัว
    'วิธีใช้คำสั่งพิเศษทั้ง 4 คือ
    'ข้อมูล --> แปลงให้เป็นรหัส ASCII(Encoding.ASCII.GetByte())--> เข้ารหัส--> แปลงให้เป็น String(Convert.ToBase64String()) --> ฐานข้อมูล
    'ข้อมูล <-- แปลงให้เป็นรหัส String(Encoding.ASCII.GetString()<-- เข้ารหัส <-- แปลงให้เป็น Byte(Convert.FromoBase64String()) <-- ฐานข้อมูล
    '//**************
    Public Function EncryptedRC2(ByVal txtdata As String, ByVal _keys As String) As String
        Dim txtencrypt As String
        Dim encryptedPassword As New MemoryStream
        Dim RC2 As New RC2CryptoServiceProvider
        '//Key ต้องเท่ากับ 5,6,7 ตัวอักษร
        RC2.Key = Encoding.ASCII.GetBytes(_keys)
        Dim iv() As Byte = {11, 12, 33, 50, 78, 25, 72, 84}
        RC2.IV = iv
        Dim myEncryptor As ICryptoTransform = RC2.CreateEncryptor
        Dim pwd() As Byte = Encoding.ASCII.GetBytes(txtdata)

        Dim myCryptoStream As New CryptoStream(encryptedPassword, myEncryptor, CryptoStreamMode.Write)
        myCryptoStream.Write(pwd, 0, pwd.Length)
        myCryptoStream.Close()

        txtencrypt = Convert.ToBase64String(encryptedPassword.ToArray)

        Return txtencrypt
    End Function

    Public Function DecryptedRC2(ByVal txtencryptdata As String, ByVal _keys As String) As String
        Dim txtdecrypt As String
        Dim decryptedPassword As New MemoryStream
        Dim RC2 As New RC2CryptoServiceProvider
        '//Key ต้องเท่ากับ 5,6,7 ตัวอักษร 
        RC2.Key = Encoding.ASCII.GetBytes(_keys)
        Dim iv() As Byte = {11, 12, 33, 50, 78, 25, 72, 84}
        RC2.IV = iv
        Dim myDecryptor As ICryptoTransform = RC2.CreateDecryptor
        Dim encryptpwd() As Byte = Convert.FromBase64String(txtencryptdata)

        Dim myCryptoStream As New CryptoStream(decryptedPassword, myDecryptor, CryptoStreamMode.Write)
        myCryptoStream.Write(encryptpwd, 0, encryptpwd.Length)
        myCryptoStream.Close()

        txtdecrypt = Encoding.ASCII.GetString(decryptedPassword.ToArray)

        Return txtdecrypt
    End Function

    Public Function EncryptedDES(ByVal txtdata As String, ByVal _keys As String) As String
        Dim txtencrypt As String
        Dim encryptedPassword As New MemoryStream
        Dim DES As New DESCryptoServiceProvider
        '//Key ต้องเท่ากับ 8 ตัวอักษร
        DES.Key = Encoding.ASCII.GetBytes(_keys)
        Dim iv() As Byte = {11, 12, 33, 50, 78, 25, 72, 84}
        DES.IV = iv
        Dim myEncryptor As ICryptoTransform = DES.CreateEncryptor
        Dim myCryptoStream As New CryptoStream(encryptedPassword, myEncryptor, CryptoStreamMode.Write)
        Dim pwd() As Byte = Encoding.ASCII.GetBytes(txtdata)
        myCryptoStream.Write(pwd, 0, pwd.Length)
        myCryptoStream.Close()

        txtencrypt = Convert.ToBase64String(encryptedPassword.ToArray)

        Return txtencrypt
    End Function

    Public Function DecryptedDES(ByVal txtencryptdata As String, ByVal _keys As String) As String
        Dim txtdecrypt As String
        Dim decryptedPassword As New MemoryStream
        Dim DES As New DESCryptoServiceProvider
        '//Key ต้องเท่ากับ 8 ตัวอักษร
        DES.Key = Encoding.ASCII.GetBytes(_keys)
        Dim iv() As Byte = {11, 12, 33, 50, 78, 25, 72, 84}
        DES.IV = iv
        Dim myDecryptor As ICryptoTransform = DES.CreateDecryptor
        Dim myCryptoStream As New CryptoStream(decryptedPassword, myDecryptor, CryptoStreamMode.Write)
        Dim encryptpwd() As Byte = Convert.FromBase64String(txtencryptdata)
        myCryptoStream.Write(encryptpwd, 0, encryptpwd.Length)
        myCryptoStream.Close()

        txtdecrypt = Encoding.ASCII.GetString(decryptedPassword.ToArray)

        Return txtdecrypt
    End Function

    Public Function EncryptedRijndael(ByVal txtdata As String, ByVal _keys As String) As String
        Dim txtencrypt As String
        Dim encryptedPassword As New MemoryStream
        Dim Rijndael As New RijndaelManaged

        'Dim key() As Byte
        'Dim IV() As Byte
        ''Create a new key and initialization vector.
        'myRijndael.GenerateKey()
        'myRijndael.GenerateIV()
        ''Get the key and IV.
        'key = myRijndael.Key
        'IV = myRijndael.IV
        ''Get an encryptor.
        'Dim encryptor As ICryptoTransform = myRijndael.CreateEncryptor(key, IV)

        '//Key ต้องเท่ากับ 16,24,32 ตัวอักษร
        Rijndael.Key = Encoding.ASCII.GetBytes(_keys)
        Dim iv() As Byte = {11, 12, 33, 50, 78, 25, 72, 84, 23, 65, 48, 69, 250, 36, 125, 147}
        Rijndael.IV = iv
        Dim myEncryptor As ICryptoTransform = Rijndael.CreateEncryptor

        Dim myCryptoStream As New CryptoStream(encryptedPassword, myEncryptor, CryptoStreamMode.Write)
        Dim pwd() As Byte = Encoding.ASCII.GetBytes(txtdata)
        myCryptoStream.Write(pwd, 0, pwd.Length)
        myCryptoStream.Close()

        txtencrypt = Convert.ToBase64String(encryptedPassword.ToArray)

        Return txtencrypt
    End Function

    Public Function DecryptedRijndael(ByVal txtencryptdata As String, ByVal _keys As String) As String
        Dim txtdecrypt As String
        Dim decryptedPassword As New MemoryStream
        Dim Rijndael As New RijndaelManaged

        '//Key ต้องเท่ากับ 16,24,32 ตัวอักษร
        Rijndael.Key = Encoding.ASCII.GetBytes(_keys)
        Dim iv() As Byte = {11, 12, 33, 50, 78, 25, 72, 84, 23, 65, 48, 69, 250, 36, 125, 147}
        Rijndael.IV = iv
        Dim myDecryptor As ICryptoTransform = Rijndael.CreateDecryptor

        Dim myCryptoStream As New CryptoStream(decryptedPassword, myDecryptor, CryptoStreamMode.Write)
        Dim encryptpwd() As Byte = Convert.FromBase64String(txtencryptdata)
        myCryptoStream.Write(encryptpwd, 0, encryptpwd.Length)
        myCryptoStream.Close()

        txtdecrypt = Encoding.ASCII.GetString(decryptedPassword.ToArray)

        Return txtdecrypt
    End Function

    '// ใช้ Xor ในการ เข้า/ถอด รหัสเขียนแบบง่าย ๆ อาจจะไม่ค่อยดีเท่าที่ควรแต่ก็พอใช้ได้
    Public Function XORCryptography(ByVal cs As String, ByVal _keys As String) As String
        Dim i, j, p
        Dim ckey As String
        Dim temp As String
        Dim etemp As String
        Dim ctemp As String

        etemp = ""
        If Len(cs) = 0 Then
            Return etemp
            Exit Function
        End If

        p = 0
        ckey = _keys
        For i = 1 To Len(cs)
            temp = Mid(cs, i, 1)
            ctemp = Asc(temp)

            For j = 1 To Len(ckey)
                ctemp = ctemp Xor (Asc(Mid(ckey, j, 1)))
            Next
            p = p + 1

            If p = Len(ckey) Then p = 1
            If p > Len(ckey) Then p = 1
            ctemp = ctemp Xor Asc(Mid(ckey, p, 1))
            etemp = etemp + Chr(ctemp)
        Next
        Return etemp
    End Function

#End Region

#Region "Asymmetric (Public-Key) Cryptography"
    '//***************
    'การเข้ารหัสแบบ Public-Key จะใช้คีย์ทั้งหมด 2 คีย์ได้แก่ 
    'Public key ในการเข้ารัหสและ Private key ในการถอดรหัส

    '//***************
    'Public Shared Property UseMachineKeyStore() As Boolean
    '    Get

    '    End Get
    '    Set(ByVal Value As Boolean)

    '    End Set
    'End Property

    Public ReadOnly Property RSAPrivateKey() As String
        Get
            Return xmlKeys
        End Get
    End Property

    Public ReadOnly Property RSAPublicKey() As String
        Get
            Return xmlPublicKey
        End Get
    End Property

    Public Function EncryptedRSA(ByVal txtdata As String, ByVal publickeys As String) As String
        Dim rsa As New RSACryptoServiceProvider

        'get the public key so you can encrypt the message:
        rsa.FromXmlString(publickeys)

        'get the message
        Dim message As String = txtdata

        '2010/07/31
        If message.Length > 58 Then
            MsgBox("You must use fewer than 59 characters")
            Return Nothing
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

    Public Function DecryptedRSA(ByVal txtencryptdata As String, ByVal privatekeys As String) As String
        Dim rsa As New RSACryptoServiceProvider

        'get the keys, thereby creating an RSA object that's identical
        rsa.FromXmlString(privatekeys)

        Dim CypherTextBArray() As Byte = Convert.FromBase64String(txtencryptdata)

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

    '********************************************************
    '* GetTextFromFile: Reads the text from a file
    '********************************************************
    Private Function GetTextFromFile(ByVal fileName As String) As String
        If File.Exists(fileName) Then
            Dim textFile As StreamReader = File.OpenText(fileName)
            Dim result As String = textFile.ReadToEnd
            textFile.Close()
            textFile = Nothing
            Return result
        Else
            Throw New IOException("Specified file does not exist")
            Return Nothing
        End If
    End Function
#End Region

#Region "Digital Signatures Cryptography"

#End Region

#Region "Hash Cryptography"
    '//**************
    'การเข้ารหัสแบบ hash จะไม่สามารถทำให้ข้อมูลเหมือนเดิมได้ลักษณะการใช้งานคือ
    'เก็บไว้ในฐานข้อมูลแล้วเปรียบกับข้อมูลที่ส่งเข้าว่าเหมือนกันหรือไม่
    'Algorithms     size digest
    'MD5
    'SHA1           28 byte
    'SHA256         44 byte
    'SHA384         64 byte
    'SHA512         88 byte
    '//***************
    Public Function EncryptedMD5(ByVal txtdata As String) As String
        Dim md5Hasher As New MD5CryptoServiceProvider
        Dim encoder As New UTF8Encoding
        Dim hashedBytes() As Byte = encoder.GetBytes(txtdata)
        'Dim hashedBytes() As Byte = Encoding.ASCII.GetBytes(txtdata)
        Dim encryptedData() As Byte = md5Hasher.ComputeHash(hashedBytes)
        Dim txtencrypted As String
        txtencrypted = Convert.ToBase64String(encryptedData)
        Return txtencrypted
    End Function

    Public Function EncryptedSHA1(ByVal txtdata As String) As String
        Dim sha1Hasher As New SHA1Managed
        Dim hashedBytes() As Byte = Encoding.ASCII.GetBytes(txtdata)
        Dim encryptedData() As Byte = sha1Hasher.ComputeHash(hashedBytes)
        Dim txtencrypted As String
        txtencrypted = Convert.ToBase64String(encryptedData)
        Return txtencrypted
    End Function

    Public Function EncryptedSHA256(ByVal txtdata As String) As String
        Dim sha256Hasher As New SHA256Managed
        Dim hashedBytes() As Byte = Encoding.ASCII.GetBytes(txtdata)
        Dim encryptedData() As Byte = sha256Hasher.ComputeHash(hashedBytes)
        Dim txtencrypted As String
        txtencrypted = Convert.ToBase64String(encryptedData)
        Return txtencrypted
    End Function

    Public Function EncryptedSHA384(ByVal txtdata As String) As String
        Dim sha384Hasher As New SHA384Managed
        Dim hashedBytes() As Byte = Encoding.ASCII.GetBytes(txtdata)
        Dim encryptedData() As Byte = sha384Hasher.ComputeHash(hashedBytes)
        Dim txtencrypted As String
        txtencrypted = Convert.ToBase64String(encryptedData)
        Return txtencrypted
    End Function

    Public Function EncryptedSHA512(ByVal txtdata As String) As String
        Dim sha512Hasher As New SHA512Managed
        Dim hashedBytes() As Byte = Encoding.ASCII.GetBytes(txtdata)
        Dim encryptedData() As Byte = sha512Hasher.ComputeHash(hashedBytes)
        Dim txtencrypted As String
        txtencrypted = Convert.ToBase64String(encryptedData)
        Return txtencrypted
    End Function

#End Region

End Class
