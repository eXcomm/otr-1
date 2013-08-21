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

namespace OffTheRecord.Model.Files.OtrFingerprints
{
    #region Namespaces
    using System;
    #endregion

    public enum Statuses
    {
        verified,
        smp,
    }

    /// <summary>
    /// fingerprint class.
    /// </summary>
    public sealed class fingerprint
    {
        #region constructor
        public fingerprint(string name, string account, string protocol, string fingerprint, Statuses status)
        {
            this.Name = name;
            this.Account = account;
            this.Protocol = protocol;
            this.Fingerprint = fingerprint;
            this.Status = status;
        }

        private fingerprint()
        {
        }
        #endregion

        #region Public properties
        public string Name { get; set; }
        public string Account { get; set; }
        public string Protocol { get; set; }
        public string Fingerprint { get; set; }
        public Statuses Status { get; set; }
        #endregion

        #region Internal methods
        /// <summary>
        /// Deserialize string to <see cref="privkeys"/> object.
        /// </summary>
        /// <param name="item">Serialized string.</param>
        /// <returns>A <see cref="privkeys"/> object.</returns>
        internal static fingerprint Deserialize(string line)
        {
            fingerprint fp = new fingerprint();

            string[] parts = line.Split('\t');
            fp.Name = parts[0];
            fp.Account = parts[1];
            fp.Protocol = parts[2];
            fp.Fingerprint = parts[3];

            Statuses status = Statuses.smp;

            Enum.TryParse(parts[4], out status);

            fp.Status = status;

            return fp;
        }

        /// <summary>
        /// Serialize object.
        /// </summary>
        /// <returns>Serialized string.</returns>
        internal string Serialize()
        {
            return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}", this.Name, '\t', this.Account, this.Protocol, this.Fingerprint, this.Status);
        }
        #endregion
    }
}
