// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Runtime.Serialization;

namespace SyncroSim.STSim
{
    [Serializable()]
    public sealed class STSimException : Exception
    {
        public STSimException() : base("STSim exception")
        {
        }

        public STSimException(string message) : base(message)
        {
        }

        public STSimException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private STSimException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable()]
    public sealed class STSimMapDuplicateItemException : Exception
    {
        public STSimMapDuplicateItemException() : base("Duplicate Item Exception")
        {
        }

        public STSimMapDuplicateItemException(string message) : base(message)
        {
        }

        public STSimMapDuplicateItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private STSimMapDuplicateItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
