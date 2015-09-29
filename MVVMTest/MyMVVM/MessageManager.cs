using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public class MessageManager
    {
        private static MessageManager _default;

        public static MessageManager Default
        {
            get
            {
                if (_default == null)
                    _default = new MessageManager();
                return _default;
            }
        }

        private List<MessageInfo> _messageList = new List<MessageInfo>();

        public void Register(string command, Action action)
        {
            _messageList.Add(new MessageInfo
            {
                
            })
        }
    }
}
