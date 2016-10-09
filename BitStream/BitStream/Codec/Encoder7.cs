using BitStream.BitIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.Codec
{
    public class Encoder7 : IEncoder
    {
        private static Encoder7 _default;
        public static Encoder7 Default
        {
            get
            {
                if (_default == null)
                    _default = new Encoder7();
                return _default;
            }
        }

        public byte[] Encode(string text)
        {
            var len = text.Length * 7 + 24;

            var writer = new BitWriter(len);
            writer.WriteByte(2);
            writer.WriteInt(text.Length, 16);

            for (int i = 0; i < text.Length; i++)
            {
                var b = Convert.ToByte(text[i]);
                writer.WriteByte(b, 7);
            }

            return writer.GetBytes();
        }
    }
}
