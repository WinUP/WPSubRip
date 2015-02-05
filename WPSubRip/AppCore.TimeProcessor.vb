Imports System.Text

Namespace AppCore.TimeProcessor

    ''' <summary>
    ''' 时间轴操作模块
    ''' </summary>
    ''' <remarks></remarks>
    Module TimeLineInformation

        ''' <summary>
        ''' 获取时间轴中的起始时间
        ''' </summary>
        ''' <param name="TimeLine">要分析的时间轴</param>
        ''' <returns>起始时间</returns>
        ''' <remarks></remarks>
        Public Function GetStartTime(ByVal TimeLine As String) As String
            Return TimeLine.Substring(0, 12)
        End Function

        ''' <summary>
        ''' 获取时间轴中的终止时间
        ''' </summary>
        ''' <param name="TimeLine">要分析的时间轴</param>
        ''' <returns>终止时间</returns>
        ''' <remarks></remarks>
        Public Function GetStopTime(ByVal TimeLine As String) As String
            Return TimeLine.Substring(17, TimeLine.Length - 17)
        End Function

        ''' <summary>
        ''' 依据时间点计算总的毫秒数
        ''' </summary>
        ''' <param name="TimePoint">要分析的时间点</param>
        ''' <returns>总的毫秒数</returns>
        ''' <remarks></remarks>
        Public Function GetTotalMillinsecond(ByVal TimePoint As String) As Integer
            Return New TimePoint(TimePoint).TotalMillisecond
        End Function

        ''' <summary>
        ''' 检查时间点的合法性
        ''' </summary>
        ''' <param name="Timeline">要分析的时间点</param>
        ''' <returns>是否合法</returns>
        ''' <remarks></remarks>
        Public Function IsTimePointRight(ByVal Timeline As String) As Boolean
            Return RegularExpressions.Regex.Split(Timeline, "(\d\d):([0-5]\d):([0-5]\d),(\d\d\d)").Count = 6
        End Function

        ''' <summary>
        ''' 检查时间轴的合法性
        ''' </summary>
        ''' <param name="Timeline">要分析的时间轴</param>
        ''' <returns>是否合法</returns>
        ''' <remarks></remarks>
        Public Function IsTimeLineRight(ByVal Timeline As String) As Boolean
            Return Timeline.Length = 29 AndAlso IsTimePointRight(GetStartTime(Timeline)) AndAlso
                IsTimePointRight(GetStopTime(Timeline)) AndAlso (GetStartTime(Timeline) <= GetStopTime(Timeline))
        End Function

        ''' <summary>
        ''' 将秒格式化为“时：分：秒”格式
        ''' </summary>
        ''' <param name="TotalTime">播放时间(秒数)</param>
        ''' <returns>“时：分：秒”格式的时间轴</returns>
        ''' <remarks></remarks>
        Public Function ComputeTimePoint(ByVal TotalTime As Integer) As String
            Dim Hour As Integer = TotalTime \ 3600
            TotalTime -= 3600 * Hour
            Dim Minutes As Integer = TotalTime \ 60
            TotalTime -= Minutes * 60
            Return String.Format("{0:D2}:{1:D2}:{2:D2}", Hour, Minutes, TotalTime)
        End Function

        ''' <summary>
        ''' 将毫秒格式化为“时:分:秒,毫秒”格式
        ''' </summary>
        ''' <param name="TotalTime">播放时间(秒数.毫秒数)</param>
        ''' <returns>“时:分:秒,毫秒”格式的时间轴</returns>
        ''' <remarks></remarks>
        Public Function ComputeTimePoint(ByVal TotalTime As Double) As String
            Return String.Format("{0},{1:D3}", ComputeTimePoint(CInt(TotalTime)), CInt(TotalTime * 1000) Mod 1000)
        End Function

    End Module

    ''' <summary>
    ''' 表示一个支持运算的时间点
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TimePoint

        Public TotalMillisecond As Integer

        Public ReadOnly Property Hour As Integer
            Get
                Return TotalMillisecond \ 3600000
            End Get
        End Property

        Public ReadOnly Property Minute As Integer
            Get
                Return TotalMillisecond \ 60000 - Hour * 60
            End Get
        End Property

        Public ReadOnly Property Second As Integer
            Get
                Return ((TotalMillisecond - Millisecond) \ 1000) Mod 60
            End Get
        End Property

        Public ReadOnly Property Millisecond As Integer
            Get
                Return TotalMillisecond Mod 1000
            End Get
        End Property

