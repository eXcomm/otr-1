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

namespace OffTheRecord.Model.Files
{
    #region Namespaces
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using OffTheRecord.Model.Files.OtrFingerprints;
    #endregion

    /// <summary>
    /// ParseOtrFingerprintsFile class.
    /// </summary>
    public static class ParseOtrFingerprintsFile
    {
        #region Public methods
        /// <summary>
        /// Deserializes the content of the file into a collection of <see cref="fingerprint"/> objects.
        /// </summary>
        /// <param name="filename">Filename to parse.</param>
        /// <returns>A collection of <see cref="fingerprint"/> objects.</returns>
        public static Collection<fingerprint> Deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            string[] data = File.ReadAllLines(filename);

            Collection<fingerprint> result = new Collection<fingerprint>();

            foreach (var line in data)
            {
                result.Add(fingerprint.Deserialize(line));
            }

            return result;
        }

        /// <summary>
        /// Serializes a collection of <see cref="fingerprint"/> objects into a string.
        /// </summary>
        /// <param name="privkeys">The collection of <see cref="fingerprint"/> objects to serialize.</param>
        /// <returns>Serialized string.</returns>
        public static string Serialize(Collection<fingerprint> fingerprints)
        {
            string result = string.Empty;

            foreach (var item in fingerprints)
            {
                result += item.Serialize() + Environment.NewLine;
            }

            return result;
        }
        #endregion
    }
}