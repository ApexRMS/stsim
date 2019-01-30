// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.StochasticTime;
using SyncroSim.Common.Forms;
using System.Data;
using System.Linq;

namespace SyncroSim.STSim
{
    internal partial class InitialConditionsSpatialDataFeedView
    {
        public InitialConditionsSpatialDataFeedView()
        {
            InitializeComponent();
        }

        private bool m_InRefresh;
        private bool m_CellAreaCalcHasChanges;
        private DataFeedView m_ICSpatialFilesView;
        private DataGridView m_ICSpatialFilesDataGrid;
        private InitialConditionsSpatialDataSheet m_ICSpatialFilesDataSheet;
        private bool m_ColumnsInitialized;
        private HourGlass m_HourGlass;
        private Color m_ReadOnlyColor = Color.FromArgb(232, 232, 232);
        private delegate void DelegateNoArgs();

        private const int ITERATION_COLUMN_INDEX = 0;
        private const int PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX = 1;
        private const int PRIMARY_STRATUM_BROWSE_COLUMN_INDEX = 2;
        private const int SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX = 3;
        private const int SECONDARY_STRATUM_BROWSE_COLUMN_INDEX = 4;
        private const int TERTIARY_STRATUM_FILE_NAME_COLUMN_INDEX = 5;
        private const int TERTIARY_STRATUM_BROWSE_COLUMN_INDEX = 6;
        private const int SCLASS_FILE_NAME_COLUMN_INDEX = 7;
        private const int SCLASS_BROWSE_COLUMN_INDEX = 8;
        private const int AGE_FILE_NAME_COLUMN_INDEX = 9;
        private const int AGE_BROWSE_COLUMN_INDEX = 10;
        private const string BROWSE_BUTTON_TEXT = "...";

        protected override void InitializeView()
        {
            base.InitializeView();

            this.m_ICSpatialFilesView = (this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario));
            this.m_ICSpatialFilesDataGrid = ((MultiRowDataFeedView)this.m_ICSpatialFilesView).GridControl;
            this.PanelInitialConditionSpatialFiles.Controls.Add(this.m_ICSpatialFilesView);
            this.LabelValidate.Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                if (this.m_HourGlass != null)
                {
                    this.m_HourGlass.Dispose();
                }

