using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MyMVVM;

namespace MVVMTest.Message
{
    public class MainWindowMsg:MessageRegisterBase
    {
        public override void Register()
        {
            RegisterMsg("ShowBox", 
                new Action<string>(s => MessageBox.Show(s)));
            
        }
    }
}
