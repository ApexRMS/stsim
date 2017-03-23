'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.StochasticTime
Imports System.Collections.ObjectModel

''' <summary>
''' Initial Conditions Distirbution collection
''' </summary>
Friend Class InitialConditionsDistributionCollection
    Inherits Collection(Of InitialConditionsDistribution)

    ''' <summary>
    ''' Get a collection of InitialConditionDistribution objects for the specified Iteration
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetForIteration(iteration As Nullable(Of Integer)) As InitialConditionsDistributionCollection
        Dim icds As New InitialConditionsDistributionCollection
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
        For Each icd As InitialConditionsDistribution In Me
            Dim iteration = icd.Iteration
            If Not lstIterations.Contains(iteration) Then
                lstIterations.Add(iteration)
            End If
        Next

        'Sort Ascending with Null at start
        lstIterations.Sort()

        Return lstIterations

    End Function

    ''' <summary>
    ''' Get a Filtered Collection of InitialConditionsDistribution for specified parameter
    ''' </summary>
    ''' <returns>A Collection of InitialConditionsDistribution</returns>
    ''' <remarks></remarks>
    Public Function GetFiltered(cell As Cell) As InitialConditionsDistributionCollection

        Dim retVal As New InitialConditionsDistributionCollection()

        For Each icd As InitialConditionsDistribution In Me

            If cell.StratumId <> icd.StratumId Then
                Continue For
            End If

            If cell.StateClassId <> ApexRaster.DEFAULT_NO_DATA_VALUE Then
                If cell.StateClassId <> icd.StateClassId Then
                    Continue For
                End If
            End If

            If cell.SecondaryStratumId <> ApexRaster.DEFAULT_NO_DATA_VALUE Then
                If cell.SecondaryStratumId <> icd.SecondaryStratumId Then
                    Continue For
                End If
            End If

            If cell.Age <> ApexRaster.DEFAULT_NO_DATA_VALUE Then
                If cell.Age < icd.AgeMin Or cell.Age > icd.AgeMax Then
                    Continue For
                End If
            End If

            ' Passed all the tests, so we'll take this one
            retVal.Add(icd)

        Next

        Return retVal

    End Function

    Public Function CalcSumOfRelativeAmount() As Double

        Dim sumOfRelativeAmount As Double = 0.0

        For Each sis As InitialConditionsDistribution In Me
            sumOfRelativeAmount += sis.RelativeAmount
        Next

        Return sumOfRelativeAmount

    End Function

End Class

