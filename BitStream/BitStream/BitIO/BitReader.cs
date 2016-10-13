using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitStream.BitIO
{
    public class BitReader
    {
        public readonly StringBuilder BinString;
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
                return BinString.Length * 8 - Position;
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

        public byte ReadByte(int offset)
        {
            var bin = BinString.ToString(offset, 8);
            return Convert.ToByte(bin, 2);
        }

        public byte ReadByte()
        {
            var result = ReadByte(Position);
            Position += 8;
            return result;
        }

        public int ReadInt(int offset, int bitLength)
        {
            var bin = BinString.ToString(offset, bitLength);
            return Convert.ToInt32(bin, 2);
        }

        public int ReadInt(int bitLength)
        {
            var result = ReadInt(Position, bitLength);
            Position += bitLength;
            return result;
        }

        public Int16 ReadInt16(int offset, int bitLength)
        {
            var bin = BinString.ToString(offset, bitLength);
            return Convert.ToInt16(bin, 2);
        }

        public Int16 ReadInt16(int bitLength)
        {
            var result = ReadInt16(Position, bitLength);
            Position += bitLength;
            return result;
        }

        public bool ReadBool(int offset)
        {
            var result = ReadInt(offset, 1);
            return offset == 1;
        }

        public bool ReadBool()
        {
            var result = ReadBool(Position);
            Position++;
            return result;
        }

        public string ReadBinString(int offset, int bitLength)
        {
            return BinString.ToString(offset, bitLength);
        }

        public string ReadBinString(int bitLength)
        {
            var result = ReadBinString(Position, bitLength);
            Position += bitLength;
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
