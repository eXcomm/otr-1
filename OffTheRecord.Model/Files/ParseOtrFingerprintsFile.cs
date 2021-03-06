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

using System;
using System.Collections.ObjectModel;
using System.IO;
using OffTheRecord.Model.Files.OtrFingerprints;

namespace OffTheRecord.Model.Files
{
    /// <summary>
    ///     ParseOtrFingerprintsFile class.
    /// </summary>
    public static class ParseOtrFingerprintsFile
    {
        #region Public methods

        /// <summary>
        ///     Gets <see cref="Fingerprints" /> object from file.
        /// </summary>
        /// <param name="filename">Filename to retrieve <see cref="Fingerprints" /> collection from.</param>
        /// <returns>A <see cref="Fingerprints" /> object.</returns>
        public static Fingerprints GetFingerprints(string filename)
        {
            var fingerprints = Deserialize(filename);
            return GetFingerprints(fingerprints);
        }

        public static Fingerprints GetFingerprintsFromString(string data)
        {
            var fingerprints = DeserializeFromString(data);
            return GetFingerprints(fingerprints);
        }

        #endregion

        #region Internal methods

        /// <summary>
        ///     Deserializes the content of the file into a collection of <see cref="fingerprint" /> objects.
        /// </summary>
        /// <param name="filename">Filename to parse.</param>
        /// <returns>A collection of <see cref="fingerprint" /> objects.</returns>
        internal static Collection<fingerprint> Deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            string data = File.ReadAllText(filename);

            return DeserializeFromString(data);
        }

        /// <summary>
        ///     Deserializes the content of the file into a collection of <see cref="fingerprint" /> objects.
        /// </summary>
        /// <param name="filename">Filename to parse.</param>
        /// <returns>A collection of <see cref="fingerprint" /> objects.</returns>
        internal static Collection<fingerprint> DeserializeFromString(string data)
        {
            var result = new Collection<fingerprint>();

            foreach (string line in data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                result.Add(fingerprint.Deserialize(line));
            }

            return result;
        }

        /// <summary>
        ///     Serializes a collection of <see cref="fingerprint" /> objects into a string.
        /// </summary>
        /// <param name="fingerprints">The collection of <see cref="fingerprint" /> objects to serialize.</param>
        /// <returns>Serialized string.</returns>
        internal static string Serialize(Collection<fingerprint> fingerprints)
        {
            string result = string.Empty;

            foreach (fingerprint item in fingerprints)
            {
                result += item.Serialize() + Environment.NewLine;
            }

            return result;
        }

        #endregion

        public static Fingerprints GetFingerprints(Collection<fingerprint> fingerprints)
        {
            var results = new Fingerprints();

            foreach (fingerprint fingerprint in fingerprints)
            {
                var fp = new Fingerprint(fingerprint.Username, fingerprint.Account, fingerprint.Protocol,
                    fingerprint.Fingerprint, fingerprint.Status.ToString());
                results.Add(fp);
            }

            return results;
        }
    }
}