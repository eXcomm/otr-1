namespace OffTheRecord.Protocol.SocialistMillionaire
{
    public class SMState
    {
        #region Constructor
        public SMState(byte[] secret)
        {
            this.g1 = 2;
            this.ProgState = SocialistMillionaire.ProgState.OK;
            this.ProgStateInformation = string.Empty;
            this.NextExpected = NextExpectedSMP.OTRL_SMP_EXPECT1;
            this.ReceivedQuestion = 0;

            this.secret = new BigInteger(secret);
        }
        #endregion

        #region Public properties
        public BigInteger secret { get; set; }
        public BigInteger x2 { get; set; }
        public BigInteger x3 { get; set; }
        public BigInteger g1 { get; set; }
        public BigInteger g2 { get; set; }
        public BigInteger g3 { get; set; }
        public BigInteger g3o { get; set; }
        public BigInteger p { get; set; }
        public BigInteger q { get; set; }
        public BigInteger pab { get; set; }
        public BigInteger qab { get; set; }
        public NextExpectedSMP NextExpected { get; set; }
        public int ReceivedQuestion { get; set; }
        public ProgState ProgState { get; set; }
        public string ProgStateInformation { get; set; }
        #endregion

    }
}