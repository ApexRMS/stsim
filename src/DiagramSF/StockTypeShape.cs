// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Drawing;
using SyncroSim.Apex.Forms;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
	internal class StockTypeShape : BoxDiagramShape
	{
		private readonly int m_StockTypeId;
		private readonly List<FlowPathway> m_OutgoingPathways = new List<FlowPathway>();
		private readonly List<FlowPathway> m_IncomingPathways = new List<FlowPathway>();
		private bool m_IsReadOnly;

		public StockTypeShape(int stockTypeId, string displayName) : 
            base(Constants.DIAGRAM_NUM_VERTICAL_CONNECTORS, Constants.DIAGRAM_NUM_HORIZONTAL_CONNECTORS)
		{
			this.TitleBarText = displayName;
			this.m_StockTypeId = stockTypeId;

			Debug.Assert(this.m_StockTypeId > 0);
		}

		public int StockTypeId
		{
			get
			{
				Debug.Assert(this.m_StockTypeId > 0);
				return this.m_StockTypeId;
			}
		}

		public List<FlowPathway> OutgoingFlowPathways
		{
			get
			{
				return this.m_OutgoingPathways;
			}
		}

		public List<FlowPathway> IncomingFlowPathways
		{
			get
			{
				return this.m_IncomingPathways;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return this.m_IsReadOnly;
			}
			set
			{
				this.m_IsReadOnly = value;
			}
		}

		public override string GetToolTipText()
		{
			return this.TitleBarText;
		}

		public override void Render(System.Drawing.Graphics g)
		{
			using (SolidBrush b = new SolidBrush(Constants.DIAGRAM_SHAPE_BACKGROUND_COLOR))
			{
				g.FillRectangle(b, this.Bounds);
			}

			Color TextColor = Constants.DIAGRAM_SHAPE_TEXT_COLOR;
			Color BorderColor = Constants.DIAGRAM_SHAPE_BORDER_COLOR;

			if (this.IsReadOnly)
			{
				TextColor = Constants.DIAGRAM_SHAPE_READONLY_TEXT_COLOR;
				BorderColor = Constants.DIAGRAM_SHAPE_READONLY_BORDER_COLOR;
			}

			using (Pen p = new Pen(BorderColor, Constants.ZOOM_SAFE_PEN_WIDTH))
			{
				g.DrawRectangle(p, this.Bounds);
			}

			Rectangle rc = new Rectangle(this.Bounds.Left, this.Bounds.Top, this.Bounds.Width, this.Bounds.Height);
			rc.Inflate(-4, -4);

			using (StringFormat sf = new StringFormat())
			{
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				if (!this.TitleBarText.Contains(" "))
				{
					sf.FormatFlags = StringFormatFlags.NoWrap;
					sf.Trimming = StringTrimming.EllipsisCharacter;
				}

				using (SolidBrush b = new SolidBrush(TextColor))
				{
					g.DrawString(this.TitleBarText, Constants.DIAGRAM_SHAPE_NORMAL_FONT, b, rc, sf);
				}
			}
		}
	}
}