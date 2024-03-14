// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal static class ColorUtilities
    {
        public static Color ColorFromString(string clr)
        {
            string[] rgb = clr.Split(Convert.ToChar(",", CultureInfo.InvariantCulture));
            Debug.Assert(rgb.Length == 4);

            if (rgb.Length == 4)
            {
                return Color.FromArgb(
                    Convert.ToInt32(rgb[0], CultureInfo.InvariantCulture), 
                    Convert.ToInt32(rgb[1], CultureInfo.InvariantCulture), 
                    Convert.ToInt32(rgb[2], CultureInfo.InvariantCulture), 
                    Convert.ToInt32(rgb[3], CultureInfo.InvariantCulture));
            }
            else
            {
                return Color.Black;
            }
        }

        public static string StringFromColor(Color color)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", color.A, color.R, color.G, color.B);
        }
    }
}
