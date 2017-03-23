﻿'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Class StratumTabStripItem
    Inherits TransitionDiagramTabStripItem

    Private m_StratumId As Nullable(Of Integer)

    Public Sub New(ByVal stratumName As String, ByVal stratumId As Nullable(Of Integer))

        MyBase.New(stratumName)
        Me.m_StratumId = stratumId

    End Sub

    Public ReadOnly Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
    End Property

End Class