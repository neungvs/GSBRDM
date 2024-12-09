Imports GSBWeb.DAL

Public Class ValidateBiz

    Public Function CheckValidCustomerRatingByDataType(checkDataType As String, rawData As String, Optional lstScenario As List(Of ScenarioEntity) = Nothing)
        Dim retData As New List(Of String)()

        'ค่า Time
        If (checkDataType = "Time") Then
            If (rawData = "") Then
                retData.Add("วันที่ของข้อมูล ห้ามว่าง")
            ElseIf (IsValidNumber(rawData) = False) Then
                retData.Add("วันที่ของข้อมูล ต้องเป็นตัวเลขเท่านั้น")
            End If
        End If

        'ค่า CIFNumber
        If (checkDataType = "CIFNumber") Then
            If (rawData = "") Then
                retData.Add("ค่า CIFNumber ห้ามว่าง")
            End If
        End If

        'สถานการณ์ภาวะวิกฤต = กรอกได้ทั้ง text,ตัวเลข และต้อง Valid ตาม list Scenario
        If (checkDataType = "Scenario") Then
            If (rawData = "") Then
                retData.Add("สถานการณ์ภาวะวิกฤต ห้ามว่าง")
            ElseIf (IsValidScenario(rawData, lstScenario) = False) Then
                retData.Add("ข้อมูลสถานการณ์ภาวะวิกฤต ไม่ถูกต้อง")
            End If
        End If

        'ปีที่ทดสอบภาวะวิกฤต = กรอกพ.ศ.เท่านั้น
        If (checkDataType = "Year") Then
            If (rawData = "") Then
                retData.Add("ปีที่ทดสอบภาวะวิกฤต ห้ามว่าง")
            ElseIf (IsValidNumber(rawData) = False) Then
                retData.Add("ปีที่ทดสอบภาวะวิกฤต ต้องเป็นตัวเลขเท่านั้น")
            ElseIf (IsValidBuddhistYear(rawData) = False) Then
                retData.Add("ปีที่ทดสอบภาวะวิกฤต กรอกพ.ศ.เท่านั้น")
            End If
        End If

        'OldPdSegment
        If (checkDataType = "OldPdSegment") Then
            If (rawData = "") Then
                retData.Add("pd_segment เก่า ห้ามว่าง")
            End If
        End If

        'NewPdSegment
        If (checkDataType = "NewPdSegment") Then
            If (rawData = "") Then
                retData.Add("pd_segment ใหม่ ห้ามว่าง")
            End If
        End If

        Return retData
    End Function

    Public Function IsListOfModelNullOrEmpty(Of T)(modelList As List(Of T)) As Boolean
        ' Check if the list is Nothing or has no items
        If modelList Is Nothing OrElse modelList.Count = 0 Then
            Return True ' List is either Nothing or empty
        End If
        Return False ' List exists and has items
    End Function

    Public Function CheckValidByDataType(checkDataType As String, rawData As String,
            Optional lstScenario As List(Of ScenarioEntity) = Nothing,
            Optional lstFactor As List(Of FactorEntity) = Nothing,
            Optional lstTime As List(Of TimeEntity) = Nothing)
        Dim retData As New List(Of String)()

        'ค่า Time
        If (checkDataType = "Time") Then
            If (rawData = "") Then
                retData.Add("วันที่ของข้อมูล ห้ามว่าง")
            ElseIf (IsValidNumber(rawData) = False) Then
                retData.Add("วันที่ของข้อมูล ต้องเป็นตัวเลขเท่านั้น")
            ElseIf (IsListOfModelNullOrEmpty(lstTime) = False AndAlso IsValidTime(rawData, lstTime) = False) Then
                retData.Add("วันที่ของข้อมูล ไม่ถูกต้อง")
            End If
        End If

        'ปีที่ทดสอบภาวะวิกฤต = กรอกพ.ศ.เท่านั้น
        If (checkDataType = "Year") Then
            If (rawData = "") Then
                retData.Add("ปีที่ทดสอบภาวะวิกฤต ห้ามว่าง")
            ElseIf (IsValidNumber(rawData) = False) Then
                retData.Add("ปีที่ทดสอบภาวะวิกฤต ต้องเป็นตัวเลขเท่านั้น")
            ElseIf (IsValidBuddhistYear(rawData) = False) Then
                retData.Add("ปีที่ทดสอบภาวะวิกฤต กรอกพ.ศ.เท่านั้น")
            End If
        End If

        'Stress Year
        If (checkDataType = "StressYear") Then
            If (rawData = "") Then
                retData.Add("Stress Year ห้ามว่าง")
            ElseIf (IsValidNumber(rawData) = False) Then
                retData.Add("Stress Year ต้องเป็นตัวเลขเท่านั้น")
            End If
        End If

        'Stress Month
        If (checkDataType = "StressMonth") Then
            If (rawData = "") Then
                retData.Add("Stress Month ห้ามว่าง")
            ElseIf (IsValidMonthName(rawData) = False) Then
                '    retData.Add("Stress Monthต้องเป็นตัวเลขเท่านั้น")
                'ElseIf (IsValidMonth(rawData) = False) Then
                retData.Add("Stress Month ไม่ถูกต้อง")
            End If
        End If

        'สถานการณ์ภาวะวิกฤต = กรอกได้ทั้ง text,ตัวเลข และต้อง Valid ตาม list Scenario
        If (checkDataType = "Scenario") Then
            If (rawData = "") Then
                retData.Add("สถานการณ์ภาวะวิกฤต ห้ามว่าง")
            ElseIf (IsValidScenario(rawData, lstScenario) = False) Then
                retData.Add("ข้อมูลสถานการณ์ภาวะวิกฤต ไม่ถูกต้อง")
            End If
        End If

        'LGD Scalar = รับได้ทั้งค่า -, +, 0, ทศนิยม15ตำแหน่ง
        If (checkDataType = "StressLgdScalar") Then
            If (rawData = "") Then
                retData.Add("ค่า LGD Scalar ห้ามว่าง")
            ElseIf (IsValidNumber(rawData) = False) Then
                retData.Add("ค่า LGD Scalar ต้องเป็นตัวเลขเท่านั้น")
            End If
        End If

        'PdSegment
        If (checkDataType = "PdSegment") Then
            If (rawData = "") Then
                retData.Add("ค่า PdSegment ห้ามว่าง")
            End If
        End If

        'LoanGrowth
        If (checkDataType = "LoanGrowth") Then
            If (rawData <> "" And IsValidNumber(rawData) = False) Then
                retData.Add("ค่า LoanGrowth ต้องเป็นตัวเลขเท่านั้น")
            End If
        End If

        'WriteOff
        If (checkDataType = "WriteOff") Then
            If (rawData <> "" And IsValidNumber(rawData) = False) Then
                retData.Add("ค่า WriteOff ต้องเป็นตัวเลขเท่านั้น")
            End If
        End If

        'Factor Name
        If (checkDataType = "FactorName") Then
            If (rawData = "") Then
                retData.Add("ชื่อตัวแปรทางเศรษฐกิจ ห้ามว่าง")
            ElseIf (IsValidFactor(rawData, lstFactor) = False) Then
                retData.Add("ชื่อตัวแปรทางเศรษฐกิจ ไม่ถูกต้อง")
            End If
        End If

        'Factor Value
        If (checkDataType = "FactorValue") Then
            If (rawData <> "" And IsValidNumber(rawData) = False) Then
                retData.Add("ค่า Fartor Value ต้องเป็นตัวเลขเท่านั้น")
            End If
        End If

        'MainMeasure
        If (checkDataType = "MainMeasure") Then
            If (rawData = "") Then
                retData.Add("ชื่อมาตรการหลัก ห้ามว่าง")
            End If
        End If

        'SubMeasure
        If (checkDataType = "SubMeasure") Then
            If (rawData = "") Then
                retData.Add("ชื่อมาตรการย่อย ห้ามว่าง")
            End If
        End If

        'AccountNumber
        If (checkDataType = "AccountNumber") Then
            If (rawData = "") Then
                retData.Add("ชื่อเลขที่บัญชี ห้ามว่าง")
            End If
        End If

        Return retData
    End Function

    'Private Function CheckValidScenario(checkDataType As String, row As DataRow) As List(Of String)
    '    Dim Time As String = row("วันที่ของข้อมูล").ToString()
    '    Dim Year As String = row("ปีที่ทดสอบภาวะวิกฤต").ToString()
    '    Dim Scenario As String = row("สถานการณ์ภาวะวิกฤต").ToString()
    '    Dim StressLgdScalar As String = row("ค่า LGD Scalar").ToString()
    '    Dim retData As New List(Of String)()

    '    'ค่า Time
    '    If (Time = "") Then
    '        retData.Add("วันที่ของข้อมูล ห้ามว่าง")
    '    ElseIf (IsValidNumber(Time) = False) Then
    '        retData.Add("วันที่ของข้อมูล ต้องเป็นตัวเลขเท่านั้น")
    '    End If

    '    'ปีที่ทดสอบภาวะวิกฤต = กรอกพ.ศ.เท่านั้น
    '    If (Year = "") Then
    '        retData.Add("ปีที่ทดสอบภาวะวิกฤต ห้ามว่าง")
    '    ElseIf (IsValidNumber(Year) = False) Then
    '        retData.Add("ปีที่ทดสอบภาวะวิกฤต ต้องเป็นตัวเลขเท่านั้น")
    '    ElseIf (IsValidBuddhistYear(Year) = False) Then
    '        retData.Add("ปีที่ทดสอบภาวะวิกฤต กรอกพ.ศ.เท่านั้น")
    '    End If

    '    'สถานการณ์ภาวะวิกฤต = กรอกได้ทั้ง text,ตัวเลข และต้อง Valid ตาม list Scenario
    '    If (Scenario = "") Then
    '        retData.Add("สถานการณ์ภาวะวิกฤต ห้ามว่าง")
    '    ElseIf (IsValidScenario(Scenario, lstScenario) = False) Then
    '        retData.Add("ข้อมูลสถานการณ์ภาวะวิกฤต ไม่ถูกต้อง")
    '    End If

    '    'ค่า LGD Scalar = รับได้ทั้งค่า -, +, 0, ทศนิยม15ตำแหน่ง
    '    If (StressLgdScalar = "") Then
    '        retData.Add("ค่า LGD Scalar ห้ามว่าง")
    '    ElseIf (IsValidNumber(StressLgdScalar) = False) Then
    '        retData.Add("ค่า LGD Scalar ต้องเป็นตัวเลขเท่านั้น")
    '    End If

    '    Return retData
    'End Function
    Function IsValidBuddhistYear(year As String) As Boolean
        year = Convert.ToInt16(year)
        Return year >= 2500
    End Function

    Function IsValidMonthName(month As String) As Boolean
        Dim thaiMonths As String() = {
            "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน",
            "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม",
            "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"
        }
        If thaiMonths.Contains(month) Then
            Return True
        End If
        Return False
    End Function

    Function IsValidMonth(month As String) As Boolean
        month = Convert.ToInt16(month)
        Return month >= 1 And month <= 12
    End Function

    Function IsValidScenario(Scenario As String, lstScenario As List(Of ScenarioEntity)) As Boolean
        For Each _senario As ScenarioEntity In lstScenario
            If (Scenario = _senario.ScenarioName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function IsValidFactor(factorName As String, lstFactor As List(Of FactorEntity)) As Boolean
        For Each _factor As FactorEntity In lstFactor
            If (factorName = _factor.FactorName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function IsValidTime(timeId As String, lstTime As List(Of TimeEntity)) As Boolean
        If (Len(timeId) = 6) Then
            Dim _dateBiz As New DateHelperUtil
            Dim _year As String = timeId.Substring(0, 4)
            Dim _month As String = timeId.Substring(4, 2)
            timeId = _dateBiz.GetLastDayOfMonth(_month, _year)
        End If
        For Each _time As TimeEntity In lstTime
            If (timeId = _time.TimeId) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function IsValidNumber(value As String) As Boolean
        Return IsNumeric(value)
    End Function
End Class
