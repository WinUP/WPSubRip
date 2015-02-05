Public Class Chform_Information : Inherits DevComponents.DotNetBar.Office2007Form

    Public Sub New(ByVal SRTStructInformation As List(Of SubRip.SRTStructure))
        InitializeComponent()
        If DataPool.SaveFileName = "" Then
            TextBoxFile.Text = "未保存"
            TextBoxURL.Text = "未保存"
        Else
            TextBoxFile.Text = DataPool.SaveFileName.Remove(0, DataPool.SaveFileName.LastIndexOf("\") + 1)
            If DataPool.IsNeedSave Then TextBoxFile.Text &= "（更改未保存）"
            TextBoxURL.Text = DataPool.SaveFileName.Remove(DataPool.SaveFileName.LastIndexOf("\"))
        End If
        If SRTStructInformation.Count = 1 Then
            TextBoxStart.Text = SRTStructInformation(0).StartTime.ToString
            TextBoxStop.Text = SRTStructInformation(0).StopTime.ToString
            TextBoxLength.Text = (SRTStructInformation(0).StopTime - SRTStructInformation(0).StartTime).ToString
            TextBoxCount.Text = "1"
        Else
            Dim TmpStartTime, TmpStopTime As TimeProcessor.TimePoint
            TmpStartTime = SRTStructInformation(0).StartTime
            TmpStopTime = SRTStructInformation(0).StopTime
            For i As Integer = 1 To SRTStructInformation.Count - 1
                If TmpStartTime > SRTStructInformation(i).StartTime Then TmpStartTime = SRTStructInformation(i).StartTime
                If TmpStopTime < SRTStructInformation(i).StopTime Then TmpStopTime = SRTStructInformation(i).StopTime
            Next
            TextBoxStart.Text = TmpStartTime.ToString
            TextBoxStop.Text = TmpStopTime.ToString
            TextBoxLength.Text = (TmpStopTime - TmpStartTime).ToString
            TextBoxCount.Text = CStr(SRTStructInformation.Count)
        End If
        Me.ActiveControl = Button1
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Close()
    End Sub
End Class