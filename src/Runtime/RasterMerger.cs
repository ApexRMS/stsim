﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class RasterMerger
    {
        private SyncroSimRaster m_rasterMerge;

        public void Merge(string inpRasterFileName, int numIterations)
        {
            if (m_rasterMerge == null)
            {
                this.m_rasterMerge = new SyncroSimRaster(inpRasterFileName, RasterDataType.DTDouble);
                m_rasterMerge.ScaleDblCells(numIterations);
                return;
            }

            SyncroSimRaster rastInput = new SyncroSimRaster(inpRasterFileName, RasterDataType.DTDouble);

            if (rastInput.Width != m_rasterMerge.Width || rastInput.Height != m_rasterMerge.Height)
            {
                string sMsg = string.Format(CultureInfo.InvariantCulture,
                    "The metadata of the merge raster file '{0}' does not match that used in previous raster files.",
                    inpRasterFileName);

                throw new ArgumentException(sMsg);
            }

            rastInput.ScaleDblCells(numIterations);
            m_rasterMerge.AddDblCells(rastInput);
        }

        public void Multiply(double mutliplier)
        {
            m_rasterMerge.ScaleDblCells(mutliplier);
        }

        public void Save(string mergedRasterOutputFilename, GeoTiffCompressionType compressionType)
        {
            if (File.Exists(mergedRasterOutputFilename))
            {
                File.SetAttributes(mergedRasterOutputFilename, FileAttributes.Normal);
                File.Delete(mergedRasterOutputFilename);
            }

            Debug.Assert(this.m_rasterMerge.DataType == RasterDataType.DTDouble);
            SyncroSimRaster OutRast = new SyncroSimRaster(mergedRasterOutputFilename, this.m_rasterMerge);

            OutRast.DblCells = this.m_rasterMerge.DblCells;
            OutRast.Save(compressionType);
        }
    }
}
