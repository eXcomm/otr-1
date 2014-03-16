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

namespace OffTheRecord.Model.Files.OtrPrivateKey
{
    #region namespaces
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// dsa class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public sealed class dsa
    {
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="dsa"/> class.
        /// </summary>
        public dsa()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="dsa"/> class.
        /// </summary>
        /// <param name="p">p parameter.</param>
        /// <param name="q">q parameter.</param>
        /// <param name="g">g parameter.</param>
        /// <param name="y">y parameter.</param>
        /// <param name="x">x paramater.</param>
        public dsa(string p, string q, string g, string y, string x)
        {
            this.p = p;
            this.q = q;
            this.g = g;
            this.y = y;
            this.x = x;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the p parameter.
        /// </summary>
        public string p { get; set; }

        /// <summary>
        /// Gets or sets the q parameter.
        /// </summary>
        public string q { get; set; }

        /// <summary>
        /// Gets or sets the g parameter.
        /// </summary>
        public string g { get; set; }

        /// <summary>
        /// Gets or sets the y parameter.
        /// </summary>
        public string y { get; set; }

        /// <summary>
        /// Gets or sets the x parameter.
        /// </summary>
        public string x { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the <see cref="DSAParameters"/> representation of the DSA key.
        /// </summary>
        /// <param name="includePrivateParameters">Whether the private part of the key should be included.</param>
        /// <returns>A <see cref="DSAParameters"/> object.</returns>
        public DSAParameters GetDSAParameters(bool includePrivateParameters)
        {
            DSAParameters param = new DSAParameters();
            param.X = Tools.General.StringToByteArray(this.x);
            param.P = Tools.General.StringToByteArray(this.p);
            param.Q = Tools.General.StringToByteArray(this.q);
            param.G = Tools.General.StringToByteArray(this.g);
            param.Y = Tools.General.StringToByteArray(this.y);

            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider(1024);
            dsa.ImportParameters(param);
            DSAParameters output = dsa.ExportParameters(includePrivateParameters);

            return output;
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Deserialize string to a <see cref="dsa"/> objects.
        /// </summary>
        /// <param name="item">Serialized string.</param>
        /// <returns>A <see cref="dsa"/> objects.</returns>
        internal static dsa Deserialize(Item item)
        {
            if (!item.Value.StartsWith("(dsa"))
            {
                throw new ArgumentException("incorrect format");
            }

            dsa dsa = new dsa();

            try
            {
                foreach (var child in item.Children)
                {
                    if (child.Value.StartsWith("(p"))
                    {
                        dsa.p = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(q"))
                    {
                        dsa.q = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(g"))
                    {
                        dsa.g = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(y"))
                    {
                        dsa.y = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                    else if (child.Value.StartsWith("(x"))
                    {
                        dsa.x = child.Value.Split(' ')[1].TrimEnd(')').Trim('#');
                    }
                }
            }
            catch
            {
                throw new Exception("Parse exception");
            }

            return dsa;
        }

        /// <summary>
        /// Serialize object.
        /// </summary>
        /// <returns>Serialized string.</returns>
        internal string Serialize()
        {
            return string.Format(" (dsa {0}  (p #{1}#){0}  (q #{2}#){0}  (g #{3}#){0}  (y #{4}#){0}  (x #{5}#){0}  ){0}", Environment.NewLine, this.p, this.q, this.g, this.y, this.x);
        }
        #endregion
    }
}