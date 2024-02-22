// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.Apex.Forms;

namespace SyncroSim.STSim
{
    internal partial class TransitionDataFeedView
    {
        public TransitionDataFeedView()
        {
            InitializeComponent();
        }

        private ToolTip m_TooltipFirst = new ToolTip();
        private ToolTip m_TooltipPrev = new ToolTip();
        private ToolTip m_TooltipNext = new ToolTip();
        private ToolTip m_TooltipSelect = new ToolTip();
        private ToolTip m_TooltipLast = new ToolTip();
        private ToolTip m_TooltipZoomOut = new ToolTip();
        private ToolTip m_TooltipZoomIn = new ToolTip();
        private TransitionFilterCriteria m_FilterCriteria = new TransitionFilterCriteria();
        private DataSheetMonitor m_Monitor;
        private bool m_ShowTooltips = true;
        private float m_CurrentZoom = 1.0F;
        private bool m_IsLoading;
        private bool m_IsEnabled;
        private bool m_ShowGrid;

        /// <summary>
        /// Overrides InitializeView
        /// </summary>
        protected override void InitializeView()
        {
            base.InitializeView();

            this.InitializeToolTips();
            this.InitializeCommands();

            this.Padding = new Padding(0, 0, 0, 1);

            ButtonZoomIn.Click += new System.EventHandler(ZoomIn);
            ButtonZoomOut.Click += new System.EventHandler(ZoomOut);
            ButtonFirst.Click += new System.EventHandler(ButtonFirst_Click);
            ButtonPrevious.Click += new System.EventHandler(ButtonPrevious_Click);
            ButtonNext.Click += new System.EventHandler(ButtonNext_Click);
            ButtonLast.Click += new System.EventHandler(ButtonLast_Click);
            ButtonSelectStratum.Click += new System.EventHandler(ButtonSelectStratum_Click);
            TabStripMain.SelectedItemChanging += OnSelectedTabItemChanging;
            TabStripMain.SelectedItemChanged += OnSelectedTabItemChanged;
            SplitContainerTabStrip.Paint += new System.Windows.Forms.PaintEventHandler(OnPaintSplitContainer);
            ScrollBarVertical.Scroll += new System.Windows.Forms.ScrollEventHandler(OnVerticalScroll);
            ScrollBarHorizontal.Scroll += new System.Windows.Forms.ScrollEventHandler(OnHorizontalScroll);

            this.m_Monitor = new DataSheetMonitor(this.Project, Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged);
            this.m_Monitor.Invoke();
        }

        /// <summary>
        /// Overrides Dispose
        /// </summary>
        /// <param name="disposing"></param>
        /// <remarks></remarks>
        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                this.DisposeToolTips();
                this.RemoveDiagramHandlers();
                this.DisposeTabStripItems();
                this.m_Monitor.Dispose();

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Overrides LoadDataFeed
        /// </summary>
        /// <param name="dataFeed"></param>
        /// <remarks>We completely refresh the entire control when the data feed changes</remarks>
        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.m_IsLoading = true;

            this.SynchronizeFilterCriteria();
            this.RefreshTabStripControls();

            this.m_IsLoading = false;
        }

        /// <summary>
        /// Overrides EnableView
        /// </summary>
        /// <param name="enable"></param>
        /// <remarks>We need to forward this to all diagrams and views since they are custom</remarks>
        public override void EnableView(bool enable)
        {
            this.m_IsEnabled = enable;

            foreach (TransitionDiagramTabStripItem Item in this.TabStripMain.Items)
            {
                if (Item.Control == null)
                {
                    continue;
                }

                if ((Item) is StratumTabStripItem)
                {
                    TransitionDiagram d = (TransitionDiagram)Item.Control;
                    d.IsReadOnly = (!this.m_IsEnabled);

                    d.ApplyReadonlySettings();
                }
                else
                {
                    DataFeedView v = (DataFeedView)Item.Control;
                    v.EnableView(this.m_IsEnabled);
                }
            }
        }

