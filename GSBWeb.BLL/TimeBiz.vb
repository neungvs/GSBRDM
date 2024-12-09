Imports GSBWeb.DAL

Public Class TimeBiz
    Dim _timeAcc As New TimeAccess
    Dim _dateBiz As New DateHelperUtil

    Public Function GetDate() As List(Of TimeEntity)
        Dim _result As List(Of TimeEntity)
        _result = _timeAcc.GetDate().OrderByDescending(Function(x) x.TimeId).ToList()
        Return _result
    End Function

    'Public Function GetDateCreateNewV2() As List(Of TimeEntity)
    '    Dim _result As List(Of TimeEntity)
    '    Dim listTime As New List(Of TimeEntity)
    '    Dim currentYear As Integer = DateTime.Now.Year - 4
    '    Dim currentMonth As Integer = DateTime.Now.Month
    '    Dim _time As TimeEntity

    '    ' Loop for the next 5 years
    '    For year As Integer = currentYear To currentYear + 4
    '        ' Loop through the months of each year
    '        For month As Integer = 1 To 12
    '            ' Display the year and month
    '            _time = New TimeEntity
    '            With _time
    '                Dim timeId As String = _dateBiz.GetLastDayOfMonth(month, year)
    '                'Dim timeId As String = year.ToString() + Right("0" + month.ToString(), 2)
    '                Dim timeName As String = GetMonthThaiName(month) & " " & (year + 543).ToString()
    '                .TimeId = timeId
    '                .TimeName = timeName
    '            End With
    '            listTime.Add(_time)
    '        Next
    '    Next
    '    _result = listTime
    '    Return _result.OrderByDescending(Function(x) x.TimeId).ToList()
    'End Function

    Public Function GetDateCreateNew() As List(Of TimeEntity)
        Dim _result As List(Of TimeEntity)
        Dim listTime As New List(Of TimeEntity)
        Dim currentYear As Integer = DateTime.Now.Year - 4
        Dim currentMonth As Integer = DateTime.Now.Month
        Dim _time As TimeEntity

        ' Loop for the next 5 years
        For year As Integer = currentYear To currentYear + 4
            ' Loop through the months of each year
            For month As Integer = 1 To 12
                ' Display the year and month
                _time = New TimeEntity
                With _time
                    Dim timeId As String = _dateBiz.GetLastDayOfMonth(month, year)
                    'Dim timeId As String = year.ToString() + Right("0" + month.ToString(), 2)
                    Dim timeName As String = GetMonthThaiName(month) & " " & (year + 543).ToString()
                    .TimeId = timeId
                    .TimeName = timeName
                End With
                listTime.Add(_time)
            Next
        Next
        _result = listTime
        Return _result.OrderByDescending(Function(x) x.TimeId).ToList()
        'Dim _result As List(Of TimeEntity)
        'Dim listTime As New List(Of TimeEntity)
        'Dim currentYear As Integer = DateTime.Now.Year - 4
        'Dim currentMonth As Integer = DateTime.Now.Month
        'Dim _time As TimeEntity

        '' Loop for the next 5 years
        'For year As Integer = currentYear To currentYear + 4
        '    ' Loop through the months of each year
        '    For month As Integer = 1 To 12
        '        ' Display the year and month
        '        _time = New TimeEntity
        '        With _time
        '            Dim timeId As String = year.ToString() + Right("0" + month.ToString(), 2)
        '            Dim timeName As String = GetMonthThaiName(month) & " " & (year + 543).ToString()
        '            .TimeId = timeId
        '            .TimeName = timeName
        '        End With
        '        listTime.Add(_time)
        '    Next
        'Next
        '_result = listTime
        'Return _result.OrderByDescending(Function(x) x.TimeId).ToList()
    End Function

    Public Function GetMonthThaiName(month As Integer) As String
        ' Array of Thai month names
        Dim thaiMonths As String() = {
            "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน",
            "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม",
            "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"
        }
        Return thaiMonths(month - 1)
    End Function

    Public Function GetScenario(timeId As String) As List(Of ScenarioEntity)
        Dim _result As List(Of ScenarioEntity)
        _result = _timeAcc.GetScenarioByTimeId(timeId)
        Return _result
    End Function

End Class