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

namespace OffTheRecord.Model.Files.OtrInstanceTags
{
    /// <summary>
    ///     instancetags class.
    /// </summary>
    public sealed class instancetag
    {
        #region constructor

        public instancetag(string account, string protocol, string instanceTag)
        {
            Account = account;
            Protocol = protocol;
            InstanceTag = instanceTag;
        }

        private instancetag()
        {
        }

        #endregion

        #region Public properties

        public string Account { get; set; }
        public string Protocol { get; set; }
        public string InstanceTag { get; set; }

        #endregion

        #region Internal methods

        /// <summary>
        ///     Deserialize string to <see cref="privkeys" /> object.
        /// </summary>
        /// <param name="item">Serialized string.</param>
        /// <returns>A <see cref="privkeys" /> object.</returns>
        internal static instancetag Deserialize(string line)
        {
            var it = new instancetag();

            string[] parts = line.Split('\t');
            it.Account = parts[0];
            it.Protocol = parts[1];
            it.InstanceTag = parts[2];

            return it;
        }

        /// <summary>
        ///     Serialize object.
        /// </summary>
        /// <returns>Serialized string.</returns>
        internal string Serialize()
        {
            return string.Format("{0}{1}{2}{1}{3}{4}", Account, '\t', Protocol, InstanceTag, Environment.NewLine);
        }

        #endregion
    }
}