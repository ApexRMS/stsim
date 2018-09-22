// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
#if DEBUG

        /// <summary>
        /// Validates the shufflable transition groups
        /// </summary>
        /// <remarks></remarks>
        private void VALIDATE_SHUFFLABLE_GROUPS()
        {
            Dictionary<int, bool> d = new Dictionary<int, bool>();

            foreach (TransitionGroup tg in this.m_ShufflableTransitionGroups)
            {
                d.Add(tg.TransitionGroupId, true);
            }
        }

        /// <summary>
        /// Validates that the specified cell has been initialized correctly
        /// </summary>
        /// <param name="c"></param>
        /// <remarks></remarks>
        private void VALIDATE_INITIALIZED_CELL(Cell c, int iteration, int timestep)
        {
            //Make sure the ages are within the correct initial ranges
            DeterministicTransition dt = this.GetDeterministicTransition(c, iteration, timestep);

            if (dt != null)
            {
                Debug.Assert(c.Age >= dt.AgeMinimum);
                Debug.Assert(c.Age <= dt.AgeMaximum);
            }

            Debug.Assert(c.Age >= 0);
        }

        /// <summary>
        /// Validates that the specified cell has the correct transitions
        /// </summary>
        /// <param name="c"></param>
        /// <remarks></remarks>
        private void VALIDATE_CELL_TRANSITIONS(Cell c, int iteration, int timestep)
        {
            TransitionCollection trlist = this.GetTransitionCollection(c, iteration, timestep);

            if (trlist == null)
            {
                Debug.Assert(c.Transitions.Count == 0);
            }
            else
            {
                Debug.Assert(trlist.Count > 0);
                int tcount = 0;

                foreach (Transition tr in trlist)
                {
                    if (c.Age < tr.AgeMinimum)
                    {
                        continue;
                    }

                    if (c.Age > tr.AgeMaximum)
                    {
                        continue;
                    }

                    if (!this.CompareTstValues(c, tr))
                    {
                        continue;
                    }

                    Debug.Assert(c.Transitions.Contains(tr));
                    tcount += 1;
                }

                Debug.Assert(tcount == c.Transitions.Count);

                foreach (Transition tr in c.Transitions)
                {
                    Debug.Assert(trlist.Contains(tr));
                }
            }
        }

#endif
    }
}
