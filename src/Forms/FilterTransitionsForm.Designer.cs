namespace SyncroSim.STSim
{
    internal partial class FilterTransitionsForm : System.Windows.Forms.Form
    {
        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components = null;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.TransitionGroupsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemCheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUncheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckboxDeterministicTransitions = new System.Windows.Forms.CheckBox();
            this.CheckboxProbabilisticTransitions = new System.Windows.Forms.CheckBox();
            this.CheckBoxPanelMain = new SyncroSim.Common.Forms.CheckBoxPanel();
            this.TransitionGroupsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonOK
            // 
            this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOK.Location = new System.Drawing.Point(537, 517);
            this.ButtonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(100, 28);
            this.ButtonOK.TabIndex = 3;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(645, 517);
            this.ButtonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(100, 28);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // TransitionGroupsContextMenu
            // 
            this.TransitionGroupsContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TransitionGroupsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemCheckSelected,
            this.MenuItemUncheckSelected,
            this.ToolStripSeparator1,
            this.MenuItemCheckAll,
            this.MenuItemUncheckAll});
            this.TransitionGroupsContextMenu.Name = "TransitionGroupsContextMenu";
            this.TransitionGroupsContextMenu.Size = new System.Drawing.Size(195, 106);
            // 
            // MenuItemCheckSelected
            // 
            this.MenuItemCheckSelected.Name = "MenuItemCheckSelected";
            this.MenuItemCheckSelected.Size = new System.Drawing.Size(194, 24);
            this.MenuItemCheckSelected.Text = "Check Selected";
            // 
            // MenuItemUncheckSelected
            // 
            this.MenuItemUncheckSelected.Name = "MenuItemUncheckSelected";
            this.MenuItemUncheckSelected.Size = new System.Drawing.Size(194, 24);
            this.MenuItemUncheckSelected.Text = "Uncheck Selected";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // MenuItemCheckAll
            // 
            this.MenuItemCheckAll.Name = "MenuItemCheckAll";
            this.MenuItemCheckAll.Size = new System.Drawing.Size(194, 24);
            this.MenuItemCheckAll.Text = "Check All";
            // 
            // MenuItemUncheckAll
            // 
            this.MenuItemUncheckAll.Name = "MenuItemUncheckAll";
            this.MenuItemUncheckAll.Size = new System.Drawing.Size(194, 24);
            this.MenuItemUncheckAll.Text = "Uncheck All";
            // 
            // CheckboxDeterministicTransitions
            // 
            this.CheckboxDeterministicTransitions.AutoSize = true;
            this.CheckboxDeterministicTransitions.Location = new System.Drawing.Point(13, 16);
            this.CheckboxDeterministicTransitions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckboxDeterministicTransitions.Name = "CheckboxDeterministicTransitions";
            this.CheckboxDeterministicTransitions.Size = new System.Drawing.Size(180, 21);
            this.CheckboxDeterministicTransitions.TabIndex = 0;
            this.CheckboxDeterministicTransitions.Text = "Deterministic transitions";
            this.CheckboxDeterministicTransitions.UseVisualStyleBackColor = true;
            // 
            // CheckboxProbabilisticTransitions
            // 
            this.CheckboxProbabilisticTransitions.AutoSize = true;
            this.CheckboxProbabilisticTransitions.Location = new System.Drawing.Point(13, 52);
            this.CheckboxProbabilisticTransitions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckboxProbabilisticTransitions.Name = "CheckboxProbabilisticTransitions";
            this.CheckboxProbabilisticTransitions.Size = new System.Drawing.Size(175, 21);
            this.CheckboxProbabilisticTransitions.TabIndex = 1;
            this.CheckboxProbabilisticTransitions.Text = "Probabilistic transitions";
            this.CheckboxProbabilisticTransitions.UseVisualStyleBackColor = true;
            this.CheckboxProbabilisticTransitions.CheckedChanged += new System.EventHandler(this.ProbabilisticTransitionsCheckbox_CheckedChanged);
            // 
            // CheckBoxPanelMain
            // 
            this.CheckBoxPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxPanelMain.IsReadOnly = false;
            this.CheckBoxPanelMain.Location = new System.Drawing.Point(13, 87);
            this.CheckBoxPanelMain.Margin = new System.Windows.Forms.Padding(5);
            this.CheckBoxPanelMain.Name = "CheckBoxPanelMain";
            this.CheckBoxPanelMain.Size = new System.Drawing.Size(730, 422);
            this.CheckBoxPanelMain.TabIndex = 2;
            this.CheckBoxPanelMain.TitleBarText = "Item Names";
            // 
            // FilterTransitionsForm
            // 
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(757, 551);
            this.Controls.Add(this.CheckBoxPanelMain);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.CheckboxDeterministicTransitions);
            this.Controls.Add(this.CheckboxProbabilisticTransitions);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(567, 414);
            this.Name = "FilterTransitionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter Transitions";
            this.TransitionGroupsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.Button ButtonCancel;
        internal System.Windows.Forms.ContextMenuStrip TransitionGroupsContextMenu;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemCheckSelected;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemUncheckSelected;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemCheckAll;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemUncheckAll;
        internal System.Windows.Forms.CheckBox CheckboxDeterministicTransitions;
        internal System.Windows.Forms.CheckBox CheckboxProbabilisticTransitions;
        internal SyncroSim.Common.Forms.CheckBoxPanel CheckBoxPanelMain;
    }
}
