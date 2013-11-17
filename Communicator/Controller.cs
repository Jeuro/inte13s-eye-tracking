using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eyeProject101 {

    public class Controller {
        private static EyetrackCommunicator comm = new EyetrackCommunicator();
        public static void Main() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            while (true) {
                MirametrixDatum data = comm.GetData();
                if (data != null) {
                    Console.WriteLine(data.x + ", " + data.y);
                }
            }
        }
    }
}
