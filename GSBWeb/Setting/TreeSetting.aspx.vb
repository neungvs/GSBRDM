Imports GSBWeb.DAL
Imports GSBWeb.BLL
Imports Arsoft.Utility
Imports System.Web
Public Class TreeSetting
    Inherits System.Web.UI.Page
    Private _UMB As New UserModuleBiz
    Dim button_click As Boolean
    Dim MessageBox_Result As Integer = -1
    Dim flag As Integer = 0
    Dim _command As Integer = -1
    Dim _userentityforlog As UserEntity

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            LoadGroup()
            Tv_AllData.Attributes.Add("onclick", "SetPostback()")
        End If
        If IsPostBack Then
            If Request.Cookies("SetFocusData") Is Nothing Then
            Else
                Dim a As String = Request.Cookies("SetFocusData").Value
                Response.Cookies("SetFocusData").Expires = DateTime.Now.AddDays(-1)
                If a = 1 Then

                    lb_setfocus.Focus()
                End If
            End If
        End If
        If flag = 0 Then
            If Request.Cookies.Count > 0 Then
                Dim cookiedata As String
                If Request.Cookies("finishdataRDM_Web") Is Nothing Then
                Else
                    cookiedata = ConvertBase64ToText(Request.Cookies("finishdataRDM_Web").Value)

                    If cookiedata.IndexOf("สำเร็จ") > -1 Then
                        MessageBoxAlert("Success", cookiedata, "", "ปิด", False, True)
                        flag = 1
                        Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    Else
                        MessageBoxAlert("Error", cookiedata, "", "ปิด", False, True)
                        flag = 1
                        Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(-1)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub LoadGroup()
        Dim _lsModule As List(Of UserEntity)
        Dim _html As New StringBuilder
        _lsModule = _UMB.GetWorkgroup("u")
        'Dim index As Integer
        'For Each modules In _lsModule
        '    cb_listgroup.Items.Add(modules.GroupID & "-" & modules.GroupName_TH)
        '    cb_listgroup.Items(index).Value = modules.GroupID
        '    index += 1
        'Next
        'cb_listgroup.DataBind()

        With cb_listgroup
            .DataSource = _lsModule
            .DataValueField = "GroupID"
            .DataTextField = "GroupName_TH"
            .DataBind()
        End With
        Create_Tree()
        cb_listgroup.Focus()
    End Sub
    Private Sub Create_Tree()
        Dim _lsModule As List(Of ModuleEntity)
        _lsModule = _UMB.SearchGroup()
        Dim node As TreeNode
        Dim hf As String
        Tv_AllData.Nodes.Clear()
        For Each row In _lsModule
            If row.LevelID = 0 Then
                '"<div style='background-color:#FF388C;color:#FFF;display:inline;'>" &"</div>"&
                node = New TreeNode("<b>" & row.ModuleNameEN.ToString() & "</b>", row.ModuleID.ToString())
                Tv_AllData.Nodes.Add(node)

            ElseIf row.LevelID = 1 Then
                node = New TreeNode("   " & row.ModuleNameTH.ToString(), row.ModuleID.ToString())
                For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
                    If Tv_AllData.Nodes.Item(i).Value.ToString() = row.ParentID.ToString() Then
                        Tv_AllData.Nodes.Item(i).ChildNodes.Add(node)
                        Exit For
                    End If
                Next
            ElseIf row.LevelID = 2 Then
                node = New TreeNode("   " & row.ModuleNameTH.ToString(), row.ModuleID.ToString())
                'hf = row.ModuleID.Substring(0, 1)
                For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
                    'If Tv_AllData.Nodes.Item(i).Value.Substring(0, 1) = hf Then
                    For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                            If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Value = row.ParentID Then
                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Add(node)
                                Exit For
                            End If
                        Next
                    'Exit For
                    'End If
                Next
            ElseIf row.LevelID = 3 Then
                node = New TreeNode("   " & row.ModuleNameTH.ToString(), row.ModuleID.ToString())
                'hf = row.ModuleID.Substring(0, 1)
                For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
                    'If Tv_AllData.Nodes.Item(i).Value.Substring(0, 1) = hf Then
                    'hf = row.ModuleID.Substring(row.ModuleID.Length - 3, 1)
                    For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                            Dim vn2 = Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Value
                            'If vn2.Substring(vn2.Length - 3, 1) = hf Then
                            For k As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                                    If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).Value = row.ParentID Then
                                        Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Add(node)
                                        Exit For
                                    End If
                                Next
                        'Exit For
                        'End If
                    Next
                    'End If
                    'Exit For
                Next
            ElseIf row.LevelID = 4 Then
                node = New TreeNode("   " & row.ModuleNameTH.ToString(), row.ModuleID.ToString())
                'hf = row.ModuleID.Substring(0, 1)
                For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
                    'If Tv_AllData.Nodes.Item(i).Value.Substring(0, 1) = hf Then
                    'hf = row.ModuleID.Substring(row.ModuleID.Length - 3, 1)
                    For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                            Dim vn2 As String = Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Value
                            'If vn2.Substring(vn2.Length - 3, 1) = hf Then
                            'hf = row.ModuleID.Substring(row.ModuleID.Length - 2, 1)
                            For k As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                                    Dim vn3 As String = Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).Value
                                'If vn3.Substring(vn3.Length - 2, 1) = hf Then
                                For l As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Count - 1
                                            Dim vn4 As String = Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).Value
                                            If vn4 = row.ParentID Then
                                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Add(node)
                                                Exit For
                                            End If
                                        Next
                            'Exit For
                            'End If
                        Next
                        'Exit For
                        'End If
                    Next
                    'Exit For
                    'End If
                Next
            ElseIf row.LevelID = 5 Then
               
            ElseIf row.LevelID = 6 Then

            ElseIf row.LevelID = 7 Then

            ElseIf row.LevelID = 8 Then

            ElseIf row.LevelID = 9 Then

            ElseIf row.LevelID = 10 Then

            End If
        Next
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            Tv_AllData.Nodes.Item(i).Expand()
        Next
        Tv_AllData.RootNodeStyle.Font.Bold = True
        Tv_AllData.NodeWrap = True
    End Sub

    Protected Sub btn_Submit_Click(sender As Object, e As EventArgs) Handles btn_Submit.Click
        CheckedAll.Checked = False
        Clear_CheckedNode()
        button_click = True
        Dim groupid As String = cb_listgroup.SelectedItem.Value.ToString()
        Dim groupname As String = cb_listgroup.SelectedItem.Text.ToString()

        Headerform.InnerHtml = _UMB.SelectGroupCodeFromGroupID(Convert.ToInt32(groupid)) + "-" + groupname

        'Dim index As Integer = Headerform.InnerHtml.Length

        'If index > 38 Then

        'Else
        '    Headerform.Attributes.Remove("style")
        'End If
        Dim _lsmodule As New List(Of ModuleEntity)
        _lsmodule = _UMB.GetGroupData(groupid)
        If _lsmodule.Count > 0 Then

            'Checkbox Checked Node0
            For Each _list As ModuleEntity In _lsmodule
                For Each Node0 As TreeNode In Tv_AllData.Nodes
                    If _list.ModuleID = Node0.Value Then
                        Node0.Checked = True
                        Exit For
                    End If
                    If _list.LevelID <> 0 Then
                        Exit For
                    End If
                Next
            Next
            CountCheckNode0()

            'Checkbox Checked Node1
            For Each _list As ModuleEntity In _lsmodule
                For Each Node0 As TreeNode In Tv_AllData.Nodes
                    For Each Node1 As TreeNode In Node0.ChildNodes
                        If _list.ModuleID = Node1.Value Then
                            Node1.Checked = True
                            Exit For
                        End If
                        If _list.LevelID <> 1 Then
                            Exit For
                        End If
                    Next
                    CountCheckNode1(Node0.Value)
                Next
            Next


            'Checkbox Checked Node2
            For Each _list As ModuleEntity In _lsmodule
                For Each Node0 As TreeNode In Tv_AllData.Nodes
                    For Each Node1 As TreeNode In Node0.ChildNodes
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            If _list.LevelID = 2 Then
                                Dim a As String = ""
                            End If
                            If _list.ModuleID = Node2.Value Then
                                Node2.Checked = True
                                Exit For
                            End If
                            If _list.LevelID <> 2 Then
                                Exit For
                            End If
                        Next
                        CountCheckNode2(Node0.Value, Node1.Value)
                    Next
                Next
            Next

            'Checkbox Checked Node3
            For Each _list As ModuleEntity In _lsmodule
                For Each Node0 As TreeNode In Tv_AllData.Nodes
                    For Each Node1 As TreeNode In Node0.ChildNodes
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            For Each Node3 As TreeNode In Node2.ChildNodes
                                If _list.ModuleID = Node3.Value Then
                                    Node3.Checked = True
                                    Exit For
                                End If
                                If _list.LevelID <> 3 Then
                                    Exit For
                                End If
                            Next
                            CountCheckNode3(Node0.Value, Node1.Value, Node2.Value)
                        Next
                    Next
                Next
            Next

            'Checkbox Checked Node4
            For Each _list As ModuleEntity In _lsmodule
                For Each Node0 As TreeNode In Tv_AllData.Nodes
                    For Each Node1 As TreeNode In Node0.ChildNodes
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            For Each Node3 As TreeNode In Node2.ChildNodes
                                For Each Node4 As TreeNode In Node3.ChildNodes
                                    If _list.ModuleID = Node4.Value Then
                                        Node4.Checked = True
                                        Exit For
                                    End If
                                    If _list.LevelID <> 4 Then
                                        Exit For
                                    End If
                                Next
                                CountCheckNode4(Node0.Value, Node1.Value, Node2.Value, Node3.Value)
                            Next
                        Next
                    Next
                Next
            Next

            ''Checkbo Checked Node5
            'For Each _list As ModuleEntity In _lsmodule
            '    For Each Node0 As TreeNode In Tv_AllData.Nodes
            '        For Each Node1 As TreeNode In Node0.ChildNodes
            '            For Each Node2 As TreeNode In Node1.ChildNodes
            '                For Each Node3 As TreeNode In Node2.ChildNodes
            '                    For Each Node4 As TreeNode In Node3.ChildNodes
            '                        For Each Node5 As TreeNode In Node4.ChildNodes
            '                            If _list.ModuleID = Node5.Value Then
            '                                Node5.Checked = True
            '                                Exit For
            '                            End If
            '                            If _list.LevelID <> 5 Then
            '                                Exit For
            '                            End If
            '                        Next
            '                        CountCheckNode5(Node0.Value, Node1.Value, Node2.Value, Node3.Value, Node4.Value)
            '                    Next
            '                Next
            '            Next
            '        Next
            '    Next
            'Next
            ''Checkbo Checked Node6
            'For Each _list As ModuleEntity In _lsmodule
            '    For Each Node0 As TreeNode In Tv_AllData.Nodes
            '        For Each Node1 As TreeNode In Node0.ChildNodes
            '            For Each Node2 As TreeNode In Node1.ChildNodes
            '                For Each Node3 As TreeNode In Node2.ChildNodes
            '                    For Each Node4 As TreeNode In Node3.ChildNodes
            '                        For Each Node5 As TreeNode In Node4.ChildNodes
            '                            For Each Node6 As TreeNode In Node5.ChildNodes
            '                                If _list.ModuleID = Node6.Value Then
            '                                    Node6.Checked = True
            '                                    Exit For
            '                                End If
            '                                If _list.LevelID <> 6 Then
            '                                    Exit For
            '                                End If
            '                            Next
            '                            CountCheckNode6(Node0.Value, Node1.Value, Node2.Value, Node3.Value, Node4.Value, Node5.Value)
            '                        Next
            '                    Next
            '                Next
            '            Next
            '        Next
            '    Next
            'Next
            ''Checkbo Checked Node7
            'For Each _list As ModuleEntity In _lsmodule
            '    For Each Node0 As TreeNode In Tv_AllData.Nodes
            '        For Each Node1 As TreeNode In Node0.ChildNodes
            '            For Each Node2 As TreeNode In Node1.ChildNodes
            '                For Each Node3 As TreeNode In Node2.ChildNodes
            '                    For Each Node4 As TreeNode In Node3.ChildNodes
            '                        For Each Node5 As TreeNode In Node4.ChildNodes
            '                            For Each Node6 As TreeNode In Node5.ChildNodes
            '                                For Each Node7 As TreeNode In Node6.ChildNodes
            '                                    If _list.ModuleID = Node7.Value Then
            '                                        Node7.Checked = True
            '                                        Exit For
            '                                    End If
            '                                    If _list.LevelID <> 7 Then
            '                                        Exit For
            '                                    End If
            '                                Next
            '                                CountCheckNode7(Node0.Value, Node1.Value, Node2.Value, Node3.Value, Node4.Value, Node5.Value, Node6.Value)
            '                            Next
            '                        Next
            '                    Next
            '                Next
            '            Next
            '        Next
            '    Next
            'Next
            ''Checkbo Checked Node8
            'For Each _list As ModuleEntity In _lsmodule
            '    For Each Node0 As TreeNode In Tv_AllData.Nodes
            '        For Each Node1 As TreeNode In Node0.ChildNodes
            '            For Each Node2 As TreeNode In Node1.ChildNodes
            '                For Each Node3 As TreeNode In Node2.ChildNodes
            '                    For Each Node4 As TreeNode In Node3.ChildNodes
            '                        For Each Node5 As TreeNode In Node4.ChildNodes
            '                            For Each Node6 As TreeNode In Node5.ChildNodes
            '                                For Each Node7 As TreeNode In Node6.ChildNodes
            '                                    For Each node8 As TreeNode In Node7.ChildNodes
            '                                        If _list.ModuleID = node8.Value Then
            '                                            node8.Checked = True
            '                                            Exit For
            '                                        End If
            '                                        If _list.LevelID <> 8 Then
            '                                            Exit For
            '                                        End If
            '                                    Next
            '                                    CountCheckNode8(Node0.Value, Node1.Value, Node2.Value, Node3.Value, Node4.Value, Node5.Value, Node6.Value, Node7.Value)
            '                                Next
            '                            Next
            '                        Next
            '                    Next
            '                Next
            '            Next
            '        Next
            '    Next
            'Next
            ''Checkbo Checked Node9
            'For Each _list As ModuleEntity In _lsmodule
            '    For Each Node0 As TreeNode In Tv_AllData.Nodes
            '        For Each Node1 As TreeNode In Node0.ChildNodes
            '            For Each Node2 As TreeNode In Node1.ChildNodes
            '                For Each Node3 As TreeNode In Node2.ChildNodes
            '                    For Each Node4 As TreeNode In Node3.ChildNodes
            '                        For Each Node5 As TreeNode In Node4.ChildNodes
            '                            For Each Node6 As TreeNode In Node5.ChildNodes
            '                                For Each Node7 As TreeNode In Node6.ChildNodes
            '                                    For Each Node8 As TreeNode In Node7.ChildNodes
            '                                        For Each Node9 As TreeNode In Node8.ChildNodes
            '                                            If _list.ModuleID = Node9.Value Then
            '                                                Node9.Checked = True
            '                                                Exit For
            '                                            End If
            '                                            If _list.LevelID <> 9 Then
            '                                                Exit For
            '                                            End If
            '                                        Next
            '                                        CountCheckNode9(Node0.Value, Node1.Value, Node2.Value, Node3.Value, Node4.Value, Node5.Value, Node6.Value, Node7.Value, Node8.Value)
            '                                    Next
            '                                Next
            '                            Next
            '                        Next
            '                    Next
            '                Next
            '            Next
            '        Next
            '    Next
            'Next
            ''Checkbo Checked Node10
            'For Each _list As ModuleEntity In _lsmodule
            '    For Each Node0 As TreeNode In Tv_AllData.Nodes
            '        For Each Node1 As TreeNode In Node0.ChildNodes
            '            For Each Node2 As TreeNode In Node1.ChildNodes
            '                For Each Node3 As TreeNode In Node2.ChildNodes
            '                    For Each Node4 As TreeNode In Node3.ChildNodes
            '                        For Each Node5 As TreeNode In Node4.ChildNodes
            '                            For Each Node6 As TreeNode In Node5.ChildNodes
            '                                For Each Node7 As TreeNode In Node6.ChildNodes
            '                                    For Each Node8 As TreeNode In Node7.ChildNodes
            '                                        For Each Node9 As TreeNode In Node8.ChildNodes
            '                                            For Each Node10 As TreeNode In Node9.ChildNodes
            '                                                If _list.ModuleID = Node10.Value Then
            '                                                    Node10.Checked = True
            '                                                    Exit For
            '                                                End If
            '                                                If _list.LevelID <> 10 Then
            '                                                    Exit For
            '                                                End If
            '                                            Next
            '                                            CountCheckNode10(Node0.Value, Node1.Value, Node2.Value, Node3.Value, Node4.Value, Node5.Value, Node6.Value, Node7.Value, Node8.Value, Node9.Value)
            '                                        Next
            '                                    Next
            '                                Next
            '                            Next
            '                        Next
            '                    Next
            '                Next
            '            Next
            '        Next
            '    Next
            'Next
        Else
            Clear_CheckedNode()
        End If

        Checking_CheckedNode()
        Tv_AllData.NodeWrap = True
        button_click = False
        Response.Cookies("SetFocusData").Expires = DateTime.Now.AddDays(-1)
        '_userentityforlog = New UserEntity
        '_userentityforlog = _UMB.GetUserInfo(Session("UserName"))
        'Dim _GroupData As String = _UMB.GetGroupDataforLogData(_userentityforlog.UserID)

        'With _userentityforlog
        '    .GroupID = 2
        '    .GroupName_TH = "กำหนดกลุ่มงานกับเมนูงาน "
        '    .UserActivity = "ค้นหาเมนูจากกลุ่มงาน " & _GroupData
        'End With
        '_UMB.AddLogdata(_userentityforlog)
    End Sub
    Private Sub Checking_CheckedNode()
        Dim checking As Integer
        'CheckAll Node 0
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            If Tv_AllData.Nodes.Item(i).Checked = True Then
                checking += 1
            End If
        Next

        If checking = Tv_AllData.Nodes.Count Then
            CheckedAll.Checked = True
        Else
            CheckedAll.Checked = False
        End If
    End Sub
    Private Sub Clear_CheckedNode()
        CheckedAll.Checked = False
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            Tv_AllData.Nodes.Item(i).Checked = False
            For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Checked = False
                For k As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                    Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).Checked = False
                    For l As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Count - 1
                        Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).Checked = False
                        For m As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Count - 1
                            Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).Checked = False
                            For n As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Count - 1
                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).Checked = False
                                For o As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Count - 1
                                    Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).Checked = False
                                    For p As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Count - 1
                                        Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).Checked = False
                                        For q As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Count - 1
                                            Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).Checked = False
                                            For r As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Count - 1
                                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Item(r).Checked = False
                                                For s As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Item(r).ChildNodes.Count - 1
                                                    Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Item(r).ChildNodes.Item(s).Checked = False

                                                Next
                                            Next
                                        Next
                                    Next
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next
    End Sub
    Private Sub CheckAlldata_CheckedNode()
        CheckedAll.Checked = True
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            Tv_AllData.Nodes.Item(i).Checked = True
            For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Checked = True
                For k As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                    Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).Checked = True
                    For l As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Count - 1
                        Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).Checked = True
                        For m As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Count - 1
                            Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).Checked = True
                            For n As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Count - 1
                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).Checked = True
                                For o As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Count - 1
                                    Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).Checked = True
                                    For p As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Count - 1
                                        Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).Checked = True
                                        For q As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Count - 1
                                            Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).Checked = True
                                            For r As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Count - 1
                                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Item(r).Checked = True
                                                For s As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Item(r).ChildNodes.Count - 1
                                                    Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes(k).ChildNodes.Item(l).ChildNodes.Item(m).ChildNodes.Item(n).ChildNodes.Item(o).ChildNodes.Item(p).ChildNodes.Item(q).ChildNodes.Item(r).ChildNodes.Item(s).Checked = True
                                                Next
                                            Next
                                        Next
                                    Next
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next
    End Sub

    Protected Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        Dim groupid As String = cb_listgroup.SelectedValue
        Dim dataprivilage As PrivilegeEntity
        Dim senddataprivilage As New List(Of PrivilegeEntity)
        Dim _result As Boolean = False
        Dim KeepAgain As String
        Dim countNode0 As Integer = 0
        Dim countNode1 As Integer = 0
        Dim countNode2 As Integer = 0
        Dim countNode3 As Integer = 0
        Dim countchecking As Integer = 0
        'Node Level 0
        For Each RootNode As TreeNode In Tv_AllData.Nodes
            If RootNode.Checked = True Then
                dataprivilage = New PrivilegeEntity
                With dataprivilage
                    .ModuleID = RootNode.Value
                    .GroupID = groupid
                End With
                senddataprivilage.Add(dataprivilage)
            End If
        Next
        'Node Level 1
        For Each RootNode As TreeNode In Tv_AllData.Nodes
            For Each FirstNode As TreeNode In RootNode.ChildNodes
                If FirstNode.Checked = True Then
                    dataprivilage = New PrivilegeEntity
                    With dataprivilage
                        .ModuleID = FirstNode.Value
                        .GroupID = groupid
                    End With
                    senddataprivilage.Add(dataprivilage)
                End If
            Next
            KeepAgain = CountCheckForGetTrueData(1, countNode0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            If KeepAgain <> "" Then
                For Each _Checkdata As PrivilegeEntity In senddataprivilage
                    If _Checkdata.ModuleID = KeepAgain Then
                        countchecking += 1
                        Exit For
                    End If
                Next
                If countchecking = 0 Then
                    dataprivilage = New PrivilegeEntity
                    With dataprivilage
                        .ModuleID = KeepAgain
                        .GroupID = groupid
                    End With
                    senddataprivilage.Add(dataprivilage)
                End If
                countchecking = ClearValue()
            End If
            countNode0 += 1
        Next
        countNode0 = ClearValue() : countNode1 = ClearValue() : countNode2 = ClearValue() : countNode3 = ClearValue()
        'Node Level 2
        For Each RootNode As TreeNode In Tv_AllData.Nodes
            For Each FirstNode As TreeNode In RootNode.ChildNodes
                For Each SecoundNode As TreeNode In FirstNode.ChildNodes
                    If SecoundNode.Checked = True Then
                        dataprivilage = New PrivilegeEntity
                        With dataprivilage
                            .ModuleID = SecoundNode.Value
                            .GroupID = groupid
                        End With
                        senddataprivilage.Add(dataprivilage)
                    End If
                Next
                KeepAgain = CountCheckForGetTrueData(2, countNode0, countNode1, 0, 0, 0, 0, 0, 0, 0, 0)
                If KeepAgain <> "" Then
                    For Each _CheckData As PrivilegeEntity In senddataprivilage
                        If _CheckData.ModuleID = KeepAgain Then
                            countchecking += 1
                            Exit For
                        End If
                    Next
                    If countchecking = 0 Then
                        dataprivilage = New PrivilegeEntity
                        With dataprivilage
                            .ModuleID = KeepAgain
                            .GroupID = groupid
                        End With
                        senddataprivilage.Add(dataprivilage)
                    End If
                    countchecking = ClearValue()
                    For Each _CheckData As PrivilegeEntity In senddataprivilage
                        If _CheckData.ModuleID = FirstNode.Value Then
                            countchecking += 1
                            Exit For
                        End If
                    Next
                    If countchecking = 0 Then
                        dataprivilage = New PrivilegeEntity
                        With dataprivilage
                            .ModuleID = FirstNode.Value
                            .GroupID = groupid
                        End With
                        senddataprivilage.Add(dataprivilage)
                    End If
                    countchecking = ClearValue()
                    For Each _CheckData As PrivilegeEntity In senddataprivilage
                        If _CheckData.ModuleID = RootNode.Value Then
                            countchecking += 1
                            Exit For
                        End If
                    Next
                    If countchecking = 0 Then
                        dataprivilage = New PrivilegeEntity
                        With dataprivilage
                            .ModuleID = RootNode.Value
                            .GroupID = groupid
                        End With
                        senddataprivilage.Add(dataprivilage)
                    End If
                    countchecking = ClearValue()
                End If
                countNode1 += 1
                    Next
            countNode1 = 0
            countNode0 += 1
        Next
        countNode0 = ClearValue() : countNode1 = ClearValue() : countNode2 = ClearValue() : countNode3 = ClearValue()
        'Node Level 3
        For Each RootNode As TreeNode In Tv_AllData.Nodes
            For Each FirstNode As TreeNode In RootNode.ChildNodes
                For Each SecoundNode As TreeNode In FirstNode.ChildNodes
                    For Each ThirdNode As TreeNode In SecoundNode.ChildNodes
                        If ThirdNode.Checked = True Then
                            dataprivilage = New PrivilegeEntity
                            With dataprivilage
                                .ModuleID = ThirdNode.Value
                                .GroupID = groupid
                            End With
                            senddataprivilage.Add(dataprivilage)
                        End If
                    Next
                    If countNode2 = 4 Then
                        Dim a As Integer = 10
                    End If
                    KeepAgain = CountCheckForGetTrueData(3, countNode0, countNode1, countNode2, 0, 0, 0, 0, 0, 0, 0)
                    If KeepAgain <> "" Then
                        For Each _CheckData As PrivilegeEntity In senddataprivilage
                            If _CheckData.ModuleID = KeepAgain Then
                                countchecking += 1
                                Exit For
                            End If
                        Next
                        If countchecking = 0 Then
                            dataprivilage = New PrivilegeEntity
                            With dataprivilage
                                .ModuleID = KeepAgain
                                .GroupID = groupid
                            End With
                            senddataprivilage.Add(dataprivilage)
                        End If
                        countchecking = ClearValue()
                        For Each _CheckData As PrivilegeEntity In senddataprivilage
                            If _CheckData.ModuleID = SecoundNode.Value Then
                                countchecking += 1
                                Exit For
                            End If
                        Next
                        If countchecking = 0 Then
                            dataprivilage = New PrivilegeEntity
                            With dataprivilage
                                .ModuleID = SecoundNode.Value
                                .GroupID = groupid
                            End With
                            senddataprivilage.Add(dataprivilage)
                        End If
                        countchecking = ClearValue()
                        For Each _CheckData As PrivilegeEntity In senddataprivilage
                            If _CheckData.ModuleID = FirstNode.Value Then
                                countchecking += 1
                                Exit For
                            End If
                        Next
                        If countchecking = 0 Then
                            dataprivilage = New PrivilegeEntity
                            With dataprivilage
                                .ModuleID = FirstNode.Value
                                .GroupID = groupid
                            End With
                            senddataprivilage.Add(dataprivilage)
                        End If
                        countchecking = ClearValue()
                        For Each _CheckData As PrivilegeEntity In senddataprivilage
                            If _CheckData.ModuleID = RootNode.Value Then
                                countchecking += 1
                                Exit For
                            End If
                        Next
                        If countchecking = 0 Then
                            dataprivilage = New PrivilegeEntity
                            With dataprivilage
                                .ModuleID = RootNode.Value
                                .GroupID = groupid
                            End With
                            senddataprivilage.Add(dataprivilage)
                        End If
                        countchecking = ClearValue()
                    End If
                    countNode2 += 1
                Next
                countNode1 += 1
                countNode2 = 0
            Next
            countNode0 += 1
            countNode1 = 0
        Next
        countNode0 = ClearValue() : countNode1 = ClearValue() : countNode2 = ClearValue() : countNode3 = ClearValue()
        'Node Level 4
        For Each RootNode As TreeNode In Tv_AllData.Nodes
            For Each FirstNode As TreeNode In RootNode.ChildNodes
                For Each SecoundNode As TreeNode In FirstNode.ChildNodes
                    For Each ThirdNode As TreeNode In SecoundNode.ChildNodes
                        For Each FourthNode As TreeNode In ThirdNode.ChildNodes
                            If FourthNode.Checked = True Then
                                dataprivilage = New PrivilegeEntity
                                With dataprivilage
                                    .ModuleID = FourthNode.Value
                                    .GroupID = groupid
                                End With
                                senddataprivilage.Add(dataprivilage)
                            End If
                        Next
                        KeepAgain = CountCheckForGetTrueData(4, countNode0, countNode1, countNode2, countNode3, 0, 0, 0, 0, 0, 0)
                        If KeepAgain <> "" Then
                            For Each _CechkData As PrivilegeEntity In senddataprivilage
                                If _CechkData.ModuleID = KeepAgain Then
                                    countchecking += 1
                                    Exit For
                                End If
                            Next
                            If countchecking = 0 Then
                                dataprivilage = New PrivilegeEntity
                                With dataprivilage
                                    .ModuleID = KeepAgain
                                    .GroupID = groupid
                                End With
                                senddataprivilage.Add(dataprivilage)
                            End If
                            countchecking = ClearValue()
                            For Each _CheckData As PrivilegeEntity In senddataprivilage
                                If _CheckData.ModuleID = ThirdNode.Value Then
                                    countchecking += 1
                                    Exit For
                                End If
                            Next
                            If countchecking = 0 Then
                                dataprivilage = New PrivilegeEntity
                                With dataprivilage
                                    .ModuleID = ThirdNode.Value
                                    .GroupID = groupid
                                End With
                                senddataprivilage.Add(dataprivilage)
                            End If
                            countchecking = ClearValue()
                            For Each _CheckData As PrivilegeEntity In senddataprivilage
                                If _CheckData.ModuleID = SecoundNode.Value Then
                                    countchecking += 1
                                    Exit For
                                End If
                            Next
                            If countchecking = 0 Then
                                dataprivilage = New PrivilegeEntity
                                With dataprivilage
                                    .ModuleID = SecoundNode.Value
                                    .GroupID = groupid
                                End With
                                senddataprivilage.Add(dataprivilage)
                            End If
                            countchecking = ClearValue()
                            For Each _CheckData As PrivilegeEntity In senddataprivilage
                                If _CheckData.ModuleID = FirstNode.Value Then
                                    countchecking += 1
                                    Exit For
                                End If
                            Next
                            If countchecking = 0 Then
                                dataprivilage = New PrivilegeEntity
                                With dataprivilage
                                    .ModuleID = FirstNode.Value
                                    .GroupID = groupid
                                End With
                                senddataprivilage.Add(dataprivilage)
                            End If
                            countchecking = ClearValue()
                            For Each _CheckData As PrivilegeEntity In senddataprivilage
                                If _CheckData.ModuleID = RootNode.Value Then
                                    countchecking += 1
                                    Exit For
                                End If
                            Next
                            If countchecking = 0 Then
                                dataprivilage = New PrivilegeEntity
                                With dataprivilage
                                    .ModuleID = RootNode.Value
                                    .GroupID = groupid
                                End With
                                senddataprivilage.Add(dataprivilage)
                            End If
                            countchecking = ClearValue()
                        End If
                        countNode3 += 1
                    Next
                    countNode2 += 1
                    countNode3 = 0
                Next
                countNode1 += 1
                countNode2 = 0
            Next
            countNode0 += 1
            countNode1 = 0
        Next
        countNode0 = ClearValue() : countNode1 = ClearValue() : countNode2 = ClearValue() : countNode3 = ClearValue()
        If senddataprivilage.Count = 0 Or Headerform.InnerHtml = "&nbsp;&nbsp;&nbsp;" Or Headerform.InnerHtml = "" Then
            Dim mark() = {"", "", ""}
            If Headerform.InnerHtml = "" Or Headerform.InnerHtml = "&nbsp;&nbsp;&nbsp;" Then
                mark(0) = "เลือกกลุ่มงาน"
            End If
            If senddataprivilage.Count = 0 Then
                If mark(0) = "" Then
                    mark(0) = "เลือกเมนู"
                Else
                    mark(1) = "เลือกเมนู"
                End If
            End If
            Dim createMessage As String = ""
            For i As Integer = 0 To mark.Length - 1
                If mark(i) <> "" Then
                    createMessage &= mark(i)
                Else
                    Exit For
                End If
            Next
            MessageBoxAlert("Error", "กรุณา" & createMessage & "ให้ครบถ้วน", "", "ปิด", False, True)
        Else
            Response.Cookies("SetCommandData_GSBWebsite").Value = 1
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            If MessageBox_Result = -1 Then
                btn_OK.Attributes.Remove("data-dismiss")
                MessageBoxAlert("Question", "ต้องการบันทึกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
            Else
                btn_OK.Attributes.Add("data-dismiss", "modal")
            End If

            If MessageBox_Result > 0 Then
                _result = _UMB.SaveGroupModulePrivilage(senddataprivilage)
                If _result = True Then
                    MessageBoxAlert("Success", "บันทึกสำเร็จ", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกสำเร็จ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                Else
                    MessageBoxAlert("Error", "บันทึกล้มเหลว กรุณาทำรายการใหม่", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("บันทึกล้มเหลว กรุณาทำรายการใหม่")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                End If
                _userentityforlog = New UserEntity
                _userentityforlog = _UMB.GetUserInfo(Session("UserName"))
                Dim _GroupData As String = _UMB.GetGroupDataforLogData(_userentityforlog.UserID)

                With _userentityforlog
                    .GroupID = 2
                    .GroupName_TH = "กำหนดกลุ่มงานกับเมนูงาน "
                    .UserActivity = "บันทึกเมนูจากกลุ่มงาน " & _GroupData
                End With
                _UMB.AddLogdata(_userentityforlog)
            End If
        End If
    End Sub

    Protected Sub Tv_AllData_TreeNodeCheckChanged(sender As Object, e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles Tv_AllData.TreeNodeCheckChanged
        If e.Node.Depth = 0 Then
            If e.Node.Checked = True Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = True
                    For j As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Count - 1
                        e.Node.ChildNodes.Item(i).ChildNodes.Item(j).Checked = True
                        For a As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                            e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(a).Checked = True
                            For b As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(a).ChildNodes.Count - 1
                                e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(a).ChildNodes.Item(b).Checked = True
                            Next
                        Next
                    Next
                Next
                CountCheckNode0()
            ElseIf e.Node.Checked = False Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = False
                    For j As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Count - 1
                        e.Node.ChildNodes.Item(i).ChildNodes.Item(j).Checked = False
                        For a As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                            e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(a).Checked = False
                            For b As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(a).ChildNodes.Count - 1
                                e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(a).ChildNodes.Item(b).Checked = False
                            Next
                        Next
                    Next
                Next
                CheckedAll.Checked = False
            End If
            Response.Cookies("SetFocusData").Value = 1
            Response.Cookies("SetFocusData").Expires = DateTime.Now.AddDays(1)
        ElseIf e.Node.Depth = 1 Then
            If e.Node.Checked = True Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = True
                    For j As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Count - 1
                        e.Node.ChildNodes.Item(i).ChildNodes.Item(j).Checked = True
                        For s As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                            e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(s).Checked = True
                        Next
                    Next
                Next
                e.Node.Parent.Checked = False
                CountCheckNode1(e.Node.Parent.Value)
            ElseIf e.Node.Checked = False Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = False
                    For j As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Count - 1
                        e.Node.ChildNodes.Item(i).ChildNodes.Item(j).Checked = False
                        For s As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                            e.Node.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(s).Checked = False
                        Next
                    Next
                Next
                CountCheckNode1(e.Node.Parent.Value)
            End If
            Response.Cookies("SetFocusData").Value = 1
            Response.Cookies("SetFocusData").Expires = DateTime.Now.AddDays(1)
        ElseIf e.Node.Depth = 2 Then
            If e.Node.Checked = True Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = True
                    For j As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Count - 1
                        e.Node.ChildNodes.Item(i).ChildNodes.Item(j).Checked = True
                    Next
                Next
                e.Node.Parent.Checked = True
                e.Node.Parent.Parent.Checked = True
                CountCheckNode2(e.Node.Parent.Parent.Value, e.Node.Parent.Value)
                CountCheckNode1(e.Node.Parent.Parent.Value)
            ElseIf e.Node.Checked = False Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = False
                    For j As Integer = 0 To e.Node.ChildNodes.Item(i).ChildNodes.Count - 1
                        e.Node.ChildNodes.Item(i).ChildNodes.Item(j).Checked = False
                    Next
                Next
                CountCheckNode2(e.Node.Parent.Parent.Value, e.Node.Parent.Value)
                CountCheckNode1(e.Node.Parent.Parent.Value)
            End If
            Response.Cookies("SetFocusData").Value = 1
            Response.Cookies("SetFocusData").Expires = DateTime.Now.AddDays(1)
        ElseIf e.Node.Depth = 3 Then
            If e.Node.Checked = True Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = True
                Next
                e.Node.Parent.Checked = True
                e.Node.Parent.Parent.Checked = True
                e.Node.Parent.Parent.Parent.Checked = True
                CountCheckNode3(e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value, e.Node.Parent.Value)
                CountCheckNode2(e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value)
                CountCheckNode1(e.Node.Parent.Parent.Parent.Value)
            ElseIf e.Node.Checked = False Then
                For i As Integer = 0 To e.Node.ChildNodes.Count - 1
                    e.Node.ChildNodes.Item(i).Checked = False
                Next
                CountCheckNode3(e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value, e.Node.Parent.Value)
                CountCheckNode2(e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value)
                CountCheckNode1(e.Node.Parent.Parent.Parent.Value)
            End If
            Response.Cookies("SetFocusData").Value = 1
            Response.Cookies("SetFocusData").Expires = DateTime.Now.AddDays(1)
        ElseIf e.Node.Depth = 4 Then
            If e.Node.Checked = True Then
                e.Node.Parent.Checked = True
                e.Node.Parent.Parent.Checked = True
                e.Node.Parent.Parent.Parent.Checked = True
                e.Node.Parent.Parent.Parent.Parent.Checked = True
                CountCheckNode4(e.Node.Parent.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value, e.Node.Parent.Value)
                CountCheckNode3(e.Node.Parent.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value)
                CountCheckNode2(e.Node.Parent.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Parent.Value)
                CountCheckNode1(e.Node.Parent.Parent.Parent.Parent.Value)
            ElseIf e.Node.Checked = False Then
                CountCheckNode4(e.Node.Parent.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value, e.Node.Parent.Value)
                CountCheckNode3(e.Node.Parent.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Value)
                CountCheckNode2(e.Node.Parent.Parent.Parent.Parent.Value, e.Node.Parent.Parent.Parent.Value)
                CountCheckNode1(e.Node.Parent.Parent.Parent.Parent.Value)
            End If
            Response.Cookies("SetFocusData").Value = 1
            Response.Cookies("SetFocusData").Expires = DateTime.Now.AddDays(1)
        End If

    End Sub

    Private Sub CountCheckNode0()
        Dim counting As Integer
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            If Tv_AllData.Nodes.Item(i).Checked = True Then
                counting += 1
            End If
        Next
        If counting = Tv_AllData.Nodes.Count Then
            CheckedAll.Checked = True
        Else
            CheckedAll.Checked = False
        End If
    End Sub

    Private Sub CountCheckNode1(ByVal _Node0_Value As Integer)
        Dim count As Integer = 0
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            If Tv_AllData.Nodes.Item(i).Value = _Node0_Value Then
                For j As Integer = 0 To Tv_AllData.Nodes(i).ChildNodes.Count - 1
                    If Tv_AllData.Nodes(i).ChildNodes.Item(j).Checked = True Then
                        count += 1
                    End If
                Next
                If Tv_AllData.Nodes.Item(i).ChildNodes.Count <> 0 Then
                    'If count = Tv_AllData.Nodes.Item(i).ChildNodes.Count Then
                    If count > 0 Then
                        Tv_AllData.Nodes(i).Checked = True
                    Else
                        Tv_AllData.Nodes(i).Checked = False
                        End If
                    End If

                End If
        Next
    End Sub

    Private Sub CountCheckNode2(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer)
        Dim counting As Integer
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            If Tv_AllData.Nodes.Item(i).Value = _Node0_Value Then
                For j As Integer = 0 To Tv_AllData.Nodes(i).ChildNodes.Count - 1
                    If Tv_AllData.Nodes(i).ChildNodes.Item(j).Value = _Node1_Value Then
                        For x As Integer = 0 To Tv_AllData.Nodes(i).ChildNodes.Item(j).ChildNodes.Count - 1
                            If Tv_AllData.Nodes(i).ChildNodes.Item(j).ChildNodes.Item(x).Checked = True Then
                                counting += 1
                            End If
                        Next
                        If Tv_AllData.Nodes(i).ChildNodes.Item(j).ChildNodes.Count <> 0 Then
                            'If counting = Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count Then
                            If counting > 0 Then
                                Tv_AllData.Nodes(i).ChildNodes.Item(j).Checked = True
                            Else
                                Tv_AllData.Nodes(i).ChildNodes.Item(j).Checked = False
                            End If
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode3(ByVal _Node0_value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer)
        Dim count As Integer
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            If Tv_AllData.Nodes.Item(i).Value = _Node0_value Then
                For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                    If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Value = _Node1_Value Then
                        For x As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                            If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).Value = _Node2_Value Then
                                For y As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Count - 1
                                    If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Checked = True Then
                                        count += 1
                                    End If
                                Next
                                If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Count <> 0 Then
                                    'If count = Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Count Then
                                    If count > 0 Then
                                        Tv_AllData.Nodes(i).ChildNodes.Item(j).ChildNodes.Item(x).Checked = True
                                    Else
                                        Tv_AllData.Nodes(i).ChildNodes.Item(j).ChildNodes.Item(x).Checked = False
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode4(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer, ByVal _Node3_value As Integer)
        Dim counting As Integer
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            If Tv_AllData.Nodes.Item(i).Value = _Node0_Value Then
                For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                    If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Value = _Node1_Value Then
                        For x As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                            If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).Value = _Node2_Value Then
                                For y As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Count - 1
                                    If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Value = _Node3_value Then
                                        For n As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Count - 1
                                            If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Item(n).Checked = True Then
                                                counting += 1
                                            End If
                                        Next
                                        If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Count <> 0 Then
                                            If counting > 0 Then
                                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Checked = True
                                            Else
                                                Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Checked = False
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode5(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer, ByVal _Node3_value As Integer, ByVal _Node4_value As Integer)
        Dim counting As Integer
        For Each Node0 As TreeNode In Tv_AllData.Nodes
            If Node0.Value = _Node0_Value Then
                For Each Node1 As TreeNode In Node0.ChildNodes
                    If Node1.Value = _Node1_Value Then
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            If Node2.Value = _Node2_Value Then
                                For Each Node3 As TreeNode In Node2.ChildNodes
                                    If Node3.Value = _Node3_value Then
                                        For Each Node4 As TreeNode In Node3.ChildNodes
                                            If Node4.Value = _Node4_value Then
                                                For Each Node5 As TreeNode In Node4.ChildNodes
                                                    If Node5.Checked = True Then
                                                        counting += 1
                                                    End If
                                                Next
                                                If Node4.ChildNodes.Count <> 0 Then
                                                    If counting > 0 Then
                                                        Node4.Checked = True
                                                    Else
                                                        Node4.Checked = False
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode6(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer, ByVal _Node3_value As Integer, ByVal _Node4_value As Integer, ByVal _Node5_value As Integer)
        Dim counting As Integer
        For Each Node0 As TreeNode In Tv_AllData.Nodes
            If Node0.Value = _Node0_Value Then
                For Each Node1 As TreeNode In Node0.ChildNodes
                    If Node1.Value = _Node1_Value Then
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            If Node2.Value = _Node2_Value Then
                                For Each Node3 As TreeNode In Node2.ChildNodes
                                    If Node3.Value = _Node3_value Then
                                        For Each Node4 As TreeNode In Node3.ChildNodes
                                            If Node4.Value = _Node4_value Then
                                                For Each Node5 As TreeNode In Node4.ChildNodes
                                                    If Node5.Value = _Node5_value Then
                                                        For Each Node6 As TreeNode In Node5.ChildNodes
                                                            If Node6.Checked = True Then
                                                                counting += 1
                                                            End If
                                                        Next
                                                        If Node5.ChildNodes.Count <> 0 Then
                                                            If counting > 0 Then
                                                                Node5.Checked = True

                                                            Else
                                                                Node5.Checked = False
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode7(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer, ByVal _Node3_value As Integer, ByVal _Node4_value As Integer, ByVal _Node5_value As Integer, ByVal _Node6_value As Integer)
        Dim counting As Integer
        For Each Node0 As TreeNode In Tv_AllData.Nodes
            If Node0.Value = _Node0_Value Then
                For Each Node1 As TreeNode In Node0.ChildNodes
                    If Node1.Value = _Node1_Value Then
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            If Node2.Value = _Node2_Value Then
                                For Each Node3 As TreeNode In Node2.ChildNodes
                                    If Node3.Value = _Node3_value Then
                                        For Each Node4 As TreeNode In Node3.ChildNodes
                                            If Node4.Value = _Node4_value Then
                                                For Each Node5 As TreeNode In Node4.ChildNodes
                                                    If Node5.Value = _Node5_value Then
                                                        For Each Node6 As TreeNode In Node5.ChildNodes
                                                            If Node6.Value = _Node6_value Then
                                                                For Each Node7 As TreeNode In Node6.ChildNodes
                                                                    If Node7.Checked = True Then
                                                                        counting += 1
                                                                    End If
                                                                Next
                                                                If Node6.ChildNodes.Count <> 0 Then
                                                                    If counting = Node6.ChildNodes.Count Then
                                                                        Node6.Checked = True
                                                                    Else
                                                                        Node6.Checked = False
                                                                    End If
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode8(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer, ByVal _Node3_value As Integer, ByVal _Node4_value As Integer, ByVal _Node5_value As Integer, ByVal _Node6_value As Integer, ByVal _Node7_value As Integer)
        Dim counting As Integer
        For Each Node0 As TreeNode In Tv_AllData.Nodes
            If Node0.Value = _Node0_Value Then
                For Each Node1 As TreeNode In Node0.ChildNodes
                    If Node1.Value = _Node1_Value Then
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            If Node2.Value = _Node2_Value Then
                                For Each Node3 As TreeNode In Node2.ChildNodes
                                    If Node3.Value = _Node3_value Then
                                        For Each Node4 As TreeNode In Node3.ChildNodes
                                            If Node4.Value = _Node4_value Then
                                                For Each Node5 As TreeNode In Node4.ChildNodes
                                                    If Node5.Value = _Node5_value Then
                                                        For Each Node6 As TreeNode In Node5.ChildNodes
                                                            If Node6.Value = _Node6_value Then
                                                                For Each Node7 As TreeNode In Node6.ChildNodes
                                                                    If Node7.Value = _Node7_value Then
                                                                        For Each Node8 As TreeNode In Node7.ChildNodes
                                                                            If Node8.Checked = True Then
                                                                                counting += 1
                                                                            End If
                                                                        Next
                                                                        If Node7.ChildNodes.Count <> 0 Then
                                                                            If counting = Node7.ChildNodes.Count Then
                                                                                Node7.Checked = True
                                                                            Else
                                                                                Node7.Checked = False
                                                                            End If
                                                                        End If
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode9(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer, ByVal _Node3_value As Integer, ByVal _Node4_value As Integer, ByVal _Node5_value As Integer, ByVal _Node6_value As Integer, ByVal _Node7_value As Integer, ByVal _Node8_value As Integer)
        Dim counting As Integer
        For Each Node0 As TreeNode In Tv_AllData.Nodes
            If Node0.Value = _Node0_Value Then
                For Each Node1 As TreeNode In Node0.ChildNodes
                    If Node1.Value = _Node1_Value Then
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            If Node2.Value = _Node2_Value Then
                                For Each Node3 As TreeNode In Node2.ChildNodes
                                    If Node3.Value = _Node3_value Then
                                        For Each Node4 As TreeNode In Node3.ChildNodes
                                            If Node4.Value = _Node4_value Then
                                                For Each Node5 As TreeNode In Node4.ChildNodes
                                                    If Node5.Value = _Node5_value Then
                                                        For Each Node6 As TreeNode In Node5.ChildNodes
                                                            If Node6.Value = _Node6_value Then
                                                                For Each Node7 As TreeNode In Node6.ChildNodes
                                                                    If Node7.Value = _Node7_value Then
                                                                        For Each Node8 As TreeNode In Node7.ChildNodes
                                                                            If Node8.Value = _Node8_value Then
                                                                                For Each Node9 As TreeNode In Node8.ChildNodes
                                                                                    If Node9.Checked = True Then
                                                                                        counting += 1
                                                                                    End If
                                                                                Next
                                                                                If Node8.ChildNodes.Count <> 0 Then
                                                                                    If counting = Node8.ChildNodes.Count Then
                                                                                        Node8.Checked = True
                                                                                    Else
                                                                                        Node8.Checked = False
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub CountCheckNode10(ByVal _Node0_Value As Integer, ByVal _Node1_Value As Integer, ByVal _Node2_Value As Integer, ByVal _Node3_value As Integer, ByVal _Node4_value As Integer, ByVal _Node5_value As Integer, ByVal _Node6_value As Integer, ByVal _Node7_value As Integer, ByVal _Node8_value As Integer, ByVal _Node9_value As Integer)
        Dim counting As Integer
        For Each Node0 As TreeNode In Tv_AllData.Nodes
            If Node0.Value = _Node0_Value Then
                For Each Node1 As TreeNode In Node0.ChildNodes
                    If Node1.Value = _Node1_Value Then
                        For Each Node2 As TreeNode In Node1.ChildNodes
                            If Node2.Value = _Node2_Value Then
                                For Each Node3 As TreeNode In Node2.ChildNodes
                                    If Node3.Value = _Node3_value Then
                                        For Each Node4 As TreeNode In Node3.ChildNodes
                                            If Node4.Value = _Node4_value Then
                                                For Each Node5 As TreeNode In Node4.ChildNodes
                                                    If Node5.Value = _Node5_value Then
                                                        For Each Node6 As TreeNode In Node5.ChildNodes
                                                            If Node6.Value = _Node6_value Then
                                                                For Each Node7 As TreeNode In Node6.ChildNodes
                                                                    If Node7.Value = _Node7_value Then
                                                                        For Each Node8 As TreeNode In Node7.ChildNodes
                                                                            If Node8.Value = _Node8_value Then
                                                                                For Each Node9 As TreeNode In Node8.ChildNodes
                                                                                    If Node9.Value = _Node9_value Then
                                                                                        For Each Node10 As TreeNode In Node9.ChildNodes
                                                                                            If Node10.Checked = True Then
                                                                                                counting += 1
                                                                                            End If
                                                                                        Next
                                                                                        If Node9.ChildNodes.Count <> 0 Then
                                                                                            If counting = Node9.ChildNodes.Count Then
                                                                                                Node9.Checked = True
                                                                                            Else
                                                                                                Node9.Checked = False
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                Next
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Protected Sub btn_Print_Click(sender As Object, e As EventArgs) Handles btn_Print.Click
        CreateArea()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "div_panel", "printing()", True)
        '_userentityforlog = New UserEntity
        '_userentityforlog = _UMB.GetUserInfo(Session("UserName"))
        'Dim _GroupData As String = _UMB.GetGroupDataforLogData(_userentityforlog.UserID)

        'With _userentityforlog
        '    .GroupID = 2
        '    .GroupName_TH = "กำหนดกลุ่มงานกับเมนูงาน "
        '    .UserActivity = "พิมพ์เมนูงาน " & _GroupData

        'End With
        '_UMB.AddLogdata(_userentityforlog)
    End Sub

    Protected Sub CreateArea()
        Dim CreateTag As String = ""
        Dim countAll As Integer = 0
        If cb_listgroup.Text.IndexOf("'") > -1 Then
            countAll = Count_Some_Node()
        Else
            countAll = Count_All_Node()
        End If
        Dim AllPageVal As Integer
        Dim countmoth As Integer
        Dim countPageValue As Integer = 1
        If countAll < 23 Then
            AllPageVal = 1
        Else
            AllPageVal = countAll / 23
            countmoth = countAll Mod 23
            If countmoth > 0 Then
                AllPageVal += 1
            End If
        End If
        Dim Header As String = "<table align='center' width='100%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>กำหนดกลุ่มงานกับเมนูงาน</td></tr>"
        Header &= "<tr><td colspan='2'>&nbsp;</td></tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr>"
        Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr style='background-color:#FF388C;color:#FFF;'><td align='left'>" & Headerform.InnerHtml.ToString() & "</td></tr>"
        countPageValue += 1
        div_Panel.InnerHtml = Header
        Dim _color As String = ""
        Dim count As Integer = 1

        Dim groupid As String = cb_listgroup.SelectedItem.Value.ToString()
        Dim _lsModule As List(Of ModuleEntity)
        _lsModule = _UMB.GetGroupData(groupid)


        If cb_listgroup.Text.IndexOf("'") > -1 Then



            For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
                If _color = "" Then
                    _color = "#FFE8EE"
                ElseIf _color = "#FFE8EE" Then
                    _color = "#FFCEDB"
                Else
                    _color = "#FFE8EE"
                End If
                WriteData("", _color, Tv_AllData.Nodes.Item(i).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).Text)
                If count = 23 Then
                    count = 0
                    countPageValue += 1
                End If
                count += 1
                For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                    If _color = "" Then
                        _color = "#FFE8EE"
                    ElseIf _color = "#FFE8EE" Then
                        _color = "#FFCEDB"
                    Else
                        _color = "#FFE8EE"
                    End If
                    WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Text)
                    If count = 23 Then
                        count = 0
                        countPageValue += 1
                    End If
                    count += 1
                    For x As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                        If _color = "" Then
                            _color = "#FFE8EE"
                        ElseIf _color = "#FFE8EE" Then
                            _color = "#FFCEDB"
                        Else
                            _color = "#FFE8EE"
                        End If
                        WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).Text)
                        If count = 23 Then
                            count = 0
                            countPageValue += 1
                        End If
                        count += 1
                        For y As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Count - 1
                            If _color = "" Then
                                _color = "#FFE8EE"
                            ElseIf _color = "#FFE8EE" Then
                                _color = "#FFCEDB"
                            Else
                                _color = "#FFE8EE"
                            End If
                            WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Text)
                            If count = 23 Then
                                count = 0
                                countPageValue += 1
                            End If
                            count += 1
                            For z As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Count - 1
                                If _color = "" Then
                                    _color = "#FFE8EE"
                                ElseIf _color = "#FFE8EE" Then
                                    _color = "#FFCEDB"
                                Else
                                    _color = "#FFE8EE"
                                End If
                                WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Item(z).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Item(z).Text)
                                If count = 23 Then
                                    count = 0
                                    countPageValue += 1
                                End If
                                count += 1
                            Next
                        Next
                    Next
                Next
            Next
        Else

            For Each _list As ModuleEntity In _lsModule
                If _color = "" Then
                    _color = "#FFE8EE"
                ElseIf _color = "#FFE8EE" Then
                    _color = "#FFCEDB"
                Else
                    _color = "#FFE8EE"
                End If
                If Not IsNothing(_list.ModuleNameTH) And Trim(_list.ModuleNameTH) <> "" Then
                    WriteData("", _color, _list.LevelID, count, Header, countPageValue, AllPageVal, _list.ModuleNameTH.ToString())
                Else
                    WriteData("", _color, _list.LevelID, count, Header, countPageValue, AllPageVal, _list.ModuleNameEN.ToString())
                End If
            Next

            '    Exit Sub

            '    'Checkbox Checked Node1
            '    For Each _list As ModuleEntity In _lsModule
            '        For Each Node0 As TreeNode In Tv_AllData.Nodes
            '            For Each Node1 As TreeNode In Node0.ChildNodes
            '                If _list.ModuleID = Node1.Value Then
            '                    Node1.Checked = True
            '                    Exit For
            '                End If
            '                If _list.LevelID <> 1 Then
            '                    Exit For
            '                End If
            '            Next
            '            CountCheckNode1(Node0.Value)
            '        Next
            '    Next


            '    'Checkbox Checked Node2
            '    For Each _list As ModuleEntity In _lsModule
            '        For Each Node0 As TreeNode In Tv_AllData.Nodes
            '            For Each Node1 As TreeNode In Node0.ChildNodes
            '                For Each Node2 As TreeNode In Node1.ChildNodes
            '                    If _list.ModuleID = Node2.Value Then
            '                        Node2.Checked = True
            '                        Exit For
            '                    End If
            '                    If _list.LevelID <> 2 Then
            '                        Exit For
            '                    End If
            '                Next
            '                CountCheckNode2(Node0.Value, Node1.Value)
            '            Next
            '        Next
            '    Next

            '    'Checkbox Checked Node3
            '    For Each _list As ModuleEntity In _lsModule
            '        For Each Node0 As TreeNode In Tv_AllData.Nodes
            '            For Each Node1 As TreeNode In Node0.ChildNodes
            '                For Each Node2 As TreeNode In Node1.ChildNodes
            '                    For Each Node3 As TreeNode In Node2.ChildNodes
            '                        If _list.ModuleID = Node3.Value Then
            '                            Node3.Checked = True
            '                            Exit For
            '                        End If
            '                        If _list.LevelID <> 3 Then
            '                            Exit For
            '                        End If
            '                    Next
            '                    CountCheckNode3(Node0.Value, Node1.Value, Node2.Value)
            '                Next
            '            Next
            '        Next
            '    Next

            '    'Checkbox Checked Node4
            '    For Each _list As ModuleEntity In _lsModule
            '        For Each Node0 As TreeNode In Tv_AllData.Nodes
            '            For Each Node1 As TreeNode In Node0.ChildNodes
            '                For Each Node2 As TreeNode In Node1.ChildNodes
            '                    For Each Node3 As TreeNode In Node2.ChildNodes
            '                        For Each Node4 As TreeNode In Node3.ChildNodes
            '                            If _list.ModuleID = Node4.Value Then
            '                                Node4.Checked = True
            '                                Exit For
            '                            End If
            '                            If _list.LevelID <> 4 Then
            '                                Exit For
            '                            End If
            '                        Next
            '                        CountCheckNode4(Node0.Value, Node1.Value, Node2.Value, Node3.Value)
            '                    Next
            '                Next
            '            Next
            '        Next
            '    Next

            '    For Each row In _lsModule

            '        If _color = "" Then
            '            _color = "#FFE8EE"
            '        ElseIf _color = "#FFE8EE" Then
            '            _color = "#FFCEDB"
            '        Else
            '            _color = "#FFE8EE"
            '        End If

            '        WriteData("", _color, row.LevelID, count, Header, countPageValue, AllPageVal, row.ModuleID.ToString() & "-" & row.ModuleNameTH.ToString())
            '        If count = 23 Then
            '            count = 0
            '            countPageValue += 1
            '        End If
            '        count += 1


            '    Next

            '    For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            '        If Tv_AllData.Nodes.Item(i).Checked = True Then
            '            If _color = "" Then
            '                _color = "#FFE8EE"
            '            ElseIf _color = "#FFE8EE" Then
            '                _color = "#FFCEDB"
            '            Else
            '                _color = "#FFE8EE"
            '            End If
            '            WriteData("", _color, Tv_AllData.Nodes.Item(i).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).Text)
            '            If count = 23 Then
            '                count = 0
            '                countPageValue += 1
            '            End If
            '            count += 1
            '            For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
            '                If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Checked = True Then
            '                    If _color = "" Then
            '                        _color = "#FFE8EE"
            '                    ElseIf _color = "#FFE8EE" Then
            '                        _color = "#FFCEDB"
            '                    Else
            '                        _color = "#FFE8EE"
            '                    End If
            '                    WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).Text)
            '                    If count = 23 Then
            '                        count = 0
            '                        countPageValue += 1
            '                    End If
            '                    count += 1
            '                    For x As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
            '                        If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).Checked = True Then
            '                            If _color = "" Then
            '                                _color = "#FFE8EE"
            '                            ElseIf _color = "#FFE8EE" Then
            '                                _color = "#FFCEDB"
            '                            Else
            '                                _color = "#FFE8EE"
            '                            End If
            '                            WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).Text)
            '                            If count = 23 Then
            '                                count = 0
            '                                countPageValue += 1
            '                            End If
            '                            count += 1
            '                            For y As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Count - 1
            '                                If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Checked = True Then
            '                                    If _color = "" Then
            '                                        _color = "#FFE8EE"
            '                                    ElseIf _color = "#FFE8EE" Then
            '                                        _color = "#FFCEDB"
            '                                    Else
            '                                        _color = "#FFE8EE"
            '                                    End If
            '                                    WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).Text)
            '                                    If count = 23 Then
            '                                        count = 0
            '                                        countPageValue += 1
            '                                    End If
            '                                    count += 1
            '                                    For z As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Count - 1
            '                                        If Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Item(z).Checked = True Then
            '                                            If _color = "" Then
            '                                                _color = "#FFE8EE"
            '                                            ElseIf _color = "#FFE8EE" Then
            '                                                _color = "#FFCEDB"
            '                                            Else
            '                                                _color = "#FFE8EE"
            '                                            End If
            '                                            WriteData("", _color, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Item(z).Depth, count, Header, countPageValue, AllPageVal, Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Item(z).Text)
            '                                            If count = 23 Then
            '                                                count = 0
            '                                                countPageValue += 1
            '                                            End If
            '                                            count += 1
            '                                        End If
            '                                    Next
            '                                End If
            '                            Next
            '                        End If
            '                    Next
            '                End If
            '            Next
            '        End If
            '    Next
        End If
    End Sub

    Protected Sub btn_Delete_Click(sender As Object, e As EventArgs) Handles btn_Delete.Click
        Response.Cookies("SetCommandData_GSBWebsite").Value = 2
        Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
        If Headerform.InnerHtml <> "&nbsp;&nbsp;&nbsp;" Then
            If MessageBox_Result = -1 Then
                btn_OK.Attributes.Remove("data-dismiss")
                MessageBoxAlert("Question", "ต้องการลบรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
            Else
                btn_OK.Attributes.Add("data-dismiss", "modal")
            End If
            If MessageBox_Result > 0 Then
                Dim _result As Boolean = _UMB.DeleteGroupModule(cb_listgroup.SelectedValue)
                If _result = True Then
                    MessageBoxAlert("Success", "ลบรายการสำเร็จ", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบรายการสำเร็จ")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                Else
                    MessageBoxAlert("Error", "ลบรายการล้มเหลว", "", "ปิด", False, True)
                    Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ลบรายการล้มเหลว")
                    Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
                End If
                _userentityforlog = New UserEntity
                _userentityforlog = _UMB.GetUserInfo(Session("UserName"))
                Dim _GroupData As String = _UMB.GetGroupDataforLogData(_userentityforlog.UserID)

                With _userentityforlog
                    .GroupID = 2
                    .GroupName_TH = "กำหนดกลุ่มงานกับเมนูงาน "
                    .UserActivity = "ลบเมนูของกลุ่มงาน " & cb_listgroup.Text
                End With
                _UMB.AddLogdata(_userentityforlog)
            End If
        Else
            MessageBoxAlert("Error", "กรุณาเลือกกลุ่มงาน", "", "ปิด", False, True)
        End If
    End Sub

    Protected Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        If MessageBox_Result = -1 Then
            btn_OK.Attributes.Remove("data-dismiss")
            Response.Cookies("SetCommandData_GSBWebsite").Value = 3
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(1)
            MessageBoxAlert("Question", "ต้องการยกเลิกรายการนี้ ใช่หรือไม่?", "ใช่", "ไม่ใช่", True, True)
        Else
            btn_OK.Attributes.Add("data-dismiss", "modal")
        End If
        If MessageBox_Result > 0 Then
            Clear_CheckedNode()
            Response.Cookies("finishdataRDM_Web").Value = ConvertTextToBase64("ยกเลิกรายการสำเร็จ")
            Response.Cookies("finishdataRDM_Web").Expires = DateTime.Now.AddDays(1)
            Headerform.InnerHtml = "&nbsp;&nbsp;&nbsp;"
            CheckedAll.Checked = False
            '_userentityforlog = New UserEntity
            '_userentityforlog = _UMB.GetUserInfo(Session("UserName"))
            'Dim _GroupData As String = _UMB.GetGroupDataforLogData(_userentityforlog.UserID)

            'With _userentityforlog
            '    .GroupID = 2
            '    .GroupName_TH = "กำหนดกลุ่มงานกับเมนูงาน "
            '    .UserActivity = "ยกเลิกการกรอกเมนูและเลือกกลุ่ม " & _GroupData
            'End With
            '_UMB.AddLogdata(_userentityforlog)
        End If
    End Sub

    Protected Sub MessageBoxAlert(ByVal title As String, ByVal _message As String, ByVal BtnOKString As String, ByVal BtnNOString As String, ByVal YesbtnStatus As Boolean, ByVal NobtnStatus As Boolean)
        lbl_Title.Text = title
        If title = "Error" Then
            Symbol_Image.ImageUrl = "~/Images/NotCorrect.png"
        ElseIf title = "Success" Then
            Symbol_Image.ImageUrl = "~/Images/Correct.png"
        ElseIf title = "Question" Then
            Symbol_Image.ImageUrl = "~/Images/Question.png"
        ElseIf title = "Warning" Then
            Symbol_Image.ImageUrl = "~/Images/Warning.png"
        ElseIf title = "Information" Then
            Symbol_Image.ImageUrl = "~/Images/Infomation.png"
        End If
        Messages.Text = _message
        btn_OK.Visible = YesbtnStatus
        btn_NO.Visible = NobtnStatus
        btn_OK.Text = BtnOKString
        btn_NO.Text = BtnNOString
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertBox", "$('#AlertBox').modal();", True)
        UpdModal.Update()
    End Sub

    Protected Function ConvertTextToBase64(ByVal _str As String) As String
        Dim _byt As Byte() = Encoding.UTF8.GetBytes(_str)
        Dim _base64 As String = Convert.ToBase64String(_byt)
        Return _base64
    End Function

    Protected Function ConvertBase64ToText(ByVal _base64str As String) As String
        Dim _results As String = ""
        Dim _bytes_ As Byte() = Convert.FromBase64String(_base64str)
        _results = Text.Encoding.UTF8.GetString(_bytes_)
        Return _results
    End Function

    Protected Sub CheckedAll_CheckedChanged(sender As Object, e As EventArgs) Handles CheckedAll.CheckedChanged
        If CheckedAll.Checked = True Then
            CheckAlldata_CheckedNode()
        Else
            Clear_CheckedNode()
        End If
    End Sub

    Protected Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If btn_OK.Visible = True Then
            MessageBox_Result = 1
            _command = Request.Cookies("SetCommandData_GSBWebsite").Value
            Response.Cookies("SetCommandData_GSBWebsite").Expires = DateTime.Now.AddDays(-1)
            If _command = 1 Then
                Save_Click(sender, e)
            ElseIf _command = 2 Then
                btn_Delete_Click(sender, e)
            ElseIf _command = 3 Then
                btn_Cancel_Click(sender, e)
            End If
            MessageBox_Result = -1
        End If
        Response.Redirect("~/Setting/TreeSetting.aspx")
    End Sub


    Protected Function Count_All_Node() As Integer
        Dim count As Integer
        For i As Integer = 0 To Tv_AllData.Nodes.Count - 1
            count += 1
            For j As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Count - 1
                count += 1
                For x As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Count - 1
                    count += 1
                    For y As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Count - 1
                        count += 1
                        For u As Integer = 0 To Tv_AllData.Nodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(x).ChildNodes.Item(y).ChildNodes.Count - 1
                            count += 1
                        Next
                    Next
                Next
            Next
        Next
        Return count
    End Function

    Protected Function Count_Some_Node() As Integer
        Dim count As Integer = 0
        Return count
    End Function

    Protected Sub WriteData(ByVal CreateTag As String, ByVal _color As String, ByVal NodeDepth As Integer, ByVal count As Integer, ByVal Header As String, ByVal countPageValue As Integer, ByVal AllPageVal As Integer, ByVal NodeText As String)

        If count = 23 Then
            CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td>ลำดับที่" & NodeDepth & "-" & NodeText & "</td></tr></table>"
            Header = "<table width='100%' border='0'><tr><td align='right'>" + countPageValue.ToString() + "/" + AllPageVal.ToString() + "</td></tr></table><table align='center' width='100%' border='0'><tr style='size:30pt;'><td align='center' colspan='2'>กำหนดกลุ่มงานกับเมนูงาน</td></tr>"
            Header &= "<tr><td colspan='2'>&nbsp;</td></tr><tr style='size:15pt;'><td align='left'>พิมพ์โดย : " + Session("UserName").ToString() + "</td><td align='right'>วันที่ : " + Date.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr>"
            Header &= "<table align='center' width='100%' style='border: solid 1px #000;'><tr style='background-color:#FF388C;color:#FFF;'><td align='left'>" & Headerform.InnerHtml.ToString() & "</td></tr>"
            If countPageValue = 2 Then
                CreateTag = CreateTag & "</table>" & "<div style='height:9.0cm;'></div>" & Header
            ElseIf countPageValue = 4 Then
                CreateTag = CreateTag & "</table>" & "<div style='height:6.4cm;'></div>" & Header
            ElseIf countPageValue = 5 Then
                CreateTag = CreateTag & "</table>" & "<div style='height:9.1cm;'></div>" & Header
            ElseIf countPageValue = 6 Then
                CreateTag = CreateTag & "</table>" & "<div style='height:4.8cm;'></div>" & Header
            ElseIf countPageValue = 7 Then
                CreateTag = CreateTag & "</table>" & "<div style='height:3.2cm;'></div>" & Header
            Else
                CreateTag = CreateTag & "</table>" & "<div style='height:8.0cm;'></div>" & Header
            End If

            div_Panel.InnerHtml &= CreateTag
        Else
            CreateTag = "<tr style='background-color:" + _color + ";color:#000;'><td>ลำดับที่" & NodeDepth & "-" & NodeText & "</td></tr>"
            div_Panel.InnerHtml &= CreateTag
        End If


    End Sub


    Private Function CountCheckForGetTrueData(ByVal _NodeDepth As Integer, ByVal _Node0 As Integer, ByVal _Node1 As Integer, ByVal _Node2 As Integer, ByVal _Node3 As Integer, ByVal _Node4 As Integer, ByVal _Node5 As Integer, ByVal _Node6 As Integer, ByVal _Node7 As Integer, ByVal _Node8 As Integer, ByVal _Node9 As Integer) As String
        Dim _result As String = ""
        Dim count As Integer = 0
        If _NodeDepth = 1 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).Value.ToString()
            End If
        ElseIf _NodeDepth = 2 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).Value.ToString()
            End If
        ElseIf _NodeDepth = 3 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).Value.ToString()
            End If
        ElseIf _NodeDepth = 4 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).Value.ToString()
            End If
        ElseIf _NodeDepth = 5 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).Value.ToString()
            End If
        ElseIf _NodeDepth = 6 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).Value.ToString()
            End If
        ElseIf _NodeDepth = 7 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).Value.ToString()
            End If
        ElseIf _NodeDepth = 8 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).Value.ToString()
            End If
        ElseIf _NodeDepth = 9 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Item(_Node8).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Item(_Node8).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Item(_Node8).Value.ToString()
            End If
        ElseIf _NodeDepth = 10 Then
            For i As Integer = 0 To Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Item(_Node8).ChildNodes.Item(_Node9).ChildNodes.Count - 1
                If Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Item(_Node8).ChildNodes.Item(_Node9).ChildNodes.Item(i).Checked = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                _result = Tv_AllData.Nodes.Item(_Node0).ChildNodes.Item(_Node1).ChildNodes.Item(_Node2).ChildNodes.Item(_Node3).ChildNodes.Item(_Node4).ChildNodes.Item(_Node5).ChildNodes.Item(_Node6).ChildNodes.Item(_Node7).ChildNodes.Item(_Node8).ChildNodes.Item(_Node9).Value.ToString()
            End If
        End If
        Return _result
    End Function

    Private Function ClearValue() As Integer
        Return 0
    End Function


End Class