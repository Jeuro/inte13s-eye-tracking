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
        private Dictionary<string, List<string>> scripts;
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
                Console.WriteLine(e);
            }
        }

        public void ReadScripts()
        {
            scripts = doc.Root.Elements().ToDictionary(x => (string) x.Attribute("menupos"),
                                       x => new List<string>(
                           x.Value.Split(new string[] { "\n" }, 
                           StringSplitOptions.RemoveEmptyEntries)));
        }

        public void Execute(string item)
        {
            foreach (string command in scripts[item])
            {
                ahk.Exec(command);
            }
        }
    }

}
