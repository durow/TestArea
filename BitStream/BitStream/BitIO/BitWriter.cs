using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.BitIO
{
    public class BitWriter
    {
        public StringBuilder BinString { get; private set; }

        public BitWriter()
        {
            BinString = new StringBuilder();
        }

        public BitWriter(int bitLength)
        {
            var add = 8 - bitLength % 8;
            BinString = new StringBuilder(bitLength + add);
        }

        public void WriteByte(byte b, int len=8)
        {
            var bin = Byte2Bin(b, len);
            BinString.Append(bin);
        }

        public void WriteInt(int i, int len)
        {
            var bin = Int2Bin(i, len);
            BinString.Append(bin);
        }

        public void WriteChar7(char c)
        {
            var bin = Char2Bin(c, 7);
            BinString.Append(bin);
        }

        public byte[] GetBytes()
        {
            Check8();
            var len = BinString.Length / 8;
            var result = new byte[len];

            for (int i = 0; i < len; i++)
            {
                var bits = BinString.ToString(i * 8, 8);
                result[i] = Convert.ToByte(bits, 2);
            }

            return result;
        }

        public string GetBinString()
        {
            Check8();
            return BinString.ToString();
        }

        public static string Byte2Bin(byte b, int len)
        {
            var bin = Convert.ToString(b, 2);
            return CheckStringLength(bin, len);
        }

        public static string Int2Bin(int i, int len)
        {
            var bin = Convert.ToString(i, 2);
            return CheckStringLength(bin, len);
        }

        public static string Char2Bin(char c, int len)
        {
            var b = Convert.ToByte(c);
            return Byte2Bin(b, len);
        }

        private static string CheckStringLength(string str, int len)
        {
            if(str.Length > len)
                throw new Exception("len is too short");

            while(str.Length < len)
            {
                str = "0" + str;
            }
            return str;
        }

        private void Check8()
        {
            var add = BinString.Capacity - BinString.Length;
            for (int i = 0; i < add; i++)
            {
                BinString.Append("0");
            }
        }
    }
}
