// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using SyncroSim.Common.Forms;

namespace SyncroSim.STSim
{
    internal partial class TransitionDiagram
    {
        private void DrawDTLines(System.Drawing.Graphics g)
        {
            foreach (ConnectorLine line in this.Lines)
            {
                if ((line) is DeterministicTransitionLine)
                {
                    if (line.IsVisible)
                    {
                        line.Render(g);
                    }
                }
            }
        }

        private DataRow[] GetIncomingDT(StateClassShape shape)
        {
            string Query = null;

            if (shape.StratumIdSource.HasValue)
            {
                Query = string.Format(CultureInfo.InvariantCulture, 
                    "((StratumIDDest={0} AND StateClassIDDest={1}) OR (StratumIDDest IS NULL AND StratumIDSource={0} AND StateClassIDDest={1}))", 
                    shape.StratumIdSource.Value, shape.StateClassIdSource);
            }
            else
            {
                Query = string.Format(CultureInfo.InvariantCulture, 
                    "StratumIDSource IS NULL AND StateClassIDDest={0}", 
                    shape.StateClassIdSource);
            }

            return this.m_DTDataSheet.GetData().Select(Query, null);
        }

        private void FillIncomingDT(StateClassShape shape)
        {
            DataRow[] rows = this.GetIncomingDT(shape);

            foreach (DataRow dr in rows)
            {
                shape.IncomingDT.Add(CreateDT(dr));
            }
        }

        private static bool IsDTToSelf(DataRow dr)
        {
            int? StratumIdSource = null;
            int StateClassIdSource = 0;
            int? StratumIdDest = null;
            int? StateClassIdDest = null;

            DTAnalyzer.GetDTFieldValues(dr, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

            if (!StateClassIdDest.HasValue)
            {
                return true;
            }

            if (StateClassIdSource == StateClassIdDest.Value)
            {
                if (!StratumIdDest.HasValue)
                {
                    return true;
                }
                else
                {
                    return NullableUtilities.NullableIdsEqual(StratumIdSource, StratumIdDest);
                }
            }

            return false;
        }

        private void CreateDTLines(DTAnalyzer analyzer)
        {
            foreach (StateClassShape Shape in this.Shapes)
            {
                this.CreateOutgoingDTLines(Shape, analyzer);
            }
        }

        private void CreateDTOffStratumCues(DTAnalyzer analyzer)
        {
            foreach (StateClassShape Shape in this.Shapes)
            {
                this.CreateIncomingDTOffStratumCues(Shape);
                this.CreateOutgoingDTOffStratumCues(Shape, analyzer);
            }
        }

        private void CreateOutgoingDTLines(StateClassShape fromShape, DTAnalyzer analyzer)
        {
            //If there is no destination state class then it is a transition-to-self

            if (!fromShape.StateClassIdDest.HasValue)
            {
                return;
            }

            StateClassShape ToShape = null;
            int? StratumIdActual = null;

            analyzer.ResolveStateClassStratum(fromShape.StratumIdSource, fromShape.StratumIdDest, fromShape.StateClassIdDest.Value, ref StratumIdActual);

            if (NullableUtilities.NullableIdsEqual(StratumIdActual, this.m_StratumId))
            {
                //If the class was found in the current stratum then it will be in the
                //explicit lookups if the current stratum is explicit and the wildcard
                //lookups if it is not.

                if (this.m_StratumId.HasValue)
                {
                    ToShape = this.m_ExplicitClasses[fromShape.StateClassIdDest.Value];
                }
                else
                {
                    ToShape = this.m_WildcardClasses[fromShape.StateClassIdDest.Value];
                }
            }
            else
            {
                //If the class was not found in the current stratum it will be in the
                //wild card lookups if its stratum is wild.

                if (this.m_StratumId.HasValue && (!StratumIdActual.HasValue))
                {
                    ToShape = this.m_WildcardClasses[fromShape.StateClassIdDest.Value];
                }
            }

            //If the class was not found or it is a transition-to-self then it is an 
            //off stratum transition.  Otherwise, add an outgoing line.

            if ((ToShape != null) && (ToShape != fromShape))
            {
                DeterministicTransitionLine Line = new DeterministicTransitionLine(Constants.DETERMINISTIC_TRANSITION_LINE_COLOR);

                this.FillLineSegments(fromShape, ToShape, Line, BoxArrowDiagramConnectorMode.Horizontal);
                this.AddLine(Line);

                fromShape.OutgoingDTLines.Add(Line);
                ToShape.IncomingDTLines.Add(Line);
            }
        }

        private void CreateIncomingDTOffStratumCues(StateClassShape shape)
        {
            foreach (DeterministicTransition t in shape.IncomingDT)
            {
                if (!NullableUtilities.NullableIdsEqual(t.StratumIdSource, shape.StratumIdSource))
                {
                    if (t.StratumIdSource.HasValue)
                    {
                        DeterministicTransitionLine l = CreateIncomingDTOffStratumCue(shape);
                        this.AddLine(l);
                    }
                }
            }
        }

        private void CreateOutgoingDTOffStratumCues(StateClassShape shape, DTAnalyzer analyzer)
        {
            //If there is no destination state class then it is a transition-to-self

            if (!shape.StateClassIdDest.HasValue)
            {
                return;
            }

            int? StratumIdActual = null;

            analyzer.ResolveStateClassStratum(shape.StratumIdSource, shape.StratumIdDest, shape.StateClassIdDest.Value, ref StratumIdActual);

            //If the class was found in the current stratum then it is not an off-stratum transition

            if (NullableUtilities.NullableIdsEqual(StratumIdActual, this.m_StratumId))
            {
                return;
            }

            //If the class was found in the wild card stratum then it is not an off-stratum transition

            if (!StratumIdActual.HasValue)
            {
                return;
            }

            this.AddLine(CreateOutgoingDTOffStratumCue(shape));
        }

        private static DeterministicTransitionLine CreateOutgoingDTOffStratumCue(StateClassShape shape)
        {
            int X1 = shape.Bounds.X + shape.Bounds.Width;
            int Y1 = shape.Bounds.Y + shape.Bounds.Height;
            int X2 = X1 + Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;
            int Y2 = Y1 + Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;

            DeterministicTransitionLine Line = new DeterministicTransitionLine(Constants.DETERMINISTIC_TRANSITION_LINE_COLOR);

            Line.AddLineSegment(X1, Y1, X2, Y2);
            Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Southeast);

            return Line;
        }

