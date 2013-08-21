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

namespace OffTheRecord.Tests.Protocol
{
    #region Namespaces
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OffTheRecord.Model;
    using OffTheRecord.Model.Files;
    using OffTheRecord.Model.Files.OtrPrivateKey;
    using OffTheRecord.Tests.Helper;
    #endregion

    /// <summary>
    /// ProtocolTest class.
    /// </summary>
    [TestClass]
    public class ProtocolTest
    {
        /// <summary>
        /// Tests the parsing of a DataMessage.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitParse, OtrTestCategories.Core)]
        public void DataMessageParseTest01()
        {
            string input = "?OTR:AAMDSyvyQvLg7pcAAAAAAQAAAAEAAADAVoV88L+aKOU6X25AixfPKDvijKUVHhGdSFZlQpA5XepzoyEqA8ATbjYPwjE7FZApV87oUx+QQog39bJ2GA/zYqrag/xrRzLZfE9K3E7PmUaeUZijLCQA5hTYemzV/crv8SQiLbasDmNDKNi8X/XQuGSPhFD2/jtl13MElkbDWWYiQzX2Ck4lhsHGp0gsNLBhOwkwPGRzmWB+1ltRvb9XqhTuF6S83qGy9iM7pm3yT048awWY4FOG24dukbja1jbNAAAAAAAAAAEAAAANXtnheROJlgrrv2dCFmJ6bYB4YqCkGD2qjQM8s6q391HnAAAAAA==.";

            OffTheRecord.Protocol.Messages.DataMessage dm = OffTheRecord.Protocol.Messages.DataMessage.Parse(input);

            Assert.AreEqual<int>(3, dm.Version);
            Assert.AreEqual<uint>(0, dm.Flags);
            Assert.AreEqual<uint>(1261171266, dm.SenderInstance);
            Assert.AreEqual<uint>(4074827415, dm.ReceiverInstance);
            Assert.AreEqual<uint>(1, dm.SenderKeyId);
            Assert.AreEqual<uint>(1, dm.ReceiverKeyId);
            Assert.AreEqual<string>("56857cf0bf9a28e53a5f6e408b17cf283be28ca5151e119d4856654290395dea73a3212a03c0136e360fc2313b15902957cee8531f90428837f5b276180ff362aada83fc6b4732d97c4f4adc4ecf99469e5198a32c2400e614d87a6cd5fdcaeff124222db6ac0e634328d8bc5ff5d0b8648f8450f6fe3b65d773049646c35966224335f60a4e2586c1c6a7482c34b0613b09303c647399607ed65b51bdbf57aa14ee17a4bcdea1b2f6233ba66df24f4e3c6b0598e05386db876e91b8dad636cd".ToUpper(), dm.Y);
            Assert.AreEqual<ulong>(1, dm.Counter);
            Assert.AreEqual<string>("5ed9e1791389960aebbf674216".ToUpper(), dm.EncryptedMessage);
            Assert.AreEqual<string>("627a6d807862a0a4183daa8d033cb3aab7f751e7".ToUpper(), dm.MAC);
        }

