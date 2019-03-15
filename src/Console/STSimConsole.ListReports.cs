// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class STSimConsole
    {
        public STSimConsole(Session session, string[] args) : base(session, args)
        {
        }

        private void HandleListReportsArgument()
        {
            if (this.Help)
            {
                System.Console.WriteLine("Lists available ST-Sim reports.");
            }
            else
            {
                System.Console.WriteLine("Available reports:");
                System.Console.WriteLine();
                System.Console.WriteLine(Strings.STATECLASS_SUMMARY_REPORT_NAME);
                System.Console.WriteLine(Strings.TRANSITION_SUMMARY_REPORT_NAME);
                System.Console.WriteLine(Strings.TRANSITION_STATECLASS_SUMMARY_REPORT_NAME);
                System.Console.WriteLine(Strings.STATE_ATTRIBUTE_REPORT_NAME);
                System.Console.WriteLine(Strings.TRANSITION_ATTRIBUTE_REPORT_NAME);
            }
        }
    }
}
