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

using System;
using System.Linq;
using System.Security.Cryptography;

namespace OffTheRecord.Protocol.DiffieHellman
{
    public static class ExtensionMethods
    {
        public static string SessionId(this Dh1536 dh)
        {
            if (dh.SharedSecret == null)
            {
                throw new ArgumentNullException("dh");
            }

            byte[] sharedSecretAsMpi = Tools.MultiPrecisionInteger.ByteArrayToMpi(dh.SharedSecret.ToByteArray(), true);
            byte[] hash = SHA1.Create().ComputeHash(new byte[] { 0x00 }.Concat(sharedSecretAsMpi).ToArray());

            return Tools.General.ByteArrayToString(hash);
        }

        public static Keys Keys(this Dh1536 dh)
        {
            if (dh.PublicKey == null || dh.TheirPublicKey == null || dh.SharedSecret == null)
            {
                throw new ArgumentNullException("dh");
            }

            bool isHigh = dh.PublicKey > dh.TheirPublicKey;

            var oneByte = new byte[] { 0x01 };
            var twoByte = new byte[] { 0x02 };

            byte[] sharedSecretAsMpi = Tools.MultiPrecisionInteger.ByteArrayToMpi(dh.SharedSecret.ToByteArray(), true);

            /* next - sendenc & rcvenv AES key */
            byte[] hash = SHA1.Create().ComputeHash((isHigh ? oneByte : twoByte).Concat(sharedSecretAsMpi).ToArray());

            var keys = new Keys
            {
                IsHigh = isHigh, 
                SendAes = Tools.General.ByteArrayToString(hash).Substring(0, 32)
            };

            keys.SendMac = keys.SendAes.MacKey();

            hash = SHA1.Create().ComputeHash((!isHigh ? oneByte : twoByte).Concat(sharedSecretAsMpi).ToArray());

            keys.ReceiveAes = Tools.General.ByteArrayToString(hash).Substring(0, 32);
            keys.ReceiveMac = keys.ReceiveAes.MacKey();

            return keys;
        }

        public static string MacKey(this string aeskey)
        {
            if (!string.IsNullOrEmpty(aeskey) && aeskey.Length == 32)
            {
                return Tools.General.ByteArrayToString(SHA1.Create().ComputeHash(Tools.General.StringToByteArray(aeskey)));
            }
            
            return string.Empty;
        }
    }
}
