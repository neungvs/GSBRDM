Imports System.Web.UI.WebControls


Public Class TimeSearch

    Public Sub New()

    End Sub

    Public Function GetYear() As ListItemCollection

        Dim yCollection As New ListItemCollection

        For i As Integer = 1 To 6

            Dim y As New ListItem

            If Year(Now) < 2500 Then
                y.Value = Year(Now) + 543 - i + 1
            Else
                y.Value = Year(Now) - i + 1
            End If

            yCollection.Add(y)

        Next

        Return yCollection

    End Function

    Public Function GetMonth() As ListItemCollection

        Dim mCollection As New ListItemCollection

        Dim m1 As New ListItem
        m1.Value = "มกราคม"
        mCollection.Add(m1)
        Dim m2 As New ListItem
        m2.Value = "กุมภาพันธ์"
        mCollection.Add(m2)
        Dim m3 As New ListItem
        m3.Value = "มีนาคม"
        mCollection.Add(m3)
        Dim m4 As New ListItem
        m4.Value = "เมษายน"
        mCollection.Add(m4)
        Dim m5 As New ListItem
        m5.Value = "พฤษภาคม"
        mCollection.Add(m5)
        Dim m6 As New ListItem
        m6.Value = "มิถุนายน"
        mCollection.Add(m6)
        Dim m7 As New ListItem
        m7.Value = "กรกฎาคม"
        mCollection.Add(m7)
        Dim m8 As New ListItem
        m8.Value = "สิงหาคม"
        mCollection.Add(m8)
        Dim m9 As New ListItem
        m9.Value = "กันยายน"
        mCollection.Add(m9)
        Dim m10 As New ListItem
        m10.Value = "ตุลาคม"
        mCollection.Add(m10)
        Dim m11 As New ListItem
        m11.Value = "พฤศจิกายน"
        mCollection.Add(m11)
        Dim m12 As New ListItem
        m12.Value = "ธันวาคม"
        mCollection.Add(m12)

        'For i As Integer = 1 To 12
        '    Dim m As New ListItem
        '    m.Value = MonthName(i)
        '    mCollection.Add(m)
        'Next

        Return mCollection

    End Function

    Public Function GetTimeID(ByVal y As String, ByVal m As String) As String

        Dim intYear As Integer
        intYear = Convert.ToInt16(y) - 543


        Return intYear.ToString + m + Format(Date.DaysInMonth(y, m), "00")

    End Function

End Class
