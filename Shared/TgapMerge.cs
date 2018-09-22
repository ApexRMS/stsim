// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Globalization;
using SyncroSim.StochasticTime;
using System.Diagnostics;

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
        /// Arithmetically merge the specified TGAP (Average Annual Transition Probability)  raster with previous raster merges. Call this method once for every TGAP raster file you 
        /// want to merge. It will copy each non-NO_DATA_VALUE pixel to merged raster.
        /// </summary>
        /// <param name="inpRasterFileName">The absolute filename of the TGAP raster file you want to add to the merged result</param>
        /// <param name="numIterations">The number of iterations performed when generating this Tgap raster. Used for weighting the cell values when merging.</param>
        /// <remarks>The raster spatial metadata (XLLCorner, cellSize, Proj, etc..) is taken from the 1st file Merge'd, although 
        /// all merged files should be consistent in this regard.</remarks>
        public void Merge(string inpRasterFileName, int numIterations)
        {
            StochasticTimeRaster rastInput = new StochasticTimeRaster();
            RasterDataType dataType = RasterDataType.DTDouble;

            // 1st time thru?
            if (m_rasterMerge == null)
            {
                m_rasterMerge = new StochasticTimeRaster();
                RasterFiles.LoadRasterFile(inpRasterFileName, m_rasterMerge, dataType);

                // Apply the numIterations to each cell
                m_rasterMerge.ScaleDbl(numIterations);
                return;
            }

            RasterFiles.LoadRasterFile(inpRasterFileName, rastInput, dataType);

            // Crude metadata compare
            if (rastInput.NumberCols != m_rasterMerge.NumberCols || rastInput.NumberRows != m_rasterMerge.NumberRows)
            {
                string sMsg = string.Format(CultureInfo.InvariantCulture, "The metadata of the merge raster file '{0}' does not match that used in previous raster files.", inpRasterFileName);
                throw new ArgumentException(sMsg);
            }

            // Apply the number of iterations multiplier
            rastInput.ScaleDbl(numIterations);

            // Now lets arithmetically merge this new raster with previous 
            m_rasterMerge.AddDbl(rastInput);
        }

        public void Multiply(double mutliplier)
        {
            m_rasterMerge.ScaleDbl(mutliplier);
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

            //DEVNOTE: Use Default NODATA_Value for all spatial output raster files
            m_rasterMerge.NoDataValue = StochasticTimeRaster.DefaultNoDataValue;

            if (!RasterFiles.ProcessDoubleRasterToFile(m_rasterMerge, mergedRasterOutputFilename, compressionType))
            {
                string sMsg = string.Format(CultureInfo.InvariantCulture, "Unable to process merged raster file '{0}'", mergedRasterOutputFilename);
                throw new ArgumentException(sMsg);
            }

            Debug.Print("Saved Merged TGAP file to '" + mergedRasterOutputFilename + "'");
        }
    }
}
