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

namespace OffTheRecord.Model.Files.OtrPrivateKey
{
    #region namespaces
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// account class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public sealed class account
    {
        #region fields
        private static Collection<string> listOfKnownProtocols = new Collection<string>();

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:FieldNamesMustNotBeginWithUnderscore", Justification = "Reviewed.")]
        private string _protocol;
        #endregion

        #region constructors
        static account()
        {
            listOfKnownProtocols.Add("prpl-msn");
            listOfKnownProtocols.Add("prpl-irc");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="account"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="protocol">The protocol.</param>
        public account(string name, string protocol)
        {
            this.name = name;
            this.protocol = protocol;
            this.private_key = new private_key();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="account"/> class from being created.
        /// </summary>
        private account()
        {
        }
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        public string protocol
        {
            get
            {
                return this._protocol;
            }

            set
            {
                if (!listOfKnownProtocols.Contains(value))
                {
                    throw new ArgumentException("unknown protocol");
                }

                this._protocol = value;
            }
        }

        /// <summary>
        /// Gets or sets the private key.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        public private_key private_key { get; set; }
        #endregion

        #region Internal methods
        /// <summary>
        /// Deserialize string to a collection of <see cref="account"/> objects.
        /// </summary>
        /// <param name="parent">Serialized string.</param>
        /// <returns>A collection of <see cref="account"/> objects.</returns>
        internal static Collection<account> Deserialize(Item parent)
        {
            Collection<account> accounts = new Collection<account>();

            foreach (var item in parent.Children)
            {
                if (!item.Value.StartsWith("(account"))
                {
                    throw new ArgumentException("incorrect format");
                }

                account account = new account();

                try
                {
                    foreach (var child in item.Children)
                    {
                        if (child.Value.StartsWith("(name"))
                        {
                            account.name = child.Value.Split(' ')[1].TrimEnd(')').Trim('"');
                        }
                        else if (child.Value.StartsWith("(protocol"))
                        {
                            account.protocol = child.Value.Split(' ')[1].TrimEnd(')');
                        }
                        else if (child.Value.StartsWith("(private-key"))
                        {
                            account.private_key = private_key.Deserialize(child);
                        }
                    }

                    accounts.Add(account);
                }
                catch
                {
                    throw new Exception("Parse exception");
                }
            }

            return accounts;
        }

        /// <summary>
        /// Serialize object.
        /// </summary>
        /// <returns>Serialized string.</returns>
        internal string Serialize()
        {
            return string.Format("(account{0}(name \"{1}\"){0}(protocol {2}){0}{3} ){0}", Environment.NewLine, this.name, this.protocol.ToString(), this.private_key.Serialize());
        }
        #endregion
    }
}
