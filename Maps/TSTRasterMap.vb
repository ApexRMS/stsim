'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Globalization
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports SyncroSim.StochasticTime.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class TSTRasterMap
    Inherits StochasticTimeExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim fileFilterRegex As String = ".*" & SPATIAL_MAP_TST_VARIABLE_NAME & "\.(tif|vrt)$"
        StochasticTimeExportTransformer.CopyRasterFiles(Me.GetActiveResultScenarios(), fileFilterRegex, location, AddressOf CreateExportFilename)

    End Sub

    ''' <summary>
    ''' Create a TST (Time Since Transition) filename using the export filename convention. This involves replacing the Id with the Transition 
    ''' Group Name
    ''' </summary>
    ''' <param name="filename">The name of the file as it appears in the internal filenaming convention</param>
    ''' <returns>The filename as it appears in the external filenaming convention</returns>
    ''' <remarks>Internal file convention is Itx-Tsy-tg-z-tst.tif. External convention is ...Tg-TransitionGroup-Tst.tif</remarks>
    Private Function CreateExportFilename(ByVal filename As String) As String

        ' Pull the Id out of the filename, and convert a name
        Dim m As Match = Regex.Match(filename, "^(.*)" & SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX & "-([\d]*)-" & SPATIAL_MAP_TST_VARIABLE_NAME & "\.(tif|vrt)")
        If Not m.Success Or m.Groups.Count <> 4 Then
            ' Something wrong here, so just return the original filename
            Debug.Assert(False, "Error parsing the TST internal filename.")
            Return filename
        End If

        Dim id As Integer = CInt(m.Groups(2).Value)
        Dim name As String = id.ToString(CultureInfo.InvariantCulture)

        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)

        For Each dr As DataRow In ds.GetData.Rows
            If CInt(dr(ds.PrimaryKeyColumn.Name)) = id Then
                name = CStr(dr("NAME"))
                Exit For
            End If
        Next

        Return String.Format(CultureInfo.InvariantCulture, "{0}{1}-{2}-{3}.{4}", m.Groups(1), SPATIAL_MAP_EXPORT_TRANSITION_GROUP_VARIABLE_PREFIX, name, SPATIAL_MAP_EXPORT_TST_VARIABLE_NAME, m.Groups(3))

    End Function

End Class
