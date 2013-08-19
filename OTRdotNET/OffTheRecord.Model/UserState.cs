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
        #region Fields
        ////private ConnectionContext context = null;
        #endregion

        #region Constructor
        public UserState()
        {
        }
        #endregion

        #region Public properties
        public ConnectionContext ContextRoot { get; private set; }

        public object PrivateKey { get; private set; }

        public InstanceTag InstanceTag { get; private set; }

        public object PendingPrivateKey { get; private set; }

        public int TimerRunning { get; private set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Read Private Keys from local storage for UserState.
        /// </summary>
        /// <param name="filename"></param>
        public void ReadPrivateKeys(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
        }

        /// <summary>
        /// Read Instance Tags from local storage for UserState.
        /// </summary>
        /// <param name="filename"></param>
        public void ReadInstanceTags(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
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

        #region Private methods
        #endregion

        ////struct s_OtrlUserState
        ////{
        ////    ConnContext* context_root;
        ////    OtrlPrivKey* privkey_root;
        ////    OtrlInsTag* instag_root;
        ////    OtrlPendingPrivKey* pending_root;
        ////    int timer_running;
        ////};
    }
}
