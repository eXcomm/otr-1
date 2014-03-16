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

using OffTheRecord.Toolkit.Remac;

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
    /// Test the Toolkit.Remac program.
    /// </summary>
    [TestClass]
    public class Remac
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Unit tests
        /// <summary>
        /// Test the otr_sesskeys.exe application.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitRemac)]
        public void TestToolkitRemac()
        {
            // Reference app to get it build and copied to output folder.
            OffTheRecord.Toolkit.Remac.Program app = new Program();

            string filename = "otr_remac.exe";

            string expectedResult = "inconclusive";

            try
            {
                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = Path.Combine(location, filename);
                p.StartInfo.Arguments = "-";
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
