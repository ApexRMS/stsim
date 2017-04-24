'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
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

''' <summary>
''' Compare Metadata Result
''' </summary>
Public Enum CompareMetadataResult
    Same
    UnimportantDifferences
    ImportantDifferences
End Enum


