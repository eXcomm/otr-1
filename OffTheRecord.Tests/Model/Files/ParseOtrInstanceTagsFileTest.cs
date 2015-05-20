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
using OffTheRecord.Model.Files.OtrInstanceTags;

namespace OffTheRecord.Tests.Model.Files
{
    [TestClass]
    public class ParseOtrInstanceTagsFileTest
    {
        [TestMethod]
        public void FileHandling_compare_serialized_results_against_Pidgin_otr_instance_tags()
        {
            var it1 = new instancetag("testuser2@irc.freenode.net", "prpl-irc", "299c2916");
            var it2 = new instancetag("test123_4@irc.freenode.net", "prpl-irc", "8cf547f1");
            var it3 = new instancetag("marshal3@irc.freenode.net", "prpl-irc", "4b2bf242");
            var it4 = new instancetag("marshal2@irc.freenode.net", "prpl-irc", "f2e0ee97");

            var instancetags = new Collection<instancetag> { it1, it2, it3, it4 };

            string actual = ParseOtrInstanceTagsFile.Serialize(instancetags);
            string expected = FileResource.otr_instance_tags;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FileHandling_deserialize_instancetags_within_Pidgin_to_internal_object()
        {
            Collection<instancetag> instancetags =
                ParseOtrInstanceTagsFile.DeserializeFromString(FileResource.otr_instance_tags);

            Assert.AreEqual(4, instancetags.Count);

            instancetag first = instancetags[0];
            instancetag last = instancetags[3];

            Assert.AreEqual("testuser2@irc.freenode.net", first.Account);
            Assert.AreEqual("prpl-irc", first.Protocol);
            Assert.AreEqual("299c2916", first.InstanceTag);

            Assert.AreEqual("marshal2@irc.freenode.net", last.Account);
            Assert.AreEqual("prpl-irc", last.Protocol);
            Assert.AreEqual("f2e0ee97", last.InstanceTag);
        }

        [TestMethod]
        public void FileHandling_deserialize_instancetags_within_Pidgin_to_external_object()
        {
            InstanceTags results =
                ParseOtrInstanceTagsFile.GetInstanceTagsFromString(FileResource.otr_instance_tags);

            Assert.AreEqual(4, results.Count);
            Assert.AreEqual("testuser2@irc.freenode.net", results[0].AccountName);
            Assert.AreEqual("prpl-irc", results[0].Protocol);
            Assert.AreEqual<uint>(698099990, results[0].Tag);
        }
    }
}
