Module NullableUtilities

    Public Function NullableIdsEqual(value1 As Nullable(Of Integer), value2 As Nullable(Of Integer)) As Boolean

        Dim s1 As Integer = 0
        Dim s2 As Integer = 0

        If (value1.HasValue) Then
            s1 = value1.Value
            Debug.Assert(s1 > 0)
        End If

        If (value2.HasValue) Then
            s2 = value2.Value
            Debug.Assert(s2 > 0)
        End If

        Return (s1 = s2)

    End Function

End Module
