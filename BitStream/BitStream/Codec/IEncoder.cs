using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.Codec
{
    public interface IEncoder
    {
        byte[] Encode(string text);
    }
}
