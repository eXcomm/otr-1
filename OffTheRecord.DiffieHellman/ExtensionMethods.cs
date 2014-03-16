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

namespace OffTheRecord.Protocol.DiffieHellman
{
    #region Namespaces
    using System.Linq;
    using System.Security.Cryptography;
    #endregion

    public static class ExtensionMethods
    {
        #region Extension methods
        /// <summary>
        /// Generates session ID based on Shared Secret.
        /// </summary>
        /// <param name="dh">the <see cref="DH1536"/> object.</param>
        /// <returns>Session ID.</returns>
        public static string SessionId(this DH1536 dh)
        {
            byte[] sharedSecretAsMPI = Tools.MPI.ByteArrayToMPI(dh.SharedSecret.ToByteArray(), true);
            byte[] hash = SHA1.Create().ComputeHash(new byte[] { 0x00 }.Concat(sharedSecretAsMPI).ToArray());

            return Tools.General.ByteArrayToString(hash);
        }

        /// <summary>
        /// Gets the MAC and AES keys based on the shared secret.
        /// </summary>
        /// <param name="dh">the <see cref="DH1536"/> object.</param>
        /// <returns>a <see cref="Keys"/> object with the keys.</returns>
        public static Keys Keys(this DH1536 dh)
        {
            bool isHigh = dh.PublicKey > dh.TheirPublicKey;

            byte[] oneByte = new byte[] { 0x01 };
            byte[] twoByte = new byte[] { 0x02 };

            byte[] sharedSecretAsMPI = Tools.MPI.ByteArrayToMPI(dh.SharedSecret.ToByteArray(), true);

            /* next - sendenc & rcvenv AES key */
            byte[] hash = SHA1.Create().ComputeHash((isHigh ? oneByte : twoByte).Concat(sharedSecretAsMPI).ToArray());

            Keys keys = new Keys();
            keys.IsHigh = isHigh;

            keys.SendAes = Tools.General.ByteArrayToString(hash).Substring(0, 32);
            keys.SendMac = keys.SendAes.MacKey();

            hash = SHA1.Create().ComputeHash((!isHigh ? oneByte : twoByte).Concat(sharedSecretAsMPI).ToArray());

            keys.ReceiveAes = Tools.General.ByteArrayToString(hash).Substring(0, 32);
            keys.ReceiveMac = keys.ReceiveAes.MacKey();

            return keys;
        }

        /// <summary>
        /// Generates MAC key from AES key.
        /// </summary>
        /// <param name="aeskey">The AES key.</param>
        /// <returns>The MAC key.</returns>
        public static string MacKey(this string aeskey)
        {
            if (!string.IsNullOrEmpty(aeskey) && aeskey.Length == 32)
            {
                return Tools.General.ByteArrayToString(SHA1.Create().ComputeHash(Tools.General.StringToByteArray(aeskey)));
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }

    /// <summary>
    /// MAC and AES Keys.
    /// </summary>
    public class Keys
    {
        #region Properties
        public string SendAes { get; set; }
        public string SendMac { get; set; }
        public string ReceiveAes { get; set; }
        public string ReceiveMac { get; set; }
        public bool IsHigh { get; set; }
        #endregion
    }
}
