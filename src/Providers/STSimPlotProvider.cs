// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class STSimPlotProvider : PlotProvider
    {
        static string AVG_ALL_ITER = "(Average for all Iterations)";
        static string AVG_PROB_ALL_ITER = "(Probability for all Iterations)";

        public override void CreateColorMaps(DataStore store, Project project)
        {
            //State Class Color Map and Legend Map
            var LegendColors = CreateLegendMap(project, Constants.SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, Strings.DATASHEET_STATECLASS_NAME, store);
            CreateColorMap(project, Constants.SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, Strings.DATASHEET_STATECLASS_NAME, LegendColors, store);

            //Primary Stratum Color Map and Legend Map
            LegendColors = CreateLegendMap(project, Constants.SPATIAL_MAP_STRATUM_VARIABLE_NAME, Strings.DATASHEET_STRATA_NAME, store);
            CreateColorMap(project, Constants.SPATIAL_MAP_STRATUM_VARIABLE_NAME, Strings.DATASHEET_STRATA_NAME, LegendColors, store);

            //Transition Groups Color Map and Legend Map
            CreateTransitionGroupMaps(project, store);

            //Age  Color Map 
            CreateAgeColorMap(project, store);
        }

        public override void RefreshCriteria(DataStore store, Layout layout, Project project)
        {
            DataView AttrGroupView = CreateMapAttributeGroupsView(project, store);

            this.AddStateClassCriteria(layout, project, store);
            this.AddAgeCriteria(layout);
            this.AddStratumCriteria(layout, project, store);
            this.AddTransitionCriteria(layout, project, store);
            this.AddStateAttributeCriteria(layout, project, store, AttrGroupView);
            this.AddTransitionAttributeCriteria(layout, project, store, AttrGroupView);
        }

        private void AddStateClassCriteria(Layout layout, Project project, DataStore store)
        {
            LayoutItem StateClassesGroup = new LayoutItem("stsim_StateClassesGroup", "State Classes", true);
            LayoutItem StateClassIterationItem = new LayoutItem(Constants.SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, "Iteration", false);
            LayoutItem StateClassAvgGroup = new LayoutItem("stsim_StateClassAvgGroup", "Probability", true);

            StateClassIterationItem.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputSpatialState"));
            StateClassIterationItem.Properties.Add(new MetaDataProperty("column", "Filename"));
            StateClassIterationItem.Properties.Add(new MetaDataProperty("colorMapSource", Strings.DATASHEET_STATECLASS_NAME));
            StateClassIterationItem.Properties.Add(new MetaDataProperty("titleOverride", "State Classes (Iteration)"));

            AddAvgMapStateClassVariables(project, StateClassAvgGroup.Items, store);

            StateClassesGroup.Items.Add(StateClassIterationItem);
            StateClassesGroup.Items.Add(StateClassAvgGroup);
            layout.Items.Add(StateClassesGroup);
        }

        private void AddAgeCriteria(Layout layout)
        {
            LayoutItem AgesGroup = new LayoutItem("stsim_AgesGroup", "Ages", true);
            LayoutItem AgesIterationItem = new LayoutItem(Constants.SPATIAL_MAP_AGE_VARIABLE_NAME, "Iteration", false);
            LayoutItem AgesAvgProbItem = new LayoutItem(Constants.SPATIAL_MAP_AVG_AGE_VARIABLE_NAME, "Average", false);

            AgesIterationItem.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputSpatialAge"));
            AgesIterationItem.Properties.Add(new MetaDataProperty("column", "Filename"));
            AgesIterationItem.Properties.Add(new MetaDataProperty("colorMapSource", Strings.DATASHEET_AGE_GROUP_NAME));
            AgesIterationItem.Properties.Add(new MetaDataProperty("colorMapSourceDataRequired", "True"));
            AgesIterationItem.Properties.Add(new MetaDataProperty("titleOverride", "Ages (Iteration)"));

            AgesAvgProbItem.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputSpatialAverageAge"));
            AgesAvgProbItem.Properties.Add(new MetaDataProperty("column", "Filename"));
            AgesAvgProbItem.Properties.Add(new MetaDataProperty("colorMapSource", Strings.DATASHEET_AGE_GROUP_NAME));
            AgesAvgProbItem.Properties.Add(new MetaDataProperty("colorMapSourceDataRequired", "True"));
            AgesAvgProbItem.Properties.Add(new MetaDataProperty("extendedIdentifier", AVG_ALL_ITER));
            AgesAvgProbItem.Properties.Add(new MetaDataProperty("titleOverride", "Ages (Average)"));

            AgesGroup.Items.Add(AgesIterationItem);
            AgesGroup.Items.Add(AgesAvgProbItem);
            layout.Items.Add(AgesGroup);
        }

        private void AddStratumCriteria(Layout layout, Project project, DataStore store)
        {
            string psl = null;
            string ssl = null;
            string tsl = null;
            DataSheet dsterm = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref psl, ref ssl, ref tsl);

            LayoutItem StratumGroup = new LayoutItem("stsim_StratumGroup", psl, true);
            LayoutItem StratumIterationItem = new LayoutItem(Constants.SPATIAL_MAP_STRATUM_VARIABLE_NAME, "Iteration", false);
            LayoutItem StratumAvgGroup = new LayoutItem("stsim_StratumAvgGroup", "Probability", true);

            StratumIterationItem.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputSpatialStratum"));
            StratumIterationItem.Properties.Add(new MetaDataProperty("column", "Filename"));
            StratumIterationItem.Properties.Add(new MetaDataProperty("colorMapSource", Strings.DATASHEET_STRATA_NAME));
            StratumIterationItem.Properties.Add(new MetaDataProperty("titleOverride", psl + " (Iteration)"));

            AddAvgMapStratumVariables(project, StratumAvgGroup.Items, store);

            StratumGroup.Items.Add(StratumIterationItem);
            StratumGroup.Items.Add(StratumAvgGroup);
            layout.Items.Add(StratumGroup);
        }

        private void AddTransitionCriteria(Layout layout, Project project, DataStore store)
        {
            LayoutItem TransitionsGroup = new LayoutItem("stsim_TransitionsGroup", "Transitions", true);
            LayoutItem TransitionsIterationGroup = new LayoutItem("stsim_TransitionsIterationsGroup", "Iteration", true);
            LayoutItem TransitionsAvgTPGroup = new LayoutItem("stsim_TransitionsAvgGroup", "Probability", true);
            LayoutItem TransitionsIterationEventsGroup = new LayoutItem("stsim_TransitionsIterationsEventsGroup", "Iteration - Events", true);
            LayoutItem TransitionsTSTGroup = new LayoutItem("stsim_TransitionsTSTGroup", "Time-Since-Transition", true);
            LayoutItem TransitionsAvgTSTGroup = new LayoutItem("stsim_TransitionsAvgTSTGroup", "Time-Since-Transition - Average", true);

            AddMapTransitionGroupVariables(
                project, TransitionsIterationGroup.Items,
                "stsim_OutputSpatialTransition", "Filename", "TransitionGroupId", "(Iteration)",
                Constants.SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_NAME, Strings.DATASHEET_TRANSITION_TYPE_NAME, store);

            AddMapTransitionGroupVariables(
                project, TransitionsAvgTPGroup.Items,
                "stsim_OutputSpatialAverageTransitionProbability", "Filename", "TransitionGroupId", AVG_PROB_ALL_ITER,
                Constants.SPATIAL_MAP_AVG_TRANSITION_PROBABILITY_VARIABLE_NAME, null, store);

            AddMapTransitionGroupVariables(
                project, TransitionsIterationEventsGroup.Items,
                "stsim_OutputSpatialTransitionEvent", "Filename", "TransitionGroupId", "(Transitions Events)",
                Constants.SPATIAL_MAP_TRANSITION_GROUP_EVENT_VARIABLE_NAME, null, store);

            AddMapTransitionGroupVariables(
                project, TransitionsTSTGroup.Items,
                "stsim_OutputSpatialTST", "Filename", "TransitionGroupId", "(Time-Since-Transition)",
                Constants.SPATIAL_MAP_TST_VARIABLE_NAME, null, store);

            AddMapTransitionGroupVariables(
                project, TransitionsAvgTSTGroup.Items,
                "stsim_OutputSpatialAverageTST", "Filename", "TransitionGroupId", "(Time-Since-Transition - Average)",
                Constants.SPATIAL_MAP_AVG_TST_VARIABLE_NAME, null, store);

            if (TransitionsIterationGroup.Items.Count > 0) { TransitionsGroup.Items.Add(TransitionsIterationGroup); }
            if (TransitionsAvgTPGroup.Items.Count > 0) { TransitionsGroup.Items.Add(TransitionsAvgTPGroup); }
            if (TransitionsIterationEventsGroup.Items.Count > 0) { TransitionsGroup.Items.Add(TransitionsIterationEventsGroup); }
            if (TransitionsTSTGroup.Items.Count > 0) { TransitionsGroup.Items.Add(TransitionsTSTGroup); }
            if (TransitionsAvgTSTGroup.Items.Count > 0) { TransitionsGroup.Items.Add(TransitionsAvgTSTGroup); }
            if (TransitionsGroup.Items.Count > 0) { layout.Items.Add(TransitionsGroup); }
        }

        private void AddStateAttributeCriteria(Layout layout, Project project, DataStore store, DataView attrGroupView)
        {
            LayoutItem StateAttributesGroup = new LayoutItem("stsim_StateAttributesGroup", "State Attributes", true);
            LayoutItem StateAttributesIterationGroup = new LayoutItem("stsim_StateAttributesIterationsGroup", "Iteration", true);
            LayoutItem StateAttributesAvgGroup = new LayoutItem("stsim_StateAttributesAvgGroup", "Average", true);

            AddMapStateAttributes(
                project, StateAttributesIterationGroup.Items, 
                "stsim_OutputSpatialStateAttribute", "Filename", "StateAttributeTypeId", 
                Constants.SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_NAME, null, store, attrGroupView);

            //Average State Attributes
            AddMapStateAttributes(
                project, StateAttributesAvgGroup.Items,
                "stsim_OutputSpatialAverageStateAttribute", "Filename", "StateAttributeTypeId",
                Constants.SPATIAL_MAP_AVG_STATE_ATTRIBUTE_VARIABLE_NAME, AVG_ALL_ITER, store, attrGroupView);

            if (StateAttributesIterationGroup.Items.Count > 0) { StateAttributesGroup.Items.Add(StateAttributesIterationGroup); }
            if (StateAttributesAvgGroup.Items.Count > 0) { StateAttributesGroup.Items.Add(StateAttributesAvgGroup); }
            if (StateAttributesGroup.Items.Count > 0) { layout.Items.Add(StateAttributesGroup); }
        }

        private void AddTransitionAttributeCriteria(Layout layout, Project project, DataStore store, DataView attrGroupView)
        {
            LayoutItem TransitionAttributesGroup = new LayoutItem("stsim_TransitionAttributesGroup", "Transition Attributes", true);
            LayoutItem TransitionAttributesIterationGroup = new LayoutItem("stsim_TransitionAttributesIterationsGroup", "Iteration", true);
            LayoutItem TransitionAttributesAvgGroup = new LayoutItem("stsim_TransitionAttributesAvgGroup", "Average", true);

            AddMapTransitionAttributes(
                project, TransitionAttributesIterationGroup.Items,
                "stsim_OutputSpatialTransitionAttribute", "Filename", "TransitionAttributeTypeId",
                Constants.SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_NAME, null, store, attrGroupView);

            AddMapTransitionAttributes(
                project, TransitionAttributesAvgGroup.Items,
                "stsim_OutputSpatialAverageTransitionAttribute", "Filename", "TransitionAttributeTypeId",
                Constants.SPATIAL_MAP_AVG_TRANSITION_ATTRIBUTE_VARIABLE_NAME, AVG_ALL_ITER, store, attrGroupView);

            if (TransitionAttributesIterationGroup.Items.Count > 0) { TransitionAttributesGroup.Items.Add(TransitionAttributesIterationGroup); }
            if (TransitionAttributesAvgGroup.Items.Count > 0) { TransitionAttributesGroup.Items.Add(TransitionAttributesAvgGroup); }
            if (TransitionAttributesGroup.Items.Count > 0) { layout.Items.Add(TransitionAttributesGroup); }
        }

        private static DataView CreateMapAttributeGroupsView(Project project, DataStore store)
        {
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);

            DataView View = new DataView(
                ds.GetData(store), 
                null, 
                ds.ValidationTable.DisplayMember, 
                DataViewRowState.CurrentRows);

            return View;
        }

        private static void AddAvgMapStratumVariables(
            Project project,
            LayoutItemCollection items, 
            DataStore store)
        {
            List<Stratum> Strata = GetStrata(project, store);
            string FilePrefix = Constants.SPATIAL_MAP_AVG_STRATUM_VARIABLE_NAME;

            Strata.Sort((Stratum st1, Stratum st2) =>
            {
                return string.Compare(st1.DisplayName, st2.DisplayName, StringComparison.CurrentCulture);
            });

            foreach (Stratum st in Strata)
            {
                string VarName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", FilePrefix, st.StratumId);
                LayoutItem Item = new LayoutItem(VarName, st.DisplayName, false);

                Item.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputSpatialAverageStratum"));
                Item.Properties.Add(new MetaDataProperty("column", "Filename"));
                Item.Properties.Add(new MetaDataProperty("filter", "StratumId"));
                Item.Properties.Add(new MetaDataProperty("itemId", st.StratumId.ToString(CultureInfo.InvariantCulture)));
                Item.Properties.Add(new MetaDataProperty("itemSource", Strings.DATASHEET_STRATA_NAME));
                Item.Properties.Add(new MetaDataProperty("extendedIdentifier", AVG_PROB_ALL_ITER));

                items.Add(Item);
            }
        }

        private static void AddAvgMapStateClassVariables(
            Project project,
            LayoutItemCollection items, 
            DataStore store)
        {
            List<StateClass> StateClasses = GetStateClasses(project, store);
            string FilePrefix = Constants.SPATIAL_MAP_AVG_STATE_CLASS_VARIABLE_NAME;

            StateClasses.Sort((StateClass sc1, StateClass sc2) =>
            {
                return string.Compare(sc1.DisplayName, sc2.DisplayName, StringComparison.CurrentCulture);
            });

            foreach (StateClass sc in StateClasses)
            {
                string VarName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", FilePrefix, sc.Id);
                LayoutItem Item = new LayoutItem(VarName, sc.DisplayName, false);

                Item.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputSpatialAverageStateClass"));
                Item.Properties.Add(new MetaDataProperty("column", "Filename"));
                Item.Properties.Add(new MetaDataProperty("filter", "StateClassId"));
                Item.Properties.Add(new MetaDataProperty("itemId", sc.Id.ToString(CultureInfo.InvariantCulture)));
                Item.Properties.Add(new MetaDataProperty("itemSource", Strings.DATASHEET_STATECLASS_NAME));
                Item.Properties.Add(new MetaDataProperty("extendedIdentifier", AVG_PROB_ALL_ITER));

                items.Add(Item);
            }
        }

        private static void AddMapTransitionGroupVariables(
            Project project, 
            LayoutItemCollection items, 
            string dataSheetName, 
            string fileColumnName, 
            string filterColumnName, 
            string extendedIdentifier, 
            string filePrefix, 
            string colorMapSource, 
            DataStore store)
        {
            Dictionary<int, TransitionGroup> PrimaryGroups = GetPrimaryTransitionGroups(project, store);
            List<TransitionGroup> PrimaryGroupList = new List<TransitionGroup>();

            foreach (TransitionGroup g in PrimaryGroups.Values)
            {
                PrimaryGroupList.Add(g);
            }

            PrimaryGroupList.Sort((TransitionGroup g1, TransitionGroup g2) =>
            {
                return string.Compare(g1.DisplayName, g2.DisplayName, StringComparison.CurrentCulture);
            });

            foreach (TransitionGroup g in PrimaryGroupList)
            {
                string VarName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", filePrefix, g.TransitionGroupId);
                LayoutItem Item = new LayoutItem(VarName, g.DisplayName, false);

                Item.Properties.Add(new MetaDataProperty("dataSheet", dataSheetName));
                Item.Properties.Add(new MetaDataProperty("column", fileColumnName));
                Item.Properties.Add(new MetaDataProperty("filter", filterColumnName));
                Item.Properties.Add(new MetaDataProperty("itemId", g.TransitionGroupId.ToString(CultureInfo.InvariantCulture)));
                Item.Properties.Add(new MetaDataProperty("itemSource", Strings.DATASHEET_TRANSITION_GROUP_NAME));
                Item.Properties.Add(new MetaDataProperty("extendedIdentifier", extendedIdentifier));
                Item.Properties.Add(new MetaDataProperty("colorMapSource", colorMapSource));

                items.Add(Item);
            }
        }

        private static void AddMapStateAttributes(
            Project project, 
            LayoutItemCollection items,
            string dataSheetName,
            string fileColumnName,
            string filterColumnName,
            string filePrefix,
            string extendedIdentifier,
            DataStore store, 
            DataView attrGroupsView)
        {
            DataSheet StateAttrsDataSheet = project.GetDataSheet(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME);

            AddMapNonGroupedAttributes(
                store, items, StateAttrsDataSheet, 
                dataSheetName, fileColumnName, filterColumnName,
                Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME, filePrefix, extendedIdentifier);

            Dictionary<string, LayoutItem> GroupsDict = new Dictionary<string, LayoutItem>();
            List<LayoutItem> GroupsList = new List<LayoutItem>();

            foreach (DataRowView drv in attrGroupsView)
            {
                string GroupName = Convert.ToString(drv.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                LayoutItem Group = new LayoutItem(GroupName, GroupName, true);

                GroupsDict.Add(GroupName, Group);
                GroupsList.Add(Group);
            }

            AddMapGroupedAttributes(
                store, GroupsDict, StateAttrsDataSheet,
                dataSheetName, fileColumnName, filterColumnName,
                Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME, filePrefix, extendedIdentifier);

            foreach (LayoutItem g in GroupsList)
            {
                if (g.Items.Count > 0)
                {
                    items.Add(g);
                }
            }
        }

        private static void AddMapTransitionAttributes(
            Project project, 
            LayoutItemCollection items,
            string dataSheetName,
            string fileColumnName,
            string filterColumnName,
            string filePrefix,
            string extendedIdentifier,
            DataStore store, 
            DataView attrGroupsView)
        {
            DataSheet TransitionAttrsDataSheet = project.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME);

            AddMapNonGroupedAttributes(
                store, items, TransitionAttrsDataSheet, 
                dataSheetName, fileColumnName, filterColumnName,
                Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME, filePrefix, extendedIdentifier);

            Dictionary<string, LayoutItem> GroupsDict = new Dictionary<string, LayoutItem>();
            List<LayoutItem> GroupsList = new List<LayoutItem>();

            foreach (DataRowView drv in attrGroupsView)
            {
                string GroupName = Convert.ToString(drv.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                LayoutItem Group = new LayoutItem(GroupName, GroupName, true);

                GroupsDict.Add(GroupName, Group);
                GroupsList.Add(Group);
            }

            AddMapGroupedAttributes(
                store, GroupsDict, TransitionAttrsDataSheet,
                dataSheetName, fileColumnName, filterColumnName,
                Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME, filePrefix, extendedIdentifier);

            foreach (LayoutItem g in GroupsList)
            {
                if (g.Items.Count > 0)
                {
                    items.Add(g);
                }
            }
        }

        private static void AddMapNonGroupedAttributes(
            DataStore store, 
            LayoutItemCollection items, 
            DataSheet attrsDataSheet, 
            string dataSheetName, 
            string fileColumnName, 
            string filterColumnName,
            string itemSource,
            string prefix, 
            string extendedIdentifier)
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

                    LayoutItem Item = new LayoutItem(AttrName, DisplayName, false);

                    Item.Properties.Add(new MetaDataProperty("dataSheet", dataSheetName));
                    Item.Properties.Add(new MetaDataProperty("column", fileColumnName));
                    Item.Properties.Add(new MetaDataProperty("filter", filterColumnName));
                    Item.Properties.Add(new MetaDataProperty("itemId", AttrId.ToString(CultureInfo.InvariantCulture)));
                    Item.Properties.Add(new MetaDataProperty("itemSource", itemSource));

                    if (extendedIdentifier != null)
                    {
                        Item.Properties.Add(new MetaDataProperty("extendedIdentifier", extendedIdentifier));
                    }

                    items.Add(Item);
                }
            }
        }

        private static void AddMapGroupedAttributes(
            DataStore store, 
            Dictionary<string, LayoutItem> groupsDict, 
            DataSheet attrsDataSheet, 
            string dataSheetName, 
            string fileColumnName, 
            string filterColumnName, 
            string itemSource,
            string prefix, 
            string extendedIdentifier)
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

                    LayoutItem Item = new LayoutItem(AttrName, DisplayName, false);

                    Item.Properties.Add(new MetaDataProperty("dataSheet", dataSheetName));
                    Item.Properties.Add(new MetaDataProperty("column", fileColumnName));
                    Item.Properties.Add(new MetaDataProperty("filter", filterColumnName));
                    Item.Properties.Add(new MetaDataProperty("itemId", AttrId.ToString(CultureInfo.InvariantCulture)));
                    Item.Properties.Add(new MetaDataProperty("itemSource", itemSource));

                    if (extendedIdentifier != null)
                    {
                        Item.Properties.Add(new MetaDataProperty("extendedIdentifier", extendedIdentifier));
                    }

                    groupsDict[GroupName].Items.Add(Item);
                }
            }
        }

        private static List<Stratum> GetStrata(Project project, DataStore store)
        {
            List<Stratum> Strata = new List<Stratum>();
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);

            foreach (DataRow dr in ds.GetData(store).Rows)
            {
                int id = Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture);
                string name = Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);

                Strata.Add(new Stratum(id, name));
            }

            return Strata;
        }

        private static List<StateClass> GetStateClasses(Project project, DataStore store)
        {
            List<StateClass> StateClasses = new List<StateClass>();
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);

            foreach (DataRow dr in ds.GetData(store).Rows)
            {
                int id = Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture);
                int slxid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                int slyid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                string name = Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);

                StateClasses.Add(new StateClass(id, slxid, slyid, name));
            }

            return StateClasses;
        }

        private static Dictionary<int, TransitionGroup> GetPrimaryTransitionGroups(Project project, DataStore store)
        {
            TransitionTypeCollection TransitionTypes = GetTransitionTypes(project, store);
            TransitionGroupCollection TransitionGroups = GetTransitionGroups(project, store);
            Dictionary<int, bool> TransitionSimulationGroups = GetTransitionSimulationGroups(project, store);
            DataSheet TypeGroupDataSheet = project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_GROUP_NAME);
            Dictionary<int, TransitionGroup> PrimaryGroups = new Dictionary<int, TransitionGroup>();

            foreach (TransitionType TType in TransitionTypes)
            {
                string query = string.Format(CultureInfo.InvariantCulture,
                    "{0}={1}", Strings.DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME, TType.TransitionTypeId);

                DataRow[] TGroupRows = TypeGroupDataSheet.GetData(store).Select(query);
                bool TypeHasNonAutoPrimaryGroup = false;

                foreach (DataRow TGroupRow in TGroupRows)
                {
                    int tgid = Convert.ToInt32(TGroupRow[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    TransitionGroup tg = TransitionGroups[tgid];

                    if (!tg.IsAuto)
                    {
                        if (TransitionSimulationGroups.ContainsKey(tgid))
                        {
                            TypeHasNonAutoPrimaryGroup = true;

                            if (!PrimaryGroups.ContainsKey(tgid))
                            {
                                PrimaryGroups.Add(tgid, tg);
                            }
                        }
                    }
                }

                if (!TypeHasNonAutoPrimaryGroup)
                {
                    foreach (DataRow TGroupRow in TGroupRows)
                    {
                        int tgid = Convert.ToInt32(TGroupRow[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                        TransitionGroup tg = TransitionGroups[tgid];

                        if (tg.IsAuto)
                        {
                            if (!PrimaryGroups.ContainsKey(tgid))
                            {
                                PrimaryGroups.Add(tgid, tg);
                            }
                        }
                    }
                }
            }

            return PrimaryGroups;
        }

        private static TransitionTypeCollection GetTransitionTypes(Project project, DataStore store)
        {
            TransitionTypeCollection types = new TransitionTypeCollection();
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_NAME);

            foreach (DataRow dr in ds.GetData(store).Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    int TransitionTypeId = Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture);
                    string DisplayName = Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture);

                    types.Add(new TransitionType(TransitionTypeId, DisplayName, 0));
                }
            }

            return types;
        }

        private static TransitionGroupCollection GetTransitionGroups(Project project, DataStore store)
        {
            TransitionGroupCollection groups = new TransitionGroupCollection();
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME);

            foreach (DataRow dr in ds.GetData(store).Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    int TransitionGroupId = Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture);
                    string DisplayName = Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture);
                    bool IsAuto = DataTableUtilities.GetDataBool(dr, Strings.IS_AUTO_COLUMN_NAME);

                    groups.Add(new TransitionGroup(TransitionGroupId, DisplayName, IsAuto));
                }
            }

            return groups;
        }

        private static Dictionary<int, bool> GetTransitionSimulationGroups(Project project, DataStore store)
        {
            Dictionary<int, bool> groups = new Dictionary<int, bool>();
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_TRANSITION_SIMULATION_GROUP_NAME);

            foreach (DataRow dr in ds.GetData(store).Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    int TransitionGroupId = Convert.ToInt32(dr[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    groups.Add(TransitionGroupId, true);
                }
            }

            return groups;
        }

        /// <summary>
        /// Create the raster color maps for the specific project. The color maps are QGis compatible, and are use when
        /// displaying the rasters in the Syncrosim Map display.
        /// </summary>
        /// <param name="project">The current Project.</param>
        /// <param name="mapVariable">The map variable. Ex sc, tg-123,str</param>
        /// <param name="datasheetName">The name of the datasheet containing the color map configuration</param>
        /// <param name="dicLegendLblColor">A dictionary with the Map Legend Label as the key, color as value</param>
        /// <remarks></remarks>
        private static void CreateColorMap(
            Project project,
            string mapVariable,
            string datasheetName,
            Dictionary<string, string> dicLegendLblColor, 
            DataStore store)
        {
            if (project.Library.Session.IsRunningOnMono)
            {
                return;
            }

            // Where are the color maps stored
            string colorMapPath = project.Library.GetFolderName(LibraryFolderType.Data, project, true);

            // What's the absolute name of the color map file
            string colorMapFilename = Spatial.GetColorMapFileName(project, mapVariable);

            // Lets toast the existing color map 
            File.Delete(colorMapFilename);

            StreamWriter fileWriter = System.IO.File.CreateText(Path.Combine(colorMapPath, colorMapFilename));
            fileWriter.WriteLine("# Syncrosim Generated State Class Color Map (QGIS-compatible),,,,,");
            fileWriter.WriteLine("INTERPOLATION:EXACT");

            //Now create the color maps
            DataSheet ds = project.GetDataSheet(datasheetName);
            DataTable dt = ds.GetData(store);

            // Check if there any non-null or non-white color definitions

            DataView dv = new DataView(dt, null, Strings.DATASHEET_NAME_COLUMN_NAME, DataViewRowState.CurrentRows);
            // ID, Name, Transparency, and Color value

            foreach (DataRowView dr in dv)
            {
                // ID, Name, Transparency, and Color value
                string id = dr.Row[Strings.DATASHEET_MAPID_COLUMN_NAME].ToString();
                var transpenciesRGB = dr.Row[Strings.DATASHEET_COLOR_COLUMN_NAME].ToString();
                var lbl = dr.Row[Strings.DATASHEET_NAME_COLUMN_NAME].ToString();
                var mapLegendLbl = dr.Row[Strings.DATASHEET_LEGEND_COLUMN_NAME].ToString();

                // Dont include a color entry for record without Id or defined colors assigned
                if (id.Trim().Length > 0 && transpenciesRGB.Length > 0)
                {
                    // Do we have a Legend Map for this map Variable. If so we need to get ""fancy""
                    if (dicLegendLblColor != null)
                    {
                        if (dicLegendLblColor.Count > 0)
                        {
                            if (mapLegendLbl.Length > 0)
                            {
                                transpenciesRGB = dicLegendLblColor[mapLegendLbl];
                            }
                            else
                            {
                                transpenciesRGB = dicLegendLblColor[Constants.LEGEND_MAP_BLANK_LEGEND_ITEM];
                            }
                        }
                    }

                    var aryColor = transpenciesRGB.Split(','); // Split into individual Transparency, Red, Green,Blue
                    if (aryColor.GetUpperBound(0) == 3)
                    {
                        // Color Map line syntax, for discrete values, is:
                        //  Value, Red, Green, Blue, Transparency, Label 
                        //  21001,168,0,87,255,UNDET:<5% Inv
                        fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", id, aryColor[1], aryColor[2], aryColor[3], aryColor[0], lbl);
                    }
                }
            }

            fileWriter.Close();
        }

        /// <summary>
        /// displaying the rasters in the Syncrosim Map display.
        /// </summary>
        /// <param name="project">The current Project.</param>
        /// <param name="mapVariable">The map variable. Ex sc, tg-123,str</param>
        /// <param name="datasheetName">The name of the datasheet containing the color map configuration</param>
        /// <returns>A dictionary of Legend labels(key), and Color values</returns>
        /// <remarks></remarks>
        private static Dictionary<string, string> CreateLegendMap(Project project, string mapVariable, string datasheetName, DataStore store)
        {
            if (project.Library.Session.IsRunningOnMono)
            {
                return null;
            }

            // Where are the legend maps stored
            string colorMapPath = project.Library.GetFolderName(LibraryFolderType.Data, project, true);

            // What's the absolute name of the legend map file
            string mapFilename = Spatial.GetLegendMapFileName(project, mapVariable);

            // Lets toast the existing color map 
            File.Delete(mapFilename);

            StreamWriter fileWriter = System.IO.File.CreateText(Path.Combine(colorMapPath, mapFilename));
            fileWriter.WriteLine("# Syncrosim Generated State Class Color Map (QGIS-compatible),,,,,");
            fileWriter.WriteLine("INTERPOLATION:EXACT");

            //Now create the legend color maps
            DataSheet ds = project.GetDataSheet(datasheetName);
            DataTable dt = ds.GetData(store).Copy();

            // Loop thru and change all Legend nulls to Blank Item string
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.IsDBNull(dr[Strings.DATASHEET_LEGEND_COLUMN_NAME]) || dr[Strings.DATASHEET_LEGEND_COLUMN_NAME].ToString().Trim().Length == 0)
                {
                    dr[Strings.DATASHEET_LEGEND_COLUMN_NAME] = Constants.LEGEND_MAP_BLANK_LEGEND_ITEM;
                }
            }

            string sort = Strings.DATASHEET_LEGEND_COLUMN_NAME + "," + Strings.DATASHEET_MAPID_COLUMN_NAME;
            string filter = null;
            DataView dv = new DataView(dt, filter, sort, DataViewRowState.CurrentRows);
            Dictionary<string, string> legendDefined = new Dictionary<string, string>();

            foreach (DataRowView dr in dv)
            {
                // ID, Name, Transparency, Color value
                string id = dr.Row[Strings.DATASHEET_MAPID_COLUMN_NAME].ToString();
                var transpenciesRGB = dr.Row[Strings.DATASHEET_COLOR_COLUMN_NAME].ToString();
                var lbl = dr.Row[Strings.DATASHEET_LEGEND_COLUMN_NAME].ToString();

                // Check to see if we've already define this legend lavel
                if (!legendDefined.ContainsKey(lbl))
                {
                    // Dont include a color entry for record without Id or defined colors assigned
                    if (id.Trim().Length > 0 && transpenciesRGB.Length > 0)
                    {
                        var aryColor = transpenciesRGB.Split(','); // Split into individual Transparency, Red, Green,Blue
                        if (aryColor.GetUpperBound(0) == 3)
                        {
                            // Color Map line syntax, for discrete values, is:
                            //  Value, Red, Green, Blue, Transparency, Label 
                            //  21001,168,0,87,255,UNDET:<5% Inv

                            // force [blank] to end of legend
                            int val = Convert.ToInt32((lbl == Constants.LEGEND_MAP_BLANK_LEGEND_ITEM) ? 9999 : legendDefined.Count + 1);
                            fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", val, aryColor[1], aryColor[2], aryColor[3], aryColor[0], lbl);
                            legendDefined.Add(lbl, transpenciesRGB);
                        }
                    }
                }
            }

            fileWriter.Close();

            //If not valid entries in legend map, then toast it, or only one - [Blank]
            if ((legendDefined.Count == 0) || (legendDefined.Count == 1 && legendDefined.ContainsKey(Constants.LEGEND_MAP_BLANK_LEGEND_ITEM)))
            {
                File.Delete(mapFilename);
                legendDefined = null;
            }

            return legendDefined;
        }

        /// <summary>
        /// Create/Replace the raster Transition Group color maps for the specific project. The color maps are QGis compatible, and are use when
        /// displaying the Transitions rasters in the Syncrosim Map display.
        /// </summary>
        /// <param name="project">The current Project</param>
        /// <remarks></remarks>
        private static void CreateTransitionGroupColorMap(Project project, DataRow drTg, Dictionary<string, string> dicLegendLblColor, DataStore store)
        {
            DataSheet dsTg = project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME);
            DataSheet dsTTG = project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_GROUP_NAME);
            DataSheet dsTT = project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_NAME);

            DataTable dtTTg = dsTTG.GetData(store);
            DataTable dtTT = dsTT.GetData(store);

            string tgId = drTg[dsTg.PrimaryKeyColumn.Name].ToString();
            string tgName = drTg[Strings.DATASHEET_NAME_COLUMN_NAME].ToString();

            var colorMapType = Constants.SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_NAME + "-" + tgId;

            // What's the absolute name of the color map file
            string colorMapFilename = Spatial.GetColorMapFileName(project, colorMapType);

            // Lets toast the existing color map 
            File.Delete(colorMapFilename);

            // Fetch all the transition types for this Transition Group
            SortedList<string, string> sortedTT = new SortedList<string, string>();
            string filter = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tgId);

            foreach (DataRow drTTG in dtTTg.Select(filter))
            {
                if (drTTG.RowState != DataRowState.Deleted)
                {
                    string TtId = drTTG[Strings.DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME].ToString();

                    // Now fetch the Transition Type record to get ID, Name, Transparency/Color value
                    string ttFilter = string.Format(CultureInfo.InvariantCulture, "{0}={1}", dsTT.PrimaryKeyColumn.Name, TtId);

                    if (dtTT.Select(ttFilter).Count() > 0)
                    {
                        DataRow drTT = dtTT.Select(ttFilter).First();

                        string id = drTT[Strings.DATASHEET_MAPID_COLUMN_NAME].ToString();
                        var lbl = drTT[Strings.DATASHEET_NAME_COLUMN_NAME].ToString();
                        var transparencyRGB = drTT[Strings.DATASHEET_COLOR_COLUMN_NAME].ToString();
                        var mapLegendLbl = drTT[Strings.DATASHEET_LEGEND_COLUMN_NAME].ToString();

                        // Dont include a color entry for Transition Type without Id or colors assigned
                        if (id.Trim().Length > 0 && transparencyRGB.Length > 0)
                        {
                            // Do we have a Legend Map for this map Variable. If so we need to get ""fancy""
                            if (dicLegendLblColor != null)
                            {
                                if (dicLegendLblColor.Count > 0)
                                {
                                    if (mapLegendLbl.Length > 0)
                                    {
                                        transparencyRGB = dicLegendLblColor[mapLegendLbl];
                                    }
                                    else
                                    {
                                        transparencyRGB = dicLegendLblColor[Constants.LEGEND_MAP_BLANK_LEGEND_ITEM];
                                    }
                                }
                            }

                            // Stuff into a list, so we can sort alphabetically
                            sortedTT.Add(lbl, id + "," + transparencyRGB);
                        }
                    }
                }
            }

            //Now create the new color map for the current Transition Group, sorted alphabetically by label
            //DEVNOTE: Create the color map even if no color definitions, as the display logic looks for this empty definition. 
            // Otherwise, it'll apply its own, which we dont want
            StreamWriter fileWriter = System.IO.File.CreateText(colorMapFilename);
            fileWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "# Syncrosim Generated Transition Group ({0}) Color Map (QGIS-compatible) Export File,,,,,", tgName));
            fileWriter.WriteLine("INTERPOLATION:EXACT");
            bool NoOccurrenceValueSeen = false;

            for (var i = 0; i < sortedTT.Count; i++)
            {
                // Dont include a color entry for Transition Type without Id or colors assigned

                string lbl = sortedTT.Keys[i].Replace(",", " "); // Dont allow comma in label
                string idColor = sortedTT.Values[i];
                var aryIdColor = idColor.Split(','); // Split into ID, Transparency, Red, Green,Blue

                if (aryIdColor.GetUpperBound(0) == 4)
                {
                    // Color Map line syntax, for discrete values, is:
                    //  Value, Red, Green, Blue, Transparency, Label 
                    //  21001,168,0,87,255,UNDET:<5% Inv

                    fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", aryIdColor[0], aryIdColor[2], aryIdColor[3], aryIdColor[4], aryIdColor[1], lbl);

                    if (Convert.ToInt32(aryIdColor[0]) == 0)
                    {
                        NoOccurrenceValueSeen = true;
                    }
                }
            }

            if (!NoOccurrenceValueSeen)
            {
                fileWriter.WriteLine("0,232,232,232,255,No Occurrence");
            }

            fileWriter.Close();
        }

        /// <summary>
        /// Create/Replace the raster Transition Group Legend AND Color maps for the specific project. 
        /// The legend & color maps are QGis compatible, and are use when displaying the Transitions rasters in the Syncrosim Map display.
        /// </summary>
        /// <param name="project">The current Project</param>
        /// <remarks></remarks>
        private static void CreateTransitionGroupMaps(Project project, DataStore store)
        {
            if (project.Library.Session.IsRunningOnMono)
            {
                return;
            }

            // Loop thru the Transition Groups
            foreach (DataRow drTg in project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME).GetData(store).Select(null, null, DataViewRowState.CurrentRows))
            {
                var dicLegendColors = CreateTransitionGroupLegendMap(project, drTg, store);
                CreateTransitionGroupColorMap(project, drTg, dicLegendColors, store);
            }
        }

        /// <summary>
        /// Create/Replace the raster Transition Group Legend maps for the specific project. The color maps are 
        /// QGis compatible, and are use when displaying the Transitions rasters in the Syncrosim Map display.
        /// </summary>
        /// <param name="project">The current Project</param>
        /// <remarks></remarks>
        private static Dictionary<string, string> CreateTransitionGroupLegendMap(Project project, DataRow drTg, DataStore store)
        {
            DataSheet dsTTG = project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_GROUP_NAME);
            DataSheet dsTT = project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_NAME);
            DataSheet dsTg = project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME);
            DataTable dtTTg = dsTTG.GetData(store);
            DataTable dtTT = dsTT.GetData(store);

            string tgId = drTg[dsTg.PrimaryKeyColumn.Name].ToString();
            string tgName = drTg[Strings.DATASHEET_NAME_COLUMN_NAME].ToString();
            var colorMapType = Constants.SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_NAME + "-" + tgId;

            // Where are the color legend stored
            string legendMapPath = project.Library.GetFolderName(LibraryFolderType.Data, project, false);

            // What's the absolute name of the legend  map file
            string legendMapFilename = Spatial.GetLegendMapFileName(project, colorMapType);

            // Lets toast the existing color map 
            File.Delete(legendMapFilename);

            // Fetch all the transition types for this Transition Group
            SortedList<string, string> sortedTT = new SortedList<string, string>();
            string filter = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tgId);
            string sort = Strings.DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME;

            foreach (DataRow drTTG in dtTTg.Select(filter, sort, DataViewRowState.CurrentRows))
            {
                string TtId = drTTG[Strings.DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME].ToString();

                // Now fetch the Transition Type record to get ID, Legend Name, Transparency/Color value
                string ttFilter = string.Format(CultureInfo.InvariantCulture, "{0}={1}", dsTT.PrimaryKeyColumn.Name, TtId);

                if (dtTT.Select(ttFilter).Count() > 0)
                {
                    DataRow drTT = dtTT.Select(ttFilter).First();

                    string id = drTT[Strings.DATASHEET_MAPID_COLUMN_NAME].ToString();
                    var lbl = drTT[Strings.DATASHEET_LEGEND_COLUMN_NAME].ToString();
                    if (lbl.Trim().Length == 0)
                    {
                        lbl = Constants.LEGEND_MAP_BLANK_LEGEND_ITEM;
                    }

                    var transparencyRGB = drTT[Strings.DATASHEET_COLOR_COLUMN_NAME].ToString();

                    // Dont include a legend entry for Transition Type without Id or colors or Map label assigned
                    if (id.Trim().Length > 0 && transparencyRGB.Length > 0 && lbl.Length > 0)
                    {
                        // Stuff into a list, so we can sort alphabetically
                        if (!sortedTT.ContainsKey(lbl))
                        {
                            sortedTT.Add(lbl, id + "," + transparencyRGB);
                        }
                        else
                        {
                            // Use the TT with the lowest Id value
                            string oldIdColor = sortedTT[lbl];

                            if (int.Parse(oldIdColor.Split(',')[0], CultureInfo.InvariantCulture) > int.Parse(id, CultureInfo.InvariantCulture))
                            {
                                sortedTT[lbl] = id + "," + transparencyRGB;
                            }
                        }
                    }
                }
            }

            //Now create the new legend map for the current Transition Group, sorted alphabetically by label
            //DEVNOTE: Don't create the legend map if no color/legend definitions. 

            Dictionary<string, string> legendColorsDefined = new Dictionary<string, string>();

            if (sortedTT.Count > 0)
            {
                StreamWriter fileWriter = File.CreateText(Path.Combine(legendMapPath, legendMapFilename));
                fileWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "# Syncrosim Generated Transition Group ({0}) Color Map (QGIS-compatible) Export File,,,,,", tgName));
                fileWriter.WriteLine("INTERPOLATION:EXACT");

                for (var i = 0; i < sortedTT.Count; i++)
                {
                    // Dont include a color entry for Transition Type without Id or colors assigned

                    string lbl = sortedTT.Keys[i].Replace(",", " "); // Dont allow comma in label
                    string idColor = sortedTT.Values[i];
                    var aryColor = idColor.Split(','); // Split into ID, Transparency, Red, Green,Blue

                    if (aryColor.GetUpperBound(0) == 4)
                    {
                        // Color Map line syntax, for discrete values, is:
                        //  Value, Red, Green, Blue, Transparency, Label 
                        //  21001,168,0,87,255,UNDET:<5% Inv

                        int val = Convert.ToInt32((lbl == Constants.LEGEND_MAP_BLANK_LEGEND_ITEM) ? 9999 : i + 1);
                        fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", val, aryColor[2], aryColor[3], aryColor[4], aryColor[1], lbl);
                        legendColorsDefined.Add(lbl, string.Join(",", aryColor[1], aryColor[2], aryColor[3], aryColor[4]));
                    }
                }

                fileWriter.Close();
            }

            //If not valid entries in legend map, then toast it, or only one - [Blank]
            if ((legendColorsDefined.Count == 0) ||
                (legendColorsDefined.Count == 1 && legendColorsDefined.ContainsKey(Constants.LEGEND_MAP_BLANK_LEGEND_ITEM)))
            {
                File.Delete(legendMapFilename);
                legendColorsDefined = null;
            }

            return legendColorsDefined;

        }

        /// <summary>
        /// Create the Age raster color maps for the specific project. The color maps are QGis compatible, and are use when
        /// displaying the Age rasters in the Syncrosim Map display.
        /// </summary>
        /// <param name="project">The current Project.</param>
        /// <remarks></remarks>
        private static void CreateAgeColorMap(Project project, DataStore store)
        {
            if (project.Library.Session.IsRunningOnMono)
            {
                return;
            }

            string colorMapType = Constants.SPATIAL_MAP_AGE_VARIABLE_NAME;

            // Where are the color maps stored
            string colorMapPath = project.Library.GetFolderName(LibraryFolderType.Data, project, true);

            // What's the absolute name of the color map file
            string colorMapFilename = Spatial.GetColorMapFileName(project, colorMapType);

            // Lets toast the existing color map 
            File.Delete(colorMapFilename);

            var cmName = Path.Combine(colorMapPath, colorMapFilename);
            if (!CreateAgeGroupColorMap(project, cmName, store))
            {
                CreateAgeTypeColorMap(project, cmName, store);
            }
        }

        /// <summary>
        /// Create a Color Map file based on the Age Group configuration for the specified project.
        /// </summary>
        /// <param name="project">The project of interest</param>
        /// <param name="colorMapFilename">The full absolute name of the color map file to be generated</param>
        /// <returns>True if successful in generating the color map file</returns>
        /// <remarks></remarks>
        private static bool CreateAgeGroupColorMap(Project project, string colorMapFilename, DataStore store)
        {
            //Now create the Age Group color maps
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_AGE_GROUP_NAME);
            DataTable dt = ds.GetData(store);

            if (dt == null)
            {
                return false;
            }

            // Sort by Max Age
            DataView dv = new DataView(dt, null, Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME, DataViewRowState.CurrentRows);
            if (dv.Count == 0)
            {
                return false;
            }

            // See if there any rows that have colors assigned - otherwise we'd 
            // create an empty color map, which will be confusing to the Spatial map package

            bool bColorsExist = false;

            foreach (DataRowView dr in dv)
            {
                var transparencyRGB = dr.Row[Strings.DATASHEET_COLOR_COLUMN_NAME].ToString();

                if (transparencyRGB.Length > 0)
                {
                    if (ColorUtilities.ColorFromString(transparencyRGB).ToArgb() != Color.White.ToArgb())
                    {
                        bColorsExist = true;
                        break;
                    }
                }
            }

            if (!bColorsExist)
            {
                return false;
            }

            StreamWriter fileWriter = System.IO.File.CreateText(colorMapFilename);
            fileWriter.WriteLine("# Syncrosim Generated Age Group Color Map (QGIS-compatible),,,,,");
            fileWriter.WriteLine("INTERPOLATION:DISCRETE");

            int prevMaxAge = 0;

            foreach (DataRowView dr in dv)
            {
                // ID, Name, Transparency, and Color value
                var transparencyRGB = dr.Row[Strings.DATASHEET_COLOR_COLUMN_NAME].ToString();
                var maxAge = dr.Row[Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME].ToString();

                // Dont include a color entry for Age Group without a Max Age or defined/non-white colors assigned

                if (maxAge.Trim().Length > 0 && transparencyRGB.Length > 0)
                {
                    if (ColorUtilities.ColorFromString(transparencyRGB).ToArgb() != Color.White.ToArgb())
                    {
                        var aryColor = transparencyRGB.Split(','); // Split into individual Transparency, Red, Green,Blue

                        if (aryColor.GetUpperBound(0) == 3)
                        {
                            // Color Map line syntax, for discrete values, is:
                            //  Value, Red, Green, Blue, Transparency, Label 
                            //  21001,168,0,87,255,UNDET:<5% Inv

                            string lbl = null;
                            if (prevMaxAge == int.Parse(maxAge, CultureInfo.InvariantCulture))
                            {
                                lbl = string.Format(CultureInfo.InvariantCulture, "{0}", maxAge);
                            }
                            else
                            {
                                lbl = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", prevMaxAge, maxAge);
                            }

                            fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", maxAge, aryColor[1], aryColor[2], aryColor[3], aryColor[0], lbl);
                            prevMaxAge = int.Parse(maxAge, CultureInfo.InvariantCulture) + 1;
                        }
                    }
                }
            }

            fileWriter.Close();

            // return success
            return true;
        }

        /// <summary>
        /// Create a Color Map file based on the Age Type configuration for the specified project.
        /// </summary>
        /// <param name="project">The project of interest</param>
        /// <param name="colorMapFilename">The full absolute name of the color map file to be generated</param>
        /// <returns>True if successful in generating the color map file</returns>
        /// <remarks></remarks>
        private static bool CreateAgeTypeColorMap(Project project, string colorMapFilename, DataStore store)
        {
            List<ClassBinDescriptor> ageDescriptors = ChartingUtilities.GetClassBinTypeDescriptors(
                project,
                Strings.DATASHEET_AGE_TYPE_NAME,
                Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME,
                Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME, 
                store);

            if (ageDescriptors != null)
            {
                StreamWriter fileWriter = System.IO.File.CreateText(colorMapFilename);
                fileWriter.WriteLine("# Syncrosim Generated Age Type Color Map (QGIS-compatible),,,,,");
                fileWriter.WriteLine("INTERPOLATION:DISCRETE");

                Color clr = new Color();

                var binColors = new Color[] {
                    Color.Blue, Color.Aqua, Color.Yellow, Color.Orange,
                    Color.Red, Color.ForestGreen, Color.Fuchsia, Color.LawnGreen};

                // DEVNOTE: We need a way to support some sort of color wheel, so we 
                // can generate predictable sequence of colors. We're OK for now

                int clrIdx = 0;

                string ageLbl = null;
                foreach (var adesc in ageDescriptors)
                {
                    clr = binColors[clrIdx % binColors.Count()]; // repeat colors when we've used them all up
                    if (adesc.Maximum != null)
                    {
                        ageLbl = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", adesc.Minimum, adesc.Maximum);
                        fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", adesc.Maximum, clr.R, clr.G, clr.B, clr.A, ageLbl);
                    }
                    else
                    {
                        // The Spatial map control will add a top level bin to take care of this
                    }
                    clrIdx += 1;
                }

                fileWriter.Close();
            }

            return true;
        }
    }
}
