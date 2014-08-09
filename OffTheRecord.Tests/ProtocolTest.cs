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
    using System.IO;
    #endregion

    [TestClass]
    [DeploymentItem(@"Files\ProtocolTest\", @"Files\ProtocolTest\")]
    public class ProtocolTest
    {
        /// <summary>
        /// Initializes a instance of the OTR Library by reading the user files
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core)]
        public void InitializeInstanceOfOtrLibraryTest()
        {
            Model.Protocol protocol = new Protocol();
            Assert.IsNotNull(protocol);
        }

        /// <summary>
        /// Create UserState object and populate with data.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core)]
        public void CreateAndPopulateUserStateWithPrivateKeysAndInstanceTagsTest()
        {
            Model.Protocol protocol = new Protocol();
            UserState userstate = protocol.CreateUserState();

            string basepath = @"Files\ProtocolTest\";

            string privateKeysFilename = Path.Combine(basepath, "otr.private_key");
            string instanceTagsFilename = Path.Combine(basepath, "otr.instance_tags");
            ////string fingerprintFilename = Path.Combine(basepath, "otr.fingerprints");

            userstate.ReadPrivateKeys(privateKeysFilename);
            userstate.ReadInstanceTags(instanceTagsFilename);
            ////userstate.ReadFingerprints(fingerprintFilename);

            Assert.AreEqual<int>(2, userstate.PrivateKeys.Count);
            Assert.AreEqual<int>(2, userstate.InstanceTags.Count);
        }

        /// <summary>
        /// Free UserState object.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core)]
        public void FreeUserStateTest()
        {
            Model.Protocol protocol = new Protocol();
            UserState userstate = protocol.CreateUserState();

            string basepath = @"Files\ProtocolTest\";

            string privateKeysFilename = Path.Combine(basepath, "otr.private_key");
            string instanceTagsFilename = Path.Combine(basepath, "otr.instance_tags");
            ////string fingerprintFilename = Path.Combine(basepath, "otr.fingerprints");

            userstate.ReadPrivateKeys(privateKeysFilename);
            userstate.ReadInstanceTags(instanceTagsFilename);
            ////userstate.ReadFingerprints(fingerprintFilename);

            userstate.Dispose();

            Assert.IsNull(userstate.PrivateKeys);
            Assert.IsNull(userstate.InstanceTags);

            userstate = null;
        }
    }
}
