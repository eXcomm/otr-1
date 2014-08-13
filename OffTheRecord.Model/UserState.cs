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
using System.IO;
using OffTheRecord.Model.Files;

namespace OffTheRecord.Model
{
    /// <remarks>
    ///     Most clients will only need one of these.
    ///     A OtrlUserState encapsulates the list of known fingerprints
    ///     and the list of private keys; if you have separate files for these
    ///     things for (say) different users, use different OtrlUserStates.  If
    ///     you've got only one user, with multiple accounts all stored together
    ///     in the same fingerprint store and privkey store files, use just one
    ///     OtrlUserState.
    /// </remarks>
    public class UserState : IDisposable
    {
        #region Fields

        private bool disposed;

        #endregion

        #region Constructor

        internal UserState()
        {
            ConnectionContexts = new ConnectionContexts();
            PrivateKeys = new PrivateKeys();
            InstanceTags = new InstanceTags();
            PendingPrivateKey = null;
            TimerRunning = 0;
        }

        ~UserState()
        {
            if (!disposed)
            {
                Dispose();
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

        public void Dispose()
        {
            disposed = true;

            if (ConnectionContexts != null)
            {
                ConnectionContexts.Dispose();
                ConnectionContexts = null;
            }

            if (PrivateKeys != null)
            {
                PrivateKeys.Dispose();
                PrivateKeys = null;
            }

            if (InstanceTags != null)
            {
                InstanceTags.Dispose();
                InstanceTags = null;
            }

            PendingPrivateKey = null;

            TimerRunning = 0;
        }

        public void ReadPrivateKeys(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            PrivateKeys = ParseOtrPrivateKeyFile.GetPrivateKeys(filename);
        }

        public void ReadInstanceTags(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            InstanceTags = ParseOtrInstanceTagsFile.GetInstanceTags(filename);
        }

        public void ReadFingerprints(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            Fingerprints fingerprints = ParseOtrFingerprintsFile.GetFingerprints(filename);

            foreach (Fingerprint fingerprint in fingerprints)
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
                ConnectionContext cc = ConnectionContexts.GetConnectionContext(fingerprint);

                /////* Add the fingerprint if not already there */
                ////fng = otrl_context_find_fingerprint(context, fingerprint, 1, NULL);
                ////otrl_context_set_trust(fng, trust);
            }
        }

        #endregion
    }
}