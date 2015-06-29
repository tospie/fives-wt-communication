﻿// This file is part of FiVES.
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
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public abstract class DataDeserializer
    {
        protected byte[] currentInputStream;
        protected int byteIndex = 0;
        protected int bitIndex = 0;

        public DataDeserializer(byte[] inputStream)
        {
            currentInputStream = inputStream;
        }

        /// <summary>
        /// Truncates the input byte stream to the remaining bytes that were not yet processed. Used to pass
        /// remaining bytes of a message to a deserializer subclass
        /// </summary>
        /// <returns>Remaining bytes that were not yet handled by the current deserializer</returns>
        protected byte[] GetRemainingBytes()
        {
            int blockLength = currentInputStream.Length - byteIndex;
            byte[] blockBytes = new byte[blockLength];
            Array.Copy(currentInputStream, byteIndex, blockBytes, 0, blockLength);
            return blockBytes;
        }

        /// <summary>
        /// Reads the next byte and returns it as bool value. False vor 0, True for any value larger 0
        /// </summary>
        /// <returns>Value as Bool</returns>
        protected bool ReadBool()
        {
            return ReadByte() > 0;
        }

        /// <summary>
        /// Reads the next byte from the input stream and returns the value as unsigned 8 bit integer.
        /// </summary>
        /// <returns>Value as 8 bit unsigned integer</returns>
        protected byte ReadByte()
        {
            byte value = currentInputStream[byteIndex];
            byteIndex++;
            return value;
        }

        /// <summary>
        /// Reads the next two bytes from the input stream and returns the value as 16 bit unsigned integer
        /// </summary>
        /// <returns>Value as 16 bit unsigned integer</returns>
        protected UInt16 ReadUInt16()
        {
            UInt16 result = BitConverter.ToUInt16(currentInputStream, byteIndex);
            Int16 test = BitConverter.ToInt16(currentInputStream, byteIndex);
            byteIndex += 2;
            return result;
        }

        /// <summary>
        /// Reads the next four bytes from the input stream and returns the value as 32 bit signed integer
        /// </summary>
        /// <returns>Value as 32 bit signed integer</returns>
        protected int ReadInt32()
        {
            byte[] intBytes = new byte[4];
            Array.Copy(currentInputStream, byteIndex, intBytes, 0, 4);

            // Int is stored in little endian binary representation in the stream, so we have have to flip byte order
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(intBytes);
            }
            int result = BitConverter.ToInt32(intBytes, 0);
            byteIndex += 4;

            return result;
        }

        /// <summary>
        /// Reads the next four bytes from the input stream and returns them as 32 bit signed single precision
        /// floating point value
        /// </summary>
        /// <returns>Bytes as 32 bit single precision float</returns>
        protected float ReadFloat()
        {
            float result = BitConverter.ToSingle(currentInputStream, byteIndex);
            byteIndex += 4;
            return result;
        }

        /// <summary>
        /// Reads a string from the input stream. For that, it first reads a single byte that encodes the
        /// total string length. After that, it reads the next n bytes from the input stream and returns them
        /// as UTF8 encoded string.
        /// </summary>
        /// <returns>n bytes as UTF8 encoded string</returns>
        protected string ReadString()
        {
            byte byteLength = ReadByte();
            return ReadString(byteLength);
        }

        /// <summary>
        /// Reads the next <paramref name="length"/> bytes from the input stream and returns them as UTF8
        /// encoded string
        /// </summary>
        /// <param name="length">Number of bytes that should be read from the input stream</param>
        /// <returns>Next <paramref name="length"/> bytes as UTF8 encoded string</returns>
        protected string ReadString(UInt16 length)
        {
            byte[] byteValue = new byte[length];
            Array.Copy(currentInputStream, byteIndex, byteValue, 0, length);
            string result = System.Text.Encoding.UTF8.GetString(byteValue);
            byteIndex += length;
            return result;
        }

        /// <summary>
        /// Reads a variable length encoded integer value. Depending on how many bytes the value needs for
        /// encoding, it reads the next 1, 2 or 4 bytes from the stream and returns them as unsigned integer
        /// </summary>
        /// <returns>Value as 8, 16 or 32 bit integer</returns>
        protected uint ReadVLE()
        {
            uint low = ReadByte();
            if ((low & 0x80) == 0)
                return low;

            low = low & 0x7f;
            uint med = ReadByte();
            if ((med & 0x80) == 0)
                return low | (med << 7);

            med = med & 0x7f;
            uint high = ReadUInt16();
            return low | (med << 7) | (high << 14);
        }

        /// <summary>
        /// Reads a number of bits and returns them as an integer with as many bytes expressiveness as bits
        /// were read
        /// </summary>
        /// <param name="bitCount">Number of bits that should be read from the stream</param>
        /// <returns>Integer value described by read bits</returns>
        protected int ReadBits(int bitCount)
        {
            int value = 0;
            byte bitsRead = 0;
            byte currentByte = currentInputStream[byteIndex];

            while (bitCount > 0)
            {
                if((currentByte & ( 1 << this.bitIndex)) != 0)
                    value |= (1 << bitsRead);

                bitsRead++;
                bitCount--;
                increaseBitIndex(bitCount, ref currentByte);
            }

            return value;
        }

        /// <summary>
        /// Increases the bit-counter of the deserializer by one and shifts to next byte if full byte was read
        /// </summary>
        /// <param name="bitCount">Remaining bits to be read in current bit reading operation</param>
        /// <param name="currentByte">Current byte of input stream</param>
        protected void increaseBitIndex(int bitCount, ref byte currentByte)
        {
            this.bitIndex++;
            if(this.bitIndex > 7)
            {
                this.bitIndex = 0;
                this.byteIndex++;
                if (bitCount > 0)
                {
                    currentByte = currentInputStream[byteIndex];
                }
            }
        }
    }
}
