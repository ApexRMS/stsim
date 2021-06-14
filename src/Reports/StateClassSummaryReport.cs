// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data.Common;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
    internal class StateClassSummaryReport : ExportTransformer
    {
        private const string CSV_INTEGER_FORMAT = "F0";
        private const string CSV_DOUBLE_FORMAT = "F4";

        protected override void Export(string location, ExportType exportType)
        {
            this.InternalExport(location, exportType, true);
        }

        internal void InternalExport(string location, ExportType exportType, bool showMessage)
        {
            if (exportType == ExportType.ExcelFile)
            {
                this.CreateExcelReport(location);
            }
            else
            {
                this.CreateCSVReport(location);

                if (showMessage)
                {
                    FormsUtilities.InformationMessageBox("Data saved to '{0}'.", location);
                }
            }
        }

        private ExportColumnCollection CreateColumnCollection()
        {
            ExportColumnCollection c = new ExportColumnCollection();

            string AmountLabel = null;
            string UnitsLabel = null;
            TerminologyUnit TermUnit = 0;
            string PrimaryStratumLabel = null;
            string SecondaryStratumLabel = null;
            string TertiaryStratumLabel = null;
            DataSheet dsterm = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            string TimestepLabel = TerminologyUtilities.GetTimestepUnits(this.Project);

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);
            TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);
            UnitsLabel = TerminologyUtilities.TerminologyUnitToString(TermUnit);
            string AmountTitle = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel);

            c.Add(new ExportColumn("ScenarioID", "Scenario ID"));
            c.Add(new ExportColumn("ScenarioName", "Scenario"));
            c.Add(new ExportColumn("Iteration", "Iteration"));
            c.Add(new ExportColumn("Timestep", TimestepLabel));
            c.Add(new ExportColumn("Stratum", PrimaryStratumLabel));
            c.Add(new ExportColumn("SecondaryStratum", SecondaryStratumLabel));
            c.Add(new ExportColumn("TertiaryStratum", TertiaryStratumLabel));
            c.Add(new ExportColumn("StateClass", "State Class"));
            c.Add(new ExportColumn("AgeMin", "Age Min"));
            c.Add(new ExportColumn("AgeMax", "Age Max"));
            c.Add(new ExportColumn("Amount", AmountTitle));

            c["Amount"].DecimalPlaces = 2;
            c["Amount"].Alignment = ColumnAlignment.Right;

            return c;
        }

        private void CreateExcelReport(string fileName)
        {
            string AmountLabel = null;
            TerminologyUnit AmountLabelUnits = 0;
            string ReportQuery = CreateReportQuery(false);
            DataTable ReportData = this.GetDataTableForReport(ReportQuery);
            DataSheet dsterm = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref AmountLabelUnits);
            string WorksheetName = string.Format(CultureInfo.InvariantCulture, "{0} by State Class", AmountLabel);

            ExportTransformer.ExcelExport(fileName, this.CreateColumnCollection(), ReportData, WorksheetName);
        }

        private void CreateCSVReport(string fileName)
        {
            using (DataStore store = this.Library.CreateDataStore())
            {
                this.CreateCSVReport(fileName, store);
            }
        }

        private void CreateCSVReport(string fileName, DataStore store)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write("ScenarioID,");
                sw.Write("Iteration,");
                sw.Write("Timestep,");
                sw.Write("StratumID,");
                sw.Write("SecondaryStratumID,");
                sw.Write("TertiaryStratumID,");
                sw.Write("StateClassID,");
                sw.Write("AgeMin,");
                sw.Write("AgeMax,");
                sw.Write("Amount,");

                using (DbCommand cmd = store.CreateCommand())
                {
                    cmd.CommandText = CreateReportQuery(true);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = store.DatabaseConnection;

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sw.Write(Environment.NewLine);

                            int ScenarioId = reader.GetInt32(0);
                            int Iteration = reader.GetInt32(1);
                            int Timestep = reader.GetInt32(2);
                            int? SecondaryStratumId = null;
                            int? TertiaryStratumId = null;
                            string SecondaryStratumName = null;
                            string TertiaryStratumName = null;

                            if (!reader.IsDBNull(4))
                            {
                                SecondaryStratumId = reader.GetInt32(4);
                            }

                            if (!reader.IsDBNull(5))
                            {
                                TertiaryStratumId = reader.GetInt32(5);
                            }

                            string StratumName = reader.GetString(6);

                            if (!reader.IsDBNull(7))
                            {
                                SecondaryStratumName = reader.GetString(7);
                            }

                            if (!reader.IsDBNull(8))
                            {
                                TertiaryStratumName = reader.GetString(8);
                            }

                            string StateClass = reader.GetString(9);
                            int? AgeMin = null;

                            if (!reader.IsDBNull(10))
                            {
                                AgeMin = reader.GetInt32(10);
                            }

                            int? AgeMax = null;

                            if (!reader.IsDBNull(11))
                            {
                                AgeMax = reader.GetInt32(11);
                            }

                            double Amount = reader.GetDouble(12);

                            sw.Write(CSVFormatInteger(ScenarioId));
                            sw.Write(",");

                            sw.Write(CSVFormatInteger(Iteration));
                            sw.Write(",");

                            sw.Write(CSVFormatInteger(Timestep));
                            sw.Write(",");

                            sw.Write(CSVFormatString(StratumName));
                            sw.Write(",");

                            if (SecondaryStratumName != null)
                            {
                                sw.Write(CSVFormatString(SecondaryStratumName));
                            }

                            sw.Write(",");

                            if (TertiaryStratumName != null)
                            {
                                sw.Write(CSVFormatString(TertiaryStratumName));
                            }

                            sw.Write(",");

                            sw.Write(CSVFormatString(StateClass));
                            sw.Write(",");

                            if (AgeMin.HasValue)
                            {
                                sw.Write(CSVFormatInteger(AgeMin.Value));
                            }

                            sw.Write(",");

                            if (AgeMax.HasValue)
                            {
                                sw.Write(CSVFormatInteger(AgeMax.Value));
                            }

                            sw.Write(",");

                            sw.Write(CSVFormatDouble(Amount));
                            sw.Write(",");
                        }
                    }
                }
            }
        }

        private string CreateReportQuery(bool isCSV)
        {
            string ScenFilter = this.CreateActiveResultScenarioFilter();

            if (isCSV)
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputStratumState.ScenarioID, " + "stsim_OutputStratumState.Iteration,  " 
                    + "stsim_OutputStratumState.Timestep,  " + "stsim_OutputStratumState.StratumID, " + "stsim_OutputStratumState.SecondaryStratumID, " +
                    "stsim_OutputStratumState.TertiaryStratumID, " + "stsim_Stratum.Name AS Stratum,  " + "stsim_SecondaryStratum.Name AS SecondaryStratum,  " + 
                    "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + "stsim_StateClass.Name as StateClass, " + "stsim_OutputStratumState.AgeMin, " + 
                    "stsim_OutputStratumState.AgeMax, " + "stsim_OutputStratumState.Amount " + "FROM stsim_OutputStratumState " + 
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumID = stsim_OutputStratumState.StratumID " + 
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumID = stsim_OutputStratumState.SecondaryStratumID " +
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumID = stsim_OutputStratumState.TertiaryStratumID " +
                    "INNER JOIN stsim_StateClass ON stsim_StateClass.StateClassID = stsim_OutputStratumState.StateClassID " + 
                    "WHERE stsim_OutputStratumState.ScenarioID IN ({0})  " + "ORDER BY " + "stsim_OutputStratumState.ScenarioID, " + 
                    "stsim_OutputStratumState.Iteration, " + "stsim_OutputStratumState.Timestep, " + "stsim_OutputStratumState.StratumID, " + 
                    "stsim_OutputStratumState.SecondaryStratumID, " + "stsim_OutputStratumState.TertiaryStratumID, " + "stsim_Stratum.Name, " +
                    "stsim_SecondaryStratum.Name, " + "stsim_TertiaryStratum.Name, " + "stsim_StateClass.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputStratumState.ScenarioID, " + "core_Scenario.Name AS ScenarioName,  " + 
                    "stsim_OutputStratumState.Iteration,  " + "stsim_OutputStratumState.Timestep,  " +
                    "stsim_OutputStratumState.StratumID, " + "stsim_OutputStratumState.SecondaryStratumID, " +
                    "stsim_OutputStratumState.TertiaryStratumID, " + "stsim_Stratum.Name AS Stratum,  " + 
                    "stsim_SecondaryStratum.Name AS SecondaryStratum,  " + "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + 
                    "stsim_StateClass.Name as StateClass, " + "stsim_OutputStratumState.AgeMin, " + "stsim_OutputStratumState.AgeMax, " + 
                    "stsim_OutputStratumState.Amount " + "FROM stsim_OutputStratumState " + 
                    "INNER JOIN core_Scenario ON core_Scenario.ScenarioID = stsim_OutputStratumState.ScenarioID " + 
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumID = stsim_OutputStratumState.StratumID " + 
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumID = stsim_OutputStratumState.SecondaryStratumID " +
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumID = stsim_OutputStratumState.TertiaryStratumID " + 
                    "INNER JOIN stsim_StateClass ON stsim_StateClass.StateClassID = stsim_OutputStratumState.StateClassID " + 
                    "WHERE stsim_OutputStratumState.ScenarioID IN ({0})  " + "ORDER BY " + "stsim_OutputStratumState.ScenarioID, " +
                    "core_Scenario.Name, " + "stsim_OutputStratumState.Iteration, " + "stsim_OutputStratumState.Timestep, " + 
                    "stsim_OutputStratumState.StratumID, " + "stsim_OutputStratumState.SecondaryStratumID, " + 
                    "stsim_OutputStratumState.TertiaryStratumID, " + "stsim_Stratum.Name, " + "stsim_SecondaryStratum.Name, " + 
                    "stsim_TertiaryStratum.Name, " + "stsim_StateClass.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
        }

        private DataTable GetDataTableForReport(string reportQuery)
        {
            using (DataStore store = this.Library.CreateDataStore())
            {
                DataTable dt = store.CreateDataTableFromQuery(reportQuery, "State Class Summary");
                return dt;
            }
        }

        /// <summary>
        /// Creates a SQL filter from the specified integers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string CreateIntegerFilter(IEnumerable<int> values)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int id in values)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "{0},", id);
            }

            Debug.Assert(values.Count() > 0);
            return sb.ToString().TrimEnd(',');
        }

        private static string CSVFormatInteger(Int32 value)
        {
            return value.ToString(CSV_INTEGER_FORMAT, CultureInfo.InvariantCulture);
        }

        private static string CSVFormatInteger(Int64 value)
        {
            return value.ToString(CSV_INTEGER_FORMAT, CultureInfo.InvariantCulture);
        }

        private static string CSVFormatDouble(double value)
        {
            return value.ToString(CSV_DOUBLE_FORMAT, CultureInfo.InvariantCulture);
        }

        private static string CSVFormatString(string value)
        {
            return InternalFormatStringCSV(value);
        }

        private static string InternalFormatStringCSV(string value)
        {
            bool ContainsComma = value.Contains(','.ToString());
            bool ContainsQuote = value.Contains('\"'.ToString());

            if (!ContainsComma && !ContainsQuote)
            {
                return value;
            }

            if (ContainsQuote)
            {
                string s = value.Replace("\"", "\"\"");
                return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", s);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", value);
            }
        }
    }
}
