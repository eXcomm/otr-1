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
    using System.Numerics;
    #endregion

    /// <summary>
    /// Endian class.
    /// </summary>
    public static class Endian
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the architecture is Little Endian.
        /// </summary>
        public static bool IsLittleEndian
        {
            get
            {
                return BitConverter.IsLittleEndian;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Swap endian for a short.
        /// </summary>
        /// <param name="inValue">input value.</param>
        /// <returns>swapped value.</returns>
        public static ushort SwapUInt16(ushort inValue)
        {
            // http://www.lastrayofhope.com/2010/01/03/csharp-endian-swap/
            return (ushort)(((inValue & 0xff00) >> 8) | ((inValue & 0x00ff) << 8));
        }

        /// <summary>
        /// Swap endian for a uint.
        /// </summary>
        /// <param name="inValue">input value.</param>
        /// <returns>swapped value.</returns>
        public static uint SwapUInt32(uint inValue)
        {
            // http://www.lastrayofhope.com/2010/01/03/csharp-endian-swap/
            return (uint)
                     (((inValue & 0xff000000) >> 24) |
                     ((inValue & 0x00ff0000) >> 8) |
                     ((inValue & 0x0000ff00) << 8) |
                     ((inValue & 0x000000ff) << 24));
        }

        /// <summary>
        /// Swap endian for an byte array.
        /// </summary>
        /// <param name="data">input data.</param>
        /// <returns>swapped data.</returns>
        public static byte[] SwapArray(byte[] data)
        {
            if (Tools.Endian.IsLittleEndian)
            {
                int length = data.Length;

                int leftOver = data.Length & 0x3;

                byte[] result = new byte[length];

                for (int i = length - 1, j = 0; i >= 3; i -= 4, j += 4)
                {
                    result[j] = data[i];
                    result[j + 1] = data[i - 1];
                    result[j + 2] = data[i - 2];
                    result[j + 3] = data[i - 3];
                }

                if (leftOver == 1)
                {
                    result[length - 1] = data[0];
                }
                else if (leftOver == 2)
                {
                    result[length - 1] = data[0];
                    result[length - 2] = data[1];
                }
                else if (leftOver == 3)
                {
                    result[length - 1] = data[0];
                    result[length - 2] = data[1];
                    result[length - 3] = data[2];
                }

                return result;
            }
            else
            {
                return data;
            }
        }

        /// <summary>
        /// Converts a byte[], created by libgcrypt, to a BigInteger, making sure the endian is matched.
        /// </summary>
        /// <param name="data">input byte array.</param>
        /// <returns>a <see cref="BigInteger"/>.</returns>
        public static BigInteger ToBigInteger(byte[] data)
        {
            return new BigInteger(SwapArray(data));
        }
        #endregion
    }
}