
namespace OffTheRecord.Model.Pidgin.UserSettingsFile
{
    #region Namespaces
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using OffTheRecord.Tools;
    #endregion

    public sealed class private_key
    {
        #region Public properties
        public dsa dsa { get; set; }
        #endregion

        #region constructor
        public private_key()
        {
            this.dsa = new dsa();
        }
        #endregion

        #region Public methods
        public static string Fingerprint(private_key privkey)
        {
            if (privkey == null)
            {
                return string.Empty;
            }

            return privkey.Fingerprint();
        }

        public string Fingerprint()
        {
            if (this.dsa == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(this.dsa.p) || string.IsNullOrEmpty(this.dsa.q) || string.IsNullOrEmpty(this.dsa.g) || string.IsNullOrEmpty(this.dsa.y))
            {
                return string.Empty;
            }

            byte[] publicKey = MPI.To(Tools.General.StringToByteArray(this.dsa.p))
                             .Concat(MPI.To(Tools.General.StringToByteArray(this.dsa.q)))
                             .Concat(MPI.To(Tools.General.StringToByteArray(this.dsa.g)))
                             .Concat(MPI.To(Tools.General.StringToByteArray(this.dsa.y)))
                             .ToArray();

            byte[] hash = SHA1.Create().ComputeHash(publicKey);
            return BasePrivateKey.otrl_privkey_hash_to_human(hash);
        }

        public byte[] GeneratePublicKey()
        {
            if (this.dsa == null)
            {
                return null;
            }

            byte[] privkey = Tools.General.StringToByteArray(this.dsa.x);

            throw new NotImplementedException();
        }
        #endregion

        #region Internal methods
        internal string Serialize()
        {
            return string.Format("(private-key{0}{1} ){0}", Environment.NewLine, this.dsa.Serialize());
        }

        internal static private_key Deserialize(Item item)
        {
            if (!item.Value.StartsWith("(private-key"))
            {
                throw new ArgumentException("incorrect format");
            }

            private_key privkey = new private_key();

            if (item.Children.Count != 0)
            {
                Item child = item.Children[0];

                if (child.Value.StartsWith("(dsa"))
                {
                    privkey.dsa = dsa.Deserialize(child);
                }
            }

            return privkey;
        }
        #endregion
    }
}
