﻿'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Terminolgy Unit
''' </summary>
''' <remarks></remarks>
Friend Enum TerminologyUnit
    None = 0
    Acres
    Hectares
    SquareKilometers
    SquareMiles
End Enum

''' <summary>
''' Patch Prioritization Type
''' </summary>
''' <remarks></remarks>
Friend Enum PatchPrioritizationType
    Smallest = 0
    SmallestEdgesOnly
    Largest
    LargestEdgesOnly
End Enum

''' <summary>
''' Size Prioritization
''' </summary>
''' <remarks></remarks>
Friend Enum SizePrioritization
    None = 0
    Smallest
    Largest
End Enum

''' <summary>
''' Cardinal Direction
''' </summary>
''' <remarks></remarks>
Friend Enum CardinalDirection
    N = 0
    NE
    E
    SE
    S
    SW
    W
    NW
End Enum


