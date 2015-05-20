using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Resources;

namespace OffTheRecord.Tests.Protocol.Utils
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void Parse_validate_that_DecodeFromBase64_returns_correct_start_value()
        {
            // Arrange
            const int amount = 3;
            int start;
            string message = "".PadRight(amount) + OtrStrings.OtrHeader;

            // Act
            OffTheRecord.Protocol.Utils.Parse.DecodeFromBase64(message, out start);

            // Assert
            start.Should().Be(amount + OtrStrings.OtrHeader.Length);
        }

        [TestMethod]
        public void Parse_validate_that_DecodeFromBase64_returns_null_when_Otr_header_is_not_found()
        {
            // Arrange
            const int amount = 3;
            int start;
            string message = "".PadRight(amount) ;

            // Act
            var value = OffTheRecord.Protocol.Utils.Parse.DecodeFromBase64(message, out start);

            // Assert
            value.Should().BeNull();
            start.Should().Be(-1);
        }

        [TestMethod]
        public void Parse_UInt32_with_incorrect_data_should_throw_exception()
        {
            // Arrange
            int start = 0;
            Action callingWithIncorrectData = () => OffTheRecord.Protocol.Utils.Parse.ReadInt32(new byte[] { 0x00 }, ref start);

            // Act & Assert
            callingWithIncorrectData.ShouldThrow<Exception>();
        }

        [TestMethod]
        public void Parse_UInt32_should_return_correct_result()
        {
            // Arrange
            int start = 0;

            // Act
            var value = OffTheRecord.Protocol.Utils.Parse.ReadInt32(new byte[] { 0x00, 0x00, 0x00, 0x01 }, ref start);

            // Assert
            value.Should().Be(1);
            start.Should().Be(4);
        }

        [TestMethod]
        public void Parse_UInt64_with_incorrect_data_should_throw_exception()
        {
            // Arrange
            int start = 0;
            Action callingWithIncorrectData = () => OffTheRecord.Protocol.Utils.Parse.ReadInt64(new byte[] { 0x00 }, ref start);

            // Act & Assert
            callingWithIncorrectData.ShouldThrow<Exception>();
        }

        [TestMethod]
        public void Parse_UInt64_should_return_correct_result()
        {
            // Arrange
            int start = 0;

            // Act
            var value = OffTheRecord.Protocol.Utils.Parse.ReadInt64(new byte[] { 0x05, 0x01, 0x02, 0x01, 0x01, 0x01, 0x02, 0x03 }, ref start);

            // Assert
            value.Should().Be(83952131);
            start.Should().Be(8);
        }

        [TestMethod]
        public void Parse_Raw_should_return_correct_result()
        {
            // Arrange
            int start = 0;

            // Act
            var value = OffTheRecord.Protocol.Utils.Parse.ReadRaw(new byte[] { 0x41, 0x41 }, ref start, 2);

            // Assert
            value.Should().Be("4141");
            start.Should().Be(2);
        }

        [TestMethod]
        public void Parse_Mpi_should_return_correct_result()
        {
            // Arrange
            int start = 0;

            // Act
            var value = OffTheRecord.Protocol.Utils.Parse.ReadMpi(new byte[] { 0x00, 0x00, 0x00, 0x02, 0x01, 0x01 }, ref start);

            // Assert
            value.Should().Be("0101");

            // TODO: not sure if this is correct! start should be the length of unit32 (4) + the value that is contained in the uint stored in the header of the raw data.
            start.Should().Be(4); 
        }
    }
}
