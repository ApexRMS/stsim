// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class FilterTransitionsForm
    {
        public FilterTransitionsForm()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Validate();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Validate();
            this.Close();
        }

        private void ProbabilisticTransitionsCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.CheckBoxPanelMain.IsReadOnly = (!this.CheckboxProbabilisticTransitions.Checked);
        }
    }
}
