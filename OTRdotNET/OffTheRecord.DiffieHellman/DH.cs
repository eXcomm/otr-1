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
    using System.Numerics;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// Diffie-Hellman implementation.
    /// </summary>
    /// <remarks>
    /// BigInteger - MSDN - http://msdn.microsoft.com/en-us/library/system.numerics.biginteger(v=vs.100).aspx .
    /// </remarks>
    public class DH
    {
        #region Fields
        private readonly BigInteger modulus;
        private readonly BigInteger value;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DH"/> class.
        /// </summary>
        /// <param name="modulus">The modulus.</param>
        /// <param name="value">The value.</param>
        public DH(BigInteger modulus, BigInteger value)
        {
            this.modulus = modulus;
            this.value = value;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the private key.
        /// </summary>
        public BigInteger PrivateKey { get; private set; }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        public BigInteger PublicKey { get; private set; }

        /// <summary>
        /// Gets the shared secret.
        /// </summary>
        public BigInteger SharedSecret { get; private set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Generate private and public key for specific bit size.
        /// </summary>
        /// <param name="bitSize">The bit size to use.</param>
        public void GeneratePrivateAndPublicKey(int bitSize)
        {
            this.GeneratePublicKey(this.GeneratePrivateKey(bitSize));
        }

        /// <summary>
        /// Generate public key based on private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        public void GeneratePublicKey(BigInteger privateKey)
        {
            this.PrivateKey = privateKey;
            this.PublicKey = BigInteger.ModPow(this.value, this.PrivateKey, this.modulus);
        }

        /// <summary>
        /// Generate shared secret based on Public Key from other <see cref="DH"/> class.
        /// </summary>
        /// <param name="publicKey">Public key to create shared secret with.</param>
        public void GenerateSharedSecret(BigInteger publicKey)
        {
            this.SharedSecret = BigInteger.ModPow(publicKey, this.PrivateKey, this.modulus);
        }
        #endregion

        #region Private methods
        private BigInteger GeneratePrivateKey(int bitSize)
        {
            byte[] bytes = new byte[bitSize / 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            return BigInteger.Abs(new BigInteger(bytes));
        }
        #endregion
    }
}
