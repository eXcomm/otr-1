using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffTheRecord.Protocol;
using OffTheRecord.Protocol.Messages;

namespace OffTheRecord.Tests.Protocol.Messages
{
    [TestClass]
    public class DataMessageTests
    {
        [TestMethod]
        public void OffTheRecord_Validate_parsing_of_DataMessage()
        {
            const string input =
                "?OTR:AAMDSyvyQvLg7pcAAAAAAQAAAAEAAADAVoV88L+aKOU6X25AixfPKDvijKUVHhGdSFZlQpA5XepzoyEqA8ATbjYPwjE7FZApV87oUx+QQog39bJ2GA/zYqrag/xrRzLZfE9K3E7PmUaeUZijLCQA5hTYemzV/crv8SQiLbasDmNDKNi8X/XQuGSPhFD2/jtl13MElkbDWWYiQzX2Ck4lhsHGp0gsNLBhOwkwPGRzmWB+1ltRvb9XqhTuF6S83qGy9iM7pm3yT048awWY4FOG24dukbja1jbNAAAAAAAAAAEAAAANXtnheROJlgrrv2dCFmJ6bYB4YqCkGD2qjQM8s6q391HnAAAAAA==.";

            DataMessage dm = DataMessage.Parse(input);

            dm.MessageType.Should().Be(OtrMessageType.DataMessage);

            dm.Version.Should().Be(3);
            dm.Flags.Should().Be(0);
            dm.SenderInstance.Should().Be(1261171266);
            dm.ReceiverInstance.Should().Be(4074827415);
            dm.SenderKeyId.Should().Be(1);
            dm.ReceiverKeyId.Should().Be(1);
            dm.Y.Should()
                .Be(
                    "56857CF0BF9A28E53A5F6E408B17CF283BE28CA5151E119D4856654290395DEA73A3212A03C0136E360FC2313B15902957CEE8531F90428837F5B276180FF362AADA83FC6B4732D97C4F4ADC4ECF99469E5198A32C2400E614D87A6CD5FDCAEFF124222DB6AC0E634328D8BC5FF5D0B8648F8450F6FE3B65D773049646C35966224335F60A4E2586C1C6A7482C34B0613B09303C647399607ED65B51BDBF57AA14EE17A4BCDEA1B2F6233BA66DF24F4E3C6B0598E05386DB876E91B8DAD636CD");
            dm.Counter.Should().Be(1);
            dm.EncryptedMessage.Should().Be("5ED9E1791389960AEBBF674216");
            dm.Mac.Should().Be("627A6D807862A0A4183DAA8D033CB3AAB7F751E7");
        }
    
    }
}
