'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Drawing

<Serializable()>
Class TransitionDiagramClipData
    Public Entries As New List(Of TransitionDiagramClipDataEntry)
End Class

<Serializable()>
Class DeterministicTransitionClipData

    Public StratumSource As String
    Public StateClassSource As String
    Public StratumDest As String
    Public StateClassDest As String

    Public StratumIdSource As Nullable(Of Integer)
    Public StateClassIdSource As Integer
    Public StratumIdDest As Nullable(Of Integer)
    Public StateClassIdDest As Nullable(Of Integer)
    Public AgeMin As Nullable(Of Integer)
    Public AgeMax As Nullable(Of Integer)

End Class

<Serializable()>
Class ProbabilisticTransitionClipData

    Public StratumSource As String
    Public StateClassSource As String
    Public StratumDest As String
    Public StateClassDest As String
    Public TransitionType As String

    Public StratumIdSource As Nullable(Of Integer)
    Public StateClassIdSource As Integer
    Public StratumIdDest As Nullable(Of Integer)
    Public StateClassIdDest As Nullable(Of Integer)
    Public TransitionTypeId As Integer
    Public Probability As Double
    Public Proportion As Nullable(Of Double)
    Public AgeMin As Nullable(Of Integer)
    Public AgeMax As Nullable(Of Integer)
    Public AgeRelative As Nullable(Of Integer)
    Public AgeReset As Nullable(Of Boolean)
    Public TstMin As Nullable(Of Integer)
    Public TstMax As Nullable(Of Integer)
    Public TstRelative As Nullable(Of Integer)

End Class

<Serializable()>
Class TransitionDiagramClipDataEntry

    Public ShapeData As New DeterministicTransitionClipData
    Public IncomingDT As New List(Of DeterministicTransitionClipData)
    Public IncomingPT As New List(Of ProbabilisticTransitionClipData)
    Public OutgoingPT As New List(Of ProbabilisticTransitionClipData)
    Public Row As Integer
    Public Column As Integer
    Public Bounds As Rectangle

End Class
