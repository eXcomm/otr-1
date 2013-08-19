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

namespace OffTheRecord.Model.Pidgin.UserSettingsFile
{
    #region Namespaces
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// privkeys class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public sealed class privkeys
    {
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="privkeys"/> class.
        /// </summary>
        public privkeys()
        {
            this.account = new Collection<account>();
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the account collection.
        /// </summary>
        public Collection<account> account { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Finds an account in the account collection based upon account name and protocol.
        /// </summary>
        /// <param name="accountName">Account name.</param>
        /// <param name="protocol">The protocol.</param>
        /// <returns>A <see cref="account"/> object or null if not found.</returns>
        public account FindAccount(string accountName, string protocol)
        {
            foreach (account account in this.account)
            {
                if (string.Compare(accountName, account.name) == 0 && string.Compare(protocol, account.protocol) == 0)
                {
                    return account;
                }
            }

            return null;
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Deserialize string to <see cref="privkeys"/> object.
        /// </summary>
        /// <param name="item">Serialized string.</param>
        /// <returns>A <see cref="privkeys"/> object.</returns>
        internal static privkeys Deserialize(Item item)
        {
            Item child = item.Children[0];

            if (!child.Value.StartsWith("(privkeys"))
            {
                throw new ArgumentException("incorrect format");
            }

            // parse tree;
            privkeys privkeys = new privkeys();
            privkeys.account = UserSettingsFile.account.Deserialize(child);

            return privkeys;
        }

        /// <summary>
        /// Serialize object.
        /// </summary>
        /// <returns>Serialized string.</returns>
        internal string Serialize()
        {
            string accounts = string.Empty;

            foreach (account account in this.account)
            {
                accounts += " " + account.Serialize();
            }

            return string.Format("(privkeys{0}{1}){0}", Environment.NewLine, accounts);
        }
        #endregion
    }
}
