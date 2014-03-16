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

namespace OffTheRecord.Model
{
    #region Namespaces
    using System;
    using System.Security.Cryptography;
    #endregion

    /// <summary>
    /// BasePrivateKey class.
    /// </summary>
    public abstract class BasePrivateKey
    {
        #region Fields
        #endregion

        #region Public Properties
        public string AccountName { get; set; }

        public string Protocol { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Convert a 20-byte hash value to a 45 chars human-readable value.
        /// </summary>
        /// <param name="hash">SHA1 hash of private key.</param>
        /// <returns>A human readable fingerprint (45 chars).</returns>
        public static string otrl_privkey_hash_to_human(byte[] hash)
        {
            if (hash.Length != 20)
            {
                throw new ArgumentException("hash should be 20 bytes.");
            }

            string fingerprint = BitConverter.ToString(hash).Replace("-", string.Empty);

            if (fingerprint.Length != 40)
            {
                throw new Exception("Fingerprint is of incorrect length.");
            }

            return fingerprint.Substring(0, 8) + " " + fingerprint.Substring(8, 8) + " " + fingerprint.Substring(16, 8) + " " + fingerprint.Substring(24, 8) + " " + fingerprint.Substring(32);
        }

        /// <summary>
        /// Calculate a human-readable hash of our DSA public key.  Return it in
        /// the passed fingerprint buffer.  Return NULL on error, or a pointer to
        /// the given buffer on success.
        /// </summary>
        /// <param name="userState">The current <see cref="UserState"/>.</param>
        /// <param name="accountName">The AccountName.</param>
        /// <param name="protocol">The Protocol.</param>
        /// <returns>A human readable fingerprint (45 chars).</returns>
        public static string otrl_privkey_fingerprint(UserState userState, string accountName, string protocol)
        {
            PrivateKey privateKey = BasePrivateKey.otrl_privkey_find(userState, accountName, protocol);

            if (privateKey != null)
            {
                byte[] hash = SHA1.Create().ComputeHash(privateKey.PublicKeyAsMPI);
                return otrl_privkey_hash_to_human(hash);
            }

            return null;
        }

        /// <summary>
        /// Calculate a raw hash of our DSA public key.  Return it in the passed 
        /// fingerprint buffer.  Return NULL on error, or a pointer to the given
        /// buffer on success.
        /// </summary>
        /// <param name="userState">The current <see cref="UserState"/>.</param>
        /// <param name="accountName">The AccountName.</param>
        /// <param name="protocol">The Protocol.</param>
        /// <returns>A 20 byte hash.</returns>
        public static byte[] otrl_privkey_fingerprint_raw(UserState userState, string accountName, string protocol)
        {
            PrivateKey privateKey = BasePrivateKey.otrl_privkey_find(userState, accountName, protocol);

            if (privateKey != null)
            {
                return SHA1.Create().ComputeHash(privateKey.PublicKeyAsMPI);
            }

            return null;
        }

        /// <summary>
        /// Create a public key block from a private key.
        /// </summary>
        /// <param name="privateKey">The <see cref="DSA"/> PrivateKey.</param>
        /// <returns>The <see cref="DSACryptoServiceProvider"/> PublicKey.</returns>
        public static DSACryptoServiceProvider make_pubkey(DSA privateKey)
        {
            DSACryptoServiceProvider publicKey = new DSACryptoServiceProvider(1024);
            publicKey.ImportParameters(privateKey.ExportParameters(false));

            if (!publicKey.PublicOnly)
            {
                publicKey.Dispose();
                throw new Exception("PublicKey contains PrivateKey information, cancelling.");
            }

            return publicKey;
        }

        /// <summary>
        /// Fetch the private key from the given OtrlUserState associated with the given account.
        /// </summary>
        /// <param name="us"></param>
        /// <param name="accountname"></param>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public static PrivateKey otrl_privkey_find(UserState us, string accountname, string protocol)
        {
            return null;
        }
        #endregion
    }
}
