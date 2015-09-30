using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public class MsgActionInfo
    {
        public object RegInstance { get; internal set; }
        public string MsgName { get; internal set; }
        public string Group { get;  internal set; }
        public Action Action { get; internal set; }

        public void Execute()
        {
            if (Action != null)
                Action();
        }
    }
}
