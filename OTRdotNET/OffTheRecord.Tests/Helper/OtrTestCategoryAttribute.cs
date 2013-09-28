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

namespace OffTheRecord.Tests.Helper
{
    #region Namespaces
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    #endregion

    /// <summary>
    /// Off-the-Record test categories.
    /// </summary>
    public enum OtrTestCategories
    {
        /// <summary>
        /// Off-the-Record Core-components related test.
        /// </summary>
        Core,

        /// <summary>
        /// Off-the-Record Toolkit.Parse related test.
        /// </summary>
        ToolkitParse,

        /// <summary>
        /// Off-the-Record Toolkit.Sesskey related test.
        /// </summary>
        ToolkitSesskey,

        /// <summary>
        /// Off-the-Record encryption-components related test.
        /// </summary>
        Encryption,

        /// <summary>
        /// Off-the-Record utilities/tools related tests.
        /// </summary>
        Tools,

        /// <summary>
        /// General component testing, not specific to Off-the-Record.
        /// </summary>
        General,
    }

    /// <summary>
    /// Off-the-Record custom TestCategoryAttribute.
    /// </summary>
    public class OtrTestCategoryAttribute : TestCategoryBaseAttribute
    {
        #region Fields
        private List<OtrTestCategories> testcategories = new List<OtrTestCategories>();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OtrTestCategoryAttribute"/> class.
        /// </summary>
        /// <param name="args">A list of <see cref="OtrTestCategories"/> the test belongs too.</param>
        public OtrTestCategoryAttribute(params OtrTestCategories[] args)
        {
            foreach (var item in args)
            {
                this.testcategories.Add(item);
            }
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the list of <see cref="OtrTestCategories"/> this test belongs too.
        /// </summary>
        public override IList<string> TestCategories
        {
            get { return this.testcategories.Select(x => x.ToString()).ToList(); }
        }
        #endregion
    }
}