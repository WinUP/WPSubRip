Public Class Chform_Import : Inherits DevComponents.DotNetBar.Office2007Form

    Private _Mode As ImportMode : Private _DataList As List(Of SubRip.SRTStructure)

    Public Sub New(ByVal Mode As ImportMode, ByVal Data As List(Of SubRip.SRTStructure))
        InitializeComponent()
        _Mode = Mode
        _DataList = Data
        Dim TmpItemList As New List(Of String)
        For i As Integer = 0 To _DataList.Count - 1
            If _Mode = ImportMode._TIMELINE_ Then
                TmpItemList.Add(_DataList(i).StartTime.ToString & " --> " & _DataList(i).StopTime.ToString)
            ElseIf _Mode = ImportMode._SUBRIP_ Then
                TmpItemList.Add(_DataList(i).Subtitle)
            Else
                TmpItemList.Add("条目：" & _DataList(i).Subtitle)
            End If
        Next
        ListBox1.Items.AddRange(TmpItemList.ToArray)
    End Sub

    Private Function GetSelectItem() As SubRip.SRTStructure()
        Return (From i In ListBox1.SelectedIndices Select _DataList(CInt(i))).ToArray
    End Function

    Private Sub DeleteSelectItem()

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndices.Count < 1 Then Exit Sub

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndices.Count < 1 Then Exit Sub

    End Sub
End Class