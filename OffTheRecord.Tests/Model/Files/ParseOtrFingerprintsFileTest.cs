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
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Model;
using OffTheRecord.Model.Files;
using OffTheRecord.Model.Files.OtrFingerprints;

namespace OffTheRecord.Tests.Model.Files
{
    [TestClass]
    public class ParseOtrFingerprintsFileTest
    {
        [TestMethod]
        public void FileHandling_compare_serialized_results_against_Pidgin_otr_fingerprints()
        {
            var fp1 = new fingerprint("marshal2", "marshal3@irc.freenode.net", "prpl-irc",
                "80724d46d9d906a28af31d15adfd510822ac3fd9", Statuses.verified);
            var fp2 = new fingerprint("marshal3", "marshal2@irc.freenode.net", "prpl-irc",
                "18dcd190ccaad02aed74e69d3b96355e61a82b3e", Statuses.verified);
            var fp3 = new fingerprint("test123_4", "testuser2@irc.freenode.net", "prpl-irc",
                "64bfb577c9591b3dbb6b697599f572ce7d1ffc9d", Statuses.smp);
            var fp4 = new fingerprint("testuser2", "test123_4@irc.freenode.net", "prpl-irc",
                "51f2e7db2a0c14facd568107aceaae73f362c869", Statuses.smp);

            var fingerprints = new Collection<fingerprint> { fp1, fp2, fp3, fp4 };

            string actual = ParseOtrFingerprintsFile.Serialize(fingerprints);
            string expected = FileResource.otr_fingerprints;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FileHandling_deserialize_fingerprints_within_Pidgin_to_internal_object()
        {
            Collection<fingerprint> fingerprints =
                ParseOtrFingerprintsFile.DeserializeFromString(FileResource.otr_fingerprints);

            Assert.AreEqual(4, fingerprints.Count);

            fingerprint first = fingerprints[0];
            fingerprint last = fingerprints[3];

            Assert.AreEqual("marshal3@irc.freenode.net", first.Account);
            Assert.AreEqual("80724d46d9d906a28af31d15adfd510822ac3fd9", first.Fingerprint);
            Assert.AreEqual("marshal2", first.Username);
            Assert.AreEqual("prpl-irc", first.Protocol);
            Assert.AreEqual(Statuses.verified, first.Status);

            Assert.AreEqual("test123_4@irc.freenode.net", last.Account);
            Assert.AreEqual("51f2e7db2a0c14facd568107aceaae73f362c869", last.Fingerprint);
            Assert.AreEqual("testuser2", last.Username);
            Assert.AreEqual("prpl-irc", last.Protocol);
            Assert.AreEqual(Statuses.smp, last.Status);
        }

        [TestMethod]
        public void FileHandling_deserialize_fingerprints_within_Pidgin_to_external_object()
        {
            Fingerprints results =
                ParseOtrFingerprintsFile.GetFingerprintsFromString(FileResource.otr_fingerprints);

            Assert.AreEqual(4, results.Count);
            Assert.AreEqual("marshal3@irc.freenode.net", results[0].AccountName);
            Assert.AreEqual("80724d46 d9d906a2 8af31d15 adfd5108 22ac3fd9", results[0].Print);
            Assert.AreEqual("marshal2", results[0].Username);
            Assert.AreEqual("prpl-irc", results[0].Protocol);
            Assert.AreEqual(Fingerprint.FingerprintStatus.Verified, results[0].Status);
        }
    }
}