                if (this.m_ICSpatialFilesDataSheet != null)
                {
                    this.m_ICSpatialFilesDataSheet.ValidatingRasters -= this.OnValidatingRasters;
                    this.m_ICSpatialFilesDataSheet.RastersValidated -= this.OnRastersValidated;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.m_ICSpatialFilesView.LoadDataFeed(dataFeed, Strings.DATASHEET_SPIC_NAME);

            if (!this.m_ColumnsInitialized)
            {
                //Add handlers
                this.m_ICSpatialFilesDataGrid.CellEnter += this.OnGridCellEnter;
                this.m_ICSpatialFilesDataGrid.CellMouseDown += this.OnGridCellMouseDown;
                this.m_ICSpatialFilesDataGrid.DataBindingComplete += this.OnGridBindingComplete;
                this.m_ICSpatialFilesDataGrid.KeyDown += this.OnGridKeyDown;

                //Add the browse button columns
                DataGridViewButtonColumn PrimStratumFileBrowseColumn = new DataGridViewButtonColumn();
                PrimStratumFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PrimStratumFileBrowseColumn.Width = 40;
                PrimStratumFileBrowseColumn.MinimumWidth = 40;
                this.m_ICSpatialFilesDataGrid.Columns.Insert(PRIMARY_STRATUM_BROWSE_COLUMN_INDEX, PrimStratumFileBrowseColumn);

                DataGridViewButtonColumn SecStratumFileBrowseColumn = new DataGridViewButtonColumn();
                SecStratumFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                SecStratumFileBrowseColumn.Width = 40;
                SecStratumFileBrowseColumn.MinimumWidth = 40;
                this.m_ICSpatialFilesDataGrid.Columns.Insert(SECONDARY_STRATUM_BROWSE_COLUMN_INDEX, SecStratumFileBrowseColumn);

                DataGridViewButtonColumn TertStratumFileBrowseColumn = new DataGridViewButtonColumn();
                TertStratumFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                TertStratumFileBrowseColumn.Width = 40;
                TertStratumFileBrowseColumn.MinimumWidth = 40;
                this.m_ICSpatialFilesDataGrid.Columns.Insert(TERTIARY_STRATUM_BROWSE_COLUMN_INDEX, TertStratumFileBrowseColumn);

                DataGridViewButtonColumn SClassFileBrowseColumn = new DataGridViewButtonColumn();
                SClassFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                SClassFileBrowseColumn.Width = 40;
                SClassFileBrowseColumn.MinimumWidth = 40;
                this.m_ICSpatialFilesDataGrid.Columns.Insert(SCLASS_BROWSE_COLUMN_INDEX, SClassFileBrowseColumn);

                DataGridViewButtonColumn AgeFileBrowseColumn = new DataGridViewButtonColumn();
                AgeFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                AgeFileBrowseColumn.Width = 40;
                AgeFileBrowseColumn.MinimumWidth = 40;
                this.m_ICSpatialFilesDataGrid.Columns.Insert(AGE_BROWSE_COLUMN_INDEX, AgeFileBrowseColumn);

                //Configure columns
                this.m_ICSpatialFilesDataGrid.Columns[PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX].DefaultCellStyle.BackColor = this.m_ReadOnlyColor;
                this.m_ICSpatialFilesDataGrid.Columns[SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX].DefaultCellStyle.BackColor = this.m_ReadOnlyColor;
                this.m_ICSpatialFilesDataGrid.Columns[TERTIARY_STRATUM_FILE_NAME_COLUMN_INDEX].DefaultCellStyle.BackColor = this.m_ReadOnlyColor;
                this.m_ICSpatialFilesDataGrid.Columns[SCLASS_FILE_NAME_COLUMN_INDEX].DefaultCellStyle.BackColor = this.m_ReadOnlyColor;
                this.m_ICSpatialFilesDataGrid.Columns[AGE_FILE_NAME_COLUMN_INDEX].DefaultCellStyle.BackColor = this.m_ReadOnlyColor;

                this.m_ColumnsInitialized = true;
            }

            this.MonitorDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged, true);
            this.m_ICSpatialFilesDataSheet = (InitialConditionsSpatialDataSheet)this.DataFeed.GetDataSheet(Strings.DATASHEET_SPIC_NAME);

            this.m_ICSpatialFilesDataSheet.ValidatingRasters += this.OnValidatingRasters;
            this.m_ICSpatialFilesDataSheet.RastersValidated += this.OnRastersValidated;
        }

        public override void RefreshControls()
        {
            this.m_InRefresh = true;
            this.ResetControls();
            this.RefreshNonCalculatedValues();
            this.RefreshCalculatedValues();
            this.m_InRefresh = false;
        }

        public override void EnableView(bool enable)
        {
            base.EnableView(enable);
            this.m_ICSpatialFilesView.EnableView(enable);
        }

        private void OnValidatingRasters(object sender, EventArgs e)
        {
            this.LabelValidate.Visible = true;
            Application.DoEvents();

            this.m_HourGlass = new HourGlass();

            //A slight delay so the user can see this message even if the validation is fast
            System.Threading.Thread.Sleep(500);
        }

        private void OnRastersValidated(object sender, EventArgs e)
        {
            this.LabelValidate.Visible = false;
            this.m_HourGlass.Dispose();
            this.m_HourGlass = null;
        }

        private void OnNewCellEnterAsync()
        {
            if (this.m_ICSpatialFilesDataGrid.CurrentCell == null)
            {
                return;
            }

            int Row = this.m_ICSpatialFilesDataGrid.CurrentCell.RowIndex;
            int Col = this.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex;

            switch (Col)
            {
                case PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX:
                case SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX:
                case TERTIARY_STRATUM_FILE_NAME_COLUMN_INDEX:
                case SCLASS_FILE_NAME_COLUMN_INDEX:
                case AGE_FILE_NAME_COLUMN_INDEX:

                    if (ModifierKeys == Keys.Shift)
                    {
                        Col -= 1;

                        while (!(this.m_ICSpatialFilesDataGrid.Columns[Col].Visible))
                        {
                            Col -= -1;
                        }
                    }
                    else
                    {
                        Col += 1;
                    }

                    this.m_ICSpatialFilesDataGrid.CurrentCell = this.m_ICSpatialFilesDataGrid.Rows[Row].Cells[Col];

                    break;
            }
        }

