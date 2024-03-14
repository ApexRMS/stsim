// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Windows.Forms;
using SyncroSim.Apex.Forms;

namespace SyncroSim.STSim
{
	internal class FlowPathwayTabStripItem : TabStripItem
	{
		private Control m_Control;

		public FlowPathwayTabStripItem(string text) : base(text)
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

		protected override void Dispose(bool disposing)
		{
			if (this.m_Control != null)
			{
				this.m_Control.Dispose();
				this.m_Control = null;
			}

			base.Dispose(disposing);
		}
	}
}