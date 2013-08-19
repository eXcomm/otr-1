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
    public class PrivateKey : BasePrivateKey
    {
        #region Public Properties
        public override BasePrivateKey Next { get { return null; } }
        public override BasePrivateKey ToUs { get { return this; } }
        public override string AccountName { get { return string.Empty; } }
        public override string Protocol { get { return string.Empty; } }
        public uint pubkey_type { get; private set; }
        public object MyPrivateKey { get; private set; }
        public byte[] PublicKey { get; private set; }
        public int PublicKeyLength { get { return this.PublicKey.Length; } }
        #endregion
    }
}
