
namespace OffTheRecord.Tests.Toolkit
{
    #region Namespaces
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OffTheRecord.Tests.Helper;
    #endregion

    /// <summary>
    /// Test the Toolkit.Sesskeys program.
    /// </summary>
    [TestClass]
    public class Sesskeys
    {
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitSesskey)]
        public void TestToolkitSesskeys()
        {
            /*
            $ ./otr_sesskeys.exe 48BFDA215C31A9F0B226B3DB11F862450A0F30DA 64bfb577c9591b3dbb6b697599f572ce7d1ffc9d

            We are the high end of this key exchange.

            Our public key: b1be99fd638d2b634f9825f753ff7f2213ae7207a390b5df3b685a8516d63d49c3bceeb826c1cd09eb030430772193b82f1f4ab01c77e38b7eff100c0fb296bd1d6148bd205fdce3a2ec33ef9c3413eb06d1f413d52ad0747b9273783f7ee88435498b5774967da987ce10e7a2cec72ceecc8f95ceaf92edf82b3e0f69faa87de5eb4748325f82f0bc43f24984b5af2c9d3043d9871c3c952b22a5b292cdead6a67caa62c0196745ed608a6aaf8797fe5801f0506b8f8aa5f431dc583ea584a8

            Session id: 8fdef45085a911525e8c408c5f9a3db1c1104cb1

            Sending   AES key: 3ecefa2e6ea280c9ebca91e6e37f2b60
            Sending   MAC key: 2829522e80354bac5be5de648116e48b665c7d6c
            Receiving AES key: 8863a4479ae2857fb9be657e3b7e37c4
            Receiving MAC key: a43167d308ba9de0127f3124a55bea9a608c10c4
            */

            string expectedResult =
                @"
We are the high end of this key exchange.

Our public key: b1be99fd638d2b634f9825f753ff7f2213ae7207a390b5df3b685a8516d63d49c3bceeb826c1cd09eb030430772193b82f1f4ab01c77e38b7eff100c0fb296bd1d6148bd205fdce3a2ec33ef9c3413eb06d1f413d52ad0747b9273783f7ee88435498b5774967da987ce10e7a2cec72ceecc8f95ceaf92edf82b3e0f69faa87de5eb4748325f82f0bc43f24984b5af2c9d3043d9871c3c952b22a5b292cdead6a67caa62c0196745ed608a6aaf8797fe5801f0506b8f8aa5f431dc583ea584a8

Session id: 8fdef45085a911525e8c408c5f9a3db1c1104cb1

Sending   AES key: 3ecefa2e6ea280c9ebca91e6e37f2b60
Sending   MAC key: 2829522e80354bac5be5de648116e48b665c7d6c
Receiving AES key: 8863a4479ae2857fb9be657e3b7e37c4
Receiving MAC key: a43167d308ba9de0127f3124a55bea9a608c10c4";

            /* execute program */
            //privkey of "alice@domain.com"
            //test123_4	testuser2@irc.freenode.net	prpl-irc	64bfb577c9591b3dbb6b697599f572ce7d1ffc9d	smp
            string[] args = new string[] { "48BFDA215C31A9F0B226B3DB11F862450A0F30DA", "64bfb577c9591b3dbb6b697599f572ce7d1ffc9d" };

            string result = string.Empty;

            OffTheRecord.Toolkit.Sesskeys.Program.Main(args);

            Assert.AreEqual<string>(expectedResult, result);
        }
    }
}
