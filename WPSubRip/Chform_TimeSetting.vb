Public Class Chform_TimeSetting : Inherits DevComponents.DotNetBar.Office2007Form

    Private Sub KnobControl1_ValueChanged(sender As Object, e As DevComponents.Instrumentation.ValueChangedEventArgs) Handles KnobControl1.ValueChanged
        Label5.Text = CStr(KnobControl1.Value)
    End Sub

    Private Sub Chform_TimeSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KnobControl1.Value = CDec(My.Settings.TimeLineAdjustment)
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        My.Settings.TimeLineAdjustment = CDbl(KnobControl1.Value)
        Close()
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        If KnobControl1.Value > 0.05 Then
            KnobControl1.Value -= CDec(0.001)
        Else
            KnobControl1.Value = 0
        End If
    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        If KnobControl1.Value < 4.95 Then
            KnobControl1.Value += CDec(0.001)
        Else
            KnobControl1.Value = 5
        End If
    End Sub
End Class