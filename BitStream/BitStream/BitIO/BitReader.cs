using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.BitIO
{
    public class BitReader
    {
        public StringBuilder BinString { get; private set; }
        public int Position { get; set; }
        public int Length
        {
            get
            {
                return BinString.Length;
            }
        }
        public int Remain
        {
            get
            {
                return BinString.Length - Position;
            }
        }

        public BitReader(byte[] data)
        {
            BinString = new StringBuilder(data.Length * 8);
            for (int i = 0; i < data.Length; i++)
            {
                BinString.Append(ByteToBinString(data[i]));
            }
            Position = 0;
        }

        public byte ReadByte(int index)
        {
            var bin = BinString.ToString(index, 8);
            return Convert.ToByte(bin, 2);
        }

        public byte ReadByte()
        {
            var result = ReadByte(Position);
            Position += 8;
            return result;
        }

        public int ReadInt(int index, int length)
        {
            var bin = BinString.ToString(index, length);
            return Convert.ToInt32(bin, 2);
        }

        public int ReadInt(int length)
        {
            var result = ReadInt(Position, length);
            Position += length;
            return result;
        }

        public string ReadBinString(int index, int length)
        {
            return BinString.ToString(index, length);
        }

        public string ReadBinString(int length)
        {
            var result = ReadBinString(Position, length);
            Position += length;
            return result;
        }

        public static char[] ByteToBinString(byte b)
        {
            var result = new char[8];

            for (int i = 0; i < 8; i++)
            {
                var temp = b & 128;
                result[i] = temp == 0 ? '0' : '1';
                b = (byte)(b << 1);
            }

            return result;
        }
    }
}