        private void OnGridCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX:
                case SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX:
                case TERTIARY_STRATUM_FILE_NAME_COLUMN_INDEX:
                case SCLASS_FILE_NAME_COLUMN_INDEX:
                case AGE_FILE_NAME_COLUMN_INDEX:

                    this.Session.MainForm.BeginInvoke(new DelegateNoArgs(this.OnNewCellEnterAsync), null);

                    break;
            }
        }

        private void OnGridCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                switch (e.ColumnIndex)
                {
                    case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX:
                    case SECONDARY_STRATUM_BROWSE_COLUMN_INDEX:
                    case TERTIARY_STRATUM_BROWSE_COLUMN_INDEX:
                    case SCLASS_BROWSE_COLUMN_INDEX:
                    case AGE_BROWSE_COLUMN_INDEX:

                        ChooseRasterFile(e.RowIndex, e.ColumnIndex - 1);

                        break;
                }
            }
        }

        private void OnGridKeyDown(object sender, KeyEventArgs e)
        {
            if (this.m_ICSpatialFilesDataGrid.CurrentCell != null)
            {
                switch (this.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex)
                {
                    case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX:
                    case SECONDARY_STRATUM_BROWSE_COLUMN_INDEX:
                    case TERTIARY_STRATUM_BROWSE_COLUMN_INDEX:
                    case SCLASS_BROWSE_COLUMN_INDEX:
                    case AGE_BROWSE_COLUMN_INDEX:

                        if (e.KeyValue == (System.Int32)Keys.Enter)
                        {
                            ChooseRasterFile(this.m_ICSpatialFilesDataGrid.CurrentCell.RowIndex, this.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex - 1);

                            e.Handled = true;
                        }

                        break;
                }
            }
        }

        private void OnGridBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow dgr in this.m_ICSpatialFilesDataGrid.Rows)
            {
                dgr.Cells[PRIMARY_STRATUM_BROWSE_COLUMN_INDEX].Value = BROWSE_BUTTON_TEXT;
                dgr.Cells[SECONDARY_STRATUM_BROWSE_COLUMN_INDEX].Value = BROWSE_BUTTON_TEXT;
                dgr.Cells[TERTIARY_STRATUM_BROWSE_COLUMN_INDEX].Value = BROWSE_BUTTON_TEXT;
                dgr.Cells[SCLASS_BROWSE_COLUMN_INDEX].Value = BROWSE_BUTTON_TEXT;
                dgr.Cells[AGE_BROWSE_COLUMN_INDEX].Value = BROWSE_BUTTON_TEXT;
            }
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            string Primary = null;
            string Secondary = null;
            string Tertiary = null;
            string AmountLabel = null;
            TerminologyUnit AmountUnits = TerminologyUnit.None;

            TerminologyUtilities.GetStratumLabelTerminology(e.DataSheet, ref Primary, ref Secondary, ref Tertiary);
            TerminologyUtilities.GetAmountLabelTerminology(e.DataSheet, ref AmountLabel, ref AmountUnits);

            this.m_ICSpatialFilesDataGrid.Columns[PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX].HeaderText = BuildLowerCaseLabel(Primary);
            this.m_ICSpatialFilesDataGrid.Columns[SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX].HeaderText = BuildLowerCaseLabel(Secondary);
            this.m_ICSpatialFilesDataGrid.Columns[TERTIARY_STRATUM_FILE_NAME_COLUMN_INDEX].HeaderText = BuildLowerCaseLabel(Tertiary);
            this.LabelCalcTtlAmount.Text = string.Format(CultureInfo.InvariantCulture, "Total {0}:", AmountLabel.ToLower(CultureInfo.InvariantCulture));
        }

        private void SetICSpatialFile(int rowIndex, int colIndex, string rasterFullFilename)
        {
            DataSheet ds = this.Scenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            DataGridViewEditMode OldMode = this.m_ICSpatialFilesDataGrid.EditMode;

            this.m_ICSpatialFilesDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.m_ICSpatialFilesDataGrid.CurrentCell = this.m_ICSpatialFilesDataGrid.Rows[rowIndex].Cells[colIndex];
            this.m_ICSpatialFilesDataGrid.Rows[rowIndex].Cells[colIndex].Value = Path.GetFileName(rasterFullFilename);
            this.m_ICSpatialFilesDataGrid.NotifyCurrentCellDirty(true);

            this.m_ICSpatialFilesDataGrid.BeginEdit(false);
            this.m_ICSpatialFilesDataGrid.EndEdit();

            this.m_ICSpatialFilesDataGrid.CurrentCell = this.m_ICSpatialFilesDataGrid.Rows[rowIndex].Cells[colIndex + 1];
            ds.AddExternalInputFile(rasterFullFilename);

            this.m_ICSpatialFilesDataGrid.EditMode = OldMode;
        }

        private void ResetControls()
        {
            this.TextBoxNumColumns.Text = null;
            this.TextBoxNumRows.Text = null;
            this.TextBoxCellArea.Text = null;
            this.TextBoxCellAreaCalc.Text = null;
            this.TextBoxCellAreaCalc.ReadOnly = true;
            this.TextBoxNumCells.Text = null;
            this.TextBoxTotalArea.Text = null;
            this.CheckBoxCellSizeOverride.Enabled = false;
            this.CheckBoxCellSizeOverride.AutoCheck = false;
        }

        private void RefreshNonCalculatedValues()
        {
            DataRow drProp = this.DataFeed.GetDataSheet(Strings.DATASHEET_SPPIC_NAME).GetDataRow();

            if (drProp == null)
            {
                return;
            }

            this.CheckBoxCellSizeOverride.Checked = DataTableUtilities.GetDataBool(drProp[Strings.DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME]);
            this.CheckBoxCellSizeOverride.Enabled = true;
            this.CheckBoxCellSizeOverride.AutoCheck = true;

            int NumRows = DataTableUtilities.GetDataInt(drProp[Strings.DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME]);
            int NumCols = DataTableUtilities.GetDataInt(drProp[Strings.DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME]);
            float CellArea = DataTableUtilities.GetDataSingle(drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME]);
            double cellAreaCalc = DataTableUtilities.GetDataDbl(drProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME]);

            this.TextBoxNumRows.Text = NumRows.ToString(CultureInfo.InvariantCulture);
            this.TextBoxNumColumns.Text = NumCols.ToString(CultureInfo.InvariantCulture);
            this.TextBoxCellArea.Text = CellArea.ToString("N4", CultureInfo.InvariantCulture);
            this.TextBoxCellAreaCalc.Text = cellAreaCalc.ToString("N4", CultureInfo.InvariantCulture);
        }

        private void RefreshCalculatedValues()
        {
            DataRow drProp = this.DataFeed.GetDataSheet(Strings.DATASHEET_SPPIC_NAME).GetDataRow();

            if (drProp == null)
            {
                return;
            }

            //Num Cells
            int NumCells = DataTableUtilities.GetDataInt(drProp[Strings.DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME]);

            this.TextBoxNumCells.Text = NumCells.ToString(CultureInfo.InvariantCulture);

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

            this.LabelRasterCellArea.Text = string.Format(CultureInfo.InvariantCulture, "Cell size ({0}):", srcAreaUnits);
            this.LabelCalcCellArea.Text = string.Format(CultureInfo.InvariantCulture, "Cell size ({0}):", destAreaLbl);
            this.LabelCalcTtlAmount.Text = string.Format(CultureInfo.InvariantCulture, "Total {0} ({1}):", amountlabel, destAreaLbl);

            // Calculate Cell Area in raster's native units
            float cellSize = DataTableUtilities.GetDataSingle(drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME]);
            double cellArea = System.Math.Pow(cellSize, 2);
            this.TextBoxCellArea.Text = cellArea.ToString("N4", CultureInfo.InvariantCulture);

            // Calc Cell Area in terminology units
            double cellAreaTU = 0;
            if (!CheckBoxCellSizeOverride.Checked)
            {
                cellAreaTU = InitialConditionsSpatialDataSheet.CalcCellArea(cellArea, srcSizeUnits, destUnitsVal);
                this.TextBoxCellAreaCalc.Text = cellAreaTU.ToString("N4", CultureInfo.InvariantCulture);
                drProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME] = cellAreaTU;
                TextBoxCellAreaCalc.ReadOnly = true;
            }
            else
            {
                cellAreaTU = DataTableUtilities.GetDataDbl(drProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME]);
                TextBoxCellAreaCalc.ReadOnly = false;
            }

            // Now calculate total area in the specified terminology units
            var ttlArea = cellAreaTU * NumCells;
            this.TextBoxTotalArea.Text = ttlArea.ToString("N4", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Chooses a new raster file
        /// </summary>
        /// <remarks>
        /// Just store the filename. For now, no path required. In the future may want to support absolute path, differentiated by x:\\
        /// </remarks>
        private void ChooseRasterFile(int rowIndex, int colIndex)
        {
            string FileName = RasterUtilities.ChooseRasterFileName("Initial Conditions Raster File", this);

            if (FileName == null)
            {
                return;
            }

            Application.DoEvents();

            using (HourGlass h = new HourGlass())
            {
                try
                {
                    StochasticTimeRaster rast = new StochasticTimeRaster(FileName, RasterDataType.DTInteger);

                    if (colIndex == PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX && rast.Projection == null)
                    {
                        const string msg = "There is no projection associated with this raster file. The model will still run but outputs will also have no projection.";
                        FormsUtilities.InformationMessageBox(msg);
                    }

                    SetICSpatialFile(rowIndex, colIndex, FileName);
                }
                catch (Exception e)
                {
                    FormsUtilities.ErrorMessageBox(e.Message);
                    return;
                }
            }
        }

        private static string BuildLowerCaseLabel(string label)
        {
            StringBuilder sb = new StringBuilder();
            string[] sp = label.Split(' ');

            if (sp.Count() <= 1)
            {
                return label;
            }
            else
            {
                for (int i = 0; i < sp.Count(); i++)
                {
                    if (i == 0)
                    {
                        sb.AppendFormat(CultureInfo.InvariantCulture, "{0} ", sp[i]);
                    }
                    else
                    {
                        sb.AppendFormat(CultureInfo.InvariantCulture, "{0} ", sp[i].ToLower(CultureInfo.InvariantCulture));
                    }
                }

                return sb.ToString().TrimEnd();
            }
        }

        /// <summary>
        /// Handles the CheckChanged event for the CellSize checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void CheckBoxCellSizeOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_InRefresh)
            {
                return;
            }

            DataSheet ds = this.DataFeed.GetDataSheet(Strings.DATASHEET_SPPIC_NAME);
            DataRow dr = ds.GetDataRow();

            if (dr == null)
            {
                // Dont allow Overide checkbox to be changed if no underlying record.
                return;
            }

            ds.SetSingleRowData(Strings.DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME, CheckBoxCellSizeOverride.Checked);
            this.RefreshCalculatedValues();
        }

        private void TextBoxCellAreaCalc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits, single decimal point, and backspace.
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '.')
            {
                if (((TextBox)sender).Text.IndexOf(".", StringComparison.OrdinalIgnoreCase) + 1 > 0)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else if ((int) e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the TextChanged event for the TextBoxCellAreaCalc text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// If we are just refreshing the data then we don't want to do anything during this event.
        /// </remarks>
        private void TextBoxCellAreaCalc_TextChanged(object sender, System.EventArgs e)
        {
            if (!this.m_InRefresh)
            {
                this.m_CellAreaCalcHasChanges = true;
            }
        }

        /// <summary>
        /// Handles the Validated event for the TextBoxCellAreaCalc text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// If we are just refreshing the data, or the text has not really changed, 
        /// then we don't want to do anything during this event.
        /// </remarks>
        private void TextBoxCellAreaCalc_Validated(object sender, System.EventArgs e)
        {
            if (this.m_InRefresh)
            {
                return;
            }

            if (!this.m_CellAreaCalcHasChanges)
            {
                return;
            }

            //Save the CellArea value
            DataSheet ds = this.DataFeed.GetDataSheet(Strings.DATASHEET_SPPIC_NAME);
            DataRow dr = ds.GetDataRow();

            if (dr == null)
            {
                ds.BeginAddRows();
                dr = ds.GetData().NewRow();
                ds.GetData().Rows.Add(dr);
                ds.EndAddRows();
            }

            ds.BeginModifyRows();

            double cellArea = 0;
            if (double.TryParse(this.TextBoxCellAreaCalc.Text, out cellArea))
            {
                dr[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME] = cellArea;
            }
            else
            {
                dr[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME] = DBNull.Value;
            }

            ds.EndModifyRows();
            RefreshCalculatedValues();

            this.m_CellAreaCalcHasChanges = false;
        }
    }
}
