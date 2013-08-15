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

namespace OffTheRecord.Protocol.Messages
{
    #region Namespaces
    using System;

    #endregion

    /// <summary>
    /// Handles OTR DataMessages.
    /// </summary>
    /// <example>
    /// ?OTR:AAMDSyvyQvLg7pcAAAAAAQAAAAEAAADAVoV88L+aKOU6X25AixfPKDvijKUVHhGdSFZlQpA5XepzoyEqA8ATbjYPwjE7FZApV87oUx+QQog39bJ2GA/zYqrag/xrRzLZfE9K3E7PmUaeUZijLCQA5hTYemzV/crv8SQiLbasDmNDKNi8X/XQuGSPhFD2/jtl13MElkbDWWYiQzX2Ck4lhsHGp0gsNLBhOwkwPGRzmWB+1ltRvb9XqhTuF6S83qGy9iM7pm3yT048awWY4FOG24dukbja1jbNAAAAAAAAAAEAAAANXtnheROJlgrrv2dCFmJ6bYB4YqCkGD2qjQM8s6q391HnAAAAAA==.
    /// Data Message:
    /// Version: 03
    /// Flags: 0
    /// Sender instance: 1261171266
    /// Receiver instance: 4074827415
    /// Sender keyid: 1
    /// Rcpt keyid: 1
    /// DH y: 56857cf0bf9a28e53a5f6e408b17cf283be28ca5151e119d4856654290395dea73
    /// a3212a03c0136e360fc2313b15902957cee8531f90428837f5b276180ff362aada83fc6b4732d97c
    /// 4f4adc4ecf99469e5198a32c2400e614d87a6cd5fdcaeff124222db6ac0e634328d8bc5ff5d0b864
    /// 8f8450f6fe3b65d773049646c35966224335f60a4e2586c1c6a7482c34b0613b09303c647399607e
    /// d65b51bdbf57aa14ee17a4bcdea1b2f6233ba66df24f4e3c6b0598e05386db876e91b8dad636cd
    /// Counter: 0000000000000001
    /// Encrypted message: 5ed9e1791389960aebbf674216
    /// MAC: 627a6d807862a0a4183daa8d033cb3aab7f751e7
    /// .
    /// </example>
    public sealed class DataMessage : BaseOTRMessage
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Public Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="DataMessage" /> class from being created.
        /// </summary>
        private DataMessage()
        {
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the original raw input data.
        /// </summary>
        public byte[] RawData { get; private set; }

        /// <summary>
        /// Gets the Off-the-Record protocol version.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Gets the sender instance.
        /// </summary>
        public uint SenderInstance { get; private set; }

        /// <summary>
        /// Gets the receiver instance.
        /// </summary>
        public uint ReceiverInstance { get; private set; }

        /// <summary>
        /// Gets the <see cref="DataMessage"/> Flags.
        /// </summary>
        public uint Flags { get; private set; }

        /// <summary>
        /// Gets the sender key id.
        /// </summary>
        public uint SenderKeyId { get; private set; }

        /// <summary>
        /// Gets the receiver key id.
        /// </summary>
        public uint ReceiverKeyId { get; private set; }

        /// <summary>
        /// Gets the Y part of the used encryption.
        /// </summary>
        public string Y { get; private set; }

        /// <summary>
        /// Gets the counter.
        /// </summary>
        public ulong Counter { get; private set; }

        /// <summary>
        /// Gets the encrypted message.
        /// </summary>
        public string EncryptedMessage { get; private set; }

        /// <summary>
        /// Gets the MAC of the EncryptedMessage.
        /// </summary>
        public string MAC { get; private set; }

        /// <summary>
        /// Gets the previous MAC keys.
        /// </summary>
        public string MACKeys { get; private set; }

        /// <summary>
        /// Gets the Off-the-Record <see cref="OTRMessageType" />.
        /// </summary>
        public override OTRMessageType MessageType
        {
            get { return OTRMessageType.DataMessage; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Parses the input string and returns a <see cref="DataMessage"/> object.
        /// </summary>
        /// <param name="msg">Input string.</param>
        /// <returns>a <see cref="DataMessage"/> object.</returns>
        public static DataMessage Parse(string msg)
        {
            try
            {
                DataMessage dm = new DataMessage();

                int start;

                dm.RawData = Utils.Parse.DecodeFromBase64(msg, out start);

                if (dm.RawData == null)
                {
                    throw new ArgumentException("msg is not a DataMessage (1)");
                }

                if (!(dm.RawData[0] == '\x00' && dm.RawData[1] == '\x01' && dm.RawData[2] == '\x03') &&
                    !(dm.RawData[0] == '\x00' && dm.RawData[1] == '\x03' && dm.RawData[2] == '\x03') &&
                    !(dm.RawData[0] == '\x00' && dm.RawData[1] == '\x02' && dm.RawData[2] == '\x03'))
                {
                    throw new ArgumentException("msg is not a DataMessage (2)");
                }

                dm.Version = dm.RawData[1];

                dm.SenderInstance = 0;
                dm.ReceiverInstance = 0;
                dm.Flags = 0;

                int offset = 3;

                if (dm.Version == 3)
                {
                    dm.SenderInstance = Utils.Parse.ReadInt32(dm.RawData, ref offset);
                    dm.ReceiverInstance = Utils.Parse.ReadInt32(dm.RawData, ref offset);
                }

                if (dm.Version == 2 || dm.Version == 3)
                {
                    dm.Flags = dm.RawData[offset];
                    offset += 1;
                }

                dm.SenderKeyId = Utils.Parse.ReadInt32(dm.RawData, ref offset);
                dm.ReceiverKeyId = Utils.Parse.ReadInt32(dm.RawData, ref offset);

                uint lenghtY = Utils.Parse.ReadInt32(dm.RawData, ref offset);

                dm.Y = Utils.Parse.ReadRaw(dm.RawData, ref offset, (int)lenghtY);

                dm.Counter = Utils.Parse.ReadInt64(dm.RawData, ref offset);

                // read Encrypted Message
                uint lengthOfEncryptedMessage = Utils.Parse.ReadInt32(dm.RawData, ref offset);
                dm.EncryptedMessage = Utils.Parse.ReadRaw(dm.RawData, ref offset, (int)lengthOfEncryptedMessage);

                dm.MAC = Utils.Parse.ReadRaw(dm.RawData, ref offset, 20);

                uint mackeyslen = Utils.Parse.ReadInt32(dm.RawData, ref offset);

                if (mackeyslen != 0)
                {
                    dm.MACKeys = Utils.Parse.ReadRaw(dm.RawData, ref offset, (int)mackeyslen);
                }

                Log.Debug(dm.Details());

                return dm;
            }
            catch (Exception ex)
            {
                Log.Debug("Failed to parse message.", ex);
                return null;
            }
        }

        /// <summary>
        /// Print the Off-the-Record message details to standard-out.
        /// </summary>
        public override void Print()
        {
            Console.WriteLine(this.Details());
        }
        #endregion

        #region Private methods
        private string Details()
        {
            return string.Format(
                "Data Message:" + Environment.NewLine +
                "\tVersion: {0}" + Environment.NewLine +
                "\tFlags: {1}" + Environment.NewLine +
                "\tSender instance: {2}" + Environment.NewLine +
                "\tReceiver instance: {3}" + Environment.NewLine +
                "\tSndr keyid: {4}" + Environment.NewLine +
                "\tRcpt keyid: {5}" + Environment.NewLine +
                "\tDH y: {6}" + Environment.NewLine +
                "\tCounter: {7}" + Environment.NewLine +
                "\tEncrypted Message: {8}" + Environment.NewLine +
                "\tMAC: {9}",
                this.Version,
                this.Flags,
                this.SenderInstance,
                this.ReceiverInstance,
                this.SenderKeyId,
                this.ReceiverKeyId,
                this.Y,
                this.Counter,
                this.EncryptedMessage,
                this.MAC);
        }
        #endregion
    }
}
