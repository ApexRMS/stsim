// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class SizeClassHelper
    {
        private List<SizeClass> m_SizeClasses = new List<SizeClass>();
        private Dictionary<double, SizeClass> m_Hint = new Dictionary<double, SizeClass>();

        public SizeClassHelper(DataSheet sizeClassDatasheet)
        {
            DataTable dt = sizeClassDatasheet.GetData();

            foreach (DataRow dr in dt.Rows)
            {
                SizeClass sc = new SizeClass();

                sc.SizeClassID = Convert.ToInt32(dr[sizeClassDatasheet.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture);
                sc.MaximumSize = Convert.ToDouble(dr[Strings.DATASHEET_SIZE_CLASS_MAXIMUM_SIZE_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (this.m_Hint.ContainsKey(sc.MaximumSize))
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, 
                        "Multiple size class definitions found for: {N}", sc.MaximumSize));
                }

                this.m_SizeClasses.Add(sc);
            }
        }

        public object GetSizeClassDatabaseValue(double value)
        {
            SizeClass sc = this.GetSizeClass(value);

            if (sc == null)
            {
                return DBNull.Value;
            }
            else
            {
                Debug.Assert(sc.SizeClassID > 0);
                return sc.SizeClassID;
            }
        }

        private SizeClass GetSizeClass(double value)
        {
            if (this.m_SizeClasses.Count == 0)
            {
                return null;
            }

            Debug.Assert(this.m_SizeClasses.Count > 0);

            if (this.m_Hint.ContainsKey(value))
            {
                return this.m_Hint[value];
            }

            SizeClass This = null;

            foreach (SizeClass sc in this.m_SizeClasses)
            {
                if (sc.MaximumSize > value)
                {
                    This = sc;
                    break;
                }
            }

            return This;          
        }
    }

    class SizeClass
    {
        public int SizeClassID;
        public double MaximumSize;
    }
}
