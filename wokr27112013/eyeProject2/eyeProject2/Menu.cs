using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace eyeProject2
{
    class Menu : UserControl
    {
        private Process.position menuType = Process.position.NONE;
        private int timerOffset = 0;
        private bool itemSelected = false;
        private static int numberOfProcess = 3;
        private AutoHotkey ahk = new AutoHotkey();
        private ScriptHandler script = null;
        private Process.sel sel = Process.sel.NONE;
        


        public Menu(Process.position x,ScriptHandler s)
        {
            this.menuType = x;
            script = s;
        }

        public void selected(Process.sel x)
        {
            if (sel != x)
            {
                sel = x;
                timerOffset = 0;
                itemSelected = false;
                this.Invalidate();
            }
            else
                timerOffset++;

            if (timerOffset > 20)
            {
                itemSelected = true;
                eyeSelector(sel);
                Process.reset();

                timerOffset = 0;
            }

        }

        private void eyeSelector(Process.sel item)
        {
            if(menuType == Process.position.UP)
            {
                switch (item)
                {
                    case Process.sel.LEFT:
                        {
                            //string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            //ahk.Exec(notepad);
                            //ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            //ahk.Exec("Send Program Name - Up Left");
                            //ahk.Exec("Send {Enter}");
                            ahk.Exec("SetTitleMatchMode 2");
                            string wmp = "IfWinExist Windows Media Player\n {\n WinActivate Windows Media Player\n }\n else\n {\n Run wmplayer\n WinWait Windows Media Player\n }";
                            ahk.Exec(wmp);
                            ahk.Exec("Send {Media_Play_Pause}");
                        }
                        break;

                    case Process.sel.RIGHT:
                        {
                            //string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            //ahk.Exec(notepad);
                            //ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            //ahk.Exec("Send Program Name - Up Right");
                            //ahk.Exec("Send {Enter}");
                            script.Execute("UP_RIGHT");
                        }
                        break;

                    case Process.sel.CENTER:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Up Center");
                            ahk.Exec("Send {Enter}");
                        }
                        break;
                }
            }
            else if (menuType == Process.position.LEFT)
            {
                switch (item)
                {
                    case Process.sel.LEFT:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Left Left");
                            ahk.Exec("Send {Enter}");
                        }
                        break;

                    case Process.sel.RIGHT:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Left Right");
                            ahk.Exec("Send {Enter}");
                        }
                        break;

                    case Process.sel.CENTER:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Left Center");
                            ahk.Exec("Send {Enter}");
                        }
                        break;
                }
            }
            else if (menuType == Process.position.RIGHT)
            {
                switch (item)
                {
                    case Process.sel.LEFT:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Right Left");
                            ahk.Exec("Send {Enter}");
                        }
                        break;

                    case Process.sel.RIGHT:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Right Right");
                            ahk.Exec("Send {Enter}");
                        }
                        break;

                    case Process.sel.CENTER:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Right Center");
                            ahk.Exec("Send {Enter}");
                        }
                        break;
                }
            }
            else if (menuType == Process.position.DOWN)
            {
                switch (item)
                {
                    case Process.sel.LEFT:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Down Left");
                            ahk.Exec("Send {Enter}");
                        }
                        break;

                    case Process.sel.RIGHT:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Down Right");
                            ahk.Exec("Send {Enter}");
                        }
                        break;

                    case Process.sel.CENTER:
                        {
                            string notepad = "IfWinExist Untitled - Notepad\n {\n WinActivate Notepad\n }\n else\n {Run Notepad\n WinWait Untitled - Notepad\n WinActivate Notepad\n }";
                            ahk.Exec(notepad);
                            ahk.Exec("Send Author-Sayantan\n Send {Enter}");
                            ahk.Exec("Send Program Name - Down Center");
                            ahk.Exec("Send {Enter}");
                        }
                        break;
                }
            }
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            Pen p = new Pen(Color.DarkBlue);
            g.FillRectangle(p.Brush, 0, Process.getScreenH()/2 - Process.getRectH()/2, Process.getScreenW(), Process.getRectH());
            g.DrawLine(p, 0, Process.getScreenH() / 2, Process.getScreenH() / 2, Process.getScreenH() / 2);

            p.Color = Color.Red;
            if (sel != Process.sel.NONE)
                g.FillRectangle(p.Brush, Process.getRectW() * (((int)sel) - 1), Process.getScreenH() / 2 - Process.getRectH() / 2, Process.getRectW(), Process.getRectH());

        }

    }
}