        /// <summary>
        /// Called when rows have been added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);
            this.HandleExternalRecordEvent(sender, e);
        }

        /// <summary>
        /// Called when the rows have been deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected override void OnRowsDeleted(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsDeleted(sender, e);
            this.HandleExternalRecordEvent(sender, e);
        }

        /// <summary>
        /// Called when the rows have been modified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected override void OnRowsModified(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsModified(sender, e);
            this.HandleExternalRecordEvent(sender, e);
        }

        /// <summary>
        /// Overrides OnResize
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            this.ResetScrollbars();
        }

        /// <summary>
        /// A callback for when the terminology changes
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            this.RefreshSelectStratumTooltip();
        }

        /// <summary>
        /// Refreshes the Select Stratum tooltip with the correct terminology
        /// </summary>
        /// <remarks></remarks>
        private void RefreshSelectStratumTooltip()
        {
            this.m_TooltipSelect.Dispose();
            this.m_TooltipSelect = new ToolTip();

            string primary = null;
            string secondary = null;
            string tertiary = null;
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetStratumLabelTerminology(ds, ref primary, ref secondary, ref tertiary);
            this.m_TooltipSelect.SetToolTip(this.ButtonSelectStratum, "Select " + primary);
        }

        /// <summary>
        /// Disposes all tab strip items
        /// </summary>
        /// <remarks></remarks>
        private void DisposeTabStripItems()
        {
            foreach (TabStripItem item in this.TabStripMain.Items)
            {
                item.Dispose();
            }
        }

        /// <summary>
        /// Initializes the tooltips for the command bar buttons
        /// </summary>
        /// <remarks></remarks>
        private void InitializeToolTips()
        {
            this.m_TooltipFirst.SetToolTip(this.ButtonFirst, "First Item");
            this.m_TooltipPrev.SetToolTip(this.ButtonPrevious, "Previous Item");
            this.m_TooltipNext.SetToolTip(this.ButtonNext, "Next Item");
            this.m_TooltipLast.SetToolTip(this.ButtonLast, "Last Item");
            this.m_TooltipZoomOut.SetToolTip(this.ButtonZoomOut, "Zoom Out");
            this.m_TooltipZoomIn.SetToolTip(this.ButtonZoomIn, "Zoom In");

            this.RefreshSelectStratumTooltip();
        }

        /// <summary>
        /// Disposes the command bar tooltips
        /// </summary>
        /// <remarks></remarks>
        private void DisposeToolTips()
        {
            this.m_TooltipFirst.Dispose();
            this.m_TooltipPrev.Dispose();
            this.m_TooltipSelect.Dispose();
            this.m_TooltipNext.Dispose();
            this.m_TooltipLast.Dispose();
            this.m_TooltipZoomOut.Dispose();
            this.m_TooltipZoomIn.Dispose();
        }

        /// <summary>
        /// Initializes the command collection
        /// </summary>
        /// <remarks></remarks>
        private void InitializeCommands()
        {
            Command CmdOpen = new Command("stsim_open_state_classes", Strings.COMMAND_STRING_OPEN, null, OnExecuteOpenCommand, OnUpdateOpenCommand);
            CmdOpen.IsBold = true;
            this.Commands.Add(CmdOpen);

            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("ssim_cut", Strings.COMMAND_STRING_CUT, Properties.Resources.Cut16x16, OnExecuteCutCommand, OnUpdateCutCommand));
            this.Commands.Add(new Command("ssim_copy", Strings.COMMAND_STRING_COPY, Properties.Resources.Copy16x16, OnExecuteCopyCommand, OnUpdateCopyCommand));
            this.Commands.Add(new Command("ssim_paste", Strings.COMMAND_STRING_PASTE, Properties.Resources.Paste16x16, OnExecutePasteCommand, OnUpdatePasteCommand));
            this.Commands.Add(new Command("stsim_paste_state_classes_special", Strings.COMMAND_STRING_PASTE_SPECIAL, OnExecutePasteSpecialCommand, OnUpdatePasteSpecialCommand));
            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("ssim_delete", Strings.COMMAND_STRING_DELETE, Properties.Resources.Delete16x16, OnExecuteDeleteCommand, OnUpdateDeleteCommand));
            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("ssim_select_all", Strings.COMMAND_STRING_SELECT_ALL, OnExecuteSelectAllCommand, OnUpdateSelectAllCommand));
            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("stsim_show_grid", "Show Grid", OnExecuteShowGridCommand, OnUpdateShowGridCommand));
            this.Commands.Add(new Command("stsim_show_tooltips", "Show Tooltips", OnExecuteShowTooltipsCommand, OnUpdateShowTooltipsCommand));
            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("stsim_add_state_class", "Add State Class...", OnExecuteAddStateClassCommand, OnUpdateAddStateClassCommand));
            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("stsim_edit_state_class", "Edit State Class...", OnExecuteEditStateClassCommand, OnUpdateEditStateClassCommand));
            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("stsim_filter_transitions", "Filter Transitions...", Properties.Resources.Filter16x16, OnExecuteFilterTransitionsCommand, OnUpdateFilterTransitionsCommand));
            this.Commands.Add(Command.CreateSeparatorCommand());
            this.Commands.Add(new Command("stsim_select_stratum", "Select Stratum...", Properties.Resources.Search16x16, OnExecuteSelectStratumCommand, OnUpdateSelectStratumCommand));
        }

        /// <summary>
        /// Removes any existing diagram handlers
        /// </summary>
        /// <remarks></remarks>
        private void RemoveDiagramHandlers()
        {
            foreach (TransitionDiagramTabStripItem Item in this.TabStripMain.Items)
            {
                if ((Item) is StratumTabStripItem)
                {
                    if (Item.Control != null)
                    {
                        BoxArrowDiagram d = (BoxArrowDiagram)Item.Control;

                        d.ZoomChanged -= OnDiagramZoomChanged;
                        d.MouseDoubleClick -= OnDiagramMouseDoubleClick;
                    }
                }
            }
        }

        /// <summary>
        /// Synchronizes the transition group criteria
        /// </summary>
        /// <remarks></remarks>
        private void SynchronizeFilterCriteria()
        {
            TransitionFilterCriteria cr = new TransitionFilterCriteria();
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME);

            cr.IncludeDeterministic = this.m_FilterCriteria.IncludeDeterministic;
            cr.IncludeProbabilistic = this.m_FilterCriteria.IncludeProbabilistic;

            foreach (DataRow dr in ds.GetData().Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    cr.TransitionGroups.Add(Convert.ToInt32(dr[ds.ValueMember], CultureInfo.InvariantCulture), true);
                }
            }

            foreach (int tg in this.m_FilterCriteria.TransitionGroups.Keys)
            {
                if (cr.TransitionGroups.ContainsKey(tg))
                {
                    cr.TransitionGroups[tg] = this.m_FilterCriteria.TransitionGroups[tg];
                }
            }

            this.m_FilterCriteria = cr;
        }

        /// <summary>
        /// Handles an external record event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void HandleExternalRecordEvent(object sender, DataSheetRowEventArgs e)
        {
            if (IsDataSheetEvent(sender, Strings.DATASHEET_STRATA_NAME))
            {
                this.SynchronizeFilterCriteria();
                this.RefreshTabStripControls();
            }
            else if (
                IsDataSheetEvent(sender, Strings.DATASHEET_TRANSITION_GROUP_NAME) || 
                IsDataSheetEvent(sender, Strings.DATASHEET_TRANSITION_TYPE_NAME) || 
                IsDataSheetEvent(sender, Strings.DATASHEET_TRANSITION_TYPE_GROUP_NAME))
            {
                this.SynchronizeFilterCriteria();
                this.RefreshTransitionDiagrams();
            }
            else if (
                IsDataSheetEvent(sender, Strings.DATASHEET_STATECLASS_NAME) || 
                IsDataSheetEvent(sender, Strings.DATASHEET_STATE_LABEL_X_NAME) || 
                IsDataSheetEvent(sender, Strings.DATASHEET_STATE_LABEL_Y_NAME))
            {
                this.RefreshTransitionDiagrams();
            }
            else if (
                IsDataSheetEvent(sender, Strings.DATASHEET_DT_NAME) || 
                IsDataSheetEvent(sender, Strings.DATASHEET_PT_NAME))
            {
                this.RefreshTransitionDiagrams();
            }
        }

        /// <summary>
        /// Determines if the specfied sender is a datasheet with the specified data feed name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataFeedName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static bool IsDataSheetEvent(object sender, string dataFeedName)
        {
            if ((sender) is DataSheet)
            {
                if (((DataSheet)sender).Name == dataFeedName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the current diagram
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private TransitionDiagram GetCurrentDiagram()
        {
            StratumTabStripItem Item = (StratumTabStripItem)this.TabStripMain.SelectedItem;
            return (TransitionDiagram)Item.Control;
        }

        /// <summary>
        /// Determines if the current tab strip item is a diagram item
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CurrentItemIsDiagramItem()
        {
            if (this.TabStripMain.SelectedItem != null)
            {
                if ((this.TabStripMain.SelectedItem) is StratumTabStripItem)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if it is possible to open state classes
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CanOpenStateClasses()
        {
            if (this.CurrentItemIsDiagramItem())
            {
                TransitionDiagram d = this.GetCurrentDiagram();

                if (d.CanOpenStateClasses())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if it is possible to delete state classes
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CanDeleteStateClasses()
        {
            if (this.CurrentItemIsDiagramItem())
            {
                TransitionDiagram d = this.GetCurrentDiagram();

                if (d.CanCutStateClasses())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if it is possible to paste state classes
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool CanPasteStateClasses()
        {
            if (this.CurrentItemIsDiagramItem())
            {
                TransitionDiagram d = this.GetCurrentDiagram();

                if (d.CanPasteStateClasses())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Executes the Open command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteOpenCommand(Command cmd)
        {
            this.OpenSelectedStateClasses();
        }

        /// <summary>
        /// Updates the Open command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateOpenCommand(Command cmd)
        {
            cmd.IsEnabled = this.CanOpenStateClasses();
        }

        /// <summary>
        /// Executes the Cut command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteCutCommand(Command cmd)
        {
            using (HourGlass h = new HourGlass())
            {
                this.GetCurrentDiagram().CutStateClasses();
            }
        }

        /// <summary>
        /// Updates the Cut command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateCutCommand(Command cmd)
        {
            cmd.IsEnabled = this.CanDeleteStateClasses();
        }

        /// <summary>
        /// Executes the Copy command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteCopyCommand(Command cmd)
        {
            using (HourGlass h = new HourGlass())
            {
                this.GetCurrentDiagram().CopyStateClasses();
            }
        }

        /// <summary>
        /// Updates the Copy command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateCopyCommand(Command cmd)
        {
            if (this.CurrentItemIsDiagramItem())
            {
                TransitionDiagram d = this.GetCurrentDiagram();

                if (d.CanCopyStateClasses())
                {
                    cmd.IsEnabled = true;
                    return;
                }
            }

            cmd.IsEnabled = false;
        }

        /// <summary>
        /// Executes the Paste command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecutePasteCommand(Command cmd)
        {
            using (HourGlass h = new HourGlass())
            {
                this.GetCurrentDiagram().PasteStateClasses(!cmd.IsRouted);
            }
        }

        /// <summary>
        /// Updates the Paste command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdatePasteCommand(Command cmd)
        {
            cmd.IsEnabled = this.CanPasteStateClasses();
        }

        /// <summary>
        /// Executes the Paste Special command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecutePasteSpecialCommand(Command cmd)
        {
            TransitionDiagramPasteSpecialForm dlg = new TransitionDiagramPasteSpecialForm();

            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            using (HourGlass h = new HourGlass())
            {
                this.GetCurrentDiagram().PasteStateClassesSpecial(
                    dlg.PasteTransitionsAll, 
                    dlg.PasteTransitionsBetween, 
                    dlg.PasteTransitionsNone, 
                    dlg.PasteTransitionsDeterministic, 
                    dlg.PasteTransitionsProbabilistic, 
                    cmd.IsRouted);
            }
        }

        /// <summary>
        /// Updates the Paste Special command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdatePasteSpecialCommand(Command cmd)
        {
            cmd.IsEnabled = this.CanPasteStateClasses();
        }

        /// <summary>
        /// Executes the Delete command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteDeleteCommand(Command cmd)
        {
            if (FormsUtilities.ApplicationMessageBox(MessageStrings.CONFIRM_DIAGRAM_DELETE, MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            using (HourGlass h = new HourGlass())
            {
                this.GetCurrentDiagram().DeleteStateClasses();
            }
        }

        /// <summary>
        /// Updates the Delete command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateDeleteCommand(Command cmd)
        {
            cmd.IsEnabled = this.CanDeleteStateClasses();
        }

        /// <summary>
        /// Executes the Select All command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteSelectAllCommand(Command cmd)
        {
            this.GetCurrentDiagram().SelectAllStateClasses();
        }

        /// <summary>
        /// Updates the Select All command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateSelectAllCommand(Command cmd)
        {
            if (this.CurrentItemIsDiagramItem())
            {
                TransitionDiagram d = this.GetCurrentDiagram();

                if (d.CanSelectAllStateClasses())
                {
                    cmd.IsEnabled = true;
                    return;
                }
            }

            cmd.IsEnabled = false;
        }

        /// <summary>
        /// Executes the Show Grid command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteShowGridCommand(Command cmd)
        {
            this.m_ShowGrid = !this.m_ShowGrid;

            foreach (TransitionDiagramTabStripItem Item in this.TabStripMain.Items)
            {
                if ((Item) is StratumTabStripItem && Item.Control != null)
                {
                    ((TransitionDiagram)Item.Control).ShowGrid = this.m_ShowGrid;
                }
            }
        }

        /// <summary>
        /// Updates the Show Grid command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateShowGridCommand(Command cmd)
        {
            cmd.IsEnabled = this.CurrentItemIsDiagramItem();
            cmd.IsChecked = (this.m_ShowGrid);
        }

        /// <summary>
        /// Executes the Show Tooltips command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteShowTooltipsCommand(Command cmd)
        {
            this.m_ShowTooltips = !this.m_ShowTooltips;

            foreach (TransitionDiagramTabStripItem Item in this.TabStripMain.Items)
            {
                if ((Item) is StratumTabStripItem && Item.Control != null)
                {
                    ((TransitionDiagram)Item.Control).ShowToolTips = this.m_ShowTooltips;
                }
            }
        }

        /// <summary>
        /// Updates the Show Tooltips command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateShowTooltipsCommand(Command cmd)
        {
            cmd.IsEnabled = this.CurrentItemIsDiagramItem();
            cmd.IsChecked = (cmd.IsEnabled && this.GetCurrentDiagram().ShowToolTips);
        }

        /// <summary>
        /// Executes the Add State Class command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteAddStateClassCommand(Command cmd)
        {
            this.GetCurrentDiagram().AddStateClass();
        }

        /// <summary>
        /// Updates the Add State Class command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateAddStateClassCommand(Command cmd)
        {
            if (this.CurrentItemIsDiagramItem())
            {
                TransitionDiagram d = this.GetCurrentDiagram();

                if (!d.IsReadOnly)
                {
                    cmd.IsEnabled = true;
                    return;
                }
            }

            cmd.IsEnabled = false;
        }

        /// <summary>
        /// Executes the Edit State Class command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteEditStateClassCommand(Command cmd)
        {
            this.GetCurrentDiagram().EditStateClass();
        }

        /// <summary>
        /// Updates the Edit State Class command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateEditStateClassCommand(Command cmd)
        {
            if (this.CurrentItemIsDiagramItem())
            {
                TransitionDiagram d = this.GetCurrentDiagram();

                if (d.IsReadOnly)
                {
                    cmd.IsEnabled = false;
                }
                else if (d.SelectedShapes.Count() != 1)
                {
                    cmd.IsEnabled = false;
                }
                else
                {
                    cmd.IsEnabled = true;
                }
            }
            else
            {
                cmd.IsEnabled = false;
            }
        }

        /// <summary>
        /// Executes the Filter Transitions command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteFilterTransitionsCommand(Command cmd)
        {
            FilterTransitionsForm dlg = new FilterTransitionsForm();
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME);

            dlg.CheckBoxPanelMain.Initialize();
            dlg.CheckBoxPanelMain.BeginAddItems();

            foreach (DataRow dr in ds.GetData().Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    int Id = Convert.ToInt32(dr[ds.ValueMember], CultureInfo.InvariantCulture);
                    string Name = Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture);
                    bool IsSelected = this.m_FilterCriteria.TransitionGroups[Id];

                    dlg.CheckBoxPanelMain.AddItem(IsSelected, Id, Name);
                }
            }

            dlg.CheckBoxPanelMain.EndAddItems();
            dlg.CheckBoxPanelMain.TitleBarText = "Transition Groups";
            dlg.CheckboxDeterministicTransitions.Checked = this.m_FilterCriteria.IncludeDeterministic;
            dlg.CheckboxProbabilisticTransitions.Checked = this.m_FilterCriteria.IncludeProbabilistic;
            dlg.CheckBoxPanelMain.IsReadOnly = (!this.m_FilterCriteria.IncludeProbabilistic);

            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            this.m_FilterCriteria.IncludeDeterministic = dlg.CheckboxDeterministicTransitions.Checked;
            this.m_FilterCriteria.IncludeProbabilistic = dlg.CheckboxProbabilisticTransitions.Checked;

            foreach (DataRow dr in dlg.CheckBoxPanelMain.DataSource.Rows)
            {
                this.m_FilterCriteria.TransitionGroups[
                    Convert.ToInt32(dr["ItemID"], CultureInfo.InvariantCulture)] = 
                    Convert.ToBoolean(dr["IsSelected"], CultureInfo.InvariantCulture);
            }

            foreach (TransitionDiagramTabStripItem Item in this.TabStripMain.Items)
            {
                if ((Item) is StratumTabStripItem && Item.Control != null)
                {
                    StratumTabStripItem i = (StratumTabStripItem)Item;
                    TransitionDiagram d = (TransitionDiagram)i.Control;

                    d.RefreshDiagram();
                    d.FilterTransitions(this.m_FilterCriteria);
                }
            }
        }

        /// <summary>
        /// Updates the Filter Transitions command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateFilterTransitionsCommand(Command cmd)
        {
            if (this.CurrentItemIsDiagramItem())
            {
                if (this.GetCurrentDiagram().Shapes.Count() > 0)
                {
                    cmd.IsEnabled = true;
                    return;
                }
            }

            cmd.IsEnabled = false;
        }

        /// <summary>
        /// Updates the Select Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnUpdateSelectStratumCommand(Command cmd)
        {
            if (this.TabStripMain.Items.Count == 0)
            {
                cmd.IsEnabled = false;
            }

            cmd.IsEnabled = true;
        }

        /// <summary>
        /// Executes the Select Stratum command
        /// </summary>
        /// <param name="cmd"></param>
        /// <remarks></remarks>
        private void OnExecuteSelectStratumCommand(Command cmd)
        {
            this.SelectStratum();
        }

        /// <summary>
        /// Allows the user to select a specific stratum
        /// </summary>
        /// <remarks></remarks>
        private void SelectStratum()
        {
            SelectStratumForm frm = new SelectStratumForm();
            frm.Initialize(this.Project, this.TabStripMain.SelectedItem.Text);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                foreach (TabStripItem item in this.TabStripMain.Items)
                {
                    if (item.Text == frm.SelectedStratum)
                    {
                        this.TabStripMain.SelectItem(item);
                        break;
                    }
                }
            }

            frm.Dispose();
        }

        /// <summary>
        /// Zooms the current diagram in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ZoomIn(object sender, System.EventArgs e)
        {
            this.GetCurrentDiagram().ZoomIn();
        }

        /// <summary>
        /// Zoooms the current diagram out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ZoomOut(object sender, System.EventArgs e)
        {
            this.GetCurrentDiagram().ZoomOut();
        }

        /// <summary>
        /// Handles the First button Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ButtonFirst_Click(object sender, System.EventArgs e)
        {
            this.TabStripMain.SelectFirstItem();
        }

        /// <summary>
        /// Handles the Previous button Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ButtonPrevious_Click(object sender, System.EventArgs e)
        {
            this.TabStripMain.SelectPreviousItem();
        }

        /// <summary>
        /// Handles the Next button Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ButtonNext_Click(object sender, System.EventArgs e)
        {
            this.TabStripMain.SelectNextItem();
        }

        /// <summary>
        /// Handles the Last button Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ButtonLast_Click(object sender, System.EventArgs e)
        {
            this.TabStripMain.SelectLastItem();
        }

        /// <summary>
        /// Handles the Search button Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void ButtonSelectStratum_Click(object sender, System.EventArgs e)
        {
            this.SelectStratum();
        }

        /// <summary>
        /// Handles the selected tab item changing event
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSelectedTabItemChanging(object sender, SelectedTabStripItemChangingEventArgs e)
        {
            //The first time we get this event there will be nothing to validate
            if (this.TabStripMain.SelectedItem == null || (this.PanelControlHost.Controls.Count == 0))
            {
                return;
            }

            TransitionDiagramTabStripItem item = (TransitionDiagramTabStripItem)this.TabStripMain.SelectedItem;

            if (!((item) is StratumTabStripItem))
            {
                SyncroSimView v = (SyncroSimView)this.PanelControlHost.Controls[0];

                if (!v.Validate())
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Handles the selected tab item changed event
        /// </summary>
        /// <remarks>If we are loading the cursor is already the hourglass and we don't want to change that here...</remarks>
        private void OnSelectedTabItemChanged(object sender, SelectedTabStripItemChangedEventArgs e)
        {
            if (!this.m_IsLoading)
            {
                using (HourGlass h = new HourGlass())
                {
                    this.OnSelectedTabItemChanged();
                }
            }
            else
            {
                this.OnSelectedTabItemChanged();
            }
        }

        /// <summary>
        /// Handles the selected tab item changed event
        /// </summary>
        /// <remarks></remarks>
        private void OnSelectedTabItemChanged()
        {
            TransitionDiagramTabStripItem item = (TransitionDiagramTabStripItem)this.TabStripMain.SelectedItem;

            if ((item) is StratumTabStripItem)
            {
                this.ActivateStratumTabStripItem((StratumTabStripItem)item);
            }
            else
            {
                this.ActivateTransitionsTabStripItem(item);
            }

            this.SetCurrentControl(item.Control);
        }

        /// <summary>
        /// Paints the split container
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>We custom paint the splitter rectangle to make it easier to see</remarks>
        private void OnPaintSplitContainer(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.SplitContainerTabStrip.SplitterRectangle.Left, this.SplitContainerTabStrip.SplitterRectangle.Top, this.SplitContainerTabStrip.SplitterRectangle.Width - 1, this.SplitContainerTabStrip.SplitterRectangle.Height - 1);
            System.Drawing.Drawing2D.LinearGradientBrush brs = new System.Drawing.Drawing2D.LinearGradientBrush(rc, Color.SteelBlue, Color.White, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(brs, rc);
            e.Graphics.DrawRectangle(Pens.SteelBlue, rc);
            rc.Inflate(-1, -1);
            e.Graphics.DrawRectangle(Pens.White, rc);

            brs.Dispose();
        }

        /// <summary>
        /// Handles the scroll event for the vertical scroll bar
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnVerticalScroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            TransitionDiagramTabStripItem item = (TransitionDiagramTabStripItem)this.TabStripMain.SelectedItem;

            if ((item) is StratumTabStripItem)
            {
                Diagram d = (Diagram)item.Control;

                d.VerticalScrollValue = e.NewValue;
                d.Invalidate();
            }
        }

        /// <summary>
        /// Handles the scroll event for the horizontal scroll bar
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnHorizontalScroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            TransitionDiagramTabStripItem item = (TransitionDiagramTabStripItem)this.TabStripMain.SelectedItem;

            if ((item) is StratumTabStripItem)
            {
                Diagram d = (Diagram)item.Control;

                d.HorizontalScrollValue = e.NewValue;
                d.Invalidate();
            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnDiagramMouseDoubleClick(object sender, MouseEventArgs e)
        {
            TransitionDiagramTabStripItem item = (TransitionDiagramTabStripItem)this.TabStripMain.SelectedItem;

            if ((item) is StratumTabStripItem)
            {
                TransitionDiagram d = (TransitionDiagram)item.Control;

                if (d.CanOpenStateClasses())
                {
                    this.OpenSelectedStateClasses();
                }
            }
        }

        /// <summary>
        /// Changes the diagram zoom for the specified diagram
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="zoom"></param>
        /// <remarks></remarks>
        private void ChangeDiagramZoom(BoxArrowDiagram diagram, float zoom)
        {
            if (zoom == diagram.MinimumZoom || zoom == diagram.MaximumZoom)
            {
                return;
            }

            float hnormal = diagram.HorizontalScrollValue / this.m_CurrentZoom;
            float vnormal = diagram.VerticalScrollValue / this.m_CurrentZoom;

            diagram.HorizontalScrollValue = Convert.ToInt32(hnormal * zoom);
            diagram.VerticalScrollValue = Convert.ToInt32(vnormal * zoom);
        }

        /// <summary>
        /// Changes the zoom for the diagram contained in the specified tab strip item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="zoom"></param>
        /// <remarks></remarks>
        private void ChangeDiagramZoom(StratumTabStripItem item, float zoom)
        {
            if (item.Control == null)
            {
                return;
            }

            StratumTabStripItem i = (StratumTabStripItem)item;
            BoxArrowDiagram d = (BoxArrowDiagram)i.Control;

            this.ChangeDiagramZoom(d, zoom);
        }

        /// <summary>
        /// Handles the zoom changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// We need to handle this event here so we can synchronize zoom states across all diagrams
        /// </remarks>
        private void OnDiagramZoomChanged(object sender, EventArgs e)
        {
            BoxArrowDiagram CurDiag = this.GetCurrentDiagram();

            foreach (TabStripItem Item in this.TabStripMain.Items)
            {
                if ((Item) is StratumTabStripItem)
                {
                    ChangeDiagramZoom((StratumTabStripItem)Item, CurDiag.Zoom);
                }
            }

            this.m_CurrentZoom = CurDiag.Zoom;
            this.ResetScrollbars();
        }

        /// <summary>
        /// Gets a tab strip item with the specified name
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private TabStripItem GetItemByName(string itemName)
        {
            foreach (TabStripItem item in this.TabStripMain.Items)
            {
                if (item.Text == itemName)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the tab for the first stratum with data
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private TabStripItem GetFirstStratumTabWithData()
        {
            DTAnalyzer Analyzer = new DTAnalyzer(this.Scenario.GetDataSheet(Strings.DATASHEET_DT_NAME).GetData(), this.Project);

            foreach (var item in this.TabStripMain.Items)
            {
                if ((item) is StratumTabStripItem)
                {
                    if (Analyzer.StratumHasData(((StratumTabStripItem)item).StratumId))
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Refreshes the tab strip controls
        /// </summary>
        /// <remarks></remarks>
        private void RefreshTabStripControls()
        {
            TabStripItem NewSelectedItem = null;
            TabStripItem OldSelectedItem = this.TabStripMain.SelectedItem;

            this.RefreshAllTabItems();

            if (OldSelectedItem != null)
            {
                NewSelectedItem = this.GetItemByName(OldSelectedItem.Text);
            }

            if (NewSelectedItem == null)
            {
                NewSelectedItem = this.GetFirstStratumTabWithData();
            }

            if (NewSelectedItem == null)
            {
                if (this.TabStripMain.Items.Count > 0)
                {
                    NewSelectedItem = this.TabStripMain.Items[0];
                }
            }

            if (NewSelectedItem != null)
            {
                this.TabStripMain.SelectItem(NewSelectedItem);
            }
        }

        /// <summary>
        /// Set the current control into the content panel
        /// </summary>
        /// <param name="c"></param>
        /// <remarks></remarks>
        private void SetCurrentControl(Control c)
        {
            this.PanelControlHost.Controls.Clear();
            this.PanelControlHost.Controls.Add(c);

            c.Dock = DockStyle.Fill;
            c.Parent = this.PanelControlHost;
        }

        /// <summary>
        /// Loads the transition diagram tab items
        /// </summary>
        /// <remarks></remarks>
        private void RefreshAllTabItems()
        {
            this.DisposeTabStripItems();
            this.TabStripMain.Items.Clear();

            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
            DataView dv = new DataView(ds.GetData(), null, ds.DisplayMember, DataViewRowState.CurrentRows);

            this.TabStripMain.BeginAddItems();

            this.TabStripMain.Items.Add(new StratumTabStripItem(Strings.DIAGRAM_ALL_STRATA_DISPLAY_NAME, null));

            foreach (DataRowView v in dv)
            {
                DataRow dr = v.Row;

                this.TabStripMain.Items.Add(
                    new StratumTabStripItem(
                        Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture), 
                        Convert.ToInt32(dr[ds.ValueMember], CultureInfo.InvariantCulture)));
            }

            this.TabStripMain.Items.Add(new DeterministicTransitionsTabStripItem());
            this.TabStripMain.Items.Add(new ProbabilisticTransitionsTabStripItem());

            this.TabStripMain.EndAddItems();
        }

        /// <summary>
        /// Refreshes all transition diagrams
        /// </summary>
        /// <remarks>
        /// Not all diagrams are actually refreshed.  All of them except for the selected diagram (if there is one) are only
        /// queued for a refresh which will happen if the user clicks that diagram's tab.  This is done because loading a diagram
        /// can be slow and there is no need to load one until the user wants to see it.
        /// </remarks>
        private void RefreshTransitionDiagrams()
        {
            foreach (TransitionDiagramTabStripItem item in this.TabStripMain.Items)
            {
                if ((item) is StratumTabStripItem)
                {
                    item.RefreshRequired = true;
                }
            }

            if ((this.TabStripMain.SelectedItem) is StratumTabStripItem)
            {
                this.OnSelectedTabItemChanged(this, null);
            }
        }

        /// <summary>
        /// Activates the specified stratum tab strip item
        /// </summary>
        /// <param name="item"></param>
        /// <remarks></remarks>
        private void ActivateStratumTabStripItem(StratumTabStripItem item)
        {
            if (item.RefreshRequired)
            {
                TransitionDiagram d = null;

                if (item.Control == null)
                {
                    d = new TransitionDiagram(item.StratumId, this.DataFeed);

                    d.ZoomChanged += OnDiagramZoomChanged;
                    d.MouseDoubleClick += OnDiagramMouseDoubleClick;

                    item.Control = d;
                }
                else
                {
                    d = (TransitionDiagram)item.Control;
                }

                d.IsReadOnly = (!this.m_IsEnabled);
                d.ShowGrid = this.m_ShowGrid;
                d.ShowToolTips = this.m_ShowTooltips;

                d.RefreshDiagram();
                d.FilterTransitions(this.m_FilterCriteria);
                ChangeDiagramZoom(d, this.m_CurrentZoom);

                item.RefreshRequired = false;
            }

            this.ButtonZoomIn.Enabled = true;
            this.ButtonZoomOut.Enabled = true;
            this.ScrollBarVertical.Visible = true;
            this.ScrollBarHorizontal.Enabled = true;
            this.ScrollBarVertical.Value = ((TransitionDiagram)item.Control).VerticalScrollValue;
            this.ScrollBarHorizontal.Value = ((TransitionDiagram)item.Control).HorizontalScrollValue;
            this.PanelControlHost.Width = this.PanelBottomControls.Width - this.ScrollBarVertical.Width - 2;

            this.ResetScrollbars();
        }

        /// <summary>
        /// Activates the specified transitions tab strip item
        /// </summary>
        /// <param name="item"></param>
        /// <remarks></remarks>
        private void ActivateTransitionsTabStripItem(TransitionDiagramTabStripItem item)
        {
            if (item.RefreshRequired)
            {
                MultiRowDataFeedView v = null;

                if (item.Control == null)
                {
                    v = this.Session.CreateMultiRowDataFeedView(this.DataFeed.Scenario, this.ControllingScenario);

                    if ((item) is DeterministicTransitionsTabStripItem)
                    {
                        v.LoadDataFeed(this.DataFeed, Strings.DATASHEET_DT_NAME);
                    }
                    else
                    {
                        v.LoadDataFeed(this.DataFeed, Strings.DATASHEET_PT_NAME);
                    }

                    item.Control = v;
                }
                else
                {
                    v = (MultiRowDataFeedView)item.Control;
                }

                v.EnableView(this.m_IsEnabled);
                item.RefreshRequired = false;
            }

            this.ButtonZoomIn.Enabled = false;
            this.ButtonZoomOut.Enabled = false;
            this.PanelControlHost.Width = this.PanelBottomControls.Width;
            this.ScrollBarVertical.Visible = false;
            this.ScrollBarHorizontal.Enabled = false;
        }

        /// <summary>
        /// Resets the scroll bars for the current tab strip item
        /// </summary>
        /// <remarks></remarks>
        private void ResetScrollbars()
        {
            if (this.TabStripMain.SelectedItem == null)
            {
                return;
            }

            TransitionDiagramTabStripItem Item = (TransitionDiagramTabStripItem)this.TabStripMain.SelectedItem;

            if (Item.Control == null)
            {
                return;
            }

            if (!((Item) is StratumTabStripItem))
            {
                return;
            }

            BoxArrowDiagram diag = (BoxArrowDiagram)Item.Control;

            this.ResetHorizontalScrollbar(diag);
            this.ResetVerticalScrollbar(diag);
        }

        /// <summary>
        /// Resets the horizontal scroll bar
        /// </summary>
        /// <param name="diag"></param>
        /// <remarks></remarks>
        private void ResetHorizontalScrollbar(BoxArrowDiagram diag)
        {
            float zoom = diag.Zoom;
            float diagwid = diag.WorkspaceRectangle.Width * zoom;
            float clientwid = diag.ClientSize.Width;
            int Extra = Convert.ToInt32(30 * zoom);

            if (clientwid >= diagwid)
            {
                this.ScrollBarHorizontal.Enabled = false;
                this.ScrollBarHorizontal.Value = 0;
                diag.HorizontalScrollValue = 0;
            }
            else
            {
                this.ScrollBarHorizontal.Enabled = true;

                this.ScrollBarHorizontal.Minimum = 0;
                this.ScrollBarHorizontal.Maximum = Convert.ToInt32(diagwid - clientwid + Extra);
                this.ScrollBarHorizontal.SmallChange = Convert.ToInt32(diag.CellSize * zoom);
                this.ScrollBarHorizontal.LargeChange = Convert.ToInt32(diag.CellSize * zoom);

                if (this.ScrollBarHorizontal.SmallChange == 0)
                {
                    this.ScrollBarHorizontal.SmallChange = 1;
                }

                if (this.ScrollBarHorizontal.LargeChange == 0)
                {
                    this.ScrollBarHorizontal.LargeChange = 1;
                }

                this.ScrollBarHorizontal.Maximum += Convert.ToInt32(this.ScrollBarHorizontal.LargeChange);

                if (diag.HorizontalScrollValue <= ScrollBarHorizontal.Maximum)
                {
                    diag.HorizontalScrollValue = diag.HorizontalScrollValue;
                }
            }
        }

        /// <summary>
        /// Resets the vertical scroll bar
        /// </summary>
        /// <param name="diag"></param>
        /// <remarks></remarks>
        private void ResetVerticalScrollbar(BoxArrowDiagram diag)
        {
            float zoom = diag.Zoom;
            float diaghgt = diag.WorkspaceRectangle.Height * zoom;
            float clienthgt = diag.ClientSize.Height;
            int Extra = Convert.ToInt32(30 * zoom);

            if (clienthgt >= diaghgt)
            {
                this.ScrollBarVertical.Enabled = false;
                this.ScrollBarVertical.Value = 0;
                diag.VerticalScrollValue = 0;
            }
            else
            {
                this.ScrollBarVertical.Enabled = true;

                this.ScrollBarVertical.Minimum = 0;
                this.ScrollBarVertical.Maximum = Convert.ToInt32(diaghgt - clienthgt + Extra);
                this.ScrollBarVertical.SmallChange = Convert.ToInt32(diag.CellSize * zoom);
                this.ScrollBarVertical.LargeChange = Convert.ToInt32(diag.CellSize * zoom);

                if (this.ScrollBarVertical.SmallChange == 0)
                {
                    this.ScrollBarVertical.SmallChange = 1;
                }

                if (this.ScrollBarVertical.LargeChange == 0)
                {
                    this.ScrollBarVertical.LargeChange = 1;
                }

                this.ScrollBarVertical.Maximum += Convert.ToInt32(this.ScrollBarVertical.LargeChange);

                if (diag.VerticalScrollValue <= ScrollBarVertical.Maximum)
                {
                    diag.VerticalScrollValue = diag.VerticalScrollValue;
                }
            }
        }

        /// <summary>
        /// Creates a quick view title for the current set of selected state clases
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CreateQuickViewTitle()
        {
            StringBuilder sb = new StringBuilder();
            DataSheet ds = this.DataFeed.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
            TransitionDiagram d = this.GetCurrentDiagram();

            sb.AppendFormat(CultureInfo.InvariantCulture, "{0} - ", this.DataFeed.Scenario.DisplayName);

            foreach (StateClassShape s in d.SelectedShapes)
            {
                object v = DataTableUtilities.GetTableValue(ds.GetData(), ds.ValueMember, s.StateClassIdSource, ds.DisplayMember);
                sb.AppendFormat(CultureInfo.InvariantCulture, "{0},", Convert.ToString(v, CultureInfo.InvariantCulture));
            }

            return sb.ToString().Trim(',');
        }

        /// <summary>
        /// Creates a quick view tag for the current set of selected state clases
        /// </summary>
        /// <returns></returns>
        /// <remarks>Note that shape selection order will affect the tag so we need to sort the Ids before using them.</remarks>
        private string CreateQuickViewTag()
        {
            StringBuilder sb = new StringBuilder();
            DataSheet dsst = this.DataFeed.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
            DataSheet dssc = this.DataFeed.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
            TransitionDiagram d = this.GetCurrentDiagram();

            if (d.StratumId.HasValue)
            {
                object v = DataTableUtilities.GetTableValue(dsst.GetData(), dsst.ValueMember, d.StratumId.Value, dsst.DisplayMember);

                sb.AppendFormat(CultureInfo.InvariantCulture, 
                    "{0}-{1}:", d.StratumId.Value, 
                    Convert.ToString(v, CultureInfo.InvariantCulture));
            }
            else
            {
                sb.Append("NULL-NULL");
            }

            List<int> lst = new List<int>();

            foreach (StateClassShape s in d.SelectedShapes)
            {
                lst.Add(s.StateClassIdSource);
            }

            lst.Sort();

            foreach (int i in lst)
            {
                foreach (StateClassShape s in d.SelectedShapes)
                {
                    if (s.StateClassIdSource == i)
                    {
                        object v = DataTableUtilities.GetTableValue(dssc.GetData(), dssc.ValueMember, s.StateClassIdSource, dssc.DisplayMember);
                        string DisplayValue = Convert.ToString(v, CultureInfo.InvariantCulture);
                        sb.AppendFormat(CultureInfo.InvariantCulture, "{0}-{1}:", s.StateClassIdSource, DisplayValue);
                    }
                }
            }

            string StateClasses = sb.ToString().Trim(':');
            return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Project.Library.Connection.ConnectionString, StateClasses);
        }

        /// <summary>
        /// Opens the selected state classes
        /// </summary>
        /// <remarks>
        /// DEVTODO: The tag we create below will not work if two libraries have the same state class and 
        /// stratum Ids.  However, the worst that will happen is that the wrong quick view will be activated...
        /// </remarks>
        private void OpenSelectedStateClasses()
        {
            List<int> lst = new List<int>();
            TransitionDiagram d = this.GetCurrentDiagram();
            string title = this.CreateQuickViewTitle();
            string tag = this.CreateQuickViewTag();

            if (this.Session.Application.GetView(tag) != null)
            {
                this.Session.Application.ActivateView(tag);
            }
            else
            {
                using (HourGlass h = new HourGlass())
                {
                    foreach (StateClassShape s in d.SelectedShapes)
                    {
                        lst.Add(s.StateClassIdSource);
                    }

                    StateClassQuickView v = (StateClassQuickView)this.Session.CreateDataFeedView(typeof(StateClassQuickView), this.Library, this.Project, this.Scenario, null);

                    v.LoadStateClasses(d.StratumId, lst, this.DataFeed, tag);
                    this.Session.Application.HostView(v, title, tag);
                }
            }
        }
    }
}