        private static DeterministicTransitionLine CreateIncomingDTOffStratumCue(StateClassShape shape)
        {
            int X1 = shape.Bounds.X - Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;
            int Y1 = shape.Bounds.Y + shape.Bounds.Height + Constants.TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE;
            int X2 = shape.Bounds.X;
            int Y2 = shape.Bounds.Y + shape.Bounds.Height;
            DeterministicTransitionLine Line = new DeterministicTransitionLine(Constants.DETERMINISTIC_TRANSITION_LINE_COLOR);

            Line.AddLineSegment(X1, Y1, X2, Y2);
            Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Northeast);

            return Line;
        }

        private DataRow[] GetDTRows()
        {
            string Query = null;

            if (this.m_StratumId.HasValue)
            {
                Query = string.Format(CultureInfo.InvariantCulture, 
                    "{0}={1} OR {2} IS NULL", 
                    Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME, 
                    this.m_StratumId, 
                    Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME);
            }
            else
            {
                Query = string.Format(CultureInfo.InvariantCulture, 
                    "{0} IS NULL", 
                    Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME);
            }

            string SortOrder = string.Format(CultureInfo.InvariantCulture, 
                "{0},{1}", 
                Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME, 
                Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME);

            return this.m_DTDataSheet.GetData().Select(Query, SortOrder);
        }

        private static DeterministicTransition CreateDT(DataRow dr)
        {
            int? Iteration = null;
            int? Timestep = null;
            int? StratumIdSource = null;
            int StateClassIdSource = Convert.ToInt32(dr[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME]);
            int? StratumIdDest = null;
            int? StateClassIdDest = null;
            int AgeMinimum = 0;
            int AgeMaximum = int.MaxValue;

            if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
            {
                Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME]);
            }

            if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
            {
                Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME]);
            }

            if (dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] != DBNull.Value)
            {
                StratumIdSource = Convert.ToInt32(dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME]);
            }

            if (dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME] != DBNull.Value)
            {
                StratumIdDest = Convert.ToInt32(dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME]);
            }

            if (dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME] != DBNull.Value)
            {
                StateClassIdDest = Convert.ToInt32(dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME]);
            }

            if (dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] != DBNull.Value)
            {
                AgeMinimum = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME]);
            }

            if (dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] != DBNull.Value)
            {
                AgeMaximum = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME]);
            }

            DeterministicTransition dt = new DeterministicTransition(
                Iteration, Timestep, StratumIdSource, StateClassIdSource, 
                StratumIdDest, StateClassIdDest, AgeMinimum, AgeMaximum);

            return dt;
        }

        private DataRow CreateDTRecord(int stateClassIdSource, int? stratumIdDestination, int? stateClassIdDestination, int? ageMinimum, int? ageMaximum, int row, int column)
        {
            DataRow dr = this.m_DTDataSheet.GetData().NewRow();

            dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(this.m_StratumId);
            dr[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME] = stateClassIdSource;
            dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(stratumIdDestination);
            dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(stateClassIdDestination);
            dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(ageMinimum);
            dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(ageMaximum);
            dr[Strings.DATASHEET_DT_LOCATION_COLUMN_NAME] = RowColToLocation(row, column);

            this.m_DTDataSheet.GetData().Rows.Add(dr);
            return dr;
        }
    }
}
