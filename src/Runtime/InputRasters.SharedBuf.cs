// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    public partial class InputRasters
    {
        private int[] m_SharedIntBuffer;
        private double[] m_SharedDoubleBuffer;
        private float[] m_SharedFloatBuffer;

        internal int[] GetSharedIntBuffer()
        {
            if (this.m_SharedIntBuffer == null)
            {
                this.m_SharedIntBuffer = new int[this.m_Width * this.m_Height];
            }

            ResetSharedIntBuffer(this.m_SharedIntBuffer);
            return this.m_SharedIntBuffer;
        }

        internal static void ResetSharedIntBuffer(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Spatial.DefaultNoDataValue;
            }
        }

        internal double[] GetSharedDoubleBuffer()
        {
            if (this.m_SharedDoubleBuffer == null)
            {
                this.m_SharedDoubleBuffer = new double[this.m_Width * this.m_Height];
            }

            ResetSharedDoubleBuffer(this.m_SharedDoubleBuffer);
            return this.m_SharedDoubleBuffer;
        }

        internal static void ResetSharedDoubleBuffer(double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Spatial.DefaultNoDataValue;
            }
        }

        internal float[] GetSharedFloatBuffer()
        {
            if (this.m_SharedFloatBuffer == null)
            {
                this.m_SharedFloatBuffer = new float[this.m_Width * this.m_Height];
            }

            ResetSharedFloatBuffer(this.m_SharedFloatBuffer);
            return this.m_SharedFloatBuffer;
        }

        internal static void ResetSharedFloatBuffer(float[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Spatial.DefaultNoDataValue;
            }
        }
    }
}
