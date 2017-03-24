'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Globalization

Partial Class TransitionDiagram

    Private Shared Sub LocationToRowCol(ByVal location As String, ByRef row As Integer, ByRef column As Integer)

        Dim LocUpper As String = location.ToUpper(CultureInfo.InvariantCulture)

        Dim CharPart As String = Mid(LocUpper, 1, 1)
        Dim NumPart As String = Mid(LocUpper, 2, LocUpper.Length - 1)

        Dim chars() As Char = CharPart.ToCharArray()
        Dim c As Char = chars(0)
        Dim CharVal As Integer = (Asc(c) - Asc("A"))

        column = CharVal
        row = CInt(NumPart) - 1

        Debug.Assert(column >= 0 And row >= 0)

    End Sub

    Private Shared Function RowColToLocation(ByVal row As Integer, ByVal column As Integer) As String

        Debug.Assert(column < 26)

        Dim s As String = CStr(ChrW(Asc("A") + column))
        s = s & (row + 1).ToString(CultureInfo.InvariantCulture)

        Return s

    End Function

    Private Sub RecordStateClassLocation(
        ByVal shape As StateClassShape,
        ByVal analyzer As DTAnalyzer)

        Debug.Assert(Me.WorkspaceRectangle.Contains(shape.Bounds))

        Dim row As DataRow = analyzer.GetStateClassRow(Me.m_StratumId, shape.StateClassIdSource)
        row(DATASHEET_DT_LOCATION_COLUMN_NAME) = RowColToLocation(shape.Row, shape.Column)

    End Sub

    Private Sub RecordNewStateClassLocations(ByVal analyzer As DTAnalyzer)

        For Each Shape As StateClassShape In Me.SelectedShapes

            If (Not Shape.IsStatic) Then
                Me.RecordStateClassLocation(Shape, analyzer)
            End If

        Next

    End Sub

    Private Function GetNextStateClassLocation() As String

        If (Me.GetShapeAt(Me.CurrentMouseRow, Me.CurrentMouseColumn) Is Nothing) Then

            Dim ColLetter As String = CStr(Chr(Asc("A") + Me.CurrentMouseColumn))
            Dim RowLetter As String = CStr(Me.CurrentMouseRow + 1)

            Return ColLetter & RowLetter

        End If

        For col As Integer = 0 To TRANSITION_DIAGRAM_MAX_COLUMNS - 1

            For row As Integer = 0 To TRANSITION_DIAGRAM_MAX_ROWS - 1

                If (Me.GetShapeAt(row, col) Is Nothing) Then

                    Dim ColLetter As String = CStr(Chr(Asc("A") + col))
                    Dim RowLetter As String = CStr(row + 1)

                    Return (ColLetter & RowLetter)

                End If

            Next

        Next

        Return Nothing

    End Function

    Private Function GetStateClassId(ByVal slxid As Integer, ByVal slyid As Integer) As Integer

        For Each dr As DataRow In Me.m_SCDataSheet.GetData().Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim xid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME))
            Dim yid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME))

            If (xid = slxid And yid = slyid) Then
                Return CInt(dr(Me.m_SCDataSheet.ValueMember))
            End If

        Next

        Return -1

    End Function

    Private Shared Function IsTransitionGroupFilterApplied(ByVal criteria As TransitionFilterCriteria) As Boolean

        For Each b As Boolean In criteria.TransitionGroups.Values

            If (Not b) Then
                Return True
            End If

        Next

        Return False

    End Function

End Class
