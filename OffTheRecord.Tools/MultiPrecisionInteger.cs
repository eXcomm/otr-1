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
    using System.Linq;
    #endregion

    public static class MultiPrecisionInteger
    {
        /// <summary>
        /// Convert to Multi Precision Integer.
        /// </summary>
        /// <param name="data">Integer as byte array.</param>
        /// <returns>MPI as byte array.</returns>
        public static byte[] ByteArrayToMPI(byte[] data, bool dataMatchEndian = false)
        {
            // Truncate leading 0 bytes from input
            data = data.SkipWhile(b => b == 0).ToArray();

            // Create "Length" prefix - 32 bit big-endian integer
            var lenBytes = BitConverter.GetBytes(data.Length);

            if (Tools.Endian.IsLittleEndian)
            {
                lenBytes = Tools.Endian.SwapArray(lenBytes);
                if (dataMatchEndian)
                {
                    data = Tools.Endian.SwapArray(data);
                }
            }

            // return MPI with Length prefix and concatted data.
            return lenBytes.Concat(data).ToArray();
        }

        /// <summary>
        /// Convert from Multi Precision Integer to byte[].
        /// </summary>
        /// <param name="data">MPI as byte array.</param>
        /// <returns>Integer as byte array.</returns>
        public static byte[] MPIToByteArray(byte[] data)
        {
            return data.Skip(4).ToArray();
        }
    }
}
