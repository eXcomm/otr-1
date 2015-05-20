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
    public class Dh
    {
        #region Fields
        private readonly BigInteger _modulus;
        private readonly BigInteger _value;
        #endregion

        #region Constructor
        public Dh(BigInteger modulus, BigInteger value)
        {
            _modulus = modulus;
            _value = value;
        }
        #endregion

        #region Public properties
        public BigInteger PrivateKey { get; private set; }
        public BigInteger PublicKey { get; private set; }
        public BigInteger SharedSecret { get; private set; }
        public BigInteger TheirPublicKey { get; private set; }
        #endregion

        #region Public methods
        public void GeneratePrivateAndPublicKey(int bitSize)
        {
            GeneratePublicKey(GeneratePrivateKey(bitSize));
        }

        public void GeneratePublicKey(BigInteger privateKey)
        {
            PrivateKey = privateKey;
            PublicKey = BigInteger.ModPow(_value, PrivateKey, _modulus);
        }

        public void GenerateSharedSecret(BigInteger theirPublicKey)
        {
            if (PrivateKey == null)
            {
                throw new Exception("PrivateKey is not set.");
            }

            TheirPublicKey = theirPublicKey;
            SharedSecret = BigInteger.ModPow(theirPublicKey, PrivateKey, _modulus);
        }
        #endregion

        #region Private methods
        private BigInteger GeneratePrivateKey(int bitSize)
        {
            var bytes = new byte[bitSize / 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            return BigInteger.Abs(new BigInteger(bytes));
        }
        #endregion
    }
}
