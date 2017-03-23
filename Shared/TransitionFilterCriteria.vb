'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Transition filter criteria class
''' </summary>
''' <remarks></remarks>
Class TransitionFilterCriteria

    Public IncludeDeterministic As Boolean = True
    Public IncludeProbabilistic As Boolean = True
    Public TransitionGroups As New Dictionary(Of Integer, Boolean)

End Class
