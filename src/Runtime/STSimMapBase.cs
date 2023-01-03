// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal abstract class STSimMapBase
    {
        private Scenario m_Scenario;
        private string m_PrimaryStratumLabel;
        private string m_SecondaryStratumLabel;
        private string m_TertiaryStratumLabel;
        private bool m_HasItems;

        protected STSimMapBase(Scenario scenario)
        {
            this.m_Scenario = scenario;

            DataSheet ds = scenario.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetStratumLabelTerminology(
                ds, 
                ref this.m_PrimaryStratumLabel, 
                ref this.m_SecondaryStratumLabel, 
                ref this.m_TertiaryStratumLabel);
        }

        protected string PrimaryStratumLabel
        {
            get
            {
                return this.m_PrimaryStratumLabel;
            }
        }

        protected string SecondaryStratumLabel
        {
            get
            {
                return this.m_SecondaryStratumLabel;
            }
        }

        protected string TertiaryStratumLabel
        {
            get
            {
                return this.m_TertiaryStratumLabel;
            }
        }

        protected void SetHasItems()
        {
            this.m_HasItems = true;
        }

        public bool HasItems
        {
            get
            {
                return this.m_HasItems;
            }
        }

        public Scenario Scenario
        {
            get
            {
                return this.m_Scenario;
            }
        }

        protected static void ThrowDuplicateItemException()
        {
            throw new STSimMapDuplicateItemException(
                "An item with the same keys has already been added.");
        }

        protected static string FormatValue(int? value)
        {
            if (!value.HasValue)
            {
                return "NULL";
            }
            else
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture);
            }
        }

        protected string GetStratumName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_STRATA_NAME, id);
        }

        protected string GetSecondaryStratumName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_SECONDARY_STRATA_NAME, id);
        }

        protected string GetTertiaryStratumName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_TERTIARY_STRATA_NAME, id);
        }

        protected string GetStateClassName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_STATECLASS_NAME, id);
        }

        protected string GetTransitionGroupName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_TRANSITION_GROUP_NAME, id);
        }

        protected string GetTransitionTypeName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_TRANSITION_TYPE_NAME, id);
        }

        protected string GetStateAttributeTypeName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME, id);
        }

        protected string GetTransitionAttributeTypeName(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME, id);
        }

        protected string GetTSTGroupString(int? id)
        {
            return this.GetProjectItemName(Strings.DATASHEET_TRANSITION_GROUP_NAME, id);
        }

        protected string GetProjectItemName(string dataSheetName, int? id)
        {
            if (!id.HasValue)
            {
                return "NULL";
            }
            else
            {
                DataSheet ds = this.m_Scenario.Project.GetDataSheet(dataSheetName);
                return ds.ValidationTable.GetDisplayName(id.Value);
            }
        }
    }
}
