'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' Transition Event class
''' </summary>
''' <remarks></remarks>
Class TransitionEvent

    Private m_targetAmount As Double

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="targetAmount">The target size for the transition event</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal targetAmount As Double)
        Me.m_targetAmount = targetAmount
    End Sub

    ''' <summary>
    ''' Gets the target amount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TargetAmount As Double
        Get
            Return Me.m_targetAmount
        End Get
    End Property

End Class
