using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Tests.Helper;
using OffTheRecord.Toolkit.Parse;

namespace OffTheRecord.Tests.Toolkit
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void Parse_Toolkit_validate_with_default_example_set()
        {
            var consoleOutput = new ConsoleOutput();

            const string input =
                "?OTR:AAMDSyvyQvLg7pcAAAAAAQAAAAEAAADAVoV88L+aKOU6X25AixfPKDvijKUVHhGdSFZlQpA5XepzoyEqA8ATbjYPwjE7FZApV87oUx+QQog39bJ2GA/zYqrag/xrRzLZfE9K3E7PmUaeUZijLCQA5hTYemzV/crv8SQiLbasDmNDKNi8X/XQuGSPhFD2/jtl13MElkbDWWYiQzX2Ck4lhsHGp0gsNLBhOwkwPGRzmWB+1ltRvb9XqhTuF6S83qGy9iM7pm3yT048awWY4FOG24dukbja1jbNAAAAAAAAAAEAAAANXtnheROJlgrrv2dCFmJ6bYB4YqCkGD2qjQM8s6q391HnAAAAAA==.";

            string expectedOutput =
                "Data Message:" + Environment.NewLine +
                "	Version: 3" + Environment.NewLine +
                "	Flags: 0" + Environment.NewLine +
                "	Sender instance: 1261171266" + Environment.NewLine +
                "	Receiver instance: 4074827415" + Environment.NewLine +
                "	Sndr keyid: 1" + Environment.NewLine +
                "	Rcpt keyid: 1" + Environment.NewLine +
                "	DH y: 56857CF0BF9A28E53A5F6E408B17CF283BE28CA5151E119D4856654290395DEA73A3212A03C0136E360FC2313B15902957CEE8531F90428837F5B276180FF362AADA83FC6B4732D97C4F4ADC4ECF99469E5198A32C2400E614D87A6CD5FDCAEFF124222DB6AC0E634328D8BC5FF5D0B8648F8450F6FE3B65D773049646C35966224335F60A4E2586C1C6A7482C34B0613B09303C647399607ED65B51BDBF57AA14EE17A4BCDEA1B2F6233BA66DF24F4E3C6B0598E05386DB876E91B8DAD636CD" + Environment.NewLine +
                "	Counter: 1" + Environment.NewLine +
                "	Encrypted Message: 5ED9E1791389960AEBBF674216" + Environment.NewLine +
                "	MAC: 627A6D807862A0A4183DAA8D033CB3AAB7F751E7" + Environment.NewLine;

            Program.Parse(input);

            var output = consoleOutput.GetOuput();

            output.Should().Be(expectedOutput);
        }
    }
}