Public Class Chform_Settings : Inherits DevComponents.DotNetBar.Office2007Form

#Region """媒体""选项卡"

    Private Sub RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged
        If RadioButton1.Checked Then
            MaskedTextBox2.Enabled = True
        ElseIf RadioButton2.Checked Then
            MaskedTextBox2.Enabled = False
            If MaskedTextBox1.Text = "" Then
                Exit Sub
            End If
            MaskedTextBox2.Text = CStr(CInt(CInt(MaskedTextBox1.Text) / 4 * 3))
        Else
            MaskedTextBox2.Enabled = False
            If MaskedTextBox1.Text = "" Then
                Exit Sub
            End If
            MaskedTextBox2.Text = CStr(CInt(CInt(MaskedTextBox1.Text) / 16 * 9))
        End If
    End Sub

    Private Sub MaskedTextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles MaskedTextBox1.TextChanged
        If MaskedTextBox1.Text = "" Then
            Exit Sub
        End If
        If Not MaskedTextBox2.Enabled Then
            If RadioButton2.Checked Then
                MaskedTextBox2.Text = CStr(CInt(CInt(MaskedTextBox1.Text) / 4 * 3))
            Else
                MaskedTextBox2.Text = CStr(CInt(CInt(MaskedTextBox1.Text) / 16 * 9))
            End If
        End If
    End Sub

#End Region

#Region "设置读取与应用"

    Private Sub Chform_Settings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        KnobControl1.Value = CDec(My.Settings.PlayBackSecond)
        MaskedTextBox1.Text = CStr(My.Settings.MediaWidth)
        MaskedTextBox2.Text = CStr(My.Settings.MediaHeight)
        If My.Settings.Proportion = "NULL" Then
            RadioButton1.Checked = True
        ElseIf My.Settings.Proportion = "4:3" Then
            RadioButton2.Checked = True
        Else
            RadioButton3.Checked = True
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Apply()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Apply()
        Close()
    End Sub

    Private Sub Apply()
        My.Settings.PlayBackSecond = CDbl(Label5.Text)
        My.Settings.MediaWidth = CInt(MaskedTextBox1.Text)
        My.Settings.MediaHeight = CInt(MaskedTextBox2.Text)
        If RadioButton1.Checked = True Then
            My.Settings.Proportion = "NULL"
        ElseIf RadioButton2.Checked = True Then
            My.Settings.Proportion = "4:3"
        Else
            My.Settings.Proportion = "16:9"
        End If
    End Sub

#End Region

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If MessageBox.Show("确定清除""最近打开的文档""列表吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            My.Settings.ROD.Clear()
            FormMain.RefreshQuickOpenList()
            MessageBox.Show("列表清除完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If
    End Sub

    Private Sub KnobControl1_ValueChanged(sender As Object, e As DevComponents.Instrumentation.ValueChangedEventArgs) Handles KnobControl1.ValueChanged
        Label5.Text = CStr(KnobControl1.Value)
    End Sub
End Class