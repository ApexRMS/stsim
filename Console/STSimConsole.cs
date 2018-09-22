// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using SyncroSim.Core;
using System.Reflection;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal partial class STSimConsole : SyncroSimConsole
    {
        protected override void Execute()
        {
            if (this.GetArguments().Count() == 1)
            {
                System.Console.WriteLine("Use the --help switch to see available options.");
                return;
            }

            if (this.IsSwitchArgument("list-reports"))
            {
                this.HandleListReportsArgument();
            }
            else if (this.IsSwitchArgument("create-report"))
            {
                this.HandleCreateReportArgument();
            }
            else if (this.IsSwitchArgument("spatial-split"))
            {
                this.HandleSpatialSplitArgument();
            }
            else
            {
                if (this.Help)
                {
                    PrintConsoleHelp();
                }
            }
        }

        private static void PrintConsoleHelp()
        {
            System.Console.WriteLine("ST-Sim Console [Arguments]");
            System.Console.WriteLine();
            System.Console.WriteLine("  --list-reports     Lists available ST-Sim reports");
            System.Console.WriteLine("  --create-report    Creates an ST-Sim report");
            System.Console.WriteLine("  --spatial-split    Splits an ST-Sim library spatially");
            System.Console.WriteLine("  --help             Prints help for an argument");
        }
    }
}
