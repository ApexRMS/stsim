'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************
Imports System.Globalization
Imports System.IO
Imports SyncroSim.StochasticTime


''' <summary>
''' Class to use when handling the merge operations performed on the TGAP (Average Annual Transition Probability) rasters. This specialized 
''' merging is required when parallel processing is employed, resulting in Job splits by Iteration. This merge should be performed before the
''' Spatial Merge operations are performed, where splitting/merging is based on secondary strata.
''' </summary>
''' <remarks></remarks>
Friend Class TgapMerge

    Dim m_rasterMerge As StochasticTimeRaster

    ''' <summary>
    ''' Arithmetically merge the specified TGAP (Average Annual Transition Probability)  raster with previous raster merges. Call this method once for every TGAP raster file you 
    ''' want to merge. It will copy each non-NO_DATA_VALUE pixel to merged raster.
    ''' </summary>
    ''' <param name="inpRasterFileName">The absolute filename of the TGAP raster file you want to add to the merged result</param>
    ''' <param name="numIterations">The number of iterations performed when generating this Tgap raster. Used for weighting the cell values when merging.</param>
    ''' <remarks>The raster spatial metadata (XLLCorner, cellSize, Proj, etc..) is taken from the 1st file Merge'd, although 
    ''' all merged files should be consistent in this regard.</remarks>
    Public Sub Merge(ByVal inpRasterFileName As String, numIterations As Integer)

        Dim rastInput As New StochasticTimeRaster
        Dim dataType As RasterDataType = RasterDataType.DTDouble

        ' 1st time thru?
        If m_rasterMerge Is Nothing Then

            m_rasterMerge = New StochasticTimeRaster()
            RasterFiles.LoadRasterFile(inpRasterFileName, m_rasterMerge, dataType)

            ' Apply the numIterations to each cell
            m_rasterMerge.ScaleDbl(numIterations)
            Exit Sub

        End If

        RasterFiles.LoadRasterFile(inpRasterFileName, rastInput, dataType)

        ' Crude metadata compare
        If rastInput.NumberCols <> m_rasterMerge.NumberCols Or rastInput.NumberRows <> m_rasterMerge.NumberRows Then
            Dim sMsg As String = String.Format(CultureInfo.InvariantCulture, "The metadata of the merge raster file '{0}' does not match that used in previous raster files.", inpRasterFileName)
            Throw New ArgumentException(sMsg)
        End If

        ' Apply the number of iterations multiplier
        rastInput.ScaleDbl(numIterations)

        ' Now lets arithmetically merge this new raster with previous 
        m_rasterMerge.AddDbl(rastInput)

    End Sub

    Public Sub Multiply(mutliplier As Double)
        m_rasterMerge.ScaleDbl(mutliplier)
    End Sub

    ''' <summary>
    ''' Save the merged result of multiple Merge calls to the specified Raster Output file.
    ''' </summary>
    ''' <param name="mergedRasterOutputFilename">The absolute file name of the raster output file.</param>
    ''' <remarks>The raster spatial metadata (XLLCorner, cellSize, Proj, etc..) is taken from the 1st file Merge'd, although 
    ''' all merged files should be consistent in this regard.</remarks>
    Public Sub Save(mergedRasterOutputFilename As String, compressionType As GeoTiffCompressionType)

        ' Get rid of any existing file
        If (File.Exists(mergedRasterOutputFilename)) Then

            File.SetAttributes(mergedRasterOutputFilename, FileAttributes.Normal)
            File.Delete(mergedRasterOutputFilename)

        End If

        'DEVNOTE: Use Default NODATA_Value for all spatial output raster files
        m_rasterMerge.NoDataValue = StochasticTimeRaster.DefaultNoDataValue

        If Not RasterFiles.ProcessDoubleRasterToFile(m_rasterMerge, mergedRasterOutputFilename, compressionType) Then
            Dim sMsg As String = String.Format(CultureInfo.InvariantCulture, "Unable to process merged raster file '{0}'", mergedRasterOutputFilename)
            Throw New ArgumentException(sMsg)
        End If

        Debug.Print("Saved Merged TGAP file to '" & mergedRasterOutputFilename & "'")

    End Sub

End Class
