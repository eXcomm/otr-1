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

namespace OffTheRecord.Toolkit.Sesskeys
{
    #region Namespaces
    using System;

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
            Usage(args);
        }
        #endregion

        #region Private methods
        private static void Usage(string[] args)
        {
            string error_msg = string.Format(
                "Usage: {0}{1}" +
                "Read Off-the-Record (OTR) Key Exchange and/or Data messages from stdin{1}" +
                "and display their contents in a more readable format.{1}",
                args[0],
                Environment.NewLine);
            Console.WriteLine(error_msg);
            return;
        }
        #endregion
    }
}
