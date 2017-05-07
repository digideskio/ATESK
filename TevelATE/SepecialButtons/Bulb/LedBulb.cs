﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Bulb {

	/// <summary>
	/// The LEDBulb is a .Net control for Windows Forms that emulates an
	/// LED light with two states On and Off.  The purpose of the control is to 
	/// provide a sleek looking representation of an LED light that is sizable, 
	/// has a transparent background and can be set to different colors.  
	/// </summary>
	public partial class LedBulb : Control {

        bool m_blinking = false;
        AutoResetEvent m_timer = new AutoResetEvent(false);
        Thread m_blinkThread = null;
		#region Public and Private Members

		private Color _color;
		private bool _on = true;
		private Color _reflectionColor = Color.FromArgb(180, 255, 255, 255);
		private Color[] _surroundColor = new Color[] { Color.FromArgb(0, 255, 255, 255) };

		/// <summary>
		/// Gets or Sets the color of the LED light
		/// </summary>
		[DefaultValue(typeof(Color), "153, 255, 54")]
		public Color Color { 
			get { return _color; } 
			set { 
				_color = value;
				this.DarkColor = ControlPaint.Dark(_color);
				this.DarkDarkColor = ControlPaint.DarkDark(_color);
				this.Invalidate();	// Redraw the control
			} 
		}
		
		/// <summary>
		/// Dark shade of the LED color used for gradient
		/// </summary>
		public Color DarkColor { get; protected set; }
		
		/// <summary>
		/// Very dark shade of the LED color used for gradient
		/// </summary>
		public Color DarkDarkColor { get; protected set; }

		/// <summary>
		/// Gets or Sets whether the light is turned on
		/// </summary>
		public bool On { 
			get { return _on; } 
			set { _on = value; this.Invalidate(); } 
		}

		#endregion

		#region Constructor
		
		public LedBulb() {
			SetStyle(ControlStyles.DoubleBuffer
			| ControlStyles.AllPaintingInWmPaint
			| ControlStyles.ResizeRedraw
			| ControlStyles.UserPaint
			| ControlStyles.SupportsTransparentBackColor, true);
			
			this.Color = Color.FromArgb(255, 153, 255, 54);
            						
		}
		
		#endregion

		#region Methods

		/// <summary>
		/// Handles the Paint event for this UserControl
		/// </summary>
		protected override void OnPaint(PaintEventArgs e){
			// Create an offscreen graphics object for double buffering
			Bitmap offScreenBmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
			using (System.Drawing.Graphics g = Graphics.FromImage(offScreenBmp)) {
				g.SmoothingMode = SmoothingMode.HighQuality;
				// Draw the control
				drawControl(g, this.On);
				// Draw the image to the screen
				e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
			}
		}

		/// <summary>
		/// Renders the control to an image
		/// </summary>
		private void drawControl(Graphics g, bool on) {
			// Is the bulb on or off
			Color lightColor = (on)? this.Color : Color.FromArgb(150, this.DarkColor);
			Color darkColor = (on) ? this.DarkColor : this.DarkDarkColor;
			
			// Calculate the dimensions of the bulb
			int width = this.Width - (this.Padding.Left + this.Padding.Right);
			int height = this.Height - (this.Padding.Top + this.Padding.Bottom);
			// Diameter is the lesser of width and height
			int diameter = Math.Min(width, height);
			// Subtract 1 pixel so ellipse doesn't get cut off
			diameter = Math.Max(diameter - 1, 1);

			// Draw the background ellipse
			var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, diameter, diameter);
			g.FillEllipse(new SolidBrush(darkColor), rectangle);

			// Draw the glow gradient
			var path = new GraphicsPath();
			path.AddEllipse(rectangle);
			var pathBrush = new PathGradientBrush(path);
			pathBrush.CenterColor = lightColor;
			pathBrush.SurroundColors = new Color[] { Color.FromArgb(0, lightColor) };
			g.FillEllipse(pathBrush, rectangle);

			// Draw the white reflection gradient
			var offset = Convert.ToInt32(diameter * .15F);
			var diameter1 = Convert.ToInt32(rectangle.Width * .8F);
			var whiteRect = new Rectangle(rectangle.X - offset, rectangle.Y - offset, diameter1, diameter1);
			var path1 = new GraphicsPath();
			path1.AddEllipse(whiteRect);
			var pathBrush1 = new PathGradientBrush(path);
			pathBrush1.CenterColor = _reflectionColor;
			pathBrush1.SurroundColors = _surroundColor;
			g.FillEllipse(pathBrush1, whiteRect);

			// Draw the border
			g.SetClip(this.ClientRectangle);
			if (this.On) g.DrawEllipse(new Pen(Color.FromArgb(85, Color.Black),1F), rectangle);
		}

        void BlinkLedThread(int milliseconds)
        {
            while (m_blinking == true)
            {
                this.On = true;
                m_timer.WaitOne(milliseconds);
                this.On = false;
                m_timer.WaitOne(milliseconds);
            }
        }

		/// <summary>
		/// Causes the Led to start blinking
		/// </summary>
		/// <param name="milliseconds">Number of milliseconds to blink for. 0 stops blinking</param>
        /// 
		public void Blink(int milliseconds) 
        {
			if (milliseconds > 0) {
                if (m_blinkThread == null)
                {
                    m_blinking = true;
                    m_blinkThread = new Thread(() => BlinkLedThread(milliseconds));
                    m_blinkThread.Start();
                }
			}
			else {
                m_blinking = false;
                if (m_blinkThread != null)
                    m_blinkThread.Join();
                m_blinkThread = null;
			}
		}

		#endregion
	}
}
