// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
	internal partial class ChooseStockTypeForm
	{
		public ChooseStockTypeForm()
		{
			InitializeComponent();
		}

		private FlowPathwayDiagram m_Diagram;
		private Project m_Project;
		private int m_StockTypeId;

		public int StockTypeId
		{
			get
			{
				Debug.Assert(this.m_StockTypeId > 0);
				return this.m_StockTypeId;
			}
		}

		public void Initialize(FlowPathwayDiagram diagram, Project project)
		{
			this.m_Diagram = diagram;
			this.m_Project = project;
			DataSheet ds = this.m_Project.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_NAME);
			DataView dv = new DataView(ds.GetData(), null, ds.DisplayMember, DataViewRowState.CurrentRows);

			foreach (DataRowView drv in dv)
			{
				this.ComboBoxStocks.Items.Add(
                    new BaseValueDisplayListItem(
                        Convert.ToInt32(drv.Row[ds.ValueMember], CultureInfo.InvariantCulture), 
                        Convert.ToString(drv.Row[ds.DisplayMember], CultureInfo.InvariantCulture)));
			}

			Debug.Assert(dv.Count > 0);
			Debug.Assert(this.ComboBoxStocks.Items.Count == dv.Count);

			this.ComboBoxStocks.SelectedIndex = 0;
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			Debug.Assert(this.m_StockTypeId == 0);
			int id = ((BaseValueDisplayListItem)this.ComboBoxStocks.SelectedItem).Value;

			if (this.m_Diagram.GetStockTypeShape(id) != null)
			{
				FormsUtilities.InformationMessageBox("The specified stock type has already been added to the diagram.");
				return;
			}

			this.m_StockTypeId = id;
			this.DialogResult = System.Windows.Forms.DialogResult.OK;

			this.Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			Debug.Assert(this.m_StockTypeId == 0);
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

			this.Close();
		}
	}
}