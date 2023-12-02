// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.Apex.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal partial class TransitionDiagram : BoxArrowDiagram
    {
        private int? m_StratumId = null;
        private DataFeed m_DataFeed;
        private DataSheet m_SLXDataSheet;
        private DataSheet m_SLYDataSheet;
        private DataSheet m_SCDataSheet;
        private DataSheet m_DTDataSheet;
        private DataSheet m_PTDataSheet;
        private DataSheet m_TTGDataSheet;
        private Dictionary<int, StateClassShape> m_ExplicitClasses = new Dictionary<int, StateClassShape>();
        private Dictionary<int, StateClassShape> m_WildcardClasses = new Dictionary<int, StateClassShape>();
        private bool m_IsFilterApplied;
        private bool m_SelectionStatic = true;
        private Dictionary<string, StateClassShape> m_SelectedStateClasses = new Dictionary<string, StateClassShape>();

        public TransitionDiagram(int? stratumId, DataFeed dataFeed)
        {
            this.InternalConstruct(stratumId, dataFeed);
        }

        public int? StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        public void RefreshDiagram()
        {
            this.InternalRefresh();
        }

        public void AddStateClass()
        {
            this.InternalAddStateClass();
        }

        public void EditStateClass()
        {
            this.InternalEditStateClass();
        }

        public bool CanOpenStateClasses()
        {
            if (this.m_StratumId.HasValue)
            {
                return (this.SelectedShapes.Count() > 0 && !this.IsReadOnly && !this.m_SelectionStatic);
            }
            else
            {
                return (this.SelectedShapes.Count() > 0 && !this.IsReadOnly);
            }
        }

        public bool CanCutStateClasses()
        {
            return (this.SelectedShapes.Count() > 0 && !this.IsReadOnly && !this.m_SelectionStatic);
        }

        public void CutStateClasses()
        {
            this.InternalCutStateClasses();
        }

        public bool CanCopyStateClasses()
        {
            return (this.SelectedShapes.Count() > 0);
        }

        public void CopyStateClasses()
        {
            this.InternalCopyToClip();
        }

        public bool CanPasteStateClasses()
        {
            return this.InternalCanPasteStateClasses();
        }

        public void PasteStateClasses(bool targeted)
        {
            this.InternalPasteSpecial(true, false, false, true, true, targeted);
        }

        public void PasteStateClassesSpecial(bool pasteTransitionsAll, bool pasteTransitionsBetween, bool pasteTransitionsNone, bool pasteTransitionsDeterministic, bool pasteTransitionsProbabilistic, bool targeted)
        {
            this.InternalPasteSpecial(pasteTransitionsAll, pasteTransitionsBetween, pasteTransitionsNone, pasteTransitionsDeterministic, pasteTransitionsProbabilistic, targeted);
        }

        public void DeleteStateClasses()
        {
            this.InternalDeleteStateClasses();
        }

        public bool CanSelectAllStateClasses()
        {
            return (this.Shapes.Count() > 0);
        }

        public void SelectAllStateClasses()
        {
            this.SelectAllShapes();
        }

        public void SelectStateClass(int stateClassId)
        {
            this.InternalSelectStateClass(stateClassId);
        }

        public void FilterTransitions(TransitionFilterCriteria criteria)
        {
            this.InternalFilterTransitions(criteria);
        }

        public void ApplyReadonlySettings()
        {
            this.InternalConfigureReadOnly();
        }

        private void InternalConstruct(int? stratumId, DataFeed dataFeed)
        {
            this.m_StratumId = stratumId;
            this.m_DataFeed = dataFeed;

            this.Rows = Constants.TRANSITION_DIAGRAM_MAX_ROWS;
            this.Columns = Constants.TRANSITION_DIAGRAM_MAX_COLUMNS;
            this.CellPadding = Constants.TRANSITION_DIAGRAM_SHAPE_PADDING;
            this.BoxSize = Constants.TRANSITION_DIAGRAM_SHAPE_SIZE;
            this.LanesBetweenShapes = Constants.TRANSITION_DIAGRAM_LANES_BETWEEN_SHAPES;

            this.m_SLXDataSheet = dataFeed.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_X_NAME);
            this.m_SLYDataSheet = dataFeed.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_Y_NAME);
            this.m_SCDataSheet = dataFeed.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
            this.m_DTDataSheet = dataFeed.Scenario.GetDataSheet(Strings.DATASHEET_DT_NAME);
            this.m_PTDataSheet = dataFeed.Scenario.GetDataSheet(Strings.DATASHEET_PT_NAME);
            this.m_TTGDataSheet = dataFeed.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_GROUP_NAME);

            this.AutoScroll = false;
        }

        private void SaveSelection()
        {
            this.m_SelectedStateClasses.Clear();

            foreach (StateClassShape Shape in this.Shapes)
            {
                if (Shape.IsSelected)
                {
                    string k = DTAnalyzer.CreateStateClassLookupKey(Shape.StratumIdSource, Shape.StateClassIdSource);
                    this.m_SelectedStateClasses.Add(k, Shape);
                }
            }
        }

        private void RestoreSelection()
        {
            foreach (StateClassShape Shape in this.Shapes)
            {
                string k = DTAnalyzer.CreateStateClassLookupKey(Shape.StratumIdSource, Shape.StateClassIdSource);

                if (this.m_SelectedStateClasses.ContainsKey(k))
                {
                    Shape.IsSelected = true;
                }
                else
                {
                    Shape.IsSelected = false;
                }
            }
        }

        private void InternalCutStateClasses()
        {
            this.CopyStateClasses();
            this.DeleteStateClasses();
        }

        private void InternalDeleteStateClasses()
        {
            this.m_DTDataSheet.BeginDeleteRows();
            this.m_PTDataSheet.BeginDeleteRows();

            DTAnalyzer Analyzer = new DTAnalyzer(this.m_DTDataSheet.GetData(), this.m_DataFeed.Project);

            foreach (StateClassShape Shape in this.SelectedShapes)
            {
                if (!Shape.IsStatic)
                {
                    DataRow row = Analyzer.GetStateClassRow(Shape.StratumIdSource, Shape.StateClassIdSource);

                    if (row.RowState == DataRowState.Added)
                    {
                        this.m_DTDataSheet.GetData().Rows.Remove(row);
                    }
                    else
                    {
                        row.Delete();
                    }
                }
            }

            this.m_DTDataSheet.EndDeleteRows();
            this.m_PTDataSheet.EndDeleteRows();
        }

        private void InternalSelectStateClass(int stateClassId)
        {
            if (this.m_ExplicitClasses.ContainsKey(stateClassId))
            {
                StateClassShape Shape = this.m_ExplicitClasses[stateClassId];
                this.SelectShape(Shape);
            }
        }

        private bool InternalCanPasteStateClasses()
        {
            if (this.IsReadOnly)
            {
                return false;
            }
            else
            {
                DataObject dobj = (DataObject)Clipboard.GetDataObject();
                return dobj.GetDataPresent(Strings.CLIPBOARD_FORMAT_TRANSITION_DIAGRAM);
            }
        }

        private void InternalRefresh()
        {
            this.SaveSelection();
            this.InternalRefreshStateClasses();
            this.InternalRefreshLookups();
            this.InternalRefreshTransitionLines();
            this.ApplyReadonlySettings();
            this.RestoreSelection();

            this.Invalidate();
        }

        private void InternalAddStateClass()
        {
            ChooseStateClassForm dlg = new ChooseStateClassForm();

            if (!dlg.Initialize(this, this.m_DataFeed, false))
            {
                return;
            }

            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            string Location = this.GetNextStateClassLocation();

            if (Location == null)
            {
                FormsUtilities.ErrorMessageBox(MessageStrings.ERROR_DIAGRAM_NO_MORE_LOCATIONS);
                return;
            }

            int StateClassId = this.GetStateClassId(dlg.ChosenStateLabelX.Value, dlg.ChosenStateLabelY.Value);

            if (StateClassId == -1)
            {
                StateClassId = this.InternalCreateNewStateClass(dlg.ChosenStateLabelX, dlg.ChosenStateLabelY);
            }

            this.m_DTDataSheet.BeginAddRows();
            DataRow dr = this.m_DTDataSheet.GetData().NewRow();

            dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(this.m_StratumId);
            dr[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME] = StateClassId;
            dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME] = DBNull.Value;
            dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME] = DBNull.Value;
            dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DBNull.Value;
            dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DBNull.Value;
            dr[Strings.DATASHEET_DT_LOCATION_COLUMN_NAME] = Location;

            this.m_DTDataSheet.GetData().Rows.Add(dr);
            this.m_DTDataSheet.EndAddRows();

            this.RefreshDiagram();
            this.SelectStateClass(StateClassId);
        }

        private void InternalEditStateClass()
        {
            Debug.Assert(this.SelectedShapes.Count() == 1);

            ChooseStateClassForm dlg = new ChooseStateClassForm();
            dlg.Initialize(this, this.m_DataFeed, true);

            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            StateClassShape EditShape = (StateClassShape)this.SelectedShapes.First();
            int OldStateClassId = this.GetStateClassId(EditShape.StateLabelXId, EditShape.StateLabelYId);
            int NewStateClassId = this.GetStateClassId(dlg.ChosenStateLabelX.Value, dlg.ChosenStateLabelY.Value);

            if (OldStateClassId == NewStateClassId)
            {
                return;
            }

            if (NewStateClassId == -1)
            {
                NewStateClassId = this.InternalCreateNewStateClass(dlg.ChosenStateLabelX, dlg.ChosenStateLabelY);
            }

            this.m_DTDataSheet.BeginModifyRows();
            this.m_PTDataSheet.BeginModifyRows();

            InternalChangeStateClassId(this.m_DTDataSheet, Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME, Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, OldStateClassId, NewStateClassId, this.m_StratumId);

            InternalChangeStateClassId(this.m_PTDataSheet, Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME, Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, OldStateClassId, NewStateClassId, this.m_StratumId);

            this.m_DTDataSheet.EndModifyRows();
            this.m_PTDataSheet.EndModifyRows();

            this.RefreshDiagram();
            this.SelectStateClass(NewStateClassId);
        }

        private void InternalFilterTransitions(TransitionFilterCriteria criteria)
        {
            bool dtflt = (!criteria.IncludeDeterministic);
            bool ptflt = (!criteria.IncludeProbabilistic);
            bool tgflt = IsTransitionGroupFilterApplied(criteria);

            this.m_IsFilterApplied = (dtflt || ptflt || tgflt);

            foreach (TransitionDiagramLine Line in this.Lines)
            {
                if ((Line) is DeterministicTransitionLine)
                {
                    Line.IsVisible = criteria.IncludeDeterministic;
                }
                else
                {
                    Line.IsVisible = criteria.IncludeProbabilistic;

                    //If no filtering has been done then make the line visible.  But if any filters have been
                    //applied then only make the line visible if one of its transition groups is not filtered out.

                    if (criteria.IncludeProbabilistic && tgflt)
                    {
                        ProbabilisticTransitionLine ptline = (ProbabilisticTransitionLine)Line;

                        if (ptline.TransitionGroups.Count > 0)
                        {
                            ptline.IsVisible = false;

                            foreach (int tg in ptline.TransitionGroups)
                            {
                                if (criteria.TransitionGroups[tg])
                                {
                                    ptline.IsVisible = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            this.Invalidate();
        }

        private void InternalConfigureReadOnly()
        {
            Color ShapeTextColor = new Color();
            Color ShapeBorderColor = new Color();

            if (this.IsReadOnly)
            {
                ShapeTextColor = Constants.TRANSITION_DIAGRAM_READONLY_TEXT_COLOR;
                ShapeBorderColor = Constants.TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR;
            }
            else
            {
                ShapeTextColor = Constants.TRANSITION_DIAGRAM_TEXT_COLOR;
                ShapeBorderColor = Constants.TRANSITION_DIAGRAM_SHAPE_BORDER_COLOR;
            }

            foreach (StateClassShape Shape in this.Shapes)
            {
                bool IsWild = (this.m_StratumId.HasValue && !Shape.StratumIdSource.HasValue);

                if (IsWild)
                {
                    Shape.TitleTextColor = Constants.TRANSITION_DIAGRAM_READONLY_TEXT_COLOR;
                    Shape.BorderColor = Constants.TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR;
                }
                else
                {
                    Shape.TitleTextColor = ShapeTextColor;
                    Shape.BorderColor = ShapeBorderColor;
                }

                foreach (DiagramShapeItem item in Shape.DiagramShapeItems)
                {
                    if (IsWild)
                    {
                        item.ForegroundColor = Constants.TRANSITION_DIAGRAM_READONLY_TEXT_COLOR;
                    }
                    else
                    {
                        item.ForegroundColor = ShapeTextColor;
                    }
                }
            }

            this.Invalidate();
        }

        private void InternalResetTransitionLines()
        {
            foreach (TransitionDiagramLine line in this.Lines)
            {
                line.IsSelected = false;

                if ((line) is DeterministicTransitionLine)
                {
                    line.LineColor = Constants.DETERMINISTIC_TRANSITION_LINE_COLOR;
                }
                else if ((line) is ProbabilisticTransitionLine)
                {
                    line.LineColor = Constants.PROBABILISTIC_TRANSITION_LINE_COLOR;
                }
            }
        }

        private void InternalRefreshLookups()
        {
            this.m_ExplicitClasses.Clear();
            this.m_WildcardClasses.Clear();

            foreach (StateClassShape Shape in this.Shapes)
            {
                if (Shape.StratumIdSource.HasValue)
                {
                    this.m_ExplicitClasses.Add(Shape.StateClassIdSource, Shape);
                }
                else
                {
                    this.m_WildcardClasses.Add(Shape.StateClassIdSource, Shape);
                }
            }

            Debug.Assert((this.m_ExplicitClasses.Count() + this.m_WildcardClasses.Count()) == this.Shapes.Count());
        }

        private void InternalRefreshStateClasses()
        {
            this.RemoveAllShapes();

            List<StateClassShape> lst = this.InternalGetStateClassShapes();
            Dictionary<int, DataTable> SeenBeforeDT = new Dictionary<int, DataTable>();
            Dictionary<int, DataTable> SeenBeforePTIn = new Dictionary<int, DataTable>();
            Dictionary<int, DataTable> SeenBeforePTOut = new Dictionary<int, DataTable>();

            foreach (StateClassShape Shape in lst)
            {
                Rectangle rc = this.GetShapeRectangleFromRowCol(Shape.Row, Shape.Column);

                Shape.SetDimensions(rc.Width, rc.Height);
                Shape.SetLocation(rc.X, rc.Y);
                Shape.CreateConnectorPoints();

                this.FillIncomingDT(Shape, SeenBeforeDT);
                this.FillIncomingPT(Shape, SeenBeforePTIn);
                this.FillOutgoingPT(Shape, SeenBeforePTOut);

                if (Shape.IsStatic)
                {
                    this.AddShape(Shape, DiagramZOrder.Last);
                }
                else
                {
                    this.AddShape(Shape, DiagramZOrder.First);
                }
            }

            Dictionary<Point, StateClassShape> d = new Dictionary<Point, StateClassShape>();

            foreach (StateClassShape Shape in this.Shapes)
            {
                if (Shape.IsStatic)
                {
                    Point p = new Point(Shape.Row, Shape.Column);
                    d.Add(p, Shape);
                }
            }

            foreach (StateClassShape Shape in this.Shapes)
            {
                if (!Shape.IsStatic)
                {
                    Point p = new Point(Shape.Row, Shape.Column);

                    if (d.ContainsKey(p))
                    {
                        Shape.SharesLocation = true;
                    }
                }
            }
        }

        private List<StateClassShape> InternalGetStateClassShapes()
        {
            List<StateClassShape> lst = new List<StateClassShape>();
            DataRow[] rows = this.GetDTRows();

            foreach (DataRow dr in rows)
            {
                int ShapeRow = -1;
                int ShapeColumn = -1;
                string Location = Convert.ToString(dr[Strings.DATASHEET_DT_LOCATION_COLUMN_NAME], CultureInfo.InvariantCulture);

                LocationToRowCol(Location, ref ShapeRow, ref ShapeColumn);

                int? StratumIdSource = null;
                int StateClassIdSource = Convert.ToInt32(dr[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
                int? StratumIdDestination = null;
                int? StateClassIdDestination = null;
                int? MinimumAge = null;
                int? MaximumAge = null;

                int StateLabelXId = Convert.ToInt32(DataTableUtilities.GetTableValue(this.m_SCDataSheet.GetData(), this.m_SCDataSheet.ValueMember, StateClassIdSource, Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME), CultureInfo.InvariantCulture);
                string StateLabelXName = Convert.ToString(DataTableUtilities.GetTableValue(this.m_SLXDataSheet.GetData(), this.m_SLXDataSheet.ValueMember, StateLabelXId, Strings.DATASHEET_NAME_COLUMN_NAME), CultureInfo.InvariantCulture);
                int StateLabelYId = Convert.ToInt32(DataTableUtilities.GetTableValue(this.m_SCDataSheet.GetData(), this.m_SCDataSheet.ValueMember, StateClassIdSource, Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME), CultureInfo.InvariantCulture);
                string StateLabelYName = Convert.ToString(DataTableUtilities.GetTableValue(this.m_SLYDataSheet.GetData(), this.m_SLYDataSheet.ValueMember, StateLabelYId, Strings.DATASHEET_NAME_COLUMN_NAME), CultureInfo.InvariantCulture);

                if (dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] != DBNull.Value)
                {
                    StratumIdSource = Convert.ToInt32(dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME] != DBNull.Value)
                {
                    StratumIdDestination = Convert.ToInt32(dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME] != DBNull.Value)
                {
                    StateClassIdDestination = Convert.ToInt32(dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] != DBNull.Value)
                {
                    MinimumAge = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] != DBNull.Value)
                {
                    MaximumAge = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                string slxdesc = StateLabelXName;
                string slydesc = StateLabelYName;

                object o1 = DataTableUtilities.GetTableValue(this.m_SLXDataSheet.GetData(), this.m_SLXDataSheet.ValueMember, StateLabelXId, Strings.DATASHEET_DESCRIPTION_COLUMN_NAME);
                object o2 = DataTableUtilities.GetTableValue(this.m_SLYDataSheet.GetData(), this.m_SLYDataSheet.ValueMember, StateLabelYId, Strings.DATASHEET_DESCRIPTION_COLUMN_NAME);

                if (o1 != DBNull.Value)
                {
                    slxdesc = Convert.ToString(o1, CultureInfo.InvariantCulture);
                }

                if (o2 != DBNull.Value)
                {
                    slydesc = Convert.ToString(o2, CultureInfo.InvariantCulture);
                }

                StateClassShape Shape = new StateClassShape(this.m_DataFeed.Project, StratumIdSource, StateClassIdSource, StratumIdDestination, StateClassIdDestination, MinimumAge, MaximumAge, StateLabelXId, StateLabelXName, slxdesc, StateLabelYId, slydesc);

                Shape.Row = ShapeRow;
                Shape.Column = ShapeColumn;

                DiagramShapeItem SlyItem = new DiagramShapeItem(StateLabelYName);
                SlyItem.Alignment = DiagramAlignment.Center;
                Shape.AddDiagramItem(SlyItem);

                int MinAge = 0;

                if (MinimumAge.HasValue)
                {
                    MinAge = MinimumAge.Value;
                }

                string AgeText = null;

                if (MaximumAge.HasValue)
                {
                    AgeText = string.Format(CultureInfo.InvariantCulture, "{0} - {1}", MinAge, MaximumAge.Value);
                }
                else
                {
                    AgeText = string.Format(CultureInfo.InvariantCulture, "{0}+", MinAge);
                }

                DiagramShapeItem item = new DiagramShapeItem(AgeText);

                item.Alignment = DiagramAlignment.Center;
                Shape.AddDiagramItem(item);

                bool IsWild = (this.m_StratumId.HasValue && !Shape.StratumIdSource.HasValue);

                if (IsWild)
                {
                    Shape.IsStatic = true;
                    Shape.TitleTextColor = Constants.TRANSITION_DIAGRAM_READONLY_TEXT_COLOR;
                    Shape.BorderColor = Constants.TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR;

                    foreach (DiagramShapeItem i in Shape.DiagramShapeItems)
                    {
                        i.ForegroundColor = Constants.TRANSITION_DIAGRAM_READONLY_TEXT_COLOR;
                    }
                }

                lst.Add(Shape);
            }

            return lst;
        }

        private void InternalRefreshTransitionLines()
        {
            this.ClearLines();

            if (this.Shapes.Count() == 0)
            {
                return;
            }

            foreach (BoxDiagramShape Shape in this.Shapes)
            {
                Shape.ResetConnectorPoints();
            }

            DTAnalyzer Analyzer = new DTAnalyzer(this.m_DTDataSheet.GetData(), this.m_DataFeed.Project);

            this.CreateDTLines(Analyzer);
            this.CreateDTOffStratumCues(Analyzer);

            this.CreatePTLines(Analyzer);
            this.CreatePTOffStratumCues(Analyzer);
        }

        private int InternalCreateNewStateClass(BaseValueDisplayListItem slxitem, BaseValueDisplayListItem slyitem)
        {
            int StateClassId = this.m_SCDataSheet.GetNextRowIdentity();

            this.m_SCDataSheet.BeginAddRows();
            DataRow NewRow = this.m_SCDataSheet.GetData().NewRow();

            NewRow[this.m_SCDataSheet.ValueMember] = StateClassId;
            NewRow[this.m_SCDataSheet.DisplayMember] = slxitem.Display + ":" + slyitem.Display;
            NewRow[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME] = slxitem.Value;
            NewRow[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME] = slyitem.Value;

            this.m_SCDataSheet.GetData().Rows.Add(NewRow);
            this.m_SCDataSheet.EndAddRows();

            return StateClassId;
        }

        private static void InternalChangeStateClassId(
            DataSheet dataSheet, string fromStratumColName, string fromStateClassColName, 
            string toStratumColName, string toStateClassColname, int oldStateClassId, int newStateClassId, int? currentStratumId)
        {
            foreach (DataRow dr in dataSheet.GetData().Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int? StratumIdSource = null;
                int StateClassIdSource = Convert.ToInt32(dr[fromStateClassColName], CultureInfo.InvariantCulture);
                int? StratumIdDest = null;
                int? StateClassIdDest = null;

                if (dr[fromStratumColName] != DBNull.Value)
                {
                    StratumIdSource = Convert.ToInt32(dr[fromStratumColName], CultureInfo.InvariantCulture);
                }

                if (dr[toStratumColName] != DBNull.Value)
                {
                    StratumIdDest = Convert.ToInt32(dr[toStratumColName], CultureInfo.InvariantCulture);
                }

                if (dr[toStateClassColname] != DBNull.Value)
                {
                    StateClassIdDest = Convert.ToInt32(dr[toStateClassColname], CultureInfo.InvariantCulture);
                }

                //If the FROM state class is the old ID then change it, but ONLY
                //if the FROM stratum is the current stratum.

                if (StateClassIdSource == oldStateClassId)
                {
                    if (NullableUtilities.NullableIdsEqual(StratumIdSource, currentStratumId))
                    {
                        dr[fromStateClassColName] = newStateClassId;
                    }
                }

                //If the TO state class is the old ID then change it, but only
                //if the TO stratum is the current stratum.

                if (!StateClassIdDest.HasValue)
                {
                    continue;
                }

                if (StateClassIdDest.Value == oldStateClassId)
                {
                    bool update = false;

                    if (currentStratumId.HasValue)
                    {
                        if (StratumIdDest.HasValue)
                        {
                            update = (StratumIdDest.Value == currentStratumId.Value);
                        }
                        else
                        {
                            update = NullableUtilities.NullableIdsEqual(StratumIdSource, currentStratumId.Value);
                        }
                    }
                    else
                    {
                        update = (!StratumIdDest.HasValue);
                    }

                    if (update)
                    {
                        dr[toStateClassColname] = newStateClassId;
                    }
                }
            }
        }
    }
}
