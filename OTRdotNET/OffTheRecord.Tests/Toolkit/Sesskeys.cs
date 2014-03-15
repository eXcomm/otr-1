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

using OffTheRecord.Toolkit.Sesskeys;

namespace OffTheRecord.Tests.Toolkit
{
    #region Namespaces
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OffTheRecord.Tests.Helper;
    #endregion

    /// <summary>
    /// Test the Toolkit.Sesskeys program.
    /// </summary>
    [TestClass]
    public class Sesskeys
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Unit tests
        /// <summary>
        /// Test the otr_sesskeys.exe application.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitSesskey)]
        public void TestToolkitSesskeys()
        {
            // Reference app to get it build and copied to output folder.
            OffTheRecord.Toolkit.Sesskeys.Program app = new Program();

            string filename = "otr_sesskeys.exe";

            string expectedResult = @"Wearethehighendofthiskeyexchange.Ourpublickey:0B1BE99FD638D2B634F9825F753FF7F2213AE7207A390B5DF3B685A8516D63D49C3BCEEB826C1CD09EB030430772193B82F1F4AB01C77E38B7EFF100C0FB296BD1D6148BD205FDCE3A2EC33EF9C3413EB06D1F413D52AD0747B9273783F7EE88435498B5774967DA987CE10E7A2CEC72CEECC8F95CEAF92EDF82B3E0F69FAA87DE5EB4748325F82F0BC43F24984B5AF2C9D3043D9871C3C952B22A5B292CDEAD6A67CAA62C0196745ED608A6AAF8797FE5801F0506B8F8AA5F431DC583EA584A8Sessionid:8FDEF45085A911525E8C408C5F9A3DB1C1104CB1SendingAESkey:3ECEFA2E6EA280C9EBCA91E6E37F2B60SendingMACkey:2829522E80354BAC5BE5DE648116E48B665C7D6CReceivingAESkey:8863A4479AE2857FB9BE657E3B7E37C4ReceivingMACkey:A43167D308BA9DE0127F3124A55BEA9A608C10C4";

            try
            {
                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = Path.Combine(location, filename);
                p.StartInfo.Arguments = "48BFDA215C31A9F0B226B3DB11F862450A0F30DA 64bfb577c9591b3dbb6b697599f572ce7d1ffc9d";
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.RedirectStandardOutput = true;
                bool started = p.Start();

                if (!started)
                {
                    Assert.Fail("Fail to start application.");
                }

                string result = p.StandardOutput.ReadToEnd();

                /* remove whitespaces, tabs, newlines for easy comparison */
                result = result.Replace(" ", string.Empty);
                result = result.Replace("\t", string.Empty);
                result = result.Replace("\n", string.Empty);
                result = result.Replace("\r", string.Empty);

                p.WaitForExit();
                int exitcode = p.ExitCode;
                p.Close();

                Assert.AreEqual<int>(0, exitcode);
                Assert.AreEqual<string>(expectedResult, result);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred starting process", ex);
                Assert.Fail("Error occurred starting process: {0}", ex);
            }
        }
        #endregion
    }
}
