<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Chform_Information
    Inherits DevComponents.DotNetBar.Office2007Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxFile = New System.Windows.Forms.TextBox()
        Me.TextBoxURL = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxStart = New System.Windows.Forms.TextBox()
        Me.TextBoxStop = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxLength = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxCount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New DevComponents.DotNetBar.ButtonX()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "字幕文件："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "路径："
        '
        'TextBoxFile
        '
        Me.TextBoxFile.BackColor = System.Drawing.Color.White
        Me.TextBoxFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxFile.Location = New System.Drawing.Point(15, 29)
        Me.TextBoxFile.Name = "TextBoxFile"
        Me.TextBoxFile.ReadOnly = True
        Me.TextBoxFile.Size = New System.Drawing.Size(264, 23)
        Me.TextBoxFile.TabIndex = 3
        '
        'TextBoxURL
        '
        Me.TextBoxURL.BackColor = System.Drawing.Color.White
        Me.TextBoxURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxURL.Location = New System.Drawing.Point(15, 75)
        Me.TextBoxURL.Name = "TextBoxURL"
        Me.TextBoxURL.ReadOnly = True
        Me.TextBoxURL.Size = New System.Drawing.Size(264, 23)
        Me.TextBoxURL.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "起始时间："
        '
        'TextBoxStart
        '
        Me.TextBoxStart.BackColor = System.Drawing.Color.White
        Me.TextBoxStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxStart.Location = New System.Drawing.Point(15, 121)
        Me.TextBoxStart.Name = "TextBoxStart"
        Me.TextBoxStart.ReadOnly = True
        Me.TextBoxStart.Size = New System.Drawing.Size(264, 23)
        Me.TextBoxStart.TabIndex = 6
        '
        'TextBoxStop
        '
        Me.TextBoxStop.BackColor = System.Drawing.Color.White
        Me.TextBoxStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxStop.Location = New System.Drawing.Point(15, 167)
        Me.TextBoxStop.Name = "TextBoxStop"
        Me.TextBoxStop.ReadOnly = True
        Me.TextBoxStop.Size = New System.Drawing.Size(264, 23)
        Me.TextBoxStop.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 147)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 17)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "终止时间："
        '
        'TextBoxLength
        '
        Me.TextBoxLength.BackColor = System.Drawing.Color.White
        Me.TextBoxLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxLength.Location = New System.Drawing.Point(15, 213)
        Me.TextBoxLength.Name = "TextBoxLength"
        Me.TextBoxLength.ReadOnly = True
        Me.TextBoxLength.Size = New System.Drawing.Size(264, 23)
        Me.TextBoxLength.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 193)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 17)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "时间跨度："
        '
        'TextBoxCount
        '
        Me.TextBoxCount.BackColor = System.Drawing.Color.White
        Me.TextBoxCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxCount.Location = New System.Drawing.Point(15, 259)
        Me.TextBoxCount.Name = "TextBoxCount"
        Me.TextBoxCount.ReadOnly = True
        Me.TextBoxCount.Size = New System.Drawing.Size(264, 23)
        Me.TextBoxCount.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 239)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "条目数："
        '
        'Button1
        '
        Me.Button1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(204, 293)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 26)
        Me.Button1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "关闭"
        '
        'Chform_Information
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(293, 331)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBoxFile)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxURL)
        Me.Controls.Add(Me.TextBoxCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxLength)
        Me.Controls.Add(Me.TextBoxStart)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxStop)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Chform_Information"
        Me.ShowInTaskbar = False
        Me.Text = "信息"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFile As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxURL As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxStart As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxStop As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLength As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As DevComponents.DotNetBar.ButtonX
End Class
