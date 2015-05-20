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

using OffTheRecord.Resources;

namespace OffTheRecord.Protocol.Messages
{
    /// <summary>
    /// Base implementation of Message to share common functionality.
    /// </summary>
    public abstract class BaseOTRMessage
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public properties
        /// <summary>
        /// Gets the Off-the-Record <see cref="OtrMessageType" />.
        /// </summary>
        public abstract OtrMessageType MessageType { get; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get the Off-the-Record <see cref="OtrMessageType" /> based on the input message.
        /// </summary>
        /// <param name="msg">The message to determine the type on.</param>
        /// <returns>The <see cref="OtrMessageType" />.</returns>
        public static OtrMessageType GetType(string msg)
        {
            int index = msg.IndexOf(OtrStrings.OtrPrefix);

            if (index == -1)
            {
                // otr_prefix not found
                if (msg.Contains(PlaintextWithTheWhitespaceTag.OtrMessageTagBase))
                {
                    return OtrMessageType.PlaintextWithTheWhitespaceTag;
                }
                else
                {
                    return OtrMessageType.PlaintextWithoutTheWhitespaceTag;
                }
            }
            else
            {
                // otr_prefix found
                string tag = msg.Substring(index, 8);

                Log.DebugFormat("Tag: {0}", tag);

                if (string.Compare(tag, OtrStrings.OtrMessagePrefixAam) == 0 || string.Compare(tag, OtrStrings.OtrMessagePrefixAai) == 0)
                {
                    char type = msg[index + 8];

                    Log.DebugFormat("Type: {0}", type);

                    switch (type)
                    {
                        case 'C':
                            return OtrMessageType.DhCommitMessage;
                        case 'K':
                            return OtrMessageType.DhKeyMessage;
                        case 'R':
                            return OtrMessageType.RevealSignatureMessage;
                        case 'S':
                            return OtrMessageType.SignatureMessage;
                        case 'D':
                            return OtrMessageType.DataMessage;
                    }
                }
                else
                {
                    if (string.Compare(msg.Substring(index, 5), OtrStrings.OtrQueryMessage1) == 0 || string.Compare(msg.Substring(index, 5), OtrStrings.OtrQueryMessage2) == 0)
                    {
                        return OtrMessageType.QueryMessage;
                    }

                    if (string.Compare(msg.Substring(index, 9), OtrStrings.OtrKeyExchangeV1Message) == 0)
                    {
                        return OtrMessageType.V1KeyExchangeMessage;
                    }

                    if (string.Compare(msg.Substring(index, 9), OtrStrings.OtrDataMessage) == 0)
                    {
                        return OtrMessageType.DataMessage;
                    }

                    if (string.Compare(msg.Substring(index, 11), OtrStrings.OtrErrorMessage) == 0)
                    {
                        return OtrMessageType.ErrorMessage;
                    }
                }
            }

            return OtrMessageType.UnknownMessage;
        }

        /// <summary>
        /// Print the Off-the-Record message details to standard-out.
        /// </summary>
        public abstract void Print();
        #endregion
    }
}
