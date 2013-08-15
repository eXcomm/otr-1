namespace OffTheRecord.Model
{
    #region Namespaces
    using System;
    using System.Security.Cryptography;
    #endregion

    public abstract class BasePrivateKey
    {
        #region Public Properties
        public abstract BasePrivateKey Next { get; }
        public abstract BasePrivateKey ToUs { get; }
        public abstract string AccountName { get; }
        public abstract string Protocol { get; }
        #endregion

        #region Public methods
        /// <summary>
        /// Convert a 20-byte hash value to a 45 chars human-readable value.
        /// </summary>
        /// <param name="hash">SHA1 hash of private key.</param>
        /// <returns>A human readable fingerprint (45 chars)</returns>
        public static string otrl_privkey_hash_to_human(byte[] hash)
        {
            if (hash.Length != 20)
            {
                throw new ArgumentException("hash should be 20 bytes.");
            }

            string fingerprint = BitConverter.ToString(hash).Replace("-", "");

            if (fingerprint.Length != 40)
            {
                throw new Exception("Fingerprint is of incorrect length.");
            }

            return fingerprint.Substring(0, 8) + " " + fingerprint.Substring(8, 8) + " " + fingerprint.Substring(16, 8) + " " + fingerprint.Substring(24, 8) + " " + fingerprint.Substring(32);

        }

        /// <summary>
        /// Calculate a human-readable hash of our DSA public key.  Return it in
        /// the passed fingerprint buffer.  Return NULL on error, or a pointer to
        /// the given buffer on success. */
        /// </summary>
        /// <param name="userState">The current <see cref="UserState"/>.</param>
        /// <param name="accountName">The AccountName.</param>
        /// <param name="protocol">The Protocol.</param>
        /// <returns>A human readable fingerprint (45 chars)</returns>
        public static string otrl_privkey_fingerprint(UserState userState, string accountName, string protocol)
        {
            PrivateKey privateKey = LocalProtocol.otrl_privkey_find(userState, accountName, protocol);

            if (privateKey != null)
            {
                byte[] hash = SHA1.Create().ComputeHash(privateKey.PublicKey);
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
        /// <returns>A 20 byte hash</returns>
        public static byte[] otrl_privkey_fingerprint_raw(UserState userState, string accountName, string protocol)
        {
            PrivateKey privateKey = LocalProtocol.otrl_privkey_find(userState, accountName, protocol);

            if (privateKey != null)
            {
                return SHA1.Create().ComputeHash(privateKey.PublicKey);
            }

            return null;
        }

        /// <summary>
        /// Create a public key block from a private key 
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
        #endregion
    }
}
