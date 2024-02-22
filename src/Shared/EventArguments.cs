// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public class STSimEventArgs : EventArgs
    {
        private int m_Iteration;
        private int m_Timestep;

        internal STSimEventArgs(int iteration, int timestep)
        {
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
        }

        public int Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }
    }

    public class CellEventArgs : STSimEventArgs
    {
        private Cell m_SimulationCell;

        internal CellEventArgs(Cell simulationCell, int iteration, int timestep) : base(iteration, timestep)
        {
            this.m_SimulationCell = simulationCell;
        }

        public Cell SimulationCell
        {
            get
            {
                return this.m_SimulationCell;
            }
        }
    }

    public class CellChangeEventArgs : CellEventArgs
    {
        private DeterministicTransition m_DeterministicPathway;
        private Transition m_ProbabilisticPathway;

        internal CellChangeEventArgs(
            Cell simulationCell, int iteration, int timestep, DeterministicTransition deterministicPathway, 
            Transition probabilisticPathway) : base(simulationCell, iteration, timestep)
        {
            this.m_DeterministicPathway = deterministicPathway;
            this.m_ProbabilisticPathway = probabilisticPathway;
        }

        public DeterministicTransition DeterministicPathway
        {
            get
            {
                return this.m_DeterministicPathway;
            }
        }

        public Transition ProbabilisticPathway
        {
            get
            {
                return this.m_ProbabilisticPathway;
            }
        }
    }

    public class SpatialTransitionEventArgs : STSimEventArgs
    {
        internal SpatialTransitionEventArgs(int iteration, int timestep) : base(iteration, timestep)
        {
        }
    }

    public class SpatialTransitionEventArgsEx : STSimEventArgs
    {
        private TransitionGroup m_TransitionGroup;
        private Cell m_SimulationCell;

        public TransitionGroup TransitionGroup
        {
            get
            {
                return this.m_TransitionGroup;
            }
        }
        public Cell SimulationCell
        {
            get
            {
                return this.m_SimulationCell;
            }
        }
        internal SpatialTransitionEventArgsEx(int iteration, int timestep, TransitionGroup tg, Cell cell) : base(iteration, timestep)
        {
            this.m_TransitionGroup = tg;
            this.m_SimulationCell = cell;
        }
    }

    public class SpatialTransitionGroupEventArgs : STSimEventArgs
    {
        private TransitionGroup m_TransitionGroup;
        private bool m_Cancel;
        private CellCollection m_Cells;
        private Transition m_Transition;
        private int[] m_TransitionedPixels;
        private Dictionary<int, double[]> m_RasterTransitionAttrValues;

        public TransitionGroup TransitionGroup
        {
            get
            {
                return this.m_TransitionGroup;
            }
        }

        public bool Cancel
        {
            get
            {
                return this.m_Cancel;
            }

            set
            {
                this.m_Cancel = value;
            }
        }

        public CellCollection Cells
        {
            get
            {
                return this.m_Cells;
            }
            set
            {
                this.m_Cells = value;
            }
        }

        public Transition Transition
        {
            get
            {
                return this.m_Transition;
            }
            set
            {
                this.m_Transition = value;
            }
        }

        public int[] TransitionedPixels
        {
            get
            {
                return this.m_TransitionedPixels;
            }
        }

        public Dictionary<int, double[]> RasterTransitionAttrValues
        {
            get
            {
                return this.m_RasterTransitionAttrValues;
            }
        }

        internal SpatialTransitionGroupEventArgs(int iteration, int timestep, TransitionGroup tg, int[] tpixels, Dictionary<int, double[]> rtav) : base(iteration, timestep)
        {
            this.m_TransitionGroup = tg;
            this.m_TransitionedPixels = tpixels;
            this.m_RasterTransitionAttrValues = rtav;
        }
    }

    public class MultiplierEventArgs : CellEventArgs
    {
        private int m_TransitionGroupId;
        private double m_Multiplier = 1.0;

        internal MultiplierEventArgs(
            Cell simulationCell, int iteration, int timestep, int transitionGroupId) : base(simulationCell, iteration, timestep)
        {
            this.m_TransitionGroupId = transitionGroupId;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public double Multiplier
        {
            get
            {
                return this.m_Multiplier;
            }
        }

        public void ApplyMultiplier(double value)
        {
            this.m_Multiplier *= value;
        }
    }
}
