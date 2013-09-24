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
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using OffTheRecord.Model.Files.OtrInstanceTags;
    #endregion

    /// <summary>
    /// ParseOtrInstanceTagsFile class.
    /// </summary>
    public static class ParseOtrInstanceTagsFile
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Public methods
        /// <summary>
        /// Gets <see cref="InstanceTags"/> object from file.
        /// </summary>
        /// <param name="filename">Filename to retrieve <see cref="InstanceTags"/> collection from.</param>
        /// <returns>A <see cref="InstanceTags"/> object.</returns>
        public static InstanceTags GetInstanceTags(string filename)
        {
            Collection<instancetag> instancetags = Deserialize(filename);

            InstanceTags results = new InstanceTags();

            foreach (var instancetag in instancetags)
            {
                InstanceTag tag = new InstanceTag(instancetag.Account, instancetag.Protocol);
                tag.SetInstanceTag(instancetag.InstanceTag);

                results.Add(tag);
            }

            return results;
        }

        /// <summary>
        /// Deserializes the content of the file into a collection of <see cref="fingerprint"/> objects.
        /// </summary>
        /// <param name="filename">Filename to parse.</param>
        /// <returns>A collection of <see cref="fingerprint"/> objects.</returns>
        public static Collection<instancetag> Deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            string[] data = File.ReadAllLines(filename);

            Collection<instancetag> result = new Collection<instancetag>();

            foreach (var line in data)
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
        /// Serializes a collection of <see cref="fingerprint"/> objects into a string.
        /// </summary>
        /// <param name="privkeys">The collection of <see cref="fingerprint"/> objects to serialize.</param>
        /// <returns>Serialized string.</returns>
        public static string Serialize(Collection<instancetag> instancetags)
        {
            string result = string.Empty;

            // add comment
            result += "# WARNING! You shouldn't copy this file to another computer. It is unnecessary and can cause problems." + Environment.NewLine;

            foreach (var item in instancetags)
            {
                result += item.Serialize();
            }

            return result;
        }
        #endregion
    }
}
