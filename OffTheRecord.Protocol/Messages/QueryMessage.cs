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
    /// QueryMessage class.
    /// </summary>
    public sealed class QueryMessage : BaseOTRMessage
    {
        #region Fields
        private const string QueryFormat = "?OTR%s\n" +
            "<b>%s</b> has requested an <a href=\"http://otr.cypherpunks.ca/\">Off-the-Record " +
            "private conversation</a>.  However, you do not have a plugin " +
            "to support that.\n" +
            "See <a href=\"http://otr.cypherpunks.ca/\">http://otr.cypherpunks.ca/</a> for more information.";

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="QueryMessage"/> class from being created.
        /// </summary>
        private QueryMessage()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Off-the-Record <see cref="OtrMessageType" />.
        /// </summary>
        public override OtrMessageType MessageType
        {
            get { return OtrMessageType.QueryMessage; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Parses the input string and returns a <see cref="QueryMessage"/> object.
        /// </summary>
        /// <param name="msg">Input string.</param>
        /// <returns>a <see cref="QueryMessage"/> object.</returns>
        public static QueryMessage Parse(string msg)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion
    }
}
