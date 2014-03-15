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

namespace OffTheRecord.Model
{
    #region Namespaces
    using System.IO;
    using OffTheRecord.Model.Files;
    #endregion

    /// <summary>
    /// Userstate class.
    /// </summary>
    /// <remarks>
    /// Most clients will only need one of these.
    /// A OtrlUserState encapsulates the list of known fingerprints
    /// and the list of private keys; if you have separate files for these
    /// things for (say) different users, use different OtrlUserStates.  If
    /// you've got only one user, with multiple accounts all stored together
    /// in the same fingerprint store and privkey store files, use just one
    /// OtrlUserState.
    /// </remarks>
    public class UserState
    {
        #region Constructor
        public UserState()
        {
            this.PrivateKeys = new PrivateKeys();
            this.InstanceTags = new InstanceTags();
        }
        #endregion

        #region Public properties
        public ConnectionContext ContextRoot { get; private set; }

        public PrivateKeys PrivateKeys { get; private set; }

        public InstanceTags InstanceTags { get; private set; }

        public object PendingPrivateKey { get; private set; }

        public int TimerRunning { get; private set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Read Private Keys from local storage for UserState.
        /// </summary>
        /// <param name="filename">File to read private keys from.</param>
        /// <remarks>
        /// gcry_error_t otrl_privkey_read(OtrlUserState us, const char *filename)
        /// </remarks>
        public void ReadPrivateKeys(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            this.PrivateKeys.Clear();
            this.PrivateKeys = ParseOtrPrivateKeyFile.GetPrivateKeys(filename);
        }

        /// <summary>
        /// Read Instance Tags from local storage for UserState.
        /// </summary>
        /// <param name="filename"></param>
        /// <remarks>
        /// 
        /// </remarks>
        public void ReadInstanceTags(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            this.InstanceTags.Clear();
            this.InstanceTags = ParseOtrInstanceTagsFile.GetInstanceTags(filename);
        }

        /// <summary>
        /// Read Fingerprints from local storage for UserState.
        /// </summary>
        /// <param name="filename"></param>
        public void ReadFingerprints(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
        }
        #endregion
    }
}
