// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using SyncroSim.Common.Forms;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class TransitionDiagram
    {
        private List<StateClassShape> GetPrioritizedShapeList()
        {
            List<StateClassShape> l = new List<StateClassShape>();
            Dictionary<int, bool> d = new Dictionary<int, bool>();

            //The user can copy the same state class if it is in both the wild card stratum 
            //and in the current stratum.  In this case we only copy the one in the current stratum.

            foreach (StateClassShape Shape in this.SelectedShapes)
            {
                if (Shape.StratumIdSource.HasValue)
                {
                    l.Add(Shape);
                    d.Add(Shape.StateClassIdSource, true);
                }
            }

            foreach (StateClassShape Shape in this.SelectedShapes)
            {
                if (!d.ContainsKey(Shape.StateClassIdSource))
                {
                    l.Add(Shape);
                }
            }

            Debug.Assert(l.Count > 0);
            return l;
        }

        private void InternalCopyToClip()
        {
            DataObject dobj = new DataObject();
            TransitionDiagramClipData data = new TransitionDiagramClipData();
            DataSheet StratumSheet = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
            DataSheet StateClassSheet = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
            DataSheet TransitionTypeSheet = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_NAME);
            List<StateClassShape> PrioritizedShapes = this.GetPrioritizedShapeList();

            foreach (StateClassShape Shape in PrioritizedShapes)
            {
                TransitionDiagramClipDataEntry Entry = new TransitionDiagramClipDataEntry();

                if (Shape.StratumIdSource.HasValue)
                {
                    Entry.ShapeData.StratumSource = StratumSheet.ValidationTable.GetDisplayName(Shape.StratumIdSource.Value);
                }

                Entry.ShapeData.StateClassSource = StateClassSheet.ValidationTable.GetDisplayName(Shape.StateClassIdSource);

                if (Shape.StratumIdDest.HasValue)
                {
                    Entry.ShapeData.StratumDest = StratumSheet.ValidationTable.GetDisplayName(Shape.StratumIdDest.Value);
                }

                if (Shape.StateClassIdDest.HasValue)
                {
                    Entry.ShapeData.StateClassDest = StateClassSheet.ValidationTable.GetDisplayName(Shape.StateClassIdDest.Value);
                }

                Entry.ShapeData.AgeMin = Shape.AgeMinimum;
                Entry.ShapeData.AgeMax = Shape.AgeMaximum;
                Entry.Row = Shape.Row;
                Entry.Column = Shape.Column;
                Entry.Bounds = Shape.Bounds;

                foreach (DeterministicTransition t in Shape.IncomingDT)
                {
                    Entry.IncomingDT.Add(DTToClipFormat(t, StratumSheet, StateClassSheet));
                }

                foreach (Transition t in Shape.IncomingPT)
                {
                    Entry.IncomingPT.Add(PTToClipFormat(t, StratumSheet, StateClassSheet, TransitionTypeSheet));
                }

                foreach (Transition t in Shape.OutgoingPT)
                {
                    Entry.OutgoingPT.Add(PTToClipFormat(t, StratumSheet, StateClassSheet, TransitionTypeSheet));
                }

                data.Entries.Add(Entry);
            }

            dobj.SetData(Strings.CLIPBOARD_FORMAT_TRANSITION_DIAGRAM, data);
            Clipboard.SetDataObject(dobj);
        }

        private void InternalPasteSpecial(bool pasteAll, bool pasteBetween, bool pasteNone, bool pasteDeterministic, bool pasteProbabilistic, bool isTargeted)
        {
            //Get the clipboard data and verify that all items can be pasted.  Note that
            //once it has been validated it will contain the correct Ids for those items.

            TransitionDiagramClipData cd = (TransitionDiagramClipData)Clipboard.GetData(Strings.CLIPBOARD_FORMAT_TRANSITION_DIAGRAM);

            DataSheet StratumSheet = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
            DataSheet StateClassSheet = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
            DataSheet TransitionTypeSheet = this.m_DataFeed.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_NAME);

            if (!ValidateClipData(cd, StratumSheet, StateClassSheet, TransitionTypeSheet))
            {
                return;
            }

            //Make sure the user is Ok with overwriting any existing data

            if (!this.ConfirmPasteOverwrite(cd))
            {
                return;
            }

            //Get the paste location deltas for the current paste shapes

            int dx = 0;
            int dy = 0;

            if (!this.GetPasteLocationDeltas(ref dx, ref dy, isTargeted))
            {
                if (isTargeted)
                {
                    FormsUtilities.ErrorMessageBox(MessageStrings.ERROR_DIAGRAM_CANNOT_PASTE_SPECIFIC_LOCATION);
                }
                else
                {
                    FormsUtilities.ErrorMessageBox(MessageStrings.ERROR_DIAGRAM_CANNOT_PASTE_ANY_LOCATION);
                }

                return;
            }

            //Paste all state classes and transitions

            this.m_DTDataSheet.BeginAddRows();
            this.m_PTDataSheet.BeginAddRows();

            DTAnalyzer Analyzer = new DTAnalyzer(this.m_DTDataSheet.GetData(), this.m_DataFeed.Project);

            this.InternalPasteStateClasses(cd, dx, dy, pasteNone, Analyzer);

            this.PasteTransitions(cd, pasteAll, pasteBetween, pasteDeterministic, pasteProbabilistic, Analyzer);

            this.m_DTDataSheet.EndAddRows();
            this.m_PTDataSheet.EndAddRows();

            this.DeselectAllShapes();

            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                if (this.m_ExplicitClasses.ContainsKey(Entry.ShapeData.StateClassIdSource))
                {
                    this.SelectShape(this.m_ExplicitClasses[Entry.ShapeData.StateClassIdSource]);
                }
                else if (this.m_WildcardClasses.ContainsKey(Entry.ShapeData.StateClassIdSource))
                {
                    this.SelectShape(this.m_WildcardClasses[Entry.ShapeData.StateClassIdSource]);
                }
            }

            this.Focus();
        }

        private void InternalPasteStateClasses(TransitionDiagramClipData cd, int dx, int dy, bool pasteNone, DTAnalyzer analyzer)
        {
            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                Dictionary<int, StateClassShape> d = null;

                if (this.m_StratumId.HasValue)
                {
                    d = this.m_ExplicitClasses;
                }
                else
                {
                    d = this.m_WildcardClasses;
                }

                //If the state class being pasted is already in the diagram then just 
                //update the ages.  Otherwise, create a new state class in this stratum.

                if (d.ContainsKey(Entry.ShapeData.StateClassIdSource))
                {
                    this.PasteStateClassesReplace(Entry, analyzer);
                }
                else
                {
                    this.PasteStateClassesCreateNew(cd, Entry, dx, dy, pasteNone, analyzer);
                }
            }
        }

        private void PasteStateClassesReplace(TransitionDiagramClipDataEntry entry, DTAnalyzer analyzer)
        {
            DataRow dr = analyzer.GetStateClassRow(this.m_StratumId, entry.ShapeData.StateClassIdSource);

            if (entry.ShapeData.AgeMin.HasValue)
            {
                dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = entry.ShapeData.AgeMin.Value;
            }
            else
            {
                dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DBNull.Value;
            }

            if (entry.ShapeData.AgeMax.HasValue)
            {
                dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = entry.ShapeData.AgeMax.Value;
            }
            else
            {
                dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DBNull.Value;
            }
        }

        private void PasteStateClassesCreateNew(TransitionDiagramClipData cd, TransitionDiagramClipDataEntry entry, int dx, int dy, bool pasteNone, DTAnalyzer analyzer)
        {
            int TargetRow = entry.Row + dy;
            int TargetColumn = entry.Column + dx;

            //If 'PasteNone" or there is no destination state class then 
            //create a transition-to-self in this stratum.

            if (pasteNone || (!entry.ShapeData.StateClassIdDest.HasValue))
            {
                this.CreateDTRecord(entry.ShapeData.StateClassIdSource, null, null, entry.ShapeData.AgeMin, entry.ShapeData.AgeMax, TargetRow, TargetColumn);

                return;
            }

            //If the destination state class is in the clipboard then create a transition
            //to that state class in this stratum.

            if (ClipContainsStateClass(cd, entry.ShapeData.StateClassIdDest.Value))
            {
                this.CreateDTRecord(entry.ShapeData.StateClassIdSource, null, entry.ShapeData.StateClassIdDest.Value, entry.ShapeData.AgeMin, entry.ShapeData.AgeMax, TargetRow, TargetColumn);

                return;
            }

            //Resolve the destination stratum and create a transition to that stratum.  Note that the resolution
            //will fail if the destination state class no longer exists and is not in the wild card stratum.  In
            //this case, create a transition-to-self.

            int? StratumIdActual = null;

            if (analyzer.ResolveStateClassStratum(entry.ShapeData.StratumIdSource, entry.ShapeData.StratumIdDest, entry.ShapeData.StateClassIdDest.Value, ref StratumIdActual))
            {
                this.CreateDTRecord(entry.ShapeData.StateClassIdSource, StratumIdActual, entry.ShapeData.StateClassIdDest.Value, entry.ShapeData.AgeMin, entry.ShapeData.AgeMax, TargetRow, TargetColumn);
            }
            else
            {
                this.CreateDTRecord(entry.ShapeData.StateClassIdSource, null, null, entry.ShapeData.AgeMin, entry.ShapeData.AgeMax, TargetRow, TargetColumn);
            }
        }

        private void PasteTransitions(TransitionDiagramClipData cd, bool pasteAll, bool pasteBetween, bool pasteDeterministic, bool pasteProbabilistic, DTAnalyzer analyzer)
        {
            if (pasteAll)
            {
                if (pasteDeterministic)
                {
                    this.PasteDTIncoming(cd, analyzer);
                }

                if (pasteProbabilistic)
                {
                    this.PastePT(cd, analyzer);
                }
            }
            else if (pasteBetween)
            {
                if (pasteProbabilistic)
                {
                    List<ProbabilisticTransitionClipData> AlreadyPasted = new List<ProbabilisticTransitionClipData>();
                    this.PastePTBetween(cd, AlreadyPasted);
                }
            }
        }

        private void PasteDTIncoming(TransitionDiagramClipData cd, DTAnalyzer analyzer)
        {
            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                if (!this.m_ExplicitClasses.ContainsKey(Entry.ShapeData.StateClassIdSource))
                {
                    this.PasteDTIncoming(cd, Entry, analyzer);
                }
            }
        }

        /// <summary>
        /// Pastes any incoming deterministic transitions
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="entry"></param>
        /// <remarks>
        /// Create all incoming deterministic transitions described in the specified clipboard entry as follows:
        /// 
        /// (1.) We are not going to look at any state classes that are in the clipboard since the transitions for these state 
        ///      classes were established when the state classes paste was performed.
        /// 
        /// (2.) If the transition is coming from a state class in the same diagram (but that state class is not found in the clipboard) then we
        ///      can create that transition.  But we can only do this if the source state class does not have a transition to another state class.
        /// 
        /// (3.) If it is a transition coming from an off-stratum state class, make sure the state class still exists in the source stratum.  And, 
        ///      as with a state class in this diagram, only do this if the source state class does not have a transition to another state class.
        /// </remarks>
        private void PasteDTIncoming(TransitionDiagramClipData cd, TransitionDiagramClipDataEntry entry, DTAnalyzer analyzer)
        {
            foreach (DeterministicTransitionClipData t in entry.IncomingDT)
            {
                if (!ClipContainsStateClass(cd, t.StateClassIdSource))
                {
                    if (this.m_ExplicitClasses.ContainsKey(t.StateClassIdSource))
                    {
                        DataRow dr = analyzer.GetStateClassRow(this.m_StratumId, t.StateClassIdSource);

                        if (IsDTToSelf(dr))
                        {
                            dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME] = entry.ShapeData.StateClassIdSource;
                        }
                    }
                    else
                    {
                        DataRow dr = analyzer.GetStateClassRow(t.StratumIdSource, t.StateClassIdSource);

                        if (dr != null && IsDTToSelf(dr))
                        {
                            dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME] = entry.ShapeData.StateClassIdSource;
                            dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME] = this.m_StratumId;
                        }
                    }
                }
            }
        }

        private void PastePT(TransitionDiagramClipData cd, DTAnalyzer analyzer)
        {
            List<ProbabilisticTransitionClipData> AlreadyPasted = new List<ProbabilisticTransitionClipData>();

            this.PastePTBetween(cd, AlreadyPasted);
            this.PastePTIncoming(cd, AlreadyPasted, analyzer);
            this.PastePTOutgoing(cd, AlreadyPasted, analyzer);
        }

        private void PastePTBetween(TransitionDiagramClipData cd, List<ProbabilisticTransitionClipData> alreadyPasted)
        {
            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                this.PastePTOutgoingBetween(cd, Entry, alreadyPasted);
            }
        }

        private void PastePTIncoming(TransitionDiagramClipData cd, List<ProbabilisticTransitionClipData> alreadyPasted, DTAnalyzer analyzer)
        {
            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                this.PastePTIncoming(Entry, alreadyPasted, analyzer);
            }
        }

        private void PastePTOutgoing(TransitionDiagramClipData cd, List<ProbabilisticTransitionClipData> alreadyPasted, DTAnalyzer analyzer)
        {
            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                this.PastePTOutgoing(Entry, alreadyPasted, analyzer);
            }
        }

        private void PastePTIncoming(TransitionDiagramClipDataEntry entry, List<ProbabilisticTransitionClipData> alreadyPasted, DTAnalyzer analyzer)
        {
            foreach (ProbabilisticTransitionClipData t in entry.IncomingPT)
            {
                if (AlreadyPastedPT(t, alreadyPasted))
                {
                    continue;
                }

                int? StratumIdDest = null;

                if (t.StateClassIdDest.HasValue)
                {
                    analyzer.ResolveStateClassStratum(t.StratumIdSource, t.StratumIdDest, t.StateClassIdDest.Value, ref StratumIdDest);
                }

                this.CreatePTRecord(this.m_StratumId, t.StateClassIdSource, StratumIdDest, entry.ShapeData.StateClassIdSource, t.TransitionTypeId, t.Probability, t.Proportion, t.AgeMin, t.AgeMax, t.AgeRelative, t.AgeReset, t.TstMin, t.TstMax, t.TstRelative);

                alreadyPasted.Add(t);
            }
        }

        private void PastePTOutgoing(TransitionDiagramClipDataEntry entry, List<ProbabilisticTransitionClipData> alreadyPasted, DTAnalyzer analyzer)
        {
            foreach (ProbabilisticTransitionClipData t in entry.OutgoingPT)
            {
                if (AlreadyPastedPT(t, alreadyPasted))
                {
                    continue;
                }

                int? StratumIdDest = null;

                if (t.StateClassIdDest.HasValue)
                {
                    analyzer.ResolveStateClassStratum(t.StratumIdSource, t.StratumIdDest, t.StateClassIdDest.Value, ref StratumIdDest);
                }

                this.CreatePTRecord(this.m_StratumId, entry.ShapeData.StateClassIdSource, StratumIdDest, t.StateClassIdDest, t.TransitionTypeId, t.Probability, t.Proportion, t.AgeMin, t.AgeMax, t.AgeRelative, t.AgeReset, t.TstMin, t.TstMax, t.TstRelative);

                alreadyPasted.Add(t);
            }
        }

        private void PastePTOutgoingBetween(TransitionDiagramClipData cd, TransitionDiagramClipDataEntry entry, List<ProbabilisticTransitionClipData> alreadyPasted)
        {
            foreach (ProbabilisticTransitionClipData t in entry.OutgoingPT)
            {
                if (AlreadyPastedPT(t, alreadyPasted))
                {
                    continue;
                }

                if (!t.StateClassIdDest.HasValue)
                {
                    continue;
                }

                if (ClipContainsStateClass(cd, t.StateClassIdDest.Value))
                {
                    this.CreatePTRecord(this.m_StratumId, entry.ShapeData.StateClassIdSource, null, t.StateClassIdDest, t.TransitionTypeId, t.Probability, t.Proportion, t.AgeMin, t.AgeMax, t.AgeRelative, t.AgeReset, t.TstMin, t.TstMax, t.TstRelative);

                    alreadyPasted.Add(t);
                }
            }
        }

        private static bool ClipContainsStateClass(TransitionDiagramClipData cd, int stateClassId)
        {
            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                if (Entry.ShapeData.StateClassIdSource == stateClassId)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ConfirmPasteOverwrite(TransitionDiagramClipData cd)
        {
            bool OverwriteRequired = false;

            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                if (this.m_ExplicitClasses.ContainsKey(Entry.ShapeData.StateClassIdSource))
                {
                    OverwriteRequired = true;
                    break;
                }
                else if (this.m_WildcardClasses.ContainsKey(Entry.ShapeData.StateClassIdSource))
                {
                    if (!this.m_StratumId.HasValue)
                    {
                        OverwriteRequired = true;
                        break;
                    }
                }
            }

            if (OverwriteRequired)
            {
                if (FormsUtilities.ApplicationMessageBox(MessageStrings.CONFIRM_DIAGRAM_PASTE_OVERWRITE, MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool PTClipObjectsEqual(ProbabilisticTransitionClipData t1, ProbabilisticTransitionClipData t2)
        {
            if (!NullableUtilities.NullableIdsEqual(t1.StratumIdSource, t2.StratumIdSource))
            {
                return false;
            }

            if (t1.StateClassIdSource != t2.StateClassIdSource)
            {
                return false;
            }

            if (!NullableUtilities.NullableIdsEqual(t1.StratumIdDest, t2.StratumIdDest))
            {
                return false;
            }

            if (!NullableUtilities.NullableIdsEqual(t1.StateClassIdDest, t2.StateClassIdDest))
            {
                return false;
            }

            if (t1.TransitionTypeId != t2.TransitionTypeId)
            {
                return false;
            }

            if (t1.Proportion != t2.Proportion)
            {
                return false;
            }

            if (t1.Probability != t2.Probability)
            {
                return false;
            }

            if (t1.AgeMin != t2.AgeMin)
            {
                return false;
            }

            if (t1.AgeMax != t2.AgeMax)
            {
                return false;
            }

            if (t1.AgeRelative != t2.AgeRelative)
            {
                return false;
            }

            if (t1.AgeReset != t2.AgeReset)
            {
                return false;
            }

            if (t1.TstMin != t2.TstMin)
            {
                return false;
            }

            if (t1.TstMax != t2.TstMax)
            {
                return false;
            }

            if (t1.TstRelative != t2.TstRelative)
            {
                return false;
            }

            return true;
        }

        private static bool AlreadyPastedPT(ProbabilisticTransitionClipData pt, List<ProbabilisticTransitionClipData> alreadyPasted)
        {
            foreach (ProbabilisticTransitionClipData t in alreadyPasted)
            {
                if (PTClipObjectsEqual(t, pt))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified x and y offsets are for a valid paste location
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool IsValidPasteLocation(int dx, int dy)
        {
            TransitionDiagramClipData cd = (TransitionDiagramClipData)Clipboard.GetData(Strings.CLIPBOARD_FORMAT_TRANSITION_DIAGRAM);

            foreach (TransitionDiagramClipDataEntry Entry in cd.Entries)
            {
                int TargetRow = Entry.Row + dy;
                int TargetColumn = Entry.Column + dx;

                if (TargetRow < 0 || TargetColumn < 0 || TargetRow >= Constants.TRANSITION_DIAGRAM_MAX_ROWS || TargetColumn >= Constants.TRANSITION_DIAGRAM_MAX_COLUMNS)
                {
                    return false;
                }

                BoxDiagramShape s = this.GetShapeAt(TargetRow, TargetColumn);

                if (s != null)
                {
                    if (!s.IsStatic)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the upper left clipboard entry
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private static TransitionDiagramClipDataEntry GetUpperLeftClipEntry()
        {
            int MinRow = int.MaxValue;
            int MinColumn = int.MaxValue;
            List<TransitionDiagramClipDataEntry> lst = new List<TransitionDiagramClipDataEntry>();

            TransitionDiagramClipData cd = (TransitionDiagramClipData)Clipboard.GetData(Strings.CLIPBOARD_FORMAT_TRANSITION_DIAGRAM);

            foreach (TransitionDiagramClipDataEntry e in cd.Entries)
            {
                if (e.Row < MinRow)
                {
                    MinRow = e.Row;
                }
            }

            foreach (TransitionDiagramClipDataEntry e in cd.Entries)
            {
                if (e.Row == MinRow)
                {
                    lst.Add(e);
                }
            }

            foreach (TransitionDiagramClipDataEntry e in lst)
            {
                if (e.Column < MinColumn)
                {
                    MinColumn = e.Column;
                }
            }

            foreach (TransitionDiagramClipDataEntry e in lst)
            {
                if (e.Column == MinColumn)
                {
                    return e;
                }
            }

            Debug.Assert(false);
            return null;
        }

        /// <summary>
        /// Gets the paste location delta values for the state classes on the clipboard
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="targeted"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetPasteLocationDeltas(ref int dx, ref int dy, bool targeted)
        {
            TransitionDiagramClipDataEntry UpperLeftEntry = GetUpperLeftClipEntry();

            //If the paste is targeted (using the context menu), then we only attempt to paste into the
            //cell that was current when the right mouse button was clicked.  But if it is a non-targeted
            //pasted then we need to search the diagram for a paste location.

            if (targeted)
            {
                dx = this.GetColumnDelta(this.CurrentMousePoint.X, UpperLeftEntry.Bounds.X);
                dy = this.GetRowDelta(this.CurrentMousePoint.Y, UpperLeftEntry.Bounds.Y);

                return (this.IsValidPasteLocation(dx, dy));
            }

            //First, try the original location

            dx = 0;
            dy = 0;

            if (this.IsValidPasteLocation(dx, dy))
            {
                return true;
            }

            //Search all rows in the current column

            dx = 0;
            dy = 0;

            while (dy < Constants.TRANSITION_DIAGRAM_MAX_ROWS)
            {
                if (this.IsValidPasteLocation(dx, dy))
                {
                    return true;
                }

                dy += 1;
            }

            //Search all columns and rows

            dx = this.GetColumnDelta(0, UpperLeftEntry.Bounds.X);

            while (dx < Constants.TRANSITION_DIAGRAM_MAX_COLUMNS)
            {
                dy = this.GetRowDelta(0, UpperLeftEntry.Bounds.Y);

                while (dy < Constants.TRANSITION_DIAGRAM_MAX_ROWS)
                {
                    if (this.IsValidPasteLocation(dx, dy))
                    {
                        return true;
                    }

                    dy += 1;
                }

                dx += 1;
            }

            //No location found anywhere!
            return false;
        }

        private static DeterministicTransitionClipData DTToClipFormat(DeterministicTransition dt, DataSheet stratumSheet, DataSheet stateClassSheet)
        {
            Debug.Assert(stratumSheet.Name == Strings.DATASHEET_STRATA_NAME);
            Debug.Assert(stateClassSheet.Name == Strings.DATASHEET_STATECLASS_NAME);

            DeterministicTransitionClipData cd = new DeterministicTransitionClipData();

            if (dt.StratumIdSource.HasValue)
            {
                cd.StratumSource = stratumSheet.ValidationTable.GetDisplayName(dt.StratumIdSource.Value);
            }

            cd.StateClassSource = stateClassSheet.ValidationTable.GetDisplayName(dt.StateClassIdSource);

            if (dt.StratumIdDestination.HasValue)
            {
                cd.StratumDest = stratumSheet.ValidationTable.GetDisplayName(dt.StratumIdDestination.Value);
            }

            if (cd.StateClassIdDest.HasValue)
            {
                cd.StateClassDest = stateClassSheet.ValidationTable.GetDisplayName(dt.StateClassIdDestination.Value);
            }

            return cd;
        }

        private static ProbabilisticTransitionClipData PTToClipFormat(Transition pt, DataSheet stratumSheet, DataSheet stateClassSheet, DataSheet transitionTypeSheet)
        {
            Debug.Assert(stratumSheet.Name == Strings.DATASHEET_STRATA_NAME);
            Debug.Assert(stateClassSheet.Name == Strings.DATASHEET_STATECLASS_NAME);
            Debug.Assert(transitionTypeSheet.Name == Strings.DATASHEET_TRANSITION_TYPE_NAME);

            ProbabilisticTransitionClipData cd = new ProbabilisticTransitionClipData();

            if (pt.StratumIdSource.HasValue)
            {
                cd.StratumSource = stratumSheet.ValidationTable.GetDisplayName(pt.StratumIdSource.Value);
            }

            cd.StateClassSource = stateClassSheet.ValidationTable.GetDisplayName(pt.StateClassIdSource);

            if (pt.StratumIdDestination.HasValue)
            {
                cd.StratumDest = stratumSheet.ValidationTable.GetDisplayName(pt.StratumIdDestination.Value);
            }

            if (pt.StateClassIdDestination.HasValue)
            {
                cd.StateClassDest = stateClassSheet.ValidationTable.GetDisplayName(pt.StateClassIdDestination.Value);
            }

            cd.TransitionType = transitionTypeSheet.ValidationTable.GetDisplayName(pt.TransitionTypeId);
            cd.Probability = pt.Probability;

            if (pt.PropnWasNull)
            {
                cd.Proportion = null;
            }
            else
            {
                cd.Proportion = pt.Proportion;
            }

            if (pt.AgeMinWasNull)
            {
                cd.AgeMin = null;
            }
            else
            {
                cd.AgeMin = pt.AgeMinimum;
            }

            if (pt.AgeMaxWasNull)
            {
                cd.AgeMax = null;
            }
            else
            {
                cd.AgeMax = pt.AgeMaximum;
            }

            if (pt.AgeRelativeWasNull)
            {
                cd.AgeRelative = null;
            }
            else
            {
                cd.AgeRelative = pt.AgeRelative;
            }

            if (pt.AgeResetWasNull)
            {
                cd.AgeReset = null;
            }
            else
            {
                cd.AgeReset = pt.AgeReset;
            }

            if (pt.TstMinimumWasNull)
            {
                cd.TstMin = null;
            }
            else
            {
                cd.TstMin = pt.TstMinimum;
            }

            if (pt.TstMaximumWasNull)
            {
                cd.TstMax = null;
            }
            else
            {
                cd.TstMax = pt.TstMaximum;
            }

            if (pt.TstRelativeWasNull)
            {
                cd.TstRelative = null;
            }
            else
            {
                cd.TstRelative = pt.TstRelative;
            }

            return cd;
        }

        private static bool ValidateClipData(TransitionDiagramClipData cd, DataSheet stratumSheet, DataSheet stateClassSheet, DataSheet transitionTypeSheet)
        {
            Debug.Assert(stratumSheet.Name == Strings.DATASHEET_STRATA_NAME);
            Debug.Assert(stateClassSheet.Name == Strings.DATASHEET_STATECLASS_NAME);
            Debug.Assert(transitionTypeSheet.Name == Strings.DATASHEET_TRANSITION_TYPE_NAME);

            foreach (TransitionDiagramClipDataEntry entry in cd.Entries)
            {
                if (!ValidateDTClipData(entry.ShapeData, stratumSheet, stateClassSheet))
                {
                    return false;
                }

                foreach (DeterministicTransitionClipData t in entry.IncomingDT)
                {
                    if (!ValidateDTClipData(t, stratumSheet, stateClassSheet))
                    {
                        return false;
                    }
                }

                foreach (ProbabilisticTransitionClipData t in entry.IncomingPT)
                {
                    if (!ValidatePTClipData(t, stratumSheet, stateClassSheet, transitionTypeSheet))
                    {
                        return false;
                    }
                }

                foreach (ProbabilisticTransitionClipData t in entry.OutgoingPT)
                {
                    if (!ValidatePTClipData(t, stratumSheet, stateClassSheet, transitionTypeSheet))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool ValidateDTClipData(DeterministicTransitionClipData dt, DataSheet stratumSheet, DataSheet stateClassSheet)
        {
            Debug.Assert(stratumSheet.Name == Strings.DATASHEET_STRATA_NAME);
            Debug.Assert(stateClassSheet.Name == Strings.DATASHEET_STATECLASS_NAME);

            if (dt.StratumSource != null)
            {
                if (!stratumSheet.ValidationTable.ContainsValue(dt.StratumSource))
                {
                    FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", dt.StratumSource);
                    return false;
                }
            }

            if (!stateClassSheet.ValidationTable.ContainsValue(dt.StateClassSource))
            {
                FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", dt.StateClassSource);
                return false;
            }

            if (dt.StratumDest != null)
            {
                if (!stratumSheet.ValidationTable.ContainsValue(dt.StratumDest))
                {
                    FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", dt.StratumDest);
                    return false;
                }
            }

            if (dt.StateClassDest != null)
            {
                if (!stateClassSheet.ValidationTable.ContainsValue(dt.StateClassDest))
                {
                    FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", dt.StateClassDest);
                    return false;
                }
            }

            if (dt.StratumSource != null)
            {
                dt.StratumIdSource = stratumSheet.ValidationTable.GetValue(dt.StratumSource);
            }

            dt.StateClassIdSource = stateClassSheet.ValidationTable.GetValue(dt.StateClassSource);

            if (dt.StratumDest != null)
            {
                dt.StratumIdDest = stratumSheet.ValidationTable.GetValue(dt.StratumDest);
            }

            if (dt.StateClassDest != null)
            {
                dt.StateClassIdDest = stateClassSheet.ValidationTable.GetValue(dt.StateClassDest);
            }

#if DEBUG
            if (dt.StratumIdSource.HasValue)
            {
                Debug.Assert(dt.StratumIdSource.Value > 0);
            }

            Debug.Assert(dt.StateClassIdSource > 0);

            if (dt.StratumDest != null)
            {
                Debug.Assert(dt.StratumIdDest.Value > 0);
            }

            if (dt.StateClassDest != null)
            {
                Debug.Assert(dt.StateClassIdDest.Value > 0);
            }
#endif

            return true;
        }

        private static bool ValidatePTClipData(ProbabilisticTransitionClipData pt, DataSheet stratumSheet, DataSheet stateClassSheet, DataSheet transitionTypeSheet)
        {
            Debug.Assert(stratumSheet.Name == Strings.DATASHEET_STRATA_NAME);
            Debug.Assert(stateClassSheet.Name == Strings.DATASHEET_STATECLASS_NAME);
            Debug.Assert(transitionTypeSheet.Name == Strings.DATASHEET_TRANSITION_TYPE_NAME);

            if (pt.StratumSource != null)
            {
                if (!stratumSheet.ValidationTable.ContainsValue(pt.StratumSource))
                {
                    FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", pt.StratumSource);
                    return false;
                }
            }

            if (!stateClassSheet.ValidationTable.ContainsValue(pt.StateClassSource))
            {
                FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", pt.StateClassSource);
                return false;
            }

            if (pt.StratumDest != null)
            {
                if (!stratumSheet.ValidationTable.ContainsValue(pt.StratumDest))
                {
                    FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", pt.StratumDest);
                    return false;
                }
            }

            if (pt.StateClassDest != null)
            {
                if (!stateClassSheet.ValidationTable.ContainsValue(pt.StateClassDest))
                {
                    FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", pt.StateClassDest);
                    return false;
                }
            }

            if (!transitionTypeSheet.ValidationTable.ContainsValue(pt.TransitionType))
            {
                FormsUtilities.ErrorMessageBox("The transition type '{0}' does not exist in this project.", pt.TransitionType);
                return false;
            }

            if (pt.StratumSource != null)
            {
                pt.StratumIdSource = stratumSheet.ValidationTable.GetValue(pt.StratumSource);
            }

            pt.StateClassIdSource = stateClassSheet.ValidationTable.GetValue(pt.StateClassSource);

            if (pt.StratumDest != null)
            {
                pt.StratumIdDest = stratumSheet.ValidationTable.GetValue(pt.StratumDest);
            }

            if (pt.StateClassDest != null)
            {
                pt.StateClassIdDest = stateClassSheet.ValidationTable.GetValue(pt.StateClassDest);
            }

            pt.TransitionTypeId = transitionTypeSheet.ValidationTable.GetValue(pt.TransitionType);

#if DEBUG
            if (pt.StratumIdSource.HasValue)
            {
                Debug.Assert(pt.StratumIdSource.Value > 0);
            }

            Debug.Assert(pt.StateClassIdSource > 0);

            if (pt.StratumDest != null)
            {
                Debug.Assert(pt.StratumIdDest.Value > 0);
            }

            if (pt.StateClassDest != null)
            {
                Debug.Assert(pt.StateClassIdDest.Value > 0);
            }

            Debug.Assert(pt.TransitionTypeId > 0);
#endif

            return true;
        }
    }
}
