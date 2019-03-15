// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class TransitionDiagramPasteSpecialForm
    {
        public TransitionDiagramPasteSpecialForm()
        {
            InitializeComponent();
        }

        public bool PasteTransitionsAll
        {
            get
            {
                return this.RadioButtonPasteAll.Checked;
            }
        }

        public bool PasteTransitionsBetween
        {
            get
            {
                return this.RadioButtonPasteBetween.Checked;
            }
        }

        public bool PasteTransitionsNone
        {
            get
            {
                return this.RadioButtonPasteNone.Checked;
            }
        }

        public bool PasteTransitionsDeterministic
        {
            get
            {
                return this.CheckboxPasteDeterministic.Checked;
            }
        }

        public bool PasteTransitionsProbabilistic
        {
            get
            {
                return this.CheckboxPasteProbabilistic.Checked;
            }
        }

        private void OnRadioButtonOptionChanged()
        {
            this.CheckboxPasteDeterministic.Enabled = (!this.RadioButtonPasteNone.Checked);
            this.CheckboxPasteProbabilistic.Enabled = (!this.RadioButtonPasteNone.Checked);

            if (this.RadioButtonPasteNone.Checked == false)
            {
                if (this.CheckboxPasteDeterministic.Checked == false && this.CheckboxPasteProbabilistic.Checked == false)
                {
                    this.CheckboxPasteDeterministic.Checked = true;
                    this.CheckboxPasteProbabilistic.Checked = true;
                }
            }
        }

        private void OnCheckboxChanged()
        {
            if (this.CheckboxPasteDeterministic.Checked == false && this.CheckboxPasteProbabilistic.Checked == false)
            {
                this.RadioButtonPasteNone.Checked = true;
            }
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void PasteNoneRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            this.OnRadioButtonOptionChanged();
        }

        private void PasteBetweenRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            this.OnRadioButtonOptionChanged();
        }

        private void PasteAllRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            this.OnRadioButtonOptionChanged();
        }

        private void PasteDeterministicCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.OnCheckboxChanged();
        }

        private void PasteProbabilisticCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.OnCheckboxChanged();
        }
    }
}
