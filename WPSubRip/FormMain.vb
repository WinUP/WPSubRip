Imports DevComponents.DotNetBar.Metro
Imports DevComponents.DotNetBar
Public Class FormMain : Inherits DevComponents.DotNetBar.Office2007RibbonForm

#Region "退出检测"

    ''' <summary>
    ''' 退出检测函数
    ''' </summary>
    ''' <param name="message">要显示的提示信息</param>
    ''' <returns>保存完毕或选择了不保存(True),其他情况(False)</returns>
    ''' <remarks></remarks>
    Private Function CloseCheck(ByVal Message As String) As Boolean
        If DataPool.IsNeedSave Then
            If DataPool.IsPlayerRun Then FormMedia.TopMost = False
            If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = False
            Dim TmpResult As Windows.Forms.DialogResult = MessageBox.Show(Message, "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If TmpResult = Windows.Forms.DialogResult.Yes Then
                If DataPool.SaveFileName = "" Then
                    If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                        If SubRip.SRTFile.SaveFile(DataPool.SubRipList, SaveFileDialog1.FileName) <> StateInteger._COMPLETE_ Then
                            MessageBox.Show("存储过程发生错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            If DataPool.IsPlayerRun Then FormMedia.TopMost = True
                            If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
                            Return False
                        End If
                        If DataPool.IsPlayerRun Then FormMedia.TopMost = True
                        If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
                        Return True
                    Else
                        If DataPool.IsPlayerRun Then FormMedia.TopMost = True
                        If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
                        Return False
                    End If
                Else
                    If SubRip.SRTFile.SaveFile(DataPool.SubRipList.ToList, DataPool.SaveFileName) <> StateInteger._COMPLETE_ Then
                        MessageBox.Show("存储过程发生错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If DataPool.IsPlayerRun Then FormMedia.TopMost = True
                        If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
                        Return False
                    End If
                    If DataPool.IsPlayerRun Then FormMedia.TopMost = True
                    If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
                    Return True
                End If
            ElseIf TmpResult = Windows.Forms.DialogResult.Cancel Then
                If DataPool.IsPlayerRun Then FormMedia.TopMost = True
                If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
                Return False
            Else
                If DataPool.IsPlayerRun Then FormMedia.TopMost = True
                If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
                Return True
            End If
        End If
        If DataPool.IsPlayerRun Then FormMedia.TopMost = True
        If DataPool.IsTimeLineRun Then FormTimeLine.TopMost = True
        Return True
    End Function

    '窗体的退出检测
    Private Sub FormMain_FormClosing(ByVal sender As System.Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If ListView1.Items.Count < 1 Then Exit Sub
        If Not CloseCheck("退出前是否保存？") Then e.Cancel = True
    End Sub

#End Region

#Region "初始化与句柄关联"

    Private Sub RibbonControl1_ExpandedChanged(sender As Object, e As EventArgs) Handles RibbonControl1.ExpandedChanged
        My.Settings.RibbonExpanded = RibbonControl1.Expanded
        If RibbonControl1.Expanded Then
            ListView1.Location = New Point(5, 155)
        Else
            ListView1.Location = New Point(5, 60)
        End If
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MessageBox.Show("您当前使用的是调试版本，因此没有帮助文档，错误也可能有很多" & Environment.NewLine &
        '                "临时帮助：" & Environment.NewLine &
        '                "Ctrl + N 新建" & Environment.NewLine &
        '                "Ctrl + O 打开" & Environment.NewLine &
        '                "Ctrl + S 保存" & Environment.NewLine &
        '                "Ctrl + E 修改" & Environment.NewLine &
        '                "Ctrl + I 插入" & Environment.NewLine &
        '                "Ctrl + Shift + S 另存为" & Environment.NewLine &
        '                "Delete 删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
        RibbonControl1.TitleText = "WinUP SubRip Creater"
        Text = "WinUP SubRip Creater"
        RibbonControl1.QuickToolbarItems.Item(6).Visible = False
        Dim TmpButtonItem As DevComponents.DotNetBar.ButtonItem
        For i As Integer = 0 To My.Settings.ROD.Count - 1
            TmpButtonItem = New DevComponents.DotNetBar.ButtonItem
            TmpButtonItem.Text = My.Settings.ROD.Item(i)
            TmpButtonItem.Tooltip = My.Settings.ROD.Item(i)
            ItemContainer5.SubItems.Add(TmpButtonItem)
            AddHandler TmpButtonItem.Click, AddressOf TmpButtonItem_Click
        Next
        RibbonControl1.Expanded = My.Settings.RibbonExpanded
        RibbonControl1_ExpandedChanged(Me, New EventArgs)
        ToastNotification.ToastBackColor = System.Drawing.Color.FromArgb(255, 89, 199, 0)
        ToastNotification.ToastForeColor = Color.White
    End Sub

    Private Sub TmpButtonItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim FileUrl As String = TryCast(sender, DevComponents.DotNetBar.ButtonItem).Text
        If Not My.Computer.FileSystem.FileExists(FileUrl) Then
            MessageBox.Show("无法找到文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If DataPool.IsNeedSave AndAlso DataPool.GetListCount > 0 Then If Not CloseCheck("打开新文件前是否保存？") Then Exit Sub
        Dim StateInformation As StateInteger
        Dim TmpSrtText As List(Of SubRip.SRTStructure) = SubRip.SRTFile.LoadFile(FileUrl, StateInformation)
        If StateInformation <> StateInteger._COMPLETE_ Then
            MessageBox.Show("读取过程发生错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        DataPool.InitalizeList()
        DataPool.AddSRTStructure(TmpSrtText)
        DataPool.IsNeedSave = False
        DataPool.SaveFileName = FileUrl
        RibbonControl1.TitleText = "WinUP SubRip Creater - " & My.Computer.FileSystem.GetName(FileUrl)
        Text = "WinUP SubRip Creater - " & My.Computer.FileSystem.GetName(FileUrl)
    End Sub

#End Region

#Region "媒体播放控制"

    Private Sub ButtonItemOpen_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItemOpen.Click
        OpenFileDialog1.Filter = "所有文件(*.*)|*.*|系统默认支持的媒体文件(*.avi;*.mp4;*.mpe;*.mpg;*.mpeg;*.wmv)|*.avi;*.mp4;*.mpe;*.mpg;*.mpeg;*.wmv"
        OpenFileDialog1.Title = "打开媒体文件"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            If DataPool.IsPlayerRun Then
                FormMedia.MediaStop()
            Else
                FormMedia.Show()
            End If
            FormMedia.LoadMedia(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonItemPlay_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItemPlay.Click
        If DataPool.IsPlayerRun Then FormMedia.MediaPlay()
    End Sub

    Private Sub ButtonItemPause_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItemPause.Click
        If DataPool.IsPlayerRun Then FormMedia.MediaPause()
    End Sub

    Private Sub ButtonItemStop_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItemStop.Click
        If DataPool.IsPlayerRun Then
            FormMedia.MediaStop()
            FormMedia.Close()
        End If
    End Sub

    Private Sub ButtonItemTOStart_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItemTOStart.Click
        If DataPool.IsPlayerRun Then FormMedia.MediaGotoStart()
    End Sub

#End Region

#Region "常用函数"

    ''' <summary>
    ''' 刷新"最近打开的文件"列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshQuickOpenList()
        ItemContainer5.SubItems.Clear()
        Dim TmpButtonItem As DevComponents.DotNetBar.ButtonItem
        For i As Integer = 0 To My.Settings.ROD.Count - 1
            TmpButtonItem = New DevComponents.DotNetBar.ButtonItem
            TmpButtonItem.Text = My.Settings.ROD.Item(i)
            TmpButtonItem.Tooltip = My.Settings.ROD.Item(i)
            ItemContainer5.SubItems.Add(TmpButtonItem)
            AddHandler TmpButtonItem.Click, AddressOf TmpButtonItem_Click
        Next
    End Sub

    ''' <summary>
    ''' 设置播放控制区域按钮的状态
    ''' </summary>
    ''' <param name="Playx">播放按钮的状态</param>
    ''' <param name="Pausex">暂停按钮的状态</param>
    ''' <param name="Stopx">停止按钮的状态</param>
    ''' <param name="ToStartx">返回开头按钮的状态</param>
    ''' <remarks></remarks>
    Public Sub SetMediaButtonEnable(Optional ByVal Playx As Boolean = False, Optional ByVal Pausex As Boolean = False, Optional ByVal Stopx As Boolean = False, Optional ByVal ToStartx As Boolean = False)
        ButtonItemPlay.Enabled = Playx
        ButtonItemPause.Enabled = Pausex
        ButtonItemStop.Enabled = Stopx
        ButtonItemTOStart.Enabled = ToStartx
    End Sub

    ''' <summary>
    ''' 设置录制区域按钮的状态
    ''' </summary>
    ''' <param name="Startx">开始录制按钮的状态</param>
    ''' <param name="Resetx">重新录制按钮的状态</param>
    ''' <remarks></remarks>
    Public Sub SetControlButtonEnable(Optional ByVal Startx As Boolean = False, Optional ByVal Resetx As Boolean = False)
        ButtonItemRStart.Enabled = Startx
        ButtonItemRReset.Enabled = Resetx
    End Sub

    ''' <summary>
    ''' 显示工具提示信息
    ''' </summary>
    ''' <param name="Parent">要显示到的控件</param>
    ''' <param name="Infomationas">信息内容</param>
    ''' <remarks></remarks>
    Public Sub ShowInfomation(ByVal Parent As System.Windows.Forms.Control, ByVal Infomationas As String)
        ToastNotification.Show(Parent, Infomationas, Nothing, 3500, eToastGlowColor.Green, eToastPosition.BottomCenter)
    End Sub

#End Region

#Region """文件""菜单"

    '保存
    Private Sub ButtonItem4_Click(sender As Object, e As EventArgs) Handles ButtonItem4.Click, ButtonItem1.Click, ButtonItem9.Click
        If ListView1.Items.Count = 0 Then
            MessageBox.Show("条目为空，无法保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If DataPool.SaveFileName <> "" Then
            SubRip.SaveFile(DataPool.SubRipList, DataPool.SaveFileName)
            DataPool.IsNeedSave = False
        Else
            ButtonItem5_Click(ButtonItem4, New EventArgs())
        End If
        ShowInfomation(Me, "保存完成")
    End Sub

    '新建
    Private Sub ButtonItem14_Click(sender As Object, e As EventArgs) Handles ButtonItem14.Click, ButtonItem2.Click, ButtonItem7.Click
        If ListView1.Items.Count > 0 And DataPool.IsNeedSave Then
            If CloseCheck("新建前是否保存？") = False Then
                Exit Sub
            End If
        End If
        DataPool.InitalizeList()
        DataPool.SaveFileName = ""
        ShowInfomation(Me, "新建完成")
    End Sub

    '打开
    Private Sub ButtonItem3_Click(sender As Object, e As EventArgs) Handles ButtonItem3.Click, ButtonItem17.Click, ButtonItem8.Click
        If ListView1.Items.Count > 0 And DataPool.IsNeedSave Then
            If CloseCheck("打开新文件前是否保存？") = False Then
                Exit Sub
            End If
        End If
        OpenFileDialog1.Title = "打开SubRip文件"
        OpenFileDialog1.Filter = "SubRip文件(*.srt)|*.srt|所有文件(*.*)|*.*"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim StateInformation As StateInteger
            Dim TmpSrtText As List(Of SubRip.SRTStructure) = SubRip.LoadFile(OpenFileDialog1.FileName, StateInformation)
            If StateInformation <> StateInteger._COMPLETE_ Then
                MessageBox.Show("读取过程发生错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            DataPool.InitalizeList()
            DataPool.AddSRTStructure(TmpSrtText)
            DataPool.IsNeedSave = False
            DataPool.SaveFileName = OpenFileDialog1.FileName
            If My.Settings.ROD.IndexOf(DataPool.SaveFileName) < 0 Then
                My.Settings.ROD.Insert(0, DataPool.SaveFileName)
                If My.Settings.ROD.Count > 10 Then My.Settings.ROD.RemoveAt(10)
                RefreshQuickOpenList()
            End If
            RibbonControl1.TitleText = "WinUP SubRip Creater - " & My.Computer.FileSystem.GetName(OpenFileDialog1.FileName)
            Text = "WinUP SubRip Creater - " & My.Computer.FileSystem.GetName(OpenFileDialog1.FileName)
            ShowInfomation(Me, "已打开文件 " & My.Computer.FileSystem.GetName(OpenFileDialog1.FileName))
        End If
    End Sub

    '另存为
    Private Sub ButtonItem5_Click(sender As Object, e As EventArgs) Handles ButtonItem5.Click
        If ListView1.Items.Count = 0 Then
            MessageBox.Show("文件为空，无法保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        SaveFileDialog1.Title = "保存文件"
        SaveFileDialog1.Filter = "SubRip文件(*.srt)|*.srt|所有文件(*.*)|*.*"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SubRip.SaveFile(DataPool.SubRipList, SaveFileDialog1.FileName)
            DataPool.IsNeedSave = False
            DataPool.SaveFileName = SaveFileDialog1.FileName
            If My.Settings.ROD.IndexOf(DataPool.SaveFileName) < 0 Then
                My.Settings.ROD.Insert(0, DataPool.SaveFileName)
                If My.Settings.ROD.Count > 10 Then My.Settings.ROD.RemoveAt(10)
                RefreshQuickOpenList()
            End If
            RibbonControl1.TitleText = "WinUP SubRip Creater - " & My.Computer.FileSystem.GetName(SaveFileDialog1.FileName)
            Text = "WinUP SubRip Creater - " & My.Computer.FileSystem.GetName(SaveFileDialog1.FileName)
            ShowInfomation(Me, "已保存到文件 " & My.Computer.FileSystem.GetName(SaveFileDialog1.FileName))
        End If
    End Sub

    '信息
    Private Sub ButtonItem6_Click(sender As Object, e As EventArgs) Handles ButtonItem6.Click, ButtonItem18.Click
        If ListView1.Items.Count = 0 Then
            MessageBox.Show("没有找到任何有效条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Using TmpInformation As New Chform_Information(DataPool.SubRipList)
            TmpInformation.ShowDialog()
        End Using
    End Sub

    '帮助
    Private Sub ButtonItem20_Click(sender As Object, e As EventArgs) Handles ButtonItem20.Click
        If Not My.Computer.FileSystem.FileExists("") Then
            MessageBox.Show("无法找到帮助文件help.chm", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Process.Start("help.chm")
    End Sub

    '关于
    Private Sub ButtonItem10_Click(sender As Object, e As EventArgs) Handles ButtonItem10.Click
        Chform_AboutBox.ShowDialog()
    End Sub

    '选项
    Private Sub ButtonItem12_Click(sender As Object, e As EventArgs) Handles ButtonItem12.Click
        Chform_Settings.ShowDialog()
    End Sub

    '退出
    Private Sub ButtonItem13_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItem13.Click
        Close()
    End Sub

#End Region

#Region """开始""选项卡"

    '开始录制
    Private Sub ButtonItemRStart_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItemRStart.Click
        If Not DataPool.IsPlayerRun Then
            MessageBox.Show("你必须先打开任何一个媒体才能开始录制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If ListView1.SelectedIndices.Count > 0 Then
            FormTimeLine.RewriteIndex = ListView1.SelectedIndices(ListView1.SelectedIndices.Count - 1)
            FormTimeLine.Text = "时间轴设定 - 模式：覆盖"
        Else
            FormTimeLine.RewriteIndex = -1
            FormTimeLine.Text = "时间轴设定 - 模式：添加"
        End If
        FormTimeLine.Show()
        SetControlButtonEnable()
    End Sub

    '重新录制
    Private Sub ButtonItemRReset_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItemRReset.Click
        If MessageBox.Show("这将删除所有已存在的记录，确定要执行吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If MessageBox.Show("确定要进行""重新录制""吗？这是不可撤销的", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                DataPool.InitalizeList()
                SetControlButtonEnable(True, False)
            End If
        End If
    End Sub

#End Region

#Region """编辑""选项卡"

    ''' <summary>
    ''' 清除条目内容
    ''' </summary>
    ''' <param name="Index">要清除的条目的序号</param>
    ''' <param name="ClearMode">清除模式(1:清除时间轴 2:清除字幕 其他:全部清除)</param>
    ''' <remarks></remarks>
    Private Sub ClearItem(ByVal Index As Integer, ByVal ClearMode As Integer)
        Dim TmpSrtStructure As SubRip.SRTStructure
        TmpSrtStructure = DataPool.GetSRTStructure(Index)
        If ClearMode = 1 Then
            TmpSrtStructure.StartTime = New TimeProcessor.TimePoint(0)
            TmpSrtStructure.StopTime = New TimeProcessor.TimePoint(0)
        ElseIf ClearMode = 2 Then
            TmpSrtStructure.Subtitle = ""
        Else
            TmpSrtStructure.StartTime = New TimeProcessor.TimePoint(0)
            TmpSrtStructure.StopTime = New TimeProcessor.TimePoint(0)
            TmpSrtStructure.Subtitle = ""
        End If
        DataPool.ChangeSRTStructure(Index, TmpSrtStructure)
    End Sub

    '清除字幕
    Private Sub ButtonItem21_Click(sender As Object, e As EventArgs) Handles ButtonItem21.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        If MessageBox.Show("你确实要清除这" & ListView1.SelectedIndices.Count & "条记录的字幕吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For Each TmpIndex As Integer In ListView1.SelectedIndices
                ClearItem(TmpIndex, 2)
            Next
            ShowInfomation(Me, "字幕清除完成")
        End If
    End Sub

    '清除时间轴
    Private Sub ButtonItem23_Click(sender As Object, e As EventArgs) Handles ButtonItem23.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        If MessageBox.Show("你确实要清除这" & ListView1.SelectedIndices.Count & "条记录的时间轴吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For Each TmpIndex As Integer In ListView1.SelectedIndices
                ClearItem(TmpIndex, 1)
            Next
            ShowInfomation(Me, "时间轴清除完成")
        End If
    End Sub

    '清除整个条目
    Private Sub ButtonItem24_Click(sender As Object, e As EventArgs) Handles ButtonItem24.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        If MessageBox.Show("你确实要清除这" & ListView1.SelectedIndices.Count & "条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For Each TmpIndex As Integer In ListView1.SelectedIndices
                ClearItem(TmpIndex, 0)
            Next
        End If
        ShowInfomation(Me, "记录清除完成")
    End Sub

    '修改
    Private Sub ButtonItem19_Click(sender As Object, e As EventArgs) Handles ButtonItem19.Click
        If DataPool.IsPlayerRun Then FormMedia.MediaPause()
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        Dim TmpSubRipIndex As New List(Of Integer)
        For Each TmpIndex As Integer In ListView1.SelectedIndices
            TmpSubRipIndex.Add(TmpIndex)
        Next
        Dim TmpEditForm As New Chform_Edit(TmpSubRipIndex.ToArray)
        TmpEditForm.Show()
    End Sub

    '插入
    Private Sub ButtonItem22_Click(sender As Object, e As EventArgs) Handles ButtonItem22.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        Dim TmpSrtStructure As New SubRip.SRTStructure
        For i As Integer = ListView1.SelectedIndices.Count - 1 To 0 Step -1
            TmpSrtStructure = New SubRip.SRTStructure
            TmpSrtStructure.Index = 0
            TmpSrtStructure.StartTime = New TimeProcessor.TimePoint(0)
            TmpSrtStructure.StopTime = New TimeProcessor.TimePoint(0)
            TmpSrtStructure.Subtitle = "------------------------------"
            DataPool.AddSRTStructure(ListView1.SelectedIndices(i) + 1, TmpSrtStructure)
        Next
    End Sub

    '删除
    Private Sub ButtonItem25_Click(sender As Object, e As EventArgs) Handles ButtonItem25.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        If MessageBox.Show("你确实要删除这" & ListView1.SelectedIndices.Count & "条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim TmpIndexList As New List(Of Integer)
            For Each TmpIndex As Integer In ListView1.SelectedIndices
                TmpIndexList.Add(TmpIndex)
            Next
            While TmpIndexList.Count > 0
                DataPool.DeleteSRTStructure(TmpIndexList(0))
                TmpIndexList.RemoveAt(0)
                For i As Integer = 0 To TmpIndexList.Count - 1
                    TmpIndexList(i) -= 1
                Next
            End While
            ShowInfomation(Me, "删除完成")
        End If
    End Sub

    Private Shared Function CompareWithTime(ByVal x As SubRip.SRTStructure, ByVal y As SubRip.SRTStructure) As Integer
        If x.StartTime = y.StartTime Then Return 0
        If x.StartTime > y.StartTime Then Return 1
        Return -1
    End Function

    '升序排序
    Private Sub ButtonItem11_Click(sender As Object, e As EventArgs) Handles ButtonItem11.Click
        DataPool.SubRipList.Sort(AddressOf CompareWithTime)
        ButtonItem27_Click(ButtonItem11, New EventArgs)
        ShowInfomation(Me, "排序完成")
    End Sub

    '编排序号
    Private Sub ButtonItem15_Click(sender As Object, e As EventArgs) Handles ButtonItem15.Click
        Dim TmpSrtStructure As SubRip.SRTStructure
        For i As Integer = 0 To DataPool.GetListCount - 1
            TmpSrtStructure = New SubRip.SRTStructure
            TmpSrtStructure = DataPool.GetSRTStructure(i)
            TmpSrtStructure.Index = i + 1
            DataPool.ChangeSRTStructure(i, TmpSrtStructure)
        Next
        ShowInfomation(Me, "编号完成")
    End Sub

    '刷新列表
    Private Sub ButtonItem27_Click(sender As Object, e As EventArgs) Handles ButtonItem27.Click
        ListView1.Items.Clear()
        Dim TmpListViewItemList As New List(Of ListViewItem)
        For Each TmpSubRip In DataPool.SubRipList
            Dim TmpListViewItem As New ListViewItem()
            TmpListViewItem.Text = CStr(TmpSubRip.Index)
            TmpListViewItem.SubItems.Add(TmpSubRip.StartTime.ToString)
            TmpListViewItem.SubItems.Add(TmpSubRip.StopTime.ToString)
            TmpListViewItem.SubItems.Add(TmpSubRip.Subtitle.ToString)
            TmpListViewItemList.Add(TmpListViewItem)
        Next
        ListView1.Items.AddRange(TmpListViewItemList.ToArray)
        DataPool.IsNeedSave = True
        ShowInfomation(Me, "刷新完成")
    End Sub

#End Region

#Region """时间轴""选项卡"

    '设置
    Private Sub ButtonItem16_Click(sender As Object, e As EventArgs) Handles ButtonItem16.Click
        Chform_TimeSetting.ShowDialog()
    End Sub

    '前移
    Private Sub ButtonItem29_Click(sender As Object, e As EventArgs) Handles ButtonItem29.Click
        Dim TmpSrtStructure As SubRip.SRTStructure
        Dim TmpIndexList As New List(Of Integer)
        For Each TmpIndex As Integer In ListView1.SelectedIndices
            TmpSrtStructure = DataPool.GetSRTStructure(TmpIndex)
            TmpIndexList.Add(TmpIndex)
            If SwitchButtonItem1.Value Then
                TmpSrtStructure.StartTime -= New TimeProcessor.TimePoint(CInt(My.Settings.TimeLineAdjustment * 1000))
            Else
                TmpSrtStructure.StopTime -= New TimeProcessor.TimePoint(CInt(My.Settings.TimeLineAdjustment * 1000))
            End If
            DataPool.ChangeSRTStructure(TmpIndex, TmpSrtStructure)
        Next
        For Each TmpIndex As Integer In TmpIndexList
            ListView1.Items.Item(TmpIndex).Selected = True
        Next
    End Sub

    '推后
    Private Sub ButtonItem30_Click(sender As Object, e As EventArgs) Handles ButtonItem30.Click
        Dim TmpSrtStructure As SubRip.SRTStructure
        Dim TmpIndexList As New List(Of Integer)
        For Each TmpIndex As Integer In ListView1.SelectedIndices
            TmpSrtStructure = DataPool.GetSRTStructure(TmpIndex)
            TmpIndexList.Add(TmpIndex)
            If SwitchButtonItem1.Value Then
                TmpSrtStructure.StartTime += New TimeProcessor.TimePoint(CInt(My.Settings.TimeLineAdjustment * 1000))
            Else
                TmpSrtStructure.StopTime += New TimeProcessor.TimePoint(CInt(My.Settings.TimeLineAdjustment * 1000))
            End If
            DataPool.ChangeSRTStructure(TmpIndex, TmpSrtStructure)
        Next
        For Each TmpIndex As Integer In TmpIndexList
            ListView1.Items.Item(TmpIndex).Selected = True
        Next
    End Sub

#End Region

#Region """导入/导出""选项卡"

    '导入SubRip文件
    Private Sub ButtonItem34_Click(sender As Object, e As EventArgs) Handles ButtonItem34.Click
        OpenFileDialog1.Title = "打开SubRip文件"
        OpenFileDialog1.Filter = "SubRip文件(*.srt)|*.srt|所有文件(*.*)|*.*"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            'Dim TmpState As StateInteger
            'Dim TmpSubripList As List(Of SubRip.SRTStructure) = SubRip.LoadFile(OpenFileDialog1.FileName, TmpState)
            'If TmpState <> StateInteger._COMPLETE_ Then
            '    MessageBox.Show("读取失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End If
            'DataPool.AddSRTStructure(TmpSubripList)
            'MessageBox.Show("导入完成", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DataPool.ImportDialog = New Chform_Import(ImportMode._SUBRIP_, SubRip.LoadFile(OpenFileDialog1.FileName))
            DataPool.ImportDialog.Show()
        End If
    End Sub

    '导入字幕
    Private Sub ButtonItem32_Click(sender As Object, e As EventArgs) Handles ButtonItem32.Click
        OpenFileDialog1.Title = "打开WSS文件"
        OpenFileDialog1.Filter = "WinUP SubRip Creater Subtitle文件(*.wss)|*.wss|所有文件(*.*)|*.*"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim TmpSubripList() As String = IO.File.ReadAllLines(OpenFileDialog1.FileName, System.Text.Encoding.Default)
            If TmpSubripList(0) > "WinUP SubRip Creater Version 1.1" Then
                MessageBox.Show("无法导入文件。可能的原因：" & Environment.NewLine & "文件已损坏" & Environment.NewLine & "文件格式不正确" & Environment.NewLine & "这个文件是用更高版本的程序生成的", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim TmpSrtStructure As SubRip.SRTStructure
            For i As Integer = 1 To TmpSubripList.Count - 1
                TmpSrtStructure = New SubRip.SRTStructure
                TmpSrtStructure.Index = 0
                TmpSrtStructure.Subtitle = TmpSubripList(i)
                TmpSrtStructure.StartTime = New TimeProcessor.TimePoint(0)
                TmpSrtStructure.StopTime = New TimeProcessor.TimePoint(0)
                DataPool.AddSRTStructure(TmpSrtStructure)
            Next
            MessageBox.Show("导入完成", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    '导入时间轴
    Private Sub ButtonItem33_Click(sender As Object, e As EventArgs) Handles ButtonItem33.Click
        OpenFileDialog1.Title = "打开WST文件"
        OpenFileDialog1.Filter = "WinUP SubRip Creater Timeline文件(*.wst)|*.wst|所有文件(*.*)|*.*"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim TmpSubripList() As String = IO.File.ReadAllLines(OpenFileDialog1.FileName, System.Text.Encoding.Default)
            If TmpSubripList(0) > "WinUP SubRip Creater Version 1.0" Then
                MessageBox.Show("您选择的文件不是标准WSS文件或该文件是由本程序的更高版本生成的，无法读取", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim TmpSrtStructure As SubRip.SRTStructure
            For i As Integer = 1 To TmpSubripList.Count - 1
                TmpSrtStructure = New SubRip.SRTStructure
                TmpSrtStructure.Index = 0
                TmpSrtStructure.Subtitle = ""
                TmpSrtStructure.StartTime = New TimeProcessor.TimePoint(TimeProcessor.GetStartTime(TmpSubripList(i)))
                TmpSrtStructure.StopTime = New TimeProcessor.TimePoint(TimeProcessor.GetStopTime(TmpSubripList(i)))
                DataPool.AddSRTStructure(TmpSrtStructure)
            Next
            MessageBox.Show("导入完成", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    '导出字幕
    Private Sub ButtonItem35_Click(sender As Object, e As EventArgs) Handles ButtonItem35.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        Dim TmpSrtList As New List(Of String)
        TmpSrtList.Add("WinUP SubRip Creater Version 1.0")
        For Each TmpIndex As Integer In ListView1.SelectedIndices
            TmpSrtList.Add(DataPool.GetSRTStructure(TmpIndex).Subtitle)
        Next
        SaveFileDialog1.Title = "导出文件"
        SaveFileDialog1.Filter = "WinUP SubRip Creater Subtitle文件(*.wss)|*.wss|所有文件(*.*)|*.*"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, String.Join(Environment.NewLine, TmpSrtList.ToArray()), False, System.Text.Encoding.Default)
            MessageBox.Show("导出选定条目完成", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    '导出时间轴
    Private Sub ButtonItem36_Click(sender As Object, e As EventArgs) Handles ButtonItem36.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        Dim TmpSrtList As New List(Of String)
        TmpSrtList.Add("WinUP SubRip Creater Version 1.0")
        For Each TmpIndex As Integer In ListView1.SelectedIndices
            TmpSrtList.Add(DataPool.GetSRTStructure(TmpIndex).StartTime.ToString & " --> " & DataPool.GetSRTStructure(TmpIndex).StopTime.ToString)
        Next
        SaveFileDialog1.Title = "导出文件"
        SaveFileDialog1.Filter = "WinUP SubRip Creater Timeline文件(*.wss)|*.wst|所有文件(*.*)|*.*"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, String.Join(Environment.NewLine, TmpSrtList.ToArray()), False, System.Text.Encoding.Default)
            MessageBox.Show("导出选定条目完成", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    '导出SubRip文件
    Private Sub ButtonItem37_Click(sender As Object, e As EventArgs) Handles ButtonItem37.Click
        If ListView1.SelectedIndices.Count < 1 Then Exit Sub
        Dim TmpSrtList As New List(Of SubRip.SRTStructure)
        For Each TmpIndex As Integer In ListView1.SelectedIndices
            TmpSrtList.Add(DataPool.GetSRTStructure(TmpIndex))
        Next
        SaveFileDialog1.Title = "导出文件"
        SaveFileDialog1.Filter = "SubRip文件(*.srt)|*.srt|所有文件(*.*)|*.*"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SubRip.SaveFile(TmpSrtList, SaveFileDialog1.FileName)
            MessageBox.Show("导出选定条目完成", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

#End Region

#Region "ListView快捷操作"

    '双击编辑
    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        ButtonItem19_Click(ListView1, New EventArgs)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If DataPool.IsTimeLineRun Then
            If ListView1.SelectedIndices.Count > 0 Then
                FormTimeLine.RewriteIndex = ListView1.SelectedIndices(ListView1.SelectedIndices.Count - 1)
                FormTimeLine.Text = "时间轴设定 - 模式：覆盖"
            Else
                FormTimeLine.RewriteIndex = -1
                FormTimeLine.Text = "时间轴设定 - 模式：添加"
            End If
        End If
    End Sub

    Public Sub ListView1_SelectNext()
        Dim SelectNow As Integer = ListView1.SelectedIndices(ListView1.SelectedIndices.Count - 1)
        SelectNow += 1
        For Each TmpItem As ListViewItem In ListView1.SelectedItems
            TmpItem.Selected = False
        Next
        If SelectNow < ListView1.Items.Count Then ListView1.Items(SelectNow).Selected = True
    End Sub

    Public Sub ListView1_SelectBack()
        Dim SelectNow As Integer = ListView1.SelectedIndices(ListView1.SelectedIndices.Count - 1)
        SelectNow -= 1
        For Each TmpItem As ListViewItem In ListView1.SelectedItems
            TmpItem.Selected = False
        Next
        If SelectNow >= 0 Then ListView1.Items(SelectNow).Selected = True
    End Sub

#End Region

    Private Sub ListView1_DragDrop(sender As Object, e As DragEventArgs) Handles ListView1.DragDrop
        If e.Data.GetDataPresent(GetType(ListViewItem)) Then

        End If
    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        If e.Data.GetDataPresent(GetType(ListViewItem)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
End Class