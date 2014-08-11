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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Tests.Helper;
using OffTheRecord.Toolkit.Mackey;

namespace OffTheRecord.Tests.Toolkit
{
    [TestClass]
    public class MackeyTests
    {
        #region Fields

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        /// <summary>
        ///     Test the otr_mackey.exe application.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitMackey)]
        public void TestToolkitMackey()
        {
            // Reference app to get it build and copied to output folder.
            var app = new Program();

            const string filename = "otr_mackey.exe";

            const string expectedResult =
                @"AESkey:8863A4479AE2857FB9BE657E3B7E37C4MACkey:A43167D308BA9DE0127F3124A55BEA9A608C10C4";

            try
            {
                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = Path.Combine(location, filename);
                p.StartInfo.Arguments = "8863A4479AE2857FB9BE657E3B7E37C4";
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.RedirectStandardOutput = true;

                Assert.IsTrue(File.Exists((p.StartInfo.FileName)), "Filename not found {0}.", p.StartInfo.FileName);

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

                Assert.AreEqual(0, exitcode);
                Assert.AreEqual(expectedResult, result);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred starting process", ex);
                Assert.Fail("Error occurred starting process: {0}", ex);
            }
        }
    }
}