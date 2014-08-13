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

using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using log4net;

namespace OffTheRecord.Model
{
    #region Namespaces

    

    #endregion

    /// <summary>
    ///     InstanceTag class.
    /// </summary>
    [DebuggerDisplay("AccountName: {AccountName}, Protocol: {Protocol}")]
    public class InstanceTag
    {
        #region Fields

        /* Instag values below this are reserved for meta instags */
        public const uint OTRL_MIN_VALID_INSTAG = 0x100;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tag" /> class.
        /// </summary>
        /// <param name="accountName">The account name.</param>
        /// <param name="protocol">The protocol.</param>
        public InstanceTag(string accountName, string protocol)
        {
            AccountName = accountName;
            Protocol = protocol;
        }

        #endregion

        #region Enum

////#define OTRL_INSTAG_MASTER 0
////#define OTRL_INSTAG_BEST 1 /* Most secure, based on: conv status, then fingerprint status, then most recent. */
////#define OTRL_INSTAG_RECENT 2
////#define OTRL_INSTAG_RECENT_RECEIVED 3
////#define OTRL_INSTAG_RECENT_SENT 4

        #endregion

        #region Public properties

        public string AccountName { get; private set; }

        public string Protocol { get; private set; }

        public uint Tag { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        ///     Set Instance Tag using Hex string as input.
        /// </summary>
        /// <param name="instanceTag">Instance tag as hex input.</param>
        public void SetInstanceTag(string instanceTag)
        {
            uint value = uint.Parse(instanceTag, NumberStyles.AllowHexSpecifier);

            if (value <= OTRL_MIN_VALID_INSTAG)
            {
                throw new ArgumentException("use SetInstanceTagWithMetaTag");
            }
            Tag = value;
            Log.DebugFormat("Set InstanceTag to: {0}", Tag);
        }

        #endregion
    }
}