Public Class FormMedia : Inherits DevComponents.DotNetBar.Office2007Form
    Private _IsMediaLoad As Boolean = False

    Private Sub FormMedia_Load(sender As Object, e As EventArgs) Handles Me.Load
        Width = My.Settings.MediaWidth + 18
        Height = My.Settings.MediaHeight + 87
        AppCore.DataPool.IsPlayerRun = True
    End Sub

    Private Sub FormTimeLine_KeyUp(sender As System.Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Left Then
            ChangeProgress(-2.0, True)
        ElseIf e.KeyCode = Keys.Right Then
            ChangeProgress(2.0, True)
        End If
    End Sub

    Private Sub FormMedia_FormClosed(sender As System.Object, e As Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        DdShowPlayer1.CleanUP()
        AppCore.DataPool.IsPlayerRun = False
        FormMain.SetMediaButtonEnable()
        FormMain.ButtonItemOpen.Enabled = True
        If AppCore.DataPool.IsTimeLineRun Then FormTimeLine.Close()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As EventArgs) Handles Timer1.Tick
        TrackBar1.Value = CInt(DdShowPlayer1.CurrentTime)
        Text = "视频播放器 - " & TimeProcessor.ComputeTimePoint(DdShowPlayer1.CurrentTime)
    End Sub

    Private Sub TrackBar1_Scroll(sender As System.Object, e As EventArgs) Handles TrackBar1.Scroll
        DdShowPlayer1.CurrentTime = CDbl(TrackBar1.Value)
    End Sub

    ''' <summary>
    ''' 载入媒体
    ''' </summary>
    ''' <param name="FileName">媒体文件位置</param>
    ''' <param name="Volume">音量</param>
    ''' <param name="Speed">播放速度</param>
    ''' <param name="Balace">左右声道平衡</param>
    ''' <remarks></remarks>
    Public Sub LoadMedia(ByVal FileName As String, Optional ByVal Volume As Integer = 50, Optional ByVal Speed As Integer = 1, Optional ByVal Balace As Integer = 0)
        If IO.File.Exists(FileName) = False Then Exit Sub
        DdShowPlayer1.Volume = Volume
        DdShowPlayer1.PlaySpeed = Speed
        DdShowPlayer1.Balance = Balace
        DdShowPlayer1.OpenMedia(FileName)
        TrackBar1.Maximum = CInt(DdShowPlayer1.TotalTime)
        TrackBar1.Value = 0
        _IsMediaLoad = True
        FormMain.ButtonItemOpen.Enabled = False
        FormMain.SetMediaButtonEnable(True, False, True, True)
    End Sub

    ''' <summary>
    ''' 开始或继续播放
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MediaPlay()
        If Not _IsMediaLoad Then Exit Sub
        Timer1.Enabled = True
        DdShowPlayer1.Play()
        FormMain.SetMediaButtonEnable(False, True, True, True)
        If AppCore.DataPool.IsTimeLineRun Then FormTimeLine.Button4.Text = "暂停播放(&P)"
    End Sub

    ''' <summary>
    ''' 暂停播放
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MediaPause()
        DdShowPlayer1.Pause()
        Timer1.Enabled = False
        FormMain.SetMediaButtonEnable(True, False, True, True)
        If AppCore.DataPool.IsTimeLineRun Then FormTimeLine.Button4.Text = "继续播放(&P)"
    End Sub

    ''' <summary>
    ''' 停止播放
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MediaStop()
        _IsMediaLoad = False
        DdShowPlayer1.StopPlay()
        Timer1.Enabled = False
        TrackBar1.Value = 0
        TrackBar1.Maximum = 1
        FormMain.SetMediaButtonEnable()
        FormMain.ButtonItemOpen.Enabled = True
    End Sub

    ''' <summary>
    ''' 重新开始播放
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MediaGotoStart()
        DdShowPlayer1.CurrentTime = 0.0
    End Sub

    ''' <summary>
    ''' 跳转进度
    ''' </summary>
    ''' <param name="PointTime">跳跃时间</param>
    ''' <param name="IsJump">是否根据当前位置跳转</param>
    ''' <remarks></remarks>
    Public Sub ChangeProgress(ByVal PointTime As Double, Optional ByVal IsJump As Boolean = False)
        If Not _IsMediaLoad Then Exit Sub
        If IsJump Then
            If (PointTime < 0 AndAlso Math.Abs(PointTime) < DdShowPlayer1.CurrentTime) OrElse (PointTime + DdShowPlayer1.CurrentTime > DdShowPlayer1.TotalTime) Then Exit Sub
            TrackBar1.Value += CInt(PointTime)
            DdShowPlayer1.JumpTime(PointTime)
        Else
            If PointTime < 0 OrElse PointTime > DdShowPlayer1.TotalTime Then Exit Sub
            TrackBar1.Value = CInt(PointTime)
            DdShowPlayer1.CurrentTime = PointTime
        End If
    End Sub

    ''' <summary>
    ''' 获取已播放的毫秒数
    ''' </summary>
    ''' <returns>已播放的毫秒数</returns>
    ''' <remarks></remarks>
    Public Function GetCurrentTime() As Double
        Return DdShowPlayer1.CurrentTime
    End Function

    ''' <summary>
    ''' 获取播放器状态
    ''' </summary>
    ''' <returns>播放器状态</returns>
    ''' <remarks></remarks>
    Public Function GetPlayState() As DDShowPlayer.PlayState
        Return DdShowPlayer1.PlayStateNow
    End Function

    ''' <summary>
    ''' 获取总播放时间
    ''' </summary>
    ''' <returns>总播放时间</returns>
    ''' <remarks></remarks>
    Public Function GetTotalTime() As Double
        Return DdShowPlayer1.TotalTime
    End Function
End Class