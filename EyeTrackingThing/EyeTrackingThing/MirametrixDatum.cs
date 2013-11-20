using System.IO;
using System;
using System.Xml;
using System.Globalization;

namespace EyeTrackingThing {

    public class MirametrixDatum {
        public float? x { get; set; }
        public float? y { get; set; }

        public MirametrixDatum(string packet) {
            // Mirametrix data format: <REC FPOGX="0.000" FPOGY="0.000" FPOGS="39713.961" FPOGD="0.115" FPOGID="12443" FPOGV="0" />
            if (packet == null) {
                x = null;
                y = null;
            } else {
                using (XmlReader reader = XmlReader.Create(new StringReader(packet))) {
                    reader.ReadToFollowing("REC");
                    x = float.Parse(reader.GetAttribute("FPOGX"), CultureInfo.InvariantCulture);
                    y = float.Parse(reader.GetAttribute("FPOGY"), CultureInfo.InvariantCulture);
                }
            }
        }
    }
}