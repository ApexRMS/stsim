// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class IntervalMeanTimestepMap
    {
        private Dictionary<int, int> m_Map = new Dictionary<int, int>();
        private int m_MinimumTimestep;
        private int m_MaximumTimestep;
        private int m_TimestepZero;
        private int m_Frequency;

        public IntervalMeanTimestepMap(int minimumTimestep, int maximumTimestep, int timestepZero, int frequency)
        {
            this.m_MinimumTimestep = minimumTimestep;
            this.m_MaximumTimestep = maximumTimestep;
            this.m_TimestepZero = timestepZero;
            this.m_Frequency = frequency;

            this.FillMap();
        }

        public int GetValue(int value)
        {
            return this.m_Map[value];
        }

        private void FillMap()
        {
            //Handle special cases that are relatively common and/or very simple
            if (this.m_MinimumTimestep == this.m_MaximumTimestep)
            {
                this.m_Map.Add(this.m_MinimumTimestep, this.m_MaximumTimestep);
            }
            else if (this.m_Frequency == 1)
            {
                for (int CurrentTimestep = this.m_MinimumTimestep; CurrentTimestep <= this.m_MaximumTimestep; CurrentTimestep++)
                {
                    this.m_Map.Add(CurrentTimestep, CurrentTimestep);
                }
            }
            else
            {
                int CurrentTimestep = this.m_MinimumTimestep;

                while (CurrentTimestep <= this.m_MaximumTimestep)
                {
                    int AggregatorTimestep = this.GetNextAggregatorTimestep(CurrentTimestep);
                    Debug.Assert(AggregatorTimestep <= this.m_MaximumTimestep);

                    while (CurrentTimestep <= AggregatorTimestep)
                    {
                        this.m_Map.Add(CurrentTimestep, AggregatorTimestep);
                        CurrentTimestep += 1;
                    }
                }
            }

#if DEBUG

            Debug.Assert(this.m_Map.Count == (this.m_MaximumTimestep - this.m_MinimumTimestep + 1));

            for (int t = this.m_MinimumTimestep; t <= this.m_MaximumTimestep; t++)
            {
                Debug.Assert(this.m_Map.ContainsKey(t));
            }

#endif
        }

        /// <summary>
        /// Gets the next 'Aggregator' timestep
        /// </summary>
        /// <param name="currentAggregatorTimestep"></param>
        /// <returns></returns>
        /// <remarks>This function exists to support the 'calculate as interval mean values' feature for summary transition output.</remarks>
        private int GetNextAggregatorTimestep(int currentAggregatorTimestep)
        {
            int NextAggregatorTimestep = currentAggregatorTimestep;

            while (NextAggregatorTimestep < this.m_MaximumTimestep)
            {
                if (((NextAggregatorTimestep - this.m_TimestepZero) % this.m_Frequency) == 0)
                {
                    return NextAggregatorTimestep;
                }

                NextAggregatorTimestep += 1;
            }

            Debug.Assert(NextAggregatorTimestep == this.m_MaximumTimestep);
            return NextAggregatorTimestep;
        }
    }
}
