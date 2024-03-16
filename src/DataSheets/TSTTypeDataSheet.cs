// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class TSTTypeDataSheet : DataSheet
    {
        protected override void Save(DataStore store)
        {
            base.Save(store);

            ChartingUtilities.UpdateTSTClassIfRequired(store, this.Project);
            ChartingUtilities.ClearTSTClassUpdateTag(this.Project);
        }

        public override string GetDeleteRowsConfirmation()
        {
            if (ProjectUtilities.ProjectHasResults(this.Project))
            {
                return MessageStrings.PROMPT_TST_TYPE_CHANGE;
            }
            else
            {
                return null;
            }
        }

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);
            ChartingUtilities.SetTSTClassUpdateTag(this.Project);
        }

        protected override void OnRowsDeleted(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsDeleted(sender, e);
            ChartingUtilities.SetTSTClassUpdateTag(this.Project);
        }

        protected override void OnRowsModified(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsModified(sender, e);
            ChartingUtilities.SetTSTClassUpdateTag(this.Project);
        }
    }
}
