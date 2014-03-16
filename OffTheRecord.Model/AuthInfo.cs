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
    using OffTheRecord.Protocol.DiffieHellman;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// AuthInfo class.
    /// </summary>
    public class AuthInfo
    {
        public object CurrentState { get; set; }
        public ConnectionContext Context { get; set; }
        public DH1536 OurDH { get; set; }
        public uint OurKeyId { get; set; }
        public string encgx { get; set; }

        /// <summary>
        /// 16 chars
        /// </summary>
        public string r { get; set; }

        /// <summary>
        /// 32 chars
        /// </summary>
        public string hashgx { get; set; }

        public DH1536 TheirPub { get; set; }
        public uint TheirKeyId { get; set; }

        public object enc_c { get; set; }
        public object enc_cp { get; set; }

        public object mac_m1 { get; set; }
        public object mac_m1p { get; set; }

        public object mac_m2 { get; set; }
        public object mac_m2p { get; set; }

        /// <summary>
        /// 20 chars
        /// </summary>
        public string fingerprint { get; set; }

        public bool Initiated { get; set; }

        public uint ProtocolVersion { get; set; }

        /// <summary>
        /// 20 chars
        /// </summary>
        public string SecureSessionId { get; set; }

        public object session_id_half { get; set; }

        public string lastauthmsg { get; set; }

        public DateTime commit_sent_time { get; set; }
    }
}
