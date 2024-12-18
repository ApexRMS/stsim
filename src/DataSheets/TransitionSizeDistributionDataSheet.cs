// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal class TransitionSizeDistributionDataSheet : DataSheet
    {
        protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
        {
            base.OnDataSheetChanged(e);

            string AmountLabel = null;
            TerminologyUnit AmountUnits = TerminologyUnit.None;

            TerminologyUtilities.GetAmountLabelTerminology(this.Project, ref AmountLabel, ref AmountUnits);

            this.Columns[Strings.DATASHEET_TRANSITION_SIZE_DISTRIBUTION_MAXIMUM_AREA_COLUMN_NAME].DisplayName = 
                (string.Format(CultureInfo.InvariantCulture, "Maximum {0} ({1})", 
                    AmountLabel, TerminologyUtilities.TerminologyUnitToString(AmountUnits)));
        }
    }
}
