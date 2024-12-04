// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Drawing2D;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.Apex.Forms;

namespace SyncroSim.STSim
{
    internal partial class InitialConditionsSpatialDataFeedViewFineRes
    {
        public InitialConditionsSpatialDataFeedViewFineRes()
        {
            InitializeComponent();
        }

        private bool m_InRefresh;
        private bool m_ColumnsInitialized;
        private bool m_AllowColumnEdits;
        private bool m_CellAreaCalcHasChanges;
        private int m_CellMouseColIndex = -1;
        private int m_CellMouseRowIndex = -1;
        private DataFeedView m_RastersView;
        private DataGridView m_RastersDataGrid;
        private InitialConditionsSpatialRasterDataSheetFineRes m_FilesDataSheet;
        private HourGlass m_HourGlass;
        private delegate void DelegateNoArgs();

        private const int BROWSE_COLUMN_WIDTH = 25;
        private const int ITERATION_COLUMN_INDEX = 0;
        private const int PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 1;
        private const int PRIMARY_STRATUM_BROWSE_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 2;
        private const int SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 3;
        private const int SECONDARY_STRATUM_BROWSE_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 4;
        private const int TERTIARY_STRATUM_FILE_NAME_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 5;
        private const int TERTIARY_STRATUM_BROWSE_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 6;
        private const int SCLASS_FILE_NAME_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 7;
        private const int SCLASS_BROWSE_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 8;
        private const int AGE_FILE_NAME_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 9;
        private const int AGE_BROWSE_COLUMN_INDEX = ITERATION_COLUMN_INDEX + 10;

        protected override void InitializeView()
        {
            base.InitializeView();

            this.m_RastersView = (this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario));
            this.m_RastersDataGrid = ((MultiRowDataFeedView)this.m_RastersView).GridControl;
            this.PanelTopContent.Controls.Add(this.m_RastersView);
            this.PanelBottomContent.BackColor = Color.FromArgb(214, 219, 233);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                if (this.m_HourGlass != null)
                {
                    this.m_HourGlass.Dispose();
                }

                if (this.m_FilesDataSheet != null)
                {
                    this.m_FilesDataSheet.ValidatingRasters -= this.OnValidatingRasters;
                    this.m_FilesDataSheet.RastersValidated -= this.OnRastersValidated;
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

            this.m_RastersView.LoadDataFeed(dataFeed, Strings.DATASHEET_SPICF_NAME);

            if (!this.m_ColumnsInitialized)
            {
                //Add handlers
                this.m_RastersDataGrid.CellFormatting += this.OnGridCellFormatting;
                this.m_RastersDataGrid.CellMouseClick += this.OnGridCellMouseClick;
                this.m_RastersDataGrid.CellPainting += this.OnGridCellPainting;
                this.m_RastersDataGrid.CellMouseEnter += this.OnGridCellMouseEnter;
                this.m_RastersDataGrid.CellMouseLeave += this.OnGridCellMouseLeave;
                this.m_RastersDataGrid.CellBeginEdit += this.OnGridBeginEdit;
                this.m_RastersDataGrid.KeyDown += this.OnGridKeyDown;

                //Add browse button columns
                this.m_RastersDataGrid.Columns.Insert(PRIMARY_STRATUM_BROWSE_COLUMN_INDEX, CreateButtonColumn());
                this.m_RastersDataGrid.Columns.Insert(SECONDARY_STRATUM_BROWSE_COLUMN_INDEX, CreateButtonColumn());
                this.m_RastersDataGrid.Columns.Insert(TERTIARY_STRATUM_BROWSE_COLUMN_INDEX, CreateButtonColumn());
                this.m_RastersDataGrid.Columns.Insert(SCLASS_BROWSE_COLUMN_INDEX, CreateButtonColumn());
                this.m_RastersDataGrid.Columns.Insert(AGE_BROWSE_COLUMN_INDEX, CreateButtonColumn());

                this.m_ColumnsInitialized = true;
            }

            this.MonitorDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged, true);

            this.m_FilesDataSheet = (InitialConditionsSpatialRasterDataSheetFineRes)this.DataFeed.GetDataSheet(Strings.DATASHEET_SPICF_NAME);
            this.m_FilesDataSheet.ValidatingRasters += this.OnValidatingRasters;
            this.m_FilesDataSheet.RastersValidated += this.OnRastersValidated;
        }

