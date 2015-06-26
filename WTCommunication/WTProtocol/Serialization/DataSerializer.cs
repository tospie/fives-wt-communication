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
using KIARA;
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
            using (var ms = new MemoryStream())
            {
                writer.Write(value);
            }
        }

        protected void AddValue(UInt16 value)
        {
            using (var ms = new MemoryStream())
            {
                byte[] valueAsByteArray = BitConverter.GetBytes(value);
                writer.Write(valueAsByteArray);
            }
        }

        protected void AddValue(uint value)
        {
            using (var ms = new MemoryStream())
            {
                byte[] valueAsByteArray = BitConverter.GetBytes(value);
                writer.Write(valueAsByteArray);
            }
        }

        protected void AddValue(int value)
        {
            using (var ms = new MemoryStream())
            {
                byte[] valueAsByteArray = BitConverter.GetBytes(value);

                // We may have to flip byte order as ints are stored little endian
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(valueAsByteArray);
                }
                writer.Write(valueAsByteArray);
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
                writer.Write(valueAsByteArray);
            }
        }

        protected void AddValue(string stringValue, byte stringLength)
        {
            AddValue(stringLength);
            AddValue(stringValue);
        }

        protected uint getIntIdFromGuid(Guid guid)
        {
            string guidAsString = guid.ToString();
            return Convert.ToUInt32(guidAsString.Split('-')[0]);
        }
    }
}
