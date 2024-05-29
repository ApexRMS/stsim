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
        private InitialConditionsFineSpatialMap m_SPICMapMultiRes;
        private InitialConditionsFineSpatialCollection m_SPICValuesMultiRes;
        //private InitialConditionsSpatialMap m_SPICMapSTSim;
        //private InitialConditionsSpatialCollection m_SPICValuesSTSim;
        private Dictionary<int, CellCollection> m_BaseForcesFineCells;
        private Dictionary<int, CellCollection> m_FineForcesBaseCells;
        private Dictionary<(int, int), Transition> m_FineTransitionDictionary; // composite key: fineCellId, transitionGroupId
        private Dictionary<(int, int), Transition> m_BaseTransitionDictionary; //composite key: baseCellId, transitionGroupId

        private static readonly string MULTIBANDING_NOT_AVAILABLE_ERROR_MSG = "Multibanding has not yet been enabled for Mutli resolution st-sim runs. Please select 'Single Band' from the spatial options data sheet before running.";

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
            //this.m_SPICValuesSTSim = CreateSPICFCollection(this.ResultScenario, Strings.DATASHEET_SPIC_NAME);
            //this.m_SPICMapSTSim = new InitialConditionsSpatialMap(this.m_SPICValuesSTSim);
            this.m_SPICValuesMultiRes = CreateSPICFCollection(this.ResultScenario, Strings.DATASHEET_SPICF_NAME);
            this.m_SPICMapMultiRes = new InitialConditionsFineSpatialMap(this.m_SPICValuesMultiRes);
            this.m_ResolutionGroups = CreateResolutionGroupCollection(this.ResultScenario);
            this.m_BaseForcesFineCells = new Dictionary<int, CellCollection>();
            this.m_FineForcesBaseCells = new Dictionary<int, CellCollection>();
            this.m_FineTransitionDictionary = new Dictionary<(int, int), Transition>();
            this.m_BaseTransitionDictionary = new Dictionary<(int, int), Transition>();

            DataSheet FineSpatialProperties = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPPICF_NAME);
            InitialConditionsFineSpatialCollection MultiResColl = CreateSPICFCollection(this.ResultScenario, Strings.DATASHEET_SPICF_NAME);
            InitialConditionsFineSpatial RefMultiResColl = MultiResColl.First();
            DataSheet MultiResDataSheet = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPICF_NAME);

            string MultiResFilename = Spatial.GetSpatialDataFileName(MultiResDataSheet, RefMultiResColl.PrimaryStratumFileName, false); //breaks if input DNE
            SyncroSimRaster MRRaster = new SyncroSimRaster(MultiResFilename, RasterDataType.DTInteger);

            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_NUM_ROWS_COLUMN_NAME, MRRaster.Height);
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_NUM_COLUMNS_COLUMN_NAME, MRRaster.Width);
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_NUM_CELLS_COLUMN_NAME, MRRaster.GetNumberValidCells());
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_XLLCORNER_COLUMN_NAME, MRRaster.XllCorner);
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_YLLCORNER_COLUMN_NAME, MRRaster.YllCorner);
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_CELL_SIZE_COLUMN_NAME, MRRaster.CellSize);
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_CELL_SIZE_UNITS_COLUMN_NAME, MRRaster.CellSizeUnits);
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_SRS_COLUMN_NAME, MRRaster.Projection);

            // Calculate cell area
            double cellArea = Math.Pow(MRRaster.CellSize, 2);
            string amountlabel = null;
            TerminologyUnit destUnitsVal = 0;
            TerminologyUtilities.GetAmountLabelTerminology(
                this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref amountlabel, ref destUnitsVal);
            double cellAreaTU = InitialConditionsFineSpatialRasterDataSheet.CalcCellArea(cellArea, MRRaster.CellSizeUnits, destUnitsVal);
            FineSpatialProperties.SetSingleRowData(Strings.DATASHEET_SPPICF_CELL_AREA_COLUMN_NAME, cellAreaTU);

            // generate age raster here if it does not exist - see STSim.Transformer.Spatial line 2031
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
                    e.Cancel = false;
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

            if (
                dr != null
                && dr[Strings.DATASHEET_CORE_SPATIAL_OPTIONS_MULTIBAND_GROUPING_INTERNAL_COLUMN_NAME] != DBNull.Value
                && Convert.ToInt32(dr[Strings.DATASHEET_CORE_SPATIAL_OPTIONS_MULTIBAND_GROUPING_INTERNAL_COLUMN_NAME], CultureInfo.InvariantCulture) != 0
            )
            {
                FormsUtilities.ErrorMessageBox(MULTIBANDING_NOT_AVAILABLE_ERROR_MSG);
                throw new STSimException(MULTIBANDING_NOT_AVAILABLE_ERROR_MSG);
            }
        }
    }
}
