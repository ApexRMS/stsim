// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;
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

    public static class MultiResolutionExceptionHandling
    {
        public static void ThrowArgumentException(string message)
        {
            ThrowArgumentException(message, new object[0]);
        }

        public static void ThrowArgumentException(string message, params object[] args)
        {
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        public static void ThrowInvalidOperationException(string message)
        {
            ThrowInvalidOperationException(message, new object[0]);
        }

        public static void ThrowInvalidOperationException(string message, params object[] args)
        {
            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        public static void ThrowRasterValidationException(string message, string multiResolutionFileName, string stsimFileName)
        {
            string s = message + Environment.NewLine + multiResolutionFileName + Environment.NewLine + stsimFileName;
            ThrowArgumentException(s);
        }
    }
}
