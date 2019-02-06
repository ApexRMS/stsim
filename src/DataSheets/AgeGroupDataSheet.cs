// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class AgeGroupDataSheet : DataSheet
    {
        protected override void Save(DataStore store)
        {
            base.Save(store);

            AgeUtilities.UpdateAgeClassIfRequired(store, this.Project);
            AgeUtilities.ClearAgeClassUpdateTag(this.Project);
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
            AgeUtilities.SetAgeClassUpdateTag(this.Project);
        }

        protected override void OnRowsDeleted(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsDeleted(sender, e);
            AgeUtilities.SetAgeClassUpdateTag(this.Project);
        }

        protected override void OnRowsModified(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsModified(sender, e);
            AgeUtilities.SetAgeClassUpdateTag(this.Project);
        }
    }
}
