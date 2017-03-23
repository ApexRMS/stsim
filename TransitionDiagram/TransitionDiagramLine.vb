'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Drawing
Imports SyncroSim.Common.Forms

Class TransitionDiagramLine
    Inherits ConnectorLine

    Public Sub New(ByVal lineColor As Color)
        MyBase.New(lineColor)
    End Sub

End Class
