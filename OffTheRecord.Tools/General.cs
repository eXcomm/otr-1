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

namespace OffTheRecord.Tools
{
    #region Namespaces
    using System;
    using System.IO;
    using System.Linq;
    using OffTheRecord.Resources;
    #endregion

    /// <summary>
    /// General class.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// String to Byte array.
        /// </summary>
        /// <param name="hex">string to convert.</param>
        /// <returns>byte array.</returns>
        public static byte[] StringToByteArray(string hex)
        {
            // XXX: remove leading '00'.
            if (hex.StartsWith("00"))
            {
                hex = hex.Substring(2);
            }

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Byte array to String.
        /// </summary>
        /// <param name="array">array to convert.</param>
        /// <returns>a String.</returns>
        public static string ByteArrayToString(byte[] array)
        {
            return BitConverter.ToString(array).Replace("-", string.Empty);
        }

        /// <summary>
        /// Read from the given stream until we see a complete OTR Key Exchange
        /// or OTR Data message.  Return a newly-allocated pointer to a copy of
        /// this message, which the caller should free().  Returns NULL if no
        /// such message could be found.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        /// <returns>A Off-the-Record message or null if not found.</returns>
        public static string ReadOtr(Stream stream)
        {
            int seen = 0;
            string result = OtrStrings.OtrHeader;

            while (seen < OtrStrings.OtrHeader.Length)
            {
                int c = stream.ReadByte();

                if (c == -1)
                {
                    return null;
                }
                else if (c == OtrStrings.OtrHeader[seen])
                {
                    seen++;
                }
                else if (c == OtrStrings.OtrHeader[0])
                {
                    seen = 1;
                }
                else
                {
                    seen = 0;
                }
            }

            while (true)
            {
                int c = stream.ReadByte();
                if (c == -1)
                {
                    return result;
                }
                else
                {
                    result += char.ConvertFromUtf32(c);
                }

                if (result[result.Length - 1] == '.')
                {
                    return result;
                }
            }
        }
    }
}
