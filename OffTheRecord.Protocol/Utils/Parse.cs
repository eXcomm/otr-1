// <copyright>
// Off The Record Messaging .NET, Copyright (c) 2013
//  based upon the original Off-the-Record Messaging library by
//    Ian Goldberg, Rob Smits, Chris Alexander,
//    Willy Lew, Lisa Du, Nikita Borisov
//    otr@cypherpunks.ca, http://www.cypherpunks.ca/otr/
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of version 2.1 of the GNU Lesser General
// Public License as published by the Free Software Foundation.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
// </copyright>
// <author>Bjorn Kuiper</author>
// <email>otr@kuiper.nu</email>

using OffTheRecord.Resources;

namespace OffTheRecord.Protocol.Utils
{
    #region Namespaces
    using System;
    using System.Linq;
    #endregion

    /// <summary>
    /// Parse class.
    /// </summary>
    public static class Parse
    {
        #region Public static methods
        /// <summary>
        /// Decodes Base64 Off-the-Record message into it's raw byte array.
        /// </summary>
        /// <param name="msg">The base64 Off-the-Record message.</param>
        /// <param name="start">The start position of the actual message (so, excluding the header of the message).</param>
        /// <returns>The raw data in a byte array.</returns>
        public static byte[] DecodeFromBase64(string msg, out int start)
        {
            start = -1;

            int header = msg.IndexOf(OtrStrings.OtrHeader, StringComparison.Ordinal);

            if (header == -1)
            {
                return null;
            }

            header += 5;
            start = header;

            int footer = msg.IndexOf('.', header);

            if (footer == -1)
            {
                footer = header + msg.Substring(header).Length;
            }

            int rawlen = footer - header;

            string input = msg.Substring(header, rawlen);

            byte[] result = Convert.FromBase64String(input);

            return result;
        }

        /// <summary>
        /// Read an uint from the raw data stream.
        /// </summary>
        /// <param name="rawData">The raw data stream.</param>
        /// <param name="startIndex">The offset.</param>
        /// <returns>the uint value.</returns>
        public static uint ReadInt32(byte[] rawData, ref int startIndex)
        {
            uint result = (uint)(rawData[startIndex] << 24 | rawData[startIndex + 1] << 16 | rawData[startIndex + 2] << 8) | rawData[startIndex + 3];
            startIndex += 4;
            return result;
        }

        /// <summary>
        /// Read an ulong from the raw data stream.
        /// </summary>
        /// <param name="rawData">The raw data stream.</param>
        /// <param name="startIndex">The offset.</param>
        /// <returns>the ulong value.</returns>
        public static ulong ReadInt64(byte[] rawData, ref int startIndex)
        {
            ulong result = (ulong)(rawData[startIndex] << 56 | rawData[startIndex + 1] << 48 | rawData[startIndex + 2] << 40 | rawData[startIndex + 3] << 32 |
                                   rawData[startIndex + 4] << 24 | rawData[startIndex + 5] << 16 | rawData[startIndex + 6] << 8) | rawData[startIndex + 7];
            startIndex += 8;

            return result;
        }

        /// <summary>
        /// Read a string from the raw data stream.
        /// </summary>
        /// <param name="rawData">The raw data stream.</param>
        /// <param name="startIndex">The offset.</param>
        /// <param name="length">The length of the string to read.</param>
        /// <returns>the string.</returns>
        public static string ReadRaw(byte[] rawData, ref int startIndex, int length)
        {
            byte[] temp = rawData.Skip(startIndex).Take(length).ToArray();
            startIndex += length;
            return Tools.General.ByteArrayToString(temp);
        }

        /// <summary>
        /// Read a multi-precision integer from the raw data stream.
        /// </summary>
        /// <param name="rawData">The raw data stream.</param>
        /// <param name="startIndex">The offset.</param>
        /// <returns>the multi-precision integer as a string.</returns>
        public static string ReadMpi(byte[] rawData, ref int startIndex)
        {
            uint length = ReadInt32(rawData, ref startIndex);
            byte[] temp = rawData.Skip(startIndex).Take((int)length).ToArray();

            return Tools.General.ByteArrayToString(temp);
        }
        #endregion
    }
}
