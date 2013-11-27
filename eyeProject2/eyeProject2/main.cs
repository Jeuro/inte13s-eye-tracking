using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace eyeProject2
{
    class main
    {
        static void Main()
        {
            Form f = new Form();
            f.Controls.Add(new Menu(1));
            f.Visible = true;
            f.WindowState = FormWindowState.Maximized;
        }
    }
}
