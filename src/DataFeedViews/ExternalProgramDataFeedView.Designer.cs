namespace SyncroSim.STSim
{
    internal partial class ExternalProgramDataFeedView : SyncroSim.Core.Forms.DataFeedView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxExe = new System.Windows.Forms.TextBox();
            this.TextBoxScript = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonChooseExe = new System.Windows.Forms.Button();
            this.ButtonChooseScript = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxBI = new System.Windows.Forms.TextBox();
            this.TextBoxAI = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBoxBT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBoxAT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ButtonClearExe = new System.Windows.Forms.Button();
            this.ButtonClearScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "External program:";
            // 
            // TextBoxExe
            // 
            this.TextBoxExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxExe.Location = new System.Drawing.Point(22, 38);
            this.TextBoxExe.Name = "TextBoxExe";
            this.TextBoxExe.ReadOnly = true;
            this.TextBoxExe.Size = new System.Drawing.Size(452, 20);
            this.TextBoxExe.TabIndex = 1;
            // 
            // TextBoxScript
            // 
            this.TextBoxScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxScript.Location = new System.Drawing.Point(22, 96);
            this.TextBoxScript.Name = "TextBoxScript";
            this.TextBoxScript.ReadOnly = true;
            this.TextBoxScript.Size = new System.Drawing.Size(452, 20);
            this.TextBoxScript.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "External script (optional):";
            // 
            // ButtonChooseExe
            // 
            this.ButtonChooseExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonChooseExe.Location = new System.Drawing.Point(488, 36);
            this.ButtonChooseExe.Name = "ButtonChooseExe";
            this.ButtonChooseExe.Size = new System.Drawing.Size(75, 23);
            this.ButtonChooseExe.TabIndex = 4;
            this.ButtonChooseExe.Text = "Browse...";
            this.ButtonChooseExe.UseVisualStyleBackColor = true;
            this.ButtonChooseExe.Click += new System.EventHandler(this.ButtonChooseExe_Click);
            // 
            // ButtonChooseScript
            // 
            this.ButtonChooseScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonChooseScript.Location = new System.Drawing.Point(488, 94);
            this.ButtonChooseScript.Name = "ButtonChooseScript";
            this.ButtonChooseScript.Size = new System.Drawing.Size(75, 23);
            this.ButtonChooseScript.TabIndex = 4;
            this.ButtonChooseScript.Text = "Browse...";
            this.ButtonChooseScript.UseVisualStyleBackColor = true;
            this.ButtonChooseScript.Click += new System.EventHandler(this.ButtonChooseScript_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Call before iterations:";
            // 
            // TextBoxBI
            // 
            this.TextBoxBI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxBI.Location = new System.Drawing.Point(147, 147);
            this.TextBoxBI.Name = "TextBoxBI";
            this.TextBoxBI.Size = new System.Drawing.Size(327, 20);
            this.TextBoxBI.TabIndex = 6;
            // 
            // TextBoxAI
            // 
            this.TextBoxAI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxAI.Location = new System.Drawing.Point(147, 173);
            this.TextBoxAI.Name = "TextBoxAI";
            this.TextBoxAI.Size = new System.Drawing.Size(327, 20);
            this.TextBoxAI.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Call after iterations:";
            // 
            // TextBoxBT
            // 
            this.TextBoxBT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxBT.Location = new System.Drawing.Point(147, 199);
            this.TextBoxBT.Name = "TextBoxBT";
            this.TextBoxBT.Size = new System.Drawing.Size(327, 20);
            this.TextBoxBT.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Call before timesteps:";
            // 
            // TextBoxAT
            // 
            this.TextBoxAT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxAT.Location = new System.Drawing.Point(147, 225);
            this.TextBoxAT.Name = "TextBoxAT";
            this.TextBoxAT.Size = new System.Drawing.Size(327, 20);
            this.TextBoxAT.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Call after timesteps:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(379, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Iterations and timesteps can be single values, ranges, or a combination of both:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 293);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Examples:  1,2     3-4     5,6,7-10";
            // 
            // ButtonClearExe
            // 
            this.ButtonClearExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClearExe.Location = new System.Drawing.Point(569, 36);
            this.ButtonClearExe.Name = "ButtonClearExe";
            this.ButtonClearExe.Size = new System.Drawing.Size(75, 23);
            this.ButtonClearExe.TabIndex = 15;
            this.ButtonClearExe.Text = "Clear";
            this.ButtonClearExe.UseVisualStyleBackColor = true;
            this.ButtonClearExe.Click += new System.EventHandler(this.ButtonClearExe_Click);
            // 
            // ButtonClearScript
            // 
            this.ButtonClearScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClearScript.Location = new System.Drawing.Point(569, 94);
            this.ButtonClearScript.Name = "ButtonClearScript";
            this.ButtonClearScript.Size = new System.Drawing.Size(75, 23);
            this.ButtonClearScript.TabIndex = 15;
            this.ButtonClearScript.Text = "Clear";
            this.ButtonClearScript.UseVisualStyleBackColor = true;
            this.ButtonClearScript.Click += new System.EventHandler(this.ButtonClearScript_Click);
            // 
            // ExternalProgramDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ButtonClearScript);
            this.Controls.Add(this.ButtonClearExe);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TextBoxAT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TextBoxBT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TextBoxAI);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextBoxBI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ButtonChooseScript);
            this.Controls.Add(this.ButtonChooseExe);
            this.Controls.Add(this.TextBoxScript);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxExe);
            this.Controls.Add(this.label1);
            this.Name = "ExternalProgramDataFeedView";
            this.Size = new System.Drawing.Size(667, 329);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxExe;
        private System.Windows.Forms.TextBox TextBoxScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonChooseExe;
        private System.Windows.Forms.Button ButtonChooseScript;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxBI;
        private System.Windows.Forms.TextBox TextBoxAI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBoxBT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBoxAT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ButtonClearExe;
        private System.Windows.Forms.Button ButtonClearScript;
    }
}
