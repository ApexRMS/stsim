'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports SyncroSim.StochasticTime.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class StateAttributeRasterMap
    Inherits StochasticTimeExportTransformer

    ReadOnly fileFilterRegex As String = String.Format(CultureInfo.CurrentCulture, FILE_FILTER_ID_REGEX, SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX)

    Protected Overrides Sub Export(location As String, exportType As ExportType)
        StochasticTimeExportTransformer.CopyRasterFiles(Me.GetActiveResultScenarios(), fileFilterRegex, location, AddressOf CreateExportFilename)
    End Sub

    ''' <summary>
    ''' Rename the Spatial Export File, from the short form name generated during Model run, to the user friendly Export name  
    ''' </summary>
    ''' <param name="sourceFileName">The short form filename, as created during Model run spatial output</param>
    ''' <returns>The long form user-friendly Export filename</returns>
    ''' <remarks></remarks>
    Private Function CreateExportFilename(sourceFileName As String) As String
        Return CreateStateAttrExportFilename(sourceFileName)
    End Function

    ''' <summary>
    ''' Create an State Attribute filename using the export filename convention. This involves replacing the Id with the State
    ''' Attribute Name
    ''' </summary>
    ''' <param name="filename">The name of the file as it appears in the internal filenaming convention</param>
    ''' <returns>The filename as it appears in the external filenaming convention</returns>
    ''' <remarks>Internal file convention is Itx-Tsy-sa-z.tif. External convention is ...Sa-StateAttributeName.tif</remarks>
    Private Function CreateStateAttrExportFilename(ByVal filename As String) As String

        ' Pull the Id out of the filename, and convert a name
        Dim m As Match = Regex.Match(filename, fileFilterRegex)
        If Not m.Success Or m.Groups.Count <> 4 Then
            ' Something wrong here, so just return the original filename
            Debug.Assert(False, "Error parsing the State Attribute internal filename.")
            Return filename
        End If

        Dim id As Integer = CInt(m.Groups(2).Value)
        Dim name As String = id.ToString(CultureInfo.InvariantCulture)

        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_STATE_ATTRIBUTE_TYPE_NAME)
        For Each dr As DataRow In ds.GetData.Rows
            If CInt(dr(ds.PrimaryKeyColumn.Name)) = id Then
                name = CStr(dr("NAME"))
                Exit For
            End If
        Next

        Return String.Format(CultureInfo.InvariantCulture, "{0}{1}-{2}.{3}", m.Groups(1), SPATIAL_MAP_EXPORT_STATE_ATTRIBUTE_VARIABLE_PREFIX, name, m.Groups(3))

    End Function

End Class
