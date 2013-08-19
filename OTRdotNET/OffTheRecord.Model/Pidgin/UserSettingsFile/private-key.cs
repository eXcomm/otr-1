﻿// <copyright>
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

namespace OffTheRecord.Model.Pidgin.UserSettingsFile
{
    #region Namespaces
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using OffTheRecord.Tools;
    #endregion

    /// <summary>
    /// private_key class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public sealed class private_key
    {
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="private_key"/> class.
        /// </summary>
        public private_key()
        {
            this.dsa = new dsa();
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the dsa property.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        public dsa dsa { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns the fingerprint.
        /// </summary>
        /// <param name="privkey"><see cref="private_key"/> object to retrieve the fingerprint from.</param>
        /// <returns>The fingerprint or empty string if failed.</returns>
        public static string Fingerprint(private_key privkey)
        {
            if (privkey == null)
            {
                return string.Empty;
            }

            return privkey.Fingerprint();
        }

        /// <summary>
        /// Returns the fingerprint.
        /// </summary>
        /// <returns>The fingerprint or empty string if failed.</returns>
        public string Fingerprint()
        {
            if (this.dsa == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(this.dsa.p) || string.IsNullOrEmpty(this.dsa.q) || string.IsNullOrEmpty(this.dsa.g) || string.IsNullOrEmpty(this.dsa.y))
            {
                return string.Empty;
            }

            byte[] publicKey = MPI.To(Tools.General.StringToByteArray(this.dsa.p))
                             .Concat(MPI.To(Tools.General.StringToByteArray(this.dsa.q)))
                             .Concat(MPI.To(Tools.General.StringToByteArray(this.dsa.g)))
                             .Concat(MPI.To(Tools.General.StringToByteArray(this.dsa.y)))
                             .ToArray();

            byte[] hash = SHA1.Create().ComputeHash(publicKey);
            return BasePrivateKey.otrl_privkey_hash_to_human(hash);
        }

        /// <summary>
        /// Generate public key.
        /// </summary>
        /// <returns>Generates a public key based on the private key (x parameter).</returns>
        [Obsolete("Currently not implemented.")]
        public byte[] GeneratePublicKey()
        {
            if (this.dsa == null)
            {
                return null;
            }

            byte[] privkey = Tools.General.StringToByteArray(this.dsa.x);

            throw new NotImplementedException();
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Deserialize string to a <see cref="private_key"/> object.
        /// </summary>
        /// <param name="item">Serialized string.</param>
        /// <returns>A <see cref="private_key"/> object.</returns>
        internal static private_key Deserialize(Item item)
        {
            if (!item.Value.StartsWith("(private-key"))
            {
                throw new ArgumentException("incorrect format");
            }

            private_key privkey = new private_key();

            if (item.Children.Count != 0)
            {
                Item child = item.Children[0];

                if (child.Value.StartsWith("(dsa"))
                {
                    privkey.dsa = dsa.Deserialize(child);
                }
            }

            return privkey;
        }

        /// <summary>
        /// Serialize object.
        /// </summary>
        /// <returns>Serialized string.</returns>
        internal string Serialize()
        {
            return string.Format("(private-key{0}{1} ){0}", Environment.NewLine, this.dsa.Serialize());
        }
        #endregion
    }
}
