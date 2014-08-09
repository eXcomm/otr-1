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
    using OffTheRecord.Protocol.SocialistMillionaire;
    using System;
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// ConnectionContext class.
    /// </summary>
    /// <remarks>The connection state between two users.</remarks>
    public class ConnectionContext : IDisposable, IEquatable<ConnectionContext>
    {
        #region Fields
        private bool disposed = false;
        #endregion

        #region Constructor
        internal ConnectionContext(string username, string accountname, string protocol)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (string.IsNullOrEmpty(accountname))
            {
                throw new ArgumentNullException("accountname");
            }

            if (string.IsNullOrEmpty(protocol))
            {
                throw new ArgumentNullException("protocol");
            }
            
            this.username = username;
            this.accountname = accountname;
            this.protocol = protocol;
        }

        ~ConnectionContext()
        {
            if (!this.disposed)
            {
                this.Dispose();
            }
        }
        #endregion

        //    struct context * next;             /* Linked list pointer */
        //    struct context ** tous;            /* A pointer to the pointer to us */

        /* Context information that is meant for internal use */
        public ConnectionContextPrivate ContextPrivate { get; set; }

        /* The user this context is for */
        public string username;

        /* The username is relative to this account... */
        public string accountname;

        /* ... and this protocol */
        public string protocol;

        /* If this is a child context, this field will point to the master context. Otherwise it will point to itself. */
        public ConnectionContext MasterContext { get; set; }

        /* If this is a master context, this points to the child context that has received a message most recently.
         * By default, it will point to the master context. In child contexts this field is NULL. */
        public ConnectionContext RecentReceivedChild { get; set; }

        /* Similar to above, but it points to the child who has sent most recently. */
        public ConnectionContext RecentSentChild { get; set; }

        /* Similar to above, but will point to the most recent of recent_rcvd_child and recent_sent_child */
        public ConnectionContext RecentChild { get; set; }

        //    otrl_instag_t our_instance;        /* Our instance tag for this computer*/
        //    otrl_instag_t their_instance;      /* The user's instance tag */

        //    OtrlMessageState msgstate;         /* The state of message disposition
        //                      with this user */
        //    OtrlAuthInfo auth;                 /* The state of ongoing
        //                      authentication with this user */

        //    Fingerprint fingerprint_root;      /* The root of a linked list of
        //                      Fingerprints entries. This list will
        //                      only be populated in master contexts.
        //                      For child contexts,
        //                      fingerprint_root.next will always
        //                      point to NULL. */
        //    Fingerprint *active_fingerprint;   /* Which fingerprint is in use now?
        //                      A pointer into the above list */

        //    unsigned char sessionid[20];       /* The sessionid and bold half */
        //    size_t sessionid_len;              /* determined when this private */
        //    OtrlSessionIdHalf sessionid_half;  /* connection was established. */

        /* The version of OTR in use */
        public uint protocol_version;

        //    enum {
        //    OFFER_NOT,
        //    OFFER_SENT,
        //    OFFER_REJECTED,
        //    OFFER_ACCEPTED
        //    } otr_offer;          /* Has this correspondent repsponded to our
        //                 OTR offers? */

        //    /* Application data to be associated with this context */
        //    void *app_data;

        //    /* A function to free the above data when we forget this context */
        //    void (*app_data_free)(void *);

        /* The state of the current socialist millionaires exchange */
        public SMState SMState { get; set; }

        #region Public methods
        public void Dispose()
        {
            this.disposed = true;

            // release resources;
        }

        public bool Equals(ConnectionContext other)
        {
            if (string.Compare(this.username, other.username, true) == 0)
            {
                if (string.Compare(this.accountname, other.accountname, true) == 0)
                {
                    if (string.Compare(this.protocol, other.protocol, true) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

    }
}
