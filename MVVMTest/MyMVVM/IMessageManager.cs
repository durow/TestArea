using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public interface IMessageManager
    {
        void Register(object regInstance, string msgName, Action action, string group = "");
        void Register<T>(object regInstance, string msgName, Action<T> action, string group = "");
        void SendMsg(string msgName, Type targetType = null, string group = "");
        void SendMsg<T>(string msgName, T msgArgs, Type targetType = null, string group = "");
        void UnRegister(object regInstance);
        void Clear();
        void WindowClose(object sender, EventArgs e);
    }
}
