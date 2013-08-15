namespace OffTheRecord.Model.Pidgin.UserSettingsFile
{
    #region namespaces
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    #endregion

    public sealed class account
    {
        #region fields
        private static Collection<string> listOfKnownProtocols = new Collection<string>();
        private string _protocol;
        #endregion

        #region constructors
        static account()
        {
            listOfKnownProtocols.Add("prpl-msn");
            listOfKnownProtocols.Add("prpl-irc");
        }

        private account()
        {
        }

        public account(string name, string protocol)
        {
            this.name = name;
            this.protocol = protocol;
            this.private_key = new private_key();
        }
        #endregion

        #region public properties
        public string name { get; set; }
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
        public private_key private_key { get; set; }
        #endregion

        #region Internal methods
        internal string Serialize()
        {
            return string.Format("(account{0}(name \"{1}\"){0}(protocol {2}){0}{3} ){0}", Environment.NewLine, this.name, this.protocol.ToString(), this.private_key.Serialize());
        }

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
        #endregion
    }
}
