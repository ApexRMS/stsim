// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal static class ExceptionUtils
    {
        public static void ThrowArgumentException(string message)
        {
            ThrowArgumentException(message, new object[0]);
        }

        public static void ThrowArgumentException(string message, params object[] args)
        {
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        public static void ThrowInvalidOperationException(string message, params object[] args)
        {
            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, message, args));
        }
    }
}
