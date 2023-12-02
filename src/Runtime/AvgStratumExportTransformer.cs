// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class AvgStratumExportTransformer : Core.Forms.MultibandExportTransformer
    {
        public override void Configure()
        {
            base.Configure();

            this.MonitorDataSheet(
                Strings.DATASHEET_TERMINOLOGY_NAME,
                this.OnTerminologyChanged,
                true);
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            string t = Convert.ToString(
                e.GetValue("PrimaryStratumLabel", "Stratum"),
                CultureInfo.InvariantCulture);

            this.DisplayName = string.Format(CultureInfo.InvariantCulture,
                "{0} Probability", t);
        }
    }
}
