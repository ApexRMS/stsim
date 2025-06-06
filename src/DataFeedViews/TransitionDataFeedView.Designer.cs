﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class TransitionDataFeedView : SyncroSim.Core.Forms.DataFeedView
    {
        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components = null;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.PanelZoomControls = new System.Windows.Forms.Panel();
            this.ButtonZoomIn = new System.Windows.Forms.Button();
            this.ButtonZoomOut = new System.Windows.Forms.Button();
            this.SplitContainerTabStrip = new System.Windows.Forms.SplitContainer();
            this.TabStripMain = new SyncroSim.Apex.Forms.TabStrip();
            this.ScrollBarHorizontal = new System.Windows.Forms.HScrollBar();
            this.PanelBottomControls = new System.Windows.Forms.Panel();
            this.PanelTabNavigator = new System.Windows.Forms.Panel();
            this.ButtonSelectStratum = new System.Windows.Forms.Button();
            this.ButtonLast = new System.Windows.Forms.Button();
            this.ButtonFirst = new System.Windows.Forms.Button();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonPrevious = new System.Windows.Forms.Button();
            this.ScrollBarVertical = new System.Windows.Forms.VScrollBar();
            this.PanelControlHost = new System.Windows.Forms.Panel();
            this.PanelZoomControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTabStrip)).BeginInit();
            this.SplitContainerTabStrip.Panel1.SuspendLayout();
            this.SplitContainerTabStrip.Panel2.SuspendLayout();
            this.SplitContainerTabStrip.SuspendLayout();
            this.PanelBottomControls.SuspendLayout();
            this.PanelTabNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelZoomControls
            // 
            this.PanelZoomControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelZoomControls.BackColor = System.Drawing.SystemColors.Control;
            this.PanelZoomControls.Controls.Add(this.ButtonZoomIn);
            this.PanelZoomControls.Controls.Add(this.ButtonZoomOut);
            this.PanelZoomControls.Location = new System.Drawing.Point(704, 0);
            this.PanelZoomControls.Name = "PanelZoomControls";
            this.PanelZoomControls.Size = new System.Drawing.Size(59, 20);
            this.PanelZoomControls.TabIndex = 14;
            // 
            // ButtonZoomIn
            // 
            this.ButtonZoomIn.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonZoomIn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ButtonZoomIn.FlatAppearance.BorderSize = 0;
            this.ButtonZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonZoomIn.Image = global::SyncroSim.STSim.Properties.Resources.Plus16x16;
            this.ButtonZoomIn.Location = new System.Drawing.Point(20, 0);
            this.ButtonZoomIn.Name = "ButtonZoomIn";
            this.ButtonZoomIn.Size = new System.Drawing.Size(19, 20);
            this.ButtonZoomIn.TabIndex = 1;
            this.ButtonZoomIn.UseVisualStyleBackColor = false;
            // 
            // ButtonZoomOut
            // 
            this.ButtonZoomOut.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonZoomOut.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ButtonZoomOut.FlatAppearance.BorderSize = 0;
            this.ButtonZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonZoomOut.Image = global::SyncroSim.STSim.Properties.Resources.Minus16x16;
            this.ButtonZoomOut.Location = new System.Drawing.Point(0, 0);
            this.ButtonZoomOut.Name = "ButtonZoomOut";
            this.ButtonZoomOut.Size = new System.Drawing.Size(19, 20);
            this.ButtonZoomOut.TabIndex = 0;
            this.ButtonZoomOut.UseVisualStyleBackColor = false;
            // 
            // SplitContainerTabStrip
            // 
            this.SplitContainerTabStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainerTabStrip.BackColor = System.Drawing.Color.LightSteelBlue;
            this.SplitContainerTabStrip.Location = new System.Drawing.Point(95, 0);
            this.SplitContainerTabStrip.Margin = new System.Windows.Forms.Padding(2);
            this.SplitContainerTabStrip.Name = "SplitContainerTabStrip";
            // 
            // SplitContainerTabStrip.Panel1
            // 
            this.SplitContainerTabStrip.Panel1.Controls.Add(this.TabStripMain);
            // 
            // SplitContainerTabStrip.Panel2
            // 
            this.SplitContainerTabStrip.Panel2.Controls.Add(this.ScrollBarHorizontal);
            this.SplitContainerTabStrip.Size = new System.Drawing.Size(608, 20);
            this.SplitContainerTabStrip.SplitterDistance = 451;
            this.SplitContainerTabStrip.SplitterWidth = 8;
            this.SplitContainerTabStrip.TabIndex = 5;
            this.SplitContainerTabStrip.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaintSplitContainer);
            // 
            // TabStripMain
            // 
            this.TabStripMain.BackColor = System.Drawing.Color.White;
            this.TabStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabStripMain.Location = new System.Drawing.Point(0, 0);
            this.TabStripMain.Margin = new System.Windows.Forms.Padding(2);
            this.TabStripMain.Name = "TabStripMain";
            this.TabStripMain.Size = new System.Drawing.Size(451, 20);
            this.TabStripMain.TabIndex = 0;
            this.TabStripMain.TabStop = false;
            this.TabStripMain.Text = "TabStripMain";
            // 
            // ScrollBarHorizontal
            // 
            this.ScrollBarHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScrollBarHorizontal.Location = new System.Drawing.Point(0, 0);
            this.ScrollBarHorizontal.Name = "ScrollBarHorizontal";
            this.ScrollBarHorizontal.Size = new System.Drawing.Size(149, 20);
            this.ScrollBarHorizontal.TabIndex = 0;
            this.ScrollBarHorizontal.TabStop = true;
            this.ScrollBarHorizontal.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnHorizontalScroll);
            // 
            // PanelBottomControls
            // 
            this.PanelBottomControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.PanelBottomControls.Controls.Add(this.PanelZoomControls);
            this.PanelBottomControls.Controls.Add(this.PanelTabNavigator);
            this.PanelBottomControls.Controls.Add(this.SplitContainerTabStrip);
            this.PanelBottomControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBottomControls.Location = new System.Drawing.Point(0, 335);
            this.PanelBottomControls.Margin = new System.Windows.Forms.Padding(2);
            this.PanelBottomControls.Name = "PanelBottomControls";
            this.PanelBottomControls.Size = new System.Drawing.Size(764, 20);
            this.PanelBottomControls.TabIndex = 0;
            // 
            // PanelTabNavigator
            // 
            this.PanelTabNavigator.Controls.Add(this.ButtonSelectStratum);
            this.PanelTabNavigator.Controls.Add(this.ButtonLast);
            this.PanelTabNavigator.Controls.Add(this.ButtonFirst);
            this.PanelTabNavigator.Controls.Add(this.ButtonNext);
            this.PanelTabNavigator.Controls.Add(this.ButtonPrevious);
            this.PanelTabNavigator.Location = new System.Drawing.Point(1, 0);
            this.PanelTabNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.PanelTabNavigator.Name = "PanelTabNavigator";
            this.PanelTabNavigator.Size = new System.Drawing.Size(95, 20);
            this.PanelTabNavigator.TabIndex = 2;
            // 
            // ButtonSelectStratum
            // 
            this.ButtonSelectStratum.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonSelectStratum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonSelectStratum.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ButtonSelectStratum.FlatAppearance.BorderSize = 0;
            this.ButtonSelectStratum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSelectStratum.Image = global::SyncroSim.STSim.Properties.Resources.Search16x16;
            this.ButtonSelectStratum.Location = new System.Drawing.Point(38, 0);
            this.ButtonSelectStratum.Name = "ButtonSelectStratum";
            this.ButtonSelectStratum.Size = new System.Drawing.Size(19, 20);
            this.ButtonSelectStratum.TabIndex = 2;
            this.ButtonSelectStratum.UseVisualStyleBackColor = false;
            // 
            // ButtonLast
            // 
            this.ButtonLast.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonLast.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ButtonLast.FlatAppearance.BorderSize = 0;
            this.ButtonLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLast.Image = global::SyncroSim.STSim.Properties.Resources.Last16x16;
            this.ButtonLast.Location = new System.Drawing.Point(75, 0);
            this.ButtonLast.Name = "ButtonLast";
            this.ButtonLast.Size = new System.Drawing.Size(19, 20);
            this.ButtonLast.TabIndex = 4;
            this.ButtonLast.UseVisualStyleBackColor = false;
            // 
            // ButtonFirst
            // 
            this.ButtonFirst.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFirst.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ButtonFirst.FlatAppearance.BorderSize = 0;
            this.ButtonFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirst.Image = global::SyncroSim.STSim.Properties.Resources.First16x16;
            this.ButtonFirst.Location = new System.Drawing.Point(0, 0);
            this.ButtonFirst.Name = "ButtonFirst";
            this.ButtonFirst.Size = new System.Drawing.Size(19, 20);
            this.ButtonFirst.TabIndex = 0;
            this.ButtonFirst.UseVisualStyleBackColor = false;
            // 
            // ButtonNext
            // 
            this.ButtonNext.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonNext.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ButtonNext.FlatAppearance.BorderSize = 0;
            this.ButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonNext.Image = global::SyncroSim.STSim.Properties.Resources.Next16x16;
            this.ButtonNext.Location = new System.Drawing.Point(56, 0);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(19, 20);
            this.ButtonNext.TabIndex = 3;
            this.ButtonNext.UseVisualStyleBackColor = false;
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonPrevious.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ButtonPrevious.FlatAppearance.BorderSize = 0;
            this.ButtonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPrevious.Image = global::SyncroSim.STSim.Properties.Resources.Previous16x16;
            this.ButtonPrevious.Location = new System.Drawing.Point(19, 0);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(19, 20);
            this.ButtonPrevious.TabIndex = 1;
            this.ButtonPrevious.UseVisualStyleBackColor = false;
            // 
            // ScrollBarVertical
            // 
            this.ScrollBarVertical.Dock = System.Windows.Forms.DockStyle.Right;
            this.ScrollBarVertical.Location = new System.Drawing.Point(744, 0);
            this.ScrollBarVertical.Name = "ScrollBarVertical";
            this.ScrollBarVertical.Size = new System.Drawing.Size(20, 335);
            this.ScrollBarVertical.TabIndex = 1;
            this.ScrollBarVertical.TabStop = true;
            this.ScrollBarVertical.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnVerticalScroll);
            // 
            // PanelControlHost
            // 
            this.PanelControlHost.BackColor = System.Drawing.Color.White;
            this.PanelControlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelControlHost.Location = new System.Drawing.Point(0, 0);
            this.PanelControlHost.Margin = new System.Windows.Forms.Padding(2);
            this.PanelControlHost.Name = "PanelControlHost";
            this.PanelControlHost.Size = new System.Drawing.Size(744, 335);
            this.PanelControlHost.TabIndex = 0;
            this.PanelControlHost.TabStop = true;
            // 
            // TransitionDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Controls.Add(this.PanelControlHost);
            this.Controls.Add(this.ScrollBarVertical);
            this.Controls.Add(this.PanelBottomControls);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TransitionDataFeedView";
            this.Size = new System.Drawing.Size(764, 355);
            this.PanelZoomControls.ResumeLayout(false);
            this.SplitContainerTabStrip.Panel1.ResumeLayout(false);
            this.SplitContainerTabStrip.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTabStrip)).EndInit();
            this.SplitContainerTabStrip.ResumeLayout(false);
            this.PanelBottomControls.ResumeLayout(false);
            this.PanelTabNavigator.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Panel PanelBottomControls;
        internal SyncroSim.Apex.Forms.TabStrip TabStripMain;
        internal System.Windows.Forms.Panel PanelTabNavigator;
        internal System.Windows.Forms.SplitContainer SplitContainerTabStrip;
        internal System.Windows.Forms.Button ButtonLast;
        internal System.Windows.Forms.Button ButtonNext;
        internal System.Windows.Forms.Button ButtonPrevious;
        internal System.Windows.Forms.Button ButtonFirst;
        internal System.Windows.Forms.Panel PanelZoomControls;
        internal System.Windows.Forms.Button ButtonZoomIn;
        internal System.Windows.Forms.Button ButtonZoomOut;
        internal System.Windows.Forms.HScrollBar ScrollBarHorizontal;
        internal System.Windows.Forms.VScrollBar ScrollBarVertical;
        internal System.Windows.Forms.Panel PanelControlHost;
        internal System.Windows.Forms.Button ButtonSelectStratum;
    }
}
