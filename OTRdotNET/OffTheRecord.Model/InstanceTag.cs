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

namespace OffTheRecord.Model
{
    public class InstanceTag
    {
        #region Public properties
        public string AccountName { get; set; }
        public string Protocol { get; set; }
        //public uint InstanceTag { get; set; }
        #endregion

        /////* Fetch the instance tag from the given OtrlUserState associated with
        //// * the given account */
        ////OtrlInsTag * otrl_instag_find(OtrlUserState us, const char *accountname,
        ////    const char *protocol);

        /////* Read our instance tag from a file on disk into the given
        //// * OtrlUserState. */
        ////gcry_error_t otrl_instag_read(OtrlUserState us, const char *filename);

        /////* Read our instance tag from a file on disk into the given
        //// * OtrlUserState. The FILE* must be open for reading. */
        ////gcry_error_t otrl_instag_read_FILEp(OtrlUserState us, FILE *instf);

        /////* Return a new valid instance tag */
        ////otrl_instag_t otrl_instag_get_new();

        /////* Get a new instance tag for the given account and write to file*/
        ////gcry_error_t otrl_instag_generate(OtrlUserState us, const char *filename,
        ////    const char *accountname, const char *protocol);

        /////* Get a new instance tag for the given account and write to file
        //// * The FILE* must be open for writing. */
        ////gcry_error_t otrl_instag_generate_FILEp(OtrlUserState us, FILE *instf,
        ////    const char *accountname, const char *protocol);

        /////* Write our instance tags to a file on disk. */
        ////gcry_error_t otrl_instag_write(OtrlUserState us, const char *filename);

        /////* Write our instance tags to a file on disk.
        //// * The FILE* must be open for writing. */
        ////gcry_error_t otrl_instag_write_FILEp(OtrlUserState us, FILE *instf);
    }
}