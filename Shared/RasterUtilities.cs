// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Windows.Forms;

namespace SyncroSim.STSim
{
    internal static class RasterUtilities
    {
        public static string ChooseRasterFileName(string dialogTitle, Control parent)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = dialogTitle;
            dlg.Filter = "GeoTIFF File (*.tif)|*.tif";

            if (dlg.ShowDialog(parent) != DialogResult.OK)
            {
                return null;
            }

            return dlg.FileName;
        }
    }
}