        public override void RefreshControls()
        {
            this.m_InRefresh = true;
            this.ResetControls();
            this.RefreshNonCalculatedValues();
            this.m_InRefresh = false;
        }

        public override void EnableView(bool enable)
        {
            this.m_RastersView.EnableView(enable);

            this.TableAttributes.Enabled = enable;
            this.TableCalculated.Enabled = enable;
        }

        private static DataGridViewTextBoxColumn CreateButtonColumn()
        {
            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();

            c.Width = BROWSE_COLUMN_WIDTH;
            c.MinimumWidth = BROWSE_COLUMN_WIDTH;

            return c;
        }

        private void OnValidatingRasters(object sender, EventArgs e)
        {
            this.Session.Application.SetStatusMessage("Validating rasters...");

            Application.DoEvents();
            this.m_HourGlass = new HourGlass();
        }

        private void OnRastersValidated(object sender, EventArgs e)
        {
            this.Session.Application.SetStatusMessage(string.Empty);

            this.m_HourGlass.Dispose();
            this.m_HourGlass = null;
        }

        private void OnGridBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewColumn IterCol = this.m_RastersDataGrid.Columns["Iteration"];

            //Always allow editing of the Iteration column
            if (e.ColumnIndex == IterCol.Index)
            {
                return;
            }

