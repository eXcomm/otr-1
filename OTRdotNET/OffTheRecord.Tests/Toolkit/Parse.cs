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
    /// Test the Toolkit.Parse program.
    /// </summary>
    [TestClass]
    public class Parse
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        /// <summary>
        /// Test the parsing of a DataMessage.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitParse)]
        public void TestToolkitParseUsingDataMessage()
        {
            string filename = "otr_parse.exe";
            string input = "?OTR:AAMDSyvyQvLg7pcAAAAAAQAAAAEAAADAVoV88L+aKOU6X25AixfPKDvijKUVHhGdSFZlQpA5XepzoyEqA8ATbjYPwjE7FZApV87oUx+QQog39bJ2GA/zYqrag/xrRzLZfE9K3E7PmUaeUZijLCQA5hTYemzV/crv8SQiLbasDmNDKNi8X/XQuGSPhFD2/jtl13MElkbDWWYiQzX2Ck4lhsHGp0gsNLBhOwkwPGRzmWB+1ltRvb9XqhTuF6S83qGy9iM7pm3yT048awWY4FOG24dukbja1jbNAAAAAAAAAAEAAAANXtnheROJlgrrv2dCFmJ6bYB4YqCkGD2qjQM8s6q391HnAAAAAA==.";

            try
            {
                ////Toolkit.Parse program = new Parse();

                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = Path.Combine(location, filename);
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                bool started = p.Start();

                if (!started)
                {
                    Assert.Fail("Fail to start application.");
                }

                using (StreamWriter s = p.StandardInput)
                {
                    s.WriteLine(input);
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
                Assert.AreEqual<string>("DataMessage:Version:3Flags:0Senderinstance:1261171266Receiverinstance:4074827415Sndrkeyid:1Rcptkeyid:1DHy:56857CF0BF9A28E53A5F6E408B17CF283BE28CA5151E119D4856654290395DEA73A3212A03C0136E360FC2313B15902957CEE8531F90428837F5B276180FF362AADA83FC6B4732D97C4F4ADC4ECF99469E5198A32C2400E614D87A6CD5FDCAEFF124222DB6AC0E634328D8BC5FF5D0B8648F8450F6FE3B65D773049646C35966224335F60A4E2586C1C6A7482C34B0613B09303C647399607ED65B51BDBF57AA14EE17A4BCDEA1B2F6233BA66DF24F4E3C6B0598E05386DB876E91B8DAD636CDCounter:1EncryptedMessage:5ED9E1791389960AEBBF674216MAC:627A6D807862A0A4183DAA8D033CB3AAB7F751E7", result);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred starting process", ex);
                Assert.Fail("Error occurred starting process: {0}", ex);
            }
        }
    }
}
