using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyMVVM;

namespace MessageTest
{
    class MainWindowViewModel:ViewModelBase
    {

        private MyCommand _msgTestCommand;

        public MyCommand MsgTestCommand
        {
            get
            {
                if (_msgTestCommand == null)
                    _msgTestCommand = new MyCommand(
                        new Action<object>(o =>
                        {
                            MsgManager.SendMsg("ShowBox", "测试测试");
                        }));
                return _msgTestCommand;
            }
        }
    }
}
