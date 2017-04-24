'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
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
