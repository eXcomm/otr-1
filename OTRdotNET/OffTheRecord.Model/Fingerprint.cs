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
        public Fingerprint(string username, string accountName, string protocol, string fingerprint, string status)
        {
            this.Username = username;
            this.AccountName = accountName;
            this.Protocol = protocol;

            this.SetFingerprint(fingerprint);

            FingerprintStatus fingerprintStatus = FingerprintStatus.smp;

            if (Enum.TryParse(status, out fingerprintStatus))
            {
                this.Status = fingerprintStatus;
            }
        }
        #endregion

        #region Enums
        public enum FingerprintStatus
        {
            verified,
            smp,
        }
        #endregion

        #region Public properties
        public string Username { get; private set; }
        public string AccountName { get; private set; }
        public string Protocol { get; private set; }
        public string Print { get; private set; }
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
