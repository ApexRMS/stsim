// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Common.Forms;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal partial class TransitionDiagram
    {
        private void DrawPTLines(System.Drawing.Graphics g, bool selected)
        {
            foreach (ConnectorLine line in this.Lines)
            {
                if ((line) is ProbabilisticTransitionLine)
                {
                    if (line.IsVisible)
                    {
                        if (line.IsSelected == selected)
                        {
                            line.Render(g);
                        }
                    }
                }
            }
        }

        private DataTable GetDataTableForIncomingPT(StateClassShape shape, Dictionary<int, DataTable> seenBefore)
        {
            int Key = shape.StratumIdSource.HasValue ? shape.StratumIdSource.Value : 0;

            if (!seenBefore.ContainsKey(Key))
            {
                string Query = null;

                if (shape.StratumIdSource.HasValue)
                {
                    Query = string.Format(CultureInfo.InvariantCulture,
                        "((StratumIDDest={0}) OR (StratumIDDest IS NULL AND StratumIDSource={0}))",
                        shape.StratumIdSource.Value);
                }
                else
                {
                    Query = "StratumIDSource IS NULL";
                }

                DataTable Source = this.m_PTDataSheet.GetData();
                DataTable Target = Source.Clone();
                DataRow[] Rows = Source.Select(Query, null);

                Rows.CopyToDataTable(Target, LoadOption.OverwriteChanges);
                seenBefore.Add(Key, Target);
            }

            return seenBefore[Key];
        }

        private DataTable GetDataTableForOutgoingPT(StateClassShape shape, Dictionary<int, DataTable> seenBefore)
        {
            int Key = shape.StratumIdSource.HasValue ? shape.StratumIdSource.Value : 0;

            if (!seenBefore.ContainsKey(Key))
            {
                string Query = null;

                if (shape.StratumIdSource.HasValue)
                {
                    Query = string.Format(CultureInfo.InvariantCulture,
                        "StratumIDSource={0}",
                        shape.StratumIdSource.Value);
                }
                else
                {
                    Query = "StratumIDSource IS NULL";
                }

                DataTable Source = this.m_PTDataSheet.GetData();
                DataTable Target = Source.Clone();
                DataRow[] Rows = Source.Select(Query, null);

                Rows.CopyToDataTable(Target, LoadOption.OverwriteChanges);
                seenBefore.Add(Key, Target);
            }

            return seenBefore[Key];
        }

        private void FillIncomingPT(StateClassShape shape, Dictionary<int, DataTable> seenBefore)
        {
            string Query = string.Format(CultureInfo.InvariantCulture,
                "StateClassIDDest={0}",
                shape.StateClassIdSource);

            DataTable dt = this.GetDataTableForIncomingPT(shape, seenBefore);
            DataRow[] rows = dt.Select(Query, null);

            foreach (DataRow dr in rows)
            {
                Debug.Assert(Convert.ToInt32(dr[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME], CultureInfo.InvariantCulture) == shape.StateClassIdSource);

                Transition pt = CreatePT(dr);
                shape.IncomingPT.Add(pt);
            }
        }

        private void FillOutgoingPT(StateClassShape shape, Dictionary<int, DataTable> seenBefore)
        {
            string Query = string.Format(CultureInfo.InvariantCulture,
                "StateClassIDSource={0}",
                shape.StateClassIdSource);

            DataTable dt = this.GetDataTableForOutgoingPT(shape, seenBefore);
            DataRow[] rows = dt.Select(Query, null);

            foreach (DataRow dr in rows)
            {
                Debug.Assert(Convert.ToInt32(dr[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture) == shape.StateClassIdSource);

                Transition pt = CreatePT(dr);
                shape.OutgoingPT.Add(pt);
            }
        }

        private void FillPTLineTransitionGroups(ProbabilisticTransitionLine line)
        {
            Debug.Assert(line.TransitionGroups.Count == 0);

            string filter = string.Format(CultureInfo.InvariantCulture, "TransitionTypeID={0}", line.TransitionTypeId);
            DataRow[] trrows = this.m_TTGDataSheet.GetData().Select(filter);

            foreach (DataRow dr in trrows)
            {
                int TransitionGroupId = Convert.ToInt32(dr["TransitionGroupID"], CultureInfo.InvariantCulture);
                line.TransitionGroups.Add(TransitionGroupId);
            }
        }

        private void CreatePTLines(DTAnalyzer analyzer)
        {
            foreach (StateClassShape Shape in this.Shapes)
            {
                this.CreateOutgoingPTLines(Shape, analyzer);
            }
        }

        private void CreatePTOffStratumCues(DTAnalyzer analyzer)
        {
            foreach (StateClassShape Shape in this.Shapes)
            {
                CreateIncomingPTOffStratumCues(Shape);
                this.CreateOutgoingPTOffStratumCues(Shape, analyzer);
            }
        }

        private void CreateOutgoingPTLines(StateClassShape fromShape, DTAnalyzer analyzer)
        {
            foreach (Transition t in fromShape.OutgoingPT)
            {
                StateClassShape ToShape = null;

                //If there is no destination state class then it is a transition-to-self

                if (!t.StateClassIdDestination.HasValue)
                {
                    ToShape = fromShape;
                }
                else
                {
                    int? StratumIdActual = null;

                    analyzer.ResolveStateClassStratum(
                        t.StratumIdSource, 
                        t.StratumIdDestination,
                        t.StateClassIdDestination.Value, 
                        ref StratumIdActual);

                    if (NullableUtilities.NullableIdsEqual(StratumIdActual, this.m_StratumId))
                    {
                        //If the class was found in the current stratum then it will be in the
                        //explicit lookups if the current stratum is explicit and the wildcard
                        //lookups if it is not.

                        if (this.m_StratumId.HasValue)
                        {
                            ToShape = this.m_ExplicitClasses[t.StateClassIdDestination.Value];
                        }
                        else
                        {
                            ToShape = this.m_WildcardClasses[t.StateClassIdDestination.Value];
                        }
                    }
                    else
                    {
                        //If the class was not found in the current stratum it will be in the
                        //wild card lookups if its stratum is wild.

                        if (this.m_StratumId.HasValue && (!StratumIdActual.HasValue))
                        {
                            ToShape = this.m_WildcardClasses[t.StateClassIdDestination.Value];
                        }
                    }
                }

                //If a shape was not found then it is an off-stratum transition.  
                //Otherwise, create the approprate line and add it to the diagram.

                if (ToShape != null)
                {
                    ProbabilisticTransitionLine Line = null;

                    if (fromShape == ToShape)
                    {
                        Line = CreatePTLineToSelf(fromShape, t.TransitionTypeId);
                    }
                    else
                    {
                        Line = new ProbabilisticTransitionLine(t.TransitionTypeId, Constants.PROBABILISTIC_TRANSITION_LINE_COLOR);
                        this.FillLineSegments(fromShape, ToShape, Line, BoxArrowDiagramConnectorMode.Vertical);
                    }

                    this.FillPTLineTransitionGroups(Line);
                    this.AddLine(Line);

                    fromShape.OutgoingPTLines.Add(Line);
                    ToShape.IncomingPTLines.Add(Line);
                }
            }
        }

        private void CreateIncomingPTOffStratumCues(StateClassShape shape)
        {
            foreach (Transition t in shape.IncomingPT)
            {
                if (!NullableUtilities.NullableIdsEqual(t.StratumIdSource, shape.StratumIdSource))
                {
                    this.AddLine(this.CreateIncomingPTOffStratumCue(shape, t));
                }
            }
        }

        private void CreateOutgoingPTOffStratumCues(StateClassShape shape, DTAnalyzer analyzer)
        {
            foreach (Transition t in shape.OutgoingPT)
            {
                //If it is a transition-to-self then it is not an off-stratum transition

                if (!t.StateClassIdDestination.HasValue)
                {
                    continue;
                }

                int? StratumIdActual = null;

                analyzer.ResolveStateClassStratum(
                    t.StratumIdSource, 
                    t.StratumIdDestination, 
                    t.StateClassIdDestination.Value, 
                    ref StratumIdActual);

                //If the class was found in the current stratum then it is not an off-stratum transition

                if (NullableUtilities.NullableIdsEqual(StratumIdActual, this.m_StratumId))
                {
                    continue;
                }

                //If the class was found in the wild card stratum then it is not an off-stratum transition

                if (!StratumIdActual.HasValue)
                {
                    continue;
                }

                this.AddLine(this.CreateOutgoingPTOffStratumCue(shape, t));
            }
        }

        private ProbabilisticTransitionLine CreateOutgoingPTOffStratumCue(StateClassShape shape, Transition t)
        {
            int X1 = shape.Bounds.X + shape.Bounds.Width;
            int Y1 = shape.Bounds.Y;
            int X2 = X1 + Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;
            int Y2 = shape.Bounds.Y - Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;

            ProbabilisticTransitionLine Line = new ProbabilisticTransitionLine(
                t.TransitionTypeId, Constants.PROBABILISTIC_TRANSITION_LINE_COLOR);

            this.FillPTLineTransitionGroups(Line);

            Line.AddLineSegment(X1, Y1, X2, Y2);
            Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Northeast);

            return Line;
        }

        private ProbabilisticTransitionLine CreateIncomingPTOffStratumCue(StateClassShape shape, Transition t)
        {
            int X1 = shape.Bounds.X - Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;
            int Y1 = shape.Bounds.Y - Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;
            int X2 = shape.Bounds.X;
            int Y2 = shape.Bounds.Y;

            ProbabilisticTransitionLine Line = new ProbabilisticTransitionLine(
                t.TransitionTypeId, Constants.PROBABILISTIC_TRANSITION_LINE_COLOR);

            this.FillPTLineTransitionGroups(Line);

            Line.AddLineSegment(X1, Y1, X2, Y2);
            Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Southeast);

            return Line;
        }

        private static ProbabilisticTransitionLine CreatePTLineToSelf(StateClassShape shape, int transitionTypeId)
        {
            ProbabilisticTransitionLine Line = new ProbabilisticTransitionLine(
                transitionTypeId, Constants.PROBABILISTIC_TRANSITION_LINE_COLOR);

            const int PT_CIRCLE_RADIUS = 10;

            int lrx = shape.Bounds.X + shape.Bounds.Width - PT_CIRCLE_RADIUS;
            int lry = shape.Bounds.Y + shape.Bounds.Height - PT_CIRCLE_RADIUS;
            Rectangle rc = new Rectangle(lrx, lry, 2 * PT_CIRCLE_RADIUS, 2 * PT_CIRCLE_RADIUS);

            Line.AddEllipse(rc);

            return Line;
        }

        private static Transition CreatePT(DataRow dr)
        {
            int? Iteration = null;
            int? Timestep = null;
            int? StratumIdSource = null;
            int StateClassIdSource = Convert.ToInt32(dr[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
            int? StratumIdDest = null;
            int? StateClassIdDest = null;
            int? SecondaryStratumId = null;
            int? TertiaryStratumId = null;
            double Propn = 1.0;
            int AgeMin = 0;
            int AgeMax = int.MaxValue;
            int AgeRel = 0;
            bool AgeReset = true;
            int TstMin = 0;
            int TstMax = int.MaxValue;
            int TstRel = 0;

            if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
            {
                Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
            {
                Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME] != DBNull.Value)
            {
                StratumIdSource = Convert.ToInt32(dr[Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME] != DBNull.Value)
            {
                StratumIdDest = Convert.ToInt32(dr[Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME] != DBNull.Value)
            {
                StateClassIdDest = Convert.ToInt32(dr[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                SecondaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                TertiaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            Transition pt = new Transition(
                Iteration, Timestep, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest, 
                SecondaryStratumId, TertiaryStratumId, 
                Convert.ToInt32(dr[Strings.DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture), 
                Convert.ToDouble(dr[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME], CultureInfo.InvariantCulture), 
                Propn, AgeMin, AgeMax, AgeRel, AgeReset, TstMin, TstMax, TstRel);

            pt.PropnWasNull = true;
            pt.AgeMinWasNull = true;
            pt.AgeMaxWasNull = true;
            pt.AgeRelativeWasNull = true;
            pt.AgeResetWasNull = true;
            pt.TstMinimumWasNull = true;
            pt.TstMaximumWasNull = true;
            pt.TstRelativeWasNull = true;

            if (dr[Strings.DATASHEET_PT_PROPORTION_COLUMN_NAME] != DBNull.Value)
            {
                pt.Proportion = Convert.ToDouble(dr[Strings.DATASHEET_PT_PROPORTION_COLUMN_NAME], CultureInfo.InvariantCulture);
                pt.PropnWasNull = false;
            }

            if (dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] != DBNull.Value)
            {
                pt.AgeMinimum = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                pt.AgeMinWasNull = false;
            }

            if (dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] != DBNull.Value)
            {
                pt.AgeMaximum = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                pt.AgeMaxWasNull = false;
            }

            if (dr[Strings.DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME] != DBNull.Value)
            {
                pt.AgeRelative = Convert.ToInt32(dr[Strings.DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME], CultureInfo.InvariantCulture);
                pt.AgeRelativeWasNull = false;
            }

            if (dr[Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME] != DBNull.Value)
            {
                pt.AgeReset = DataTableUtilities.GetDataBool(dr[Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME]);
                pt.AgeResetWasNull = false;
            }

            if (dr[Strings.DATASHEET_PT_TST_MIN_COLUMN_NAME] != DBNull.Value)
            {
                pt.TstMinimum = Convert.ToInt32(dr[Strings.DATASHEET_PT_TST_MIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                pt.TstMinimumWasNull = false;
            }

            if (dr[Strings.DATASHEET_PT_TST_MAX_COLUMN_NAME] != DBNull.Value)
            {
                pt.TstMaximum = Convert.ToInt32(dr[Strings.DATASHEET_PT_TST_MAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                pt.TstMaximumWasNull = false;
            }

            if (dr[Strings.DATASHEET_PT_TST_RELATIVE_COLUMN_NAME] != DBNull.Value)
            {
                pt.TstRelative = Convert.ToInt32(dr[Strings.DATASHEET_PT_TST_RELATIVE_COLUMN_NAME], CultureInfo.InvariantCulture);
                pt.TstRelativeWasNull = false;
            }

            return pt;
        }

        private DataRow CreatePTRecord(
            int? stratumIdSource, int stateClassIdSource, int? stratumIdDest, int? stateClassIdDest, 
            int transitionTypeId, double probability, double? proportion, int? ageMin, int? ageMax, 
            int? ageRelative, bool? ageReset, int? tstMin, int? tstMax, int? tstRelative)
        {
            DataRow dr = this.m_PTDataSheet.GetData().NewRow();

            dr[Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(stratumIdSource);
            dr[Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME] = stateClassIdSource;
            dr[Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(stratumIdDest);
            dr[Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(stateClassIdDest);
            dr[Strings.DATASHEET_PT_TRANSITION_TYPE_COLUMN_NAME] = transitionTypeId;
            dr[Strings.DATASHEET_PT_PROBABILITY_COLUMN_NAME] = probability;
            dr[Strings.DATASHEET_PT_PROPORTION_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(proportion);
            dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(ageMin);
            dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(ageMax);
            dr[Strings.DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(ageRelative);
            dr[Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(ageReset);
            dr[Strings.DATASHEET_PT_TST_MIN_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(tstMin);
            dr[Strings.DATASHEET_PT_TST_MAX_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(tstMax);
            dr[Strings.DATASHEET_PT_TST_RELATIVE_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(tstRelative);

#if DEBUG
            if (dr[Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME] != DBNull.Value)
            {
                Debug.Assert(
                    Convert.ToInt32(dr[Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME], CultureInfo.InvariantCulture) == 0 || 
                    Convert.ToInt32(dr[Strings.DATASHEET_PT_AGE_RESET_COLUMN_NAME], CultureInfo.InvariantCulture) == -1);
            }
#endif

            this.m_PTDataSheet.GetData().Rows.Add(dr);
            return dr;
        }
    }
}
