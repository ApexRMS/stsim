// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class PostProcessTransformer : Transformer
    {
        public override void Transform()
        {
            using (SyncroSimTransactionScope scope = Session.CreateTransactionScope())
            {
                using (DataStore store = this.Library.CreateDataStore())
                {
                    AgeUtilities.UpdateAgeClassWork(store, this.ResultScenario);
                }

                scope.Complete();
            }
        }
    }
}
