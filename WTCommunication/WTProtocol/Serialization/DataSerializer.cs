// This file is part of FiVES.
//
// FiVES is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// FiVES is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with FiVES.  If not, see <http://www.gnu.org/licenses/>.
using SINFONI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public abstract class DataSerializer
    {
        protected MemoryStream dataView = new MemoryStream();
        protected BinaryWriter writer;
        protected int bitIndex = 0;
        protected int byteIndex = 0;

        public DataSerializer()
        {
            writer = new BinaryWriter(dataView);
        }

        protected void AddVLEValue(UInt32 value)
        {
            if (value < 0x80)
            {
                AddValue((byte)value);
            }
            else if (value < 0x4000)
            {
                AddValue((byte)(value & 0x7f | 0x80));
                AddValue((byte)(value >> 7));
            }
            else
            {
                AddValue((byte)(value & 0x7f | 0x80));
                AddValue((byte)(((value >> 7) & 0x7f) | 0x80));
                AddValue((UInt16)(value >> 14));
            }
        }

        protected void AddValue(bool value)
        {
            byte valueAsByte = value ? (byte)1 : (byte)0;
            AddValue(valueAsByte);
        }

        protected void AddValue(byte value)
        {
            if (bitIndex == 0)
            {
                using (var ms = new MemoryStream())
                {
                    writer.Write(value);
                    byteIndex++;
                }
            }
            else
            {
                AddBits(8, value);
            }
        }

        protected void AddValue(UInt16 value)
        {
            if (bitIndex == 0)
            {
                using (var ms = new MemoryStream())
                {
                    byte[] valueAsByteArray = BitConverter.GetBytes(value);
                    writer.Write(valueAsByteArray);
                    byteIndex += 2;
                }
            }
            else
            {
                AddBits(16, value);
            }
        }

        protected void AddValue(uint value)
        {
            if (bitIndex == 0)
            {
                using (var ms = new MemoryStream())
                {
                    byte[] valueAsByteArray = BitConverter.GetBytes(value);
                    writer.Write(valueAsByteArray);
                    byteIndex += 4;
                }
            }
            else
            {
                AddBits(32, value);
            }
        }

        protected void AddValue(int value)
        {
            byte[] valueAsByteArray = BitConverter.GetBytes(value);

            // We may have to flip byte order as ints are stored little endian
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(valueAsByteArray);
            }

            if (bitIndex == 0)
            {
                using (var ms = new MemoryStream())
                {
                    writer.Write(valueAsByteArray);
                    byteIndex += 4;
                }
            }
            else
            {
                AddArrayBuffer(valueAsByteArray);
            }
        }

        protected void AddValue(float value)
        {
            using (var ms = new MemoryStream())
            {
                byte[] valueAsByteArray = BitConverter.GetBytes(value);
                writer.Write(valueAsByteArray);
            }
        }

        protected void AddValue(string value)
        {
            using (var ms = new MemoryStream())
            {
                byte[] valueAsByteArray = System.Text.Encoding.UTF8.GetBytes(value);
                AddArrayBuffer(valueAsByteArray);
            }
        }

        protected void AddValue(string stringValue, byte stringLength)
        {
            AddValue(stringLength);
            AddValue(stringValue);
        }

        protected void AddArrayBuffer(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                AddValue(buffer[i]);
            }
        }

        protected void AddBits(int bitCount, uint value)
        {
            var shift = 0;
            byte currentByte = dataView.Length > 0 ? dataView.ToArray()[byteIndex] : (byte)0;
            while (bitCount > 0)
            {
                if ((value & (1 << shift)) != 0)
                    currentByte |= (byte)(1 << bitIndex);
                else
                    currentByte &= (byte)(0xff - (1 << bitIndex));

                shift++;
                bitCount--;
                bitIndex++;

                if (bitIndex > 7)
                {
                    bitIndex = 0;
                    dataView.WriteByte((byte)currentByte);
                    byteIndex++;
                    if (bitCount > 0)
                        currentByte = 0;
                }
            }

            if (this.bitIndex != 0)
            {
                this.dataView.WriteByte(currentByte);
                // When byte was not fully written yet, keep writing position at the current
                // byte
                this.dataView.Position = this.dataView.Position - 1;
            }
        }

        protected uint getIntIdFromGuid(Guid guid)
        {
            string guidAsString = guid.ToString();
            return Convert.ToUInt32(guidAsString.Split('-')[0]);
        }
    }
}
