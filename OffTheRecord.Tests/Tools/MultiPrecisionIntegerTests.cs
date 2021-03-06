﻿// <copyright>
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

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Tools;

namespace OffTheRecord.Tests.Tools
{
    [TestClass]
    public class MultiPrecisionIntegerTests
    {
        [TestMethod]
        public void MultiPrecisionInteger_validate_implementation_matches_original_libgcrypt_implementation()
        {
            // Arrange

            /* p parameter DH */
            const string expected =
                "0080E83BBEA78425E04F546D7168C5A55FF4A89DBD82E92344E774157F84EF604E4F65CB73B5F459EE7F5690B1FD75597B073347F4F33C06C531BE63ED145CB4079AABB6D9F7162ABFEC25C01D6098B032C2835F2EA7F0F025E88BCEFA6F2B95BF78617B00385A3149248C0005F84DBB3AE0B97CC5867C2480164EF3C5C472954B";

            // Act
            byte[] p = General.StringToByteArray(expected);
            byte[] mpiP = MultiPrecisionInteger.ByteArrayToMpi(p);

            byte[] resultp = MultiPrecisionInteger.MpiToByteArray(mpiP);
            string result = General.ByteArrayToString(resultp);

            // Assert

            /* http://comments.gmane.org/gmane.comp.encryption.gpg.libgcrypt.devel/1669 */
            /* Why do you think it is unsafe?  Leading zeroes of numbers are
             * meanigless in computations.  When computing m = c^d mod n there won't
             * be a leading zero.  We need to invent it.  The reason pkcs#1 requires
             * the leading zero is to state that this is a non-negative number and
             * less than n. */

            result.Should().Be(expected.TrimStart(new[] {'0'}));
        }
    }
}