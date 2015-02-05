Imports WPSubRip.AppCore.TimeProcessor

Namespace AppCore.SubRip

    ''' <summary>
    ''' 表示单个SubRip段
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SRTStructure
        Public Index As Integer
        Public StartTime As TimePoint
        Public StopTime As TimePoint
        Public Subtitle As String
    End Structure

    ''' <summary>
    ''' SubRip文件操作模块
    ''' </summary>
    ''' <remarks></remarks>
    Module SRTFile

        ''' <summary>
        ''' 读取SubRip文件内容
        ''' </summary>
        ''' <param name="FileName">要读取的文件的路径</param>
        ''' <returns>SRTStructure结构的文件内容</returns>
        ''' <remarks></remarks>
        Public Function LoadFile(ByVal FileName As String, Optional ByRef StateInformation As StateInteger = StateInteger._COMPUTING_) As List(Of SRTStructure)
            Dim SRTFileText() As String
            Try
                SRTFileText = IO.File.ReadAllLines(FileName, System.Text.Encoding.Default)
            Catch ex As Exception
                StateInformation = StateInteger._ERROR_
                Return Nothing
            End Try
            Dim SRTStructureList As New List(Of SRTStructure)
            Dim Iter As IEnumerator(Of String) = SRTFileText.ToList().GetEnumerator()
            While Iter.MoveNext()
                While Iter.Current = ""
                    Iter.MoveNext()
                End While
                Dim TmpSRTStruct As New SRTStructure
                TmpSRTStruct.Index = CInt(Iter.Current)
                Iter.MoveNext()
                TmpSRTStruct.StartTime = New TimePoint(GetStartTime(Iter.Current))
                TmpSRTStruct.StopTime = New TimePoint(GetStopTime(Iter.Current))
                Dim TmpSubtitleList As New List(Of String)
                While Iter.MoveNext()
                    If Iter.Current <> "" Then
                        TmpSubtitleList.Add(Iter.Current)
                    Else
                        Exit While
                    End If
                End While
                TmpSRTStruct.Subtitle = String.Join("{\n}", TmpSubtitleList.ToArray())
                SRTStructureList.Add(TmpSRTStruct)
            End While
            StateInformation = StateInteger._COMPLETE_
            Return SRTStructureList
        End Function

        ''' <summary>
        ''' 保存SRTStructure结构的内容到SubRip文件
        ''' </summary>
        ''' <param name="SRTStructure">要保存的SRTStructure结构</param>
        ''' <param name="FileURL">要保存到的文件的路径</param>
        ''' <param name="IsAppend">是否是向文件追加内容(设置为True时为追加)</param>
        ''' <returns>状态字符串</returns>
        ''' <remarks></remarks>
        Public Function SaveFile(ByVal SRTStructure As List(Of SRTStructure), ByVal FileURL As String, Optional ByVal IsAppend As Boolean = False) As StateInteger
            Dim TextList As New List(Of String)
            For Each SRT In SRTStructure
                TextList.Add(SRT.Index.ToString())
                TextList.Add(String.Format("{0} --> {1}", SRT.StartTime.ToString(), SRT.StopTime.ToString()))
                TextList.Add(SRT.Subtitle.Replace("{\n}", Environment.NewLine))
                TextList.Add("")
            Next
            Try
                IO.File.WriteAllLines(FileURL, TextList.ToArray(), System.Text.Encoding.Default)
            Catch
                Return StateInteger._ERROR_
            End Try
            Return StateInteger._COMPLETE_
        End Function

    End Module

End Namespace
