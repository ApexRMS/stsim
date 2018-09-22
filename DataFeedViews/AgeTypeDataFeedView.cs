// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Reflection;
using SyncroSim.Common.Forms;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal partial class AgeTypeDataFeedView
    {
        public AgeTypeDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(this.TextBoxFrequency, Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxMaximum, Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME);

            this.RefreshBoundControls();
            this.AddStandardCommands();
        }

        protected override void OnBoundTextBoxValidated(System.Windows.Forms.TextBox textBox, string columnName, string newValue)
        {
            using (HourGlass h = new HourGlass())
            {
                base.OnBoundTextBoxValidated(textBox, columnName, newValue);
            }
        }
    }
}
