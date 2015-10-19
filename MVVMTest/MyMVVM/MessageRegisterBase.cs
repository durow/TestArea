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

        public MessageRegisterBase(object regInstance=null, IMessageManager msgManager=null)
        {
            if (regInstance != null) RegInstance = regInstance;
            if (msgManager != null) MsgManager = msgManager;
        }

        public abstract void Register();
        protected void RegisterMsg(string msgName, Action action, string group="")
        {
            MsgManager.Register(RegInstance, msgName, action, group);
        }

        protected void RegisterMsg<T>(string msgName, Action<T> action, string group="")
        {
            MsgManager.Register(RegInstance,msgName, action, group);
        }
    }
}
