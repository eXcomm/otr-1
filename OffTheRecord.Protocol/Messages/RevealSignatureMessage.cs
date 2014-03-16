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
    /// Handles OTR Reveal Signature Message.
    /// </summary>
    /// <example>
    /// ?OTR:AAMRKZwpFoz1R/EAAAAQC0nl/DaDsUlZuO9CZ91smQAAAdJdD0kXUrngjHS6kvdk17DDjXGXyIbIZmgNGo8wlXZBr7LZkct226XTVVhf/6UGUP2Mvgzc22TIe9f4TRTj4hkEk16E/e2ENSos5Dx2tnLtmTzped0Jc1VOTZ9c4o926r/01yXr55ExF7Rr/slAYx1j9BSYCsDbjgLSp3+NHsd1ncof/IayfWI+/90sYRmhNUKu1oDyMq61NZZ1tMPRfBoP5fgJ6yqnmDk5FLLrixNHGHWcmFo5FjB2L5UUJly5vuQrFuAvci+6qUThw5AxyLYmFWMYqyHnQLvW1HBmLUF3n64QorvqXwdu1actMsSiFMrrOUN6dixcgT+kVrQzms7NmWIxwnaOp4ido7XH9w/Lg5KsGu2khR81wbdlYnSb43QzhPB9ZFFm8E3ZbKpK94x+UGKKTPZHKiW7j/w1u6wkUwHPK5lCbbmk+TadVjBH5BojaqE+/f4ktGqxWqK7pOyQPm6dwg3FSL2HPgfxsUVlKvo+nMGX3h3G7iaSBYPLBL8Y9Qu0Y1HpyeMcK5yryb2RtXwjCyXn/VSREdY5z31A7sdKBbU3kEZy2cEipiUT4RXAItXEcZqUA/VHRILFf+HiypuA50KxPW0DtRkwmrE5AlifwmoQkxKPuCXV6peklY3tN+j3/f8=.
    /// Reveal Signature Message:
    ///        Version: 03
    ///        Sender instance: 698099990
    ///        Receiver instance: 2364884977
    ///        Key: 0b49e5fc3683b14959b8ef4267dd6c99
    ///        Encrypted Signature: 5d0f491752b9e08c74ba92f764d7b0c38d7197c886c866680d1
    /// a8f30957641afb2d991cb76dba5d355585fffa50650fd8cbe0cdcdb64c87bd7f84d14e3e21904935
    /// e84fded84352a2ce43c76b672ed993ce979dd0973554e4d9f5ce28f76eabff4d725ebe7913117b46
    /// bfec940631d63f414980ac0db8e02d2a77f8d1ec7759dca1ffc86b27d623effdd2c6119a13542aed
    /// 680f232aeb5359675b4c3d17c1a0fe5f809eb2aa798393914b2eb8b134718759c985a391630762f9
    /// 514265cb9bee42b16e02f722fbaa944e1c39031c8b626156318ab21e740bbd6d470662d41779fae1
    /// 0a2bbea5f076ed5a72d32c4a214caeb39437a762c5c813fa456b4339acecd996231c2768ea7889da
    /// 3b5c7f70fcb8392ac1aeda4851f35c1b76562749be3743384f07d645166f04dd96caa4af78c7e506
    /// 28a4cf6472a25bb8ffc35bbac245301cf2b99426db9a4f9369d563047e41a236aa13efdfe24b46ab
    /// 15aa2bba4ec903e6e9dc20dc548bd873e07f1b145652afa3e9cc197de1dc6ee26920583cb04bf18f
    /// 50bb46351e9c9e31c2b9cabc9bd91b57c230b25e7fd549111d639cf7d40eec74a05b537904672d9c
    /// 122a62513e115c022d5c4719a9403f5474482c57fe1e2ca9b80e742b13d6d03b519309ab13902589
    /// f
    ///        MAC: c26a1093128fb825d5ea97a4958ded37e8f7fdff
    /// .
    /// </example>
    public sealed class RevealSignatureMessage : BaseOTRMessage
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="RevealSignatureMessage"/> class from being created.
        /// </summary>
        private RevealSignatureMessage()
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
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the encrypted signature.
        /// </summary>
        public string EncryptedSignature { get; private set; }

        /// <summary>
        /// Gets the MAC.
        /// </summary>
        public string MAC { get; private set; }

        /// <summary>
        /// Gets the Off-the-Record <see cref="OTRMessageType" />.
        /// </summary>
        public override OTRMessageType MessageType
        {
            get { return OTRMessageType.RevealSignatureMessage; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Parses the input string and returns a <see cref="RevealSignatureMessage"/> object.
        /// </summary>
        /// <param name="msg">Input string.</param>
        /// <returns>a <see cref="RevealSignatureMessage"/> object.</returns>
        public static RevealSignatureMessage Parse(string msg)
        {
            try
            {
                RevealSignatureMessage rsm = new RevealSignatureMessage();

                int start;

                rsm.RawData = Utils.Parse.DecodeFromBase64(msg, out start);

                if (rsm.RawData == null)
                {
                    throw new ArgumentException("msg is not a RevealSignatureMessage (1)");
                }

                rsm.Version = rsm.RawData[1];

                int offset = 3;

                if (rsm.RawData[0] == '\x00' && rsm.RawData[1] == '\x03' && rsm.RawData[2] == '\x11')
                {
                    rsm.SenderInstance = Utils.Parse.ReadInt32(rsm.RawData, ref offset);
                    rsm.ReceiverInstance = Utils.Parse.ReadInt32(rsm.RawData, ref offset);
                }
                else if (rsm.RawData[0] == '\x00' && rsm.RawData[1] == '\x02' && rsm.RawData[2] == '\x11')
                {
                    rsm.SenderInstance = 0;
                    rsm.ReceiverInstance = 0;
                }
                else
                {
                    throw new ArgumentException("msg is not a Reveal Signature Message (2)");
                }

                uint keylen = Utils.Parse.ReadInt32(rsm.RawData, ref offset);

                rsm.Key = Utils.Parse.ReadRaw(rsm.RawData, ref offset, (int)keylen);

                uint signlen = Utils.Parse.ReadInt32(rsm.RawData, ref offset);

                rsm.EncryptedSignature = Utils.Parse.ReadRaw(rsm.RawData, ref offset, (int)signlen);

                rsm.MAC = Utils.Parse.ReadRaw(rsm.RawData, ref offset, 20);

                return rsm;
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
                "Reveal Signature Message:" + Environment.NewLine +
                "\tVersion: {0}" + Environment.NewLine +
                "\tSender instance: {1}" + Environment.NewLine +
                "\tReceiver instance: {2}" + Environment.NewLine +
                "\tKey: {3}" + Environment.NewLine +
                "\tEncrypted Signature: {4}" + Environment.NewLine +
                "\tMAC: {5}",
                this.Version,
                this.SenderInstance,
                this.ReceiverInstance,
                this.Key,
                this.EncryptedSignature,
                this.MAC);
        }
        #endregion
    }
}
