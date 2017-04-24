'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Windows.Forms
Imports SyncroSim.Common.Forms

Class TransitionDiagramTabStripItem
    Inherits TabStripItem

    Private m_Control As Control
    Private m_RefreshRequired As Boolean = True
    Private m_IsDisposed As Boolean

    Public Sub New(ByVal text As String)
        MyBase.New(text)
    End Sub

    Public Property Control As Control
        Get
            Return Me.m_Control
        End Get
        Set(value As Control)
            Debug.Assert(Me.m_Control Is Nothing)
            Me.m_Control = value
        End Set
    End Property

    Public Property RefreshRequired As Boolean
        Get
            Return Me.m_RefreshRequired
        End Get
        Set(value As Boolean)
            Me.m_RefreshRequired = value
        End Set
    End Property

    Protected Overrides Sub Dispose(disposing As Boolean)

        If (disposing And (Not Me.m_IsDisposed)) Then

            If (Me.m_Control IsNot Nothing) Then

                Me.m_Control.Dispose()
                Me.m_Control = Nothing

            End If

            Me.m_IsDisposed = True

        End If

        MyBase.Dispose(disposing)

    End Sub

End Class
