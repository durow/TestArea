using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public class MessageInfo
    {
        public object Sender { get; internal set; }
        public string Command { get; internal set; }
        public string Group { get;  internal set; }
        private Action _action;

        public MessageInfo(string command,string group,Action action)
        {
            Command = command;
            Group = group;
            _action = action;
        }

        public void Invoke()
        {
            if (_action != null)
                _action();
        }
    }
}
