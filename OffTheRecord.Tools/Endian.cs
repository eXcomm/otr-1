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
    using System;

    public static class Endian
    {
        #region Properties
        public static bool ArchitectureIsLittleEndian
        {
            get
            {
                return BitConverter.IsLittleEndian;
            }
        }
        #endregion

        public static byte[] ConvertToBigEndianBytes(byte[] data)
        {
            if (ArchitectureIsLittleEndian)
            {
                int length = data.Length;

                int leftOver = data.Length & 0x3;

                var result = new byte[length];

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
            
            return data;
        }
    }
}