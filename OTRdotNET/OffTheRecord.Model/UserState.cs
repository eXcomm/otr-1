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
    using System.Collections.ObjectModel;
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
        #region Constructor
        public UserState()
        {
            this.PrivateKey = new Collection<PrivateKey>();
        }
        #endregion

        #region Public properties
        public ConnectionContext ContextRoot { get; private set; }

        public Collection<PrivateKey> PrivateKey { get; private set; }

        public InstanceTag InstanceTag { get; private set; }

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

            // open file for reading (rb mode) 

            ////int privfd;
            ////struct stat st;
            ////char *buf;
            ////const char *token;
            ////size_t tokenlen;
            ////gcry_error_t err;
            ////gcry_sexp_t allkeys;
            ////int i;

            ////if (!privf) return gcry_error(GPG_ERR_NO_ERROR);

            /////* Release any old ideas we had about our keys */
            ////otrl_privkey_forget_all(us);
            this.PrivateKey.Clear();

            /////* Load the data into a buffer */
            ////privfd = fileno(privf);
            ////if (fstat(privfd, &st)) {
            ////err = gcry_error_from_errno(errno);
            ////return err;
            ////}
            ////buf = malloc(st.st_size);
            ////if (!buf && st.st_size > 0) {
            ////return gcry_error(GPG_ERR_ENOMEM);
            ////}
            ////if (fread(buf, st.st_size, 1, privf) != 1) {
            ////err = gcry_error_from_errno(errno);
            ////free(buf);
            ////return err;
            ////}

            ////err = gcry_sexp_new(&allkeys, buf, st.st_size, 0);
            ////free(buf);
            ////if (err) {
            ////return err;
            ////}

            ////token = gcry_sexp_nth_data(allkeys, 0, &tokenlen);
            ////if (tokenlen != 8 || strncmp(token, "privkeys", 8)) {
            ////gcry_sexp_release(allkeys);
            ////return gcry_error(GPG_ERR_UNUSABLE_SECKEY);
            ////}

            /////* Get each account */
            ////for(i=1; i<gcry_sexp_length(allkeys); ++i) {
            ////gcry_sexp_t names, protos, privs;
            ////char *name, *proto;
            ////gcry_sexp_t accounts;
            ////OtrlPrivKey *p;

            /////* Get the ith "account" S-exp */
            ////accounts = gcry_sexp_nth(allkeys, i);

            /////* It's really an "account" S-exp? */
            ////token = gcry_sexp_nth_data(accounts, 0, &tokenlen);
            ////if (tokenlen != 7 || strncmp(token, "account", 7)) {
            ////    gcry_sexp_release(accounts);
            ////    gcry_sexp_release(allkeys);
            ////    return gcry_error(GPG_ERR_UNUSABLE_SECKEY);
            ////}
            /////* Extract the name, protocol, and privkey S-exps */
            ////names = gcry_sexp_find_token(accounts, "name", 0);
            ////protos = gcry_sexp_find_token(accounts, "protocol", 0);
            ////privs = gcry_sexp_find_token(accounts, "private-key", 0);
            ////gcry_sexp_release(accounts);
            ////if (!names || !protos || !privs) {
            ////    gcry_sexp_release(names);
            ////    gcry_sexp_release(protos);
            ////    gcry_sexp_release(privs);
            ////    gcry_sexp_release(allkeys);
            ////    return gcry_error(GPG_ERR_UNUSABLE_SECKEY);
            ////}
            /////* Extract the actual name and protocol */
            ////token = gcry_sexp_nth_data(names, 1, &tokenlen);
            ////if (!token) {
            ////    gcry_sexp_release(names);
            ////    gcry_sexp_release(protos);
            ////    gcry_sexp_release(privs);
            ////    gcry_sexp_release(allkeys);
            ////    return gcry_error(GPG_ERR_UNUSABLE_SECKEY);
            ////}
            ////name = malloc(tokenlen + 1);
            ////if (!name) {
            ////    gcry_sexp_release(names);
            ////    gcry_sexp_release(protos);
            ////    gcry_sexp_release(privs);
            ////    gcry_sexp_release(allkeys);
            ////    return gcry_error(GPG_ERR_ENOMEM);
            ////}
            ////memmove(name, token, tokenlen);
            ////name[tokenlen] = '\0';
            ////gcry_sexp_release(names);

            ////token = gcry_sexp_nth_data(protos, 1, &tokenlen);
            ////if (!token) {
            ////    free(name);
            ////    gcry_sexp_release(protos);
            ////    gcry_sexp_release(privs);
            ////    gcry_sexp_release(allkeys);
            ////    return gcry_error(GPG_ERR_UNUSABLE_SECKEY);
            ////}
            ////proto = malloc(tokenlen + 1);
            ////if (!proto) {
            ////    free(name);
            ////    gcry_sexp_release(protos);
            ////    gcry_sexp_release(privs);
            ////    gcry_sexp_release(allkeys);
            ////    return gcry_error(GPG_ERR_ENOMEM);
            ////}
            ////memmove(proto, token, tokenlen);
            ////proto[tokenlen] = '\0';
            ////gcry_sexp_release(protos);

            /////* Make a new OtrlPrivKey entry */
            ////p = malloc(sizeof(*p));
            ////if (!p) {
            ////    free(name);
            ////    free(proto);
            ////    gcry_sexp_release(privs);
            ////    gcry_sexp_release(allkeys);
            ////    return gcry_error(GPG_ERR_ENOMEM);
            ////}

            /////* Fill it in and link it up */
            ////p->accountname = name;
            ////p->protocol = proto;
            ////p->pubkey_type = OTRL_PUBKEY_TYPE_DSA;
            ////p->privkey = privs;
            ////p->next = us->privkey_root;
            ////if (p->next) {
            ////    p->next->tous = &(p->next);
            ////}
            ////p->tous = &(us->privkey_root);
            ////us->privkey_root = p;
            ////err = make_pubkey(&(p->pubkey_data), &(p->pubkey_datalen), p->privkey);
            ////if (err) {
            ////    gcry_sexp_release(allkeys);
            ////    otrl_privkey_forget(p);
            ////    return gcry_error(GPG_ERR_UNUSABLE_SECKEY);
            ////}
            ////}
            ////gcry_sexp_release(allkeys);

            ////return gcry_error(GPG_ERR_NO_ERROR);

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
