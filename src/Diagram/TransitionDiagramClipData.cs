// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Drawing;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    [Serializable()]
    internal class TransitionDiagramClipData
    {
        public List<TransitionDiagramClipDataEntry> Entries = new List<TransitionDiagramClipDataEntry>();
    }

    [Serializable()]
    internal class DeterministicTransitionClipData
    {
        public string StratumSource;
        public string StateClassSource;
        public string StratumDest;
        public string StateClassDest;

        public int? StratumIdSource;
        public int StateClassIdSource;
        public int? StratumIdDest;
        public int? StateClassIdDest;
        public int? AgeMin;
        public int? AgeMax;
    }

    [Serializable()]
    internal class ProbabilisticTransitionClipData
    {
        public string StratumSource;
        public string StateClassSource;
        public string StratumDest;
        public string StateClassDest;
        public string TransitionType;

        public int? StratumIdSource;
        public int StateClassIdSource;
        public int? StratumIdDest;
        public int? StateClassIdDest;
        public int TransitionTypeId;
        public double Probability;
        public double? Proportion;
        public int? AgeMin;
        public int? AgeMax;
        public int? AgeRelative;
        public bool? AgeReset;
        public int? TstMin;
        public int? TstMax;
        public int? TstRelative;
    }

    [Serializable()]
    internal class TransitionDiagramClipDataEntry
    {
        public DeterministicTransitionClipData ShapeData = new DeterministicTransitionClipData();
        public List<DeterministicTransitionClipData> IncomingDT = new List<DeterministicTransitionClipData>();
        public List<ProbabilisticTransitionClipData> IncomingPT = new List<ProbabilisticTransitionClipData>();
        public List<ProbabilisticTransitionClipData> OutgoingPT = new List<ProbabilisticTransitionClipData>();
        public int Row;
        public int Column;
        public Rectangle Bounds;
    }
}
