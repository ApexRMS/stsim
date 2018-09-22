// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Globalization;
using System.Reflection;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal class TransitionGroupDataSheet : DataSheet
    {
        public override void Validate(object proposedValue, string columnName)
        {
            base.Validate(proposedValue, columnName);

            if (columnName == Strings.DATASHEET_NAME_COLUMN_NAME)
            {
                ValidateName(Convert.ToString(proposedValue));
            }
        }

        public override void Validate(DataRow proposedRow, DataTransferMethod transferMethod)
        {
            base.Validate(proposedRow, transferMethod);
            ValidateName(Convert.ToString(proposedRow[Strings.DATASHEET_NAME_COLUMN_NAME]));
        }

        public override void Validate(DataTable proposedData, DataTransferMethod transferMethod)
        {
            base.Validate(proposedData, transferMethod);

            foreach (DataRow dr in proposedData.Rows)
            {
                if (!DataTableUtilities.GetDataBool(dr, Strings.IS_AUTO_COLUMN_NAME))
                {
                    ValidateName(Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME]));
                }
            }
        }

        private static void ValidateName(string name)
        {
            if (name.EndsWith(Strings.AUTO_COLUMN_SUFFIX, StringComparison.Ordinal))
            {
                string msg = string.Format(CultureInfo.InvariantCulture, 
                    "The transition group name cannot have the suffix: '{0}'.", 
                    Strings.AUTO_COLUMN_SUFFIX);

                throw new DataException(msg);
            }
        }
    }
}
