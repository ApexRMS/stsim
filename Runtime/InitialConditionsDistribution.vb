'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' Initial Conditions Distribution
''' </summary>
''' <remarks></remarks>
Friend Class InitialConditionsDistribution

    Private m_StratumId As Integer
    Private m_Iteration As Nullable(Of Integer)
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_TertiaryStratumId As Nullable(Of Integer)
    Private m_StateClassId As Integer
    Private m_AgeMin As Integer
    Private m_AgeMax As Integer
    Private m_RelativeAmount As Double

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal iteration As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal ageMin As Integer,
        ByVal ageMax As Integer,
        ByVal relativeAmount As Double)

        Me.m_StratumId = stratumId
        Me.m_Iteration = iteration
        Me.m_SecondaryStratumId = secondaryStratumId
        Me.m_TertiaryStratumId = tertiaryStratumId
        Me.m_StateClassId = stateClassId
        Me.m_AgeMin = ageMin
        Me.m_AgeMax = ageMax
        Me.m_RelativeAmount = relativeAmount

    End Sub

    ''' <summary>
    ''' Gets the stratum Id
    ''' </summary>
    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the Iteration
    ''' </summary>
    Public ReadOnly Property Iteration As Nullable(Of Integer)
        Get
            Return m_Iteration
        End Get
    End Property

    ''' <summary>
    ''' Gets the Secondary Stratum Id
    ''' </summary>
    Public ReadOnly Property SecondaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_SecondaryStratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the Tertiary Stratum Id
    ''' </summary>
    Public ReadOnly Property TertiaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_TertiaryStratumId
        End Get
    End Property

    ''' <summary>
    ''' StateClass Id for the cell
    ''' </summary>
    Public ReadOnly Property StateClassId As Integer
        Get
            Return Me.m_StateClassId
        End Get
    End Property

    ''' <summary>
    ''' Minimum age for the pathway
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property AgeMin As Integer
        Get
            Return Me.m_AgeMin
        End Get
    End Property

    ''' <summary>
    ''' Maximum age for the pathway
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property AgeMax As Integer
        Get
            Return Me.m_AgeMax
        End Get
    End Property

    ''' <summary>
    ''' Total amount (e.g. area) for the simulation
    ''' </summary>
    Public ReadOnly Property RelativeAmount As Double
        Get
            Return Me.m_RelativeAmount
        End Get
    End Property

End Class

