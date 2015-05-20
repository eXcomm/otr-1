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
    /// Handles OTR DH Commit Message.
    /// </summary>
    /// <example>
    /// ?OTR:AAMC8uDulwAAAAAAAADEdJDE3Q/lG1LdKWVfWc6+NmWhTBvd06+VGvVbH19vhKmGeVZ16dpXsWfqXDFsRVxovU7pFLe0+5GGiTcc16gC681IOJfEsfm9JTpaFOr2XDsJQwHnIhFp8qSkvwfqEwx0FLelaSmI6ipNzlQCtzZ3Y7ihAAMZV3cEc05w0xwHqkIXLtn73+Fohp1RuLw2ubIFbFJRlZcteyK6VFfAN9HBv+TnvsSQ8iHSIBMQpfzZgT1A0IxrboAyg1xPu1XSA65G+HLfoQAAACC9FgJItQxwRnJCFYBGYjofLhZntfzxbHTusuV/5JrT2g==.
    /// D-H Commit Message:
    /// Version: 03
    /// Sender instance: 4074827415
    /// Receiver instance: 0
    /// Encrypted Key: 7490c4dd0fe51b52dd29655f59cebe3665a14c1bddd3af951af55b1f5
    /// f6f84a986795675e9da57b167ea5c316c455c68bd4ee914b7b4fb918689371cd7a802ebcd483897c
    /// 4b1f9bd253a5a14eaf65c3b094301e7221169f2a4a4bf07ea130c7414b7a5692988ea2a4dce5402b
    /// 7367763b8a1000319577704734e70d31c07aa42172ed9fbdfe168869d51b8bc36b9b2056c5251959
    /// 72d7b22ba5457c037d1c1bfe4e7bec490f221d2201310a5fcd9813d40d08c6b6e8032835c4fbb55d
    /// 203ae46f872dfa1
    /// Hashed Key: bd160248b50c70467242158046623a1f2e1667b5fcf16c74eeb2e57fe49ad3da
    /// .
    /// </example>
    public sealed class DhCommitMessage : BaseOTRMessage
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="DhCommitMessage"/> class from being created.
        /// </summary>
        private DhCommitMessage()
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
        /// Gets the encrypted message.
        /// </summary>
        public string EncryptedMessage { get; private set; }

        /// <summary>
        /// Gets the hash key.
        /// </summary>
        public string HashKey { get; private set; }

        /// <summary>
        /// Gets the Off-the-Record <see cref="OtrMessageType" />.
        /// </summary>
        public override OtrMessageType MessageType
        {
            get { return OtrMessageType.DhCommitMessage; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Parses the input string and returns a <see cref="DhCommitMessage"/> object.
        /// </summary>
        /// <param name="msg">Input string.</param>
        /// <returns>a <see cref="DhCommitMessage"/> object.</returns>
        public static DhCommitMessage Parse(string msg)
        {
            try
            {
                DhCommitMessage dhcm = new DhCommitMessage();

                int start;

                dhcm.RawData = Utils.Parse.DecodeFromBase64(msg, out start);

                if (dhcm.RawData == null)
                {
                    throw new ArgumentException("msg is not a DH Commit Message (1)");
                }

                dhcm.Version = dhcm.RawData[1];

                int offset = 3;

                if (dhcm.RawData[0] == '\x00' && dhcm.RawData[1] == '\x03' && dhcm.RawData[2] == '\x02')
                {
                    dhcm.SenderInstance = Utils.Parse.ReadInt32(dhcm.RawData, ref offset);
                    dhcm.ReceiverInstance = Utils.Parse.ReadInt32(dhcm.RawData, ref offset);
                }
                else if (dhcm.RawData[0] == '\x00' && dhcm.RawData[1] == '\x02' && dhcm.RawData[2] == '\x02')
                {
                    dhcm.SenderInstance = 0;
                    dhcm.ReceiverInstance = 0;
                }
                else
                {
                    throw new ArgumentException("msg is not a DH Commit Message (2)");
                }

                uint enckeylen = Utils.Parse.ReadInt32(dhcm.RawData, ref offset);
                dhcm.EncryptedMessage = Utils.Parse.ReadRaw(dhcm.RawData, ref offset, (int)enckeylen);

                uint haskeylen = Utils.Parse.ReadInt32(dhcm.RawData, ref offset);
                dhcm.HashKey = Utils.Parse.ReadRaw(dhcm.RawData, ref offset, (int)haskeylen);

                Log.Debug(dhcm.Details());

                return dhcm;
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
                "D-H Commit Message:" + Environment.NewLine +
                "\tVersion: {0}" + Environment.NewLine +
                "\tSender instance: {1}" + Environment.NewLine +
                "\tReceiver instance: {2}" + Environment.NewLine +
                "\tEncrypted Key: {3}" + Environment.NewLine +
                "\tHashed Key: {4}",
                this.Version,
                this.SenderInstance,
                this.ReceiverInstance,
                this.EncryptedMessage,
                this.HashKey);
        }
        #endregion
    }
}
