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
            this.sevenSegmentArray1 = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.sevenSegmentArray2 = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.sevenSegmentArray3 = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sevenSegmentArray1
            // 
            this.sevenSegmentArray1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sevenSegmentArray1.ArrayCount = 2;
            this.sevenSegmentArray1.ColorBackground = System.Drawing.Color.Black;
            this.sevenSegmentArray1.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sevenSegmentArray1.ColorLight = System.Drawing.Color.Red;
            this.sevenSegmentArray1.DecimalShow = true;
            this.sevenSegmentArray1.ElementPadding = new System.Windows.Forms.Padding(6, 4, 4, 4);
            this.sevenSegmentArray1.ElementWidth = 10;
            this.sevenSegmentArray1.ItalicFactor = -0.1F;
            this.sevenSegmentArray1.Location = new System.Drawing.Point(2, 1);
            this.sevenSegmentArray1.Name = "sevenSegmentArray1";
            this.sevenSegmentArray1.Size = new System.Drawing.Size(57, 39);
            this.sevenSegmentArray1.TabIndex = 9;
            this.sevenSegmentArray1.TabStop = false;
            this.sevenSegmentArray1.Value = "00";
            // 
            // sevenSegmentArray2
            // 
            this.sevenSegmentArray2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sevenSegmentArray2.ArrayCount = 2;
            this.sevenSegmentArray2.ColorBackground = System.Drawing.Color.Black;
            this.sevenSegmentArray2.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sevenSegmentArray2.ColorLight = System.Drawing.Color.Red;
            this.sevenSegmentArray2.DecimalShow = true;
            this.sevenSegmentArray2.ElementPadding = new System.Windows.Forms.Padding(6, 4, 4, 4);
            this.sevenSegmentArray2.ElementWidth = 10;
            this.sevenSegmentArray2.ItalicFactor = -0.1F;
            this.sevenSegmentArray2.Location = new System.Drawing.Point(76, 0);
            this.sevenSegmentArray2.Name = "sevenSegmentArray2";
            this.sevenSegmentArray2.Size = new System.Drawing.Size(57, 39);
            this.sevenSegmentArray2.TabIndex = 10;
            this.sevenSegmentArray2.TabStop = false;
            this.sevenSegmentArray2.Value = "00";
            // 
            // sevenSegmentArray3
            // 
            this.sevenSegmentArray3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sevenSegmentArray3.ArrayCount = 2;
            this.sevenSegmentArray3.ColorBackground = System.Drawing.Color.Black;
            this.sevenSegmentArray3.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sevenSegmentArray3.ColorLight = System.Drawing.Color.Red;
            this.sevenSegmentArray3.DecimalShow = true;
            this.sevenSegmentArray3.ElementPadding = new System.Windows.Forms.Padding(6, 4, 4, 4);
            this.sevenSegmentArray3.ElementWidth = 10;
            this.sevenSegmentArray3.ItalicFactor = -0.1F;
            this.sevenSegmentArray3.Location = new System.Drawing.Point(148, 1);
            this.sevenSegmentArray3.Name = "sevenSegmentArray3";
            this.sevenSegmentArray3.Size = new System.Drawing.Size(57, 39);
            this.sevenSegmentArray3.TabIndex = 11;
            this.sevenSegmentArray3.TabStop = false;
            this.sevenSegmentArray3.Value = "00";
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
            // ClockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sevenSegmentArray3);
            this.Controls.Add(this.sevenSegmentArray2);
            this.Controls.Add(this.sevenSegmentArray1);
            this.Name = "ClockControl";
            this.Size = new System.Drawing.Size(220, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DmitryBrant.CustomControls.SevenSegmentArray sevenSegmentArray1;
        private DmitryBrant.CustomControls.SevenSegmentArray sevenSegmentArray2;
        private DmitryBrant.CustomControls.SevenSegmentArray sevenSegmentArray3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}
