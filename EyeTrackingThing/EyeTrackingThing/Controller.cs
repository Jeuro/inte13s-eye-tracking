using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EyeTrackingThing {

    public class Controller {
        private static EyetrackCommunicator comm = new EyetrackCommunicator();
        public static void Main() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            AutoHotkey ahk = new AutoHotkey();
            ahk.Exec("Run notepad.exe");
            while (true) {
                MirametrixDatum data = comm.GetData();
                if (data != null) {
                    Console.WriteLine(data.x + ", " + data.y);
                }
            }
        }
    }
}
