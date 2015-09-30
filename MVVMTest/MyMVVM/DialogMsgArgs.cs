using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public class DialogMsgArgs:MsgArgs
    {
        public object Data { get; set; }
        public object Result { get; set; }

        public DialogMsgArgs(object sender=null)
            :base(sender)
        { }
    }
}
