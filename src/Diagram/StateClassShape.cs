// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Drawing;
using SyncroSim.Core;
using SyncroSim.Common.Forms;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class StateClassShape : BoxDiagramShape
    {
        private Project m_Project;
        private int? m_StratumIdSource;
        private int m_StateClassIdSource;
        private int? m_StratumIdDest;
        private int? m_StateClassIdDest;
        private int? m_AgeMinimum;
        private int? m_AgeMaximum;
        private int m_SLXId;
        private string m_SLXDisplayName;
        private int m_SLYId;
        private string m_SLYDisplayName;
        private string m_TooltipText;
        private List<DeterministicTransition> m_IncomingDT = new List<DeterministicTransition>();
        private List<DeterministicTransitionLine> m_IncomingDTLines = new List<DeterministicTransitionLine>();
        private List<DeterministicTransitionLine> m_OutgoingDTLines = new List<DeterministicTransitionLine>();
        private List<Transition> m_IncomingPT = new List<Transition>();
        private List<ProbabilisticTransitionLine> m_IncomingPTLines = new List<ProbabilisticTransitionLine>();
        private List<Transition> m_OutgoingPT = new List<Transition>();
        private List<ProbabilisticTransitionLine> m_OutgoingPTLines = new List<ProbabilisticTransitionLine>();
        private Pen m_StaticBorderPen = new Pen(Constants.TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR, ZOOM_SAFE_PEN_WIDTH);
        private SolidBrush m_StaticBkBrush = new SolidBrush(Constants.TRANSITION_DIAGRAM_SHAPE_BACKGROUND_COLOR);
        private bool m_SharesLocation;
        private DataSheetMonitor m_Monitor;
        private bool m_IsDisposed;
        private const int ZOOM_SAFE_PEN_WIDTH = -1;

        public StateClassShape(Project project, int? stratumIdSource, int stateClassIdSource, int? stratumIdDestination, int? stateClassIdDestination, int? ageMinimum, int? ageMaximum, int stateLabelXId, string stateLabelXName, string stateLabelXDisplayName, int stateLabelYId, string stateLabelYDisplayName) : base(Constants.TRANSITION_DIAGRAM_NUM_VERTICAL_CONNECTORS, Constants.TRANSITION_DIAGRAM_NUM_HORIZONTAL_CONNECTORS)
        {
            this.m_Project = project;
            this.TitleBarText = stateLabelXName;
            this.m_StratumIdSource = stratumIdSource;
            this.m_StateClassIdSource = stateClassIdSource;
            this.m_StratumIdDest = stratumIdDestination;
            this.m_StateClassIdDest = stateClassIdDestination;
            this.m_AgeMinimum = ageMinimum;
            this.m_AgeMaximum = ageMaximum;
            this.m_SLXId = stateLabelXId;
            this.m_SLXDisplayName = stateLabelXDisplayName;
            this.m_SLYId = stateLabelYId;
            this.m_SLYDisplayName = stateLabelYDisplayName;
            this.TitleHeight = Constants.TRANSITION_DIAGRAM_TITLE_BAR_HEIGHT;
            this.ItemHeight = Constants.TRANSITION_DIAGRAM_ITEM_HEIGHT;
            this.BackgroundColor = Constants.TRANSITION_DIAGRAM_SHAPE_BACKGROUND_COLOR;
            this.SelectedBackgroundColor = this.BackgroundColor;
            this.TitleBackgroundColor = this.BackgroundColor;
            this.TitleSelectedBackgroundColor = this.BackgroundColor;
            this.TitleTextColor = Constants.TRANSITION_DIAGRAM_TEXT_COLOR;
            this.TitleSelectedTextColor = Constants.TRANSITION_DIAGRAM_SELECTED_TEXT_COLOR;
            this.BorderColor = Constants.TRANSITION_DIAGRAM_SHAPE_BORDER_COLOR;
            this.DrawItemSeparators = false;

            this.m_Monitor = new DataSheetMonitor(this.m_Project, Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged);
            this.m_Monitor.Invoke();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.m_IsDisposed)
            {
                if (this.m_Monitor != null)
                {
                    this.m_Monitor.Dispose();
                }

                if (this.m_StaticBorderPen != null)
                {
                    this.m_StaticBorderPen.Dispose();
                }

                if (this.m_StaticBkBrush != null)
                {
                    this.m_StaticBkBrush.Dispose();
                }

                this.m_IsDisposed = true;
            }

            base.Dispose(disposing);
        }

        public int? StratumIdSource
        {
            get
            {
                return this.m_StratumIdSource;
            }
        }

        public int StateClassIdSource
        {
            get
            {
                return this.m_StateClassIdSource;
            }
        }

        public int? StratumIdDest
        {
            get
            {
                return this.m_StratumIdDest;
            }
        }

        public int? StateClassIdDest
        {
            get
            {
                return this.m_StateClassIdDest;
            }
        }

        public int? AgeMinimum
        {
            get
            {
                return this.m_AgeMinimum;
            }
        }

        public int? AgeMaximum
        {
            get
            {
                return this.m_AgeMaximum;
            }
        }

        public int StateLabelXId
        {
            get
            {
                return this.m_SLXId;
            }
        }

        public int StateLabelYId
        {
            get
            {
                return this.m_SLYId;
            }
        }

        public List<Transition> OutgoingPT
        {
            get
            {
                return this.m_OutgoingPT;
            }
        }

        public List<ProbabilisticTransitionLine> OutgoingPTLines
        {
            get
            {
                return this.m_OutgoingPTLines;
            }
        }

        public List<Transition> IncomingPT
        {
            get
            {
                return this.m_IncomingPT;
            }
        }

        public List<ProbabilisticTransitionLine> IncomingPTLines
        {
            get
            {
                return this.m_IncomingPTLines;
            }
        }

        public List<DeterministicTransition> IncomingDT
        {
            get
            {
                return this.m_IncomingDT;
            }
        }

        public List<DeterministicTransitionLine> IncomingDTLines
        {
            get
            {
                return this.m_IncomingDTLines;
            }
        }

        public List<DeterministicTransitionLine> OutgoingDTLines
        {
            get
            {
                return this.m_OutgoingDTLines;
            }
        }

        public bool SharesLocation
        {
            set
            {
                this.m_SharesLocation = value;
            }
        }

        public override string GetToolTipText()
        {
            return this.m_TooltipText;
        }

        public override void Render(System.Drawing.Graphics g)
        {
            if (this.m_SharesLocation && !this.IsSelected)
            {
                Rectangle rc = new Rectangle(this.Bounds.Left - 8, this.Bounds.Top - 8, this.Bounds.Width, this.Bounds.Height);

                g.FillRectangle(this.m_StaticBkBrush, rc);
                g.DrawRectangle(this.m_StaticBorderPen, rc);
            }

            base.Render(g);
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            string slxlabel = null;
            string slylabel = null;

            DataSheet ds = this.m_Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            TerminologyUtilities.GetStateLabelTerminology(ds, ref slxlabel, ref slylabel);

            this.m_TooltipText = slxlabel + ": " + this.m_SLXDisplayName + Environment.NewLine + slylabel + ": " + this.m_SLYDisplayName;
        }
    }
}
