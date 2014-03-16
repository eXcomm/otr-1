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
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OffTheRecord.Tests.Helper;
    using OffTheRecord.Model;
    #endregion

    [TestClass]
    [DeploymentItem(@"Files\ProtocolTest\", @"Files\ProtocolTest\")]
    public class ProtocolTest
    {
        /// <summary>
        /// Authenticated Key Exchange using SIGMA protocol test.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core)]
        public void SignAndMacProtocolTest()
        {
            // get userstate information.
            UserState userstate = new UserState();
            userstate.ReadPrivateKeys(@"Files\ProtocolTest\otr.private_key");

            var alice = userstate.PrivateKeys["__alice@irc.freenode.net"];
            var bob = userstate.PrivateKeys["__bob@irc.freenode.net"];

            Assert.IsNotNull(alice);
            Assert.IsNotNull(bob);

            // otrl_message_receiving function

            // start negotiating.

            Assert.Inconclusive();
        }
    }
}
