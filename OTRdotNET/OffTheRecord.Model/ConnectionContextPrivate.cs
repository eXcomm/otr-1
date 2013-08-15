﻿namespace OffTheRecord.Model
{
    #region Namespaces
    using System;
    using System.Numerics;
    #endregion

    /// <summary>
    /// The ConnectionContextPrivate class.
    /// </summary>
    public class ConnectionContextPrivate
    {
        /* The part of the fragmented message we've seen so far */
        public string Fragment { get; set; }

        /* The length of fragment */
        public int FragementLength
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Fragment))
                {
                    return this.Fragment.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        /* The total number of fragments in this message */
        public ushort FragementNumbers { get; set; }

        /* The highest fragment number we've seen so far for this message */
        public ushort FragementK { get; set; }

        /* current keyid used by other side; this is set to 0 if we get a OTRL_TLV_DISCONNECTED message from them. */
        public uint TheirKeyId { get; set; }

        /* Y[their_keyid] (their DH pubkey) */
        public BigInteger TheirY { get; set; }

        /* Y[their_keyid-1] (their prev DH pubkey) */
        public BigInteger TheirOldY { get; set; }

        /* current keyid used by us */
        public uint OurKeyId { get; set; }

        /* DH key[our_keyid] */
        public object OurDhKey { get; set; }

        /* DH key[our_keyid-1] */
        public object OurOldDhKey { get; set; }

        /* sesskeys[i][j] are the session keys derived from DH key[our_keyid-i] and mpi Y[their_keyid-j] */
        public object SessionKeys { get; set; }

        /* saved mac keys to be revealed later */
        public uint NumberOfSavedKeys { get; set; }

        public string SavedMacKeys { get; set; }

        /* generation number: increment every time we go private, and never reset to 0 (unless we remove the context entirely) */
        public uint Generation { get; set; }

        /* The last time a Data Message was sent */
        public DateTime LastSentMessage { get; set; }

        /* The last time a Data Message was received */
        public DateTime LastReceivedMessage { get; set; }

        /* The plaintext of the last Data Message sent */
        public string LastMessageSent { get; set; }

        /* Is the last message eligible for retransmission? */
        public int MayRetransmit { get; set; }
    }
}
