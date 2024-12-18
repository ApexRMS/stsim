﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class STSimResultsProvider : LayoutProvider
    {
        protected override void ModifyLayout(Layout layout)
        {
            if (this.Library == null)
            {
                Debug.Assert(false);
                return;
            }

            if (this.Library.ActiveProject == null)
            {
                Debug.Assert(false);
                return;
            }

            LayoutItem PrimaryStrataGroup = layout.Items.FindItem("stsim_PrimaryStrata", true);

            if (PrimaryStrataGroup == null)
            {
                Debug.Assert(false);
                return;                
            }

            string psl = null;
            string ssl = null;
            string tsl = null;

            TerminologyUtilities.GetStratumLabelTerminology(this.Library.ActiveProject, ref psl, ref ssl, ref tsl);

            PrimaryStrataGroup.DisplayName = psl;
        }
    }
}
