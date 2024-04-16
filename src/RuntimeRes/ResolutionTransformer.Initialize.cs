using SyncroSim.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncroSim.STSim
{
    internal partial class ResolutionTransformer
    {
        private InitialConditionsSpatialMap m_SPICMapMultiRes;
        private InitialConditionsSpatialCollection m_SPICValuesMultiRes;
        private InitialConditionsSpatialMap m_SPICMapSTSim;
        private InitialConditionsSpatialCollection m_SPICValuesSTSim;
        private Dictionary<int, CellCollection> m_BaseForcesFineCells;
        private Dictionary<int, CellCollection> m_FineForcesBaseCells;
        private Dictionary<(int, int), Transition> m_FineTransitionDictionary; // composite key: fineCellId, transitionGroupId
        private Dictionary<(int, int), Transition> m_BaseTransitionDictionary; //composite key: baseCellId, transitionGroupId

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
            this.m_SPICValuesSTSim = CreateSPICCollection(this.ResultScenario, Constants.DATASHEET_STSIM_SPIC_NAME);
            this.m_SPICMapSTSim = new InitialConditionsSpatialMap(this.m_SPICValuesSTSim);
            this.m_SPICValuesMultiRes = CreateSPICCollection(this.ResultScenario, Constants.DATASHEET_SPIC_NAME);
            this.m_SPICMapMultiRes = new InitialConditionsSpatialMap(this.m_SPICValuesMultiRes);
            this.m_ResolutionGroups = CreateResolutionGroupCollection(this.ResultScenario);
            this.m_BaseForcesFineCells = new Dictionary<int, CellCollection>();
            this.m_FineForcesBaseCells = new Dictionary<int, CellCollection>();
            this.m_FineTransitionDictionary = new Dictionary<(int, int), Transition>();
            this.m_BaseTransitionDictionary = new Dictionary<(int, int), Transition>();

            DataSheet STSimSpatialProperties = this.ResultScenario.GetDataSheet(Constants.DATASHEET_STSIM_SPPIC_NAME);
            InitialConditionsSpatialCollection MultiResColl = CreateSPICCollection(this.ResultScenario, Constants.DATASHEET_SPIC_NAME);
            InitialConditionsSpatial RefMultiResColl = MultiResColl.First();
            DataSheet MultiResDataSheet = this.ResultScenario.GetDataSheet(Constants.DATASHEET_SPIC_NAME);

            //QUESTION FOR KATIE: How do we rectify this in Syncrosim 3? Are we using stochastic time?
            string MultiResFilename = Spatial.GetSpatialDataFileName(MultiResDataSheet, RefMultiResColl.PrimaryStratumFileName, false); //breaks if input DNE
            SyncroSimRaster MRRaster = new SyncroSimRaster(MultiResFilename, RasterDataType.DTInteger);

            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_NUM_ROWS_COLUMN_NAME, MRRaster.Height);
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_NUM_COLUMNS_COLUMN_NAME, MRRaster.Width);
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_NUM_CELLS_COLUMN_NAME, MRRaster.GetNumberValidCells());
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_XLLCORNER_COLUMN_NAME, MRRaster.XllCorner);
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_YLLCORNER_COLUMN_NAME, MRRaster.YllCorner);
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_CELL_SIZE_COLUMN_NAME, MRRaster.CellSize);
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME, MRRaster.CellSizeUnits);
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_SRS_COLUMN_NAME, MRRaster.Projection);

            // Calculate cell area
            double cellArea = Math.Pow(MRRaster.CellSize, 2);
            string amountlabel = null;
            TerminologyUnit destUnitsVal = 0;
            TerminologyUtilities.GetAmountLabelTerminology(
                this.Project.GetDataSheet(Constants.DATASHEET_STSIM_TERMINOLOGY_NAME), ref amountlabel, ref destUnitsVal);
            double cellAreaTU = InitialConditionsFineSpatialRasterDataSheet.CalcCellArea(cellArea, MRRaster.CellSizeUnits, destUnitsVal);
            STSimSpatialProperties.SetSingleRowData(Constants.DATASHEET_STSIM_SPPIC_CELL_AREA_COLUMN_NAME, cellAreaTU);

            // generate age raster here if it does not exist - see STSim.Transformer.Spatial line 2031

            base.Initialize();
        }

        public void PerformIteration(int iteration)
        {
            /*
             * QUESTION FOR KATIE: these methods are marked as protected in STSimTransformer, so we can't call these methods on this.STSimTransformer,
             * but we also probably shouldn't break Alex's access patterns.
             */
            //this.OnBeforeIteration(iteration);
            //this.OnIteration(iteration);
        }

        public void PerformTimestep(int iteration, int timestep)
        {
            //this.OnBeforeTimestep(iteration, timestep);
            //this.OnTimestep(iteration, timestep);
        }

        public void EndModelRun()
        {
            //this.WriteSpatialAveragingRasters();
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
                MultiResolutionExceptionHandling.ThrowRasterValidationException(Constants.ERROR_RASTERS_TOO_SMALL, multiResolutionFileName, stsimFileName);
            }

            if (MRRaster.Width % STRaster.Width != 0 || MRRaster.Height % STRaster.Height != 0)
            {
                MultiResolutionExceptionHandling.ThrowRasterValidationException(Constants.ERROR_RASTERS_WRONG_PROPORTION, multiResolutionFileName, stsimFileName);
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

        protected void OnSpatialTransitionGroup(object sender, SpatialTransitionGroupEventArgs e)
        {
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
                            this.STSimTransformer.InvokeProbabilisticTransitionForCell(simulationCell, e.TransitionGroup, forcedFineTransition, e.Iteration, e.Timestep, e.TransitionedPixels, e.RasterTransitionAttrValues);
                        }
                    }
                }
                e.Cancel = true;
            }
        }

        protected void OnApplySpatialTransition(int iteration, int timestep, TransitionGroup tg, Cell simulationCell)
        {
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
    }
}
