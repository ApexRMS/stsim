// SyncroSim Modeling Framework
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
// The TERMS OF USE and END USER LICENSE AGREEMENT for this software can be found in the LICENSE file.

namespace SyncroSim.Core
{
    public class CellOffset
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public CellOffset(int rowOffset, int columnOffset)
        {
            Column = columnOffset;
            Row = rowOffset;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(this.GetType() == obj.GetType()))
            {
                return false;
            }

            CellOffset p = (CellOffset)obj;
            return this.Row == p.Row && this.Column == p.Column;
        }

        public override int GetHashCode()
        {
            return Row ^ Column;
        }
    }
}
