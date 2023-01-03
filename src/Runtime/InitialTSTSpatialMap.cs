// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.Common;
using System.Globalization;
using SyncroSim.StochasticTime;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class InitialTSTSpatialMap : STSimMapBase
    {
        private bool m_HasItems;

        private Dictionary<string, StochasticTimeRaster> m_Rasters = 
            new Dictionary<string, StochasticTimeRaster>();

        private MultiLevelKeyMap1<SortedKeyMap1<InitialTSTSpatial>> m_Map = 
            new MultiLevelKeyMap1<SortedKeyMap1<InitialTSTSpatial>>();

        private DataSheet m_DataSheet;
        private InputRasters m_InputRasters;

        new public bool HasItems
        {
            get
            {
                return this.m_HasItems;
            }
        }

        public InitialTSTSpatialMap(
            InitialTSTSpatialCollection items, 
            Scenario scenario, 
            InputRasters inputRasters) : base(scenario)
        {
            this.m_DataSheet = scenario.GetDataSheet(Strings.DATASHEET_INITIAL_TST_SPATIAL_NAME);
            this.m_InputRasters = inputRasters;

            foreach (InitialTSTSpatial item in items)
            {
                this.AddItem(item);
            }
        }

        private void AddItem(InitialTSTSpatial item)
        {
            SortedKeyMap1<InitialTSTSpatial> m = this.m_Map.GetItemExact(item.TSTGroupId);

            if (m == null)
            {
                m = new SortedKeyMap1<InitialTSTSpatial>(SearchMode.ExactPrev);
                this.m_Map.AddItem(item.TSTGroupId, m);
            }

            InitialTSTSpatial v = m.GetItemExact(item.Iteration);

            if (v != null)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    "A record already exists for Transition Group={0} and iteration={1}.",
                    this.GetTransitionGroupName(item.TSTGroupId),
                    STSimMapBase.FormatValue(item.Iteration));

                throw new ArgumentException(msg);
            }

            m.AddItem(item.Iteration, item);
            this.m_HasItems = true;
        }

        public StochasticTimeRaster GetRaster(int transitionGroupId, int iteration)
        {
            if (!this.m_HasItems)
            {
                return null;
            }

            SortedKeyMap1<InitialTSTSpatial> m = this.m_Map.GetItem(transitionGroupId);

            if (m == null)
            {
                return null;
            }

            InitialTSTSpatial v = m.GetItem(iteration);

            if (v == null)
            {
                return null;
            }

            string FullFileName = Spatial.GetSpatialInputFileName(this.m_DataSheet, v.FileName, false);

            if (!this.m_Rasters.ContainsKey(FullFileName))
            {
                string CmpMsg = null;
                StochasticTimeRaster r = new StochasticTimeRaster(FullFileName, RasterDataType.DTInteger);
                CompareMetadataResult cmpResult = this.m_InputRasters.CompareMetadata(r, ref CmpMsg);

                if (cmpResult == CompareMetadataResult.RowColumnMismatch)
                {
                    string Message = string.Format(CultureInfo.InvariantCulture, 
                        MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, 
                        v.FileName, CmpMsg);

                    throw new STSimException(Message);
                }
                else if (cmpResult == CompareMetadataResult.UnimportantDifferences)
                {
                    string Message = string.Format(CultureInfo.InvariantCulture, 
                        MessageStrings.STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, 
                        v.FileName, CmpMsg);

                    this.Scenario.RecordStatus(StatusType.Information, Message);
                }

                this.m_Rasters.Add(FullFileName, r);
            }

            return this.m_Rasters[FullFileName];
        }
    }
}