        /// <summary>
        /// Tests the parsing of a DH Commit Message.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitParse, OtrTestCategories.Core)]
        public void DHCommitMessageParseTest01()
        {
            string input = "?OTR:AAMC8uDulwAAAAAAAADEdJDE3Q/lG1LdKWVfWc6+NmWhTBvd06+VGvVbH19vhKmGeVZ16dpXsWfqXDFsRVxovU7pFLe0+5GGiTcc16gC681IOJfEsfm9JTpaFOr2XDsJQwHnIhFp8qSkvwfqEwx0FLelaSmI6ipNzlQCtzZ3Y7ihAAMZV3cEc05w0xwHqkIXLtn73+Fohp1RuLw2ubIFbFJRlZcteyK6VFfAN9HBv+TnvsSQ8iHSIBMQpfzZgT1A0IxrboAyg1xPu1XSA65G+HLfoQAAACC9FgJItQxwRnJCFYBGYjofLhZntfzxbHTusuV/5JrT2g==.";

            OffTheRecord.Protocol.Messages.DHCommitMessage dhcm = OffTheRecord.Protocol.Messages.DHCommitMessage.Parse(input);

            Assert.AreEqual<int>(3, dhcm.Version);
            Assert.AreEqual<uint>(4074827415, dhcm.SenderInstance);
            Assert.AreEqual<uint>(0, dhcm.ReceiverInstance);
            Assert.AreEqual<string>("7490c4dd0fe51b52dd29655f59cebe3665a14c1bddd3af951af55b1f5f6f84a986795675e9da57b167ea5c316c455c68bd4ee914b7b4fb918689371cd7a802ebcd483897c4b1f9bd253a5a14eaf65c3b094301e7221169f2a4a4bf07ea130c7414b7a5692988ea2a4dce5402b7367763b8a1000319577704734e70d31c07aa42172ed9fbdfe168869d51b8bc36b9b2056c525195972d7b22ba5457c037d1c1bfe4e7bec490f221d2201310a5fcd9813d40d08c6b6e8032835c4fbb55d203ae46f872dfa1".ToUpper(), dhcm.EncryptedMessage);
            Assert.AreEqual<string>("bd160248b50c70467242158046623a1f2e1667b5fcf16c74eeb2e57fe49ad3da".ToUpper(), dhcm.HashKey);
        }

        /// <summary>
        /// Tests the parsing of a DH Key Message.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitParse, OtrTestCategories.Core)]
        public void DHKeyMessageParseTest01()
        {
            string input = "?OTR:AAMKSyvyQvLg7pcAAADALSQOLMyMB7ZF8LeghEQw2HNeP+dnuWe+NAue2s+d1ZtnqVvzXhDJIDG8wdiIZ3NCOYcRRyn11sFEZPeRQ7AoIeh+cNmGccALb91T9HFoISs+/+LW+1jBjiJ9Wn2adtUcZmkukP60VSXF+PnSQGXQKMTXUlOM9QsELm2Z3k6dNMM0hAALDEGvWbSX8wW9ETgKPNx3HINA+dM8NEy+0BPv5laCEDzmyu6s8cAF0Z7pumHys7ZSxR7UunzejGIy7CiE.";

            OffTheRecord.Protocol.Messages.DHKeyMessage dhkm = OffTheRecord.Protocol.Messages.DHKeyMessage.Parse(input);

            Assert.AreEqual<int>(3, dhkm.Version);
            Assert.AreEqual<uint>(1261171266, dhkm.SenderInstance);
            Assert.AreEqual<uint>(4074827415, dhkm.ReceiverInstance);
            Assert.AreEqual<string>("2d240e2ccc8c07b645f0b7a0844430d8735e3fe767b967be340b9edacf9dd59b67a95bf35e10c92031bcc1d8886773423987114729f5d6c14464f79143b02821e87e70d98671c00b6fdd53f47168212b3effe2d6fb58c18e227d5a7d9a76d51c66692e90feb45525c5f8f9d24065d028c4d752538cf50b042e6d99de4e9d34c33484000b0c41af59b497f305bd11380a3cdc771c8340f9d33c344cbed013efe65682103ce6caeeacf1c005d19ee9ba61f2b3b652c51ed4ba7cde8c6232ec2884".ToUpper(), dhkm.MPI);
        }

