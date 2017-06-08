'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Windows.Forms

Module RasterUtilities

    Public Function ChooseRasterFileName(ByVal dialogTitle As String, ByVal parent As Control) As String

        Dim dlg As New OpenFileDialog

        dlg.Title = dialogTitle
        dlg.Filter = "GeoTIFF File (*.tif)|*.tif"

        If (dlg.ShowDialog(parent) <> DialogResult.OK) Then
            Return Nothing
        End If

        Return dlg.FileName

    End Function

End Module
