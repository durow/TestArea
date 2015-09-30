using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyMVVM;

namespace MessageTest.Message
{
    public class MainRegister:MessageRegisterBase
    {
        public override void Register()
        {
            MsgManager.Register<string>(RegInstance, "ShowBox", new Action<string>(s => MessageBox.Show(s)));
        }
    }
}
