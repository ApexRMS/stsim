// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal partial class StockTypeQuickView
	{
		public StockTypeQuickView()
		{
			InitializeComponent();
		}

		private DataFeed m_DataFeed;
		private List<int> m_StockTypeIds;
		private DataSheet m_FlowDiagramSheet;
		private DataTable m_FlowDiagramData;
		private MultiRowDataFeedView m_FlowPathwayView;
		private BaseDataGridView m_FlowPathwayGrid;
		private DataSheet m_FlowPathwaySheet;
		private DataTable m_FlowPathwayData;
		private bool m_ShowFlowsFrom = true;
		private bool m_ShowFlowsTo;
		private bool m_FromStratumVisible;
        private bool m_FromSecondaryStratumVisible;
        private bool m_FromTertiaryStratumVisible;
		private bool m_FromStateClassVisible;
		private bool m_FromMinAgeVisible;
		private bool m_ToStratumVisible;
		private bool m_ToStateClassVisible;
		private bool m_ToMinAgeVisible;
		private bool m_TransitionGroupVisible;
		private bool m_StateAttributeTypeVisible;
        private bool m_TransferToStratumVisible;
        private bool m_TransferToSecondaryStratumVisible;
        private bool m_TransferToTertiaryStratumVisible;
        private bool m_TransferToStateClassVisible;
        private bool m_TransferToMinAgeVisible;

		public void LoadStockTypes(DataFeed dataFeed, List<int> stockTypeIds)
		{
			this.m_DataFeed = dataFeed;
			this.m_StockTypeIds = stockTypeIds;

			WinFormSession sess = (WinFormSession)this.Project.Library.Session;

			this.m_FlowDiagramSheet = this.m_DataFeed.Scenario.GetDataSheet(Strings.DATASHEET_FLOW_PATHWAY_DIAGRAM_NAME);
			this.m_FlowDiagramData = this.m_FlowDiagramSheet.GetData();

			this.m_FlowPathwaySheet = this.m_DataFeed.GetDataSheet(Strings.DATASHEET_FLOW_PATHWAY_NAME);
			this.m_FlowPathwayData = this.m_FlowPathwaySheet.GetData();

			this.m_FlowPathwayView = (MultiRowDataFeedView)sess.CreateMultiRowDataFeedView(dataFeed.Scenario, dataFeed.Scenario);
			this.m_FlowPathwayView.LoadDataFeed(this.m_DataFeed, Strings.DATASHEET_FLOW_PATHWAY_NAME);

			this.m_FlowPathwayGrid = this.m_FlowPathwayView.GridControl;

			this.FilterFlowPathways();
			this.ConfigureContextMenus();
			this.InitializeColumnVisiblity();
			this.UpdateColumnVisibility();
			this.ConfigureColumnsReadOnly();

			this.PanelMain.Controls.Add(this.m_FlowPathwayView);
			this.m_FlowPathwayView.ManageOptionalColumns = false;

			this.m_FlowPathwayGrid.CellBeginEdit += OnGridCellBeginEdit;
			this.m_FlowPathwayGrid.CellEndEdit += OnGridCellEndEdit;
		}

		private void FilterFlowPathways()
		{
			string filter = this.CreateGridFilterString();
			((BindingSource)this.m_FlowPathwayGrid.DataSource).Filter = filter;
		}

		private string CreateGridFilterString()
		{
			string Filter = CreateIntegerFilterSpec(this.m_StockTypeIds);

			string FromFormatString = "FromStockTypeId IS NULL OR FromStockTypeId IN ({0})";
			string ToFormatString = "ToStockTypeId IS NULL OR ToStockTypeId IN ({0})";

			if (this.m_ShowFlowsFrom)
			{
				return string.Format(CultureInfo.InvariantCulture, FromFormatString, Filter);
			}
			else
			{
				Debug.Assert(this.m_ShowFlowsTo);
				return string.Format(CultureInfo.InvariantCulture, ToFormatString, Filter);
			}       
		}

		private void InitializeColumnVisiblity()
		{
			this.m_FromStratumVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.FROM_STRATUM_ID_COLUMN_NAME);
			this.m_FromSecondaryStratumVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.FROM_SECONDARY_STRATUM_ID_COLUMN_NAME);
			this.m_FromTertiaryStratumVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.FROM_TERTIARY_STRATUM_ID_COLUMN_NAME);
			this.m_FromStateClassVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.FROM_STATECLASS_ID_COLUMN_NAME);
			this.m_FromMinAgeVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.FROM_MIN_AGE_COLUMN_NAME);
			this.m_ToStratumVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TO_STRATUM_ID_COLUMN_NAME);
			this.m_ToStateClassVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TO_STATECLASS_ID_COLUMN_NAME);
			this.m_ToMinAgeVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TO_MIN_AGE_COLUMN_NAME);
			this.m_TransitionGroupVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME);
			this.m_StateAttributeTypeVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME);
            this.m_TransferToStratumVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TRANSFER_TO_STRATUM_ID_COLUMN_NAME);
            this.m_TransferToSecondaryStratumVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TRANSFER_TO_SECONDARY_STRATUM_ID_COLUMN_NAME);
            this.m_TransferToTertiaryStratumVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TRANSFER_TO_TERTIARY_STRATUM_ID_COLUMN_NAME);
            this.m_TransferToStateClassVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TRANSFER_TO_STATECLASS_ID_COLUMN_NAME);
            this.m_TransferToMinAgeVisible = DataTableUtilities.TableHasData(this.m_FlowPathwayData, Strings.TRANSFER_TO_MIN_AGE_COLUMN_NAME);
		}

		private void UpdateColumnVisibility()
		{
			if (this.m_FlowPathwayGrid.CurrentCell != null)
			{
				bool ResetCurrentCell = false;
				int CurrentColumnIndex = this.m_FlowPathwayGrid.CurrentCell.ColumnIndex;
				int CurrentRowIndex = this.m_FlowPathwayGrid.CurrentCell.RowIndex;
				string CurrentColumnName = this.m_FlowPathwayGrid.Columns[CurrentColumnIndex].Name;

				if (CurrentColumnName == Strings.FROM_STRATUM_ID_COLUMN_NAME && (!this.m_FromStratumVisible))
				{
					ResetCurrentCell = true;
				}

				if (ResetCurrentCell)
				{
					this.m_FlowPathwayGrid.CurrentCell = this.m_FlowPathwayGrid.Rows[CurrentRowIndex].Cells[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME];
				}
			}

			this.m_FlowPathwayGrid.Columns[Strings.FROM_STRATUM_ID_COLUMN_NAME].Visible = this.m_FromStratumVisible;
			this.m_FlowPathwayGrid.Columns[Strings.FROM_SECONDARY_STRATUM_ID_COLUMN_NAME].Visible = this.m_FromSecondaryStratumVisible;
			this.m_FlowPathwayGrid.Columns[Strings.FROM_TERTIARY_STRATUM_ID_COLUMN_NAME].Visible = this.m_FromTertiaryStratumVisible;
			this.m_FlowPathwayGrid.Columns[Strings.FROM_STATECLASS_ID_COLUMN_NAME].Visible = this.m_FromStateClassVisible;
			this.m_FlowPathwayGrid.Columns[Strings.FROM_MIN_AGE_COLUMN_NAME].Visible = this.m_FromMinAgeVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TO_STRATUM_ID_COLUMN_NAME].Visible = this.m_ToStratumVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TO_STATECLASS_ID_COLUMN_NAME].Visible = this.m_ToStateClassVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TO_MIN_AGE_COLUMN_NAME].Visible = this.m_ToMinAgeVisible;
			this.m_FlowPathwayGrid.Columns[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME].Visible = this.m_TransitionGroupVisible;
			this.m_FlowPathwayGrid.Columns[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME].Visible = this.m_StateAttributeTypeVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TRANSFER_TO_STRATUM_ID_COLUMN_NAME].Visible = this.m_TransferToStratumVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TRANSFER_TO_SECONDARY_STRATUM_ID_COLUMN_NAME].Visible = this.m_TransferToSecondaryStratumVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TRANSFER_TO_TERTIARY_STRATUM_ID_COLUMN_NAME].Visible = this.m_TransferToTertiaryStratumVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TRANSFER_TO_STATECLASS_ID_COLUMN_NAME].Visible = this.m_TransferToStateClassVisible;
			this.m_FlowPathwayGrid.Columns[Strings.TRANSFER_TO_MIN_AGE_COLUMN_NAME].Visible = this.m_TransferToMinAgeVisible;
		}

		private void ConfigureContextMenus()
		{
			for (int i = this.m_FlowPathwayView.Commands.Count - 1; i >= 0; i--)
			{
				Command c = this.m_FlowPathwayView.Commands[i];

				if (c.Name == "ssim_delete_all" || c.Name == "ssim_import" || c.Name == "ssim_export" || c.Name == "ssim_export_all")
				{
					this.m_FlowPathwayView.Commands.RemoveAt(i);
				}
			}

			string StratumLabel = this.GetStratumLabelTerminology();
			string SecondaryStratumLabel = this.GetSecondaryStratumLabelTerminology();
			string TertiaryStratumLabel = this.GetTertiaryStratumLabelTerminology();
			string FromStratum = string.Format(CultureInfo.InvariantCulture, "From {0}", StratumLabel);
			string FromSecondaryStratum = string.Format(CultureInfo.InvariantCulture, "From {0}", SecondaryStratumLabel);
			string FromTertiaryStratum = string.Format(CultureInfo.InvariantCulture, "From {0}", TertiaryStratumLabel);
			string ToStratum = string.Format(CultureInfo.InvariantCulture, "To {0}", StratumLabel);
			string TransferToStratum = string.Format(CultureInfo.InvariantCulture, "Transfer To {0}", StratumLabel);
			string TransferToSecondaryStratum = string.Format(CultureInfo.InvariantCulture, "Transfer To {0}", SecondaryStratumLabel);
			string TransferToTertiaryStratum = string.Format(CultureInfo.InvariantCulture, "Transfer To {0}", TertiaryStratumLabel);

			this.m_FlowPathwayView.Commands.Add(new Command("Flows To", OnExecuteFlowsToCommand, OnUpdateFlowsToCommand));
			this.m_FlowPathwayView.Commands.Add(new Command("Flows From", OnExecuteFlowsFromCommand, OnUpdateFlowsFromCommand));
			this.m_FlowPathwayView.Commands.Add(Command.CreateSeparatorCommand());
			this.m_FlowPathwayView.Commands.Add(new Command(FromStratum, OnExecuteFromStratumCommand, OnUpdateFromStratumCommand));
			this.m_FlowPathwayView.Commands.Add(new Command(FromSecondaryStratum, OnExecuteFromSecondaryStratumCommand, OnUpdateFromSecondaryStratumCommand));
			this.m_FlowPathwayView.Commands.Add(new Command(FromTertiaryStratum, OnExecuteFromTertiaryStratumCommand, OnUpdateFromTertiaryStratumCommand));
			this.m_FlowPathwayView.Commands.Add(new Command("From State Class", OnExecuteFromStateClassCommand, OnUpdateFromStateClassCommand));
			this.m_FlowPathwayView.Commands.Add(new Command("From Min Age", OnExecuteFromMinAgeCommand, OnUpdateFromMinAgeCommand));
			this.m_FlowPathwayView.Commands.Add(new Command(ToStratum, OnExecuteToStratumCommand, OnUpdateToStratumCommand));
			this.m_FlowPathwayView.Commands.Add(new Command("To State Class", OnExecuteToStateClassCommand, OnUpdateToStateClassCommand));
			this.m_FlowPathwayView.Commands.Add(new Command("To Min Age", OnExecuteToMinAgeCommand, OnUpdateToMinAgeCommand));
			this.m_FlowPathwayView.Commands.Add(new Command("Transition Group", OnExecuteTransitionGroupCommand, OnUpdateTransitionGroupCommand));
			this.m_FlowPathwayView.Commands.Add(new Command("State Attribute Type", OnExecuteStateAttributeTypeCommand, OnUpdateStateAttributeTypeCommand));
            this.m_FlowPathwayView.Commands.Add(new Command(TransferToStratum, OnExecuteTransferToStratumCommand, OnUpdateTransferToStratumCommand));
            this.m_FlowPathwayView.Commands.Add(new Command(TransferToSecondaryStratum, OnExecuteTransferToSecondaryStratumCommand, OnUpdateTransferToSecondaryStratumCommand));
            this.m_FlowPathwayView.Commands.Add(new Command(TransferToTertiaryStratum, OnExecuteTransferToTertiaryStratumCommand, OnUpdateTransferToTertiaryStratumCommand));
            this.m_FlowPathwayView.Commands.Add(new Command("Transfer To State Class", OnExecuteTransferToStateClassCommand, OnUpdateTransferToStateClassCommand));
            this.m_FlowPathwayView.Commands.Add(new Command("Transfer To Min Age", OnExecuteTransferToMinAgeCommand, OnUpdateTransferToMinAgeCommand));

            this.m_FlowPathwayView.RefreshContextMenuStrip();

			for (int i = this.m_FlowPathwayGrid.ContextMenuStrip.Items.Count - 1; i >= 0; i--)
			{
				ToolStripItem item = this.m_FlowPathwayGrid.ContextMenuStrip.Items[i];

				if (item.Name == "ssim_optional_column_separator" || item.Name == "ssim_optional_column_item")
				{
					this.m_FlowPathwayGrid.ContextMenuStrip.Items.RemoveAt(i);
				}
			}
		}

		private void OnExecuteFlowsToCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_ShowFlowsFrom = false;
			this.m_ShowFlowsTo = true;

			this.FilterFlowPathways();
			this.ConfigureColumnsReadOnly();
			this.UpdateColumnVisibility();
		}

		private void OnUpdateFlowsToCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_ShowFlowsTo;
		}

		private void OnExecuteFlowsFromCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_ShowFlowsFrom = true;
			this.m_ShowFlowsTo = false;

			this.FilterFlowPathways();
			this.ConfigureColumnsReadOnly();
			this.UpdateColumnVisibility();
		}

		private void OnUpdateFlowsFromCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_ShowFlowsFrom;
		}

		private void OnExecuteFromStratumCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_FromStratumVisible = (!this.m_FromStratumVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateFromStratumCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_FromStratumVisible;
		}

        private void OnExecuteFromSecondaryStratumCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_FromSecondaryStratumVisible = (!this.m_FromSecondaryStratumVisible);
            this.UpdateColumnVisibility();
        }

        private void OnUpdateFromSecondaryStratumCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_FromSecondaryStratumVisible;
        }

        private void OnExecuteFromTertiaryStratumCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_FromTertiaryStratumVisible = (!this.m_FromTertiaryStratumVisible);
            this.UpdateColumnVisibility();
        }

        private void OnUpdateFromTertiaryStratumCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_FromTertiaryStratumVisible;
        }

        private void OnExecuteFromStateClassCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_FromStateClassVisible = (!this.m_FromStateClassVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateFromStateClassCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_FromStateClassVisible;
		}

		private void OnExecuteFromMinAgeCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_FromMinAgeVisible = (!this.m_FromMinAgeVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateFromMinAgeCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_FromMinAgeVisible;
		}

		private void OnExecuteToStratumCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_ToStratumVisible = (!this.m_ToStratumVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateToStratumCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_ToStratumVisible;
		}

        private void OnExecuteToStateClassCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_ToStateClassVisible = (!this.m_ToStateClassVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateToStateClassCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_ToStateClassVisible;
		}

		private void OnExecuteToMinAgeCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_ToMinAgeVisible = (!this.m_ToMinAgeVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateToMinAgeCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_ToMinAgeVisible;
		}

		private void OnExecuteTransitionGroupCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_TransitionGroupVisible = (!this.m_TransitionGroupVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateTransitionGroupCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_TransitionGroupVisible;
		}

		private void OnExecuteStateAttributeTypeCommand(Command cmd)
		{
			if (!this.Validate())
			{
				return;
			}

			this.m_StateAttributeTypeVisible = (!this.m_StateAttributeTypeVisible);
			this.UpdateColumnVisibility();
		}

		private void OnUpdateStateAttributeTypeCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = this.m_StateAttributeTypeVisible;
		}

        private void OnExecuteTransferToStratumCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_TransferToStratumVisible = (!this.m_TransferToStratumVisible);
            this.UpdateColumnVisibility();
        }

        private void OnUpdateTransferToStratumCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_TransferToStratumVisible;
        }

        private void OnExecuteTransferToSecondaryStratumCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_TransferToSecondaryStratumVisible = (!this.m_TransferToSecondaryStratumVisible);
            this.UpdateColumnVisibility();
        }

        private void OnUpdateTransferToSecondaryStratumCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_TransferToSecondaryStratumVisible;
        }

        private void OnExecuteTransferToTertiaryStratumCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_TransferToTertiaryStratumVisible = (!this.m_TransferToTertiaryStratumVisible);
            this.UpdateColumnVisibility();
        }

        private void OnUpdateTransferToTertiaryStratumCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_TransferToTertiaryStratumVisible;
        }

        private void OnExecuteTransferToStateClassCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_TransferToStateClassVisible = (!this.m_TransferToStateClassVisible);
            this.UpdateColumnVisibility();
        }

        private void OnUpdateTransferToStateClassCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_TransferToStateClassVisible;
        }

        private void OnExecuteTransferToMinAgeCommand(Command cmd)
        {
            if (!this.Validate())
            {
                return;
            }

            this.m_TransferToMinAgeVisible = (!this.m_TransferToMinAgeVisible);
            this.UpdateColumnVisibility();
        }

        private void OnUpdateTransferToMinAgeCommand(Command cmd)
        {
            cmd.IsEnabled = true;
            cmd.IsChecked = this.m_TransferToMinAgeVisible;
        }

        private void OnGridCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			if (e.ColumnIndex == this.m_FlowPathwayGrid.Columns[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME].Index)
			{
				this.FilterStockTypeCombo(Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME, e.RowIndex, true);
			}
			else if (e.ColumnIndex == this.m_FlowPathwayGrid.Columns[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME].Index)
			{
				this.FilterStockTypeCombo(Strings.TO_STOCK_TYPE_ID_COLUMN_NAME, e.RowIndex, false);
		    }
		}

		private void OnGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == this.m_FlowPathwayGrid.Columns[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME].Index)
			{
				this.ResetComboFilter(Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME, e.RowIndex);
			}
			else if (e.ColumnIndex == this.m_FlowPathwayGrid.Columns[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME].Index)
			{
				this.ResetComboFilter(Strings.TO_STOCK_TYPE_ID_COLUMN_NAME, e.RowIndex);
			}
		}

		private void ResetComboFilter(string columnName, int rowIndex)
		{
			DataGridViewRow dgv = this.m_FlowPathwayGrid.Rows[rowIndex];
			DataGridViewComboBoxCell DestStratumCell = (DataGridViewComboBoxCell)dgv.Cells[columnName];
			DataGridViewComboBoxColumn DestStratumColumn = (DataGridViewComboBoxColumn)this.m_FlowPathwayGrid.Columns[columnName];

			DestStratumCell.DataSource = DestStratumColumn.DataSource;
			DestStratumCell.ValueMember = DestStratumColumn.ValueMember;
			DestStratumCell.DisplayMember = DestStratumColumn.DisplayMember;
		}

		private void FilterStockTypeCombo(string columnName, int rowIndex, bool selectedTypesOnly)
		{
			DataSheet ds = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_NAME);
			DataGridViewRow dgr = this.m_FlowPathwayGrid.Rows[rowIndex];
			DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgr.Cells[columnName];
			string filter = null;

			if (selectedTypesOnly)
			{
				filter = this.CreateFromStockTypeFilter();
			}
			else
			{
				filter = this.CreateToStockTypeFilter();
			}

			DataView dv = new DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows);

			cell.DataSource = dv;
			cell.ValueMember = Strings.STOCK_TYPE_ID_COLUMN_NAME;
			cell.DisplayMember = Strings.DATASHEET_NAME_COLUMN_NAME;
		}

		private string CreateFromStockTypeFilter()
		{
			string spec = CreateIntegerFilterSpec(this.m_StockTypeIds);
			return string.Format(CultureInfo.InvariantCulture, "{0} IN ({1})", Strings.STOCK_TYPE_ID_COLUMN_NAME, spec);
		}

		private string CreateToStockTypeFilter()
		{
			List<int> lst = new List<int>();

			foreach (DataRow dr in this.m_FlowDiagramData.Rows)
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					int Id = Convert.ToInt32(dr[Strings.STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

					if (!lst.Contains(Id))
					{
						lst.Add(Id);
					}
				}
			}

			if (lst.Count == 0)
			{
				return "StockTypeId=-1";
			}
			else
			{
				string filter = CreateIntegerFilterSpec(lst);
				return string.Format(CultureInfo.InvariantCulture, "StockTypeId IN ({0})", filter);
			}
		}

		private static string CreateIntegerFilterSpec(List<int> ids)
		{
			StringBuilder sb = new StringBuilder();

			foreach (int i in ids)
			{
				sb.Append(i.ToString(CultureInfo.InvariantCulture));
				sb.Append(",");
			}

			return sb.ToString().TrimEnd(',');
		}

		private string GetStratumLabelTerminology()
		{
			string l = "Stratum";
			DataRow dr = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME).GetDataRow();

			if (dr != null)
			{
				if (dr["PrimaryStratumLabel"] != DBNull.Value)
				{
					l = Convert.ToString(dr["PrimaryStratumLabel"], CultureInfo.InvariantCulture);
				}
			}

			return l;
		}

        private string GetSecondaryStratumLabelTerminology()
        {
            string l = "Secondary Stratum";
            DataRow dr = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME).GetDataRow();

            if (dr != null)
            {
                if (dr["SecondaryStratumLabel"] != DBNull.Value)
                {
                    l = Convert.ToString(dr["SecondaryStratumLabel"], CultureInfo.InvariantCulture);
                }
            }

            return l;
        }

        private string GetTertiaryStratumLabelTerminology()
        {
            string l = "Tertiary Stratum";
            DataRow dr = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME).GetDataRow();

            if (dr != null)
            {
                if (dr["TertiaryStratumLabel"] != DBNull.Value)
                {
                    l = Convert.ToString(dr["TertiaryStratumLabel"], CultureInfo.InvariantCulture);
                }
            }

            return l;
        }

        private void SetColumnReadOnly(string columnName)
		{
			DataGridViewColumn col = this.m_FlowPathwayGrid.Columns[columnName];
            col.DefaultCellStyle.BackColor = Constants.READONLY_COLUMN_COLOR;
			col.ReadOnly = true;     
		}

		private void ConfigureColumnsReadOnly()
		{
			Debug.Assert(!(this.m_ShowFlowsTo && this.m_ShowFlowsFrom));

			foreach (DataGridViewColumn c in this.m_FlowPathwayGrid.Columns)
			{
				c.DefaultCellStyle.BackColor = Color.White;
				c.ReadOnly = false;
			}

			if (this.m_ShowFlowsTo)
			{
				SetColumnReadOnly(Strings.TO_STOCK_TYPE_ID_COLUMN_NAME);
				SetColumnReadOnly(Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME);
			}
		}
	}
}