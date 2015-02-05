Imports DDShowPlayer

Public Class FormTimeLine : Inherits DevComponents.DotNetBar.Office2007Form
    Private _IsPlayBack As Boolean = False
    Private _TmpSrtStructure As New SubRip.SRTStructure
    Private _RewriteIndex As Integer

    ''' <summary>
    ''' 指定要重写的条目的序号(-1为不重写)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RewriteIndex As Integer
        Get
            Return _RewriteIndex
        End Get
        Set(value As Integer)
            _RewriteIndex = value
        End Set
    End Property

    Private Sub FormTimeLine_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        DataPool.IsTimeLineRun = True
        Button2.Enabled = False
        Button3.Enabled = False
        TextBox1.Enabled = False
        If FormMedia.GetPlayState <> PlayState.Running Then Button4.Text = "继续播放(&P)"
        ActiveControl = Button1
    End Sub

    Private Sub FormTimeLine_KeyUp(sender As System.Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If ActiveControl Is TextBox1 Then Exit Sub
        If e.KeyCode = Keys.S Then
            If Button1.Enabled Then Button1_Click(Me, New EventArgs)
        ElseIf e.KeyCode = Keys.E Then
            If Button2.Enabled Then Button2_Click(Me, New EventArgs)
        ElseIf e.KeyCode = Keys.R Then
            If Button3.Enabled Then Button3_Click(Me, New EventArgs)
        ElseIf e.KeyCode = Keys.P Then
            If Button4.Enabled Then Button4_Click(Me, New EventArgs)
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As EventArgs) Handles Button1.Click
        _TmpSrtStructure.StartTime = New TimeProcessor.TimePoint(CInt(FormMedia.GetCurrentTime * 1000))
        If _IsPlayBack Then
            ListBox1.Items.Item(1) = String.Format("{0} --> {1} {2}", _TmpSrtStructure.StartTime.ToString, _TmpSrtStructure.StopTime.ToString, _TmpSrtStructure.Subtitle)
        Else
            ListBox1.Items.RemoveAt(0)
            ListBox1.Items.Add(_TmpSrtStructure.StartTime.ToString & " --> ")
        End If
        Button3.Enabled = False
        Button2.Enabled = True
        ActiveControl = Button2
        Button1.Enabled = False
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As EventArgs) Handles Button2.Click
        FormMedia.MediaPause()
        If Button2.Text.Chars(0) = "设" Then
            _TmpSrtStructure.StopTime = New TimeProcessor.TimePoint(CInt(FormMedia.GetCurrentTime * 1000))
            ListBox1.Items.Item(1) = String.Format("{0} --> {1}", _TmpSrtStructure.StartTime.ToString, _TmpSrtStructure.StopTime.ToString)
            TextBox1.Enabled = True
            RemoveHandler TextBox1.KeyUp, AddressOf TextBox1_KeyUp
            ActiveControl = TextBox1
            Button2.Text = "重设终止(&E)"
        Else
            FormMedia.ChangeProgress(_TmpSrtStructure.StartTime.TotalMillisecond / 1000.0)
            Button2.Text = "设为终止(&E)"
            ActiveControl = Button2
            FormMedia.MediaPlay()
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As EventArgs) Handles Button3.Click
        FormMedia.MediaPause()
        FormMedia.ChangeProgress(If(_TmpSrtStructure.StartTime.TotalMillisecond / 1000 - My.Settings.PlayBackSecond >= 0, _TmpSrtStructure.StartTime.TotalMillisecond / 1000 - My.Settings.PlayBackSecond, 0))
        _IsPlayBack = True
        Button2.Text = "设为终止(&E)"
        Button2.Enabled = True
        If RewriteIndex > 0 Then FormMain.ListView1_SelectBack()
        FormMedia.MediaPlay()
        Button3.Enabled = False
    End Sub

    Public Sub Button4_Click(sender As System.Object, e As EventArgs) Handles Button4.Click
        If Button4.Text.Chars(0) = "暂" Then
            FormMedia.MediaPause()
            Button4.Text = "继续播放(&P)"
        Else
            FormMedia.MediaPlay()
            Button4.Text = "暂停播放(&P)"
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As System.Object, e As Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If (e.KeyCode = Keys.Enter) AndAlso (TextBox1.Text <> "") Then
            _TmpSrtStructure.Subtitle = TextBox1.Text
            ListBox1.Items.Item(1) = String.Format("{0} --> {1} {2}", _TmpSrtStructure.StartTime.ToString, _TmpSrtStructure.StopTime.ToString, _TmpSrtStructure.Subtitle)
            TextBox1.Text = ""
            Button1.Enabled = True
            Button2.Enabled = False
            Button3.Enabled = True
            ActiveControl = Button1
            '此处为对主窗体的操作
            If RewriteIndex < 0 Then
                If Not _IsPlayBack Then
                    DataPool.AddSRTStructure(_TmpSrtStructure)
                Else
                    DataPool.ChangeSRTStructure(DataPool.GetListCount - 1, _TmpSrtStructure)
                    _IsPlayBack = False
                End If
            Else
                DataPool.ChangeSRTStructure(RewriteIndex, _TmpSrtStructure)
                FormMain.ListView1_SelectNext()
            End If
                '----------------------
                Button2.Text = "设为终止(&E)"
                FormMedia.MediaPlay()
                TextBox1.Enabled = False
            End If
    End Sub

    Private Sub TextBox1_Enter(sender As System.Object, e As EventArgs) Handles TextBox1.Enter
        If RewriteIndex > 0 Then
            _TmpSrtStructure.Subtitle = DataPool.GetSRTStructure(RewriteIndex).Subtitle
            TextBox1.Text = _TmpSrtStructure.Subtitle
        End If
        If _IsPlayBack Then TextBox1.Text = _TmpSrtStructure.Subtitle
        AddHandler TextBox1.KeyUp, AddressOf TextBox1_KeyUp
    End Sub

    Private Sub FormTimeLine_FormClosing(sender As System.Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        FormMain.SetControlButtonEnable(True, True)
        FormMain.ButtonItemRStart.Enabled = True
        DataPool.IsTimeLineRun = False
    End Sub

End Class