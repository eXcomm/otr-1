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

using System.Globalization;
using System.Numerics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Protocol.DiffieHellman;

namespace OffTheRecord.Tests.Protocol.DiffieHellman
{
    [TestClass]
    public class DiffieHellmanTests
    {
        /// <remarks>
        ///     tests against the example as provided on wikipedia:
        ///     http://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange .
        /// </remarks>
        [TestMethod]
        public void DiffieHellman_basic_test_of_shared_secret()
        {
            // Arrange
            var modulus = new BigInteger(23);
            var value = new BigInteger(5);

            var alice = new Dh(modulus, value);
            alice.GeneratePublicKey(6);
            var bob = new Dh(modulus, value);
            bob.GeneratePublicKey(15);

            // Act
            alice.GenerateSharedSecret(bob.PublicKey);
            bob.GenerateSharedSecret(alice.PublicKey);

            // Assert
            alice.PublicKey.Should().Be(8);
            bob.PublicKey.Should().Be(19);
            alice.SharedSecret.Should().Be(bob.SharedSecret);
        }

        /// <remarks>
        ///     tests against the example as provided on wikipedia:
        ///     http://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange .
        /// </remarks>
        [TestMethod]
        public void DiffieHellman_1536bit_basic_test_of_shared_secret()
        {
            // Arrange
            var alice = new Dh1536();
            alice.GeneratePrivateAndPublicKey();

            var bob = new Dh1536();
            bob.GeneratePrivateAndPublicKey();

            // Act
            alice.GenerateSharedSecret(bob.PublicKey);
            bob.GenerateSharedSecret(alice.PublicKey);

            // Assert
            alice.SharedSecret.Should().Be(bob.SharedSecret);
        }

        [TestMethod]
        public void Validate_private_key_parsing_to_BigInteger()
        {
            // Arrange
            const string privatekey = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA";

            // Act
            BigInteger privateKeyAsInt = BigInteger.Parse(privatekey, NumberStyles.HexNumber);

            // Assert
            privateKeyAsInt.ToString("X").ToLowerInvariant().Should().Be(privatekey.ToLowerInvariant());
        }

        [TestMethod]
        public void DiffieHellman_1536bit_validate_public_key_generation()
        {
            // Arrange
            const string privatekey = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA";
            const string expectedPublicKey =
                "B1BE99FD638D2B634F9825F753FF7F2213AE7207A390B5DF3B685A8516D63D49C3BCEEB826C1CD09EB030430772193B82F1F4AB01C77E38B7EFF100C0FB296BD1D6148BD205FDCE3A2EC33EF9C3413EB06D1F413D52AD0747B9273783F7EE88435498B5774967DA987CE10E7A2CEC72CEECC8F95CEAF92EDF82B3E0F69FAA87DE5EB4748325F82F0BC43F24984B5AF2C9D3043D9871C3C952B22A5B292CDEAD6A67CAA62C0196745ED608A6AAF8797FE5801F0506B8F8AA5F431DC583EA584A8";

            BigInteger privateKeyAsInt = BigInteger.Parse(privatekey, NumberStyles.HexNumber);

            var dh = new Dh1536();

            // Act
            dh.GeneratePublicKey(privateKeyAsInt);
            BigInteger publicKey = dh.PublicKey;

            // Assert
            publicKey.ToString("X").TrimStart(new[] {'0'}).Should().Be(expectedPublicKey);
        }

        [TestMethod]
        public void DiffieHellman_1536bit_validate_Their_public_key_generation()
        {
            // Arrange
            const string theirPublicKey = "64bfb577c9591b3dbb6b697599f572ce7d1ffc9d";
            const string privatekey = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA";

            BigInteger privateKeyAsInt = BigInteger.Parse(privatekey, NumberStyles.HexNumber);

            // Act
            var dh = new Dh1536();
            dh.GeneratePublicKey(privateKeyAsInt);
            dh.GenerateSharedSecret(BigInteger.Parse(theirPublicKey, NumberStyles.HexNumber));

            // Assert
            dh.TheirPublicKey.ToString("X").Should().Be(theirPublicKey.ToUpperInvariant());
        }

        [TestMethod]
        public void DiffieHellman_1536bit_validate_SessionId_generation()
        {
            // Arrange
            const string privatekey = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA";
            const string theirPublicKey = "64bfb577c9591b3dbb6b697599f572ce7d1ffc9d";
            const string expectedSessionId = "8FDEF45085A911525E8C408C5F9A3DB1C1104CB1";

            BigInteger privateKeyAsInt = BigInteger.Parse(privatekey, NumberStyles.HexNumber);

            // Act
            var dh = new Dh1536();
            dh.GeneratePublicKey(privateKeyAsInt);
            dh.GenerateSharedSecret(BigInteger.Parse(theirPublicKey, NumberStyles.HexNumber));

            // Assert
            dh.SessionId().Should().Be(expectedSessionId);
        }

        [TestMethod]
        public void DiffieHellman_1536bit_validate_key_generation()
        {
            // Arrange
            const string privatekey = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA";
            const string theirPublicKey = "64BFB577C9591B3DBB6B697599F572CE7D1FFC9D";

            BigInteger privateKeyAsInt = BigInteger.Parse(privatekey, NumberStyles.HexNumber);

            // Act 
            var dh = new Dh1536();
            dh.GeneratePublicKey(privateKeyAsInt);
            dh.GenerateSharedSecret(BigInteger.Parse(theirPublicKey, NumberStyles.HexNumber));
            Keys keys = dh.Keys();

            // Assert
            keys.SendAes.Should().Be("3ECEFA2E6EA280C9EBCA91E6E37F2B60");
            keys.SendMac.Should().Be("2829522E80354BAC5BE5DE648116E48B665C7D6C");
            keys.ReceiveAes.Should().Be("8863A4479AE2857FB9BE657E3B7E37C4");
            keys.ReceiveMac.Should().Be("A43167D308BA9DE0127F3124A55BEA9A608C10C4");
            keys.IsHigh.Should().BeTrue();
        }
    }
}