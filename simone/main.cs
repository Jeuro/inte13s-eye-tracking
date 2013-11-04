using System;
using System.Windows.Forms;

// Author: Simone Salvo
// Made in Italy :-)

namespace eyeProject101
{ 
    public class main
    {
        public static void Main()
        {
            Form f = new Form();
            f.Visible = true;
            f.WindowState = FormWindowState.Maximized;
            f.Opacity = 0.2f;
            f.Text = " Viva l'Italia";

            slideMenu sm = new slideMenu();
            f.Controls.Add(sm);

            sm.Dock = DockStyle.Fill;
            Application.Run(f);
        }
    }
}