        /// <summary>
        /// Tests the parsing of a Reveal Signature Message.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitParse, OtrTestCategories.Core)]
        public void RevealSignatureMessageParseTest01()
        {
            string input = "?OTR:AAMRKZwpFoz1R/EAAAAQC0nl/DaDsUlZuO9CZ91smQAAAdJdD0kXUrngjHS6kvdk17DDjXGXyIbIZmgNGo8wlXZBr7LZkct226XTVVhf/6UGUP2Mvgzc22TIe9f4TRTj4hkEk16E/e2ENSos5Dx2tnLtmTzped0Jc1VOTZ9c4o926r/01yXr55ExF7Rr/slAYx1j9BSYCsDbjgLSp3+NHsd1ncof/IayfWI+/90sYRmhNUKu1oDyMq61NZZ1tMPRfBoP5fgJ6yqnmDk5FLLrixNHGHWcmFo5FjB2L5UUJly5vuQrFuAvci+6qUThw5AxyLYmFWMYqyHnQLvW1HBmLUF3n64QorvqXwdu1actMsSiFMrrOUN6dixcgT+kVrQzms7NmWIxwnaOp4ido7XH9w/Lg5KsGu2khR81wbdlYnSb43QzhPB9ZFFm8E3ZbKpK94x+UGKKTPZHKiW7j/w1u6wkUwHPK5lCbbmk+TadVjBH5BojaqE+/f4ktGqxWqK7pOyQPm6dwg3FSL2HPgfxsUVlKvo+nMGX3h3G7iaSBYPLBL8Y9Qu0Y1HpyeMcK5yryb2RtXwjCyXn/VSREdY5z31A7sdKBbU3kEZy2cEipiUT4RXAItXEcZqUA/VHRILFf+HiypuA50KxPW0DtRkwmrE5AlifwmoQkxKPuCXV6peklY3tN+j3/f8=.";

            OffTheRecord.Protocol.Messages.RevealSignatureMessage rsm = OffTheRecord.Protocol.Messages.RevealSignatureMessage.Parse(input);

            Assert.AreEqual<int>(3, rsm.Version);
            Assert.AreEqual<uint>(698099990, rsm.SenderInstance);
            Assert.AreEqual<uint>(2364884977, rsm.ReceiverInstance);
            Assert.AreEqual<string>("0b49e5fc3683b14959b8ef4267dd6c99".ToUpperInvariant(), rsm.Key);
            Assert.AreEqual<string>("5d0f491752b9e08c74ba92f764d7b0c38d7197c886c866680d1a8f30957641afb2d991cb76dba5d355585fffa50650fd8cbe0cdcdb64c87bd7f84d14e3e21904935e84fded84352a2ce43c76b672ed993ce979dd0973554e4d9f5ce28f76eabff4d725ebe7913117b46bfec940631d63f414980ac0db8e02d2a77f8d1ec7759dca1ffc86b27d623effdd2c6119a13542aed680f232aeb5359675b4c3d17c1a0fe5f809eb2aa798393914b2eb8b134718759c985a391630762f9514265cb9bee42b16e02f722fbaa944e1c39031c8b626156318ab21e740bbd6d470662d41779fae10a2bbea5f076ed5a72d32c4a214caeb39437a762c5c813fa456b4339acecd996231c2768ea7889da3b5c7f70fcb8392ac1aeda4851f35c1b76562749be3743384f07d645166f04dd96caa4af78c7e50628a4cf6472a25bb8ffc35bbac245301cf2b99426db9a4f9369d563047e41a236aa13efdfe24b46ab15aa2bba4ec903e6e9dc20dc548bd873e07f1b145652afa3e9cc197de1dc6ee26920583cb04bf18f50bb46351e9c9e31c2b9cabc9bd91b57c230b25e7fd549111d639cf7d40eec74a05b537904672d9c122a62513e115c022d5c4719a9403f5474482c57fe1e2ca9b80e742b13d6d03b519309ab13902589f".ToUpperInvariant(), rsm.EncryptedSignature);
            Assert.AreEqual<string>("c26a1093128fb825d5ea97a4958ded37e8f7fdff".ToUpperInvariant(), rsm.MAC);
        }

