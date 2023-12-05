// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Terminolgy Unit
    /// </summary>
    /// <remarks></remarks>
    public enum TerminologyUnit
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
        RowColumnMismatch
    }

    /// <summary>
    /// OutputFilterFlagTransitionGroup
    /// </summary>
    [Flags()]
    internal enum OutputFilterFlagTransitionGroup
    {
        None = 0,
        Summary = 1,
        SummaryByStateClass = 2,
        SummaryTST = 4,
        Spatial = 8,
        SpatialEvents = 16,
        SpatialTST = 32,
        SpatialProbability = 64,
        AvgSpatialTST = 128
    }

    /// <summary>
    /// OutputFilterFlagAttribute
    /// </summary>
    [Flags()]
    internal enum OutputFilterFlagAttribute
    {
        None = 0,
        Summary = 1,
        Spatial = 2,
        AvgSpatial = 4
    }

    public enum TargetType
    {
        Flow = 0,
        ToStock = 1,
        FromStock = 2
    };

    [Flags()]
    public enum OutputFilter
    {
        None = 0,
        Tabular = 1,
        Spatial = 2,
        AvgSpatial = 4
    }
}
