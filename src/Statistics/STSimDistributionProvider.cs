// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.StochasticTime;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    public sealed class STSimDistributionProvider : DistributionProvider
    {
        private DistributionValueCollection m_DistributionValues = new DistributionValueCollection();
        private STSimDistributionValueMap m_DistributionValueMap;

        public STSimDistributionProvider(Scenario scenario, RandomGenerator randomGenerator) : base(scenario, randomGenerator)
        {
            this.FillDistributionValueCollection();
            this.CreateDistributionValueMap();
        }

        public DistributionValueCollection Values
        {
            get
            {
                return this.m_DistributionValues;
            }
        }

        public void STSimInitializeDistributionValues()
        {
            foreach (STSimDistributionValue Value in this.m_DistributionValues)
            {
                Value.Initialize(this);
            }
        }

        public double STSimSample(int distributionTypeId, double? distributionMean, double? distributionSD, double? distributionMinimum, double? distributionMaximum, int iteration, int timestep, int? stratumId, int? secondaryStratumId)
        {
            return this.STSimInternalSample(distributionTypeId, distributionMean, distributionSD, distributionMinimum, distributionMaximum, iteration, timestep, stratumId, secondaryStratumId);
        }

        private double STSimInternalSample(int distributionTypeId, double? distributionMean, double? distributionSD, double? distributionMinimum, double? distributionMaximum, int iteration, int timestep, int? stratumId, int? secondaryStratumId)
        {
            if (distributionTypeId == this.BetaDistributionTypeId || distributionTypeId == this.NormalDistributionTypeId || distributionTypeId == this.UniformDistributionTypeId || distributionTypeId == this.UniformIntegerDistributionTypeId)
            {
                return base.Sample(distributionTypeId, distributionMean, distributionSD, distributionMinimum, distributionMaximum, iteration, timestep);
            }
            else
            {
                return this.STSimGetUserDistribution(distributionTypeId, iteration, timestep, stratumId, secondaryStratumId);
            }
        }

        private void FillDistributionValueCollection()
        {
            Debug.Assert(this.m_DistributionValues.Count == 0);
            DataSheet ds = this.Scenario.GetDataSheet(Strings.DISTRIBUTION_VALUE_DATASHEET_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                try
                {
                    DistributionFrequency? ValueDistributionFrequency = null;

                    if (dr[Strings.DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                    {
                        ValueDistributionFrequency = (DistributionFrequency)(long)dr[Strings.DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME];
                    }

                    STSimDistributionValue Item = new STSimDistributionValue(
                        DataTableUtilities.GetNullableInt(dr, Strings.DATASHEET_ITERATION_COLUMN_NAME), 
                        DataTableUtilities.GetNullableInt(dr, Strings.DATASHEET_TIMESTEP_COLUMN_NAME), 
                        DataTableUtilities.GetNullableInt(dr, Strings.DATASHEET_STRATUM_ID_COLUMN_NAME), 
                        DataTableUtilities.GetNullableInt(dr, Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME), 
                        DataTableUtilities.GetNullableInt(dr, Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME),
                        Convert.ToInt32(dr[Strings.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture), 
                        DataTableUtilities.GetNullableInt(dr, 
                        Strings.DISTRIBUTION_VALUE_EXTVAR_TYPE_ID_COLUMN_NAME), 
                        DataTableUtilities.GetNullableDouble(dr, Strings.DISTRIBUTION_VALUE_EXTVAR_MIN_COLUMN_NAME), 
                        DataTableUtilities.GetNullableDouble(dr, Strings.DISTRIBUTION_VALUE_EXTVAR_MAX_COLUMN_NAME), 
                        DataTableUtilities.GetNullableDouble(dr, Strings.DISTRIBUTION_VALUE_DIST_VALUE_COLUMN_NAME),
                        DataTableUtilities.GetNullableInt(dr, Strings.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME), 
                        ValueDistributionFrequency, 
                        DataTableUtilities.GetNullableDouble(dr, Strings.DISTRIBUTION_VALUE_VALUE_DIST_SD_COLUMN_NAME), 
                        DataTableUtilities.GetNullableDouble(dr, Strings.DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME),
                        DataTableUtilities.GetNullableDouble(dr, Strings.DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME), 
                        DataTableUtilities.GetNullableDouble(dr, Strings.DISTRIBUTION_VALUE_VALUE_DIST_RELATIVE_FREQUENCY_COLUMN_NAME));

                    this.Validate(Item.ValueDistributionTypeId, Item.Value, Item.ValueDistributionSD, Item.ValueDistributionMin, Item.ValueDistributionMax);

                    this.m_DistributionValues.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void CreateDistributionValueMap()
        {
            Debug.Assert(this.m_DistributionValueMap == null);
            this.m_DistributionValueMap = new STSimDistributionValueMap();

            foreach (STSimDistributionValue Value in this.m_DistributionValues)
            {
                this.m_DistributionValueMap.AddValue(Value);
            }
        }

        private double STSimGetUserDistribution(int distributionTypeId, int iteration, int timestep, int? stratumId, int? secondaryStratumId)
        {
            const string SAMPLE_ERROR = "Attempted to sample from a distribution that has no corresponding distribution values in the scenario.  More information:" + "\r\n" + "Type={0}, Iteration={1}, Timestep={2}, Stratum={3}, SecondaryStratum={4}";

            DistributionValueCollection Values = this.m_DistributionValueMap.GetValues(distributionTypeId, iteration, timestep, stratumId, secondaryStratumId);

            if (Values == null)
            {
                this.ThrowNoValuesException(SAMPLE_ERROR, distributionTypeId, iteration, timestep, stratumId, secondaryStratumId);
            }

            return base.SampleUserDistribution(Values, distributionTypeId, iteration, timestep);
        }

        private string GetProjectItemName(string dataSheetName, int id)
        {
            DataSheet ds = this.Scenario.Project.GetDataSheet(dataSheetName);
            return ds.ValidationTable.GetDisplayName(id);
        }

        private void ThrowNoValuesException(string message, int distributionTypeId, int iteration, int timestep, int? stratumId, int? secondaryStratumId)
        {
            string StratumName = "NULL";
            string SecondaryStratumName = "NULL";

            if (stratumId.HasValue)
            {
                StratumName = this.GetProjectItemName(Strings.DATASHEET_STRATA_NAME, stratumId.Value);
            }

            if (secondaryStratumId.HasValue)
            {
                SecondaryStratumName = this.GetProjectItemName(Strings.DATASHEET_SECONDARY_STRATA_NAME, secondaryStratumId.Value);
            }

            ExceptionUtils.ThrowInvalidOperationException(message, this.GetProjectItemName(Strings.DISTRIBUTION_TYPE_DATASHEET_NAME, distributionTypeId), iteration, timestep, StratumName, SecondaryStratumName);
        }
    }
}
