'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Option Strict Off
Option Explicit Off

Imports SyncroSim.StochasticTime

Public Class InputRasters

    Private m_numRows As Integer    ' Number of cell rows
    Private m_numCols As Integer     ' Number of cell columns
    Private m_cellSize As Decimal   ' Cell size
    Private m_XllCorner As Decimal  'X coordinate of the origin (by lower left corner of the cell).	
    Private m_YllCorner As Decimal 'Y coordinate of the origin (by lower left corner of the cell).
    Private m_sclass_cells() As Integer  ' Single dimension array of State Class raster cells. 
    Private m_stratum_cells() As Integer  ' Single dimension array of Primary Stratum raster cells. 
    Private m_seconday_stratum_cells() As Integer  ' Single dimension array of Secondary Stratum raster cells. 
    Private m_age_cells() As Integer  ' Single dimension array of Age raster cells. 
    Private m_dem_cells() As Double  ' Single dimension array of Digital Elevation Model (DEM) raster cells. 
    Private m_NoDataValue As Double = -9999  ' The NODATA value of the raster
    Private m_projectionString As String    ' Store the contents of the Raster .prj file here, so we can create a name.prj file when Exporting Raster
    Private m_cellArea As Double    ' The cell area
    Private m_cellAreaOverride As Boolean   ' Is the cell area overriden by the user
    Private m_cellSizeUnits As String       ' The raster native cell units
    Private m_primary_stratum_name As String     'The name of the Primary Stratum raster file
    Private m_secondary_stratum_name As String     'The name of the Primary Stratum raster file
    Private m_stateClass_name As String      'The name of the State Class raster file
    Private m_age_name As String     'The name of the Age raster file
    Private m_dem_name As String     'The name of the DEM raster file

    Public Property NumberRows() As Integer
        Get
            Return m_numRows
        End Get
        Set(ByVal value As Integer)
            m_numRows = value
        End Set
    End Property

    Public Property NumberColumns() As Integer
        Get
            Return m_numCols
        End Get
        Set(ByVal value As Integer)
            m_numCols = value
        End Set
    End Property

    Public ReadOnly Property NumberCells() As Integer
        Get
            Return m_numCols * m_numRows
        End Get
    End Property

    Public Property CellSize() As Decimal
        Get
            Return m_cellSize
        End Get
        Set(ByVal value As Decimal)
            m_cellSize = value
        End Set
    End Property

    ''' <summary>
    ''' Primary Stratum Cells
    ''' </summary>
    Public Property StratumCells() As Integer()
        Get
            Return m_stratum_cells
        End Get
        Set(ByVal value As Integer())
            m_stratum_cells = value
        End Set
    End Property

    ''' <summary>
    ''' Secondary Stratum Cells
    ''' </summary>
    Public Property SecondaryStratumCells() As Integer()
        Get
            Return m_seconday_stratum_cells
        End Get
        Set(ByVal value As Integer())
            m_seconday_stratum_cells = value
        End Set
    End Property

    Public Property SClassCells() As Integer()
        Get
            Return m_sclass_cells
        End Get
        Set(ByVal value As Integer())
            m_sclass_cells = value
        End Set
    End Property

    Public Property AgeCells() As Integer()
        Get
            Return m_age_cells
        End Get
        Set(ByVal value As Integer())
            m_age_cells = value
        End Set
    End Property

    ''' <summary>
    ''' Digital Elevation Model (DEM) Cells
    ''' </summary>
    Public Property DemCells() As Double()
        Get
            Return m_dem_cells
        End Get
        Set(ByVal value As Double())
            m_dem_cells = value
        End Set
    End Property

    Public Property ProjectionString() As String
        Get
            Return m_projectionString
        End Get
        Set(ByVal value As String)
            m_projectionString = value
        End Set
    End Property

    Public Property XllCorner() As Decimal
        Get
            Return m_XllCorner
        End Get
        Set(ByVal value As Decimal)
            m_XllCorner = value
        End Set
    End Property

    Public Property YllCorner() As Decimal
        Get
            Return m_YllCorner
        End Get
        Set(ByVal value As Decimal)
            m_YllCorner = value
        End Set
    End Property

    Public Property NoDataValue() As Double
        Get
            Return m_NoDataValue
        End Get
        Set(ByVal value As Double)
            m_NoDataValue = value
        End Set
    End Property

    ''' <summary>
    ''' Get the NoDataValue as an Integer. Stored internally as a Double
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property NoDataValueAsInteger() As Integer
        Get
            If m_NoDataValue < Integer.MinValue Or m_NoDataValue > Integer.MaxValue Then
                Return ApexRaster.DEFAULT_NO_DATA_VALUE
            Else
                Return CInt(m_NoDataValue)
            End If

        End Get
    End Property

    Public Property CellArea() As Double
        Get
            Return m_cellArea
        End Get
        Set(ByVal value As Double)
            m_cellArea = value
        End Set
    End Property

    Public Property CellAreaOverride() As Boolean
        Get
            Return m_cellAreaOverride
        End Get
        Set(ByVal value As Boolean)
            m_cellAreaOverride = value
        End Set
    End Property

    Public Property CellSizeUnits() As String
        Get
            Return m_cellSizeUnits
        End Get
        Set(ByVal value As String)
            m_cellSizeUnits = value
        End Set
    End Property

    Public Property PrimaryStratumName As String
        Get
            Return m_primary_stratum_name
        End Get
        Set(value As String)
            m_primary_stratum_name = value
        End Set
    End Property

    Public Property SecondaryStratumName As String
        Get
            Return m_secondary_stratum_name
        End Get
        Set(value As String)
            m_secondary_stratum_name = value
        End Set
    End Property

    Public Property StateClassName As String
        Get
            Return m_stateClass_name
        End Get
        Set(value As String)
            m_stateClass_name = value
        End Set
    End Property

    Public Property AgeName As String
        Get
            Return m_age_name
        End Get
        Set(value As String)
            m_age_name = value
        End Set
    End Property

    Public Property DemName As String
        Get
            Return m_dem_name
        End Get
        Set(value As String)
            m_dem_name = value
        End Set
    End Property

    ''' <summary>
    ''' Set the Raster metadata properties within the current class instance
    ''' </summary>
    ''' <param name="rast">The source of the metadata values</param>
    ''' <remarks></remarks>
    Public Sub SetMetadata(rast As ApexRaster)

        Me.m_numCols = rast.NumberCols
        Me.m_numRows = rast.NumberRows
        Me.m_cellSize = rast.CellSize
        Me.m_cellSizeUnits = rast.CellSizeUnits
        Me.m_XllCorner = rast.XllCorner
        Me.m_YllCorner = rast.YllCorner
        Me.m_NoDataValue = rast.NoDataValue
        Me.m_projectionString = rast.ProjectionString

    End Sub

    ''' <summary>
    ''' Set the metadata properties in the specified Raster object based on the current Metadata values
    ''' in the current class instance
    ''' </summary>
    ''' <param name="rast"></param>
    ''' <remarks></remarks>
    Public Sub GetMetadata(rast As ApexRaster)

        rast.NumberCols = m_numCols
        rast.NumberRows = m_numRows
        rast.CellSize = m_cellSize
        rast.CellSizeUnits = m_cellSizeUnits
        rast.XllCorner = m_XllCorner
        rast.YllCorner = m_YllCorner
        rast.NoDataValue = m_NoDataValue
        rast.ProjectionString = m_projectionString

    End Sub

    Enum CompareMetadataResult
        Same
        UnimportantDifferences
        ImportantDifferences
    End Enum
    ''' <summary>
    ''' Compare the values of the metadata properties to those of the raster argument
    ''' </summary>
    ''' <param name="rast">A instance of class Raster</param>
    ''' <returns>An Enum containing the comparison Result</returns>
    ''' <remarks></remarks>
    Public Function CompareMetadata(rast As ApexRaster, ByRef compareMsg As String) As CompareMetadataResult

        Dim retVal As CompareMetadataResult = CompareMetadataResult.Same
        compareMsg = ""

        ' Test number of cols. 
        If Me.NumberColumns <> rast.NumberCols Then
            compareMsg = String.Format("Mismatch in Number of Columns ({1} vs {0})", Me.NumberColumns, rast.NumberCols)
            Return CompareMetadataResult.ImportantDifferences
        End If

        ' Test number of rows. 
        If Me.NumberRows <> rast.NumberRows Then
            compareMsg = String.Format("Mismatch in Number of Rows ({1} vs {0})", Me.NumberRows, rast.NumberRows)
            Return CompareMetadataResult.ImportantDifferences
        End If

        ' Test XLL Corner. See if NOT negligable difference - arbitrarily 1/10 of cell size. 
        ' Can't use equality, because of float error 
        If Math.Abs(Me.XllCorner - rast.XllCorner) > (Me.CellSize / 10.0) Then
            compareMsg = String.Format("Mismatch in XllCorner ({1} vs {0})", Me.XllCorner, rast.XllCorner)
            retVal = CompareMetadataResult.UnimportantDifferences
        End If

        ' Test YLL Corner.  See if NOT negligable difference - arbitrarily 1/10 of cell size. 
        ' Can't use equality, because of float error  
        If Math.Abs(Me.YllCorner - rast.YllCorner) > (Me.CellSize / 10.0) Then
            compareMsg = String.Format("Mismatch in YllCorner ({1} vs {0})", Me.YllCorner, rast.YllCorner)
            retVal = CompareMetadataResult.UnimportantDifferences
        End If

        ' Test ProjectionString 
        If Me.ProjectionString <> rast.ProjectionString Then
            compareMsg = String.Format("Mismatch in Projection String")
            retVal = CompareMetadataResult.UnimportantDifferences
        End If

        ' Test Cell Size. Cant use equality because of precision errors ( eg. 30D vs 30.000000000004D)
        If Math.Abs(Me.CellSize - rast.CellSize) > 0.0001 Then
            compareMsg = String.Format("Mismatch in Cell Size ({1} vs {0})", Me.CellSize, rast.CellSize)
            retVal = CompareMetadataResult.UnimportantDifferences
        End If

        ' Test Cell Units
        If Me.CellSizeUnits <> rast.CellSizeUnits Then
            compareMsg = String.Format("Mismatch in Cell Size Units ({1} vs {0})", Me.CellSizeUnits, rast.CellSizeUnits)
            retVal = CompareMetadataResult.UnimportantDifferences
        End If

        Return retVal

    End Function

    ''' <summary>
    ''' Get the row and column based on the specified cell Id. All 0-index
    ''' </summary>
    ''' <param name="cellNumber">The cellid we would like to convert</param>
    ''' <param name="row">The converted row number</param>
    ''' <param name="col">The converted column number</param>
    ''' <remarks></remarks>
    Public Sub GetRowColForId(ByVal cellNumber As Integer, ByRef row As Integer, ByRef col As Integer)

        Debug.Assert(cellNumber < Me.NumberCells)

        col = cellNumber Mod Me.m_numCols
        row = cellNumber \ Me.m_numCols

    End Sub

    ''' <summary>
    ''' Get ID based on the row and column specified.
    ''' </summary>
    ''' <param name="row">The row number</param>
    ''' <param name="column">The column number</param>
    ''' <returns>The ID for the specified row and column</returns>
    ''' <remarks></remarks>
    Public Function GetIdForRowCol(ByVal row As Integer, ByVal column As Integer) As Integer

        Debug.Assert(row < Me.NumberRows)
        Debug.Assert(column < Me.NumberColumns)

        Return row * m_numCols + column

    End Function

    ''' <summary>
    ''' Get the Cell Id, given an offset from the specified cell. 
    ''' </summary>
    ''' <param name="initiationCellId">The source cell which we've calculating our offset from</param>
    ''' <param name="rowOffset">The row offset that we're looking for. Specify -1 for N,NW,NE 0 for W,E, and 1 for S,SW, SE</param>
    ''' <param name="colOffset">The column offset we're interested in. Specify -1 for W, NW,SW, 0 for N,S, and +1 for E, NE, SE</param>
    ''' <returns>The cell id of the cell neighbor we're looking for. Return -1 if out of bounds.</returns>
    ''' <remarks>
    ''' With the assumption that the raster is orientated with its 0,0 corner representation the NW extreme, and MaxRow, MaxCol represented 
    ''' the SE extreme, smaller rows are more northerly, larger more southerly. For columns, lower are West, larger are East. Thus the compass directions
    ''' can be represented as:
    ''' -1,-1 -1,0  -1,1
    '''  0,-1  0,0   0,1
    '''  1,-1  1,0   1,1
    ''' </remarks>
    Public Function GetCellIdByOffset(ByVal initiationCellId As Integer, rowOffset As Integer, colOffset As Integer) As Integer

        Dim cellRow As Integer
        Dim cellCol As Integer


        Me.GetRowColForId(initiationCellId, cellRow, cellCol)

        ' For NW, specify row/col offset of -1,-1
        Dim newCol As Integer = cellCol + colOffset
        Dim newRow As Integer = cellRow + rowOffset

        If newCol < 0 Or newCol >= Me.NumberColumns Then
            Return -1
        End If

        If newRow < 0 Or newRow >= Me.NumberRows Then
            Return -1
        End If

        Return GetIdForRowCol(newRow, newCol)

    End Function
    ''' <summary>
    ''' Get the Cell Id, given an offset from the specified cell. 
    ''' </summary>
    ''' <param name="cellRow">The row of the cell from which we're calculating offset from</param>
    ''' <param name="cellColumn">The column of the cell from which we're calculating offset from</param>
    ''' <param name="rowOffset">The row offset that we're looking for. Specify -1 for N,NW,NE 0 for W,E, and 1 for S,SW, SE</param>
    ''' <param name="colOffset">The column offset we're interested in. Specify -1 for W, NW,SW, 0 for N,S, and +1 for E, NE, SE</param>
    ''' <returns>The cell id of the cell neighbor we're looking for. Return -1 if out of bounds.</returns>
    ''' <remarks>
    ''' With the assumption that the raster is orientated with its 0,0 corner representation the NW extreme, and MaxRow, MaxCol represented 
    ''' the SE extreme, smaller rows are more northerly, larger more southerly. For columns, lower are West, larger are East. Thus the compass directions
    ''' can be represented as:
    ''' -1,-1 -1,0  -1,1
    '''  0,-1  0,0   0,1
    '''  1,-1  1,0   1,1
    ''' </remarks>
    Public Function GetCellIdByOffset(ByVal cellRow As Integer, ByVal cellColumn As Integer, rowOffset As Integer, colOffset As Integer) As Integer


        ' For NW, specify row/col offset of -1,-1
        Dim newCol As Integer = cellColumn + colOffset
        Dim newRow As Integer = cellRow + rowOffset

        If newCol < 0 Or newCol >= Me.NumberColumns Then
            Return -1
        End If

        If newRow < 0 Or newRow >= Me.NumberRows Then
            Return -1
        End If

        Return GetIdForRowCol(newRow, newCol)

    End Function

    ''' <summary>
    ''' Get the Cell Size in Meters
    ''' </summary>
    ''' <returns>The Cell size in Meters</returns>
    ''' <remarks></remarks>
    Public Function GetCellSizeMeters() As Double

        ' DEVNOTE: TKR - For now assume the native units of the raster is units. At some point in time,
        ' will have to take into account Cell Size Overide
        Return Me.m_cellSize

    End Function

    ''' <summary>
    ''' Get the Cell Diagonal distance in Metres
    ''' </summary>
    ''' <returns>The Cell diagonal distance in Metres</returns>
    ''' <remarks></remarks>
    Public Function GetCellSizeDiagonalMeters() As Double

        Dim cellSizeM = Me.GetCellSizeMeters()
        Return Math.Sqrt(2 * cellSizeM ^ 2)

    End Function

    ''' <summary>Gets the cell neighbor ID based on specified direction and distance </summary>
    ''' <param name="InitiationCellId">The cell we're looking for the neighbor of</param>
    ''' <param name="Degrees">The direction in degrees.</param>
    ''' <param name="Distance">The distance in metres</param>
    ''' <returns>The Cell object at the specified direction/distance</returns>
    ''' <remarks>Determines The cell id based on the direction and distance from the origin cell center
    ''' If Distance outside of cell takes you outside of existing extent of landscape returns Nothing
    ''' </remarks>
    Public Function GetCellIdByDistanceAndDirection(ByVal initiationCellId As Integer, ByVal degrees As Integer, ByVal distance As Double) As Integer

        Dim cellSizeM = Me.GetCellSizeMeters()

        Dim angle As Double = Math.PI * degrees / 180.0
        Dim horizDist As Double = distance * Math.Cos(angle)
        Dim vertDist As Double = distance * Math.Sin(angle)

        ' Take into account the cellsize. So for instance, if a CellSize = 30, then its (exclusive) bounds are +/- 15, so the 
        ' the next cell range is >=15 and < 45, and >-45 and =<-15. 
        If horizDist < 0 Then
            horizDist -= cellSizeM / 2
        Else
            horizDist += cellSizeM / 2
        End If

        If vertDist < 0 Then
            vertDist -= cellSizeM / 2
        Else
            vertDist += cellSizeM / 2
        End If

        Dim colOffset = Fix(horizDist / cellSizeM)
        Dim rowOffset = Fix(vertDist / cellSizeM)

        Dim id As Integer = GetCellIdByOffset(initiationCellId, -rowOffset, colOffset)
        If id = -1 Then
            Return Nothing
        Else
            Return id
        End If

    End Function

    ''' <summary>Gets the offsets for neighbor cells contained within the specified radius</summary>
    ''' <param name="radius">The radius of the search circle in metres</param>
    ''' <returns>A List of cell offset founds</returns>
    Public Function GetCellNeighborOffsetsForRadius(ByVal radius As Double) As IEnumerable(Of CellOffset)


        ' The max number of cell rows and columns for the specified radius
        Dim numRadiusCells As Integer = Math.Truncate((radius) / Me.GetCellSizeMeters())

        ' To determine the maximum cell extent within a circle described by a radius, we calculate the relative distance of a 
        ' cell centroid from the circle centre, and compare it to the radius. If large than the radius, then we move "closer",
        ' by decrementing to the next closer cell and repeat comparison. The result is the maximum cell extent for a quadrant, which
        ' we can mirror for the other three quadrants. 

        ' Let evaluate the NE quadrant ( all positive values). 
        Dim maxColRows(numRadiusCells) As Integer

        Dim y As Integer = numRadiusCells
        For x As Integer = 0 To numRadiusCells
            maxColRows(x) = -1

            Do Until y = -1

                ' Is the cell centroid outside the circle radius.
                If GetRelativeCellDistance(y, x) > radius Then
                    y = y - 1
                Else
                    ' Save the y coordinate for later
                    maxColRows(x) = y
                    Exit Do
                End If
            Loop

        Next

        ' We've got the NE quadrant max row/cols pairs, so lets use these values to determine all the enclosed cells
        ' for all quadrants.
        Dim listCellOffsets As New List(Of CellOffset)
        For column = 0 To maxColRows.GetUpperBound(0)
            For row = 0 To maxColRows(column)
                'NE quadrant
                AddCellOffsetToList(listCellOffsets, row, column)
                'NW quadrant
                AddCellOffsetToList(listCellOffsets, row, -column)
                'SE quadrant
                AddCellOffsetToList(listCellOffsets, -row, column)
                'SW quadrant
                AddCellOffsetToList(listCellOffsets, -row, -column)
            Next
        Next

        Return listCellOffsets

    End Function

    ''' <summary>
    ''' Add a cell offset value to a list object, checking for uniqueness
    ''' </summary>
    ''' <param name="listCellIds"></param>
    ''' <param name="rowOffset"></param>
    ''' <param name="columnOffset"></param>
    ''' <remarks></remarks>
    Private Shared Sub AddCellOffsetToList(ByRef listCellIds As List(Of CellOffset), rowOffset As Integer, columnOffset As Integer)

        Dim coord As New CellOffset(rowOffset, columnOffset)
        If Not listCellIds.Contains(coord) Then
            listCellIds.Add(coord)
        End If

    End Sub

    ''' <summary>
    ''' Get the relative distance between two cells.
    ''' </summary>
    ''' <param name="rowDiff">The number of rows that the cells are apart</param>
    ''' <param name="colDiff">The number of columns that the cells are aparat</param>
    ''' <returns>The distance between the two cells</returns>
    ''' <remarks>This a generalize function, where we only care about the relative distance, and not the acutal cells themselves.</remarks>
    Private Function GetRelativeCellDistance(rowDiff As Integer, colDiff As Integer) As Double

        Return Math.Sqrt(rowDiff ^ 2 + colDiff ^ 2) * Me.CellSize

    End Function

End Class


