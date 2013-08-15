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
    /// Handles OTR D-H Key Message.
    /// </summary>
    /// <example>
    /// ?OTR:AAMKSyvyQvLg7pcAAADALSQOLMyMB7ZF8LeghEQw2HNeP+dnuWe+NAue2s+d1ZtnqVvzXhDJIDG8wdiIZ3NCOYcRRyn11sFEZPeRQ7AoIeh+cNmGccALb91T9HFoISs+/+LW+1jBjiJ9Wn2adtUcZmkukP60VSXF+PnSQGXQKMTXUlOM9QsELm2Z3k6dNMM0hAALDEGvWbSX8wW9ETgKPNx3HINA+dM8NEy+0BPv5laCEDzmyu6s8cAF0Z7pumHys7ZSxR7UunzejGIy7CiE.
    /// D-H Key Message:
    /// Version: 03
    /// Sender instance: 1261171266
    /// Receiver instance: 4074827415
    /// D-H Key: 2d240e2ccc8c07b645f0b7a0844430d8735e3fe767b967be340b9edacf9dd59
    /// b67a95bf35e10c92031bcc1d8886773423987114729f5d6c14464f79143b02821e87e70d98671c00
    /// b6fdd53f47168212b3effe2d6fb58c18e227d5a7d9a76d51c66692e90feb45525c5f8f9d24065d02
    /// 8c4d752538cf50b042e6d99de4e9d34c33484000b0c41af59b497f305bd11380a3cdc771c8340f9d
    /// 33c344cbed013efe65682103ce6caeeacf1c005d19ee9ba61f2b3b652c51ed4ba7cde8c6232ec288
    /// 4
    /// .
    /// </example>
    public sealed class DHKeyMessage : BaseOTRMessage
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="DHKeyMessage"/> class from being created.
        /// </summary>
        private DHKeyMessage()
        {
        }
        #endregion

        #region Public Properties
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
        /// Gets the Multi-precision integer.
        /// </summary>
        public string MPI { get; private set; }

        /// <summary>
        /// Gets the Off-the-Record <see cref="OTRMessageType" />.
        /// </summary>
        public override OTRMessageType MessageType
        {
            get { return OTRMessageType.DHKeyMessage; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Parses the input string and returns a <see cref="DHKeyMessage"/> object.
        /// </summary>
        /// <param name="msg">Input string.</param>
        /// <returns>a <see cref="DHKeyMessage"/> object.</returns>
        public static DHKeyMessage Parse(string msg)
        {
            try
            {
                DHKeyMessage dhkm = new DHKeyMessage();

                int start;

                dhkm.RawData = Utils.Parse.DecodeFromBase64(msg, out start);

                if (dhkm.RawData == null)
                {
                    throw new ArgumentException("msg is not a D-H Key Message (1)");
                }

                dhkm.Version = dhkm.RawData[1];

                int offset = 3;

                if (dhkm.RawData[0] == '\x00' && dhkm.RawData[1] == '\x03' && dhkm.RawData[2] == '\x0a')
                {
                    dhkm.SenderInstance = Utils.Parse.ReadInt32(dhkm.RawData, ref offset);
                    dhkm.ReceiverInstance = Utils.Parse.ReadInt32(dhkm.RawData, ref offset);
                }
                else if (dhkm.RawData[0] == '\x00' && dhkm.RawData[1] == '\x02' && dhkm.RawData[2] == '\x0a')
                {
                    dhkm.SenderInstance = 0;
                    dhkm.ReceiverInstance = 0;
                }
                else
                {
                    throw new ArgumentException("msg is not a D-H Key Message (2)");
                }

                dhkm.MPI = Utils.Parse.ReadMPI(dhkm.RawData, ref offset);

                Log.Debug(dhkm.Details());

                return dhkm;
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
                "D-H Key Message:" + Environment.NewLine +
                "\tVersion: {0}" + Environment.NewLine +
                "\tSender instance: {1}" + Environment.NewLine +
                "\tReceiver instance: {2}" + Environment.NewLine +
                "\tD-H Key: {3}",
                this.Version,
                this.SenderInstance,
                this.ReceiverInstance,
                this.MPI);
        }
        #endregion
    }
}
