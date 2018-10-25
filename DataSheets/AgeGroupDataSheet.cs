﻿// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
            return MessageStrings.PROMPT_AGE_GROUP_CHANGE;
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
