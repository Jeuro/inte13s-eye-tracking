using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace TestGui
{
    public partial class MiddleMenu : Form
    {

        private static int screenW = Screen.PrimaryScreen.Bounds.Width;
        private static int screenH = Screen.PrimaryScreen.Bounds.Height;

        private static int rectW = screenW / 5;
        private static int rectH = screenH / 5;

        private static Rectangle rectU = new Rectangle(MiddleMenu.screenW / 2 - MiddleMenu.rectW / 2, 0, MiddleMenu.rectW, MiddleMenu.rectH);
        private static Rectangle rectL = new Rectangle(0, screenH / 2 - rectH / 2, rectH, rectW);
        private static Rectangle rectR = new Rectangle(screenW - rectW, screenH / 2 - rectH / 2, rectH, rectW);
        private static Rectangle rectB = new Rectangle(MiddleMenu.screenW / 2 - MiddleMenu.rectW / 2, screenH - rectH, MiddleMenu.rectW, MiddleMenu.rectH);
        private static Rectangle emptyRect = new Rectangle(1, 1, 1, 1);
        //private static Rectangle rectM = new Rectangle((MiddleMenu.screenW - MiddleMenu.rectW) / 3, (MiddleMenu.screenH - MiddleMenu.rectH) / 3, MiddleMenu.rectW * 2, MiddleMenu.rectH * 2);
        private static Rectangle rectM = new Rectangle(0, (MiddleMenu.screenH - MiddleMenu.rectH) / 3, screenW, MiddleMenu.rectH * 2);

        private string ProgramCategory = ""; 

        private AutoHotkey ahk = new AutoHotkey();

        public MiddleMenu()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.DimGray;
            this.Visible = true;
            this.WindowState = FormWindowState.Maximized;
            this.Region = new Region(MiddleMenu.rectM);
            //ahk.Exec("Run Notepad.exe");

            //try
            //{
            //    timer.Enabled = true;
            //    timer.Interval = 1 * 1000;
            //}
            //catch
            //{
            //    timer.Enabled = false;
            //}
            
        }
        static void Main()
        {
            MiddleMenu m = new MiddleMenu();
            Application.Run(m);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ProgramCategory = "PROGRAMMING";
            //ahk.Exec("Run Notepad");
            //ahk.Exec("Run Notepad\n Run Notepad");
            //ahk.Exec("SetTitleMatchMode 2");
            //ahk.Exec("IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }");
            //ahk.Exec("WinActivate Notepad");
            //ahk.Exec("Send Sayantan");
            //ahk.Exec("Send {Enter}");
            //ahk.Exec("WinActivate Notepad");
            //ahk.Exec("Send Sayantan");
            //ahk.Exec("Send {Tab}");
            //ahk.Exec("Run Winword.exe");
            //ahk.Exec("IfWinExist Document1 - Winword {WinActivate}");
            //ahk.Exec("WinActivate");
            //ahk.Exec("else");
            //ahk.Exec("Run Winword.exe");
            ahk.Exec("SetTitleMatchMode 2");
            if (ProgramCategory == "PROGRAMMING")
            {
                string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                ahk.Exec(notepad);
                ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                ahk.Exec("Send Program Name - Untitled Program");
                ahk.Exec("Send {Enter}");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            
            List<Image> icons = AttachPrograms(ProgramCategory);

            int k = 0;
            foreach (var icon in icons)
            {
                e.Graphics.DrawImage(icon, new Rectangle(k * MiddleMenu.rectM.Width / 3 + MiddleMenu.rectM.Width / (3 * 2) - 100, (MiddleMenu.rectM.Top + MiddleMenu.rectM.Height / 2 - 100), 200, 200));
                k++;
            }
            
        }

        private List<Image> AttachPrograms(string MenuType)
        {
            var icons = new List<Image>();


            if (MenuType == "MULTIMEDIA")
            {
                icons.Add(Image.FromFile(@"C:\Users\tobii\Documents\Visual Studio 2013\Projects\TestGui\TestGui\Images\Ncrow-Mega-Pack-2-Windows-Media-Player-10.ico"));

                icons.Add(Image.FromFile(@"C:\Users\tobii\Documents\Visual Studio 2013\Projects\TestGui\TestGui\Images\VLC-media-player-icon.png"));

                icons.Add(Image.FromFile(@"C:\Users\tobii\documents\visual studio 2013\Projects\TestGui\TestGui\Images\MetroUI-Apps-Windows-MovieMaker-icon.png"));
            }
            else if (MenuType == "DOCUMENTS")
            {
                icons.Add(Image.FromFile(@"C:\Users\tobii\documents\visual studio 2013\Projects\TestGui\TestGui\Images\Word_15.png"));

                icons.Add(Image.FromFile(@"C:\Users\tobii\documents\visual studio 2013\Projects\TestGui\TestGui\Images\Excel_15.png"));

                icons.Add(Image.FromFile(@"C:\Users\tobii\documents\visual studio 2013\Projects\TestGui\TestGui\Images\PowerPoint_15.png"));
            }
            else if (MenuType == "PROGRAMMING")
            {
                icons.Add(Image.FromFile(@"C:\Users\tobii\documents\visual studio 2013\Projects\TestGui\TestGui\Images\notepad++.png"));

                icons.Add(Image.FromFile(@"C:\Users\tobii\documents\visual studio 2013\Projects\TestGui\TestGui\Images\Visual_Studio_2012.png"));

                icons.Add(Image.FromFile(@"C:\Users\tobii\documents\visual studio 2013\Projects\TestGui\TestGui\Images\PowerPoint_15.png"));
            }

            return icons;
        }

        
    }
}
