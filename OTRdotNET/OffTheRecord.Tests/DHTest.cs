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

namespace OffTheRecord.Tests
{
    #region Namespaces
    using System.Numerics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OffTheRecord.Protocol.DiffieHellman;
    using OffTheRecord.Tests.Helper;

    #endregion

    /// <summary>
    /// DHTest class.
    /// </summary>
    [TestClass]
    public class DHTest
    {
        /// <summary>
        /// A test for CalculateSharedSecret.
        /// </summary>
        /// <remarks>
        /// tests against the example as provided on wikipedia: http://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange .
        /// </remarks>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core, OtrTestCategories.Encryption)]
        public void TestDH()
        {
            System.Numerics.BigInteger modulus = new System.Numerics.BigInteger(23);
            System.Numerics.BigInteger value = new System.Numerics.BigInteger(5);

            DH alice = new DH(modulus, value);
            alice.GeneratePublicKey(6);

            DH bob = new DH(modulus, value);
            bob.GeneratePublicKey(15);

            alice.GenerateSharedSecret(bob.PublicKey);
            bob.GenerateSharedSecret(alice.PublicKey);

            Assert.AreEqual(8, alice.PublicKey);
            Assert.AreEqual(19, bob.PublicKey);
            Assert.AreEqual(alice.SharedSecret, bob.SharedSecret);
        }

        /// <summary>
        /// A test for CalculateSharedSecret.
        /// </summary>
        /// <remarks>
        /// tests against the example as provided on wikipedia: http://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange .
        /// </remarks>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core, OtrTestCategories.Encryption)]
        public void TestDH1536()
        {
            DH1536 alice = new DH1536();
            alice.GeneratePrivateAndPublicKey();

            DH1536 bob = new DH1536();
            bob.GeneratePrivateAndPublicKey();

            alice.GenerateSharedSecret(bob.PublicKey);
            bob.GenerateSharedSecret(alice.PublicKey);

            Assert.AreEqual(alice.SharedSecret, bob.SharedSecret);
        }

        /// <summary>
        /// A test for replicating DH1536 behavior of OTR.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core, OtrTestCategories.Encryption)]
        public void TestDH1536WithRealTestData()
        {
            string privatekey = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA";
            string expectedPublicKey = "b1be99fd638d2b634f9825f753ff7f2213ae7207a390b5df3b685a8516d63d49c3bceeb826c1cd09eb030430772193b82f1f4ab01c77e38b7eff100c0fb296bd1d6148bd205fdce3a2ec33ef9c3413eb06d1f413d52ad0747b9273783f7ee88435498b5774967da987ce10e7a2cec72ceecc8f95ceaf92edf82b3e0f69faa87de5eb4748325f82f0bc43f24984b5af2c9d3043d9871c3c952b22a5b292cdead6a67caa62c0196745ed608a6aaf8797fe5801f0506b8f8aa5f431dc583ea584a8".ToUpper();

            BigInteger privateKeyAsInt = BigInteger.Parse(privatekey, System.Globalization.NumberStyles.HexNumber);

            Assert.AreEqual<string>(privateKeyAsInt.ToString("X").ToLower(), privatekey.ToLower());

            DH1536 dh = new DH1536();
            dh.GeneratePublicKey(privateKeyAsInt);

            BigInteger publicKey = dh.PublicKey;

            string publicKeyAsHexString = publicKey.ToString("X").TrimStart(new char[] { '0' });
            Assert.AreEqual<string>(expectedPublicKey, publicKeyAsHexString);
        }
    }
}
