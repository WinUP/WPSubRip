Namespace AppCore.DataPool

    ''' <summary>
    ''' 应用程序的数据池
    ''' </summary>
    ''' <remarks></remarks>
    Module StateVariables

        ''' <summary>
        ''' 是否需要保存
        ''' </summary>
        ''' <remarks></remarks>
        Public IsNeedSave As Boolean = False
        ''' <summary>
        ''' 播放器是否在运行
        ''' </summary>
        ''' <remarks></remarks>
        Public IsPlayerRun As Boolean = False
        ''' <summary>
        ''' 是否正在录制
        ''' </summary>
        ''' <remarks></remarks>
        Public IsTimeLineRun As Boolean = False
        ''' <summary>
        ''' 编辑器是否已打开
        ''' </summary>
        ''' <remarks></remarks>
        Public IsEditorOpen As Boolean = False
        ''' <summary>
        ''' 当前操作的文件的路径
        ''' </summary>
        ''' <remarks></remarks>
        Public SaveFileName As String = ""
        ''' <summary>
        ''' 全局使用的导入窗口
        ''' </summary>
        ''' <remarks></remarks>
        Public ImportDialog As Chform_Import

    End Module

    '!这里本不应该出现对ListView的操作，不过现在还没有很好的解决办法
    ''' <summary>
    ''' 字幕存储池
    ''' </summary>
    ''' <remarks></remarks>
    Module SubRipTextList

        ''' <summary>
        ''' 字幕列表
        ''' </summary>
        ''' <remarks></remarks>
        Public SubRipList As New List(Of SubRip.SRTStructure)

        ''' <summary>
        ''' 获取列表元素数目
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetListCount() As Integer
            Return SubRipList.Count
        End Function

        ''' <summary>
        ''' 添加一个条目
        ''' </summary>
        ''' <param name="SRTStructure">要添加的条目的结构体</param>
        ''' <returns>添加后列表的元素数目</returns>
        ''' <remarks></remarks>
        Public Function AddSRTStructure(ByVal SRTStructure As SubRip.SRTStructure) As Integer
            SubRipList.Add(SRTStructure)
            Dim TmpListViewItem As New ListViewItem
            TmpListViewItem.Text = CStr(SRTStructure.Index)
            TmpListViewItem.SubItems.Add(SRTStructure.StartTime.ToString)
            TmpListViewItem.SubItems.Add(SRTStructure.StopTime.ToString)
            TmpListViewItem.SubItems.Add(SRTStructure.Subtitle)
            FormMain.ListView1.Items.Add(TmpListViewItem)
            IsNeedSave = True
            Return GetListCount()
        End Function

        ''' <summary>
        ''' 添加一组条目
        ''' </summary>
        ''' <param name="SRTStructureList">要添加的条目的列表</param>
        ''' <returns>添加后列表的元素数目</returns>
        ''' <remarks></remarks>
        Public Function AddSRTStructure(ByVal SRTStructureList As List(Of SubRip.SRTStructure)) As Integer
            SubRipList.AddRange(SRTStructureList)
            Dim TmpListViewItemList As New List(Of ListViewItem)
            For Each TmpSubRip In SRTStructureList
                Dim TmpListViewItem As New ListViewItem
                TmpListViewItem.Text = CStr(TmpSubRip.Index)
                TmpListViewItem.SubItems.Add(TmpSubRip.StartTime.ToString)
                TmpListViewItem.SubItems.Add(TmpSubRip.StopTime.ToString)
                TmpListViewItem.SubItems.Add(TmpSubRip.Subtitle)
                TmpListViewItemList.Add(TmpListViewItem)
            Next
            FormMain.ListView1.Items.AddRange(TmpListViewItemList.ToArray)
            IsNeedSave = True
            Return GetListCount()
        End Function

        ''' <summary>
        ''' 插入一个条目
        ''' </summary>
        ''' <param name="InsertPoint">插入位置</param>
        ''' <param name="SRTStructure">要插入的条目的列表</param>
        ''' <returns>插入后列表的元素数目</returns>
        ''' <remarks></remarks>
        Public Function AddSRTStructure(ByVal InsertPoint As Integer, ByVal SRTStructure As SubRip.SRTStructure) As Integer
            If InsertPoint >= GetListCount() Then Return GetListCount()
            SubRipList.Insert(InsertPoint, SRTStructure)
            Dim TmpListViewItem As New ListViewItem
            TmpListViewItem.Text = CStr(SRTStructure.Index)
            TmpListViewItem.SubItems.Add(SRTStructure.StartTime.ToString)
            TmpListViewItem.SubItems.Add(SRTStructure.StopTime.ToString)
            TmpListViewItem.SubItems.Add(SRTStructure.Subtitle)
            FormMain.ListView1.Items.Insert(InsertPoint, TmpListViewItem)
            IsNeedSave = True
            Return GetListCount()
        End Function

        ''' <summary>
        ''' 更改一个条目
        ''' </summary>
        ''' <param name="Index">要更改的条目的编号</param>
        ''' <param name="Content">要更改成的内容</param>
        ''' <remarks></remarks>
        Public Sub ChangeSRTStructure(ByVal Index As Integer, ByVal Content As SubRip.SRTStructure)
            If Index > GetListCount() - 1 Then Exit Sub
            SubRipList.Item(Index) = Content
            Dim TmpListViewItem As ListViewItem = FormMain.ListView1.Items(Index)
            TmpListViewItem.Text = CStr(Content.Index)
            TmpListViewItem.SubItems(1).Text = Content.StartTime.ToString
            TmpListViewItem.SubItems(2).Text = Content.StopTime.ToString
            TmpListViewItem.SubItems(3).Text = Content.Subtitle
            IsNeedSave = True
        End Sub

        ''' <summary>
        ''' 删除一个条目
        ''' </summary>
        ''' <param name="Index">要删除的条目的编号</param>
        ''' <remarks></remarks>
        Public Sub DeleteSRTStructure(ByVal Index As Integer)
            If Index > GetListCount() - 1 Then Exit Sub
            SubRipList.RemoveAt(Index)
            FormMain.ListView1.Items.RemoveAt(Index)
            IsNeedSave = True
        End Sub

        ''' <summary>
        ''' 获取一个条目
        ''' </summary>
        ''' <param name="Index">要获取的条目的编号</param>
        ''' <returns>目标条目</returns>
        ''' <remarks></remarks>
        Public Function GetSRTStructure(ByVal Index As Integer) As SubRip.SRTStructure
            If Index > GetListCount() - 1 Then Return Nothing
            Return SubRipList.Item(Index)
        End Function

        ''' <summary>
        ''' 重置字幕列表
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitalizeList()
            SubRipList = New List(Of SubRip.SRTStructure)
            FormMain.ListView1.Items.Clear()
            IsNeedSave = True
        End Sub

    End Module

End Namespace