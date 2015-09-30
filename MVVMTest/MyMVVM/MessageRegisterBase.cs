using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MyMVVM
{
    public abstract class MessageRegisterBase:IMessageRegister
    {
        private object _regInstance;

        public object RegInstance
        {
            get
            {
                if (_regInstance == null)
                    _regInstance = Application.Current.MainWindow;
                return _regInstance;
            }
            set { _regInstance = value; }
        }

        private IMessageManager _msgManager;

        public IMessageManager MsgManager
        {
            get
            {
                if (_msgManager == null)
                    _msgManager = MessageManager.Default;
                return _msgManager;
            }
            set { _msgManager = value; }
        }

        public abstract void Register();
    }
}
