// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Diagnostics;
using System.Globalization;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Utilities relating to the terminology data feed
    /// </summary>
    /// <remarks></remarks>
    internal static class TerminologyUtilities
    {
        public static string GetTimestepUnits(Project project)
        {
            DataRow dr = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME).GetDataRow();

            if (dr == null || dr["TimestepUnits"] == DBNull.Value)
            {
                return "Timestep";
            }
            else
            {
                return Convert.ToString(dr["TimestepUnits"], CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Converts a terminology unit to its corresponding string
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string TerminologyUnitToString(TerminologyUnit unit)
        {
            if (unit == TerminologyUnit.Acres)
            {
                return "Acres";
            }
            else if (unit == TerminologyUnit.Hectares)
            {
                return "Hectares";
            }
            else if (unit == TerminologyUnit.SquareKilometers)
            {
                return "Square Kilometers";
            }
            else if (unit == TerminologyUnit.SquareMiles)
            {
                return "Square Miles";
            }
            else if (unit == TerminologyUnit.None)
            {
                return "None";
            }

            Debug.Assert(false);
            return null;
        }

        /// <summary>
        /// Gets the amount label terminology
        /// </summary>
        /// <param name="store"></param>
        /// <param name="terminologyDataSheet"></param>
        /// <param name="amountLabel"></param>
        /// <param name="amountUnits"></param>
        /// <remarks></remarks>
        public static void GetAmountLabelTerminology(
            DataSheet terminologyDataSheet, 
            ref string amountLabel, 
            ref TerminologyUnit amountUnits)
        {
            DataRow dr = terminologyDataSheet.GetDataRow();

            amountLabel = "Amount";
            amountUnits = TerminologyUnit.None;

            if (dr != null)
            {
                if (dr[Strings.DATASHEET_TERMINOLOGY_AMOUNT_LABEL_COLUMN_NAME] != DBNull.Value)
                {
                    amountLabel = Convert.ToString(
                        dr[Strings.DATASHEET_TERMINOLOGY_AMOUNT_LABEL_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERMINOLOGY_AMOUNT_UNITS_COLUMN_NAME] != DBNull.Value)
                {
                    int value = Convert.ToInt32(
                        dr[Strings.DATASHEET_TERMINOLOGY_AMOUNT_UNITS_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);

                    amountUnits = (TerminologyUnit)value;
                }
            }
        }

        /// <summary>
        /// Gets the state label terminology
        /// </summary>
        /// <param name="store"></param>
        /// <param name="terminologyDataSheet"></param>
        /// <param name="slxlabel"></param>
        /// <param name="slylabel"></param>
        /// <remarks></remarks>
        public static void GetStateLabelTerminology(
            DataSheet terminologyDataSheet, 
            ref string slxlabel, 
            ref string slylabel)
        {
            DataRow dr = terminologyDataSheet.GetDataRow();

            slxlabel = "State Label X";
            slylabel = "State Label Y";

            if (dr != null)
            {
                if (dr[Strings.DATASHEET_TERMINOLOGY_STATELABELX_COLUMN_NAME] != DBNull.Value)
                {
                    slxlabel = Convert.ToString(
                        dr[Strings.DATASHEET_TERMINOLOGY_STATELABELX_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERMINOLOGY_STATELABELY_COLUMN_NAME] != DBNull.Value)
                {
                    slylabel = Convert.ToString(
                        dr[Strings.DATASHEET_TERMINOLOGY_STATELABELY_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Gets the stratum label terminology
        /// </summary>
        /// <param name="store"></param>
        /// <param name="terminologyDataSheet"></param>
        /// <param name="primaryStratumLabel"></param>
        /// <param name="secondaryStratumLabel"></param>
        /// <param name="tertiaryStratumLabel"></param>
        /// <remarks></remarks>
        public static void GetStratumLabelTerminology(
            DataSheet terminologyDataSheet, 
            ref string primaryStratumLabel, 
            ref string secondaryStratumLabel, 
            ref string tertiaryStratumLabel)
        {
            DataRow dr = terminologyDataSheet.GetDataRow();

            primaryStratumLabel = "Stratum";
            secondaryStratumLabel = "Secondary Stratum";
            tertiaryStratumLabel = "Tertiary Stratum";

            if (dr != null)
            {
                if (dr[Strings.DATASHEET_TERMINOLOGY_PRIMARY_STRATUM_LABEL_COLUMN_NAME] != DBNull.Value)
                {
                    primaryStratumLabel = Convert.ToString(
                        dr[Strings.DATASHEET_TERMINOLOGY_PRIMARY_STRATUM_LABEL_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME] != DBNull.Value)
                {
                    secondaryStratumLabel = Convert.ToString(
                        dr[Strings.DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERMINOLOGY_TERTIARY_STRATUM_LABEL_COLUMN_NAME] != DBNull.Value)
                {
                    tertiaryStratumLabel = Convert.ToString(
                        dr[Strings.DATASHEET_TERMINOLOGY_TERTIARY_STRATUM_LABEL_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
