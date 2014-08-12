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

namespace OffTheRecord.Model
{
    #region Namespaces
    using System;
    #endregion

    /// <summary>
    /// Fingerprint class.
    /// </summary>
    public class Fingerprint
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Fingerprint"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="accountName">The account name.</param>
        /// <param name="protocol">The protocol.</param>
        /// <param name="fingerprint">The fingerprint as read from the file (40chars).</param>
        /// <param name="status">The status.</param>
        public Fingerprint(string username, string accountName, string protocol, string fingerprint, string status)
        {
            this.Username = username;
            this.AccountName = accountName;
            this.Protocol = protocol;

            this.SetFingerprint(fingerprint);

            FingerprintStatus fingerprintStatus = FingerprintStatus.Empty;

            if (Enum.TryParse(status, true, out fingerprintStatus))
            {
                this.Status = fingerprintStatus;
            }
        }
        #endregion

        #region Enums
        /// <summary>
        /// Fingerprint status enum.
        /// </summary>
        public enum FingerprintStatus
        {
            // Empty (no state set)
            Empty,

            /// <summary>
            /// Verified fingerprint / user.
            /// </summary>
            Verified,

            /// <summary>
            /// Socialist Millionare Protocol (not executed?!).
            /// </summary>
            SMP,
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the accountname.
        /// </summary>
        public string AccountName { get; private set; }

        /// <summary>
        /// Gets the protocol.
        /// </summary>
        public string Protocol { get; private set; }

        /// <summary>
        /// Gets the fingerprint.
        /// </summary>
        public string Print { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public FingerprintStatus Status { get; private set; }
        #endregion

        #region Private methods
        private void SetFingerprint(string fingerprint)
        {
            this.Print = fingerprint.Substring(0, 8) + " " + fingerprint.Substring(8, 8) + " " + fingerprint.Substring(16, 8) + " " + fingerprint.Substring(24, 8) + " " + fingerprint.Substring(32);
        }
        #endregion
    }
}
