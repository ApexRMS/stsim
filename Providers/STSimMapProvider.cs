// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.StochasticTime.Forms;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal class STSimMapProvider : MapProvider
    {
        public override void CreateColorMaps(Project project)
        {
            //STATECLASS Color Map and Legend Map
            var LegendColors = SpatialUtilities.CreateLegendMap(project, Constants.SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, Strings.DATASHEET_STATECLASS_NAME);
            SpatialUtilities.CreateColorMap(project, Constants.SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, Strings.DATASHEET_STATECLASS_NAME, LegendColors);

            //Primary Stratum Color Map and Legend Map
            LegendColors = SpatialUtilities.CreateLegendMap(project, Constants.SPATIAL_MAP_STRATUM_VARIABLE_NAME, Strings.DATASHEET_STRATA_NAME);
            SpatialUtilities.CreateColorMap(project, Constants.SPATIAL_MAP_STRATUM_VARIABLE_NAME, Strings.DATASHEET_STRATA_NAME, LegendColors);

            //Transition Groups Color Map and Legend Map
            SpatialUtilities.CreateTransitionGroupMaps(project);

            //Age  Color Map 
            SpatialUtilities.CreateAgeColorMap(project);
        }

        public override void RefreshCriteria(SyncroSimLayout layout, Project project)
        {
            using (DataStore store = project.Library.CreateDataStore())
            {
                SyncroSimLayoutItem g0 = new SyncroSimLayoutItem("BasicGroup", "Basic", true);
                SyncroSimLayoutItem g1 = new SyncroSimLayoutItem("TransitionsGroup", "Transitions", true);
                SyncroSimLayoutItem g2 = new SyncroSimLayoutItem("AATPGroup", "Average Annual Probability", true);
                SyncroSimLayoutItem g3 = new SyncroSimLayoutItem("StateAttributeGroup", "State Attributes", true);
                SyncroSimLayoutItem g4 = new SyncroSimLayoutItem("TransitionAttributeGroup", "Transition Attributes", true);

                DataView AttrGroupView = CreateMapAttributeGroupsView(project, store);

                //Categorical
                AddCategoricalVariables(project, g0);

                //Transitions
                AddMapTransitionGroupVariables(project, store, g1.Items, "STSim_OutputSpatialTransition", "Filename", "TransitionGroupID", "(Transitions)", Constants.SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX, Strings.DATASHEET_TRANSITION_TYPE_NAME);

                //Average Annual Transition Probability
                AddMapTransitionGroupVariables(project, store, g2.Items, "STSim_OutputSpatialAverageTransitionProbability", "Filename", "TransitionGroupID", "(Avg. Annual Prob. - All Iterations)", Constants.SPATIAL_MAP_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX, null);

                //State Attributes
                AddMapStateAttributes(g3.Items, project, store, AttrGroupView);

                //Transition Attributes
                AddMapTransitionAttributes(g4.Items, project, store, AttrGroupView);

                layout.Items.Add(g0);

                if (g1.Items.Count > 0)
                {
                    layout.Items.Add(g1);
                }

                if (g2.Items.Count > 0)
                {
                    layout.Items.Add(g2);
                }

                if (g3.Items.Count > 0)
                {
                    layout.Items.Add(g3);
                }

                if (g4.Items.Count > 0)
                {
                    layout.Items.Add(g4);
                }
            }
        }

        private static DataView CreateMapAttributeGroupsView(Project project, DataStore store)
        {
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);

            DataView View = new DataView(ds.GetData(store), null, ds.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);

            return View;
        }

        private static void AddCategoricalVariables(Project project, SyncroSimLayoutItem g0)
        {
            string psl = null;
            string ssl = null;
            string tsl = null;
            DataSheet dsterm = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref psl, ref ssl, ref tsl);

            SyncroSimLayoutItem i1 = new SyncroSimLayoutItem(Constants.SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, "State Class", false);
            SyncroSimLayoutItem i2 = new SyncroSimLayoutItem(Constants.SPATIAL_MAP_AGE_VARIABLE_NAME, "Age", false);
            SyncroSimLayoutItem i3 = new SyncroSimLayoutItem(Constants.SPATIAL_MAP_STRATUM_VARIABLE_NAME, psl, false);

            i1.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputSpatialState"));
            i1.Properties.Add(new MetaDataProperty("column", "Filename"));
            i1.Properties.Add(new MetaDataProperty("colorMapSource", Strings.DATASHEET_STATECLASS_NAME));

            i2.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputSpatialAge"));
            i2.Properties.Add(new MetaDataProperty("column", "Filename"));
            i2.Properties.Add(new MetaDataProperty("colorMapSource", Strings.DATASHEET_AGE_GROUP_NAME));

            i3.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputSpatialStratum"));
            i3.Properties.Add(new MetaDataProperty("column", "Filename"));
            i3.Properties.Add(new MetaDataProperty("colorMapSource", Strings.DATASHEET_STRATA_NAME));

            g0.Items.Add(i1);
            g0.Items.Add(i2);
            g0.Items.Add(i3);
        }

        private static void AddMapTransitionGroupVariables(Project project, DataStore store, SyncroSimLayoutItemCollection items, string dataSheetName, string fileColumnName, string filterColumnName, string extendedIdentifier, string prefix, string colorMapSource)
        {
            DataSheet dstg = project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME);
            DataSheet dsttg = project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_GROUP_NAME);
            DataTable dttg = dstg.GetData(store);
            DataTable dtttg = dsttg.GetData(store);
            DataView dvtg = new DataView(dttg, null, dstg.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);

            foreach (DataRowView drv in dvtg)
            {
                int tgid = Convert.ToInt32(drv.Row[dstg.ValidationTable.ValueMember], CultureInfo.InvariantCulture);
                string query = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tgid);
                DataRow[] rows = dtttg.Select(query);

                foreach (DataRow dr in rows)
                {
                    if (DataSheetUtilities.IsPrimaryTypeByGroup(dr))
                    {
                        string DisplayName = Convert.ToString(drv.Row[dstg.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);
                        string VarName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", prefix, tgid);
                        SyncroSimLayoutItem Item = new SyncroSimLayoutItem(VarName, DisplayName, false);

                        Item.Properties.Add(new MetaDataProperty("dataSheet", dataSheetName));
                        Item.Properties.Add(new MetaDataProperty("column", fileColumnName));
                        Item.Properties.Add(new MetaDataProperty("filter", filterColumnName));
                        Item.Properties.Add(new MetaDataProperty("extendedIdentifier", extendedIdentifier));
                        Item.Properties.Add(new MetaDataProperty("itemId", tgid.ToString(CultureInfo.InvariantCulture)));
                        Item.Properties.Add(new MetaDataProperty("colorMapSource", colorMapSource));

                        items.Add(Item);

                        break;
                    }
                }
            }
        }

        private static void AddMapStateAttributes(SyncroSimLayoutItemCollection items, Project project, DataStore store, DataView attrGroupsView)
        {
            DataSheet StateAttrsDataSheet = project.GetDataSheet(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME);

            AddMapNonGroupedAttributes(
                store, items, StateAttrsDataSheet, 
                "STSim_OutputSpatialStateAttribute", "Filename", "StateAttributeTypeID",
                Constants.SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX, null);

            Dictionary<string, SyncroSimLayoutItem> GroupsDict = new Dictionary<string, SyncroSimLayoutItem>();
            List<SyncroSimLayoutItem> GroupsList = new List<SyncroSimLayoutItem>();

            foreach (DataRowView drv in attrGroupsView)
            {
                string GroupName = Convert.ToString(drv.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                SyncroSimLayoutItem Group = new SyncroSimLayoutItem(GroupName, GroupName, true);

                GroupsDict.Add(GroupName, Group);
                GroupsList.Add(Group);
            }

            AddMapGroupedAttributes(
                store, GroupsDict, StateAttrsDataSheet, 
                "STSim_OutputSpatialStateAttribute", "Filename", "StateAttributeTypeID", 
                Constants.SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX, null);

            foreach (SyncroSimLayoutItem g in GroupsList)
            {
                if (g.Items.Count > 0)
                {
                    items.Add(g);
                }
            }
        }

        private static void AddMapTransitionAttributes(SyncroSimLayoutItemCollection items, Project project, DataStore store, DataView attrGroupsView)
        {
            DataSheet TransitionAttrsDataSheet = project.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME);

            AddMapNonGroupedAttributes(
                store, items, TransitionAttrsDataSheet, 
                "STSim_OutputSpatialTransitionAttribute", "Filename", "TransitionAttributeTypeID", 
                Constants.SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX, null);

            Dictionary<string, SyncroSimLayoutItem> GroupsDict = new Dictionary<string, SyncroSimLayoutItem>();
            List<SyncroSimLayoutItem> GroupsList = new List<SyncroSimLayoutItem>();

            foreach (DataRowView drv in attrGroupsView)
            {
                string GroupName = Convert.ToString(drv.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                SyncroSimLayoutItem Group = new SyncroSimLayoutItem(GroupName, GroupName, true);

                GroupsDict.Add(GroupName, Group);
                GroupsList.Add(Group);
            }

            AddMapGroupedAttributes(
                store, GroupsDict, TransitionAttrsDataSheet, 
                "STSim_OutputSpatialTransitionAttribute", "Filename", "TransitionAttributeTypeID",
                Constants.SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX, null);

            foreach (SyncroSimLayoutItem g in GroupsList)
            {
                if (g.Items.Count > 0)
                {
                    items.Add(g);
                }
            }
        }

        private static void AddMapNonGroupedAttributes(DataStore store, SyncroSimLayoutItemCollection items, DataSheet attrsDataSheet, string dataSheetName, string fileColumnName, string filterColumnName, string prefix, string colorMapSource)
        {
            DataTable Table = attrsDataSheet.GetData(store);
            DataView View = new DataView(Table, null, attrsDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);
            Debug.Assert(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME == Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);

            foreach (DataRowView drv in View)
            {
                if (drv.Row[Strings.DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME] == DBNull.Value)
                {
                    int AttrId = Convert.ToInt32(drv.Row[attrsDataSheet.ValueMember], CultureInfo.InvariantCulture);
                    string AttrName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", prefix, AttrId);
                    string DisplayName = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (drv.Row[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME] != DBNull.Value)
                    {
                        DisplayName = string.Format(CultureInfo.InvariantCulture, 
                            "{0} ({1})", 
                            DisplayName, 
                            Convert.ToString(drv.Row[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME], CultureInfo.InvariantCulture));
                    }

                    SyncroSimLayoutItem Item = new SyncroSimLayoutItem(AttrName, DisplayName, false);

                    Item.Properties.Add(new MetaDataProperty("dataSheet", dataSheetName));
                    Item.Properties.Add(new MetaDataProperty("column", fileColumnName));
                    Item.Properties.Add(new MetaDataProperty("filter", filterColumnName));
                    Item.Properties.Add(new MetaDataProperty("itemId", AttrId.ToString(CultureInfo.InvariantCulture)));
                    Item.Properties.Add(new MetaDataProperty("colorMapSource", colorMapSource));

                    items.Add(Item);
                }
            }
        }

        private static void AddMapGroupedAttributes(DataStore store, Dictionary<string, SyncroSimLayoutItem> groupsDict, DataSheet attrsDataSheet, string dataSheetName, string fileColumnName, string filterColumnName, string prefix, string colorMapSource)
        {
            DataTable Table = attrsDataSheet.GetData(store);
            DataView View = new DataView(Table, null, attrsDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);
            DataSheet GroupsDataSheet = attrsDataSheet.Project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);
            Debug.Assert(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME == Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);

            foreach (DataRowView drv in View)
            {
                if (drv.Row[Strings.DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME] != DBNull.Value)
                {
                    int GroupId = Convert.ToInt32(drv.Row[Strings.DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    string GroupName = GroupsDataSheet.ValidationTable.GetDisplayName(GroupId);
                    int AttrId = Convert.ToInt32(drv.Row[attrsDataSheet.ValueMember], CultureInfo.InvariantCulture);
                    string AttrName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", prefix, AttrId);
                    string DisplayName = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (drv.Row[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME] != DBNull.Value)
                    {
                        DisplayName = string.Format(CultureInfo.InvariantCulture, 
                            "{0} ({1})", 
                            DisplayName, 
                            Convert.ToString(drv.Row[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME], CultureInfo.InvariantCulture));
                    }

                    SyncroSimLayoutItem Item = new SyncroSimLayoutItem(AttrName, DisplayName, false);

                    Item.Properties.Add(new MetaDataProperty("dataSheet", dataSheetName));
                    Item.Properties.Add(new MetaDataProperty("column", fileColumnName));
                    Item.Properties.Add(new MetaDataProperty("filter", filterColumnName));
                    Item.Properties.Add(new MetaDataProperty("itemId", AttrId.ToString(CultureInfo.InvariantCulture)));
                    Item.Properties.Add(new MetaDataProperty("colorMapSource", colorMapSource));

                    groupsDict[GroupName].Items.Add(Item);
                }
            }
        }
    }
}
