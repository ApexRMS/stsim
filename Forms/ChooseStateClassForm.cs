// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal partial class ChooseStateClassForm
    {
        public ChooseStateClassForm()
        {
            InitializeComponent();
        }

        private TransitionDiagram m_Diagram;
        private DataSheet m_TerminologyDataSheet;
        private DataSheet m_StateLabelXDataSheet;
        private DataSheet m_StateLabelYDataSheet;
        private BaseValueDisplayListItem m_ChosenStateLabelX;
        private BaseValueDisplayListItem m_ChosenStateLabelY;
        private bool m_EditMode;

        public BaseValueDisplayListItem ChosenStateLabelX
        {
            get
            {
                return this.m_ChosenStateLabelX;
            }
        }

        public BaseValueDisplayListItem ChosenStateLabelY
        {
            get
            {
                return this.m_ChosenStateLabelY;
            }
        }

        public bool Initialize(TransitionDiagram diagram, DataFeed dataFeed, bool editMode)
        {
            this.m_Diagram = diagram;

            this.m_TerminologyDataSheet = dataFeed.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            this.m_StateLabelXDataSheet = dataFeed.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_X_NAME);
            this.m_StateLabelYDataSheet = dataFeed.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_Y_NAME);

            this.FillComboBoxes();

            if (this.ComboBoxStateLabelX.Items.Count == 0)
            {
                FormsUtilities.ErrorMessageBox(MessageStrings.ERROR_DIAGRAM_NO_STATE_LABEL_X_VALUES);
                return false;
            }
            else if (this.ComboBoxStateLabelY.Items.Count == 0)
            {
                FormsUtilities.ErrorMessageBox(MessageStrings.ERROR_DIAGRAM_NO_STATE_LABEL_Y_VALUES);
                return false;
            }

            this.ResetSlxAndSlyLabels();
            this.m_EditMode = editMode;

            if (this.m_EditMode)
            {
                this.Text = "Edit State Class";

                StateClassShape EditShape = (StateClassShape)diagram.SelectedShapes.First();
                this.SelectComboValues(EditShape.StateLabelXId, EditShape.StateLabelYId);
            }
            else
            {
                this.Text = "Add State Class";
            }

            return true;
        }

        private void FillComboBoxes()
        {
            DataView dvct = new DataView(
                this.m_StateLabelXDataSheet.GetData(), 
                null, 
                this.m_StateLabelXDataSheet.DisplayMember, 
                DataViewRowState.CurrentRows);

            foreach (DataRowView drv in dvct)
            {
                int Id = Convert.ToInt32(drv.Row[this.m_StateLabelXDataSheet.ValueMember]);
                string Name = Convert.ToString(drv.Row[this.m_StateLabelXDataSheet.DisplayMember]);

                this.ComboBoxStateLabelX.Items.Add(new BaseValueDisplayListItem(Id, Name));
            }

            DataView dvss = new DataView(
                this.m_StateLabelYDataSheet.GetData(), 
                null, 
                this.m_StateLabelYDataSheet.DisplayMember, 
                DataViewRowState.CurrentRows);

            foreach (DataRowView drv in dvss)
            {
                int Id = Convert.ToInt32(drv.Row[this.m_StateLabelYDataSheet.ValueMember]);
                string Name = Convert.ToString(drv.Row[this.m_StateLabelYDataSheet.DisplayMember]);

                this.ComboBoxStateLabelY.Items.Add(new BaseValueDisplayListItem(Id, Name));
            }

            if (this.ComboBoxStateLabelX.Items.Count > 0)
            {
                this.ComboBoxStateLabelX.SelectedIndex = 0;
            }

            if (this.ComboBoxStateLabelY.Items.Count > 0)
            {
                this.ComboBoxStateLabelY.SelectedIndex = 0;
            }
        }

        private void SelectComboValues(int stateLabelXId, int stateLabelYId)
        {
            Debug.Assert(this.m_EditMode);

            foreach (BaseValueDisplayListItem item in this.ComboBoxStateLabelX.Items)
            {
                if (item.Value == stateLabelXId)
                {
                    this.ComboBoxStateLabelX.SelectedItem = item;
                    break;
                }
            }

            foreach (BaseValueDisplayListItem item in this.ComboBoxStateLabelY.Items)
            {
                if (item.Value == stateLabelYId)
                {
                    this.ComboBoxStateLabelY.SelectedItem = item;
                    break;
                }
            }
        }

        private void ResetSlxAndSlyLabels()
        {
            string slxlabel = null;
            string slylabel = null;

            TerminologyUtilities.GetStateLabelTerminology(this.m_TerminologyDataSheet, ref slxlabel, ref slylabel);

            this.LabelStateLabelX.Text = slxlabel + ":";
            this.LabelStateLabelY.Text = slylabel + ":";
        }

        private bool StateClassInDiagram(int slxid, int slyid)
        {
            foreach (StateClassShape s in this.m_Diagram.Shapes)
            {
                if (this.m_Diagram.StratumId.HasValue)
                {
                    if (!s.StratumIdSource.HasValue)
                    {
                        continue;
                    }
                }

                if (s.StateLabelXId == slxid && s.StateLabelYId == slyid)
                {
                    return true;
                }
            }

            return false;
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            this.m_ChosenStateLabelX = (BaseValueDisplayListItem)this.ComboBoxStateLabelX.Items[this.ComboBoxStateLabelX.SelectedIndex];
            this.m_ChosenStateLabelY = (BaseValueDisplayListItem)this.ComboBoxStateLabelY.Items[this.ComboBoxStateLabelY.SelectedIndex];

            if (!this.m_EditMode)
            {
                if (this.StateClassInDiagram(this.m_ChosenStateLabelX.Value, this.m_ChosenStateLabelY.Value))
                {
                    FormsUtilities.ErrorMessageBox(MessageStrings.ERROR_DIAGRAM_STATE_CLASS_EXISTS);
                    return;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
