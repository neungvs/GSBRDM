Imports GSBWeb.DAL
Imports System.Web.Configuration
Imports Microsoft.Reporting.WebForms
Imports System.Web.HttpApplicationState
Imports System.Web.HttpServerUtility
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Globalization
Imports System.Threading
Imports System.Data
Imports Arsoft.Utility


Public Class IndustryBiz

    Dim _Industryacc As New IndustryAccess

    Public Sub New()
    End Sub


#Region "ISICCODE"

    Public Function GetDataIndustyLimit() As DataTable
        Dim _result As DataTable
        _result = _Industryacc.GetDataIndustyLimit()
        Return _result
    End Function

    Public Function GetSectorLimit(ByVal _effectiveDate As String) As List(Of IndustyDetailEntity)
        Dim _result As List(Of IndustyDetailEntity)
        _result = _Industryacc.GetSectorLimit(_effectiveDate)
        Return _result
    End Function

    Public Function DeleteByEffective(ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean = False
        _result = _Industryacc.DeleteByEffective(_effectiveDate)
        Return _result
    End Function

    Public Function GetSectorLimitCriteria(ByVal _effectiveDate As String, ByVal _isicCode As String, ByVal _isicSubCode As String) As List(Of IndustyDetailEntity)
        Dim _result As List(Of IndustyDetailEntity)
        _result = _Industryacc.GetSectorLimitCriteria(_effectiveDate, _isicCode, _isicSubCode)
        Return _result
    End Function

    Public Function GetHeadder(ByVal _effectiveDate As String) As List(Of IndustyHeaderEntity)
        Dim _result As List(Of IndustyHeaderEntity)
        _result = _Industryacc.GetHeadder(_effectiveDate)
        Return _result
    End Function

    Public Function UpdateHeadder(ByVal _effectiveDate As String, ByVal _entHeader As IndustyHeaderEntity, ByVal _newEffectiveDate As String)
        _Industryacc.UpdateHeadder(_effectiveDate, _entHeader, _newEffectiveDate)
    End Function

    Public Function DeleteDetail(ByVal _secID As String)
        _Industryacc.DeleteDetail(_secID)
    End Function

    Public Function GetEditData(ByVal _secID As String) As IndustyDetailEntity
        Dim _result As IndustyDetailEntity
        _result = _Industryacc.GetEditData(_secID)
        Return _result
    End Function

    Public Function UpdateDetail(ByVal _secID As String, ByVal _inPercent As Double?, ByVal _inAmount As Double?)
        _Industryacc.UpdateDetail(_secID, _inPercent, _inAmount)
    End Function

    Public Function InsertDetail(ByVal entDetail As IndustyLimitDetailEntity) As Boolean
        Dim _result As Boolean
        _result = _Industryacc.InsertDetail(entDetail)
        Return _result
    End Function

    Public Function AddNewIndustryLimit(ByVal _entHeader As IndustyHeaderEntity, ByVal _entDetail As List(Of IndustyDetailEntity))
        _Industryacc.AddNewIndustryLimit(_entHeader, _entDetail)
    End Function

    Public Function AddNewHeaderIndustryLimit(ByVal _entHeader As IndustyHeaderEntity) As Boolean
        Dim _result As Boolean
        _result = _Industryacc.AddNewHeaderIndustryLimit(_entHeader)
        Return _result
    End Function

    Public Function AddDetailIndustryLimit(ByVal _entDetail As List(Of IndustyLimitDetailEntity)) As Boolean
        Dim _result As Boolean
        _result = _Industryacc.AddDetailIndustryLimit(_entDetail)
        Return _result
    End Function

    Public Function LoadDDL_ISICCODE() As DataTable
        Dim _result As DataTable
        _result = _Industryacc.LoadDDL_ISICCODE()
        Return _result
    End Function

    Public Function LoadDDL_ISICCODESUBLEVEL(ByVal _ISICCODE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.LoadDDL_ISICCODESUBLEVEL(_ISICCODE)
        Return _result
    End Function

    Public Function Check_HeightLevel(ByVal _CODE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.Check_HeightLevel(_CODE)
        Return _result
    End Function

    Public Function Load_Industry(ByVal _CODE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.Load_Industry(_CODE)
        Return _result
    End Function

    Public Function CheckISIC(ByVal _ISICODE As String, ByVal _ISICSUB As String, ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean
        _result = _Industryacc.CheckISIC(_ISICODE, _ISICSUB, _effectiveDate)
        Return _result
    End Function

    Public Function CheckLnCode(ByVal _LNCODE As String, ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean
        _result = _Industryacc.CheckLnCode(_LNCODE, _effectiveDate)
        Return _result
    End Function

    Public Function CheckEffectiveDate(ByVal _effectiveDate As String) As Boolean
        Dim _result As Boolean
        _result = _Industryacc.CheckEffectiveDate(_effectiveDate)
        Return _result
    End Function

    Public Function GetLnmktcode(ByVal _prefix As String) As String()
        Dim _result As String()
        _result = _Industryacc.GetLnmktcode(_prefix)
        Return _result
    End Function

    Public Function GetLnmktcodeBySearch(ByVal _txtSearch As String) As String
        Dim _result As String
        _result = _Industryacc.GetLnmktcodeBySearch(_txtSearch)
        Return _result
    End Function

#End Region


#Region "LOANTYPE"

    Public Function Load_Lntypecode(ByVal _LNCODE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.Load_Lntypecode(_LNCODE)
        Return _result
    End Function

    Public Function LoadDDL_LNTYPECODE() As DataTable
        Dim _result As DataTable
        _result = _Industryacc.LoadDDL_LNTYPECODE()
        Return _result
    End Function

    Public Function LoadDDL_LNSUBTYPE() As DataTable
        Dim _result As DataTable
        _result = _Industryacc.LoadDDL_LNSUBTYPE()
        Return _result
    End Function

    Public Function GetLnsubtypeByLntypecode(ByVal _LNTYPECODE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.GetLnsubtypeByLntypecode(_LNTYPECODE)
        Return _result
    End Function

    Public Function Load_Lnsubtype(ByVal _LNSUB As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.Load_Lnsubtype(_LNSUB)
        Return _result
    End Function

    Public Function LoadDDL_LNMKTCODE() As DataTable
        Dim _result As DataTable
        _result = _Industryacc.LoadDDL_LNMKTCODE()
        Return _result
    End Function

    Public Function Load_Lnmktcode(ByVal _MKCODE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.Load_Lnmktcode(_MKCODE)
        Return _result
    End Function

    Public Function GetLnmktcodeByLnsubtype(ByVal _LNSUBTYPE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.GetLnmktcodeByLnsubtype(_LNSUBTYPE)
        Return _result
    End Function

    Public Function GetLnmktcodeByLntypecode(ByVal _LNTYPECODE As String) As DataTable
        Dim _result As DataTable
        _result = _Industryacc.GetLnmktcodeByLntypecode(_LNTYPECODE)
        Return _result
    End Function

#End Region

End Class
