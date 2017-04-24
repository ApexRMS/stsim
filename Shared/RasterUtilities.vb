'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Windows.Forms

Module RasterUtilities

    ''' <summary>
    ''' Propmts the user for a raster file name
    ''' </summary>
    ''' <param name="dialogTitle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChooseRasterFileName(ByVal dialogTitle As String) As String

        Dim dlg As New OpenFileDialog

        dlg.Title = dialogTitle
        dlg.Filter = "GeoTIFF File (*.tif)|*.tif"

        If (dlg.ShowDialog() <> DialogResult.OK) Then
            Return Nothing
        End If

        Return dlg.FileName

    End Function



End Module
