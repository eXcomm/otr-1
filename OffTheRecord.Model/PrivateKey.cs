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

using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using OffTheRecord.Resources;
using OffTheRecord.Tools;

namespace OffTheRecord.Model
{
    #region Namespaces

    

    #endregion

    /// <summary>
    ///     PrivateKey class.
    /// </summary>
    [DebuggerDisplay("PrivateKey Fingerprint: {Fingerprint}")]
    public class PrivateKey : BasePrivateKey
    {
        #region Fields

        private DSAParameters privateKey;

        #endregion

        #region Constructor

        private PrivateKey()
        {
            // do nothing
        }

        internal PrivateKey(DSAParameters dsaParameters)
        {
            privateKey = dsaParameters;

            var dsa = new DSACryptoServiceProvider(1024);
            dsa.ImportParameters(privateKey);
            PublicKey = dsa.ExportParameters(false);

            PublicKeyAsMPI = MultiPrecisionInteger.ByteArrayToMPI(PublicKey.P)
                .Concat(MultiPrecisionInteger.ByteArrayToMPI(PublicKey.Q))
                .Concat(MultiPrecisionInteger.ByteArrayToMPI(PublicKey.G))
                .Concat(MultiPrecisionInteger.ByteArrayToMPI(PublicKey.Y))
                .ToArray();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the PublicKeyType.
        /// </summary>
        public uint PublicKeyType
        {
            get { return OtrValues.OffTheRecordPublicKeyTypeDSA; }
        }

        /// <summary>
        ///     Gets the PublicKey as <see cref="DSAParameters" /> object.
        /// </summary>
        public DSAParameters PublicKey { get; private set; }

        /// <summary>
        ///     Gets the Fingerprint representation of the Public Key.
        /// </summary>
        public string Fingerprint
        {
            get
            {
                byte[] hash = SHA1.Create().ComputeHash(PublicKeyAsMPI);
                return otrl_privkey_hash_to_human(hash);
            }
        }

        public static async Task<PrivateKey> CreatePrivateKey()
        {
            // generate new private key for user.
            return await Task.Run(() => { return new PrivateKey(); });
        }

        /// <summary>
        ///     Gets the PublicKey as MPI.
        /// </summary>
        public byte[] PublicKeyAsMPI { get; private set; }

        #endregion
    }
}