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
    using System.Linq;
    using System.Security.Cryptography;
    using OffTheRecord.Resources;
    using OffTheRecord.Tools;
    #endregion

    /// <summary>
    /// PrivateKey class.
    /// </summary>
    public class PrivateKey : BasePrivateKey
    {
        #region Fields
        private DSAParameters privateKey;
        private string accountName = string.Empty;
        private string protocol = string.Empty;
        #endregion

        #region Constructor
        public PrivateKey(DSAParameters dsaParameters)
        {
            this.privateKey = dsaParameters;

            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider(1024);
            dsa.ImportParameters(this.privateKey);
            this.PublicKey = dsa.ExportParameters(false);

            this.PublicKeyAsMPI = MPI.To(this.PublicKey.P)
                 .Concat(MPI.To(this.PublicKey.Q))
                 .Concat(MPI.To(this.PublicKey.G))
                 .Concat(MPI.To(this.PublicKey.Y))
                 .ToArray();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the PublicKeyType.
        /// </summary>
        public uint PublicKeyType
        {
            get { return OtrValues.OffTheRecordPublicKeyTypeDSA; }
        }

        /// <summary>
        /// Gets the PublicKey as <see cref="DSAParameters"/> object.
        /// </summary>
        public DSAParameters PublicKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Fingerprint representation of the Public Key.
        /// </summary>
        public string Fingerprint
        {
            get
            {
                byte[] hash = SHA1.Create().ComputeHash(this.PublicKeyAsMPI);
                return BasePrivateKey.otrl_privkey_hash_to_human(hash);
            }
        }

        /// <summary>
        /// Gets the PublicKey as MPI.
        /// </summary>
        public byte[] PublicKeyAsMPI
        {
            get;
            private set;
        }
        #endregion
    }
}
