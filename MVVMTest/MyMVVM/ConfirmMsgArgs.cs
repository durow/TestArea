using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public class ConfirmMsgArgs:MsgArgs
    {
        public bool Result { get;  set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        public ConfirmMsgArgs(string title, string content,object sender=null)
            :base(sender)
        {
            Title = title;
            Content = content;
        }
    }
}
