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

using System.Linq;
using System.Security.Cryptography;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Tests.Helper;
using OffTheRecord.Tools;

namespace OffTheRecord.Tests
{
    [TestClass]
    public class DigitalSignatureAlgorithmTests
    {
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.Core, OtrTestCategories.Encryption)]
        public void DigitalSignatureAlgorithm_compare_parameters_generation_with_original_Pidgin_OffTheRecord_data()
        {
            // Arrange
            const string p = "AEC0FBB4CEA96EF8BDD0E91D1BA2F6641B6535CBDA8D739CC2898FE7B472865AB60AD2B1BAA2368603C7439E63BC2F2F33D422E70173F70DB738DF5979EAEAF3CAC343CBF711960E16786703C80DF0734D8330DC955DA84B521DAB5C729202F1244D805E6BF2CC7A7142CAD74BE5FFFC14B9CCB6CABB7DB10A8F2DDB4E82383F";
            const string q = "A2A2BC20E2D94C44C63608479C79068CE7914EF3";
            const string g = "69B9FC5A73F3F6EA3A86F8FA3A203F42DACDC3A1516002025E5765A9DCB975F348ACBBA2116230E19CE3FC5256546FD168A2940809BDA8655771967E9CD90AF44D2C20F97F448494213A775E23607F33C255A9A74E2A5FC7B4D50BAD024D7EFAC282E67332D51A5F69239011FE058D7E75E97A788FBD5B3BAD796B2C6D8C6C3E";
            const string y = "9931144F3059D92FCB2AAC03B130DAE43ED1EF30AA2F0E670C3974C3E80C7110D1A60210F92479D7F640C20E1F16E01B4A72FF8D45443B01EBE2D67DF49791CAC6191B159AC39446EB6A2EA597B6B678CC3157AECEAB12A804CF0772068A942EC819138EDD6005620FE746522FF408BBC8211ABD9D6016AA46EEC87F3F04CFA4";
            const string x = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA"; /* private key */

            // Act
            var param = new DSAParameters();
            param.X = General.StringToByteArray(x);
            param.P = General.StringToByteArray(p);
            param.Q = General.StringToByteArray(q);
            param.G = General.StringToByteArray(g);
            param.Y = General.StringToByteArray(y);

            var dsa = new DSACryptoServiceProvider(1024);
            dsa.ImportParameters(param);
            var output = dsa.ExportParameters(true);

            // Assert
            param.X.SequenceEqual(output.X).Should().BeTrue();
            param.P.SequenceEqual(output.P).Should().BeTrue();
            param.Q.SequenceEqual(output.Q).Should().BeTrue();
            param.G.SequenceEqual(output.G).Should().BeTrue();
            param.Y.SequenceEqual(output.Y).Should().BeTrue();
        }
    }
}