#Region "构造函数"

        ''' <summary>
        ''' 构造函数
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            TotalMillisecond = 0
        End Sub

        ''' <summary>
        ''' 构造函数
        ''' </summary>
        ''' <param name="Hour">初始化使用的小时数</param>
        ''' <param name="Minute">初始化使用的分钟数</param>
        ''' <param name="Second">初始化使用的秒数</param>
        ''' <param name="Millisecond">初始化使用的毫秒数</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal Hour As Integer, ByVal Minute As Integer, ByVal Second As Integer, ByVal Millisecond As Integer)
            TotalMillisecond = Millisecond + Second * 1000 + Minute * 60000 + Hour * 3600000
        End Sub

        ''' <summary>
        ''' 构造函数
        ''' </summary>
        ''' <param name="TimePoint">初始化使用的时间点</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal TimePoint As String)
            Dim TmpTimeLineArray() As String = RegularExpressions.Regex.Split(TimePoint, "(\d\d):([0-5]\d):([0-5]\d),(\d\d\d)")
            If TmpTimeLineArray.Count = 6 Then
                TotalMillisecond =
                    CInt(TmpTimeLineArray(4)) +
                    CInt(TmpTimeLineArray(3)) * 1000 +
                    CInt(TmpTimeLineArray(2)) * 60000 +
                    CInt(TmpTimeLineArray(1)) * 3600000
            Else
                TotalMillisecond = 0
            End If
        End Sub

        ''' <summary>
        ''' 构造函数
        ''' </summary>
        ''' <param name="TotalMillisecond">初始化使用的总毫秒数</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal TotalMillisecond As Integer)
            Me.TotalMillisecond = TotalMillisecond
        End Sub

#End Region

#Region "运算符重载"

        Public Shared Operator +(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As TimePoint
            Return New TimePoint(TimeLine1.TotalMillisecond + TimeLine2.TotalMillisecond)
        End Operator

        Public Shared Operator -(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As TimePoint
            Dim Delta As Integer = TimeLine1.TotalMillisecond - TimeLine2.TotalMillisecond
            Return New TimePoint(If(Delta > 0, Delta, 0))
        End Operator

        Public Shared Operator =(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As Boolean
            Return Not TimeLine1 <> TimeLine2
        End Operator

        Public Shared Operator <>(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As Boolean
            Return TimeLine1.TotalMillisecond <> TimeLine2.TotalMillisecond
        End Operator

        Public Shared Operator <(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As Boolean
            Return TimeLine1.TotalMillisecond < TimeLine2.TotalMillisecond
        End Operator

        Public Shared Operator >(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As Boolean
            Return TimeLine1.TotalMillisecond > TimeLine2.TotalMillisecond
        End Operator

        Public Shared Operator <=(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As Boolean
            Return Not TimeLine1 > TimeLine2
        End Operator

        Public Shared Operator >=(ByVal TimeLine1 As TimePoint, ByVal TimeLine2 As TimePoint) As Boolean
            Return Not TimeLine1 < TimeLine2
        End Operator

#End Region

        ''' <summary>
        ''' 返回完整的时间点
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return String.Format("{0:D2}:{1:D2}:{2:D2},{3:D3}", Hour, Minute, Second, Millisecond)
        End Function

    End Class

End Namespace