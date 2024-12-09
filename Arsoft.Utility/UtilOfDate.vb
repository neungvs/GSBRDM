Imports Microsoft.VisualBasic
Imports System.Globalization

Public Class UtilOfDate

#Region "Attributes"
    Private Shared MonthThai() As String = {"มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"}
#End Region

#Region "Methods"

    Public Enum DateFormat
        DateDMY = 1
        DateMDY = 2
        DateYMD = 3
        StringDMY = 4
        StringYMD = 5
        StringMY = 6
        StringYM = 7
    End Enum

    Public Enum YearFormat
        Buddhist = 1
        Christian = 2 'Defuat
    End Enum

    Public Shared Function ConvertCurrentLongDateThai() As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As String
        _dd = Format(Now.Day, "00")
        _mm = Format(Now.Month, "00")
        _yy = Now.Year
        If _yy < 2400 Then
            _yy = _yy + 543
        End If
        _date = _dd & "  " & MonthThai(_mm - 1) & "  " & _yy
        Return _date
    End Function

    Public Shared Function ConvertLongDateThai(ByVal _dt As String) As String
        Dim _date As String
        Dim _dd As Integer
        Dim _mm As Integer
        Dim _yy As Integer
        _dd = CInt(Right(_dt, 2))
        _mm = CInt(Mid(_dt, 5, 2))
        _yy = CInt(Left(_dt, 4))
        If _yy < 2400 Then
            _yy = _yy + 543
        End If
        _date = _dd & "  " & MonthThai(_mm - 1) & "  " & _yy
        Return _date
    End Function

    Public Shared Function ConvertCurrentShortDateThai() As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As String
        _dd = Format(Now.Day, "00")
        _mm = Format(Now.Month, "00")
        _yy = Now.Year
        If _yy < 2400 Then
            _yy = _yy + 543
        End If
        _date = _dd & "/" & _mm & "/" & _yy
        Return _date
    End Function

    Public Shared Function ConvertShortDateThai(ByVal _dt As String) As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As Integer
        _dd = Right(_dt, 2)
        _mm = Mid(_dt, 5, 2)
        _yy = CInt(Left(_dt, 4))
        If _yy < 2400 Then
            _yy = _yy + 543
        End If
        _date = _dd & "/" & _mm & "/" & _yy
        Return _date
    End Function

    Public Shared Function ConvertYearThai(ByVal _dt As String) As String
        Dim _yy As Integer
        _yy = Int(Left(_dt, 4))
        If _yy < 2400 Then
            _yy = _yy + 543
        End If
        Return Str(_yy)
    End Function

    Public Shared Function ConvertDate(ByVal _dt As String, ByVal _display As DateFormat, Optional ByVal _year As YearFormat = YearFormat.Christian) As String
        Dim _date As String = ""
        If Not IsNothing(_dt) Then
            If _dt.Trim.Length > 0 Then
                Dim _dd As String
                Dim _mm As String
                Dim _yy As String
                Dim _data() As String
                _data = Split(_dt, "/")
                If _data.Length > 1 Then
                    _dd = _data(0)
                    _mm = _data(1)
                    _yy = _data(2)
                Else
                    _dd = Right(_dt, 2)
                    _mm = Mid(_dt, 5, 2)
                    _yy = Left(_dt, 4)
                End If

                If _dd.Trim.Length = 1 Then
                    _dd = "0" & _dd.Trim
                End If

                If _mm.Trim.Length = 1 Then
                    _mm = "0" & _mm.Trim
                End If

                Select Case _year
                    Case YearFormat.Buddhist
                        If CInt(_yy) < 2400 Then
                            _yy = CInt(_yy) + 543
                        End If
                    Case YearFormat.Christian
                        If CInt(_yy) > 2400 Then
                            _yy = CInt(_yy) - 543
                        End If
                End Select

                Select Case _display
                    Case DateFormat.DateDMY
                        _date = _dd & "/" & _mm & "/" & _yy
                    Case DateFormat.DateMDY
                        _date = _mm & "/" & _dd & "/" & _yy
                    Case DateFormat.DateYMD
                        _date = _yy & "/" & _mm & "/" & _dd
                    Case DateFormat.StringDMY
                        _date = _dd & _mm & _yy
                    Case DateFormat.StringYMD
                        _date = _yy & _mm & _dd
                    Case DateFormat.StringMY
                        _date = _mm & _yy
                    Case DateFormat.StringYM
                        _date = _yy & _mm
                End Select
            End If
        End If
        Return _date
    End Function

    Public Shared Function ConvertDate(ByVal _dt As DateTime) As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As String
        _dd = Format(_dt.Day, "00")
        _mm = Format(_dt.Month, "00")
        _yy = _dt.Year
        _date = _yy & _mm & _dd
        Return _date
    End Function

    Public Shared Function ConvertDate(_dt As DateTime, _format As String) As String
        Dim _result As String = ""
        Dim _culture = New CultureInfo("en-US")
        _result = _dt.ToString(_format, _culture)
        Return _result
    End Function

    Public Shared Function CurrentDate() As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As String
        _dd = Format(Now.Day, "00")
        _mm = Format(Now.Month, "00")
        _yy = Now.Year
        _date = _dd & "/" & _mm & "/" & _yy
        Return _date
    End Function

    Public Shared Function CurrentDateTime() As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As String
        _dd = Format(Now.Day, "00")
        _mm = Format(Now.Month, "00")
        _yy = Now.Year
        _date = _dd & "/" & _mm & "/" & _yy & " " & Format(Now, "HH:mm:ss")
        Return _date
    End Function

    Public Shared Function ConvertCurrentDate() As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As String
        _dd = Format(Now.Day, "00")
        _mm = Format(Now.Month, "00")
        _yy = Now.Year
        If _yy > 2400 Then
            _yy = _yy - 543
        End If
        _date = _yy & _mm & _dd
        Return _date
    End Function

    Public Shared Function ConvertCurrentDateTime() As String
        Dim _date As String
        Dim _dd As String
        Dim _mm As String
        Dim _yy As String
        _dd = Format(Now.Day, "00")
        _mm = Format(Now.Month, "00")
        _yy = Now.Year
        _date = _yy & _mm & _dd & Format(Now, "HHmmss")
        Return _date
    End Function

    Public Shared Function MonthEnglishShortName(ByVal MonthNumber As Integer) As String
        Select Case MonthNumber
            Case 1
                Return "Jan"
            Case 2
                Return "Feb"
            Case 3
                Return "Mar"
            Case 4
                Return "Apr"
            Case 5
                Return "May"
            Case 6
                Return "Jun"
            Case 7
                Return "Jul"
            Case 8
                Return "Aug"
            Case 9
                Return "Sep"
            Case 10
                Return "Oct"
            Case 11
                Return "Nov"
            Case 12
                Return "Dec"
        End Select

        Return ""
    End Function

    Public Shared Function MonthThaiName(ByVal MonthNumber As Integer) As String
        Select Case MonthNumber
            Case 1
                Return "มกราคม"
            Case 2
                Return "กุมภาพันธ์"
            Case 3
                Return "มีนาคม"
            Case 4
                Return "เมษายน"
            Case 5
                Return "พฤษภาคม"
            Case 6
                Return "มิถุนายน"
            Case 7
                Return "กรกฎาคม"
            Case 8
                Return "สิงหาคม"
            Case 9
                Return "กันยายน"
            Case 10
                Return "ตุลาคม"
            Case 11
                Return "พฤศจิกายน"
            Case 12
                Return "ธันวาคม"
        End Select

        Return ""
    End Function

    Public Shared Function LastDayOfMonth(dt As DateTime) As DateTime
        Dim firstDayOfTheMonth As DateTime = New DateTime(dt.Year, dt.Month, 1)
        Return firstDayOfTheMonth.AddMonths(1).AddDays(-1)
    End Function

    Public Shared Function CheckDate(_dt As String) As Boolean
        Dim _result As Boolean = False

        If Not IsNothing(_dt) Then
            If _dt.Trim.Length > 0 Then
                Dim _dd As String
                Dim _mm As String
                Dim _yy As String
                Dim _data() As String
                _data = Split(_dt, "/")
                If _data.Length > 1 Then
                    _dd = _data(0)
                    _mm = _data(1)
                    _yy = _data(2)
                Else
                    _dd = Right(_dt, 2)
                    _mm = Mid(_dt, 5, 2)
                    _yy = Left(_dt, 4)
                End If

                If _dd.Trim.Length = 1 Then
                    _dd = "0" & _dd.Trim
                End If

                If _mm.Trim.Length = 1 Then
                    _mm = "0" & _mm.Trim
                End If

                'Check Leap Year
                If CInt(_yy) > 2400 Then
                    _yy = CInt(_yy) - 543
                End If

                _dt = _yy & "/" & _mm & "/" & _dd
                If IsDate(_dt) Then
                    _result = True
                Else
                    If DateTime.IsLeapYear(CInt(_yy)) Then
                        If CInt(_mm) = 2 Then
                            If CInt(_dd) < 30 Then
                                _result = True
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Return _result

    End Function
#End Region

End Class
