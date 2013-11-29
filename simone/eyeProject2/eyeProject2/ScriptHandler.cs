using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eyeProject2
{
    class ScriptHandler
    {
        private Dictionary<string, string> scripts;
        private XDocument doc;
        private AutoHotkey ahk;

        public ScriptHandler(string file)
        {
            try
            {
                ahk = new AutoHotkey();
                doc = XDocument.Load(file);
                ReadScripts();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Script file not found. " + e);
            }
        }

        public void ReadScripts()
        {
            scripts = doc.Root.Elements().ToDictionary(x => (string)x.Attribute("menupos"), x => x.Value);
        }

        public void Execute(string key)
        {
            ahk.Exec(scripts[key]);
        }
    }
}