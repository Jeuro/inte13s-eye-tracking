using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace eyeProject2 {
    class Menu : UserControl {
        private Process.position menuType = Process.position.NONE;
        private int timerOffset = 0;
        private bool itemSelected = false;
        public static int maxItems = 3;
        private AutoHotkey ahk = new AutoHotkey();
        private ScriptHandler script;
        private Process.sel sel = Process.sel.NONE;
        private Items[] items = new Items[maxItems];

        public Menu(Process.position x, ScriptHandler s, Items i1, Items i2, Items i3) {
            this.menuType = x;
            this.items[0] = i1;
            this.items[1] = i2;
            this.items[2] = i3;
            script = s;
        }

        public void selected(Process.sel x) {
            if (sel != x) {
                sel = x;
                timerOffset = 0;
                itemSelected = false;
                this.Invalidate();
            } else
                timerOffset++;

            if (timerOffset > 20) {
                itemSelected = true;
                eyeSelector(sel);
                Process.reset();

                timerOffset = 0;
            }

        }

        private void eyeSelector(Process.sel item) {
            if (menuType == Process.position.UP) {
                switch (item) {
                    case Process.sel.LEFT:
                        script.Execute("UP_LEFT");
                        break;

                    case Process.sel.RIGHT:
                        script.Execute("UP_RIGHT");
                        break;

                    case Process.sel.CENTER:
                        script.Execute("UP_CENTER");
                        break;
                }
            } else if (menuType == Process.position.LEFT) {
                switch (item) {
                    case Process.sel.LEFT:
                        script.Execute("LEFT_LEFT");
                        break;

                    case Process.sel.RIGHT:
                        script.Execute("LEFT_RIGHT");
                        break;

                    case Process.sel.CENTER:
                        script.Execute("LEFT_CENTER");
                        break;
                }
            } else if (menuType == Process.position.RIGHT) {
                switch (item) {
                    case Process.sel.LEFT:
                        script.Execute("RIGHT_LEFT");
                        break;

                    case Process.sel.RIGHT:
                        script.Execute("RIGHT_RIGHT");
                        break;

                    case Process.sel.CENTER:
                        script.Execute("RIGHT_CENTER");
                        break;
                }
            } else if (menuType == Process.position.DOWN) {
                switch (item) {
                    case Process.sel.LEFT:
                        script.Execute("DOWN_LEFT");
                        break;

                    case Process.sel.RIGHT:
                        script.Execute("DOWN_RIGHT");
                        break;

                    case Process.sel.CENTER:
                        script.Execute("DOWN_CENTER");
                        break;
                }
            }

        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            Pen p = new Pen(Color.WhiteSmoke);
            if (sel != Process.sel.NONE)
                g.FillRectangle(p.Brush, Process.getRectW() * (((int)sel) - 1), Process.getScreenH() / 2 - Process.getRectH() / 2, Process.getRectW(), Process.getRectH());

            for (int i = 0; i < maxItems; i++)
                g.DrawImage(items[i].getLogo(),
                    (i * Process.getRectW()) + Process.getRectW() / 2 - ((items[i].getLogo().Width - 280) / 2),
                    ((Process.getScreenH() / 2) - (items[i].getLogo().Height - 280) / 2),
                    items[i].getLogo().Width - 280, items[i].getLogo().Height - 280);
        }

    }
}
