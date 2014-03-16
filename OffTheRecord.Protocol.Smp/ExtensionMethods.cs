namespace OffTheRecord.Protocol.SocialistMillionaire
{
    using System;

    public static class ExtensionMethods
    {
        public static byte[] Append(this byte[] left, byte[] right)
        {
            int length = left.Length + right.Length;
            byte[] combined = new byte[length];
            left.CopyTo(combined, 0);
            right.CopyTo(combined, left.Length);

            return combined;
        }

        public static byte[] FromHexToByteArray(this string Hex)
        {
            byte[] bytes = new byte[Hex.Length / 2];
            int[] hexValue = new int[] {
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                bytes[x] = (byte)(hexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  hexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }
            return bytes;
        }
    }
}
