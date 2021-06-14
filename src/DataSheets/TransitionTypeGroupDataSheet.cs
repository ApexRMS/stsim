// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Data;
using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionTypeGroupDataSheet : DataSheet
    {
        public override void DeleteRows(IEnumerable<DataRow> rows)
        {
            List<DataRow> l = new List<DataRow>();

            foreach (DataRow dr in rows)
            {
                if (!DataTableUtilities.GetDataBool(dr, Strings.IS_AUTO_COLUMN_NAME))
                {
                    l.Add(dr);
                }
            }

            if (l.Count > 0)
            {
                base.DeleteRows(l);
            }
        }
    }
}
