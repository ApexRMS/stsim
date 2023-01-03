// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    public class InputRasters
    {
        private int m_Width; // Number of cell columns
        private int m_Height; // Number of cell rows
        private double m_cellSize; // Cell size
        private double m_XllCorner; //X coordinate of the origin (by lower left corner of the cell).
        private double m_YllCorner; //Y coordinate of the origin (by lower left corner of the cell).
        private double m_NoDataValue = -9999; // The NODATA value of the raster
        private string m_Projection; // Store the contents of the Raster .prj file here, so we can create a name.prj file when Exporting Raster
        private double m_cellArea; // The cell area
        private bool m_cellAreaOverride; // Is the cell area overriden by the user
        private string m_cellSizeUnits; // The raster native cell units
        private StochasticTimeRaster m_PrimaryStratumRaster;
        private StochasticTimeRaster m_SecondaryStratumRaster;
        private StochasticTimeRaster m_TertiaryStratumRaster;
        private StochasticTimeRaster m_StateClassRaster;
        private StochasticTimeRaster m_AgeRaster;
        private StochasticTimeRaster m_DEMRaster;

        public int Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                m_Width = value;
            }
        }

        public int Height
        {
            get
            {
                return m_Height;
            }
            set
            {
                m_Height = value;
            }
        }

        public int NumberCells
        {
            get
            {
                return m_Width * m_Height;
            }
        }

        public double CellSize
        {
            get
            {
                return m_cellSize;
            }
            set
            {
                m_cellSize = value;
            }
        }

        public int[] PrimaryStratumCells
        {
            get
            {
                return this.m_PrimaryStratumRaster.IntCells;
            }
            set
            {
                this.m_PrimaryStratumRaster.IntCells = value;
            }
        }

        public int[] SecondaryStratumCells
        {
            get
            {
                return this.m_SecondaryStratumRaster.IntCells;
            }
            set
            {
                this.m_SecondaryStratumRaster.IntCells = value;
            }
        }

        public int[] TertiaryStratumCells
        {
            get
            {
                return this.m_TertiaryStratumRaster.IntCells;
            }
            set
            {
                this.m_TertiaryStratumRaster.IntCells = value;
            }
        }

        public int[] SClassCells
        {
            get
            {
                return this.m_StateClassRaster.IntCells;
            }
            set
            {
                this.m_StateClassRaster.IntCells = value;
            }
        }

        public int[] AgeCells
        {
            get
            {
                return this.m_AgeRaster.IntCells;
            }
            set
            {
                this.m_AgeRaster.IntCells = value;
            }
        }

        public double[] DemCells
        {
            get
            {
                return this.m_DEMRaster.DblCells;
            }
            set
            {
                this.m_DEMRaster.DblCells = value;
            }
        }

        public string Projection
        {
            get
            {
                return m_Projection;
            }
        }

        public double XllCorner
        {
            get
            {
                return m_XllCorner;
            }
        }

        public double YllCorner
        {
            get
            {
                return m_YllCorner;
            }
        }

        public double CellArea
        {
            get
            {
                return m_cellArea;
            }
            set
            {
                m_cellArea = value;
            }
        }

        public bool CellAreaOverride
        {
            get
            {
                return m_cellAreaOverride;
            }
            set
            {
                m_cellAreaOverride = value;
            }
        }

        public string CellSizeUnits
        {
            get
            {
                return m_cellSizeUnits;
            }
            set
            {
                m_cellSizeUnits = value;
            }
        }

        public string PrimaryStratumName
        {
            get
            {
                return this.m_PrimaryStratumRaster.FileName;
            }
        }

        public string SecondaryStratumName
        {
            get
            {
                return this.m_SecondaryStratumRaster.FileName;
            }
        }

        public string TertiaryStratumName
        {
            get
            {
                return this.m_TertiaryStratumRaster.FileName;
            }
        }

        public string StateClassName
        {
            get
            {
                return this.m_StateClassRaster.FileName;
            }
        }

        public string AgeName
        {
            get
            {
                return this.m_AgeRaster.FileName;
            }
        }

        public string DemName
        {
            get
            {
                return this.m_DEMRaster.FileName;
            }
        }

        public StochasticTimeRaster PrimaryStratumRaster
        {
            get
            {
                return m_PrimaryStratumRaster;
            }
            set
            {
                m_PrimaryStratumRaster = value;
            }
        }

        public StochasticTimeRaster SecondaryStratumRaster
        {
            get
            {
                return m_SecondaryStratumRaster;
            }
            set
            {
                m_SecondaryStratumRaster = value;
            }
        }

        public StochasticTimeRaster TertiaryStratumRaster
        {
            get
            {
                return m_TertiaryStratumRaster;
            }
            set
            {
                m_TertiaryStratumRaster = value;
            }
        }

        public StochasticTimeRaster StateClassRaster
        {
            get
            {
                return m_StateClassRaster;
            }
            set
            {
                m_StateClassRaster = value;
            }
        }

        public StochasticTimeRaster AgeRaster
        {
            get
            {
                return m_AgeRaster;
            }
            set
            {
                m_AgeRaster = value;
            }
        }

        public StochasticTimeRaster DEMRaster
        {
            get
            {
                return m_DEMRaster;
            }
            set
            {
                m_DEMRaster = value;
            }
        }
        
        public void SetMetadata(StochasticTimeRaster raster)
        {
            this.m_Width = raster.Width;
            this.m_Height = raster.Height;
            this.m_cellSize = raster.CellSize;
            this.m_cellSizeUnits = raster.CellSizeUnits;
            this.m_XllCorner = raster.XllCorner;
            this.m_YllCorner = raster.YllCorner;
            this.m_NoDataValue = Spatial.DefaultNoDataValue;
            this.m_Projection = raster.Projection;
        }

        public StochasticTimeRaster CreateOutputRaster(RasterDataType dataType)
        {
            StochasticTimeRaster rast = new StochasticTimeRaster(
                "output",
                dataType,  
                1,                                
                this.m_Width, 
                this.m_Height,
                this.m_XllCorner, 
                this.m_YllCorner,                 
                this.m_cellSize, 
                this.m_cellSizeUnits, 
                this.m_Projection,
                this.m_PrimaryStratumRaster.GeoTransform,
                this.m_NoDataValue,
                false, 
                Spatial.UndefinedRasterBand);

            if (dataType == RasterDataType.DTInteger)
            {
                rast.InitIntCells();
            }
            else if (dataType == RasterDataType.DTDouble)
            {
                rast.InitDblCells();
            } 
            else
            {
                rast.InitFloatCells();
            }

            return rast;
        }

        /// <summary>
        /// Compare the values of the metadata properties to those of the raster argument
        /// </summary>
        /// <param name="raster">A instance of class Raster</param>
        /// <returns>An Enum containing the comparison Result</returns>
        /// <remarks></remarks>
        public CompareMetadataResult CompareMetadata(StochasticTimeRaster raster, ref string compareMsg)
        {
            CompareMetadataResult retVal = CompareMetadataResult.Same;
            compareMsg = "";

            // Test number of cols. 
            if (this.Width != raster.Width)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Number of Columns ({1} vs {0})", this.Width, raster.Width);
                return CompareMetadataResult.RowColumnMismatch;
            }

            // Test number of rows. 
            if (this.Height != raster.Height)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Number of Rows ({1} vs {0})", this.Height, raster.Height);
                return CompareMetadataResult.RowColumnMismatch;
            }

            // Test XLL Corner. See if NOT negligable difference - arbitrarily 1/10 of cell size. 
            // Can't use equality, because of float error 
            if (Math.Abs(this.XllCorner - raster.XllCorner) > (this.CellSize / 10.0))
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in XllCorner ({1} vs {0})", this.XllCorner, raster.XllCorner);
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            // Test YLL Corner.  See if NOT negligable difference - arbitrarily 1/10 of cell size. 
            // Can't use equality, because of float error  
            if (Math.Abs(this.YllCorner - raster.YllCorner) > (this.CellSize / 10.0))
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in YllCorner ({1} vs {0})", this.YllCorner, raster.YllCorner);
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            // Test ProjectionString 
            if (this.Projection != raster.Projection)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Projection String");
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            // Test Cell Size. Cant use equality because of precision errors ( eg. 30D vs 30.000000000004D)
            if (Math.Abs(this.CellSize - raster.CellSize) > 0.0001)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Cell Size ({1} vs {0})", this.CellSize, raster.CellSize);
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            // Test Cell Units
            if (this.CellSizeUnits != raster.CellSizeUnits)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Cell Size Units ({1} vs {0})", this.CellSizeUnits, raster.CellSizeUnits);
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            return retVal;
        }

        /// <summary>
        /// Get the row and column based on the specified cell Id. All 0-index
        /// </summary>
        /// <param name="cellNumber">The cellid we would like to convert</param>
        /// <param name="row">The converted row number</param>
        /// <param name="col">The converted column number</param>
        /// <remarks></remarks>
        public void GetRowColForId(int cellNumber, ref int row, ref int col)
        {
            Debug.Assert(cellNumber < this.NumberCells);

            col = cellNumber % this.m_Width;
            row = cellNumber / this.m_Width;
        }

        /// <summary>
        /// Get ID based on the row and column specified.
        /// </summary>
        /// <param name="row">The row number</param>
        /// <param name="column">The column number</param>
        /// <returns>The ID for the specified row and column</returns>
        /// <remarks></remarks>
        public int GetIdForRowCol(int row, int column)
        {
            Debug.Assert(row < this.Height);
            Debug.Assert(column < this.Width);

            return row * m_Width + column;
        }

        /// <summary>
        /// Get the Cell Id, given an offset from the specified cell. 
        /// </summary>
        /// <param name="initiationCellId">The source cell which we've calculating our offset from</param>
        /// <param name="rowOffset">The row offset that we're looking for. Specify -1 for N,NW,NE 0 for W,E, and 1 for S,SW, SE</param>
        /// <param name="colOffset">The column offset we're interested in. Specify -1 for W, NW,SW, 0 for N,S, and +1 for E, NE, SE</param>
        /// <returns>The cell id of the cell neighbor we're looking for. Return -1 if out of bounds.</returns>
        /// <remarks>
        /// With the assumption that the raster is orientated with its 0,0 corner representation the NW extreme, and MaxRow, MaxCol represented 
        /// the SE extreme, smaller rows are more northerly, larger more southerly. For columns, lower are West, larger are East. Thus the compass directions
        /// can be represented as:
        /// -1,-1 -1,0  -1,1
        ///  0,-1  0,0   0,1
        ///  1,-1  1,0   1,1
        /// </remarks>
        public int GetCellIdByOffset(int initiationCellId, int rowOffset, int colOffset)
        {
            int cellRow = 0;
            int cellCol = 0;

            this.GetRowColForId(initiationCellId, ref cellRow, ref cellCol);

            // For NW, specify row/col offset of -1,-1
            int newCol = cellCol + colOffset;
            int newRow = cellRow + rowOffset;

            if (newCol < 0 || newCol >= this.Width)
            {
                return -1;
            }

            if (newRow < 0 || newRow >= this.Height)
            {
                return -1;
            }

            return GetIdForRowCol(newRow, newCol);
        }
        /// <summary>
        /// Get the Cell Id, given an offset from the specified cell. 
        /// </summary>
        /// <param name="cellRow">The row of the cell from which we're calculating offset from</param>
        /// <param name="cellColumn">The column of the cell from which we're calculating offset from</param>
        /// <param name="rowOffset">The row offset that we're looking for. Specify -1 for N,NW,NE 0 for W,E, and 1 for S,SW, SE</param>
        /// <param name="colOffset">The column offset we're interested in. Specify -1 for W, NW,SW, 0 for N,S, and +1 for E, NE, SE</param>
        /// <returns>The cell id of the cell neighbor we're looking for. Return -1 if out of bounds.</returns>
        /// <remarks>
        /// With the assumption that the raster is orientated with its 0,0 corner representation the NW extreme, and MaxRow, MaxCol represented 
        /// the SE extreme, smaller rows are more northerly, larger more southerly. For columns, lower are West, larger are East. Thus the compass directions
        /// can be represented as:
        /// -1,-1 -1,0  -1,1
        ///  0,-1  0,0   0,1
        ///  1,-1  1,0   1,1
        /// </remarks>
        public int GetCellIdByOffset(int cellRow, int cellColumn, int rowOffset, int colOffset)
        {

            // For NW, specify row/col offset of -1,-1
            int newCol = cellColumn + colOffset;
            int newRow = cellRow + rowOffset;

            if (newCol < 0 || newCol >= this.Width)
            {
                return -1;
            }

            if (newRow < 0 || newRow >= this.Height)
            {
                return -1;
            }

            return GetIdForRowCol(newRow, newCol);
        }

        /// <summary>
        /// Get the Cell Size in Meters
        /// </summary>
        /// <returns>The Cell size in Meters</returns>
        /// <remarks></remarks>
        public double GetCellSizeMeters()
        {
            // DEVNOTE: TKR - For now assume the native units of the raster is units. At some point in time,
            // will have to take into account Cell Size Overide
            return Convert.ToDouble(this.m_cellSize, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get the Cell Diagonal distance in Metres
        /// </summary>
        /// <returns>The Cell diagonal distance in Metres</returns>
        /// <remarks></remarks>
        public double GetCellSizeDiagonalMeters()
        {
            var cellSizeM = this.GetCellSizeMeters();
            return Math.Sqrt(2 * System.Math.Pow(cellSizeM, 2));
        }

        /// <summary>Gets the cell neighbor ID based on specified direction and distance </summary>
        /// <param name="InitiationCellId">The cell we're looking for the neighbor of</param>
        /// <param name="Degrees">The direction in degrees.</param>
        /// <param name="Distance">The distance in metres</param>
        /// <returns>The Cell object at the specified direction/distance</returns>
        /// <remarks>Determines The cell id based on the direction and distance from the origin cell center
        /// If Distance outside of cell takes you outside of existing extent of landscape returns Nothing
        /// </remarks>
        public int GetCellIdByDistanceAndDirection(int initiationCellId, int degrees, double distance)
        {
            var cellSizeM = this.GetCellSizeMeters();

            double angle = Math.PI * degrees / 180.0;
            double horizDist = distance * Math.Cos(angle);
            double vertDist = distance * Math.Sin(angle);

            // Take into account the cellsize. So for instance, if a CellSize = 30, then its (exclusive) bounds are +/- 15, so the 
            // the next cell range is >=15 and < 45, and >-45 and =<-15. 
            if (horizDist < 0)
            {
                horizDist -= cellSizeM / 2;
            }
            else
            {
                horizDist += cellSizeM / 2;
            }

            if (vertDist < 0)
            {
                vertDist -= cellSizeM / 2;
            }
            else
            {
                vertDist += cellSizeM / 2;
            }

            var colOffset = (int)(horizDist / cellSizeM);
            var rowOffset = (int)(vertDist / cellSizeM);

            int id = GetCellIdByOffset(initiationCellId, -rowOffset, colOffset);
            if (id == -1)
            {
                return 0;
            }
            else
            {
                return id;
            }
        }

        /// <summary>Gets the offsets for neighbor cells contained within the specified radius</summary>
        /// <param name="radius">The radius of the search circle in metres</param>
        /// <returns>A List of cell offset founds</returns>
        public IEnumerable<CellOffset> GetCellNeighborOffsetsForRadius(double radius)
        {
            // The max number of cell rows and columns for the specified radius
            int numRadiusCells = Convert.ToInt32(Math.Truncate((radius) / this.GetCellSizeMeters()));

            // To determine the maximum cell extent within a circle described by a radius, we calculate the relative distance of a 
            // cell centroid from the circle centre, and compare it to the radius. If large than the radius, then we move "closer",
            // by decrementing to the next closer cell and repeat comparison. The result is the maximum cell extent for a quadrant, which
            // we can mirror for the other three quadrants. 

            // Let evaluate the NE quadrant ( all positive values). 
            int[] maxColRows = new int[numRadiusCells + 1];

            int y = numRadiusCells;
            for (int x = 0; x <= numRadiusCells; x++)
            {
                maxColRows[x] = -1;

                while (!(y == -1))
                {
                    // Is the cell centroid outside the circle radius.
                    if (GetRelativeCellDistance(y, x) > radius)
                    {
                        y = y - 1;
                    }
                    else
                    {
                        // Save the y coordinate for later
                        maxColRows[x] = y;
                        break;
                    }
                }
            }

            // We've got the NE quadrant max row/cols pairs, so lets use these values to determine all the enclosed cells
            // for all quadrants.
            List<CellOffset> listCellOffsets = new List<CellOffset>();
            for (var column = 0; column <= maxColRows.GetUpperBound(0); column++)
            {
                int t = maxColRows[column];

                for (var row = 0; row <= t; row++)
                {
                    //NE quadrant
                    AddCellOffsetToList(ref listCellOffsets, row, column);
                    //NW quadrant
                    AddCellOffsetToList(ref listCellOffsets, row, -column);
                    //SE quadrant
                    AddCellOffsetToList(ref listCellOffsets, -row, column);
                    //SW quadrant
                    AddCellOffsetToList(ref listCellOffsets, -row, -column);
                }
            }

            return listCellOffsets;
        }

        /// <summary>
        /// Add a cell offset value to a list object, checking for uniqueness
        /// </summary>
        /// <param name="listCellIds"></param>
        /// <param name="rowOffset"></param>
        /// <param name="columnOffset"></param>
        /// <remarks></remarks>
        private static void AddCellOffsetToList(ref List<CellOffset> listCellIds, int rowOffset, int columnOffset)
        {
            CellOffset coord = new CellOffset(rowOffset, columnOffset);
            if (!listCellIds.Contains(coord))
            {
                listCellIds.Add(coord);
            }
        }

        /// <summary>
        /// Get the relative distance between two cells.
        /// </summary>
        /// <param name="rowDiff">The number of rows that the cells are apart</param>
        /// <param name="colDiff">The number of columns that the cells are aparat</param>
        /// <returns>The distance between the two cells</returns>
        /// <remarks>This a generalize function, where we only care about the relative distance, and not the acutal cells themselves.</remarks>
        private double GetRelativeCellDistance(int rowDiff, int colDiff)
        {
            return Math.Sqrt(System.Math.Pow(rowDiff, 2) + System.Math.Pow(colDiff, 2)) * (double)this.CellSize;
        }
    }
}
