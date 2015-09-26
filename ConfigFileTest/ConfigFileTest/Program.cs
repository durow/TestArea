using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigFile.LoadOrCreateFile("config.test");
            config["string"] = "这是一条文本记录";
            config["intX"] = 45.ToString();
            config.AddOrSetConfigValue("intY", 88);
            config.AddOrSetConfigValue("bool", true);
            config["Date"] = DateTime.Now.ToString();

            var x = config.GetConfigValueInt("intX");
            int y;
            config.TryParseConfigValue("intY", out y);
            DateTime dt;
            config.TryParseConfigValue("Date", out dt);
            bool b = config.GetConfigValueBool("bool");
            var c = x + y;

            Console.WriteLine(config["string"]);
            Console.WriteLine(c);
            Console.WriteLine(b);
            Console.WriteLine(dt);

            Console.ReadKey();
        }
    }
}
