// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;

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
