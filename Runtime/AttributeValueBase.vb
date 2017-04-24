'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' Base attribute value class
''' </summary>
''' <remarks></remarks>
Friend Class AttributeValueBase

    Private m_AttributeTypeId As Integer
    Private m_StratumId As Nullable(Of Integer)
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_StateClassId As Nullable(Of Integer)
    Private m_MinimumAge As Nullable(Of Integer)
    Private m_MaximumAge As Nullable(Of Integer)
    Private m_Value As Double

    Public Sub New(
        ByVal attributeTypeId As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stateClassId As Nullable(Of Integer),
        ByVal minimumAge As Nullable(Of Integer),
        ByVal maximumAge As Nullable(Of Integer),
        ByVal value As Double)

        Me.m_AttributeTypeId = attributeTypeId
        Me.m_StratumId = stratumId
        Me.m_SecondaryStratumId = secondaryStratumId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StateClassId = StateClassId
        Me.m_MinimumAge = MinimumAge
        Me.m_MaximumAge = MaximumAge
        Me.m_Value = Value

    End Sub

    Public ReadOnly Property AttributeTypeId As Integer
        Get
            Return Me.m_AttributeTypeId
        End Get
    End Property

    Public ReadOnly Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public ReadOnly Property SecondaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_SecondaryStratumId
        End Get
    End Property

    Public ReadOnly Property Iteration As Nullable(Of Integer)
        Get
            Return Me.m_Iteration
        End Get
    End Property

    Public ReadOnly Property Timestep As Nullable(Of Integer)
        Get
            Return Me.m_Timestep
        End Get
    End Property

    Public ReadOnly Property StateClassId As Nullable(Of Integer)
        Get
            Return Me.m_StateClassId
        End Get
    End Property

    Public ReadOnly Property MinimumAge As Nullable(Of Integer)
        Get
            Return Me.m_MinimumAge
        End Get
    End Property

    Public ReadOnly Property MaximumAge As Nullable(Of Integer)
        Get
            Return Me.m_MaximumAge
        End Get
    End Property

    Public ReadOnly Property Value As Double
        Get
            Return Me.m_Value
        End Get
    End Property

End Class
