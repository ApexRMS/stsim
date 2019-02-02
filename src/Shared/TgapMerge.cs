// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Globalization;
using SyncroSim.StochasticTime;
using System.Diagnostics;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Class to use when handling the merge operations performed on the TGAP (Average Annual Transition Probability) rasters. This specialized 
    /// merging is required when parallel processing is employed, resulting in Job splits by Iteration. This merge should be performed before the
    /// Spatial Merge operations are performed, where splitting/merging is based on secondary strata.
    /// </summary>
    /// <remarks></remarks>
    internal class TgapMerge
    {
        private StochasticTimeRaster m_rasterMerge;

        /// <summary>
        /// Arithmetically merge the specified TGAP (Average Annual Transition Probability) raster with previous raster merges. 
        /// Call this method once for every TGAP raster file you want to merge. It will copy each non-NO_DATA_VALUE pixel to merged raster.
        /// </summary>
        /// <param name="inpRasterFileName">The absolute filename of the TGAP raster file you want to add to the merged result</param>
        /// <param name="numIterations">The number of iterations performed when generating this Tgap raster. Used for weighting the cell values when merging.</param>
        /// <remarks>The raster spatial metadata (XLLCorner, cellSize, Proj, etc..) is taken from the 1st file Merge'd, although 
        /// all merged files should be consistent in this regard.</remarks>
        public void Merge(string inpRasterFileName, int numIterations)
        {
            // 1st time thru?
            if (m_rasterMerge == null)
            {
                this.m_rasterMerge = new StochasticTimeRaster(inpRasterFileName, RasterDataType.DTDouble);

                // Apply the numIterations to each cell
                m_rasterMerge.ScaleDblCells(numIterations);
                return;
            }

            StochasticTimeRaster rastInput = new StochasticTimeRaster(inpRasterFileName, RasterDataType.DTDouble);

            // Crude metadata compare
            if (rastInput.Width != m_rasterMerge.Width || rastInput.Height != m_rasterMerge.Height)
            {
                string sMsg = string.Format(CultureInfo.InvariantCulture, 
                    "The metadata of the merge raster file '{0}' does not match that used in previous raster files.", 
                    inpRasterFileName);

                throw new ArgumentException(sMsg);
            }

            // Apply the number of iterations multiplier
            rastInput.ScaleDblCells(numIterations);

            // Now lets arithmetically merge this new raster with previous 
            m_rasterMerge.AddDblCells(rastInput);
        }

        public void Multiply(double mutliplier)
        {
            m_rasterMerge.ScaleDblCells(mutliplier);
        }

        /// <summary>
        /// Save the merged result of multiple Merge calls to the specified Raster Output file.
        /// </summary>
        /// <param name="mergedRasterOutputFilename">The absolute file name of the raster output file.</param>
        /// <remarks>The raster spatial metadata (XLLCorner, cellSize, Proj, etc..) is taken from the 1st file Merge'd, although 
        /// all merged files should be consistent in this regard.</remarks>
        public void Save(string mergedRasterOutputFilename, GeoTiffCompressionType compressionType)
        {
            // Get rid of any existing file
            if (File.Exists(mergedRasterOutputFilename))
            {
                File.SetAttributes(mergedRasterOutputFilename, FileAttributes.Normal);
                File.Delete(mergedRasterOutputFilename);
            }

            Debug.Assert(this.m_rasterMerge.DataType == RasterDataType.DTDouble);
            Debug.Assert(this.m_rasterMerge.NoDataValue == Spatial.DefaultNoDataValue);

            StochasticTimeRaster OutRast = new StochasticTimeRaster(mergedRasterOutputFilename, this.m_rasterMerge);

            OutRast.DblCells = this.m_rasterMerge.DblCells;
            OutRast.Save(compressionType);

            Debug.Print("Saved Merged TGAP file to '" + mergedRasterOutputFilename + "'");
        }
    }
}
