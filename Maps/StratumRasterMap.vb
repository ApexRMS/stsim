'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Reflection
Imports SyncroSim.Core.Forms
Imports SyncroSim.StochasticTime.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class StratumRasterMap
    Inherits StochasticTimeExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)

        Dim fileFilterRegex As String = ".*" & SPATIAL_MAP_STRATUM_VARIABLE_NAME & "\.(tif|vrt)"
        StochasticTimeExportTransformer.CopyRasterFiles(Me.GetActiveResultScenarios(), fileFilterRegex, location, AddressOf CreateExportFilename)

    End Sub

    ''' <summary>
    ''' Rename the Spatial Export File, from the short form name generated during Model run, to the user friendly Export name  
    ''' </summary>
    ''' <param name="sourceName">The short form filename, as created during Model run spatial output</param>
    ''' <returns>The long form user-friendly Export filename</returns>
    ''' <remarks></remarks>
    Private Function CreateExportFilename(sourceName As String) As String
        Return sourceName.Replace(SPATIAL_MAP_STRATUM_VARIABLE_NAME, SPATIAL_MAP_EXPORT_STRATUM_VARIABLE_NAME)
    End Function

End Class
