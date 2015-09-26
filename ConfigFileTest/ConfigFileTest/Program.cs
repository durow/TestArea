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
            //创建或载入配置文件
            var config = ConfigFile.LoadOrCreateFile("config.test");

            //使用带参数的属性快速保存配置信息
            config["string"] = "这是一条文本记录";
            //使用带参数的属性快速保存非字符串
            config["intX"] = 45.ToString();
            config["Date"] = DateTime.Now.ToString();
            //保存配置信息的一般方式
            config.AddOrSetConfigValue("intY", 88);
            config.AddOrSetConfigValue("bool", true);

            //读取特定类型的数据
            var x = config.GetConfigValueInt("intX");
            //使用TryParse类方法读取特定类型的数据
            int y;
            config.TryParseConfigValue("intY", out y);
            DateTime dt;
            config.TryParseConfigValue("Date", out dt);
            //使用GetConfigValueXXX方法读取特定类型的数据
            bool b = config.GetConfigValueBool("bool");
            var c = x + y;

            //字符串类型数据直接读取
            Console.WriteLine(config["string"]);
            Console.WriteLine(c);
            Console.WriteLine(b);
            Console.WriteLine(dt);

            Console.ReadKey();
        }
    }
}
