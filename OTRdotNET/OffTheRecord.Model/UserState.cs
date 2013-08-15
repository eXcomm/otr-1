using System.IO;
namespace OffTheRecord.Model
{
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
