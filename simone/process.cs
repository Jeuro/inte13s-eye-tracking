using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace eyeProject2
{
    class Process : Form
    {
        private bool left = false;
        private bool right = false;
        private bool bottom = false;
        private bool eyeCapted = false;
        private bool eyeSelected = false;
        private bool up = false;

        private static int screenW = Screen.PrimaryScreen.Bounds.Width;
        private static int screenH = Screen.PrimaryScreen.Bounds.Height;

        private static int rectW = screenW / 5;
        private static int rectH = screenH / 5;

        private static int clock = 50;

        private static Rectangle rectU = new Rectangle(Process.screenW / 2 - Process.rectW / 2, 0, Process.rectW, Process.rectH);
        private static Rectangle rectL = new Rectangle(0, screenH / 2 - rectH / 2, rectH, rectW);
        private static Rectangle rectR = new Rectangle(screenW - rectW, screenH/2 - rectH/2, rectH,rectW);
        private static Rectangle rectB = new Rectangle(Process.screenW / 2 - Process.rectW / 2, screenH - rectH, Process.rectW, Process.rectH);
        private static Rectangle emptyRect = new Rectangle(1,1,1,1);

        private int timeOffset = 0;

        private static int timeToSelect = 15000;

        public Process()
        {
            Timer t = new Timer();
            GraphicsPath gp = new GraphicsPath();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.DimGray;
            this.Visible = true;
            this.WindowState = FormWindowState.Maximized;
            this.Region = new Region(Process.emptyRect);
            t.Interval = Process.clock;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
           base.OnPaint(e);
        }

        static void Main()
        {
            Process p = new Process();
            Application.Run(p);
        }

        private void t_Tick(object sender, EventArgs e)
        {
            timeOffset+= Process.clock;
            this.whereIsEye();
            if (this.eyeCapted)
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Maximized;
                else if (timeOffset >= Process.timeToSelect)
                    this.eyeSelected = true;

                this.Invalidate();
            }
            else
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.Region = new Region(Process.emptyRect);
                    this.Invalidate();
                }
            }
        }


        private void whereIsEye()
        {
            // Get x,y from device and enable one of bool variable or nothing   
            // if the eye is in good position set eyeCapted and the relative bool value
            // up, bottom, left or right and set the new region for the form.
            if (this.eyeSelected)
            {
                
                // Is inside the circle?
            }
            else
            {
                // Is left, right, up or bottom?
            }
        }
    }
}
