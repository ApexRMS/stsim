'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core

Partial Class STSimConsole

    Public Sub New(ByVal session As Session, ByVal args As String())
        MyBase.New(session, args)
    End Sub

    Private Sub HandleListReportsArgument()

        If (Me.Help) Then
            System.Console.WriteLine("Lists available ST-Sim reports.")
        Else

            System.Console.WriteLine("Available reports:")
            System.Console.WriteLine()
            System.Console.WriteLine(STATECLASS_SUMMARY_REPORT_NAME)
            System.Console.WriteLine(TRANSITION_SUMMARY_REPORT_NAME)
            System.Console.WriteLine(TRANSITION_STATECLASS_SUMMARY_REPORT_NAME)
            System.Console.WriteLine(STATE_ATTRIBUTE_REPORT_NAME)
            System.Console.WriteLine(TRANSITION_ATTRIBUTE_REPORT_NAME)

        End If

    End Sub

End Class