        /// <summary>
        /// Tests the parsing of a Signature Message.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitParse, OtrTestCategories.Core)]
        public void SignatureMessageParseTest01()
        {
            string input = "?OTR:AAMSjPVH8SmcKRYAAAHSHKlFfK//TonP5D9qOJiHhyQkEObTaiHFa/AGGeUfSYzYD1PpqchG0yrHENzeOpkAnlsJwxJMlPJt/WsTNq+Pz+7bZmNt01LVozM/hIzU8OIHVSNlwEV1PAY9M0mi0QnNIBmHVyQ3DG9e0QZivVwD6Mjd+Qp9sGD/eSy2BYwCWlX1m8AC6k+i97ajl/Ldg3Cl25FpDbxpXy5uSUhl2tXSKSRjwBCK+wMKMeqrhiIRFFghwStxXTN3VurltsfOH0/FexpsVJQ8+vxBWOvSqDKEYcs3AEYHgwNz6nihKnueust2+flj52V3KNIY5G0qwIpoOEJaIQ53X/KScN6ZkNmD5qIK+Z1OgNoXmtS/Bpv879v/uNhFt5asPcmNw4LZ2i3OrzqwIKu2SsmxenXwZ6e4kz8XSzE0aL3K07qEGEzJ+kn82nYV3NpwDirWMgLq4A5Fc3uIYJAvBCOD+7mqIi9XgfJojvk8q7sqFOogFlkJwN/98saNoGBIkUMfGzFlMclLjuOGvBzztekNW7HNkWEd7tzXDiydCMpUUc2lQKE+pnCSz/2O5DnakWGCA2CFj7y9XoOvft913ENSm+q2NxDY4+vjrsbmeQZFj1Cr3rIXMiuMBB73BRnamNYdAYZnaDr21M95J0Yk.";

            OffTheRecord.Protocol.Messages.SignatureMessage sm = OffTheRecord.Protocol.Messages.SignatureMessage.Parse(input);

            Assert.AreEqual<int>(3, sm.Version);
            Assert.AreEqual<uint>(2364884977, sm.SenderInstance);
            Assert.AreEqual<uint>(698099990, sm.ReceiverInstance);
            Assert.AreEqual<string>("1ca9457cafff4e89cfe43f6a38988787242410e6d36a21c56bf00619e51f498cd80f53e9a9c846d32ac710dcde3a99009e5b09c3124c94f26dfd6b1336af8fcfeedb66636dd352d5a3333f848cd4f0e207552365c045753c063d3349a2d109cd2019875724370c6f5ed10662bd5c03e8c8ddf90a7db060ff792cb6058c025a55f59bc002ea4fa2f7b6a397f2dd8370a5db91690dbc695f2e6e494865dad5d2292463c0108afb030a31eaab862211145821c12b715d337756eae5b6c7ce1f4fc57b1a6c54943cfafc4158ebd2a8328461cb37004607830373ea78a12a7b9ebacb76f9f963e7657728d218e46d2ac08a6838425a210e775ff29270de9990d983e6a20af99d4e80da179ad4bf069bfcefdbffb8d845b796ac3dc98dc382d9da2dceaf3ab020abb64ac9b17a75f067a7b8933f174b313468bdcad3ba84184cc9fa49fcda7615dcda700e2ad63202eae00e45737b8860902f042383fbb9aa222f5781f2688ef93cabbb2a14ea20165909c0dffdf2c68da0604891431f1b316531c94b8ee386bc1cf3b5e90d5bb1cd91611deedcd70e2c9d08ca5451cda540a13ea67092cffd8ee439da9161820360858fbcbd5e83af7edf75dc43529beab63710d8e3ebe3aec6e67906458f50abdeb217322b8c04".ToUpperInvariant(), sm.EncryptedSignature);
            Assert.AreEqual<string>("1ef70519da98d61d018667683af6d4cf79274624".ToUpperInvariant(), sm.MAC);
        }

        /// <summary>
        /// Create a UserState object, and do more...
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core)]
        public void CreateUserState()
        {
            // Create userstate..
            UserState userstate = new UserState();

            Assert.IsNotNull(userstate, "Unable to initialize a userstate object.");

            string filename = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Files\otr.private_key");
            privkeys privkeys = ParseOtrPrivateKeyFile.Deserialize(filename);

            // Read keys..
            ////otrl_privkey_read(userstate, privkeyfilename);

            // To read stored instance tags:
            ////otrl_instag_read(userstate, instagfilename);

            // To read stored fingerprints:
            ////otrl_privkey_read_fingerprints(userstate, fingerprintfilename,
            ////    add_app_info, add_app_info_data);
        }
    }
}
