// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal partial class ResolutionTransformer
    {
		public static bool CanDoMultiResolution(Scenario resultScenario)
		{
			DataRow dr = resultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME).GetDataRow();

			//If it's not a spatial run then we can't do anything
			if (!GetDataBool(dr[Strings.RUN_CONTROL_IS_SPATIAL_COLUMN_NAME]))
			{
				return false;
			}

			//If there are no resolution groups then we can't do anything
			TransitionGroupResolutionCollection ResGroups = CreateResolutionGroupCollection(resultScenario);
			{
				if (ResGroups.Count == 0)
				{
					return false;
				}
			}

			//If there are no initial conditions rasters we can't do anything
			InitialConditionsSpatialCollection MultiResColl = CreateSPICCollection(resultScenario, Strings.DATASHEET_SPICF_NAME);
			InitialConditionsFineSpatialMap MultiResMap = new InitialConditionsFineSpatialMap(MultiResColl);

			if (MultiResMap.AllItems.Count == 0)
			{
				return false;
			}

			return true;
		}

		public static bool GetDataBool(object value)
		{
			if (object.ReferenceEquals(value, DBNull.Value))
			{
				return false;
			}
			else
			{
				return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
			}
		}

		public static InitialConditionsSpatialCollection CreateSPICCollection(Scenario resultScenario, string datasheetName)
		{
			InitialConditionsSpatialCollection c = new InitialConditionsSpatialCollection();
			DataSheet ds = resultScenario.GetDataSheet(datasheetName);

			foreach (DataRow dr in ds.GetData().Rows)
			{
				int? Iteration = null;
				string PrimaryStratumName;
				string SecondaryStratumName;
				string TertiaryStratumName;
				string StateClassName;
				string AgeName;

				if (dr[Strings.DATASHEET_SPICF_ITERATION_COLUMN_NAME] != DBNull.Value)
				{
					Iteration = Convert.ToInt32(dr[Strings.DATASHEET_SPICF_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
				}

				PrimaryStratumName = dr[Strings.DATASHEET_SPICF_STRATUM_FILE_COLUMN_NAME].ToString();
				SecondaryStratumName = dr[Strings.DATASHEET_SPICF_SECONDARY_STRATUM_FILE_COLUMN_NAME].ToString();
				TertiaryStratumName = dr[Strings.DATASHEET_SPICF_TERTIARY_STRATUM_FILE_COLUMN_NAME].ToString();
				StateClassName = dr[Strings.DATASHEET_SPICF_STATE_CLASS_FILE_COLUMN_NAME].ToString();
				AgeName = dr[Strings.DATASHEET_SPICF_AGE_FILE_COLUMN_NAME].ToString();

				InitialConditionsSpatial InitialStateRecord = new InitialConditionsSpatial(
					Iteration, PrimaryStratumName, SecondaryStratumName, TertiaryStratumName, StateClassName, AgeName);

				c.Add(InitialStateRecord);
			}

			return c;
		}

		public static TransitionGroupResolutionCollection CreateResolutionGroupCollection(Scenario scenario)
		{
			TransitionGroupResolutionCollection groups = new TransitionGroupResolutionCollection();
			DataSheet ds = scenario.GetDataSheet(Strings.DATASHEET_TRG_NAME);

			foreach (DataRow dr in ds.GetData().Rows)
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					int Id = Convert.ToInt32(dr[Strings.DATASHEET_TRG_TGID_COLUMN_NAME], CultureInfo.InvariantCulture);
					Resolution Res = (Resolution)(long)Convert.ToInt32(dr[Strings.DATASHEET_TRG_RESOLUTION_COLUMN_NAME]);
					Double Propn = 0.0;

					if (dr[Strings.DATASHEET_FFB_THRESHOLD_PROPORTION_COLUMN_NAME] != DBNull.Value)
					{
						Propn = Convert.ToDouble(dr[Strings.DATASHEET_FFB_THRESHOLD_PROPORTION_COLUMN_NAME]);
					}

					groups.Add(new TransitionGroupResolution(Id, Res, Propn));
				}
			}

			return groups;
		}
	}
}
