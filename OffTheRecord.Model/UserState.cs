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

namespace OffTheRecord.Model
{
    #region Namespaces
    using OffTheRecord.Model.Files;
    using System.IO;
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
    public class UserState : IDisposable
    {
        #region Fields
        private bool disposed = false;
        #endregion

        #region Constructor
        internal UserState()
        {
            this.ConnectionContexts = new ConnectionContexts();
            this.PrivateKeys = new PrivateKeys();
            this.InstanceTags = new InstanceTags();
            this.PendingPrivateKey = null;
            this.TimerRunning = 0;
        }

        ~UserState()
        {
            if (!this.disposed)
            {
                this.Dispose();
            }
        }
        #endregion

        #region Public properties
        public ConnectionContexts ConnectionContexts { get; private set; }

        public PrivateKeys PrivateKeys { get; private set; }

        public InstanceTags InstanceTags { get; private set; }

        public object PendingPrivateKey { get; private set; }

        public int TimerRunning { get; private set; }
        #endregion

        #region Public methods
        public void ReadPrivateKeys(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            this.PrivateKeys = ParseOtrPrivateKeyFile.GetPrivateKeys(filename);
        }

        public void ReadInstanceTags(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            this.InstanceTags = ParseOtrInstanceTagsFile.GetInstanceTags(filename);
        }

        public void ReadFingerprints(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            var fingerprints = ParseOtrFingerprintsFile.GetFingerprints(filename);

            foreach (var fingerprint in fingerprints)
            {
                // find or create a connectioncontext;

////#define OTRL_INSTAG_MASTER 0
////#define OTRL_INSTAG_BEST 1 /* Most secure, based on: conv status,
////                * then fingerprint status, then most recent. */
////#define OTRL_INSTAG_RECENT 2
////#define OTRL_INSTAG_RECENT_RECEIVED 3
////#define OTRL_INSTAG_RECENT_SENT 4
                
                ////UserState, fingerprint.username, fingerprint.accountname, fingerprint.protocol

                /////* Get the context for this user, adding if not yet present */
                ////context = otrl_context_find(us, username, accountname, protocol, OTRL_INSTAG_MASTER, 1, NULL, add_app_data, data);
                var cc = this.ConnectionContexts.GetConnectionContext(fingerprint);

                /////* Add the fingerprint if not already there */
                ////fng = otrl_context_find_fingerprint(context, fingerprint, 1, NULL);
                ////otrl_context_set_trust(fng, trust);
            }
        }

        public void Dispose()
        {
            this.disposed = true;

            if (this.ConnectionContexts != null)
            {
                this.ConnectionContexts.Dispose();
                this.ConnectionContexts = null;
            }

            if (this.PrivateKeys != null)
            {
                this.PrivateKeys.Dispose();
                this.PrivateKeys = null;
            }

            if (this.InstanceTags != null)
            {
                this.InstanceTags.Dispose();
                this.InstanceTags = null;
            }

            this.PendingPrivateKey = null;

            this.TimerRunning = 0;
        }
        #endregion
    }
}
