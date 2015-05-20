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
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using log4net;
using OffTheRecord.Model.Files.OtrInstanceTags;

namespace OffTheRecord.Model.Files
{
    /// <summary>
    ///     ParseOtrInstanceTagsFile class.
    /// </summary>
    public static class ParseOtrInstanceTagsFile
    {
        #region Fields

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public methods

        /// <summary>
        ///     Gets <see cref="InstanceTags" /> object from file.
        /// </summary>
        /// <param name="filename">Filename to retrieve <see cref="InstanceTags" /> collection from.</param>
        /// <returns>A <see cref="InstanceTags" /> object.</returns>
        public static InstanceTags GetInstanceTags(string filename)
        {
            Collection<instancetag> instancetags = Deserialize(filename);
            return GetInstanceTags(instancetags);
        }

        public static InstanceTags GetInstanceTagsFromString(string data)
        {
            Collection<instancetag> instancetags = DeserializeFromString(data);
            return GetInstanceTags(instancetags);
        }

        #endregion

        #region Internal methods

        /// <summary>
        ///     Deserializes the content of the file into a collection of <see cref="fingerprint" /> objects.
        /// </summary>
        /// <param name="filename">Filename to parse.</param>
        /// <returns>A collection of <see cref="fingerprint" /> objects.</returns>
        internal static Collection<instancetag> Deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            string data = File.ReadAllText(filename);

            return DeserializeFromString(data);
        }

        internal static Collection<instancetag> DeserializeFromString(string data)
        {
            var result = new Collection<instancetag>();

            foreach (string line in data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                // ignore comments.
                if (line.StartsWith("#"))
                {
                    continue;
                }

                result.Add(instancetag.Deserialize(line));
            }

            return result;
        }

        /// <summary>
        ///     Serializes a collection of <see cref="fingerprint" /> objects into a string.
        /// </summary>
        /// <param name="instancetags">The collection of <see cref="fingerprint" /> objects to serialize.</param>
        /// <returns>Serialized string.</returns>
        internal static string Serialize(Collection<instancetag> instancetags)
        {
            string result = string.Empty;

            // add comment
            result +=
                "# WARNING! You shouldn't copy this file to another computer. It is unnecessary and can cause problems." +
                Environment.NewLine;

            foreach (instancetag item in instancetags)
            {
                result += item.Serialize();
            }

            return result;
        }

        #endregion

        public static InstanceTags GetInstanceTags(Collection<instancetag> instancetags)
        {
            var results = new InstanceTags();

            foreach (instancetag instancetag in instancetags)
            {
                var tag = new InstanceTag(instancetag.Account, instancetag.Protocol);
                tag.SetInstanceTag(instancetag.InstanceTag);

                results.Add(tag);
            }

            return results;
        }
    }
}