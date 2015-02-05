Imports WPSubRip.AppCore.DataPool

Public Class Chform_Edit : Inherits DevComponents.DotNetBar.Office2007Form
    Private _SubRipIndex() As Integer
    Private _LastIndex As Integer = 0
    Private _IsUserWantClose As Boolean = False

    Public Sub New(ByVal SubRipIndex() As Integer)
        InitializeComponent()
        _SubRipIndex = SubRipIndex
    End Sub

    Private Sub SetMainControlEnabled(ByVal Enabledx As Boolean)
        With FormMain
            .ButtonItem2.Enabled = Enabledx
            .ButtonItem17.Enabled = Enabledx
            .ButtonItem1.Enabled = Enabledx
            .ButtonItem18.Enabled = Enabledx
            .RibbonTabItem2.Enabled = Enabledx
            .RibbonTabItem3.Enabled = Enabledx
            .RibbonTabItem4.Enabled = Enabledx
            .RibbonBar3.Enabled = Enabledx
            .RibbonBar1.Enabled = Enabledx
            .ListView1.Enabled = Enabledx
            .Office2007StartButton1.Enabled = Enabledx
        End With
    End Sub

    Private Sub Chform_Edit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsEditorOpen = True
        SetMainControlEnabled(False)
        Dim TmpNode As DevComponents.AdvTree.Node
        Dim TmpStructure As SubRip.SRTStructure
        For Each TmpIndex As Integer In _SubRipIndex
            TmpNode = New DevComponents.AdvTree.Node
            TmpStructure = DataPool.GetSRTStructure(TmpIndex)
            TmpNode.Text = "条目: " & TmpStructure.Index & "<br />&nbsp;&nbsp;&nbsp;&nbsp;" & TmpStructure.Subtitle.Replace(Environment.NewLine, "{\n}")
            TmpNode.Nodes.Add(New DevComponents.AdvTree.Node("起始时间: " & TmpStructure.StartTime.ToString))
            TmpNode.Nodes.Add(New DevComponents.AdvTree.Node("终止时间: " & TmpStructure.StopTime.ToString))
            AdvTree1.Nodes.Add(TmpNode)
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MaskedTextBox1.TextChanged, MaskedTextBox2.TextChanged, TextBox1.TextChanged
        If AdvTree1.SelectedNodes.Count > 0 Then AdvTree1.SelectedNode.TagString = "1"
    End Sub

    Private Sub AdvTree1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AdvTree1.SelectedIndexChanged
        Dim TmpNode As DevComponents.AdvTree.Node = AdvTree1.Nodes.Item(_LastIndex)
        If TmpNode.TagString = "1" Then
            TmpNode.Text = "条目: " & GetSRTStructure(_SubRipIndex(_LastIndex)).Index & "<br />&nbsp;&nbsp;&nbsp;&nbsp;" & TextBox1.Text.Replace(Environment.NewLine, "{\n}")
            TmpNode.Nodes.Item(0).Text = "起始时间: " & MaskedTextBox1.Text
            TmpNode.Nodes.Item(1).Text = "终止时间: " & MaskedTextBox2.Text
            TmpNode.TagString = ""
        End If
        RemoveHandler TextBox1.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler MaskedTextBox1.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler MaskedTextBox2.TextChanged, AddressOf TextBox_TextChanged
        If AdvTree1.SelectedNodes.Count < 1 Then
            TextBox1.Text = ""
            MaskedTextBox1.Text = ""
            MaskedTextBox2.Text = ""
        ElseIf AdvTree1.SelectedNode.Nodes.Count > 1 Then
            _LastIndex = AdvTree1.SelectedNode.Index
            TmpNode = AdvTree1.SelectedNode
            TextBox1.Text = TmpNode.Text.Remove(0, TmpNode.Text.IndexOf("<") + 30).Replace("{\n}", Environment.NewLine)
            MaskedTextBox1.Text = TmpNode.Nodes.Item(0).Text.Remove(0, 6)
            MaskedTextBox2.Text = TmpNode.Nodes.Item(1).Text.Remove(0, 6)
            If IsPlayerRun AndAlso TimeProcessor.IsTimePointRight(MaskedTextBox1.Text) Then FormMedia.ChangeProgress(CDbl(TimeProcessor.GetTotalMillinsecond(MaskedTextBox1.Text)) / 1000)
        End If
        AddHandler TextBox1.TextChanged, AddressOf TextBox_TextChanged
        AddHandler MaskedTextBox1.TextChanged, AddressOf TextBox_TextChanged
        AddHandler MaskedTextBox2.TextChanged, AddressOf TextBox_TextChanged
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If MessageBox.Show("你要撤销对本条目的所有更改吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim TmpNode As DevComponents.AdvTree.Node = AdvTree1.Nodes(_LastIndex)
            Dim TmpSRTStruct As SubRip.SRTStructure = GetSRTStructure(_SubRipIndex(_LastIndex))
            TmpNode.Text = "条目: " & TmpSRTStruct.Index & "<br />&nbsp;&nbsp;&nbsp;&nbsp;" & TmpSRTStruct.Subtitle.Replace(Environment.NewLine, "{\n}")
            TmpNode.Nodes.Item(0).Text = "起始时间: " & TmpSRTStruct.StartTime.ToString
            TmpNode.Nodes.Item(1).Text = "终止时间: " & TmpSRTStruct.StopTime.ToString
            TmpNode.TagString = ""
            RemoveHandler TextBox1.TextChanged, AddressOf TextBox_TextChanged
            RemoveHandler MaskedTextBox1.TextChanged, AddressOf TextBox_TextChanged
            RemoveHandler MaskedTextBox2.TextChanged, AddressOf TextBox_TextChanged
            MaskedTextBox1.Text = TmpSRTStruct.StartTime.ToString
            MaskedTextBox2.Text = TmpSRTStruct.StopTime.ToString
            TextBox1.Text = TmpSRTStruct.Subtitle.Replace("{\n}", Environment.NewLine)
            AddHandler TextBox1.TextChanged, AddressOf TextBox_TextChanged
            AddHandler MaskedTextBox1.TextChanged, AddressOf TextBox_TextChanged
            AddHandler MaskedTextBox2.TextChanged, AddressOf TextBox_TextChanged
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        AdvTree1_SelectedIndexChanged(Button3, New EventArgs())
        For Each TmpCheckNode As DevComponents.AdvTree.Node In AdvTree1.Nodes
            If Not TimeProcessor.IsTimePointRight(TmpCheckNode.Nodes.Item(0).Text) Then
                MessageBox.Show("第" & TmpCheckNode.Index + 1 & "个条目的起始时间格式不合法", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                AdvTree1.SelectedNode = TmpCheckNode
                Exit Sub
            End If
            If Not TimeProcessor.IsTimePointRight(TmpCheckNode.Nodes.Item(1).Text) Then
                MessageBox.Show("第" & TmpCheckNode.Index + 1 & "个条目的终止时间格式不合法", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                AdvTree1.SelectedNode = TmpCheckNode
                Exit Sub
            End If
            If TmpCheckNode.Nodes.Item(0).Text.Remove(0, 6) > TmpCheckNode.Nodes.Item(1).Text.Remove(0, 6) Then
                MessageBox.Show("第" & TmpCheckNode.Index + 1 & "个条目的起始时间大于了终止时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                AdvTree1.SelectedNode = TmpCheckNode
                Exit Sub
            End If
        Next
        Dim TmpSRTStruct As SubRip.SRTStructure
        For Each TmpNode As DevComponents.AdvTree.Node In AdvTree1.Nodes
            TmpSRTStruct = New SubRip.SRTStructure
            TmpSRTStruct.Index = GetSRTStructure(_SubRipIndex(TmpNode.Index)).Index
            TmpSRTStruct.StartTime = New TimeProcessor.TimePoint(TmpNode.Nodes.Item(0).Text.Remove(0, 6))
            TmpSRTStruct.StopTime = New TimeProcessor.TimePoint(TmpNode.Nodes.Item(1).Text.Remove(0, 6))
            TmpSRTStruct.Subtitle = TmpNode.Text.Remove(0, TmpNode.Text.IndexOf("<") + 30)
            ChangeSRTStructure(_SubRipIndex(TmpNode.Index), TmpSRTStruct)
        Next
        _IsUserWantClose = True
        SetMainControlEnabled(True)
        Close()
    End Sub

    Private Sub Chform_Edit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _IsUserWantClose Then Exit Sub
        If MessageBox.Show("你确实要放弃更改?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            SetMainControlEnabled(True)
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Chform_Edit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        IsEditorOpen = False
    End Sub

End Class