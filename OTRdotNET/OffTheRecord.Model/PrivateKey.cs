
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
