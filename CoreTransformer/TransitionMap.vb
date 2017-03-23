'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Class TransitionMap
    Inherits STSimMapBase2(Of TransitionCollection)

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal transitions As TransitionCollection)

        MyBase.New(scenario)

        For Each t As Transition In transitions
            AddTransition(t)
        Next

    End Sub

    Public Function GetTransitions(
        ByVal stratumId As Integer,
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionCollection

        Return Me.GetItem(stratumId, stateClassId, iteration, timestep)

    End Function

    Private Sub AddTransition(ByVal t As Transition)

        Dim c As TransitionCollection =
            Me.GetItemExact(t.StratumIdSource, t.StateClassIdSource, t.Iteration, t.Timestep)

        If (c Is Nothing) Then

            c = New TransitionCollection()
            Me.AddItem(t.StratumIdSource, t.StateClassIdSource, t.Iteration, t.Timestep, c)

        End If

        c.Add(t)
        Debug.Assert(Me.HasItems())

    End Sub

End Class
