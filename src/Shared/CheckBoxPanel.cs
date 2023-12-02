// SyncroSim Modeling Framework
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
// The TERMS OF USE and END USER LICENSE AGREEMENT for this software can be found in the LICENSE file.

using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SyncroSim.STSim
{
    public partial class CheckBoxPanel
    {
        private DataTable m_DataSource;
        private bool m_IsReadOnly;

        private static Color GRID_EMPTY_COLOR =
            Color.FromArgb(
            Convert.ToInt32(Convert.ToByte(245)),
            Convert.ToInt32(Convert.ToByte(245)),
            Convert.ToInt32(Convert.ToByte(245)));

        public CheckBoxPanel()
        {
            InitializeComponent();

            CheckAllCheckbox.CheckedChanged += new EventHandler(CheckAllCheckbox_CheckedChanged);
            ItemsDataGrid.CurrentCellDirtyStateChanged += new EventHandler(ItemsDataGrid_CurrentCellDirtyStateChanged);
            ItemsDataGrid.CellPainting += new DataGridViewCellPaintingEventHandler(MainDataGrid_CellPainting);
        }

        public void Initialize()
        {
            this.InitializeDataSource();

            this.ItemsDataGrid.MultiSelect = false;
            this.ItemsDataGrid.ApplyFocusColors = false;
            this.ItemsDataGrid.StandardTab = true;
            this.ItemsDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ItemsDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ItemsDataGrid.BackgroundColor = GRID_EMPTY_COLOR;
            this.ItemsDataGrid.DefaultCellStyle.SelectionBackColor = Color.White;
            this.ItemsDataGrid.RowsDefaultCellStyle.SelectionBackColor = Color.White;
        }

        public DataTable DataSource
        {
            get
            {
                return this.m_DataSource;
            }
        }

        public string TitleBarText
        {
            get
            {
                return this.ItemNameColumn.HeaderText;
            }
            set
            {
                this.ItemNameColumn.HeaderText = value;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.m_IsReadOnly;
            }
            set
            {
                this.m_IsReadOnly = value;
                this.ItemsDataGrid.IsReadOnly = this.m_IsReadOnly;
                this.CheckAllCheckbox.Enabled = (!this.m_IsReadOnly);
            }
        }

        public void BeginAddItems()
        {
            this.ItemsDataGrid.DataSource = null;
        }

        public void AddItem(bool isSelected, int itemId, string itemName)
        {
            DataRow NewRow = this.m_DataSource.NewRow();

            NewRow["IsSelected"] = isSelected;
            NewRow["ItemID"] = itemId;
            NewRow["ItemName"] = itemName;

            this.m_DataSource.Rows.Add(NewRow);
        }

        public void EndAddItems()
        {
            this.ItemsDataGrid.DataSource = new DataView(this.m_DataSource, null, "ItemName", DataViewRowState.CurrentRows);
            this.UpdateCheckAllCheckbox();
       }

        private void InitializeDataSource()
        {
            this.m_DataSource = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };

            this.m_DataSource.Columns.Add(new DataColumn("IsSelected", typeof(bool)));
            this.m_DataSource.Columns.Add(new DataColumn("ItemID", typeof(int)));
            this.m_DataSource.Columns.Add(new DataColumn("ItemName", typeof(string)));
        }

        private void CheckAllItems(bool selected)
        {
            foreach (DataRow dr in this.m_DataSource.Rows)
            {
                dr["IsSelected"] = selected;
            }
        }

        private bool AllItemsChecked()
        {
            foreach (DataRow dr in this.m_DataSource.Rows)
            {
                bool selected = Convert.ToBoolean(dr["IsSelected"], CultureInfo.InvariantCulture);

                if (selected == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateCheckAllCheckbox()
        {
            CheckAllCheckbox.CheckedChanged -= CheckAllCheckbox_CheckedChanged;
            this.CheckAllCheckbox.Checked = this.AllItemsChecked();
            CheckAllCheckbox.CheckedChanged += CheckAllCheckbox_CheckedChanged;
        }

        private void CheckAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckAllItems(this.CheckAllCheckbox.Checked);
            this.UpdateCheckAllCheckbox();
        }

        private void ItemsDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            this.Validate();
            this.UpdateCheckAllCheckbox();
        }

        private void MainDataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                if (this.m_IsReadOnly)
                {
                    e.PaintBackground(e.CellBounds, false);

                    Point pt = e.CellBounds.Location;
                    pt.Offset(e.CellBounds.Width / 2 - 8 + 2, e.CellBounds.Height / 2 - 8 + 1);

                    bool CellValue = Convert.ToBoolean(
                        this.ItemsDataGrid.Rows[e.RowIndex].Cells[0].Value, CultureInfo.InvariantCulture);

                    if (CellValue == true)
                    {
                        CheckBoxRenderer.DrawCheckBox(
                            e.Graphics, pt, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled);
                    }
                    else
                    {
                        CheckBoxRenderer.DrawCheckBox(
                            e.Graphics, pt, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled);
                    }

                    e.Handled = true;
                }
            }
        }
    }
}
