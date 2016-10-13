using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.BitIO
{
    public class BitWriter
    {
        public readonly StringBuilder BinString;

        public BitWriter()
        {
            BinString = new StringBuilder();
        }

        public BitWriter(int bitLength)
        {
            var add = 8 - bitLength % 8;
            BinString = new StringBuilder(bitLength + add);
        }

        public void WriteByte(byte b, int bitLength=8)
        {
            var bin = Convert.ToString(b, 2);
            AppendBinString(bin, bitLength);
        }

        public void WriteInt(int i, int bitLength)
        {
            var bin = Convert.ToString(i, 2);
            AppendBinString(bin, bitLength);
        }

        public void WriteChar7(char c)
        {
            var b = Convert.ToByte(c);
            var bin = Convert.ToString(b, 2);
            AppendBinString(bin, 7);
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


        private void AppendBinString(string bin, int bitLength)
        {
            if (bin.Length > bitLength)
                throw new Exception("len is too short");
            var add = bitLength - bin.Length;
            for (int i = 0; i < add; i++)
            {
                BinString.Append('0');
            }
            BinString.Append(bin);
        }

        private void Check8()
        {
            var add = 8 - BinString.Length % 8;
            for (int i = 0; i < add; i++)
            {
                BinString.Append("0");
            }
        }
    }
}
