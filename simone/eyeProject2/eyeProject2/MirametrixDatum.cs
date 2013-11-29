using System.Xml;
using System.Globalization;
using System.IO;
using System;

namespace eyeProject2
{
    public class MirametrixDatum
    {
        public float? x { get; set; }
        public float? y { get; set; }

        public MirametrixDatum(string packet)
        {
            // Mirametrix data format: <REC FPOGX="0.000" FPOGY="0.000" FPOGS="39713.961" FPOGD="0.115" FPOGID="12443" FPOGV="0" />
            if (packet == null)
            {
                x = 0;
                y = 0;
            }
            else
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(packet)))
                {
                    try
                    {
                        reader.ReadToFollowing("REC");
                        x = float.Parse(reader.GetAttribute("FPOGX"), CultureInfo.InvariantCulture);
                        y = float.Parse(reader.GetAttribute("FPOGY"), CultureInfo.InvariantCulture);
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}