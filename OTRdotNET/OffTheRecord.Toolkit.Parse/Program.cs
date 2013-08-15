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

namespace OffTheRecord.Toolkit.Parse
{
    #region Namespaces
    using System;
    using System.IO;
    using OffTheRecord.Protocol;
    using OffTheRecord.Protocol.Messages;
    using OffTheRecord.Tools;
    #endregion

    /// <summary>
    /// Off-the-Record Parse Program (otr_parse.exe).
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
            if (args.Length > 1)
            {
                Usage(args);
                return;
            }

            Stream s = Console.OpenStandardInput();

            string otr = General.ReadOtr(s);

            ////while ((otrmsg = readotr(stdin)) != NULL)
            ////{
            ////    parse(otrmsg);
            ////    free(otrmsg);
            ////}

            if (otr == null)
            {
                Usage(args);
                return;
            }

            Log.DebugFormat("Found Off-the-Record message: {0}", otr);

            try
            {
                OTRMessageType type = BaseOTRMessage.GetType(otr);

                Log.DebugFormat("Off-the-Record MessageType: {0}", type);

                switch (type)
                {
                    case OTRMessageType.DataMessage:
                        BaseOTRMessage msg = DataMessage.Parse(otr);
                        if (msg == null)
                        {
                            Console.WriteLine("Invalid Data Message");
                        }
                        else
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.DHCommitMessage:
                        msg = DHCommitMessage.Parse(otr);
                        if (msg == null)
                        {
                            Console.WriteLine("Invalid D-H Commit Message");
                        }
                        else
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.DHKeyMessage:
                        msg = DHKeyMessage.Parse(otr);
                        if (msg == null)
                        {
                            Console.WriteLine("Invalid D-H Key Message");
                        }
                        else
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.ErrorMessage:
                        msg = ErrorMessage.Parse(otr);
                        if (msg != null)
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.PlaintextWithoutTheWhitespaceTag:
                        msg = PlaintextWithoutTheWhitespaceTag.Parse(otr);
                        if (msg != null)
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.PlaintextWithTheWhitespaceTag:
                        msg = PlaintextWithTheWhitespaceTag.Parse(otr);
                        if (msg != null)
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.QueryMessage:
                        msg = QueryMessage.Parse(otr);
                        if (msg != null)
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.RevealSignatureMessage:
                        msg = RevealSignatureMessage.Parse(otr);
                        if (msg == null)
                        {
                            Console.WriteLine("Invalid Reveal Signature Message");
                        }
                        else
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.SignatureMessage:
                        msg = SignatureMessage.Parse(otr);
                        if (msg == null)
                        {
                            Console.WriteLine("Invalid Signature Message");
                        }
                        else
                        {
                            msg.Print();
                        }

                        break;
                    case OTRMessageType.V1KeyExchangeMessage:
                        throw new NotSupportedException();
                }
            }
            catch
            {
                Usage(args);
                Environment.Exit(1);
            }
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
