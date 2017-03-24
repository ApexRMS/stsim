'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Transition Attribute Type
''' </summary>
''' <remarks></remarks>
Class TransitionAttributeType

    Private m_TransitionAttributeId As Integer

    Public Sub New(ByVal transitionAttributeId As Integer)
        Me.m_TransitionAttributeId = transitionAttributeId
    End Sub

    Public ReadOnly Property TransitionAttributeId As Integer
        Get
            Return Me.m_TransitionAttributeId
        End Get
    End Property

End Class
