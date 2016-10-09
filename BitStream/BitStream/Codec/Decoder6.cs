using BitStream.BitIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.Codec
{
    public class Decoder6:IDecoder
    {
        private static Decoder6 _default;
        public static Decoder6 Default
        {
            get
            {
                if (_default == null)
                    _default = new Decoder6();
                return _default;
            }
        }

        public string Decode(byte[] data)
        {
            var reader = new BitReader(data);
            while(reader.Remain > 8)
            {
                var start = reader.ReadByte();
                if (start == 2)
                    break;
            }
            var len = reader.ReadInt(16);
            var result = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                var index = reader.ReadInt(6);
                var ch = Dict.Custom[index];
                result.Append(ch);
            }

            return result.ToString();
        }
    }
}
