Namespace AppCore

    ''' <summary>
    ''' 状态常量
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum StateInteger As Integer
        _COMPUTING_
        _COMPLETE_
        _ERROR_
        _EMPTYFILE_
        _NOPERATION_
        _CANCEL_
    End Enum

    Public Enum ImportMode As Integer
        _TIMELINE_
        _SUBTITLE_
        _SUBRIP_
    End Enum

End Namespace