'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Drawing
Imports System.Globalization

Module ColorUtilities

    Public Function ColorFromString(clr As String) As Color

        Dim rgb As String() = clr.Split(Convert.ToChar(",", CultureInfo.InvariantCulture))
        Debug.Assert(rgb.Length = 4)

        If (rgb.Length = 4) Then

            Return Color.FromArgb(
                Convert.ToInt32(rgb(0), CultureInfo.InvariantCulture),
                Convert.ToInt32(rgb(1), CultureInfo.InvariantCulture),
                Convert.ToInt32(rgb(2), CultureInfo.InvariantCulture),
                Convert.ToInt32(rgb(3), CultureInfo.InvariantCulture))

        Else
            Return Color.Black
        End If

    End Function

    Public Function StringFromColor(color As Color) As String
        Return String.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", color.A, color.R, color.G, color.B)
    End Function

End Module
