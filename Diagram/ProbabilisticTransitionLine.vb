'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Drawing

Class ProbabilisticTransitionLine
    Inherits TransitionDiagramLine

    Private m_TransitionTypeId As Integer
    Private m_TransitionGroups As New List(Of Integer)

    Public Sub New(ByVal transitionTypeId As Integer, ByVal lineColor As Color)

        MyBase.New(lineColor)
        Me.m_TransitionTypeId = transitionTypeId

    End Sub

    Friend ReadOnly Property TransitionTypeId() As Integer
        Get
            Return Me.m_TransitionTypeId
        End Get
    End Property

    Friend ReadOnly Property TransitionGroups() As List(Of Integer)
        Get
            Return Me.m_TransitionGroups
        End Get
    End Property

End Class
