using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public class MsgArgs
    {
        public object Sender { get; private set; }
        public Action<object> Callback { get; private set; }

        public MsgArgs(object sender = null, Action<object> callback = null)
        {
            Sender = sender;
            Callback = callback;
        }
    }
}
