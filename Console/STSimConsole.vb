'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class STSimConsole
    Inherits SyncroSimConsole

    Public Overrides Sub Execute()

        If (Me.GetArguments.Count = 1) Then
            System.Console.WriteLine("Use the --help switch to see available options.")
            Return
        End If

        If (Me.IsSwitchArgument("list-reports")) Then
            Me.HandleListReportsArgument()
        ElseIf (Me.IsSwitchArgument("create-report")) Then
            Me.HandleCreateReportArgument()
        ElseIf (Me.IsSwitchArgument("spatial-split")) Then
            Me.HandleSpatialSplitArgument()
        Else
            If (Me.Help) Then
                PrintConsoleHelp()
            End If
        End If

    End Sub

    Private Shared Sub PrintConsoleHelp()

        System.Console.WriteLine("ST-Sim Console [Arguments]")
        System.Console.WriteLine()
        System.Console.WriteLine("  --list-reports     Lists available ST-Sim reports")
        System.Console.WriteLine("  --create-report    Creates an ST-Sim report")
        System.Console.WriteLine("  --spatial-split    Splits an ST-Sim library spatially")
        System.Console.WriteLine("  --help             Prints help for an argument")

    End Sub

End Class
