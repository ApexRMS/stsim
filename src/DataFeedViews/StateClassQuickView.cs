// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
    internal partial class StateClassQuickView
    {
        public StateClassQuickView()
        {
            InitializeComponent();
        }

        private int? m_StratumId;
        private DataSheet m_StratumDataSheet;
        private List<int> m_StateClasses;
        private DataFeed m_DataFeed;
        private DataSheet m_DTDataSheet;
        private MultiRowDataFeedView m_DTView;
        private BaseDataGridView m_DTGrid;
        private bool m_DTIterationVisible;
        private bool m_DTTimestepVisible;
        private bool m_DTStratumVisible;
        private bool m_DTToStratumVisible;
        private bool m_DTToClassVisible;
        private bool m_DTAgeMinVisible;
        private bool m_DTAgeMaxVisible;
        private MultiRowDataFeedView m_PTView;
        private BaseDataGridView m_PTGrid;
        private bool m_PTIterationVisible;
        private bool m_PTTimestepVisible;
        private bool m_PTStratumVisible;
        private bool m_PTToStratumVisible;
        private bool m_PTToClassVisible;
        private bool m_PTSecondaryStratumVisible;
        private bool m_PTTertiaryStratumVisible;
        private bool m_PTProportionVisible;
        private bool m_PTAgeMinVisible;
        private bool m_PTAgeMaxVisible;
        private bool m_PTAgeRelativeVisible;
        private bool m_PTAgeResetVisible;
        private bool m_PTTstMinVisible;
        private bool m_PTTstMaxVisible;
        private bool m_PTTstRelativeVisible;
        private bool m_ShowTransitionsFrom = true;
        private bool m_ShowTransitionsTo;
        private bool m_AddingDefaultValues;
        private delegate void DelegateNoArgs();
        private string m_Tag;

        public void LoadStateClasses(int? stratumId, List<int> stateClasses, DataFeed dataFeed, string tag)
        {
            this.m_StratumId = stratumId;
            this.m_StratumDataSheet = this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
            this.m_StateClasses = stateClasses;
            this.m_DataFeed = dataFeed;
            this.m_Tag = tag;

            WinFormSession sess = (WinFormSession)this.Project.Library.Session;

            this.m_DTView = (MultiRowDataFeedView)sess.CreateMultiRowDataFeedView(dataFeed.Scenario, dataFeed.Scenario);
            this.m_DTView.LoadDataFeed(this.m_DataFeed, Strings.DATASHEET_DT_NAME);
            this.m_DTDataSheet = this.m_DataFeed.GetDataSheet(Strings.DATASHEET_DT_NAME);
            this.m_DTGrid = this.m_DTView.GridControl;
            this.PanelDeterministic.Controls.Add(this.m_DTView);

            this.m_PTView = (MultiRowDataFeedView)sess.CreateMultiRowDataFeedView(dataFeed.Scenario, dataFeed.Scenario);
            this.m_PTView.LoadDataFeed(this.m_DataFeed, Strings.DATASHEET_PT_NAME);
            this.m_PTGrid = this.m_PTView.GridControl;
            this.PanelProbabilistic.Controls.Add(this.m_PTView);

            this.m_DTGrid.PaintGridBorders = false;
            this.m_DTView.ManageOptionalColumns = false;

            this.m_PTGrid.PaintGridBorders = false;
            this.m_PTView.ManageOptionalColumns = false;

            this.FilterDeterministicTransitions();
            this.FilterProbabilisticTransitions();

            this.m_DTGrid.CellValueChanged += OnDeterministicCellValueChanged;
            this.m_DTGrid.CellBeginEdit += OnDeterministicCellBeginEdit;
            this.m_DTGrid.CellEndEdit += OnDeterministicCellEndEdit;
            this.m_PTGrid.CellValueChanged += OnProbabilisticCellValueChanged;
            this.m_PTGrid.CellBeginEdit += OnProbabilisticCellBeginEdit;
            this.m_PTGrid.CellEndEdit += OnProbabilisticCellEndEdit;
            this.m_PTGrid.DefaultValuesNeeded += OnProbabilisticOnDefaultValuesNeeded;
            this.m_PTGrid.BeforeCmdKeyPress += OnBeforePTGridCmdKey;
            this.m_StratumDataSheet.RowsDeleted += OnStratumDeleted;

            this.ConfigureContextMenus();
            this.InitializeColumnVisiblity();
            this.UpdateDTColumnVisibility();
            this.UpdatePTColumnVisibility();
            this.ConfigureColumnsReadOnly();

            //Mysteriously, if we set AllowUserToAddRows in this function it crashes and I'm not sure why.  However, 
            //The problem goes away if we set it asynchronously so that's what we are doing here.

            this.Session.MainForm.BeginInvoke(new DelegateNoArgs(GridConfigAsyncTarget), null);
        }

        /// <summary>
        /// Async Target for grid configuration
        /// </summary>
        /// <param name="arg"></param>
        /// <remarks></remarks>
        private void GridConfigAsyncTarget()
        {
            this.m_DTGrid.AllowUserToAddRows = false;

            if (this.m_DTToClassVisible)
            {
                DataGridViewColumn ToClassCol = this.m_DTGrid.Columns[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME];
                this.m_DTGrid.CurrentCell = this.m_DTGrid.Rows[0].Cells[ToClassCol.Index];
            }
            else
            {
                DataGridViewColumn FromClassCol = this.m_DTGrid.Columns[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                this.m_DTGrid.CurrentCell = this.m_DTGrid.Rows[0].Cells[FromClassCol.Index];
            }
        }

        /// <summary>
        /// Overrides Dispose
        /// </summary>
        /// <param name="disposing"></param>
        /// <remarks></remarks>
        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                this.m_DTGrid.CellValueChanged -= OnDeterministicCellValueChanged;
                this.m_DTGrid.CellBeginEdit -= OnDeterministicCellBeginEdit;
                this.m_DTGrid.CellEndEdit -= OnDeterministicCellEndEdit;
                this.m_PTGrid.CellValueChanged -= OnProbabilisticCellValueChanged;
                this.m_PTGrid.CellBeginEdit -= OnProbabilisticCellBeginEdit;
                this.m_PTGrid.CellEndEdit -= OnProbabilisticCellEndEdit;
                this.m_PTGrid.DefaultValuesNeeded -= OnProbabilisticOnDefaultValuesNeeded;
                this.m_PTGrid.BeforeCmdKeyPress -= OnBeforePTGridCmdKey;
                this.m_StratumDataSheet.RowsDeleted -= OnStratumDeleted;

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// OnStratumDeleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// If the stratum data feed is deleting our stratum then we need to close.  Note that follwing the call
        /// to CloseHostedView, this controls containing form will be gone...
        /// </remarks>
        private void OnStratumDeleted(object sender, SyncroSim.Core.DataSheetRowEventArgs e)
        {
            bool found = false;

            foreach (DataRow dr in this.m_StratumDataSheet.ValidationTable.DataSource.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (Convert.ToInt32(dr[this.m_StratumDataSheet.ValueMember], CultureInfo.InvariantCulture) == this.m_StratumId)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                WinFormSession sess = (WinFormSession)this.Project.Session;
                sess.Application.CloseView(this.m_Tag);

                return;
            }
        }

        /// <summary>
        /// Configures the grids' context menus
        /// </summary>
        /// <remarks></remarks>
        private void ConfigureContextMenus()
        {
            //Terminology
            string psl = null;
            string ssl = null;
            string tsl = null;
            DataSheet dsterm = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref psl, ref ssl, ref tsl);

            //Deterministic
            this.m_DTGrid.ContextMenuStrip = this.ContextMenuStripDeterministic;
            this.MenuItemTransitionsToDeterministic.Checked = this.m_ShowTransitionsTo;
            this.MenuItemTransitionsFromDeterministic.Checked = this.m_ShowTransitionsFrom;
            this.MenuItemStratumDeterministic.Text = psl;
            this.MenuItemToStratumDeterministic.Text = "To " + psl;

            //Probabilistic
            for (int i = this.m_PTView.Commands.Count - 1; i >= 0; i--)
            {
                Command c = this.m_PTView.Commands[i];

                if (c.Name == "ssim_delete_all" || c.Name == "ssim_import" || c.Name == "ssim_export" || c.Name == "ssim_export_all")
                {
                    this.m_PTView.Commands.RemoveAt(i);
                }
            }

            this.m_PTView.Commands.Add(new Command("Transitions To", OnExecuteProbabilisticTranstionsToCommand, OnUpdateProbabilisticTranstionsToCommand));
            this.m_PTView.Commands.Add(new Command("Transitions From", OnExecuteProbabilisticTranstionsFromCommand, OnUpdateProbabilisticTranstionsFromCommand));
            this.m_PTView.Commands.Add(Command.CreateSeparatorCommand());
            this.m_PTView.Commands.Add(new Command("Iteration", OnExecuteProbabilisticIterationCommand, OnUpdateProbabilisticIterationCommand));
            this.m_PTView.Commands.Add(new Command("Timestep", OnExecuteProbabilisticTimestepCommand, OnUpdateProbabilisticTimestepCommand));
            this.m_PTView.Commands.Add(new Command(psl, OnExecuteProbabilisticStratumCommand, OnUpdateProbabilisticStratumCommand));
            this.m_PTView.Commands.Add(new Command("To " + psl, OnExecuteProbabilisticToStratumCommand, OnUpdateProbabilisticToStratumCommand));
            this.m_PTView.Commands.Add(new Command("To Class", OnExecuteProbabilisticToClassCommand, OnUpdateProbabilisticToClassCommand));
            this.m_PTView.Commands.Add(new Command(ssl, OnExecuteProbabilisticSSCommand, OnUpdateProbabilisticSSCommand));
            this.m_PTView.Commands.Add(new Command(tsl, OnExecuteProbabilisticTSCommand, OnUpdateProbabilisticTSCommand));
            this.m_PTView.Commands.Add(new Command("Proportion", OnExecuteProbabilisticProportionCommand, OnUpdateProbabilisticProportionCommand));
            this.m_PTView.Commands.Add(new Command("Age Min", OnExecuteProbabilisticAgeMinCommand, OnUpdateProbabilisticAgeMinCommand));
            this.m_PTView.Commands.Add(new Command("Age Max", OnExecuteProbabilisticAgeMaxCommand, OnUpdateProbabilisticAgeMaxCommand));
            this.m_PTView.Commands.Add(new Command("Age Shift", OnExecuteProbabilisticAgeRelativeCommand, OnUpdateProbabilisticAgeRelativeCommand));
            this.m_PTView.Commands.Add(new Command("Age Reset", OnExecuteProbabilisticAgeResetCommand, OnUpdateProbabilisticAgeResetCommand));
            this.m_PTView.Commands.Add(new Command("TST Min", OnExecuteProbabilisticTstMinCommand, OnUpdateProbabilisticTstMinCommand));
            this.m_PTView.Commands.Add(new Command("TST Max", OnExecuteProbabilisticTstMaxCommand, OnUpdateProbabilisticTstMaxCommand));
            this.m_PTView.Commands.Add(new Command("TST Shift", OnExecuteProbabilisticTstRelativeCommand, OnUpdateProbabilisticTstRelativeCommand));

            this.m_PTView.RefreshContextMenuStrip();

            //Remove Optional menu items
            for (int i = this.m_PTGrid.ContextMenuStrip.Items.Count - 1; i >= 0; i--)
            {
                ToolStripItem item = this.m_PTGrid.ContextMenuStrip.Items[i];

                if (item.Name == "ssim_optional_column_separator" || item.Name == "ssim_optional_column_item")
                {
                    this.m_PTGrid.ContextMenuStrip.Items.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Creates the filter string for the grids
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CreateGridFilterString()
        {
            string SelectedClassesFilter = CreateIntegerFilterSpec(this.m_StateClasses);

            string FromFormatString = null;
            string ToFormatString = null;

            if (this.m_StratumId.HasValue)
            {
                FromFormatString = "StratumIDSource={0} AND StateClassIDSource IN ({1})";
                ToFormatString = "(StratumIDDest={0} AND StateClassIDDest IN ({1})) OR (StratumIDSource={0} AND StratumIDDest IS NULL AND StateClassIDDest IN ({1}))";
            }
            else
            {
                FromFormatString = "StratumIDSource IS NULL AND StateClassIDSource IN ({1})";
                ToFormatString = "(StratumIDSource IS NULL AND StateClassIDDest IN ({1}))";
            }

            if (this.m_ShowTransitionsFrom)
            {
                return string.Format(CultureInfo.InvariantCulture, FromFormatString, this.m_StratumId, SelectedClassesFilter);
            }
            else
            {
                Debug.Assert(this.m_ShowTransitionsTo);
                return string.Format(CultureInfo.InvariantCulture, ToFormatString, this.m_StratumId, SelectedClassesFilter);
            }
        }

        /// <summary>
        /// Filters the deterministic transitions
        /// </summary>
        /// <remarks></remarks>
        private void FilterDeterministicTransitions()
        {
            string filter = this.CreateGridFilterString();
            ((BindingSource)this.m_DTGrid.DataSource).Filter = filter;
        }

        /// <summary>
        /// Filters the probabilistic transitions
        /// </summary>
        /// <remarks></remarks>
        private void FilterProbabilisticTransitions()
        {
            string filter = this.CreateGridFilterString();
            ((BindingSource)this.m_PTGrid.DataSource).Filter = filter;
        }

        /// <summary>
        /// Handles the CellValueChanged event from the deterministic transitions data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnDeterministicCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.m_DTGrid.Columns[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME].Index)
            {
                this.SetNewDestinationStateClassCellValue(Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME, Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, this.m_DTGrid, e.RowIndex);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event for the deterministic transitions grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnDeterministicCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == this.m_DTGrid.Columns[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME].Index)
            {
                this.FilterStateClassCombo(Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, this.m_DTGrid, e.RowIndex, false);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event for the deterministic transitions grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnDeterministicCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.m_DTGrid.Columns[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME].Index)
            {
                ResetComboBoxRowFilter(e.RowIndex, Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, this.m_DTGrid);
            }
            else if (e.ColumnIndex == this.m_DTGrid.Columns[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME].Index)
            {
                ResetComboBoxRowFilter(e.RowIndex, Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, this.m_DTGrid);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event from the probabilistic transitions grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnProbabilisticCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.m_AddingDefaultValues)
            {
                return;
            }

            if (e.ColumnIndex == this.m_PTGrid.Columns[Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME].Index)
            {
                this.SetNewDestinationStateClassCellValue(Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME, Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, this.m_PTGrid, e.RowIndex);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit for the probabilistic transitions grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnProbabilisticCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == this.m_PTGrid.Columns[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME].Index)
            {
                this.FilterStateClassCombo(Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME, this.m_PTGrid, e.RowIndex, this.m_ShowTransitionsFrom);
            }
            else if (e.ColumnIndex == this.m_PTGrid.Columns[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME].Index)
            {
                this.FilterStateClassCombo(Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, this.m_PTGrid, e.RowIndex, false);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event for the probabilistic transitions grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnProbabilisticCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.m_PTGrid.Columns[Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME].Index)
            {
                ResetComboBoxRowFilter(e.RowIndex, Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, this.m_PTGrid);
            }
            else if (e.ColumnIndex == this.m_PTGrid.Columns[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME].Index)
            {
                ResetComboBoxRowFilter(e.RowIndex, Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME, this.m_PTGrid);
            }
            else if (e.ColumnIndex == this.m_PTGrid.Columns[Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME].Index)
            {
                ResetComboBoxRowFilter(e.RowIndex, Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, this.m_PTGrid);
            }
            else if (e.ColumnIndex == this.m_PTGrid.Columns[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME].Index)
            {
                ResetComboBoxRowFilter(e.RowIndex, Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, this.m_PTGrid);
            }
        }

        /// <summary>
        /// Handles the default values changed event for the probabilistic transitions grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnProbabilisticOnDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewRow row = e.Row;

            this.m_AddingDefaultValues = true;

            row.Cells[Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME].Value = DataTableUtilities.GetNullableDatabaseValue(this.m_StratumId);
            row.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME].Value = this.m_StateClasses[0];
            row.Cells[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME].Value = this.m_StateClasses[0];

            this.m_AddingDefaultValues = false;
        }

        /// <summary>
        /// Handler for base grid custom OnBeforeCmdKey event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// When the user clicks on the 'new row' they get a bunch of default values which we supply
        /// in response to the DefaultValuesNeeded event.  This works well unless:
        /// 
        /// (a.) there is only one row
        /// (b.) that row is the 'new row'
        /// (c.) the user hits the ESC key.  
        /// 
        /// Doing this will clear the default values, but the 'new' row will still be the current row and
        /// the DefaultValuesNeeded event will not be raised again.  This crashes eventually because we expect 
        /// those default values to be there. 
        /// 
        /// This seems like a bug in the DataGridView to me - why does hitting ESC not completely cancel the new row
        /// editing session instead of just removing the defaults?
        /// 
        /// To make the grid think that it is starting the editing session over, we detect this condition and set 
        /// the focus to the parent control.  This makes things work and the user will probably not notice that 
        /// the current cell gets reset...
        /// </remarks>
        private void OnBeforePTGridCmdKey(object sender, BaseGridCmdKeyInfoEventArgs e)
        {
            if (this.m_PTGrid.CurrentRow == null)
            {
                return;
            }

            if (e.KeyData == Keys.Escape)
            {
                if (this.m_PTGrid.CurrentRow.IsNewRow)
                {
                    if (this.m_PTGrid.Rows.Count == 1)
                    {
                        this.PanelProbabilistic.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// Resets the row filter for the combo box cell at the specified row and column
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <remarks></remarks>
        private static void ResetComboBoxRowFilter(int rowIndex, string columnName, DataGridView grid)
        {
            DataGridViewRow dgv = grid.Rows[rowIndex];
            DataGridViewComboBoxCell DestStratumCell = (DataGridViewComboBoxCell)dgv.Cells[columnName];
            DataGridViewComboBoxColumn DestStratumColumn = (DataGridViewComboBoxColumn)grid.Columns[columnName];

            DestStratumCell.DataSource = DestStratumColumn.DataSource;
            DestStratumCell.ValueMember = DestStratumColumn.ValueMember;
            DestStratumCell.DisplayMember = DestStratumColumn.DisplayMember;
        }

        /// <summary>
        /// Gets the combo box cell for the specified column
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static DataGridViewComboBoxCell GetComboCell(string columnName, DataGridView grid, int rowIndex)
        {
            DataGridViewRow dgv = grid.Rows[rowIndex];
            return (DataGridViewComboBoxCell)dgv.Cells[columnName];
        }

        /// <summary>
        /// Gets the correct stratum Id from the specified set of source and destination columns
        /// </summary>
        /// <param name="sourceStratumColumnName"></param>
        /// <param name="destStratumColumnName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int? GetDestStratumIdFromGridValues(string sourceStratumColumnName, string destStratumColumnName, DataGridView grid, int rowIndex)
        {
            DataGridViewRow dgv = grid.Rows[rowIndex];
            DataGridViewComboBoxCell SourceStratumCell = (DataGridViewComboBoxCell)dgv.Cells[sourceStratumColumnName];
            DataGridViewComboBoxCell DestStratumCell = (DataGridViewComboBoxCell)dgv.Cells[destStratumColumnName];

            Debug.Assert(DestStratumCell.Value != null);
            Debug.Assert(SourceStratumCell.Value != null);

            if (DestStratumCell.Value != DBNull.Value)
            {
                return Convert.ToInt32(DestStratumCell.Value, CultureInfo.InvariantCulture);
            }

            if (SourceStratumCell.Value != DBNull.Value)
            {
                Debug.Assert(this.m_StratumId.HasValue);
                Debug.Assert(Convert.ToInt32(SourceStratumCell.Value, CultureInfo.InvariantCulture) == this.m_StratumId.Value);

                return this.m_StratumId;
            }
            else
            {
                Debug.Assert(!this.m_StratumId.HasValue);
                return null;
            }
        }

        /// <summary>
        /// Creates a comma separated filter specification for the specified list of integers
        /// </summary>
        /// <param name="stateClassIDList"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string CreateIntegerFilterSpec(List<int> stateClassIDList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int i in stateClassIDList)
            {
                sb.Append(i.ToString(CultureInfo.InvariantCulture));
                sb.Append(",");
            }

            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// Creates a filter for all state classes in the specified stratum
        /// </summary>
        /// <param name="sourceStratumId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CreateStateClassIdSourceFilter(int? sourceStratumId)
        {
            List<int> lst = new List<int>();
            string query = null;

            if (sourceStratumId.HasValue)
            {
                query = string.Format(CultureInfo.InvariantCulture, "StratumIDSource={0} OR (StratumIDSource IS NULL)", sourceStratumId.Value);
            }
            else
            {
                query = "StratumIDSource IS NULL";
            }

            DataTable dt = this.m_DTDataSheet.GetData();
            DataRow[] rows = dt.Select(query, null, DataViewRowState.CurrentRows);

            foreach (DataRow dr in rows)
            {
                int id = Convert.ToInt32(dr["StateClassIDSource"], CultureInfo.InvariantCulture);

                if (!lst.Contains(id))
                {
                    lst.Add(id);
                }
            }

            if (lst.Count == 0)
            {
                return "StateClassID=-1";
            }
            else
            {
                string filter = CreateIntegerFilterSpec(lst);
                return string.Format(CultureInfo.InvariantCulture, "StateClassID IN ({0})", filter);
            }
        }

        /// <summary>
        /// Determines whether the specified view contains the specified state class Id
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="stateClassId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static bool ViewContainsStateClass(DataView dv, int stateClassId)
        {
            foreach (DataRowView drv in dv)
            {
                if (Convert.ToInt32(drv.Row["StateClassID"], CultureInfo.InvariantCulture) == stateClassId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates a state class dataview filtered by the specified stratum Id
        /// </summary>
        /// <param name="stratumID"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private DataView CreateNewFilteredStateClassView(int? stratumId)
        {
            string filter = this.CreateStateClassIdSourceFilter(stratumId);
            DataSheet ds = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);

            return new DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows);
        }

        /// <summary>
        /// Sets a new destination state class value
        /// </summary>
        /// <param name="sourceStratumColumnName"></param>
        /// <param name="sourceStateClassColumnName"></param>
        /// <param name="destStratumColumnName"></param>
        /// <param name="destStateClassColumnName"></param>
        /// <param name="grid"></param>
        /// <param name="rowIndex"></param>
        /// <remarks></remarks>
        private void SetNewDestinationStateClassCellValue(string sourceStratumColumnName, string sourceStateClassColumnName, string destStratumColumnName, string destStateClassColumnName, DataGridView grid, int rowIndex)
        {
            DataGridViewComboBoxCell SourceStateClassCell = GetComboCell(sourceStateClassColumnName, grid, rowIndex);
            DataGridViewComboBoxCell DestStateClassCell = GetComboCell(destStateClassColumnName, grid, rowIndex);
            int? DestStratumId = this.GetDestStratumIdFromGridValues(sourceStratumColumnName, destStratumColumnName, grid, rowIndex);
            int? DestStateClassId = null;
            int CurrentDestStateClassId = Convert.ToInt32(SourceStateClassCell.Value, CultureInfo.InvariantCulture);
            DataView dv = this.CreateNewFilteredStateClassView(DestStratumId);

            if (DestStateClassCell.Value == DBNull.Value)
            {
                DestStateClassId = Convert.ToInt32(SourceStateClassCell.Value, CultureInfo.InvariantCulture);
            }
            else
            {
                DestStateClassId = Convert.ToInt32(DestStateClassCell.Value, CultureInfo.InvariantCulture);
            }

            Debug.Assert(CurrentDestStateClassId > 0);

            //If the new stratum has the current destination state class Id then we can use
            //that one.  Otherwise use the first one found.

            if (ViewContainsStateClass(dv, CurrentDestStateClassId))
            {
                DestStateClassCell.Value = CurrentDestStateClassId;
            }
            else
            {
                if (dv.Count > 0)
                {
                    DataRowView drvzero = dv[0];
                    DestStateClassCell.Value = Convert.ToInt32(drvzero.Row["StateClassID"], CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Filters the state class combo for the specified column
        /// </summary>
        /// <param name="sourceStratumColumnName"></param>
        /// <param name="destStratumColumnName"></param>
        /// <param name="stateClassColumnName"></param>
        /// <param name="grid"></param>
        /// <param name="rowIndex"></param>
        /// <param name="selectedClassesOnly"></param>
        /// <remarks>
        /// When the destination state class editing control is shown we want to filter the state classes for the destination stratum
        /// </remarks>
        private void FilterStateClassCombo(string sourceStratumColumnName, string destStratumColumnName, string stateClassColumnName, DataGridView grid, int rowIndex, bool selectedClassesOnly)
        {
            DataSheet ds = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
            DataGridViewComboBoxCell StateClassComboCell = GetComboCell(stateClassColumnName, grid, rowIndex);
            int? TargetStratumId = this.GetDestStratumIdFromGridValues(sourceStratumColumnName, destStratumColumnName, grid, rowIndex);
            string filter = null;

            if (selectedClassesOnly)
            {
                filter = string.Format(CultureInfo.InvariantCulture, "StateClassID IN ({0})", CreateIntegerFilterSpec(this.m_StateClasses));
            }
            else
            {
                filter = this.CreateStateClassIdSourceFilter(TargetStratumId);
            }

            DataView dv = new DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows);

            StateClassComboCell.DataSource = dv;
            StateClassComboCell.ValueMember = "StateClassID";
            StateClassComboCell.DisplayMember = "Name";
        }

        /// <summary>
        /// Determines whether the specified column contains any data
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static bool ColumnContainsData(string columnName, DataGridView grid)
        {
            foreach (DataGridViewRow dgr in grid.Rows)
            {
                object v = dgr.Cells[columnName].Value;

                if (v != DBNull.Value && v != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Initializes the visibility flags for each column
        /// </summary>
        /// <remarks></remarks>
        private void InitializeColumnVisiblity()
        {
            //Deterministic
            this.m_DTIterationVisible = ColumnContainsData(Strings.DATASHEET_ITERATION_COLUMN_NAME, this.m_DTGrid);
            this.m_DTTimestepVisible = ColumnContainsData(Strings.DATASHEET_TIMESTEP_COLUMN_NAME, this.m_DTGrid);
            this.m_DTStratumVisible = false;
            this.m_DTToStratumVisible = ColumnContainsData(Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, this.m_DTGrid);
            this.m_DTToClassVisible = ColumnContainsData(Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, this.m_DTGrid);
            this.m_DTAgeMinVisible = ColumnContainsData(Strings.DATASHEET_AGE_MIN_COLUMN_NAME, this.m_DTGrid);
            this.m_DTAgeMaxVisible = ColumnContainsData(Strings.DATASHEET_AGE_MAX_COLUMN_NAME, this.m_DTGrid);

            //Probabilistic
            this.m_PTIterationVisible = ColumnContainsData(Strings.DATASHEET_ITERATION_COLUMN_NAME, this.m_PTGrid);
            this.m_PTTimestepVisible = ColumnContainsData(Strings.DATASHEET_TIMESTEP_COLUMN_NAME, this.m_PTGrid);
            this.m_PTStratumVisible = false;
            this.m_PTToStratumVisible = ColumnContainsData(Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, this.m_PTGrid);
            this.m_PTToClassVisible = true;
            this.m_PTSecondaryStratumVisible = ColumnContainsData(Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME, this.m_PTGrid);
            this.m_PTTertiaryStratumVisible = ColumnContainsData(Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME, this.m_PTGrid);
            this.m_PTAgeMinVisible = ColumnContainsData(Strings.DATASHEET_AGE_MIN_COLUMN_NAME, this.m_PTGrid);
            this.m_PTAgeMaxVisible = ColumnContainsData(Strings.DATASHEET_AGE_MAX_COLUMN_NAME, this.m_PTGrid);
            this.m_PTAgeRelativeVisible = ColumnContainsData(Strings.DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME, this.m_PTGrid);
            this.m_PTAgeResetVisible = ColumnContainsData(Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME, this.m_PTGrid);
            this.m_PTProportionVisible = ColumnContainsData(Strings.DATASHEET_PT_PROPORTION_COLUMN_NAME, this.m_PTGrid);
            this.m_PTTstMinVisible = ColumnContainsData(Strings.DATASHEET_PT_TST_MIN_COLUMN_NAME, this.m_PTGrid);
            this.m_PTTstMaxVisible = ColumnContainsData(Strings.DATASHEET_PT_TST_MAX_COLUMN_NAME, this.m_PTGrid);
            this.m_PTTstRelativeVisible = ColumnContainsData(Strings.DATASHEET_PT_TST_RELATIVE_COLUMN_NAME, this.m_PTGrid);
        }

        /// <summary>
        /// Updates the visibility of the columns in the deterministic transitions grid
        /// </summary>
        /// <remarks></remarks>
        private void UpdateDTColumnVisibility()
        {
            if (this.m_DTGrid.CurrentCell != null)
            {
                int ci = this.m_DTGrid.CurrentCell.ColumnIndex;
                int ri = this.m_DTGrid.CurrentCell.RowIndex;
                string cn = this.m_DTGrid.Columns[ci].Name;
                DataGridViewRow dgr = this.m_DTGrid.Rows[ri];

                if (cn == Strings.DATASHEET_ITERATION_COLUMN_NAME && !this.m_DTIterationVisible)
                {
                    this.m_DTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_TIMESTEP_COLUMN_NAME && !this.m_DTTimestepVisible)
                {
                    this.m_DTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME && !this.m_DTStratumVisible)
                {
                    this.m_DTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME && !this.m_DTToStratumVisible)
                {
                    this.m_DTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME && !this.m_DTToClassVisible)
                {
                    this.m_DTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_AGE_MIN_COLUMN_NAME && !this.m_DTAgeMinVisible)
                {
                    this.m_DTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_AGE_MAX_COLUMN_NAME && !this.m_DTAgeMaxVisible)
                {
                    this.m_DTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
            }

            this.m_DTGrid.Columns[Strings.DATASHEET_ITERATION_COLUMN_NAME].Visible = this.m_DTIterationVisible;
            this.m_DTGrid.Columns[Strings.DATASHEET_TIMESTEP_COLUMN_NAME].Visible = this.m_DTTimestepVisible;
            this.m_DTGrid.Columns[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME].Visible = this.m_DTStratumVisible;
            this.m_DTGrid.Columns[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME].Visible = this.m_DTToStratumVisible;
            this.m_DTGrid.Columns[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME].Visible = this.m_DTToClassVisible;
            this.m_DTGrid.Columns[Strings.DATASHEET_AGE_MIN_COLUMN_NAME].Visible = this.m_DTAgeMinVisible;
            this.m_DTGrid.Columns[Strings.DATASHEET_AGE_MAX_COLUMN_NAME].Visible = this.m_DTAgeMaxVisible;

            this.MenuItemIterationDeterministic.Checked = this.m_DTIterationVisible;
            this.MenuItemTimestepDeterministic.Checked = this.m_DTTimestepVisible;
            this.MenuItemStratumDeterministic.Checked = this.m_DTStratumVisible;
            this.MenuItemToStratumDeterministic.Checked = this.m_DTToStratumVisible;
            this.MenuItemToClassDetreministic.Checked = this.m_DTToClassVisible;
            this.MenuItemAgeMinDeterministic.Checked = this.m_DTAgeMinVisible;
            this.MenuItemAgeMaxDeterministic.Checked = this.m_DTAgeMaxVisible;
        }

        /// <summary>
        /// Updates the visibility of the columns in the probabilistic transitions grid
        /// </summary>
        /// <remarks></remarks>
        private void UpdatePTColumnVisibility()
        {
            if (this.m_PTGrid.CurrentCell != null)
            {
                int ci = this.m_PTGrid.CurrentCell.ColumnIndex;
                int ri = this.m_PTGrid.CurrentCell.RowIndex;
                string cn = this.m_PTGrid.Columns[ci].Name;
                DataGridViewRow dgr = this.m_PTGrid.Rows[ri];

                if (cn == Strings.DATASHEET_ITERATION_COLUMN_NAME && !this.m_PTIterationVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_TIMESTEP_COLUMN_NAME && !this.m_PTTimestepVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME && !this.m_PTStratumVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME && !this.m_PTToStratumVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME && !this.m_PTToClassVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME && !this.m_PTSecondaryStratumVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME && !this.m_PTTertiaryStratumVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_PROPORTION_COLUMN_NAME && !this.m_PTProportionVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_AGE_MIN_COLUMN_NAME && !this.m_PTAgeMinVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_AGE_MAX_COLUMN_NAME && !this.m_PTAgeMaxVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME && !this.m_PTAgeRelativeVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME && !this.m_PTAgeResetVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_TST_MIN_COLUMN_NAME && !this.m_PTTstMinVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_TST_MAX_COLUMN_NAME && !this.m_PTTstMaxVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
                else if (cn == Strings.DATASHEET_PT_TST_RELATIVE_COLUMN_NAME && !this.m_PTTstRelativeVisible)
                {
                    this.m_PTGrid.CurrentCell = dgr.Cells[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME];
                }
            }

            this.m_PTGrid.Columns[Strings.DATASHEET_ITERATION_COLUMN_NAME].Visible = this.m_PTIterationVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_TIMESTEP_COLUMN_NAME].Visible = this.m_PTTimestepVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME].Visible = this.m_PTStratumVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME].Visible = this.m_PTToStratumVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME].Visible = this.m_PTToClassVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME].Visible = this.m_PTSecondaryStratumVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME].Visible = this.m_PTTertiaryStratumVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_PROPORTION_COLUMN_NAME].Visible = this.m_PTProportionVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_AGE_MIN_COLUMN_NAME].Visible = this.m_PTAgeMinVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_AGE_MAX_COLUMN_NAME].Visible = this.m_PTAgeMaxVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME].Visible = this.m_PTAgeRelativeVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME].Visible = this.m_PTAgeResetVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_TST_MIN_COLUMN_NAME].Visible = this.m_PTTstMinVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_TST_MAX_COLUMN_NAME].Visible = this.m_PTTstMaxVisible;
            this.m_PTGrid.Columns[Strings.DATASHEET_PT_TST_RELATIVE_COLUMN_NAME].Visible = this.m_PTTstRelativeVisible;
        }

        /// <summary>
        /// Sets a column to read-only mode
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grid"></param>
        /// <remarks></remarks>
        private static void SetColumnReadOnly(string columnName, DataGridView grid)
        {
            DataGridViewColumn col = grid.Columns[columnName];
            col.DefaultCellStyle.BackColor = Color.FromArgb(232, 232, 232);
            col.ReadOnly = true;
        }

        /// <summary>
        /// Configures the read-only properties of the columns depending on the chosen filter
        /// </summary>
        /// <remarks></remarks>
        private void ConfigureColumnsReadOnly()
        {
            Debug.Assert(!(this.m_ShowTransitionsTo && this.m_ShowTransitionsFrom));

            foreach (DataGridViewColumn c in this.m_DTGrid.Columns)
            {
                c.DefaultCellStyle.BackColor = Color.White;
                c.ReadOnly = false;
            }

            foreach (DataGridViewColumn c in this.m_PTGrid.Columns)
            {
                c.DefaultCellStyle.BackColor = Color.White;
                c.ReadOnly = false;
            }

            SetColumnReadOnly(Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME, this.m_DTGrid);
            SetColumnReadOnly(Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME, this.m_DTGrid);
            SetColumnReadOnly(Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, this.m_PTGrid);

            if (this.m_ShowTransitionsTo)
            {
                SetColumnReadOnly(Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, this.m_DTGrid);
                SetColumnReadOnly(Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, this.m_DTGrid);
                SetColumnReadOnly(Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, this.m_PTGrid);
                SetColumnReadOnly(Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, this.m_PTGrid);
                SetColumnReadOnly(Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME, this.m_PTGrid);
            }
        }

        /// <summary>
        /// Toggles the visibility of the Deterministic iteration column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleDeterministicIterationVisible()
        {
            this.m_DTIterationVisible = (!this.m_DTIterationVisible);
            this.UpdateDTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Deterministic timestep column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleDeterministicTimestepVisible()
        {
            this.m_DTTimestepVisible = (!this.m_DTTimestepVisible);
            this.UpdateDTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Deterministic Stratum column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleDeterministicStratumVisible()
        {
            this.m_DTStratumVisible = (!this.m_DTStratumVisible);
            this.UpdateDTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Deterministic To Stratum column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleDeterministicToStratumVisible()
        {
            this.m_DTToStratumVisible = (!this.m_DTToStratumVisible);
            this.UpdateDTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Deterministic To Class column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleDeterministicToClassVisible()
        {
            this.m_DTToClassVisible = (!this.m_DTToClassVisible);
            this.UpdateDTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibilty of the Deterministic age min column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleDeterministicAgeMinVisible()
        {
            this.m_DTAgeMinVisible = (!this.m_DTAgeMinVisible);
            this.UpdateDTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibilty of the Deterministic age max column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleDeterministicAgeMaxVisible()
        {
            this.m_DTAgeMaxVisible = (!this.m_DTAgeMaxVisible);
            this.UpdateDTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Probabilistic iteration column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticIterationVisible()
        {
            this.m_PTIterationVisible = (!this.m_PTIterationVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Probabilistic timestep column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticTimestepVisible()
        {
            this.m_PTTimestepVisible = (!this.m_PTTimestepVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Probabilistic Stratum column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticStratumVisible()
        {
            this.m_PTStratumVisible = (!this.m_PTStratumVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Probabilistic To Stratum column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticToStratumVisible()
        {
            this.m_PTToStratumVisible = (!this.m_PTToStratumVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Probabilistic To Class column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticToClassVisible()
        {
            this.m_PTToClassVisible = (!this.m_PTToClassVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Probabilistic Secondary Stratum column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticSSVisible()
        {
            this.m_PTSecondaryStratumVisible = (!this.m_PTSecondaryStratumVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibility of the Probabilistic Tertiary Stratum column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticTSVisible()
        {
            this.m_PTTertiaryStratumVisible = (!this.m_PTTertiaryStratumVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibilty of the Probabilistic age min column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticAgeMinVisible()
        {
            this.m_PTAgeMinVisible = (!this.m_PTAgeMinVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibilty of the Probabilistic age max column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticAgeMaxVisible()
        {
            this.m_PTAgeMaxVisible = (!this.m_PTAgeMaxVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibilty of the Probabilistic age relative (shift) column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticAgeRelativeVisible()
        {
            this.m_PTAgeRelativeVisible = (!this.m_PTAgeRelativeVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Toggles the visibilty of the Probabilistic age reset column
        /// </summary>
        /// <remarks></remarks>
        private void ToggleProbabilisticAgeResetVisible()
        {
            this.m_PTAgeResetVisible = (!this.m_PTAgeResetVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Shows transitions To
        /// </summary>
        /// <remarks></remarks>
        private void ShowTransitionsTo()
        {
            this.m_ShowTransitionsFrom = false;
            this.m_ShowTransitionsTo = true;

            this.FilterDeterministicTransitions();
            this.FilterProbabilisticTransitions();

            this.MenuItemTransitionsToDeterministic.Checked = this.m_ShowTransitionsTo;
            this.MenuItemTransitionsFromDeterministic.Checked = this.m_ShowTransitionsFrom;

            this.ConfigureColumnsReadOnly();
        }

        /// <summary>
        /// Shows transitions from
        /// </summary>
        /// <remarks></remarks>
        private void ShowTransitionsFrom()
        {
            this.m_ShowTransitionsFrom = true;
            this.m_ShowTransitionsTo = false;

            this.FilterDeterministicTransitions();
            this.FilterProbabilisticTransitions();

            this.MenuItemTransitionsToDeterministic.Checked = this.m_ShowTransitionsTo;
            this.MenuItemTransitionsFromDeterministic.Checked = this.m_ShowTransitionsFrom;

            this.ConfigureColumnsReadOnly();
        }

        /// <summary>
        /// Executes the Probabilistic Transtions To command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticTranstionsToCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ShowTransitionsTo();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions To command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticTranstionsToCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_ShowTransitionsTo;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions From command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticTranstionsFromCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ShowTransitionsFrom();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions From command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticTranstionsFromCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_ShowTransitionsFrom;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Iteration command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticIterationCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticIterationVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Iteration command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticIterationCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTIterationVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Timestep command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticTimestepCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticTimestepVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Timestep command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticTimestepCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTTimestepVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticStratumCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticStratumVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticStratumCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTStratumVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions To Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticToStratumCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticToStratumVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions To Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticToStratumCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTToStratumVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions To Class command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticToClassCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticToClassVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions To Class command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticToClassCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTToClassVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Secondary Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticSSCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticSSVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Secondary Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticSSCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTSecondaryStratumVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Tertiary Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticTSCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticTSVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Tertiary Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticTSCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTTertiaryStratumVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Proportion command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticProportionCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_PTProportionVisible = (!this.m_PTProportionVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Proportion command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticProportionCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTProportionVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Age Min command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticAgeMinCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticAgeMinVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Age Min command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticAgeMinCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTAgeMinVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Age Max command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticAgeMaxCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticAgeMaxVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Age Max command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticAgeMaxCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTAgeMaxVisible;
        }
        /// <summary>
        /// Executes the Probabilistic Transtions Age Relative command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticAgeRelativeCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticAgeRelativeVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Age Relative command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticAgeRelativeCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTAgeRelativeVisible;
        }
        /// <summary>
        /// Executes the Probabilistic Transtions Age Reset command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticAgeResetCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.ToggleProbabilisticAgeResetVisible();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Age Reset command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticAgeResetCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTAgeResetVisible;
        }


        /// <summary>
        /// Executes the Probabilistic Transtions Tst Min command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticTstMinCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_PTTstMinVisible = (!this.m_PTTstMinVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Tst Min command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticTstMinCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTTstMinVisible;
        }
        /// <summary>
        /// Executes the Probabilistic Transtions Tst Max command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticTstMaxCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_PTTstMaxVisible = (!this.m_PTTstMaxVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Tst Max command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticTstMaxCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTTstMaxVisible;
        }

        /// <summary>
        /// Executes the Probabilistic Transtions Tst Relative command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteProbabilisticTstRelativeCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_PTTstRelativeVisible = (!this.m_PTTstRelativeVisible);
            this.UpdatePTColumnVisibility();
        }

        /// <summary>
        /// Updates the Probabilistic Transtions Tst Relative command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateProbabilisticTstRelativeCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_PTTstRelativeVisible;
        }

        /// <summary>
        /// Handles the Transitions To context menu item Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemTransitionsToDeterministic_Click(object sender, System.EventArgs e)
        {
            if (this.Validate())
            {
                this.ShowTransitionsTo();
            }
        }

        /// <summary>
        /// Handles the Transitions From context menu item Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemTransitionsFromDeterministic_Click(object sender, System.EventArgs e)
        {
            if (this.Validate())
            {
                this.ShowTransitionsFrom();
            }
        }

        /// <summary>
        /// Handles the Iteration context menu item Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemIterationDeterministic_Click(object sender, EventArgs e)
        {
            this.ToggleDeterministicIterationVisible();
        }

        /// <summary>
        /// Handles the Timestep context menu item Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemTimestepDeterministic_Click(object sender, EventArgs e)
        {
            this.ToggleDeterministicTimestepVisible();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemStratumDeterministic_Click(object sender, System.EventArgs e)
        {
            this.ToggleDeterministicStratumVisible();
        }

        /// <summary>
        /// Handles the To Stratum context menu item Clicked event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemToStratumDeterministic_Click(object sender, EventArgs e)
        {
            this.ToggleDeterministicToStratumVisible();
        }

        /// <summary>
        /// Handles the To Class context menu item Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemToClassDetreministic_Click(object sender, EventArgs e)
        {
            this.ToggleDeterministicToClassVisible();
        }

        /// <summary>
        /// Handles the Ages context menu item Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemAgeMinDeterministic_Click(object sender, System.EventArgs e)
        {
            this.ToggleDeterministicAgeMinVisible();
        }

        /// <summary>
        /// Handles the Ages context menu item Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void MenuItemAgeMaxDeterministic_Click(object sender, System.EventArgs e)
        {
            this.ToggleDeterministicAgeMaxVisible();
        }
    }
}
