// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Windows.Forms;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
    internal partial class ExternalProgramDataFeedView : DataFeedView
    {
        public ExternalProgramDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(this.TextBoxExe, Strings.EXTERNAL_DATASHEET_EXE_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxScript, Strings.EXTERNAL_DATASHEET_SCRIPT_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxBI, Strings.EXTERNAL_DATASHEET_BI_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAI, Strings.EXTERNAL_DATASHEET_AI_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxBT, Strings.EXTERNAL_DATASHEET_BT_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAT, Strings.EXTERNAL_DATASHEET_AT_COLUMN_NAME);
        }

        public override void EnableView(bool enable)
        {
            base.EnableView(enable);

            if (enable)
            {
                this.EnableControls();
            }
        }

        protected override bool OnBoundTextBoxValidating(TextBox textBox, string columnName, string proposedValue)
        {
            if (textBox == this.TextBoxBI || textBox == this.TextBoxAI || textBox == this.TextBoxBT || textBox == this.TextBoxAT)
            {
                string t = textBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(t))
                {
                    return true;
                }

                string[] split = t.Split(',');

                foreach (string s in split)
                {
                    if (s.Contains("-"))
                    {
                        if (!ValidateRange(s)) { return false; }
                    }
                    else
                    {
                        if (!ValidateSingle(s)) { return false; }
                    }
                }
            }

            return true;
        }

        private static bool ValidateSingle(string s)
        {
            string t = s.Trim();

            if (!int.TryParse(t, out _))
            {
                ValidationError();
                return false;
            }

            return true;
        }

        private static bool ValidateRange(string s)
        {
            string t = s.Trim();
            string[] split = t.Split('-');

            if (split.Length != 2)
            {
                ValidationError();
                return false;
            }

            if (!int.TryParse(split[0], out _) || !int.TryParse(split[1], out _))
            {
                ValidationError();
                return false;
            }

            return true;
        }

        internal static DialogResult ValidationError()
        {
            return MessageBox.Show(
                "The specified values are not valid.", 
                "ST-Sim External Program", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error, 
                MessageBoxDefaultButton.Button1, 
                (MessageBoxOptions)0);
        }

        private void EnableControls()
        {
            this.ButtonClearExe.Enabled = (!string.IsNullOrWhiteSpace(this.TextBoxExe.Text));
            this.ButtonClearScript.Enabled = (!string.IsNullOrWhiteSpace(this.TextBoxScript.Text));
        }

        private void ButtonChooseExe_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog(this) == DialogResult.OK)
            {
                DataSheet ds = this.DataFeed.GetDataSheet(Strings.EXTERNAL_DATASHEET_NAME);
                ds.SetSingleRowData(Strings.EXTERNAL_DATASHEET_EXE_COLUMN_NAME, d.FileName);
                this.EnableControls();
            }
        }

        private void ButtonChooseScript_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog(this) == DialogResult.OK)
            {
                DataSheet ds = this.DataFeed.GetDataSheet(Strings.EXTERNAL_DATASHEET_NAME);
                ds.SetSingleRowData(Strings.EXTERNAL_DATASHEET_SCRIPT_COLUMN_NAME, d.FileName);
                this.EnableControls();
            }
        }

        private void ButtonClearExe_Click(object sender, System.EventArgs e)
        {
            DataSheet ds = this.DataFeed.GetDataSheet(Strings.EXTERNAL_DATASHEET_NAME);
            ds.SetSingleRowData(Strings.EXTERNAL_DATASHEET_EXE_COLUMN_NAME, null);
            this.EnableControls();
        }

        private void ButtonClearScript_Click(object sender, System.EventArgs e)
        {
            DataSheet ds = this.DataFeed.GetDataSheet(Strings.EXTERNAL_DATASHEET_NAME);
            ds.SetSingleRowData(Strings.EXTERNAL_DATASHEET_SCRIPT_COLUMN_NAME, null);
            this.EnableControls();
        }
    }
}
