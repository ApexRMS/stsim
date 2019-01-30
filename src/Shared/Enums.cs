// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    /// <summary>
    /// Terminolgy Unit
    /// </summary>
    /// <remarks></remarks>
    internal enum TerminologyUnit
    {
        None = 0,
        Acres,
        Hectares,
        SquareKilometers,
        SquareMiles
    }

    /// <summary>
    /// Patch Prioritization Type
    /// </summary>
    /// <remarks></remarks>
    internal enum PatchPrioritizationType
    {
        Smallest = 0,
        SmallestEdgesOnly,
        Largest,
        LargestEdgesOnly
    }

    /// <summary>
    /// Size Prioritization
    /// </summary>
    /// <remarks></remarks>
    internal enum SizePrioritization
    {
        None = 0,
        Smallest,
        Largest
    }

    /// <summary>
    /// Cardinal Direction
    /// </summary>
    /// <remarks></remarks>
    internal enum CardinalDirection
    {
        N = 0,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }

    /// <summary>
    /// Compare Metadata Result
    /// </summary>
    public enum CompareMetadataResult
    {
        Same,
        UnimportantDifferences,
        ImportantDifferences
    }
}
