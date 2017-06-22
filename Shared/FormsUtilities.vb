'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************
Imports System.Globalization
Imports System.Windows.Forms

Module FormsUtilities

    Function ErrorMessageBox(text As [String]) As DialogResult
        Return MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1, DirectCast(0, MessageBoxOptions))
    End Function

    Function ErrorMessageBox(text As [String], ParamArray args As Object()) As DialogResult
        Return ErrorMessageBox([String].Format(CultureInfo.InvariantCulture, text, args))
    End Function

    Function ApplicationMessageBox(text As [String], buttons As MessageBoxButtons) As DialogResult
        Return MessageBox.Show(text, Application.ProductName, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, DirectCast(0, MessageBoxOptions))
    End Function

    Function ApplicationMessageBox(text As [String], buttons As MessageBoxButtons, ParamArray args As Object()) As DialogResult
        Return ApplicationMessageBox([String].Format(CultureInfo.InvariantCulture, text, args), buttons)
    End Function

    Function InformationMessageBox(text As [String]) As DialogResult
        Return ApplicationMessageBox(text, MessageBoxButtons.OK)
    End Function

    Function InformationMessageBox(text As [String], ParamArray args As Object()) As DialogResult
        Return ApplicationMessageBox(text, MessageBoxButtons.OK, args)
    End Function

End Module
