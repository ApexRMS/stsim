// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    public class TransitionEx : Transition
    {
        internal bool PropnWasNull;
        internal bool AgeMinWasNull;
        internal bool AgeMaxWasNull;
        internal bool AgeRelativeWasNull;
        internal bool AgeResetWasNull;
        internal bool TstMinimumWasNull;
        internal bool TstMaximumWasNull;
        internal bool TstRelativeWasNull;

        public TransitionEx(
            int? iteration, int? timestep, int? stratumIdSource, int stateClassIdSource, int? stratumIdDestination, int? stateClassIdDestination, 
            int? secondaryStratumId, int? tertiaryStratumId, int transitionTypeId, double probability, double proportion, int ageMinimum, 
            int ageMaximum, int ageRelative, bool ageReset, int tstMinimum, int tstMaximum, int tstRelative) : 
                base(iteration, timestep, stratumIdSource, stateClassIdSource, stratumIdDestination, stateClassIdDestination,
                secondaryStratumId, tertiaryStratumId, transitionTypeId, probability, proportion, ageMinimum,
                ageMaximum, ageRelative, ageReset, tstMinimum, tstMaximum, tstRelative)

        {

        }
    }
}
