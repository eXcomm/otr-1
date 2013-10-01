﻿// <copyright>
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

namespace OffTheRecord.Toolkit.Sesskeys
{
    #region Namespaces
    using System;
    using System.Numerics;
    using OffTheRecord.Protocol.DiffieHellman;
    #endregion

    /// <summary>
    /// Off-the-Record Session Keys Program (otr_sesskeys.exe).
    /// </summary>
    public class Program
    {
        #region Fields
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Main
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Application arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Usage(args);
                return;
            }

            if (args[0].Length != 40)
            {
                Usage(args);
                return;
            }

            string our_x = args[0];
            string their_y = args[1];

            BigInteger our_privateKey = BigInteger.Parse(our_x, System.Globalization.NumberStyles.HexNumber);
            BigInteger their_publicKey = BigInteger.Parse(their_y, System.Globalization.NumberStyles.HexNumber);

            DH1536 dh1536_us = new DH1536();
            dh1536_us.GeneratePublicKey(our_privateKey);
            dh1536_us.GenerateSharedSecret(their_publicKey);

            string sessionId = dh1536_us.SessionId();
            Keys keys = dh1536_us.Keys();

            Console.WriteLine("We are the {0} end of this key exchange.\n", keys.IsHigh ? "high" : "low");
            Console.WriteLine();
            Console.WriteLine("Our public key: {0}", dh1536_us.PublicKey.ToString("X"));
            Console.WriteLine();
            Console.WriteLine("Session id: {0}", sessionId);
            Console.WriteLine();
            Console.WriteLine("Sending   AES key: {0}", keys.SendAes);
            Console.WriteLine("Sending   MAC key: {0}", keys.SendMac);
            Console.WriteLine("Receiving AES key: {0}", keys.ReceiveAes);
            Console.WriteLine("Receiving MAC key: {0}", keys.ReceiveMac);
            Console.WriteLine();

            return;
        }
        #endregion

        #region Private methods
        private static void Usage(string[] args)
        {
            string error_msg = string.Format(
                "Usage: {0} our_privkey their_pubkey{1}" +
                "Calculate and display our public key, the session id, two AES keys,{1}" +
                "and two MAC keys generated by the given DH private key and public key.{1}",
                args[0],
                Environment.NewLine);
            Console.WriteLine(error_msg);
            return;
        }
        #endregion
    }
}
