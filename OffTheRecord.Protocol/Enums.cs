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

namespace OffTheRecord.Protocol
{
    #region Namespaces

    #endregion

    /// <summary>
    /// Off the Record Message Type.
    /// </summary>
    public enum OTRMessageType
    {
        /// <summary>
        /// Data Message.
        /// </summary>
        DataMessage,

        /// <summary>
        /// D-H Commit Message.
        /// </summary>
        DHCommitMessage,

        /// <summary>
        /// D-H Key Message.
        /// </summary>
        DHKeyMessage,

        /// <summary>
        /// Error Message.
        /// </summary>
        ErrorMessage,

        /// <summary>
        /// Plaintext without the whitespace tag.
        /// </summary>
        PlaintextWithoutTheWhitespaceTag,

        /// <summary>
        /// Plaintext WITH the whitespace tag.
        /// </summary>
        PlaintextWithTheWhitespaceTag,

        /// <summary>
        /// Query Message.
        /// </summary>
        QueryMessage,

        /// <summary>
        /// Reveal Signature Message.
        /// </summary>
        RevealSignatureMessage,

        /// <summary>
        /// Signature Message.
        /// </summary>
        SignatureMessage,

        /// <summary>
        /// Version 1 Key Exchange Message.
        /// </summary>
        V1KeyExchangeMessage,

        /// <summary>
        /// Not a Off the Record Message.
        /// </summary>
        NotOffTheRecordMessage,

        /// <summary>
        /// Unknown Message.
        /// </summary>
        UnknownMessage,
    }
}
