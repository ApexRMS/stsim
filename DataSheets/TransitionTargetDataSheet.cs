// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Globalization;
using System.Reflection;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal class TransitionTargetDataSheet : DataSheet
    {
        protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
        {
            base.OnDataSheetChanged(e);

            string AmountLabel = null;
            TerminologyUnit AmountUnits = TerminologyUnit.None;

            TerminologyUtilities.GetAmountLabelTerminology(e.DataSheet, ref AmountLabel, ref AmountUnits);

            this.Columns[Strings.DATASHEET_AMOUNT_COLUMN_NAME].DisplayName = (string.Format(CultureInfo.InvariantCulture, "Target {0} ({1})", AmountLabel, TerminologyUtilities.TerminologyUnitToString(AmountUnits)));
            this.Columns[Strings.DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Target {0} Distribution", AmountLabel);
            this.Columns[Strings.DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Target {0} Sampling Frequency", AmountLabel);
            this.Columns[Strings.DATASHEET_DISTRIBUTIONSD_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Target {0} SD", AmountLabel);
            this.Columns[Strings.DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Target {0} Min", AmountLabel);
            this.Columns[Strings.DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Target {0} Max", AmountLabel);
        }
    }
}
