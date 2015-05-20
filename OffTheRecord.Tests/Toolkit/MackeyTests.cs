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

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Tests.Helper;
using OffTheRecord.Toolkit.Mackey;

namespace OffTheRecord.Tests.Toolkit
{
    [TestClass]
    public class MackeyTests
    {
        [TestMethod]
        public void MacKey_Toolkit_validate_with_default_example_set()
        {
            var consoleOutput = new ConsoleOutput();

            const string arguments = "8863A4479AE2857FB9BE657E3B7E37C4";

            Program.Main(new string[] {arguments});

            var output = consoleOutput.GetOuput();

            output.Should().Be(ToolkitResultResource.otr_mackey_exe);
        }
    }
}