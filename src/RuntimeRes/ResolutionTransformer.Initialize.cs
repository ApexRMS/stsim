﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SyncroSim.STSim
{
    internal partial class ResolutionTransformer
    {
        private InitialConditionsSpatialCollectionFineRes m_SPICValuesMultiRes;
        private Dictionary<int, CellCollection> m_BaseForcesFineCells;
        private Dictionary<int, CellCollection> m_FineForcesBaseCells;
        private Dictionary<(int, int), Transition> m_FineTransitionDictionary;
        private Dictionary<(int, int), Transition> m_BaseTransitionDictionary;

        private static readonly string MULTIBANDING_NOT_AVAILABLE_ERROR_MSG = "Multi-banding has not yet been enabled for Multi Resolution ST-Sim runs. Please select the 'Single Band' grouping from the library options before running.";

        public Dictionary<int, CellCollection> BaseForcesFineCells
        {
            get
            {
                return this.m_BaseForcesFineCells;
            }
        }

        public Dictionary<int, CellCollection> FineForcesBaseCells
        {
            get
            {
                return this.m_FineForcesBaseCells;
            }
        }

        public Dictionary<(int, int), Transition> FineTransitionDictionary
        {
            get
            {
                return this.m_FineTransitionDictionary;
            }
        }

        public Dictionary<(int, int), Transition> BaseTransitionDictionary
        {
            get
            {
                return this.m_BaseTransitionDictionary;
            }
        }

        private void AuxillarySetup()
        {
            this.m_SPICValuesMultiRes = CreateSPICFCollection(this.ResultScenario, Strings.DATASHEET_SPICF_NAME);
            this.m_ResolutionGroups = CreateResolutionGroupCollection(this.ResultScenario);
            this.m_BaseForcesFineCells = new Dictionary<int, CellCollection>();
            this.m_FineForcesBaseCells = new Dictionary<int, CellCollection>();
            this.m_FineTransitionDictionary = new Dictionary<(int, int), Transition>();
            this.m_BaseTransitionDictionary = new Dictionary<(int, int), Transition>();

            DataSheet SpatialPropertiesFineRes = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPPICF_NAME);
            InitialConditionsSpatialCollectionFineRes MultiResColl = CreateSPICFCollection(this.ResultScenario, Strings.DATASHEET_SPICF_NAME);
            InitialConditionsSpatialFineRes RefMultiResColl = MultiResColl.First();
            DataSheet MultiResDataSheet = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPICF_NAME);

            string MultiResFilename = Spatial.GetSpatialDataFileName(MultiResDataSheet, RefMultiResColl.PrimaryStratumFileName, false); //breaks if input DNE
            SyncroSimRaster MRRaster = new SyncroSimRaster(MultiResFilename, RasterDataType.DTInteger);

            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_NUM_ROWS_COLUMN_NAME, MRRaster.Height);
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_NUM_COLUMNS_COLUMN_NAME, MRRaster.Width);
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_NUM_CELLS_COLUMN_NAME, MRRaster.GetNumberValidCells());
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_XLLCORNER_COLUMN_NAME, MRRaster.XllCorner);
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_YLLCORNER_COLUMN_NAME, MRRaster.YllCorner);
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_CELL_SIZE_COLUMN_NAME, MRRaster.CellSize);
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_CELL_SIZE_UNITS_COLUMN_NAME, MRRaster.CellSizeUnits);
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_SRS_COLUMN_NAME, MRRaster.Projection);

            // Calculate cell area
            double cellArea = Math.Pow(MRRaster.CellSize, 2);
            string amountlabel = null;
            TerminologyUnit destUnitsVal = 0;
            TerminologyUtilities.GetAmountLabelTerminology(this.Project, ref amountlabel, ref destUnitsVal);
            double cellAreaTU = InitialConditionsSpatialRasterDataSheetFineRes.CalcCellArea(cellArea, MRRaster.CellSizeUnits, destUnitsVal);
            SpatialPropertiesFineRes.SetSingleRowData(Strings.DATASHEET_SPPICF_CELL_AREA_COLUMN_NAME, cellAreaTU);
        }

        protected void PerformIteration(int iteration)
        {
            this.OnBeforeIteration(iteration);
            this.OnIteration(iteration);
        }

        public void PerformTimestep(int iteration, int timestep)
        {
            this.OnBeforeTimestep(iteration, timestep);
            this.OnTimestep(iteration, timestep);
        }

        public void EndModelRun()
        {
            this.WriteSpatialAveragingRasters();
        }

        internal void ValidateGroups(int transitionGroupId)
        {
            // set default transition group resolution to base only
            Resolution defaultRes = 0;

            if (!this.m_ResolutionGroups.Contains(transitionGroupId))
            {
                TransitionGroupResolution tgr = new TransitionGroupResolution(transitionGroupId, defaultRes, 0);
                this.m_ResolutionGroups.Add(tgr);
            }

            //DEVTODO
            //TransitionGroupIDs not listed in the datasheet assumed to be “Base Only”
            //TransitionGroupID must be either an auto-group, or a simulation group.
            //If an auto - group, must not belong to simulation group. 
        }

        internal void ValidateRaster(string multiResolutionFileName, string stsimFileName)
        {
            SyncroSimRaster MRRaster = new SyncroSimRaster(multiResolutionFileName, RasterDataType.DTInteger, false, 1);
            SyncroSimRaster STRaster = new SyncroSimRaster(stsimFileName, RasterDataType.DTInteger, false, 1);

            if (MRRaster.Width <= STRaster.Width || MRRaster.Height < STRaster.Height)
            {
                MultiResolutionExceptionHandling.ThrowRasterValidationException(Strings.ERROR_RASTERS_TOO_SMALL, multiResolutionFileName, stsimFileName);
            }

            if (MRRaster.Width % STRaster.Width != 0 || MRRaster.Height % STRaster.Height != 0)
            {
                MultiResolutionExceptionHandling.ThrowRasterValidationException(Strings.ERROR_RASTERS_WRONG_PROPORTION, multiResolutionFileName, stsimFileName);
            }
        }

        protected void ValidateInputRasterFiles(string rasterFileName)
        {
            if (!File.Exists(rasterFileName))
            {
                throw new FileNotFoundException(String.Format("Spatial Initial Conditions file not found: {0}.",
                    rasterFileName));
            }
        }

        protected override void OnSpatialTransitionGroup(object sender, SpatialTransitionGroupEventArgs e)
        {
            base.OnSpatialTransitionGroup(sender, e);
            
            this.ValidateGroups(e.TransitionGroup.TransitionGroupId);

            if (this.m_ResolutionGroups.Contains(e.TransitionGroup.TransitionGroupId))
            {
                TransitionGroupResolution tgr = this.m_ResolutionGroups[e.TransitionGroup.TransitionGroupId];

                if (tgr.Resolution == Resolution.BaseOnly)
                {
                    e.Cancel = true;
                }
                else if ((tgr.Resolution == Resolution.BaseForcesFine) && (this.m_BaseForcesFineCells.ContainsKey(e.TransitionGroup.TransitionGroupId)))
                {
                    // For each bff cell, find corresponding fine resolution cells and force transition
                    // create some sort of map from base to fine based on Cell IDs? 
                    // Call InvokeProbTransitions 
                    // Also need to know what transition to apply - cell can have multiple transitions, should match the base transition
                    // Best way to do this? - Dictionary of Base transitions? Modify Cell Transitions when adding to BaseForcesFineCells collection
                    foreach (Cell simulationCell in this.m_BaseForcesFineCells[e.TransitionGroup.TransitionGroupId])
                    {
                        if (this.m_FineTransitionDictionary.ContainsKey((simulationCell.CellId, e.TransitionGroup.TransitionGroupId)))
                        {
                            Transition forcedFineTransition = this.m_FineTransitionDictionary[(simulationCell.CellId, e.TransitionGroup.TransitionGroupId)];
                            this.InvokeProbabilisticTransitionForCell(simulationCell, e.TransitionGroup, forcedFineTransition, e.Iteration, e.Timestep, e.TransitionedPixels, e.RasterTransitionAttrValues);
                        }
                    }

                    e.Cancel = true;
                }
                else if ((tgr.Resolution == Resolution.FineOnly || tgr.Resolution == Resolution.FineForcesBase))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        protected override void OnApplySpatialTransition(int iteration, int timestep, TransitionGroup tg, Cell simulationCell)
        {
            base.OnApplySpatialTransition(iteration, timestep, tg, simulationCell);
            
            if (this.m_ResolutionGroups.Contains(tg.TransitionGroupId))
            {
                TransitionGroupResolution tgr = this.m_ResolutionGroups[tg.TransitionGroupId];

                if (tgr.Resolution == Resolution.FineForcesBase)
                {
                    if (!this.FineForcesBaseCells.ContainsKey(tg.TransitionGroupId))
                    {
                        this.FineForcesBaseCells.Add(tg.TransitionGroupId, new CellCollection());
                    }
                    this.FineForcesBaseCells[tg.TransitionGroupId].Add(simulationCell); // collection of FINE cells
                }
            }
        }

        private void ValidateIsNotMultibandingRun()
        {
            DataSheet spatialOptionDatasheet = this.Library.GetDataSheet(Strings.DATASHEET_CORE_SPATIAL_OPTIONS);
            DataRow dr = spatialOptionDatasheet.GetDataRow();

            var multibandingGroupSetting = dr[Strings.DATASHEET_CORE_SPATIAL_OPTIONS_MULTIBAND_GROUPING_INTERNAL_COLUMN_NAME];
            if (
                dr != null
                && multibandingGroupSetting != DBNull.Value
                && Convert.ToInt32(multibandingGroupSetting, CultureInfo.InvariantCulture) != 0
            )
            {
                FormsUtilities.ErrorMessageBox(MULTIBANDING_NOT_AVAILABLE_ERROR_MSG);
                throw new STSimException(MULTIBANDING_NOT_AVAILABLE_ERROR_MSG);
            }
        }
    }
}
