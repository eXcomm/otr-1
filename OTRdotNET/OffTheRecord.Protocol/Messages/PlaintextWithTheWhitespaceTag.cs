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
    using System.Diagnostics.CodeAnalysis;
    #endregion

    /// <summary>
    /// PlaintextWithTheWhitespaceTag class.
    /// </summary>
    public sealed class PlaintextWithTheWhitespaceTag : BaseOTRMessage
    {
        #region Fields
        /* If we ever see this sequence in a plaintext message, we'll assume the other side speaks OTR, and try to establish a connection. */

        /// <summary>
        /// Off-the-Record message tag base.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
        internal static string OtrMessageTagBase = " \t  \t\t\t\t \t \t \t  ";

        /// <summary>
        /// Log4Net logger.
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /* The following must each be of length 8 */

        /// <summary>
        /// Off-the-Record Message Tag version 1.
        /// </summary>
        private static string otrMessageTagV1 = " \t \t  \t ";

        /// <summary>
        /// Off-the-Record Message Tag version 2.
        /// </summary>
        private static string otrMessageTagV2 = "  \t\t  \t ";

        /// <summary>
        /// Off-the-Record Message Tag version 3.
        /// </summary>
        private static string otrMessageTagV3 = "  \t\t  \t\t";
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="PlaintextWithTheWhitespaceTag"/> class from being created.
        /// </summary>
        private PlaintextWithTheWhitespaceTag()
        {
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the plain text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the Off-the-Record <see cref="OTRMessageType" />.
        /// </summary>
        public override OTRMessageType MessageType
        {
            get { return OTRMessageType.PlaintextWithTheWhitespaceTag; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Parses the input string and returns a <see cref="PlaintextWithTheWhitespaceTag"/> object.
        /// </summary>
        /// <param name="msg">Input string.</param>
        /// <returns>a <see cref="PlaintextWithTheWhitespaceTag"/> object.</returns>
        public static PlaintextWithTheWhitespaceTag Parse(string msg)
        {
            try
            {
                PlaintextWithTheWhitespaceTag ptmsg = new PlaintextWithTheWhitespaceTag();
                ptmsg.Text = msg;

                Log.Debug(ptmsg.Details());

                return ptmsg;
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
                "Plaintext with the white space tag:" + Environment.NewLine +
                "\tText: {0}" + Environment.NewLine +
                this.Text);
        }
        #endregion
    }
}
