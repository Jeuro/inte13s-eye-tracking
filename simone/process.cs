using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace eyeProject2
{
    class Process : Form
    {
        private enum position : int { NONE = 0, UP = 1,LEFT=3,RIGHT=2, DOWN=3};
        private int iSee = (int)position.NONE;
        private int iSaw = (int) position.NONE;
        private bool eyeSelected = false;

        private static int screenW = Screen.PrimaryScreen.Bounds.Width;
        private static int screenH = Screen.PrimaryScreen.Bounds.Height;

        private static int rectW = screenW / 3;
        private static int rectH = screenH / 3;

        private static int clock = 50;

        private static Rectangle rectU = new Rectangle(Process.screenW / 2 - Process.rectW / 2, 0, Process.rectW, Process.rectH);
        private static Rectangle rectL = new Rectangle(0, screenH / 2 - rectH / 2, rectH, rectW);
        private static Rectangle rectR = new Rectangle(screenW - rectH, screenH/2 - rectH/2, rectH,rectW);
        private static Rectangle rectB = new Rectangle(Process.screenW / 2 - Process.rectW / 2, screenH - rectH, Process.rectW, Process.rectH);
        private static Rectangle emptyRect = new Rectangle(1,1,1,1);

        private int timeOffset = 0;

        private static int timeToSelect = 15000;
        private static EyetrackCommunicator comm = new EyetrackCommunicator();
        private MirametrixDatum data = comm.GetData();

        public Process()
        {
            Timer t = new Timer();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.CadetBlue;
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            this.Region = new Region(Process.rectL);

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
          //  timeOffset+= Process.clock;
            this.iSaw = this.iSee;
            this.whereIsEye();

            if (this.iSee != (int) position.NONE)
            {
                if (this.iSee != this.iSaw)
                {
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Maximized;
                        this.Visible = true;
                    }
                    this.Invalidate();
                }
                else if (timeOffset >= Process.timeToSelect)
                {
                    this.eyeSelected = true;
                    timeOffset = 0;
                    this.Invalidate();
                }
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Region = new Region(Process.emptyRect);
                this.Visible = false;
                this.Invalidate();
            }
        }


        private void whereIsEye()
        {
           data = comm.GetData();
           float miraX = (float)data.x * 1000;
           float miraY = (float)data.y * 1000;

            if (this.eyeSelected)
            {
                // Is inside the circle?
            }
            else
            {
                // Is left, right, up or bottom?
                if (data != null)
                {
                    if ((miraY <= Process.rectH) && (miraX >= screenW/2 - rectW/2) && (miraX<= screenW/2 + rectW/2))
                    {
                        this.iSee = (int)position.UP;
                        this.Region = new Region(Process.rectU);
                        Console.WriteLine("UP");
                    }
                    else if ((miraY >= (screenH/2) - (rectW/2)) && (miraY <= (screenH / 2) + (rectW / 2)) && (miraX <= rectH))
                    {
                        this.iSee = (int) position.LEFT;
                        this.Region = new Region(Process.rectL);
                        Console.WriteLine("LEFT");
                    }
                    else if ((miraY >= screenH / 2 - rectW / 2) && (miraY <= screenH / 2 + rectW / 2) && (miraX > screenW - rectH))
                    {
                        this.iSee = (int) position.RIGHT;
                        this.Region = new Region(Process.rectR);
                        Console.WriteLine("RIGHT");
                    }
                    else if ((miraY >= screenH - rectH) && (miraX >= screenW / 2 - rectW / 2) && (miraX <= screenW / 2 + rectW / 2))
                    {
                        this.iSee = (int) position.DOWN;
                        this.Region = new Region(Process.rectB);
                        Console.WriteLine("DOWN");
                    }
                    else
                    {
                        this.iSee = 0;
                        this.Region = new Region(Process.emptyRect);
                    }
                }
            }
        }
    }
}
