
using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace eyeProject2
{
    class Process : Form
    {
        public enum position : int { NONE = -1, UP = 0,LEFT=1,RIGHT=2, DOWN=3};
        public enum sel : int { NONE = 0, LEFT = 1, CENTER = 2, RIGHT = 3 };

        private position iSee = position.NONE;
        private position iSaw =  position.NONE;
        private bool eyeSelected = false;

        private static int screenW = Screen.PrimaryScreen.Bounds.Width;
        private static int screenH = Screen.PrimaryScreen.Bounds.Height;

        private static int rectW = screenW / 3;
        private static int rectH = screenH / 3;

        private static int clock = 100;
        private float miraX = 0;
        private float miraY = 0;

        private static Rectangle rectU = new Rectangle(Process.screenW / 2 - Process.rectW / 2, 0, Process.rectW, Process.rectH);
        private static Rectangle rectL = new Rectangle(0, screenH / 2 - rectW / 2, rectH, rectW);
        private static Rectangle rectR = new Rectangle(screenW - rectH, screenH/2 - rectW/2, rectH,rectW);
        private static Rectangle rectB = new Rectangle(Process.screenW / 2 - Process.rectW / 2, screenH - rectH, Process.rectW, Process.rectH);
        private static Rectangle emptyRect = new Rectangle(1,1,1,1);
        private static Rectangle menuRect = new Rectangle(0, Process.getScreenH() / 2 - Process.getRectH() / 2, Process.getScreenW(), Process.getRectH());

        private int timeOffset = 0;
        private int offsetOut = 0;

        private static int timeToSelect = 2500;
        private static EyetrackCommunicator comm = new EyetrackCommunicator();
        private MirametrixDatum data = comm.GetData();

        private Menu[] m = new Menu[4];

        private int numItem = 4;
        public Process()
        {
            Timer t = new Timer();

            for (int i = 0; i < numItem; i++)
                m[i] = null;

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.CadetBlue;
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            this.Region = new Region(Process.emptyRect);

            t.Interval = Process.clock;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        public static int getScreenW()
        {
            if (screenH != 0)
                return screenW;
            else 
                return 0;
        }

        public static int getScreenH()
        {
            return screenH;
        }

        public static int getRectW()
        {
            return rectW;
        }

        public static int getRectH()
        {
            return rectH;
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
            data = comm.GetData();
            if (!this.eyeSelected)
                timeOffset+= Process.clock;
            
            this.iSaw = this.iSee;
            this.whereIsEye();

            if (!this.eyeSelected)
            {
                if (this.iSee !=  position.NONE)
                {
                    if (this.iSee != this.iSaw)
                    {
                        this.Visible = true;
                        this.timeOffset = 0;
                        this.Invalidate();
                    }
                    else if (timeOffset >= Process.timeToSelect)
                    {
                        Console.WriteLine("occhio selezionato");
                        this.eyeSelected = true;
                        this.Region = new Region(menuRect);

                        if (this.m[(int)iSee] == null)
                            m[(int)iSee] = new Menu(this.iSee);

                        this.Visible = true;
                        timeOffset = 0;
                        this.Invalidate();
                        this.Controls.Add(m[(int)iSee]);
                    }
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Maximized;

                }
                else if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Minimized;
                    timeOffset = 0;
                    this.Region = new Region(Process.emptyRect);
                    this.Visible = false;
                    this.Invalidate();
                }
            }
        }


        private void whereIsEye()
        {
            if (data.x != null)
                this.miraX = (float)data.x * 1000;
            if (data.y != null)
                this.miraY = (float)data.y * 1000;
               
            if (this.eyeSelected)
            {
                // Is inside the circle?
                if ((miraY >= screenH / 2 - rectH / 2) && (miraY <= screenH / 2 + rectH / 2))
                {
                    if (miraX <= rectW)
                        m[(int)this.iSee].selected(sel.LEFT);
                    else if (miraX <= screenW / 2 + rectW / 2)
                        m[(int)this.iSee].selected(sel.CENTER);
                    else
                        m[(int)this.iSee].selected(sel.RIGHT);

                    offsetOut = 0;
                }
                else
                {
                    offsetOut += Process.clock;
                    m[(int)this.iSee].selected(sel.NONE);

                    if ( offsetOut >= timeToSelect)
                    {
                        eyeSelected = false;
                        this.Region = new Region(emptyRect);
                        this.Controls.Remove(this.m[(int)this.iSee]);
                        this.iSee = position.NONE;
                        offsetOut = 0;
                    }
                }
            }
            else if (data != null)
            {

                if ((miraY <= Process.rectH) && (miraX >= (screenW/2) - (rectW/2)) && (miraX<= (screenW/2) + (rectW/2)))
                {
                    this.iSee = (int)position.UP;
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.rectU);
                }
                else if ((miraY >= (screenH/2) - (rectW/2)) && (miraY <= (screenH / 2) + (rectW / 2)) && (miraX <= rectH))
                {
                    this.iSee =  position.LEFT;
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.rectL);
                }
                else if ((miraY >= (screenH / 2) - (rectW / 2)) && (miraY <= (screenH / 2) + (rectW / 2)) && (miraX >= (screenW - rectH)))
                {
                    this.iSee = position.RIGHT;
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.rectR);
                        Console.WriteLine("RIGHT");
                }
                else if ((miraY >= screenH - rectH) && (miraX >= (screenW / 2) - (rectW / 2)) && (miraX <= (screenW / 2) + (rectW / 2)))
                {

                    this.iSee = position.DOWN;
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.rectB);
                }
                else
                {
                    this.iSee = position.NONE;
                    this.Region = new Region(Process.emptyRect);
                }
            }
        }
    }
}