            if (!this.m_AllowColumnEdits)
            {
                e.Cancel = true;
            }
        }

        private void OnGridCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (!this.ShouldEnableView())
            {
                return;
            }

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

        private void OnGridCellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.m_CellMouseColIndex = -1;
            this.m_CellMouseRowIndex = -1;

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.m_CellMouseColIndex = e.ColumnIndex;
            this.m_CellMouseRowIndex = e.RowIndex;

            this.m_RastersDataGrid.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        private void OnGridCellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.m_CellMouseColIndex = -1;
            this.m_CellMouseRowIndex = -1;

            this.m_RastersDataGrid.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        private void OnGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewCell Cell = this.m_RastersDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (!Cell.OwningRow.Selected)
            {
                e.CellStyle.ForeColor = Color.Gray;
            }
        }

        private void OnGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            switch (e.ColumnIndex)
            {
                case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX:
                case SECONDARY_STRATUM_BROWSE_COLUMN_INDEX:
                case TERTIARY_STRATUM_BROWSE_COLUMN_INDEX:
                case SCLASS_BROWSE_COLUMN_INDEX:
                case AGE_BROWSE_COLUMN_INDEX:

                    Image img = Properties.Resources.Open16x16;
                    int X = e.CellBounds.Left + e.CellBounds.Width / 2 - 8;
                    int Y = e.CellBounds.Top + e.CellBounds.Height / 2 - 8;
                    DataGridViewCell Cell = this.m_RastersDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    bool MouseInCell = (
                        e.RowIndex == this.m_CellMouseRowIndex &&
                        e.ColumnIndex == this.m_CellMouseColIndex);

                    bool IsFocusedCell = (
                        this.m_RastersDataGrid.ContainsFocus &&
                        Cell == this.m_RastersDataGrid.CurrentCell);

                    e.PaintBackground(e.ClipBounds, Cell.OwningRow.Selected);

                    if (this.ShouldEnableView())
                    {
                        e.Graphics.DrawImage(img, X, Y);
                    }
                    else
                    {
                        ControlPaint.DrawImageDisabled(e.Graphics, img, X, Y, this.BackColor);
                    }

                    if (IsFocusedCell || MouseInCell)
                    {
                        Rectangle rc = new Rectangle(
                            e.CellBounds.Left, e.CellBounds.Top,
                            e.CellBounds.Width - 2, e.CellBounds.Height - 2);

                        Color clr = Color.FromArgb(132, 172, 221);
                        SmoothingMode OldMode = e.Graphics.SmoothingMode;
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        using (Pen p = new Pen(clr, 1.5F))
                        {
                            e.Graphics.DrawRectangle(p, rc);
                        }

                        e.Graphics.SmoothingMode = OldMode;
                    }

                    e.Handled = true;
                    break;
            }
        }

        private void OnGridKeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewCell CurCell = this.m_RastersDataGrid.CurrentCell;

            if (CurCell == null)
            {
                return;
            }

            if (CurCell.RowIndex < 0 || CurCell.ColumnIndex < 0)
            {
                return;
            }

            if (!this.ShouldEnableView())
            {
                return;
            }

            switch (CurCell.ColumnIndex)
            {
                case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX:
                case SECONDARY_STRATUM_BROWSE_COLUMN_INDEX:
                case TERTIARY_STRATUM_BROWSE_COLUMN_INDEX:
                case SCLASS_BROWSE_COLUMN_INDEX:
                case AGE_BROWSE_COLUMN_INDEX:

                    if (e.KeyValue == (Int32)Keys.Enter)
                    {
                        ChooseRasterFile(
                            this.m_RastersDataGrid.CurrentCell.RowIndex,
                            this.m_RastersDataGrid.CurrentCell.ColumnIndex - 1);

                        e.Handled = true;
                    }

                    break;
            }
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            string Primary = null;
            string Secondary = null;
            string Tertiary = null;
            string AmountLabel = null;
            TerminologyUnit AmountUnits = TerminologyUnit.None;

            TerminologyUtilities.GetStratumLabelTerminology(this.Project, ref Primary, ref Secondary, ref Tertiary);
            TerminologyUtilities.GetAmountLabelTerminology(this.Project, ref AmountLabel, ref AmountUnits);

            this.m_RastersDataGrid.Columns[PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX].HeaderText = BuildLowerCaseLabel(Primary);
            this.m_RastersDataGrid.Columns[SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX].HeaderText = BuildLowerCaseLabel(Secondary);
            this.m_RastersDataGrid.Columns[TERTIARY_STRATUM_FILE_NAME_COLUMN_INDEX].HeaderText = BuildLowerCaseLabel(Tertiary);

            this.RefreshNonCalculatedValues();
        }

        private void SetICSpatialFile(int rowIndex, int colIndex, string rasterFullFilename)
        {
            try
            {
                this.m_AllowColumnEdits = true;

                using (HourGlass h = new HourGlass())
                {
                    DataSheet ds = this.Scenario.GetDataSheet(Strings.DATASHEET_SPICF_NAME);
                    DataGridViewEditMode OldMode = this.m_RastersDataGrid.EditMode;

                    this.m_RastersDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
                    this.m_RastersDataGrid.CurrentCell = this.m_RastersDataGrid.Rows[rowIndex].Cells[colIndex];

                    this.m_RastersDataGrid.BeginEdit(false);
                    this.m_RastersDataGrid.EditingControl.Text = Path.GetFileName(rasterFullFilename);
                    this.m_RastersDataGrid.EndEdit();

                    this.m_RastersDataGrid.CurrentCell = this.m_RastersDataGrid.Rows[rowIndex].Cells[colIndex + 1];
                    this.m_RastersDataGrid.EditMode = OldMode;

                    ds.AddExternalInputFile(rasterFullFilename);
                }
            }
            finally
            {
                this.m_AllowColumnEdits = false;
            }
        }

        private void ResetControls()
        {
            this.TextBoxNumColumns.Text = null;
            this.TextBoxNumRows.Text = null;
            this.TextBoxCellSize.Text = null;
            this.TextBoxCellAreaCalc.Text = null;
            this.TextBoxCellAreaCalc.ReadOnly = true;
            this.TextBoxNumCells.Text = null;
            this.TextBoxTotalArea.Text = null;
            this.CheckBoxCellSizeOverride.Enabled = false;
            this.CheckBoxCellSizeOverride.AutoCheck = false;
        }

        private void RefreshNonCalculatedValues()
        {
            DataRow drProp = this.DataFeed.GetDataSheet(Strings.DATASHEET_SPPICF_NAME).GetDataRow();

            if (drProp == null)
            {
                return;
            }

            this.CheckBoxCellSizeOverride.Checked = DataTableUtilities.GetDataBool(drProp[Strings.DATASHEET_SPPICF_CELL_AREA_OVERRIDE_COLUMN_NAME]);
            this.CheckBoxCellSizeOverride.Enabled = true;
            this.CheckBoxCellSizeOverride.AutoCheck = true;

            int NumRows = DataTableUtilities.GetDataInt(drProp[Strings.DATASHEET_SPPICF_NUM_ROWS_COLUMN_NAME]);
            int NumCols = DataTableUtilities.GetDataInt(drProp[Strings.DATASHEET_SPPICF_NUM_COLUMNS_COLUMN_NAME]);
            float CellSize = DataTableUtilities.GetDataSingle(drProp[Strings.DATASHEET_SPPICF_CELL_SIZE_COLUMN_NAME]);
            double cellAreaCalc = DataTableUtilities.GetDataDbl(drProp[Strings.DATASHEET_SPPICF_CELL_AREA_COLUMN_NAME]);
            int NumCells = DataTableUtilities.GetDataInt(drProp[Strings.DATASHEET_SPPICF_NUM_CELLS_COLUMN_NAME]);

            string srcSizeUnits = DataTableUtilities.GetDataStr(drProp[Strings.DATASHEET_SPPICF_CELL_SIZE_UNITS_COLUMN_NAME]);
            string amountlabel = null;
            TerminologyUnit destUnitsVal = 0;

            TerminologyUtilities.GetAmountLabelTerminology(this.Project, ref amountlabel, ref destUnitsVal);

            string destAreaLbl = TerminologyUtilities.TerminologyUnitToString(destUnitsVal);

            srcSizeUnits = srcSizeUnits.ToLower(CultureInfo.InvariantCulture);
            amountlabel = amountlabel.ToLower(CultureInfo.InvariantCulture);
            destAreaLbl = destAreaLbl.ToLower(CultureInfo.InvariantCulture);

            this.TextBoxNumRows.Text = NumRows.ToString(CultureInfo.InvariantCulture);
            this.TextBoxNumColumns.Text = NumCols.ToString(CultureInfo.InvariantCulture);
            this.TextBoxCellSize.Text = CellSize.ToString("N4", CultureInfo.InvariantCulture);
            this.TextBoxCellAreaCalc.Text = cellAreaCalc.ToString("N4", CultureInfo.InvariantCulture);
            this.TextBoxNumCells.Text = NumCells.ToString(CultureInfo.InvariantCulture);
            this.LabelRasterCellArea.Text = string.Format(CultureInfo.InvariantCulture, "Cell size ({0}):", srcSizeUnits);
            this.LabelCalcCellArea.Text = string.Format(CultureInfo.InvariantCulture, "Cell area ({0}):", destAreaLbl);
            this.LabelCalcTtlAmount.Text = string.Format(CultureInfo.InvariantCulture, "Total {0} ({1}):", amountlabel, destAreaLbl);

            var ttlArea = cellAreaCalc * NumCells;
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
                    SyncroSimRaster rast = new SyncroSimRaster(FileName, RasterDataType.DTInteger);

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

            DataSheet ds = this.DataFeed.GetDataSheet(Strings.DATASHEET_SPPICF_NAME);
            DataRow dr = ds.GetDataRow();

            if (dr == null)
            {
                // Dont allow Overide checkbox to be changed if no underlying record.
                return;
            }

            ds.SetSingleRowData(Strings.DATASHEET_SPPICF_CELL_AREA_OVERRIDE_COLUMN_NAME, CheckBoxCellSizeOverride.Checked);
            this.RefreshNonCalculatedValues();
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
        private void TextBoxCellAreaCalc_TextChanged(object sender, EventArgs e)
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
        private void TextBoxCellAreaCalc_Validated(object sender, EventArgs e)
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
            DataSheet ds = this.DataFeed.GetDataSheet(Strings.DATASHEET_SPPICF_NAME);
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
                dr[Strings.DATASHEET_SPPICF_CELL_AREA_COLUMN_NAME] = cellArea;
            }
            else
            {
                dr[Strings.DATASHEET_SPPICF_CELL_AREA_COLUMN_NAME] = DBNull.Value;
            }

            ds.EndModifyRows();
            RefreshNonCalculatedValues();

            this.m_CellAreaCalcHasChanges = false;
        }
    }
}
