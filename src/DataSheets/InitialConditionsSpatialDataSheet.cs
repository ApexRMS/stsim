// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    public class InitialConditionsSpatialDataSheet : DataSheet
    {
        public event EventHandler<EventArgs> ValidatingRasters;
        public event EventHandler<EventArgs> RastersValidated;

        private DataSheetMonitor m_TerminologyMonitor;
        private bool m_IsDisposed;

        protected override void Initialize(DataStore store)
        {
            base.Initialize(store);

            this.m_TerminologyMonitor = new DataSheetMonitor(this.Project, Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.m_IsDisposed)
            {
                if (this.m_TerminologyMonitor != null)
                {
                    this.m_TerminologyMonitor.Dispose();
                }

                this.m_IsDisposed = true;
            }

            base.Dispose(disposing);
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            if (this.Scenario.IsResult)
            {
                return;
            }

            string Primary = null;
            string Secondary = null;
            string Tertiary = null;
            string AmountLabel = null;
            TerminologyUnit AmountUnits = TerminologyUnit.None;

            TerminologyUtilities.GetStratumLabelTerminology(e.DataSheet, ref Primary, ref Secondary, ref Tertiary);
            TerminologyUtilities.GetAmountLabelTerminology(e.DataSheet, ref AmountLabel, ref AmountUnits);

            DataSheet dsProp = this.GetDataSheet(Strings.DATASHEET_SPPIC_NAME);
            DataRow drProp = dsProp.GetDataRow();

            if (drProp == null)
            {
                return;
            }

            //Num Cells
            int NumCells = DataTableUtilities.GetDataInt(drProp[Strings.DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME]);

            //Get the units and refresh the units labels - the default Raster Cell Units is Metres^2
            string srcSizeUnits = DataTableUtilities.GetDataStr(drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME]);
            string srcAreaUnits = srcSizeUnits + "^2";
            string amountlabel = null;
            TerminologyUnit destUnitsVal = 0;

            TerminologyUtilities.GetAmountLabelTerminology(
                this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref amountlabel, ref destUnitsVal);

            string destAreaLbl = TerminologyUtilities.TerminologyUnitToString(destUnitsVal);

            srcAreaUnits = srcAreaUnits.ToLower(CultureInfo.InvariantCulture);
            amountlabel = amountlabel.ToLower(CultureInfo.InvariantCulture);
            destAreaLbl = destAreaLbl.ToLower(CultureInfo.InvariantCulture);

            // Calculate Cell Area in raster's native units
            float cellSize = DataTableUtilities.GetDataSingle(drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME]);
            double cellArea = Math.Pow(cellSize, 2);

            // Calc Cell Area in terminology units
            double cellAreaTU = 0;
            bool SizeOverride = DataTableUtilities.GetDataBool(drProp[Strings.DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME]);
            double? currentCellArea = (double)drProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME];

            if (!SizeOverride)
            {
                cellAreaTU = InitialConditionsSpatialDataSheet.CalcCellArea(cellArea, srcSizeUnits, destUnitsVal);
                drProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME] = cellAreaTU;
            }
            else
            {
                cellAreaTU = DataTableUtilities.GetDataDbl(drProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME]);
            }

            if (currentCellArea == null || currentCellArea != cellAreaTU)
            {
                dsProp.Changes.Add(new ChangeRecord(this, "Changed Cell Size"));
            }
        }



        public override void Validate(DataRow proposedRow, DataTransferMethod transferMethod)
        {
            base.Validate(proposedRow, transferMethod);

            List<string> ColNames = new List<string>();

            ColNames.Add(Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME);
            ColNames.Add(Strings.DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME);
            ColNames.Add(Strings.DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME);
            ColNames.Add(Strings.DATASHEET_SPIC_AGE_FILE_COLUMN_NAME);

            DataTable ThisData = this.GetData();
            StochasticTimeRaster FirstRaster = null;

            if (ThisData.DefaultView.Count == 0)
            {
                FirstRaster = this.LoadRaster(proposedRow, Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME);
            }
            else
            {
                FirstRaster = this.LoadRaster(ThisData.DefaultView[0].Row, Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME);
            }

            try
            {
                ValidatingRasters?.Invoke(this, new EventArgs());

                foreach (string s in ColNames)
                {
                    if (proposedRow[s] != DBNull.Value)
                    {
                        StochasticTimeRaster rast = this.LoadRaster(proposedRow, s);

                        try
                        {
                            this.ValidateRaster(rast, FirstRaster.Height, FirstRaster.Width, s);
                        }
                        catch (Exception)
                        {
                            proposedRow[s] = DBNull.Value;
                            throw;
                        }
                    }
                }
            }
            finally
            {
                RastersValidated?.Invoke(this, new EventArgs());
            }
        }

        protected override void BeforeImportData(DataTable proposedData, string importFileName)
        {
            base.BeforeImportData(proposedData, importFileName);

            if (proposedData.Rows.Count == 0)
            {
                return;
            }

            List<string> ColNames = new List<string>();

            ColNames.Add(Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME);
            ColNames.Add(Strings.DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME);
            ColNames.Add(Strings.DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME);
            ColNames.Add(Strings.DATASHEET_SPIC_AGE_FILE_COLUMN_NAME);

            DataTable ThisData = this.GetData();
            StochasticTimeRaster FirstRaster = null;

            if (ThisData.DefaultView.Count == 0)
            {
                FirstRaster = this.LoadRaster(proposedData.Rows[0], Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME);
            }
            else
            {
                FirstRaster = this.LoadRaster(ThisData.DefaultView[0].Row, Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME);
            }

            try
            {
                ValidatingRasters?.Invoke(this, new EventArgs());

                foreach (DataRow dr in proposedData.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        foreach (string s in ColNames)
                        {
                            if (dr[s] != DBNull.Value)
                            {
                                StochasticTimeRaster rast = this.LoadRaster(dr, s);

                                try
                                {
                                    this.ValidateRaster(rast, FirstRaster.Height, FirstRaster.Width, s);
                                }
                                catch (Exception)
                                {
                                    dr[s] = DBNull.Value;
                                    throw;
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                RastersValidated?.Invoke(this, new EventArgs());
            }
        }

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);

            var ThisData = this.GetData();
            DataSheet dsProp = this.GetDataSheet(Strings.DATASHEET_SPPIC_NAME);
            DataRow drProp = dsProp.GetDataRow();

            if (drProp == null && ThisData.DefaultView.Count > 0)
            {
                dsProp.BeginAddRows();
                drProp = dsProp.GetData().NewRow();

                DataRow FirstRow = ThisData.DefaultView[0].Row;
                StochasticTimeRaster FirstRast = this.LoadRaster(FirstRow, Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME);

                if (FirstRast.IntCells == null)
                {
                    FirstRast.LoadData();
                }

                drProp[Strings.DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME] = FirstRast.Height;
                drProp[Strings.DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME] = FirstRast.Width;
                drProp[Strings.DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME] = FirstRast.GetNumberValidCells();
                drProp[Strings.DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME] = FirstRast.XllCorner;
                drProp[Strings.DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME] = FirstRast.YllCorner;
                drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME] = FirstRast.CellSize;
                drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME] = FirstRast.CellSizeUnits;
                drProp[Strings.DATASHEET_SPPIC_SRS_COLUMN_NAME] = FirstRast.Projection;
                drProp[Strings.DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME] = false;

                string amountlabel = null;
                TerminologyUnit destUnitsVal = 0;
                double cellArea = System.Math.Pow((double)FirstRast.CellSize, 2);

                TerminologyUtilities.GetAmountLabelTerminology(this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref amountlabel, ref destUnitsVal);
                drProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME] = CalcCellArea(cellArea, FirstRast.CellSizeUnits, destUnitsVal);

                dsProp.Changes.Add(new ChangeRecord(this, "Added raster metadata"));
                dsProp.GetData().Rows.Add(drProp);
                dsProp.EndAddRows();
            }
        }

        protected override void OnRowsDeleted(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsDeleted(sender, e);

            if (this.GetData().DefaultView.Count == 0)
            {
                DataSheet PropsDataSheet = this.GetDataSheet(Strings.DATASHEET_SPPIC_NAME);

                PropsDataSheet.GetData();
                PropsDataSheet.ClearData();
            }
        }

        private StochasticTimeRaster LoadRaster(DataRow dr, string fileNameColumn)
        {
            string FileName = Convert.ToString(dr[fileNameColumn], CultureInfo.InvariantCulture);
            string InputFilename = Spatial.GetSpatialInputFileName(this, FileName, true);

            return new StochasticTimeRaster(
                InputFilename, 
                RasterDataType.DTInteger, 
                false, 
                Spatial.UndefinedRasterBand);
        }

        private void ValidateRaster(StochasticTimeRaster rast, int rows, int columns, string columnName)
        {
            string PrimaryStratumLabel = null;
            string SecondaryStratumLabel = null;
            string TertiaryStratumLabel = null;
            string ColumnDisplayName = this.Columns[columnName].DisplayName;

            DataSheet TerminologySheet =
                this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetStratumLabelTerminology(
                TerminologySheet, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);

            if (columnName == Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
            {
                ColumnDisplayName = PrimaryStratumLabel;
            }
            else if (columnName == Strings.DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME)
            {
                ColumnDisplayName = SecondaryStratumLabel;
            }

            if (rast.Height != rows)
            {
                string msg = null;

                if (columnName == Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
                {
                    msg = string.Format(CultureInfo.InvariantCulture,
                        "The number of rows for the '{0}' raster does not match that of the other '{1}' rasters.",
                        PrimaryStratumLabel, PrimaryStratumLabel);
                }
                else
                {
                    msg = string.Format(CultureInfo.InvariantCulture,
                        "The number of rows for the '{0}' raster does not match that of the '{1}' raster.",
                        ColumnDisplayName, PrimaryStratumLabel);
                }

                throw new DataException(msg);
            }
            else if (rast.Width != columns)
            {
                string msg = null;

                if (columnName == Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
                {
                    msg = string.Format(CultureInfo.InvariantCulture,
                        "The number of columns for the '{0}' raster does not match that of the other '{1}' rasters.",
                        PrimaryStratumLabel, PrimaryStratumLabel);
                }
                else
                {
                    msg = string.Format(CultureInfo.InvariantCulture,
                        "The number of columns for the '{0}' raster does not match that of the '{1}' raster.",
                        ColumnDisplayName, PrimaryStratumLabel);
                }

                throw new DataException(msg);
            }
        }

        /// <summary>
        /// Convert the Cell Area as specified in the raster units to Cell Area as specified in user-configurable Terminology Units
        /// </summary>
        /// <param name="srcCellArea">The Cell Area in the raster files native units</param>
        /// <param name="srcSizeUnits">The native linear size units of the raster files</param>
        /// <param name="destAreaUnits">The specified Area Units</param>
        /// <returns>The calculated Cell Area</returns>
        /// <remarks></remarks>
        public static double CalcCellArea(double srcCellArea, string srcSizeUnits, TerminologyUnit destAreaUnits)
        {
            double convFactor = 0;

            // Convert the Source Area to M2
            srcSizeUnits = srcSizeUnits.Replace(" ", "_"); // replace space with an underscore

            // Convert from ft^2 to M2
            if ((srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.Foot.ToString().ToUpper(CultureInfo.InvariantCulture)) ||
                (srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.Foot_US.ToString().ToUpper(CultureInfo.InvariantCulture)) ||
                (srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.US_survey_foot.ToString().ToUpper(CultureInfo.InvariantCulture)))
            {
                convFactor = 0.092903;
            }
            else if (
                (srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.Metre.ToString().ToUpper(CultureInfo.InvariantCulture)) ||
                (srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.Meter.ToString().ToUpper(CultureInfo.InvariantCulture)) ||
                (srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.Meters.ToString().ToUpper(CultureInfo.InvariantCulture)))
            {
                // No conversion needed for Meters
                convFactor = 1;
            }
            else if (
                (srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.Undefined.ToString().ToUpper(CultureInfo.InvariantCulture)) ||
                (srcSizeUnits.ToUpper(CultureInfo.InvariantCulture) == RasterCellSizeUnit.Undetermined.ToString().ToUpper(CultureInfo.InvariantCulture)))
            {
                return 0;
            }

            var areaM2 = srcCellArea * convFactor;

            // Now lets convert M2 to Terminology Units

            //Calculate the total area and cell size
            switch (destAreaUnits)
            {
                case TerminologyUnit.Acres:
                    // 1m2 = 0.000247105 Acres
                    convFactor = 0.000247105;

                    break;
                case TerminologyUnit.Hectares:
                    // 1m2 = 0.0001 Hectares
                    convFactor = 0.0001;

                    break;
                case TerminologyUnit.SquareKilometers:
                    // 1m2 = 1e-6 Km2
                    convFactor = 0.000001;

                    break;
                case TerminologyUnit.SquareMiles:
                    // 1m2 = 3.861e-7 Mi2
                    convFactor = 0.0000003861;
                    break;
                default:
                    convFactor = 0;
                    break;
            }

            return areaM2 * convFactor;
        }
    }
}
