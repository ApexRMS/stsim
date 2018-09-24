// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;
using SyncroSim.StochasticTime;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public class InputRasters
    {
        private int m_numRows; // Number of cell rows
        private int m_numCols; // Number of cell columns
        private decimal m_cellSize; // Cell size
        private decimal m_XllCorner; //X coordinate of the origin (by lower left corner of the cell).
        private decimal m_YllCorner; //Y coordinate of the origin (by lower left corner of the cell).
        private int[] m_sclass_cells; // Single dimension array of State Class raster cells.
        private int[] m_stratum_cells; // Single dimension array of Primary Stratum raster cells.
        private int[] m_secondary_stratum_cells; // Single dimension array of Secondary Stratum raster cells.
        private int[] m_tertiary_stratum_cells; // Single dimension array of Tertiary Stratum raster cells.
        private int[] m_age_cells; // Single dimension array of Age raster cells.
        private double[] m_dem_cells; // Single dimension array of Digital Elevation Model (DEM) raster cells.
        private double m_NoDataValue = -9999; // The NODATA value of the raster
        private string m_projectionString; // Store the contents of the Raster .prj file here, so we can create a name.prj file when Exporting Raster
        private double m_cellArea; // The cell area
        private bool m_cellAreaOverride; // Is the cell area overriden by the user
        private string m_cellSizeUnits; // The raster native cell units
        private string m_primary_stratum_name; //The name of the Primary Stratum raster file
        private string m_secondary_stratum_name; //The name of the Secondary Stratum raster file
        private string m_tertiary_stratum_name; //The name of the Tertiary Stratum raster file
        private string m_stateClass_name; //The name of the State Class raster file
        private string m_age_name; //The name of the Age raster file
        private string m_dem_name; //The name of the DEM raster file

        public int NumberRows
        {
            get
            {
                return m_numRows;
            }
            set
            {
                m_numRows = value;
            }
        }

        public int NumberColumns
        {
            get
            {
                return m_numCols;
            }
            set
            {
                m_numCols = value;
            }
        }

        public int NumberCells
        {
            get
            {
                return m_numCols * m_numRows;
            }
        }

        public decimal CellSize
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

        /// <summary>
        /// Primary Stratum Cells
        /// </summary>
        public int[] StratumCells
        {
            get
            {
                return m_stratum_cells;
            }
            set
            {
                m_stratum_cells = value;
            }
        }

        /// <summary>
        /// Secondary Stratum Cells
        /// </summary>
        public int[] SecondaryStratumCells
        {
            get
            {
                return m_secondary_stratum_cells;
            }
            set
            {
                m_secondary_stratum_cells = value;
            }
        }

        /// <summary>
        /// Tertiary Stratum Cells
        /// </summary>
        public int[] TertiaryStratumCells
        {
            get
            {
                return m_tertiary_stratum_cells;
            }
            set
            {
                m_tertiary_stratum_cells = value;
            }
        }

        public int[] SClassCells
        {
            get
            {
                return m_sclass_cells;
            }
            set
            {
                m_sclass_cells = value;
            }
        }

        public int[] AgeCells
        {
            get
            {
                return m_age_cells;
            }
            set
            {
                m_age_cells = value;
            }
        }

        /// <summary>
        /// Digital Elevation Model (DEM) Cells
        /// </summary>
        public double[] DemCells
        {
            get
            {
                return m_dem_cells;
            }
            set
            {
                m_dem_cells = value;
            }
        }

        public string ProjectionString
        {
            get
            {
                return m_projectionString;
            }
            set
            {
                m_projectionString = value;
            }
        }

        public decimal XllCorner
        {
            get
            {
                return m_XllCorner;
            }
            set
            {
                m_XllCorner = value;
            }
        }

        public decimal YllCorner
        {
            get
            {
                return m_YllCorner;
            }
            set
            {
                m_YllCorner = value;
            }
        }

        public double NoDataValue
        {
            get
            {
                return m_NoDataValue;
            }
            set
            {
                m_NoDataValue = value;
            }
        }

        /// <summary>
        /// Get the NoDataValue as an Integer. Stored internally as a Double
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int NoDataValueAsInteger
        {
            get
            {
                if (m_NoDataValue < int.MinValue || m_NoDataValue > int.MaxValue)
                {
                    return StochasticTimeRaster.DefaultNoDataValue;
                }
                else
                {
                    return Convert.ToInt32(m_NoDataValue);
                }
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
                return m_primary_stratum_name;
            }
            set
            {
                m_primary_stratum_name = value;
            }
        }

        public string SecondaryStratumName
        {
            get
            {
                return m_secondary_stratum_name;
            }
            set
            {
                m_secondary_stratum_name = value;
            }
        }

        public string TertiaryStratumName
        {
            get
            {
                return m_tertiary_stratum_name;
            }
            set
            {
                m_tertiary_stratum_name = value;
            }
        }

        public string StateClassName
        {
            get
            {
                return m_stateClass_name;
            }
            set
            {
                m_stateClass_name = value;
            }
        }

        public string AgeName
        {
            get
            {
                return m_age_name;
            }
            set
            {
                m_age_name = value;
            }
        }

        public string DemName
        {
            get
            {
                return m_dem_name;
            }
            set
            {
                m_dem_name = value;
            }
        }

        /// <summary>
        /// Set the Raster metadata properties within the current class instance
        /// </summary>
        /// <param name="raster">The source of the metadata values</param>
        /// <remarks></remarks>
        public void SetMetadata(StochasticTimeRaster raster)
        {
            this.m_numCols = raster.NumberCols;
            this.m_numRows = raster.NumberRows;
            this.m_cellSize = raster.CellSize;
            this.m_cellSizeUnits = raster.CellSizeUnits;
            this.m_XllCorner = raster.XllCorner;
            this.m_YllCorner = raster.YllCorner;
            this.m_NoDataValue = raster.NoDataValue;
            this.m_projectionString = raster.ProjectionString;
        }

        /// <summary>
        /// Set the metadata properties in the specified Raster object based on the current Metadata values
        /// in the current class instance
        /// </summary>
        /// <param name="raster"></param>
        /// <remarks></remarks>
        public void GetMetadata(StochasticTimeRaster raster)
        {
            raster.NumberCols = m_numCols;
            raster.NumberRows = m_numRows;
            raster.CellSize = m_cellSize;
            raster.CellSizeUnits = m_cellSizeUnits;
            raster.XllCorner = m_XllCorner;
            raster.YllCorner = m_YllCorner;
            raster.NoDataValue = m_NoDataValue;
            raster.ProjectionString = m_projectionString;
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
            if (this.NumberColumns != raster.NumberCols)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Number of Columns ({1} vs {0})", this.NumberColumns, raster.NumberCols);
                return CompareMetadataResult.ImportantDifferences;
            }

            // Test number of rows. 
            if (this.NumberRows != raster.NumberRows)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Number of Rows ({1} vs {0})", this.NumberRows, raster.NumberRows);
                return CompareMetadataResult.ImportantDifferences;
            }

            // Test XLL Corner. See if NOT negligable difference - arbitrarily 1/10 of cell size. 
            // Can't use equality, because of float error 
            if (Math.Abs(this.XllCorner - raster.XllCorner) > (this.CellSize / (decimal)10.0))
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in XllCorner ({1} vs {0})", this.XllCorner, raster.XllCorner);
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            // Test YLL Corner.  See if NOT negligable difference - arbitrarily 1/10 of cell size. 
            // Can't use equality, because of float error  
            if (Math.Abs(this.YllCorner - raster.YllCorner) > (this.CellSize / (decimal)10.0))
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in YllCorner ({1} vs {0})", this.YllCorner, raster.YllCorner);
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            // Test ProjectionString 
            if (this.ProjectionString != raster.ProjectionString)
            {
                compareMsg = string.Format(CultureInfo.InvariantCulture, "Mismatch in Projection String");
                retVal = CompareMetadataResult.UnimportantDifferences;
            }

            // Test Cell Size. Cant use equality because of precision errors ( eg. 30D vs 30.000000000004D)
            if (Math.Abs(this.CellSize - raster.CellSize) > (decimal) 0.0001)
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

            col = cellNumber % this.m_numCols;
            row = cellNumber / this.m_numCols;
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
            Debug.Assert(row < this.NumberRows);
            Debug.Assert(column < this.NumberColumns);

            return row * m_numCols + column;
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

            if (newCol < 0 || newCol >= this.NumberColumns)
            {
                return -1;
            }

            if (newRow < 0 || newRow >= this.NumberRows)
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

            if (newCol < 0 || newCol >= this.NumberColumns)
            {
                return -1;
            }

            if (newRow < 0 || newRow >= this.NumberRows)
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
