using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Tests.Helper;
using OffTheRecord.Toolkit.Parse;

namespace OffTheRecord.Tests.Toolkit
{
    [TestClass]
    public class Parse
    {
        /// <summary>
        ///     Test the parsing of a DataMessage.
        /// </summary>
        [TestMethod]
        [OtrTestCategory(OtrTestCategories.ToolkitParse)]
        public void Parse_Toolkit_validate_with_default_example_set()
        {
            // Reference app to get it build and copied to output folder.
            var app = new Program();

            const string filename = "otr_parse.exe";
            const string input =
                "?OTR:AAMDSyvyQvLg7pcAAAAAAQAAAAEAAADAVoV88L+aKOU6X25AixfPKDvijKUVHhGdSFZlQpA5XepzoyEqA8ATbjYPwjE7FZApV87oUx+QQog39bJ2GA/zYqrag/xrRzLZfE9K3E7PmUaeUZijLCQA5hTYemzV/crv8SQiLbasDmNDKNi8X/XQuGSPhFD2/jtl13MElkbDWWYiQzX2Ck4lhsHGp0gsNLBhOwkwPGRzmWB+1ltRvb9XqhTuF6S83qGy9iM7pm3yT048awWY4FOG24dukbja1jbNAAAAAAAAAAEAAAANXtnheROJlgrrv2dCFmJ6bYB4YqCkGD2qjQM8s6q391HnAAAAAA==.";

            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var result = ToolkitRunner.Run(location, filename, null, input);

            // Assert
            result.Item1.Should().Be(0);
            result.Item2.Should().Be(ToolkitResultResource.otr_parse_exe);
        }
    }
}