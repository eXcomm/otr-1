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
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Tests.Helper;
using OffTheRecord.Toolkit.Sesskeys;

namespace OffTheRecord.Tests.Toolkit
{
    [TestClass]
    public class SesskeysTests
    {
        [TestMethod]
        public void Sesskeys_Toolkit_validate_with_default_example_set()
        {
            var consoleOutput = new ConsoleOutput();

            string[] arguments = {"48BFDA215C31A9F0B226B3DB11F862450A0F30DA", "64bfb577c9591b3dbb6b697599f572ce7d1ffc9d"};

            string expectedOutput =
                "We are the high end of this key exchange." + Environment.NewLine +
                "" + Environment.NewLine +
                "Our public key: 0B1BE99FD638D2B634F9825F753FF7F2213AE7207A390B5DF3B685A8516D63D49C3BCEEB826C1CD09EB030430772193B82F1F4AB01C77E38B7EFF100C0FB296BD1D6148BD205FDCE3A2EC33EF9C3413EB06D1F413D52AD0747B9273783F7EE88435498B5774967DA987CE10E7A2CEC72CEECC8F95CEAF92EDF82B3E0F69FAA87DE5EB4748325F82F0BC43F24984B5AF2C9D3043D9871C3C952B22A5B292CDEAD6A67CAA62C0196745ED608A6AAF8797FE5801F0506B8F8AA5F431DC583EA584A8" + Environment.NewLine +
                "" + Environment.NewLine +
                "Session id: 8FDEF45085A911525E8C408C5F9A3DB1C1104CB1" + Environment.NewLine +
                "" + Environment.NewLine +
                "Sending   AES key: 3ECEFA2E6EA280C9EBCA91E6E37F2B60" + Environment.NewLine +
                "Sending   MAC key: 2829522E80354BAC5BE5DE648116E48B665C7D6C" + Environment.NewLine +
                "Receiving AES key: 8863A4479AE2857FB9BE657E3B7E37C4" + Environment.NewLine +
                "Receiving MAC key: A43167D308BA9DE0127F3124A55BEA9A608C10C4" + Environment.NewLine +
                "" + Environment.NewLine;

            Program.Main(arguments);

            consoleOutput.GetOuput().Should().Be(expectedOutput);
        }
    }
}