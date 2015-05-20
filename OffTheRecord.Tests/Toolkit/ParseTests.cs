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

            Program.Parse(input);

            var output = consoleOutput.GetOuput();

            output.Should().Be(ToolkitResultResource.otr_parse_exe);
        }
    }
}