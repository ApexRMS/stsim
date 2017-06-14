'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.IO
Imports System.Drawing
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

 Module SpatialUtilities

    Public Function SavePrimaryStratumInputRaster(raster As StochasticTimeRaster, scenario As Scenario, iteration As Integer, timestep As Integer) As String

        Dim fileName As String
        '        Name template = Itx-Tsy-Stratum.tif
        fileName = String.Format(CultureInfo.InvariantCulture, "It{0}-Ts{1}-{2}",
                                 iteration.ToString("0000", CultureInfo.InvariantCulture),
                                 timestep.ToString("0000", CultureInfo.InvariantCulture),
                                 SPATIAL_MAP_STRATUM_VARIABLE_NAME)

        Return RasterFiles.SaveIntegerInputRaster(raster, scenario.GetDataSheet(DATASHEET_SPIC_NAME),fileName)

    End Function

    Public Function SaveSecondaryStratumInputRaster(raster As StochasticTimeRaster, scenario As Scenario, iteration As Integer, timestep As Integer) As String

        Dim fileName As String
        '        Name template = Itx-Tsy-secstr.tif
        fileName = String.Format(CultureInfo.InvariantCulture, "It{0}-Ts{1}-{2}",
                                 iteration.ToString("0000", CultureInfo.InvariantCulture),
                                 timestep.ToString("0000", CultureInfo.InvariantCulture),
                                 SPATIAL_MAP_SECONDARY_STRATUM_VARIABLE_NAME)

        Return RasterFiles.SaveIntegerInputRaster(raster, scenario.GetDataSheet(DATASHEET_SPIC_NAME),fileName)

    End Function

    Public Function SaveStateClassInputRaster(raster As StochasticTimeRaster, scenario As Scenario, iteration As Integer, timestep As Integer) As String

        Dim fileName As String
        '        Name template = Itx-Tsy-sc.tif
        fileName = String.Format(CultureInfo.InvariantCulture, "It{0}-Ts{1}-{2}",
                                 iteration.ToString("0000", CultureInfo.InvariantCulture),
                                 timestep.ToString("0000", CultureInfo.InvariantCulture),
                                 SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME)

        Return RasterFiles.SaveIntegerInputRaster(raster, scenario.GetDataSheet(DATASHEET_SPIC_NAME),fileName)

    End Function

    Public Function SaveAgeInputRaster(raster As StochasticTimeRaster, scenario As Scenario, iteration As Integer, timestep As Integer) As String

        Dim fileName As String
        '        Name template = Itx-Tsy-Age.tif
        fileName = String.Format(CultureInfo.InvariantCulture, "It{0}-Ts{1}-{2}",
                                 iteration.ToString("0000", CultureInfo.InvariantCulture),
                                 timestep.ToString("0000", CultureInfo.InvariantCulture),
                                 SPATIAL_MAP_AGE_VARIABLE_NAME)

        Return RasterFiles.SaveIntegerInputRaster(raster, scenario.GetDataSheet(DATASHEET_SPIC_NAME),fileName)

    End Function



    ''' <summary>
    ''' Create the raster color maps for the specific project. The color maps are QGis compatible, and are use when
    ''' displaying the rasters in the Syncrosim Map display.
    ''' </summary>
    ''' <param name="project">The current Project.</param>
    ''' <param name="mapVariable">The map variable. Ex sc, tg-123,str</param>
    ''' <param name="datasheetName">The name of the datasheet containing the color map configuration</param>
    ''' <param name="dicLegendLblColor">A dictionary with the Map Legend Label as the key, color as value</param>
    ''' <remarks></remarks>
    Public Sub CreateColorMap(project As Project, mapVariable As String, datasheetName As String, dicLegendLblColor As Dictionary(Of String, String))

        If (project.Library.Session.IsRunningOnMono) Then
            Return
        End If

        ' Where are the color maps stored
        Dim colorMapPath As String = project.Library.GetFolderName(LibraryFolderType.Input, project, True)

        ' What's the absolute name of the color map file
        Dim colorMapFilename As String = RasterFiles.GetColorMapFileName(project, mapVariable)

        ' Lets toast the existing color map 
        IO.File.Delete(colorMapFilename)

        Dim fileWriter As StreamWriter = System.IO.File.CreateText(Path.Combine(colorMapPath, colorMapFilename))
        fileWriter.WriteLine("# Syncrosim Generated State Class Color Map (QGIS-compatible),,,,,")
        fileWriter.WriteLine("INTERPOLATION:EXACT")

        'Now create the color maps
        Dim ds As DataSheet = project.GetDataSheet(datasheetName)
        Dim dt As DataTable = ds.GetData()

        ' Check if there any non-null or non-white color definitions

        Dim dv As New DataView(dt, Nothing, DATASHEET_NAME_COLUMN_NAME, DataViewRowState.CurrentRows)
        ' ID, Name, Transparency, and Color value

        For Each dr As DataRowView In dv
            ' ID, Name, Transparency, and Color value
            Dim id As String = dr.Row(DATASHEET_MAPID_COLUMN_NAME).ToString()
            Dim transpenciesRGB = dr.Row(DATASHEET_COLOR_COLUMN_NAME).ToString()
            Dim lbl = dr.Row(DATASHEET_NAME_COLUMN_NAME).ToString()
            Dim mapLegendLbl = dr.Row(DATASHEET_LEGEND_COLUMN_NAME).ToString()

            ' Dont include a color entry for record without ID or defined colors assigned
            If id.Trim().Length > 0 And transpenciesRGB.Length > 0 Then

                ' Do we have a Legend Map for this map Variable. If so we need to get ""fancy""
                If Not (dicLegendLblColor Is Nothing) Then
                    If dicLegendLblColor.Count > 0 Then
                        If mapLegendLbl.Length > 0 Then
                            transpenciesRGB = dicLegendLblColor.Item(mapLegendLbl)
                        Else
                            transpenciesRGB = dicLegendLblColor.Item(LEGEND_MAP_BLANK_LEGEND_ITEM)
                        End If
                    End If
                End If

                Dim aryColor = Split(transpenciesRGB, ",")   ' Split into individual Transparency, Red, Green,Blue
                If UBound(aryColor) = 3 Then
                    ' Color Map line syntax, for discrete values, is:
                    '  Value, Red, Green, Blue, Transparency, Label 
                    '  21001,168,0,87,255,UNDET:<5% Inv
                    fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", id, aryColor(1), aryColor(2), aryColor(3), aryColor(0), lbl)
                End If
            End If
        Next

        fileWriter.Close()

    End Sub

    ''' <summary>
    ''' displaying the rasters in the Syncrosim Map display.
    ''' </summary>
    ''' <param name="project">The current Project.</param>
    ''' <param name="mapVariable">The map variable. Ex sc, tg-123,str</param>
    ''' <param name="datasheetName">The name of the datasheet containing the color map configuration</param>
    ''' <returns>A dictionary of Legend labels(key), and Color values</returns>
    ''' <remarks></remarks>
    Public Function CreateLegendMap(project As Project, mapVariable As String, datasheetName As String) As Dictionary(Of String, String)


        If (project.Library.Session.IsRunningOnMono) Then
            Return Nothing
        End If

        ' Where are the legend maps stored
        Dim colorMapPath As String = project.Library.GetFolderName(LibraryFolderType.Input, project, True)

        ' What's the absolute name of the legend map file
        Dim mapFilename As String = RasterFiles.GetLegendMapFileName(project, mapVariable)

        ' Lets toast the existing color map 
        IO.File.Delete(mapFilename)

        Dim fileWriter As StreamWriter = System.IO.File.CreateText(Path.Combine(colorMapPath, mapFilename))
        fileWriter.WriteLine("# Syncrosim Generated State Class Color Map (QGIS-compatible),,,,,")
        fileWriter.WriteLine("INTERPOLATION:EXACT")

        'Now create the legend color maps
        Dim ds As DataSheet = project.GetDataSheet(datasheetName)
        Dim dt As DataTable = ds.GetData().Copy()

        ' Loop thru and change all Legend nulls to Blank Item string
        For Each dr As DataRow In dt.Rows
            If IsDBNull(dr(DATASHEET_LEGEND_COLUMN_NAME)) Or dr(DATASHEET_LEGEND_COLUMN_NAME).ToString().Trim().Length = 0 Then
                dr(DATASHEET_LEGEND_COLUMN_NAME) = LEGEND_MAP_BLANK_LEGEND_ITEM
            End If
        Next


        Dim sort As String = DATASHEET_LEGEND_COLUMN_NAME & "," & DATASHEET_MAPID_COLUMN_NAME
        Dim filter As String = Nothing
        Dim dv As New DataView(dt, filter, sort, DataViewRowState.CurrentRows)
        Dim legendDefined As New Dictionary(Of String, String)

        For Each dr As DataRowView In dv
            ' ID, Name, Transparency, Color value
            Dim id As String = dr.Row(DATASHEET_MAPID_COLUMN_NAME).ToString()
            Dim transpenciesRGB = dr.Row(DATASHEET_COLOR_COLUMN_NAME).ToString()
            Dim lbl = dr.Row(DATASHEET_LEGEND_COLUMN_NAME).ToString()

            ' Check to see if we've already define this legend lavel
            If Not legendDefined.ContainsKey(lbl) Then

                ' Dont include a color entry for record without ID or defined colors assigned
                If id.Trim().Length > 0 And transpenciesRGB.Length > 0 Then

                    Dim aryColor = Split(transpenciesRGB, ",")   ' Split into individual Transparency, Red, Green,Blue
                    If UBound(aryColor) = 3 Then
                        ' Color Map line syntax, for discrete values, is:
                        '  Value, Red, Green, Blue, Transparency, Label 
                        '  21001,168,0,87,255,UNDET:<5% Inv

                        ' force [blank] to end of legend
                        Dim val As Integer = CInt(IIf(lbl = LEGEND_MAP_BLANK_LEGEND_ITEM, 9999, legendDefined.Count + 1))
                        fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", val, aryColor(1), aryColor(2), aryColor(3), aryColor(0), lbl)
                        legendDefined.Add(lbl, transpenciesRGB)
                    End If
                End If
            End If
        Next

        fileWriter.Close()

        'If not valid entries in legend map, then toast it, or only one - [Blank]
        If (legendDefined.Count = 0) Or (legendDefined.Count = 1 And legendDefined.ContainsKey(LEGEND_MAP_BLANK_LEGEND_ITEM)) Then
            IO.File.Delete(mapFilename)
            legendDefined = Nothing
        End If

        Return legendDefined

    End Function


    ''' <summary>
    ''' Create/Replace the raster Transition Group color maps for the specific project. The color maps are QGis compatible, and are use when
    ''' displaying the Transitions rasters in the Syncrosim Map display.
    ''' </summary>
    ''' <param name="project">The current Project</param>
    ''' <remarks></remarks>
    Public Sub CreateTransitionGroupColorMap(project As Project, drTg As DataRow, dicLegendLblColor As Dictionary(Of String, String))

        Dim dsTg As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)
        Dim dsTTG As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_TYPE_GROUP_NAME)
        Dim dsTT As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_TYPE_NAME)

        Dim dtTTg As DataTable = dsTTG.GetData()
        Dim dtTT As DataTable = dsTT.GetData()

        Dim tgId As String = drTg(dsTg.PrimaryKeyColumn.Name).ToString()
        Dim tgName As String = drTg(DATASHEET_NAME_COLUMN_NAME).ToString()

        Dim colorMapType = SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX & "-" & tgId

        ' What's the absolute name of the color map file
        Dim colorMapFilename As String = RasterFiles.GetColorMapFileName(project, colorMapType)

        ' Lets toast the existing color map 
        IO.File.Delete(colorMapFilename)

        ' Fetch all the transition types for this Transition Group
        Dim sortedTT As New SortedList(Of String, String)
        Dim filter As String = String.Format(CultureInfo.InvariantCulture, "{0}={1}", DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tgId)

        For Each drTTG As DataRow In dtTTg.Select(filter)

            If drTTG.RowState <> DataRowState.Deleted Then

                Dim TtId As String = drTTG(DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME).ToString()

                ' Now fetch the Transition Type record to get ID, Name, Transparency/Color value
                Dim ttFilter As String = String.Format(CultureInfo.InvariantCulture, "{0}={1}", dsTT.PrimaryKeyColumn.Name, TtId)

                If dtTT.Select(ttFilter).Count() > 0 Then

                    Dim drTT As DataRow = dtTT.Select(ttFilter).First

                    Dim id As String = drTT(DATASHEET_MAPID_COLUMN_NAME).ToString()
                    Dim lbl = drTT(DATASHEET_NAME_COLUMN_NAME).ToString()
                    Dim transparencyRGB = drTT(DATASHEET_COLOR_COLUMN_NAME).ToString()
                    Dim mapLegendLbl = drTT(DATASHEET_LEGEND_COLUMN_NAME).ToString()

                    ' Dont include a color entry for Transition Type without ID or colors assigned
                    If id.Trim().Length > 0 And transparencyRGB.Length > 0 Then

                        ' Do we have a Legend Map for this map Variable. If so we need to get ""fancy""
                        If Not (dicLegendLblColor Is Nothing) Then
                            If dicLegendLblColor.Count > 0 Then
                                If mapLegendLbl.Length > 0 Then
                                    transparencyRGB = dicLegendLblColor.Item(mapLegendLbl)
                                Else
                                    transparencyRGB = dicLegendLblColor.Item(LEGEND_MAP_BLANK_LEGEND_ITEM)
                                End If
                            End If
                        End If

                        ' Stuff into a list, so we can sort alphabetically
                        sortedTT.Add(lbl, id & "," & transparencyRGB)
                    End If

                End If

            End If

        Next

        'Now create the new color map for the current Transition Group, sorted alphabetically by label
        'DEVNOTE: Create the color map even if no color definitions, as the display logic looks for this empty definition. 
        ' Otherwise, it'll apply its own, which we dont want
        Dim fileWriter As StreamWriter = System.IO.File.CreateText(colorMapFilename)
        fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "# Syncrosim Generated Transition Group ({0}) Color Map (QGIS-compatible) Export File,,,,,", tgName))
        fileWriter.WriteLine("INTERPOLATION:EXACT")

        For i = 0 To sortedTT.Count - 1
            ' Dont include a color entry for Transition Type without ID or colors assigned

            Dim lbl As String = sortedTT.Keys(i).Replace(",", " ")   ' Dont allow comma in label
            Dim idColor As String = sortedTT.Values(i)
            Dim aryIdColor = Split(idColor, ",")   ' Split into ID, Transparency, Red, Green,Blue

            If UBound(aryIdColor) = 4 Then
                ' Color Map line syntax, for discrete values, is:
                '  Value, Red, Green, Blue, Transparency, Label 
                '  21001,168,0,87,255,UNDET:<5% Inv

                fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", aryIdColor(0), aryIdColor(2), aryIdColor(3), aryIdColor(4), aryIdColor(1), lbl)
            End If

        Next

        fileWriter.Close()


    End Sub

    ''' <summary>
    ''' Create/Replace the raster Transition Group Legend AND Color maps for the specific project. The legend & color maps are QGis compatible, and are use when
    ''' displaying the Transitions rasters in the Syncrosim Map display.
    ''' </summary>
    ''' <param name="project">The current Project</param>
    ''' <remarks></remarks>
    Public Sub CreateTransitionGroupMaps(project As Project)

        If (project.Library.Session.IsRunningOnMono) Then
            Return
        End If


        ' Loop thru the Transition Groups
        For Each drTg As DataRow In project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME).GetData().Select(Nothing, Nothing, DataViewRowState.CurrentRows)

            Dim dicLegendColors = CreateTransitionGroupLegendMap(project, drTg)
            CreateTransitionGroupColorMap(project, drTg, dicLegendColors)
        Next



    End Sub

    ''' <summary>
    ''' Create/Replace the raster Transition Group Legend maps for the specific project. The color maps are QGis compatible, and are use when
    ''' displaying the Transitions rasters in the Syncrosim Map display.
    ''' </summary>
    ''' <param name="project">The current Project</param>
    ''' <remarks></remarks>
    Public Function CreateTransitionGroupLegendMap(project As Project, drTg As DataRow) As Dictionary(Of String, String)


        Dim dsTTG As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_TYPE_GROUP_NAME)
        Dim dsTT As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_TYPE_NAME)
        Dim dsTg As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)

        Dim dtTTg As DataTable = dsTTG.GetData()
        Dim dtTT As DataTable = dsTT.GetData()


        Dim tgId As String = drTg(dsTg.PrimaryKeyColumn.Name).ToString()
        Dim tgName As String = drTg(DATASHEET_NAME_COLUMN_NAME).ToString()
        Dim colorMapType = SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX & "-" & tgId

        ' Where are the color legend stored
        Dim legendMapPath As String = project.Library.GetFolderName(LibraryFolderType.Input, project, False)

        ' What's the absolute name of the legend  map file
        Dim legendMapFilename As String = RasterFiles.GetLegendMapFileName(project, colorMapType)

        ' Lets toast the existing color map 
        IO.File.Delete(legendMapFilename)

        ' Fetch all the transition types for this Transition Group
        Dim sortedTT As New SortedList(Of String, String)
        Dim filter As String = String.Format(CultureInfo.InvariantCulture, "{0}={1}", DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tgId)
        Dim sort As String = DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME

        For Each drTTG As DataRow In dtTTg.Select(filter, sort, DataViewRowState.CurrentRows)

            Dim TtId As String = drTTG(DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME).ToString()

            ' Now fetch the Transition Type record to get ID, Legend Name, Transparency/Color value
            Dim ttFilter As String = String.Format(CultureInfo.InvariantCulture, "{0}={1}", dsTT.PrimaryKeyColumn.Name, TtId)

            If dtTT.Select(ttFilter).Count() > 0 Then

                Dim drTT As DataRow = dtTT.Select(ttFilter).First

                Dim id As String = drTT(DATASHEET_MAPID_COLUMN_NAME).ToString()
                Dim lbl = drTT(DATASHEET_LEGEND_COLUMN_NAME).ToString()
                If lbl.Trim().Length = 0 Then
                    lbl = LEGEND_MAP_BLANK_LEGEND_ITEM
                End If

                Dim transparencyRGB = drTT(DATASHEET_COLOR_COLUMN_NAME).ToString()

                ' Dont include a legend entry for Transition Type without ID or colors or Map label assigned
                If id.Trim().Length > 0 And transparencyRGB.Length > 0 And lbl.Length > 0 Then

                    ' Stuff into a list, so we can sort alphabetically
                    If Not sortedTT.ContainsKey(lbl) Then
                        sortedTT.Add(lbl, id & "," & transparencyRGB)
                    Else
                        ' Use the TT with the lowest ID value
                        Dim oldIdColor As String = sortedTT.Item(lbl)
                        If Integer.Parse(Split(oldIdColor, ",")(0), CultureInfo.InvariantCulture) > CInt(id) Then
                            sortedTT.Item(lbl) = id & "," & transparencyRGB
                        End If
                    End If

                End If

            End If

        Next

        'Now create the new legend map for the current Transition Group, sorted alphabetically by label
        'DEVNOTE: Don't create the legend map if no color/legend definitions. 

        Dim legendColorsDefined As New Dictionary(Of String, String)

        If sortedTT.Count > 0 Then
            Dim fileWriter As StreamWriter = System.IO.File.CreateText(Path.Combine(legendMapPath, legendMapFilename))
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "# Syncrosim Generated Transition Group ({0}) Color Map (QGIS-compatible) Export File,,,,,", tgName))
            fileWriter.WriteLine("INTERPOLATION:EXACT")

            For i = 0 To sortedTT.Count - 1
                ' Dont include a color entry for Transition Type without ID or colors assigned

                Dim lbl As String = sortedTT.Keys(i).Replace(",", " ")   ' Dont allow comma in label
                Dim idColor As String = sortedTT.Values(i)
                Dim aryColor = Split(idColor, ",")   ' Split into ID, Transparency, Red, Green,Blue

                If UBound(aryColor) = 4 Then
                    ' Color Map line syntax, for discrete values, is:
                    '  Value, Red, Green, Blue, Transparency, Label 
                    '  21001,168,0,87,255,UNDET:<5% Inv

                    Dim val As Integer = CInt(IIf(lbl = LEGEND_MAP_BLANK_LEGEND_ITEM, 9999, i + 1))
                    fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", val, aryColor(2), aryColor(3), aryColor(4), aryColor(1), lbl)

                    legendColorsDefined.Add(lbl, String.Join(",", aryColor(1), aryColor(2), aryColor(3), aryColor(4)))
                End If

            Next

            fileWriter.Close()
        End If

        'If not valid entries in legend map, then toast it, or only one - [Blank]
        If (legendColorsDefined.Count = 0) Or (legendColorsDefined.Count = 1 And legendColorsDefined.ContainsKey(LEGEND_MAP_BLANK_LEGEND_ITEM)) Then
            IO.File.Delete(legendMapFilename)
            legendColorsDefined = Nothing
        End If


        Return legendColorsDefined


    End Function



    ''' <summary>
    ''' Create the Age raster color maps for the specific project. The color maps are QGis compatible, and are use when
    ''' displaying the Age rasters in the Syncrosim Map display.
    ''' </summary>
    ''' <param name="project">The current Project.</param>
    ''' <remarks></remarks>
    Public Sub CreateAgeColorMap(project As Project)

        If (project.Library.Session.IsRunningOnMono) Then
            Return
        End If

        Dim colorMapType As String = SPATIAL_MAP_AGE_VARIABLE_NAME

        ' Where are the color maps stored
        Dim colorMapPath As String = project.Library.GetFolderName(LibraryFolderType.Input, project, True)

        ' What's the absolute name of the color map file
        Dim colorMapFilename As String = RasterFiles.GetColorMapFileName(project, colorMapType)

        ' Lets toast the existing color map 
        IO.File.Delete(colorMapFilename)

        Dim cmName = Path.Combine(colorMapPath, colorMapFilename)
        If Not CreateAgeGroupColorMap(project, cmName) Then
            CreateAgeTypeColorMap(project, cmName)
        End If

    End Sub

    ''' <summary>
    ''' Create a Color Map file based on the Age Group configuration for the specified project.
    ''' </summary>
    ''' <param name="project">The project of interest</param>
    ''' <param name="colorMapFilename">The full absolute name of the color map file to be generated</param>
    ''' <returns>True if successful in generating the color map file</returns>
    ''' <remarks></remarks>
    Private Function CreateAgeGroupColorMap(project As Project, ByVal colorMapFilename As String) As Boolean

        'Now create the Age Group color maps
        Dim ds As DataSheet = project.GetDataSheet(DATASHEET_AGE_GROUP_NAME)
        Dim dt As DataTable = ds.GetData()

        If dt Is Nothing Then
            Return False
        End If

        ' Sort by Max Age
        Dim dv As New DataView(dt, Nothing, DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME, DataViewRowState.CurrentRows)
        If dv.Count = 0 Then
            Return False
        End If

        ' See if there any rows that have colors assigned - otherwise we'd create an empty color map, which will be confusing to the Spatial map module
        Dim bColorsExist As Boolean = False

        For Each dr As DataRowView In dv

            Dim transparencyRGB = dr.Row(DATASHEET_COLOR_COLUMN_NAME).ToString()

            If transparencyRGB.Length > 0 Then

                If ColorUtilities.ColorFromString(transparencyRGB).ToArgb() <> Color.White.ToArgb() Then
                    bColorsExist = True
                    Exit For
                End If

            End If
        Next

        If Not bColorsExist Then
            Return False
        End If

        Dim fileWriter As StreamWriter = System.IO.File.CreateText(colorMapFilename)
        fileWriter.WriteLine("# Syncrosim Generated Age Group Color Map (QGIS-compatible),,,,,")
        fileWriter.WriteLine("INTERPOLATION:DISCRETE")

        Dim prevMaxAge As Integer = 0

        For Each dr As DataRowView In dv

            ' ID, Name, Transparency, and Color value
            Dim transparencyRGB = dr.Row(DATASHEET_COLOR_COLUMN_NAME).ToString()
            Dim maxAge = dr.Row(DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME).ToString()

            ' Dont include a color entry for Age Group without a Max Age or defined/non-white colors assigned

            If maxAge.Trim().Length > 0 And transparencyRGB.Length > 0 Then

                If ColorUtilities.ColorFromString(transparencyRGB).ToArgb() <> Color.White.ToArgb() Then

                    Dim aryColor = Split(transparencyRGB, ",")   ' Split into individual Transparency, Red, Green,Blue

                    If UBound(aryColor) = 3 Then

                        ' Color Map line syntax, for discrete values, is:
                        '  Value, Red, Green, Blue, Transparency, Label 
                        '  21001,168,0,87,255,UNDET:<5% Inv

                        Dim lbl As String
                        If prevMaxAge = CInt(maxAge) Then
                            lbl = String.Format(CultureInfo.InvariantCulture, "{0}", maxAge)
                        Else
                            lbl = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", prevMaxAge, maxAge)
                        End If

                        fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", maxAge, aryColor(1), aryColor(2), aryColor(3), aryColor(0), lbl)
                        prevMaxAge = CInt(maxAge) + 1

                    End If

                End If

            End If

        Next

        fileWriter.Close()

        ' return success
        Return True

    End Function

    ''' <summary>
    ''' Create a Color Map file based on the Age Type configuration for the specified project.
    ''' </summary>
    ''' <param name="project">The project of interest</param>
    ''' <param name="colorMapFilename">The full absolute name of the color map file to be generated</param>
    ''' <returns>True if successful in generating the color map file</returns>
    ''' <remarks></remarks>
    Private Function CreateAgeTypeColorMap(project As Project, ByVal colorMapFilename As String) As Boolean

        Dim ageDescriptors As IEnumerable(Of AgeDescriptor)
        ageDescriptors = GetAgeTypeDescriptors(project)

        If ageDescriptors IsNot Nothing Then
            Dim fileWriter As StreamWriter = System.IO.File.CreateText(colorMapFilename)
            fileWriter.WriteLine("# Syncrosim Generated Age Type Color Map (QGIS-compatible),,,,,")
            fileWriter.WriteLine("INTERPOLATION:DISCRETE")

            Dim clr As Color
            Dim binColors = New Color() {Color.Blue, Color.Aqua, Color.Yellow, Color.Orange, Color.Red, Color.ForestGreen, Color.Fuchsia, Color.LawnGreen}

            ' DEVNOTE: We need a way to support some sort of color wheel, so we can generate predictable sequence of colors. We're OK for now

            Dim clrIdx As Integer = 0

            Dim ageLbl As String
            For Each adesc In ageDescriptors
                clr = binColors(clrIdx Mod binColors.Count())  ' repeat colors when we've used them all up
                If adesc.MaximumAge IsNot Nothing Then
                    ageLbl = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", adesc.MinimumAge, adesc.MaximumAge)
                    fileWriter.WriteLine("{0},{1},{2},{3},{4},{5}", adesc.MaximumAge, clr.R, clr.G, clr.B, clr.A, ageLbl)
                Else
                    ' The Spatial map control will add a top level bin to take care of this
                End If
                clrIdx += 1
            Next

            fileWriter.Close()
        End If

        Return True

    End Function

End Module
