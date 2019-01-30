// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal partial class TransitionDiagram
    {
        private static void LocationToRowCol(string location, ref int row, ref int column)
        {
            string LocUpper = location.ToUpper(CultureInfo.InvariantCulture);

            string CharPart = LocUpper.Substring(0, 1);
            string NumPart = LocUpper.Substring(1, LocUpper.Length - 1);

            char[] chars = CharPart.ToCharArray();
            char c = chars[0];
            int CharVal = ((int)c - (int)'A');
            column = CharVal;
            row = int.Parse(NumPart, CultureInfo.InvariantCulture) - 1;

            Debug.Assert(column >= 0 && row >= 0);
        }

        private static string RowColToLocation(int row, int column)
        {
            Debug.Assert(column < 26);

            string s = Convert.ToString((char)((int)'A' + column), CultureInfo.InvariantCulture);
            s = s + (row + 1).ToString(CultureInfo.InvariantCulture);

            return s;
        }

        private void RecordStateClassLocation(StateClassShape shape, DTAnalyzer analyzer)
        {
            Debug.Assert(this.WorkspaceRectangle.Contains(shape.Bounds));

            DataRow row = analyzer.GetStateClassRow(this.m_StratumId, shape.StateClassIdSource);
            row[Strings.DATASHEET_DT_LOCATION_COLUMN_NAME] = RowColToLocation(shape.Row, shape.Column);
        }

        private void RecordNewStateClassLocations(DTAnalyzer analyzer)
        {
            foreach (StateClassShape Shape in this.SelectedShapes)
            {
                if (!Shape.IsStatic)
                {
                    this.RecordStateClassLocation(Shape, analyzer);
                }
            }
        }

        private string GetNextStateClassLocation()
        {
            if (this.GetShapeAt(this.CurrentMouseRow, this.CurrentMouseColumn) == null)
            {
                string ColLetter = Convert.ToString((char)((int)'A' + this.CurrentMouseColumn), CultureInfo.InvariantCulture);
                string RowLetter = Convert.ToString(this.CurrentMouseRow + 1, CultureInfo.InvariantCulture);

                return ColLetter + RowLetter;
            }

            for (int col = 0; col < Constants.TRANSITION_DIAGRAM_MAX_COLUMNS; col++)
            {
                for (int row = 0; row < Constants.TRANSITION_DIAGRAM_MAX_ROWS; row++)
                {
                    if (this.GetShapeAt(row, col) == null)
                    {
                        string ColLetter = Convert.ToString((char)((int)'A' + col), CultureInfo.InvariantCulture);
                        string RowLetter = (row + 1).ToString(CultureInfo.InvariantCulture);

                        return (ColLetter + RowLetter);
                    }
                }
            }

            return null;
        }

        private int GetStateClassId(int slxid, int slyid)
        {
            foreach (DataRow dr in this.m_SCDataSheet.GetData().Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int xid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                int yid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (xid == slxid && yid == slyid)
                {
                    return Convert.ToInt32(dr[this.m_SCDataSheet.ValueMember], CultureInfo.InvariantCulture);
                }
            }

            return -1;
        }

        private static bool IsTransitionGroupFilterApplied(TransitionFilterCriteria criteria)
        {
            foreach (bool b in criteria.TransitionGroups.Values)
            {
                if (!b)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
