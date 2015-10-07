using MyMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageTest.Message
{
    class ComputeMsgArgs:MsgArgs
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public int Result { get; set; }

        public ComputeMsgArgs(int a, int b)
        {
            A = a;
            B = b;
        }
    }
}
