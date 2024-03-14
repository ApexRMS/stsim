// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.Apex.Forms;

namespace SyncroSim.STSim
{
    internal partial class SlopeMultiplierDataFeedView
    {
        public SlopeMultiplierDataFeedView()
        {
            InitializeComponent();
        }

        protected override void InitializeView()
        {
            base.InitializeView();

            DataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);

            this.PanelMultipliers.Controls.Add(v);
        }

        public override void LoadDataFeed(Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            DataFeedView v = (DataFeedView)this.PanelMultipliers.Controls[0];
            v.LoadDataFeed(dataFeed, Strings.DATASHEET_TRANSITION_SLOPE_MULTIPLIER_NAME);
        }

        public override void EnableView(bool enable)
        {
            if (this.PanelMultipliers.Controls.Count > 0)
            {
                DataFeedView v = (DataFeedView)this.PanelMultipliers.Controls[0];
                v.EnableView(enable);
            }

            this.TextBoxDEMFilename.Enabled = enable;

            if (enable)
            {
                this.EnableButtons();
            }
            else
            {
                this.ButtonBrowse.Enabled = false;
                this.ButtonClear.Enabled = false;
            }

            this.LabelDEM.Enabled = enable;
            this.LabelTMV.Enabled = enable;
        }

        public override void RefreshControls()
        {
            base.RefreshControls();

            this.TextBoxDEMFilename.Text = null;

            DataRow dr = this.GetDataRow();

            if (dr == null)
            {
                return;
            }

            this.TextBoxDEMFilename.Text = Convert.ToString(
                dr[Strings.DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME], 
                CultureInfo.InvariantCulture);
        }

        private void EnableButtons()
        {
            this.ButtonBrowse.Enabled = true;
            this.ButtonClear.Enabled = false;

            DataRow dr = this.GetDataRow();

            if (dr == null)
            {
                return;
            }

            if (dr[Strings.DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME] != DBNull.Value)
            {
                this.ButtonBrowse.Enabled = false;
                this.ButtonClear.Enabled = true;
            }
        }

        private DataSheet GetDataSheet()
        {
            if (this.DataFeed != null)
            {
                return this.DataFeed.GetDataSheet(Strings.DATASHEET_DIGITAL_ELEVATION_MODEL_NAME);
            }
            else
            {
                return null;
            }
        }

        private DataRow GetDataRow()
        {
            DataSheet ds = this.GetDataSheet();

            if (ds != null)
            {
                return ds.GetDataRow();
            }
            else
            {
                return null;
            }
        }

        private void ButtonBrowse_Click(object sender, System.EventArgs e)
        {
            string RasterFile = RasterUtilities.ChooseRasterFileName("Digital Elevation Model File", this);

            if (RasterFile == null)
            {
                return;
            }

            using (HourGlass h = new HourGlass())
            {
                DataSheet ds = this.GetDataSheet();
                DataRow dr = ds.GetDataRow();
                string RasterFileName = Path.GetFileName(RasterFile);

                if (dr == null)
                {
                    dr = ds.GetData().NewRow();

                    ds.BeginAddRows();
                    dr[Strings.DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME] = RasterFileName;
                    ds.GetData().Rows.Add(dr);
                    ds.EndAddRows();
                }
                else
                {
                    ds.BeginModifyRows();
                    dr[Strings.DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME] = RasterFileName;
                    ds.EndModifyRows();
                }

                ds.AddExternalInputFile(RasterFile);

                this.RefreshControls();
                this.EnableButtons();
            }
        }

        private void ButtonClear_Click(object sender, System.EventArgs e)
        {
            using (HourGlass h = new HourGlass())
            {
                DataSheet ds = GetDataSheet();

                ds.ClearData();
                this.RefreshControls();
                this.EnableButtons();
            }
        }
    }
}
