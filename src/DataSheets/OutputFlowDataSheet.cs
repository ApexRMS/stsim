// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Reflection;
using System.Globalization;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal class OutputFlowDataSheet : DataSheet
	{
		protected override void OnDataFeedsRefreshed(DataStore store)
		{
			base.OnDataFeedsRefreshed(store);

			string s = null;
			string ss = null;
			string ts = null;

			TerminologyUtilities.GetStratumLabelTerminology(this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref s, ref ss, ref ts);

			this.Columns[Strings.FROM_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "From {0}", s);
			this.Columns[Strings.FROM_SECONDARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "From {0}", ss);
			this.Columns[Strings.FROM_TERTIARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "From {0}", ts);

			this.Columns[Strings.TO_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "To {0}", s);

			this.Columns[Strings.END_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "End {0}", s);
			this.Columns[Strings.END_SECONDARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "End {0}", ss);
			this.Columns[Strings.END_TERTIARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "End {0}", ts);
		}
	}
}
