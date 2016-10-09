using BitStream.BitIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.Codec
{
    public class Encoder6:IEncoder
    {
        private static Encoder6 _default;
        public static Encoder6 Default
        {
            get
            {
                if (_default == null)
                    _default = new Encoder6();
                return _default;
            }
        }

        public byte[] Encode(string text)
        {
            text = text.ToUpper();
            var len = text.Length * 6 + 24;

            var writer = new BitWriter(len);
            writer.WriteByte(2);
            writer.WriteInt(text.Length, 16);

            for (int i = 0; i < text.Length; i++)
            {
                var index = GetChar6Index(text[i]);
                writer.WriteInt(index, 6);
            }

            return writer.GetBytes();

        }

        private int GetChar6Index(char c)
        {
            for (int i = 0; i < 64; i++)
            {
                if (Dict.Custom[i] == c)
                    return i;
            }
            return 10; //return *
        }
    }
}
