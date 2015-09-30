using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyMVVM;

namespace MessageTest.Message
{
    public class MainRegister:IMessageRegister
    {
        public MessageManager MsgManager { get; set; }
        public object RegInstance { get; set; }
        public void Register()
        {
            if (RegInstance == null) return;
            if (MsgManager == null) return;
            MsgManager.Register<string>(RegInstance, "ShowBox", new Action<string>(s => MessageBox.Show(s)));
        }
    }
}
