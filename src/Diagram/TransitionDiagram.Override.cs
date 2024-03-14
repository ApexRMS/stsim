// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Drawing;
using System.Windows.Forms;
using SyncroSim.Apex.Forms;

namespace SyncroSim.STSim
{
    internal partial class TransitionDiagram
    {
        protected override void OnDropSelectedShapes(DiagramDragEventArgs e)
        {
            base.OnDropSelectedShapes(e);

            DTAnalyzer Analyzer = new DTAnalyzer(this.m_DTDataSheet.GetData(), this.m_DataFeed.Project);

            this.SaveSelection();
            this.RecordNewStateClassLocations(Analyzer);
            this.InternalRefreshLookups();
            this.InternalRefreshTransitionLines();

            //DEVTODO: It would be better not to do this, but there is no obvious way to tell
            //if the user is about to drop the shapes they are dragging.  Is there?

            this.m_DTDataSheet.BeginModifyRows();
            this.m_DTDataSheet.EndModifyRows();

            this.RestoreSelection();
            this.Focus();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.m_IsFilterApplied)
            {
                e.Graphics.DrawImage(Properties.Resources.Filter16x16, new Point(4, 4));
            }
        }

        protected override void DrawLines(System.Drawing.Graphics g)
        {
            this.DrawPTLines(g, false);
            this.DrawPTLines(g, true);
            this.DrawDTLines(g);
        }

        protected override void OnShapeSelectionChanged()
        {
            base.OnShapeSelectionChanged();

            this.InternalResetTransitionLines();
            this.m_SelectionStatic = true;

            foreach (StateClassShape Shape in this.SelectedShapes)
            {
                if (!Shape.IsStatic)
                {
                    this.m_SelectionStatic = false;
                }

                if (ModifierKeys == Keys.Shift)
                {
                    foreach (DeterministicTransitionLine l in Shape.IncomingDTLines)
                    {
                        l.IsSelected = true;
                        l.LineColor = Constants.TRANSITION_SELECTED_LINE_COLOR;
                    }

                    foreach (ProbabilisticTransitionLine l in Shape.IncomingPTLines)
                    {
                        l.IsSelected = true;
                        l.LineColor = Constants.TRANSITION_SELECTED_LINE_COLOR;
                    }
                }
                else
                {
                    foreach (DeterministicTransitionLine l in Shape.OutgoingDTLines)
                    {
                        l.IsSelected = true;
                        l.LineColor = Constants.TRANSITION_SELECTED_LINE_COLOR;
                    }

                    foreach (ProbabilisticTransitionLine l in Shape.OutgoingPTLines)
                    {
                        l.IsSelected = true;
                        l.LineColor = Constants.TRANSITION_SELECTED_LINE_COLOR;
                    }
                }
            }

            this.Invalidate();
        }
    }
}
