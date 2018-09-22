// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Globalization;
using System.Windows.Forms;

namespace SyncroSim.STSim
{
    internal static class FormsUtilities
    {
        public static DialogResult ErrorMessageBox(string text)
        {
            return MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
        }

        public static DialogResult ErrorMessageBox(string text, params object[] args)
        {
            return ErrorMessageBox(string.Format(CultureInfo.InvariantCulture, text, args));
        }

        public static DialogResult ApplicationMessageBox(string text, MessageBoxButtons buttons)
        {
            return MessageBox.Show(text, Application.ProductName, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
        }

        public static DialogResult ApplicationMessageBox(string text, MessageBoxButtons buttons, params object[] args)
        {
            return ApplicationMessageBox(string.Format(CultureInfo.InvariantCulture, text, args), buttons);
        }

        public static DialogResult InformationMessageBox(string text)
        {
            return ApplicationMessageBox(text, MessageBoxButtons.OK);
        }

        public static DialogResult InformationMessageBox(string text, params object[] args)
        {
            return ApplicationMessageBox(text, MessageBoxButtons.OK, args);
        }
    }
}
