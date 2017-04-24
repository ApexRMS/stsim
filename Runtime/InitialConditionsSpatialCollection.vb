'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Initial Conditions Distirbution collection
''' </summary>
Friend Class InitialConditionsSpatialCollection
    Inherits Collection(Of InitialConditionsSpatial)

    ''' <summary>
    ''' Get a Sorted List of Iterations contained in this collection
    ''' </summary>
    ''' <returns>A list of iterations</returns>
    ''' <remarks></remarks>
    Public Function GetSortedIterationList() As List(Of Nullable(Of Integer))

        Dim lstIterations As New List(Of Nullable(Of Integer))
        For Each icd As InitialConditionsSpatial In Me
            Dim iteration = icd.Iteration
            If Not lstIterations.Contains(iteration) Then
                lstIterations.Add(iteration)
            End If
        Next

        'Sort Ascending with Null at start
        lstIterations.Sort()
        Return lstIterations

    End Function

End Class

