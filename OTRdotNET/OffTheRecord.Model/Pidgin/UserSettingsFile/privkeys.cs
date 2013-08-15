namespace OffTheRecord.Model.Pidgin.UserSettingsFile
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    #endregion

    public sealed class privkeys
    {
        #region Public properties
        public Collection<account> account { get; set; }
        #endregion

        #region constructor
        public privkeys()
        {
            this.account = new Collection<account>();
        }
        #endregion

        #region Public methods
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
        internal string Serialize()
        {
            string accounts = string.Empty;

            foreach (account account in this.account)
            {
                accounts += " " + account.Serialize();
            }

            return string.Format("(privkeys{0}{1}){0}", Environment.NewLine, accounts);
        }

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
        #endregion
    }
}
