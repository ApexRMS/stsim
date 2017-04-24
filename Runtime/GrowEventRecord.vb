'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' Grow event record class
''' </summary>
''' <remarks></remarks>
Class GrowEventRecord

    Private m_Cell As Cell
    Private m_TravelTime As Double
    Private m_Likelihood As Double

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="travelTime"></param>
    ''' <param name="likelihood"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cell As Cell, ByVal travelTime As Double, ByVal likelihood As Double)

        Me.m_Cell = cell
        Me.m_TravelTime = travelTime
        Me.m_Likelihood = likelihood

        Debug.Assert(Me.m_Likelihood > 0.0)

    End Sub

    ''' <summary>
    ''' Gets the cell fpr this grow event record
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Cell As Cell
        Get
            Return Me.m_Cell
        End Get
    End Property

    ''' <summary>
    ''' Gets the travel time for this grow event record
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TravelTime As Double
        Get
            Return Me.m_TravelTime
        End Get
    End Property

    ''' <summary>
    ''' Gets the likelihood this grow event record
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Likelihood As Double
        Get
            Return Me.m_Likelihood
        End Get
    End Property

End Class