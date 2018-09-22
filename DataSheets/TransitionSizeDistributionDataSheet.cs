// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Reflection;
using System.Globalization;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal class TransitionSizeDistributionDataSheet : DataSheet
    {
        protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
        {
            base.OnDataSheetChanged(e);

            string AmountLabel = null;
            TerminologyUnit AmountUnits = TerminologyUnit.None;

            TerminologyUtilities.GetAmountLabelTerminology(e.DataSheet, ref AmountLabel, ref AmountUnits);

            this.Columns[Strings.DATASHEET_TRANSITION_SIZE_DISTRIBUTION_MAXIMUM_AREA_COLUMN_NAME].DisplayName = 
                (string.Format(CultureInfo.InvariantCulture, "Maximum {0} ({1})", 
                    AmountLabel, TerminologyUtilities.TerminologyUnitToString(AmountUnits)));
        }
    }
}
