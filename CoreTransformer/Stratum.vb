'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Stratum Class
''' </summary>
''' <remarks></remarks>
Friend Class Stratum

    Private m_StratumId As Integer
    Private m_Cells As New Dictionary(Of Integer, Cell)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="stratumId">The Id of the stratum</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal stratumId As Integer)
        Me.m_StratumId = stratumId
    End Sub

    ''' <summary>
    ''' Gets the stratum Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the cell collection for this stratum
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Cells As Dictionary(Of Integer, Cell)
        Get
            Return Me.m_Cells
        End Get
    End Property

End Class
