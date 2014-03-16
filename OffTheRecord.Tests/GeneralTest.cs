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
    using OffTheRecord.Tests.Helper;
    #endregion

    /// <summary>
    /// GeneralTest class.
    /// </summary>
    [TestClass]
    public class GeneralTest
    {
        /// <summary>
        /// Tests the StringToByteArray and vice versa converter.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Tools)]
        public void Converter()
        {
            string expected = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA"; /* private key */

            byte[] privkey = Tools.General.StringToByteArray(expected);
            string actual = Tools.General.ByteArrayToString(privkey);

            Assert.AreEqual<string>(expected, actual);
        }
    }
}
