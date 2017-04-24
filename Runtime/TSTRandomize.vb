'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' TST Randomize class
''' </summary>
''' <remarks></remarks>
Friend Class TstRandomize

    Private m_MaxInitialTst As Integer
    Private m_MinInitialTst As Integer

    Public Sub New(
        ByVal minInitialTst As Integer,
        ByVal maxInitialTst As Integer)

        Me.m_MinInitialTst = minInitialTst
        Me.m_MaxInitialTst = maxInitialTst

    End Sub

    Public ReadOnly Property MaxInitialTst As Integer
        Get
            Return Me.m_MaxInitialTst
        End Get
    End Property

    Public ReadOnly Property MinInitialTst As Integer
        Get
            Return m_MinInitialTst
        End Get
    End Property
End Class

