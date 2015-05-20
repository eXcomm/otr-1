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
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Usage(args);
                return;
            }

            Stream s = Console.OpenStandardInput();

            string otr = General.ReadOtr(s);

            try
            {
                Parse(otr);
            }
            catch (ArgumentNullException)
            {
                Usage(args);
                return;
            }
            catch (Exception)
            {
                Usage(args);
                Environment.Exit(1);
            }
        }

        public static void Parse(string otr)
        {
            if (otr == null)
            {
                throw new ArgumentNullException("otr");
            }

            Log.DebugFormat("Found Off-the-Record message: {0}", otr);

            OtrMessageType type = BaseOTRMessage.GetType(otr);

            Log.DebugFormat("Off-the-Record MessageType: {0}", type);

            switch (type)
            {
                case OtrMessageType.DataMessage:
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
                case OtrMessageType.DhCommitMessage:
                    msg = DhCommitMessage.Parse(otr);
                    if (msg == null)
                    {
                        Console.WriteLine("Invalid D-H Commit Message");
                    }
                    else
                    {
                        msg.Print();
                    }

                    break;
                case OtrMessageType.DhKeyMessage:
                    msg = DhKeyMessage.Parse(otr);
                    if (msg == null)
                    {
                        Console.WriteLine("Invalid D-H Key Message");
                    }
                    else
                    {
                        msg.Print();
                    }

                    break;
                case OtrMessageType.ErrorMessage:
                    msg = ErrorMessage.Parse(otr);
                    if (msg != null)
                    {
                        msg.Print();
                    }

                    break;
                case OtrMessageType.PlaintextWithoutTheWhitespaceTag:
                    msg = PlaintextWithoutTheWhitespaceTag.Parse(otr);
                    if (msg != null)
                    {
                        msg.Print();
                    }

                    break;
                case OtrMessageType.PlaintextWithTheWhitespaceTag:
                    msg = PlaintextWithTheWhitespaceTag.Parse(otr);
                    if (msg != null)
                    {
                        msg.Print();
                    }

                    break;
                case OtrMessageType.QueryMessage:
                    msg = QueryMessage.Parse(otr);
                    if (msg != null)
                    {
                        msg.Print();
                    }

                    break;
                case OtrMessageType.RevealSignatureMessage:
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
                case OtrMessageType.SignatureMessage:
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
                case OtrMessageType.V1KeyExchangeMessage:
                    throw new NotSupportedException();
            }
        }

        private static void Usage(string[] args)
        {
            string errorMsg = string.Format(
                "Usage: {0}{1}" +
                "Read Off-the-Record (OTR) Key Exchange and/or Data messages from stdin{1}" +
                "and display their contents in a more readable format.{1}",
                args[0],
                Environment.NewLine);
            Console.WriteLine(errorMsg);
            return;
        }
    }
}