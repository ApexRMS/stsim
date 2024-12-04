// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
    internal partial class InitialConditionsNonSpatialDataFeedView
    {
        public InitialConditionsNonSpatialDataFeedView()
        {
            InitializeComponent();
        }

        protected override void InitializeView()
        {
            base.InitializeView();

            DataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
            this.PanelDistribution.Controls.Add(v);
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(this.TextBoxTotalAmount, Strings.DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxNumCells, Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxCalcFromDist, Strings.DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME);

            DataFeedView v = (DataFeedView)this.PanelDistribution.Controls[0];
            v.LoadDataFeed(dataFeed, Strings.DATASHEET_NSIC_DISTRIBUTION_NAME);

            this.RefreshBoundControls();
            this.CalculateCellSize();
        }

        protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
        {
            base.OnDataSheetChanged(e);

            string amountlabel = null;
            TerminologyUnit units = 0;
            string unitsLbl = null;

            TerminologyUtilities.GetAmountLabelTerminology(this.Project, ref amountlabel, ref units);
            unitsLbl = TerminologyUtilities.TerminologyUnitToString(units).ToLower(CultureInfo.InvariantCulture);

            this.LabelTotalAmount.Text = string.Format(CultureInfo.InvariantCulture, "Total ({0}):", unitsLbl);
            this.LabelCellSize.Text = string.Format(CultureInfo.InvariantCulture, "Cell size ({0}):", unitsLbl);
            this.TextBoxNumCells.Enabled = (this.ShouldEnableView() && (!this.CheckBoxCalcFromDist.Checked));
        }

        public override void EnableView(bool enable)
        {
            if (this.PanelDistribution.Controls.Count > 0)
            {
                DataFeedView v = (DataFeedView)this.PanelDistribution.Controls[0];
                v.EnableView(enable);
            }

            this.TextBoxTotalAmount.Enabled = enable;
            this.TextBoxNumCells.Enabled = (enable && (!this.CheckBoxCalcFromDist.Checked));
            this.CheckBoxCalcFromDist.Enabled = enable;
            this.ButtonClearAll.Enabled = enable;
        }

        protected override void OnRowsAdded(object sender, Core.DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);
            this.RecomputeNumCellsForDistributionChange(sender);
        }

        protected override void OnRowsDeleted(object sender, Core.DataSheetRowEventArgs e)
        {
            base.OnRowsDeleted(sender, e);
            this.RecomputeNumCellsForDistributionChange(sender);
        }

        protected override void OnRowsModified(object sender, Core.DataSheetRowEventArgs e)
        {
            base.OnRowsModified(sender, e);
            this.RecomputeNumCellsForDistributionChange(sender);
        }

        protected override void OnBoundTextBoxChanged(TextBox textBox, string columnName)
        {
            base.OnBoundTextBoxChanged(textBox, columnName);
            this.CalculateCellSize();
        }

        protected override void OnBoundCheckBoxChanged(CheckBox checkBox, string columnName)
        {
            base.OnBoundCheckBoxChanged(checkBox, columnName);

            if (this.CheckBoxCalcFromDist.Checked)
            {
                this.TextBoxNumCells.Enabled = false;
                this.SetNumCellsFromDistribution();
            }
            else
            {
                this.TextBoxNumCells.Enabled = true;
                this.SetTextBoxData(this.TextBoxNumCells, null);
            }

            this.CalculateCellSize();
        }

        private void RecomputeNumCellsForDistributionChange(object sender)
        {
            DataSheet ds = this.DataFeed.GetDataSheet(Strings.DATASHEET_NSIC_DISTRIBUTION_NAME);

            if (sender == ds && this.Scenario == this.ControllingScenario)
            {
                if (this.CheckBoxCalcFromDist.Checked)
                {
                    this.SetNumCellsFromDistribution();
                    this.CalculateCellSize();
                }
            }
        }

        private void SetNumCellsFromDistribution()
        {
            Debug.Assert(this.CheckBoxCalcFromDist.Checked);
            int NumCells = this.CalculateNumCellsFromDistribution();

            if (NumCells > 0)
            {
                this.SetTextBoxData(this.TextBoxNumCells, NumCells.ToString("N4", CultureInfo.InvariantCulture));
            }
            else
            {
                this.SetTextBoxData(this.TextBoxNumCells, null);
            }
        }

        private int CalculateNumCellsFromDistribution()
        {
            int NumCells = 0;
            DataTable dt = this.DataFeed.GetDataSheet(Strings.DATASHEET_NSIC_DISTRIBUTION_NAME).GetData();

            // Use just the lowest(1st iteration) entered to deal with multiple iterations
            int minIteration = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (Convert.IsDBNull(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME]))
                    {
                        minIteration = 0;
                    }
                    else
                    {
                        minIteration = Math.Min(
                            minIteration, 
                            Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture));
                    }
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    var iteration = Convert.ToInt32(
                        Convert.IsDBNull(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME]) ? 0 : 
                        dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);

                    if (iteration == minIteration)
                    {
                        double val = Convert.ToDouble(
                            dr[Strings.DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME], 
                            CultureInfo.InvariantCulture);

                        NumCells += Convert.ToInt32(Math.Round(val), CultureInfo.InvariantCulture);
                    }
                }
            }

            return NumCells;
        }

        private void CalculateCellSize()
        {
            string ns = this.TextBoxNumCells.Text.Trim();
            string ta = this.TextBoxTotalAmount.Text.Trim();

            if (string.IsNullOrEmpty(ns) | string.IsNullOrEmpty(ta))
            {
                this.TextBoxCellSize.Text = null;
                return;
            }

            double TotalAmount = 0.0;
            int NumCells = 0;

            if (!double.TryParse(ta, NumberStyles.Any, CultureInfo.InvariantCulture, out TotalAmount))
            {
                this.TextBoxCellSize.Text = null;
                return;
            }

            if (!int.TryParse(ns, NumberStyles.Any, CultureInfo.InvariantCulture, out NumCells))
            {
                this.TextBoxCellSize.Text = null;
                return;
            }

            double CellSize = 0;

            if (TotalAmount == 0.0 || NumCells == 0.0)
            {
                CellSize = 0.0;
            }
            else
            {
                CellSize = TotalAmount / NumCells;
            }

            this.TextBoxCellSize.Text = CellSize.ToString("N4", CultureInfo.InvariantCulture);
        }

        private void ButtonClearAll_Click(object sender, EventArgs e)
        {
            this.ResetBoundControls();
            this.DataFeed.DataSheets[Strings.DATASHEET_NSIC_NAME].ClearData();
            this.TextBoxCellSize.Text = null;
        }
    }
}
