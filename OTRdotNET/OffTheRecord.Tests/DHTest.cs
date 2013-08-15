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
    }
}
