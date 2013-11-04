using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows;


// Author: Simone Salvo
// Made in Italy :-)

namespace eyeProject101
{

    class slideMenu : UserControl
    {

        private static int screenW = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
        private static int screenH = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
        private static int slideWidth = screenW / 7;
        private static int slideHeigh = screenH / 2;
        private static bool mouseOnLeftSide = false;

        protected override void OnMouseMove(MouseEventArgs e)
        {
 	     //   base.OnMouseMove(e);
            
            // Mouse is on left side?
            if (!mouseOnLeftSide)
            {
                if ((e.X <= slideWidth) &&
                   (e.Y <= ((screenH / 2) + slideHeigh / 2)) &&
                   (e.Y >= ((screenH / 2) - slideHeigh / 2)))
                {
                    mouseOnLeftSide = true;
                    this.Invalidate();
                }
            }
            else if ((e.X > slideWidth) ||
                   (e.Y > ((screenH / 2) + slideHeigh / 2)) ||
                   (e.Y < ((screenH / 2) - slideHeigh / 2)))
            {
                mouseOnLeftSide = false;
                this.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
             //base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
             //base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
             //base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
 	        base.OnPaint(e);
            Graphics g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Pen p = new Pen(Color.Gray);
            p.Width = 10;

            if (mouseOnLeftSide)
                g.FillRectangle(p.Brush, 0, (screenH / 2) - (slideHeigh / 2), slideWidth, slideHeigh);
        }
    }
}
