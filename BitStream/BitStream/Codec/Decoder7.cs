using BitStream.BitIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.Codec
{
    public class Decoder7 : IDecoder
    {
        private static Decoder7 _default;
        public static Decoder7 Default
        {
            get
            {
                if (_default == null)
                    _default = new Decoder7();
                return _default;
            }
        }
        public string Decode(byte[] data)
        {
            var reader = new BitReader(data);
            while (reader.Remain > 8)
            {
                var start = reader.ReadByte();
                if (start == 2)
                    break;
            }
            var len = reader.ReadInt(16);
            var result = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                var b = reader.ReadInt(7);
                var ch = Convert.ToChar(b);
                result.Append(ch);
            }

            return result.ToString();
        }
    }
}
