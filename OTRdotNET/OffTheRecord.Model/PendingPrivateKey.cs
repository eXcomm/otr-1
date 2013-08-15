
namespace OffTheRecord.Model
{
    public class PendingPrivateKey : BasePrivateKey
    {
        public override BasePrivateKey Next { get { return null; } }
        public override BasePrivateKey ToUs { get { return this; } }
        public override string AccountName { get { return string.Empty; } }
        public override string Protocol { get { return string.Empty; } }

    }
}
