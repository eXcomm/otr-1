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
    /// Handles OTR Signature Message.
    /// </summary>
    /// <example>
    /// ?OTR:AAMSjPVH8SmcKRYAAAHSHKlFfK//TonP5D9qOJiHhyQkEObTaiHFa/AGGeUfSYzYD1PpqchG0yrHENzeOpkAnlsJwxJMlPJt/WsTNq+Pz+7bZmNt01LVozM/hIzU8OIHVSNlwEV1PAY9M0mi0QnNIBmHVyQ3DG9e0QZivVwD6Mjd+Qp9sGD/eSy2BYwCWlX1m8AC6k+i97ajl/Ldg3Cl25FpDbxpXy5uSUhl2tXSKSRjwBCK+wMKMeqrhiIRFFghwStxXTN3VurltsfOH0/FexpsVJQ8+vxBWOvSqDKEYcs3AEYHgwNz6nihKnueust2+flj52V3KNIY5G0qwIpoOEJaIQ53X/KScN6ZkNmD5qIK+Z1OgNoXmtS/Bpv879v/uNhFt5asPcmNw4LZ2i3OrzqwIKu2SsmxenXwZ6e4kz8XSzE0aL3K07qEGEzJ+kn82nYV3NpwDirWMgLq4A5Fc3uIYJAvBCOD+7mqIi9XgfJojvk8q7sqFOogFlkJwN/98saNoGBIkUMfGzFlMclLjuOGvBzztekNW7HNkWEd7tzXDiydCMpUUc2lQKE+pnCSz/2O5DnakWGCA2CFj7y9XoOvft913ENSm+q2NxDY4+vjrsbmeQZFj1Cr3rIXMiuMBB73BRnamNYdAYZnaDr21M95J0Yk.
    /// Signature Message:
    ///        Version: 03
    ///        Sender instance: 2364884977
    ///        Receiver instance: 698099990
    ///        Encrypted Signature: 1ca9457cafff4e89cfe43f6a38988787242410e6d36a21c56bf
    /// 00619e51f498cd80f53e9a9c846d32ac710dcde3a99009e5b09c3124c94f26dfd6b1336af8fcfeed
    /// b66636dd352d5a3333f848cd4f0e207552365c045753c063d3349a2d109cd2019875724370c6f5ed
    /// 10662bd5c03e8c8ddf90a7db060ff792cb6058c025a55f59bc002ea4fa2f7b6a397f2dd8370a5db9
    /// 1690dbc695f2e6e494865dad5d2292463c0108afb030a31eaab862211145821c12b715d337756eae
    /// 5b6c7ce1f4fc57b1a6c54943cfafc4158ebd2a8328461cb37004607830373ea78a12a7b9ebacb76f
    /// 9f963e7657728d218e46d2ac08a6838425a210e775ff29270de9990d983e6a20af99d4e80da179ad
    /// 4bf069bfcefdbffb8d845b796ac3dc98dc382d9da2dceaf3ab020abb64ac9b17a75f067a7b8933f1
    /// 74b313468bdcad3ba84184cc9fa49fcda7615dcda700e2ad63202eae00e45737b8860902f042383f
    /// bb9aa222f5781f2688ef93cabbb2a14ea20165909c0dffdf2c68da0604891431f1b316531c94b8ee
    /// 386bc1cf3b5e90d5bb1cd91611deedcd70e2c9d08ca5451cda540a13ea67092cffd8ee439da91618
    /// 20360858fbcbd5e83af7edf75dc43529beab63710d8e3ebe3aec6e67906458f50abdeb217322b8c0
    /// 4
    ///        MAC: 1ef70519da98d61d018667683af6d4cf79274624
    /// .
    /// </example>
    public sealed class SignatureMessage : BaseOTRMessage
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="SignatureMessage"/> class from being created.
        /// </summary>
        private SignatureMessage()
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
        /// Gets the encrypted signature.
        /// </summary>
        public string EncryptedSignature { get; private set; }

        /// <summary>
        /// Gets the MAC.
        /// </summary>
        public string MAC { get; private set; }

        /// <summary>
        /// Gets the Off-the-Record <see cref="OtrMessageType" />.
        /// </summary>
        public override OtrMessageType MessageType
        {
            get { return OtrMessageType.SignatureMessage; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Parses the input string and returns a <see cref="SignatureMessage"/> object.
        /// </summary>
        /// <param name="msg">Input string.</param>
        /// <returns>a <see cref="SignatureMessage"/> object.</returns>
        public static SignatureMessage Parse(string msg)
        {
            try
            {
                SignatureMessage sm = new SignatureMessage();

                int start;

                sm.RawData = Utils.Parse.DecodeFromBase64(msg, out start);

                if (sm.RawData == null)
                {
                    throw new ArgumentException("msg is not a RevealSignatureMessage (1)");
                }

                sm.Version = sm.RawData[1];

                int offset = 3;

                if (sm.RawData[0] == '\x00' && sm.RawData[1] == '\x03' && sm.RawData[2] == '\x12')
                {
                    sm.SenderInstance = Utils.Parse.ReadInt32(sm.RawData, ref offset);
                    sm.ReceiverInstance = Utils.Parse.ReadInt32(sm.RawData, ref offset);
                }
                else if (sm.RawData[0] == '\x00' && sm.RawData[1] == '\x02' && sm.RawData[2] == '\x12')
                {
                    sm.SenderInstance = 0;
                    sm.ReceiverInstance = 0;
                }
                else
                {
                    throw new ArgumentException("msg is not a Reveal Signature Message (2)");
                }

                uint signlen = Utils.Parse.ReadInt32(sm.RawData, ref offset);

                sm.EncryptedSignature = Utils.Parse.ReadRaw(sm.RawData, ref offset, (int)signlen);

                sm.MAC = Utils.Parse.ReadRaw(sm.RawData, ref offset, 20);

                return sm;
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
                "Signature Message:" + Environment.NewLine +
                "\tVersion: {0}" + Environment.NewLine +
                "\tSender instance: {1}" + Environment.NewLine +
                "\tReceiver instance: {2}" + Environment.NewLine +
                "\tEncrypted Signature: {3}" + Environment.NewLine +
                "\tMAC: {4}",
                this.Version,
                this.SenderInstance,
                this.ReceiverInstance,
                this.EncryptedSignature,
                this.MAC);
        }
        #endregion
    }
}
