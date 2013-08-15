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
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OffTheRecord.Model.Pidgin;
    using OffTheRecord.Model.Pidgin.UserSettingsFile;
    using OffTheRecord.Tests.Helper;
    #endregion

    /// <summary>
    /// PidginUserSettingsFileTest class.
    /// </summary>
    [TestClass]
    public class PidginUserSettingsFileTest
    {
        /// <summary>
        /// Tests the Pidgin OTR User Settings File serializer.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.General)]
        public void Serialize()
        {
            privkeys privkeys = new privkeys();
            account act1 = new account("alice@secret.com", "prpl-msn");
            act1.private_key = new private_key() { dsa = new dsa("00AEC0FBB4CEA96EF8BDD0E91D1BA2F6641B6535CBDA8D739CC2898FE7B472865AB60AD2B1BAA2368603C7439E63BC2F2F33D422E70173F70DB738DF5979EAEAF3CAC343CBF711960E16786703C80DF0734D8330DC955DA84B521DAB5C729202F1244D805E6BF2CC7A7142CAD74BE5FFFC14B9CCB6CABB7DB10A8F2DDB4E82383F", "00A2A2BC20E2D94C44C63608479C79068CE7914EF3", "69B9FC5A73F3F6EA3A86F8FA3A203F42DACDC3A1516002025E5765A9DCB975F348ACBBA2116230E19CE3FC5256546FD168A2940809BDA8655771967E9CD90AF44D2C20F97F448494213A775E23607F33C255A9A74E2A5FC7B4D50BAD024D7EFAC282E67332D51A5F69239011FE058D7E75E97A788FBD5B3BAD796B2C6D8C6C3E", "009931144F3059D92FCB2AAC03B130DAE43ED1EF30AA2F0E670C3974C3E80C7110D1A60210F92479D7F640C20E1F16E01B4A72FF8D45443B01EBE2D67DF49791CAC6191B159AC39446EB6A2EA597B6B678CC3157AECEAB12A804CF0772068A942EC819138EDD6005620FE746522FF408BBC8211ABD9D6016AA46EEC87F3F04CFA4", "48BFDA215C31A9F0B226B3DB11F862450A0F30DA") };
            account act2 = new account("bob@secret.com", "prpl-irc");
            act2.private_key = new private_key() { dsa = new dsa("0080E83BBEA78425E04F546D7168C5A55FF4A89DBD82E92344E774157F84EF604E4F65CB73B5F459EE7F5690B1FD75597B073347F4F33C06C531BE63ED145CB4079AABB6D9F7162ABFEC25C01D6098B032C2835F2EA7F0F025E88BCEFA6F2B95BF78617B00385A3149248C0005F84DBB3AE0B97CC5867C2480164EF3C5C472954B", "00D67F5ECEF98D8AF597C743F2B286CCB059C241CB", "62361C169893F571CF3DD1F5528E89D8FAEE843B5B243DABD11F36C03FBFEC1EDFC20C7BDD8F32C2F8CF0EAEFE80181364A4339882C2B4AF21C4CFC076C9CB3ECB63A3CAE62286C8D0F98EF4804AF4706B93CEEA877202AF814833F2E950E3F08AFD05BF073FA592881DA8FA1A7AB4479DBC27CB9B43EDA58BE0BBFAADBD23F8", "3D82B953839E7C9295D67B403655DB54247268004C830004DBF86F692A21407249249AFC5D40F999ED3E9D4F54895B8404FD59B12297D564D9E16CB24803B48760D236C2F41F9263CC76BF065878B50EC5789443DEE2EDF771765F6B105E2A32ACB8E3DA8E44D187C4B99AED1B348FE81A34CC00EF89D790EE8FC832E7C018A1", "00C09DFA159A9B887A8E34BED3F9382463B4C4FB93") };

            privkeys.account.Add(act1);
            privkeys.account.Add(act2);

            string result = ParseUserSettingsFile.Serialize(privkeys);

            Assert.Inconclusive();
        }

        /// <summary>
        /// Tests the Pidgin OTR User Settings File deserializer.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.General)]
        public void Deserialize()
        {
            string filename = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Files\otr.private_key");
            privkeys privkeys = ParseUserSettingsFile.Deserialize(filename);

            Assert.Inconclusive();
        }

        /// <summary>
        /// Tests the creation of a OTR fingerprint.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.General)]
        public void FingerPrintTest()
        {
            string filename = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Files\otr.private_key");

            var privkeys = ParseUserSettingsFile.Deserialize(filename);

            string expectedFingerprint = "03E216F6 E65C5043 F819FFBC E1FA4FCF 7114F7D4";

            string actualFingerprint = privkeys.account[0].private_key.Fingerprint();

            Assert.AreEqual<string>(expectedFingerprint, actualFingerprint, "Fingerprints don't match");
        }
    }
}
