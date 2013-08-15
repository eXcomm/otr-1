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

namespace OffTheRecord.Tests
{
    #region Namespaces
    using System.Numerics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OffTheRecord.Tests.Helper;
    using OffTheRecord.Tools;
    #endregion

    /// <summary>
    /// EndianTest class.
    /// </summary>
    [TestClass]
    public class EndianTest
    {
        #region Test methods
        /// <summary>
        /// Determines the Endian for the current architecture.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Tools)]
        public void CheckEndian()
        {
            Assert.IsTrue(Tools.Endian.IsLittleEndian, "Running on Windows 8, this is expected to be Little Endian.");
        }

        /// <summary>
        /// Compares a libgcrypt generated private key from a big endian architecture to the actual result, validating the endian conversion is working correctly.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Tools)]
        public void ByteArrayToBigInteger()
        {
            /* expected value */
            string expected = "415325779662433871844955547383752003988573073626";

            /* private key generated by libgcrypt, using different endian */
            string x = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA";

            byte[] otrPrivateKey = Tools.General.StringToByteArray(x);
            byte[] mpiOtrPrivateKey = MPI.To(otrPrivateKey);

            /* removed any leading zero's; thus different then otrPrivateKey */
            byte[] actualPrivateKeyValue = MPI.From(mpiOtrPrivateKey);

            BigInteger actual = Endian.ToBigInteger(actualPrivateKeyValue);

            Assert.AreEqual<string>(expected, actual.ToString());
        }
        #endregion
    }
}
