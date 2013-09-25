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

namespace OffTheRecord.Model.Files
{
    #region Namespaces
    using System.IO;
    using OffTheRecord.Model.Files.OtrPrivateKey;
    #endregion

    /// <summary>
    /// ParseUserSettingsFile class.
    /// </summary>
    public static class ParseOtrPrivateKeyFile
    {
        #region Public methods
        /// <summary>
        /// Gets <see cref="PrivateKeys"/> object from file.
        /// </summary>
        /// <param name="filename">Filename to retrieve <see cref="PrivateKeys"/> collection from.</param>
        /// <returns>A <see cref="PrivateKeys"/> object.</returns>
        public static PrivateKeys GetPrivateKeys(string filename)
        {
            privkeys keys = Deserialize(filename);

            PrivateKeys privateKeys = new PrivateKeys();

            foreach (var account in keys.account)
            {
                PrivateKey privateKey = new PrivateKey(account.private_key.dsa.GetDSAParameters(true));
                privateKey.AccountName = account.name;
                privateKey.Protocol = account.protocol;

                privateKeys.Add(privateKey);
            }

            return privateKeys;
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Deserializes the content of the file into a <see cref="privkeys"/> object structure.
        /// </summary>
        /// <param name="filename">Filename to parse.</param>
        /// <returns>A <see cref="privkeys"/> object or null if failed.</returns>
        internal static privkeys Deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            string data = File.ReadAllText(filename);

            // build parse tree;
            Item root = ParseOtrPrivateKeyFile.BuildTree(data);

            return privkeys.Deserialize(root);
        }

        /// <summary>
        /// Serializes a <see cref="privkeys"/> object into a string.
        /// </summary>
        /// <param name="privkeys">The <see cref="privkeys"/> object to serialize.</param>
        /// <returns>Serialized string.</returns>
        internal static string Serialize(privkeys privkeys)
        {
            return privkeys.Serialize();
        }
        #endregion

        #region Private methods
        private static Item BuildTree(string input)
        {
            Item parent = new Item();

            if (input.Length == 0)
            {
                return parent;
            }

            return BuildTree(input, parent);
        }

        private static Item BuildTree(string input, Item parent)
        {
            int start = -1;
            int nested = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    if (start == -1)
                    {
                        start = i;
                    }
                    else
                    {
                        nested++;
                    }
                }
                else if (input[i] == ')')
                {
                    if (nested == 0)
                    {
                        if (start != -1)
                        {
                            string value = input.Substring(start, i - start);
                            string content = input.Substring(start + 1, i - start - 2);

                            Item child = new Item();
                            child.Value = value;
                            child.Parent = parent;

                            BuildTree(content, child);

                            parent.Children.Add(child);

                            start = -1;
                            nested = 0;
                        }
                    }
                    else
                    {
                        nested--;
                    }
                }
            }

            return parent;
        }
        #endregion
    }
}
