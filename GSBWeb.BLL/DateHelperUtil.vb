Public Class DateHelperUtil

    Public Function GetLastDayOfMonth(timeId As String) As String
        Dim _year As String = timeId.Substring(0, 4)
        Dim _month As String = timeId.Substring(4, 2)
        timeId = GetLastDayOfMonth(_month, _year)
        Return timeId
    End Function

    Public Function GetLastDayOfMonth(month As String, year As String) As String
        ' Get the last day of the month using DaysInMonth method
        Dim _year As Integer = Convert.ToInt16(year)
        Dim _month As Integer = Convert.ToInt16(month)
        Dim _lastDayOfMonth As Integer = DateTime.DaysInMonth(_year, _month)
        Dim _dateStr As String = Right("0" & _lastDayOfMonth, 2)
        Dim _monthStr As String = Right("0" & _month, 2)
        Return _year.ToString() & _monthStr & _dateStr
    End Function


    Public Function GetMonthIdByMonthName(monthName As String) As String
        Dim thaiMonths As New Dictionary(Of String, Integer) From {
                        {"มกราคม", 1},
                        {"กุมภาพันธ์", 2},
                        {"มีนาคม", 3},
                        {"เมษายน", 4},
                        {"พฤษภาคม", 5},
                        {"มิถุนายน", 6},
                        {"กรกฎาคม", 7},
                        {"สิงหาคม", 8},
                        {"กันยายน", 9},
                        {"ตุลาคม", 10},
                        {"พฤศจิกายน", 11},
                        {"ธันวาคม", 12}
               }
        Dim monthID As Integer = thaiMonths(monthName)
        Return monthID.ToString()
    End Function

End Class
