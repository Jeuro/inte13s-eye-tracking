using System;
using System.Windows;
using System.Windows.Forms;
using eyeProject2;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace eyeProject2
{
    class Process : Form
    {
        public enum position : int { NONE = -1, UP = 0, LEFT = 1, RIGHT = 2, DOWN = 3 };
        public enum sel : int { NONE = 0, LEFT = 1, CENTER = 2, RIGHT = 3 };

        private position iSee = position.NONE;
        private position iSaw = position.NONE;

        private static bool resetMode = false;
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
        private static Rectangle rectR = new Rectangle(screenW - rectH, screenH / 2 - rectW / 2, rectH, rectW);
        private static Rectangle rectB = new Rectangle(Process.screenW / 2 - Process.rectW / 2, screenH - rectH, Process.rectW, Process.rectH);
        private static Rectangle emptyRect = new Rectangle(1, 1, 1, 1);
        private static Rectangle menuRect = new Rectangle(0, Process.getScreenH() / 2 - Process.getRectH() / 2, Process.getScreenW(), Process.getRectH());

        private int timeOffset = 0;

        public static int numberOfSide = 4;

        private static int timeToSelect = 800;
        private static EyetrackCommunicator comm = new EyetrackCommunicator();
        private Items[,] items = new Items[numberOfSide, eyeProject2.Menu.maxItems];

        private Image[] icons = new Image[eyeProject2.Menu.maxItems * numberOfSide];
        private MirametrixDatum data = comm.GetData();

        private ScriptHandler scrpthndl;

        private Menu[] m = new Menu[4];

        public Process()
        {
            icons[0] = Image.FromFile(@"imgProject\Applications\backw.png");
            icons[1] = Image.FromFile(@"imgProject\Applications\playPause.png");
            icons[2] = Image.FromFile(@"imgProject\Applications\forw.png");
            icons[3] = Image.FromFile(@"imgProject\Web Browsers\Firefox.png");
            icons[4] = Image.FromFile(@"imgProject\Web Browsers\Internet Explorer.png");
            icons[5] = Image.FromFile(@"imgProject\Web Browsers\Google Chrome.png");
            icons[6] = Image.FromFile(@"imgProject\Applications\Notepad.png");
            icons[7] = Image.FromFile(@"imgProject\Applications\Visual Studio 2012.png");
            icons[8] = Image.FromFile(@"imgProject\Applications\Live Mail.png");
            icons[9] = Image.FromFile(@"imgProject\Other\Phone.png");
            icons[10] = Image.FromFile(@"imgProject\Other\Phoner.png");
            icons[11] = Image.FromFile(@"imgProject\Other\Reject.png");

            this.initializeCustomItem();
            scrpthndl = new ScriptHandler("scripts.xml");
            this.Opacity = 0.2f;
            Timer t = new Timer();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.CadetBlue;
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            this.Region = new Region(emptyRect);

            t.Interval = Process.clock;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        private void initializeCustomItem()
        {
            for (int i = 0; i < numberOfSide; i++)
                for (int j = 0; j < eyeProject2.Menu.maxItems; j++)
                    items[i, j] = new Items(icons[i * (eyeProject2.Menu.maxItems) + j]);
        }

        public static void reset()
        {
            resetMode = true;
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
            if (!eyeSelected)
                timeOffset += Process.clock;

            this.iSaw = this.iSee;
            this.whereIsEye();

            if (!eyeSelected)
            {
                if (this.iSee != position.NONE)
                {
                    if (this.iSee != this.iSaw)
                    {
                        this.Visible = true;
                        this.timeOffset = 0;
                        this.Invalidate();
                    }
                    else if (timeOffset >= Process.timeToSelect)
                    {
                        eyeSelected = true;

                        this.Region = new Region(menuRect);

                        if (this.m[(int)iSee] == null)
                            m[(int)iSee] = new Menu(this.iSee, scrpthndl, items[(int)iSee, 0], items[(int)iSee, 1], items[(int)iSee, 2]);

                        this.Visible = true;
                        timeOffset = 0;
                        this.Controls.Add(m[(int)iSee]);
                        m[(int)iSee].Dock = DockStyle.Fill;
                        this.Invalidate();


                    }
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Maximized;

                }
                else if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Minimized;
                    timeOffset = 0;
                    this.Visible = false;
                    this.Invalidate();
                }
            }
            else if (resetMode)
            {
                eyeSelected = false;
                this.WindowState = FormWindowState.Minimized;
                this.Controls.Remove(this.m[(int)this.iSee]);
                this.Visible = false;
                this.Region = new Region(emptyRect);
                resetMode = false;
                this.Invalidate();
            }
        }

        private void whereIsEye()
        {
            if (data.x != null)
                this.miraX = (float)data.x * screenW;
            if (data.y != null)
                this.miraY = (float)data.y * screenH;

            if (eyeSelected)
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

                    timeOffset = 0;
                }
                else
                {
                    timeOffset += Process.clock;
                    m[(int)this.iSee].selected(sel.NONE);

                    if (timeOffset >= timeToSelect)
                    {
                        eyeSelected = false;
                        this.Region = new Region(emptyRect);
                        this.Controls.Remove(this.m[(int)this.iSee]);
                        this.iSee = position.NONE;
                        timeOffset = 0;
                    }
                }
            }
            else if (data != null)
            {

                if ((miraY <= Process.rectH) && (miraX >= (screenW / 2) - (rectW / 2)) && (miraX <= (screenW / 2) + (rectW / 2)))
                {
                    this.iSee = (int)position.UP;
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.rectU);
                }
                else if ((miraY >= (screenH / 2) - (rectW / 2)) && (miraY <= (screenH / 2) + (rectW / 2)) && (miraX <= rectH))
                {
                    this.iSee = position.LEFT;
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.rectL);
                }
                else if ((miraY >= (screenH / 2) - (rectW / 2)) && (miraY <= (screenH / 2) + (rectW / 2)) && (miraX >= (screenW - rectH)))
                {
                    this.iSee = position.RIGHT;
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.rectR);
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
                    if (this.iSee != this.iSaw)
                        this.Region = new Region(Process.emptyRect);
                }
            }
        }
    }
}