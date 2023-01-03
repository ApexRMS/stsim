// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class AgeGroupDataSheet : DataSheet
    {
        protected override void Save(DataStore store)
        {
            base.Save(store);

            ChartingUtilities.UpdateAgeClassIfRequired(store, this.Project);
            ChartingUtilities.ClearAgeClassUpdateTag(this.Project);
        }

        public override string GetDeleteRowsConfirmation()
        {
            if (ProjectUtilities.ProjectHasResults(this.Project))
            {
                return MessageStrings.PROMPT_AGE_GROUP_CHANGE;
            }
            else
            {
                return null;
            }              
        }

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);
            ChartingUtilities.SetAgeClassUpdateTag(this.Project);
        }

        protected override void OnRowsDeleted(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsDeleted(sender, e);
            ChartingUtilities.SetAgeClassUpdateTag(this.Project);
        }

        protected override void OnRowsModified(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsModified(sender, e);
            ChartingUtilities.SetAgeClassUpdateTag(this.Project);
        }
    }
}
