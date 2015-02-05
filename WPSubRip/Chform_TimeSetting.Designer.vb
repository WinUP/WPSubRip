<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Chform_TimeSetting
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
        Dim KnobColorTable1 As DevComponents.Instrumentation.KnobColorTable = New DevComponents.Instrumentation.KnobColorTable()
        Dim LinearGradientColorTable1 As DevComponents.Instrumentation.Primitives.LinearGradientColorTable = New DevComponents.Instrumentation.Primitives.LinearGradientColorTable()
        Dim LinearGradientColorTable2 As DevComponents.Instrumentation.Primitives.LinearGradientColorTable = New DevComponents.Instrumentation.Primitives.LinearGradientColorTable()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.KnobControl1 = New DevComponents.Instrumentation.KnobControl()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX3 = New DevComponents.DotNetBar.ButtonX()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "每次微调的时间跨度(秒)："
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.Location = New System.Drawing.Point(78, 228)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(188, 30)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 1
        Me.ButtonX1.Text = "确定"
        '
        'KnobControl1
        '
        LinearGradientColorTable1.End = System.Drawing.Color.DarkGreen
        LinearGradientColorTable1.GradientAngle = 40
        LinearGradientColorTable1.Start = System.Drawing.Color.LightGreen
        KnobColorTable1.KnobFaceColor = LinearGradientColorTable1
        LinearGradientColorTable2.End = System.Drawing.Color.Green
        LinearGradientColorTable2.GradientAngle = 40
        KnobColorTable1.KnobIndicatorColor = LinearGradientColorTable2
        KnobColorTable1.MajorTickColor = System.Drawing.Color.Gray
        KnobColorTable1.MinorTickColor = System.Drawing.Color.Gray
        KnobColorTable1.ZoneIndicatorColor = System.Drawing.Color.Green
        Me.KnobControl1.KnobColor = KnobColorTable1
        Me.KnobControl1.KnobStyle = DevComponents.Instrumentation.eKnobStyle.Style4
        Me.KnobControl1.Location = New System.Drawing.Point(155, 10)
        Me.KnobControl1.MajorTickAmount = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.KnobControl1.MaxValue = New Decimal(New Integer() {5, 0, 0, 0})
        Me.KnobControl1.MaxZonePercentage = 0
        Me.KnobControl1.MinValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.KnobControl1.Name = "KnobControl1"
        Me.KnobControl1.SelectionDecimals = 3
        Me.KnobControl1.Size = New System.Drawing.Size(182, 177)
        Me.KnobControl1.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(219, 170)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 17)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "0.000"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX2.Location = New System.Drawing.Point(189, 164)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(24, 23)
        Me.ButtonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX2.TabIndex = 16
        Me.ButtonX2.Text = "-"
        '
        'ButtonX3
        '
        Me.ButtonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX3.Location = New System.Drawing.Point(272, 164)
        Me.ButtonX3.Name = "ButtonX3"
        Me.ButtonX3.Size = New System.Drawing.Size(24, 23)
        Me.ButtonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX3.TabIndex = 0
        Me.ButtonX3.Text = "+"
        '
        'Chform_TimeSetting
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(340, 270)
        Me.Controls.Add(Me.ButtonX3)
        Me.Controls.Add(Me.ButtonX2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.KnobControl1)
        Me.Controls.Add(Me.ButtonX1)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Chform_TimeSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "时间轴微调设置"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents KnobControl1 As DevComponents.Instrumentation.KnobControl
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX3 As DevComponents.DotNetBar.ButtonX
End Class
