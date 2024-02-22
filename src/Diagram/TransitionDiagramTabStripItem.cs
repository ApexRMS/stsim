// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Windows.Forms;
using SyncroSim.Apex.Forms;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionDiagramTabStripItem : TabStripItem
    {
        private Control m_Control;
        private bool m_RefreshRequired = true;
        private bool m_IsDisposed;

        public TransitionDiagramTabStripItem(string text) : base(text)
        {
        }

        public Control Control
        {
            get
            {
                return this.m_Control;
            }
            set
            {
                Debug.Assert(this.m_Control == null);
                this.m_Control = value;
            }
        }

        public bool RefreshRequired
        {
            get
            {
                return this.m_RefreshRequired;
            }
            set
            {
                this.m_RefreshRequired = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (!this.m_IsDisposed))
            {
                if (this.m_Control != null)
                {
                    this.m_Control.Dispose();
                    this.m_Control = null;
                }

                this.m_IsDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
