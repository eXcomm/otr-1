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

namespace OffTheRecord.Toolkit.Mackey
{
    using System;
    using OffTheRecord.Protocol.DiffieHellman;

    /// <summary>
    /// Off-the-Record Mac Key Program (otr_mackey.exe).
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
            /* $ ./otr_mackey.exe 8863A4479AE2857FB9BE657E3B7E37C4
             * AES key: 8863a4479ae2857fb9be657e3b7e37c4
             * MAC key: a43167d308ba9de0127f3124a55bea9a608c10c4
             */

            if (args.Length != 1)
            {
                Usage(args);
                return;
            }

            if (args[0].Length != 32)
            {
                Console.WriteLine("The AES key must be 32 hex chars long.");
                Usage(args);
                return;
            }

            string aeskey = args[0];
            string mackey = aeskey.MacKey();

            Console.WriteLine("AES key: {0}", aeskey);
            Console.WriteLine("MAC key: {0}", mackey);

            return;
        }
        #endregion

        #region Private methods
        private static void Usage(string[] args)
        {
            string errorMsg = string.Format(
                "Usage: {0} aeskey{1}" +
                "Calculate and display the MAC key derived from a given AES key.{1}",
                args[0],
                Environment.NewLine);
            Console.WriteLine(errorMsg);
            return;
        }
        #endregion

    }
}
