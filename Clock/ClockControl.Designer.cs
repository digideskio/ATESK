namespace Clock
{
    partial class ClockControl
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ssSec = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.ssMin = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.ssHour = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(60, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(12, 37);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = ":";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Red;
            this.textBox2.Location = new System.Drawing.Point(133, -1);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(12, 37);
            this.textBox2.TabIndex = 13;
            this.textBox2.Text = ":";
            // 
            // ssSec
            // 
            this.ssSec.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ssSec.ArrayCount = 2;
            this.ssSec.ColorBackground = System.Drawing.Color.Black;
            this.ssSec.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ssSec.ColorLight = System.Drawing.Color.Red;
            this.ssSec.DecimalShow = true;
            this.ssSec.ElementPadding = new System.Windows.Forms.Padding(6, 4, 4, 4);
            this.ssSec.ElementWidth = 10;
            this.ssSec.ItalicFactor = -0.1F;
            this.ssSec.Location = new System.Drawing.Point(148, 1);
            this.ssSec.Name = "ssSec";
            this.ssSec.Size = new System.Drawing.Size(57, 39);
            this.ssSec.TabIndex = 11;
            this.ssSec.TabStop = false;
            this.ssSec.Value = "00";
            // 
            // ssMin
            // 
            this.ssMin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ssMin.ArrayCount = 2;
            this.ssMin.ColorBackground = System.Drawing.Color.Black;
            this.ssMin.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ssMin.ColorLight = System.Drawing.Color.Red;
            this.ssMin.DecimalShow = true;
            this.ssMin.ElementPadding = new System.Windows.Forms.Padding(6, 4, 4, 4);
            this.ssMin.ElementWidth = 10;
            this.ssMin.ItalicFactor = -0.1F;
            this.ssMin.Location = new System.Drawing.Point(76, 0);
            this.ssMin.Name = "ssMin";
            this.ssMin.Size = new System.Drawing.Size(57, 39);
            this.ssMin.TabIndex = 10;
            this.ssMin.TabStop = false;
            this.ssMin.Value = "00";
            // 
            // ssHour
            // 
            this.ssHour.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ssHour.ArrayCount = 2;
            this.ssHour.ColorBackground = System.Drawing.Color.Black;
            this.ssHour.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ssHour.ColorLight = System.Drawing.Color.Red;
            this.ssHour.DecimalShow = true;
            this.ssHour.ElementPadding = new System.Windows.Forms.Padding(6, 4, 4, 4);
            this.ssHour.ElementWidth = 10;
            this.ssHour.ItalicFactor = -0.1F;
            this.ssHour.Location = new System.Drawing.Point(2, 1);
            this.ssHour.Name = "ssHour";
            this.ssHour.Size = new System.Drawing.Size(57, 39);
            this.ssHour.TabIndex = 9;
            this.ssHour.TabStop = false;
            this.ssHour.Value = "00";
            // 
            // ClockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ssSec);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ssMin);
            this.Controls.Add(this.ssHour);
            this.Name = "ClockControl";
            this.Size = new System.Drawing.Size(220, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DmitryBrant.CustomControls.SevenSegmentArray ssHour;
        private DmitryBrant.CustomControls.SevenSegmentArray ssMin;
        private DmitryBrant.CustomControls.SevenSegmentArray ssSec;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}
