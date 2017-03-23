'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Initial Conditions Distirbution collection
''' </summary>
Friend Class InitialConditionsSpatialCollection
    Inherits Collection(Of InitialConditionsSpatial)

    ''' <summary>
    ''' Get a collection of InitialConditionSpatial objects for the specified Iteration
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetForIteration(iteration As Nullable(Of Integer)) As InitialConditionsSpatialCollection
        Dim icds As New InitialConditionsSpatialCollection
        For Each icd In Me
            If (Nullable.Equals(icd.Iteration, iteration)) Then
                icds.Add(icd)
            End If
        Next

        Return icds
    End Function

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

