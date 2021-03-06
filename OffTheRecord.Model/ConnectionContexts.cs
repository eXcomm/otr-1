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

using System;
using System.Collections.ObjectModel;

namespace OffTheRecord.Model
{
    #region Namespaces

    

    #endregion

    public class ConnectionContexts : Collection<ConnectionContext>, IDisposable
    {
        #region Fields

        private bool disposed;

        #endregion

        ~ConnectionContexts()
        {
            if (!disposed)
            {
                Dispose();
            }
        }

        #region Public methods

        public void Dispose()
        {
            disposed = true;

            // release resources;
            // XXX: call dispose on each object within collection, then clear.
            Clear();
        }

        public ConnectionContext GetConnectionContext(Fingerprint fingerprint, bool addIfNotFound = true)
        {
            // XXX: ignoring instance tag for now.
            var cc = new ConnectionContext(fingerprint.Username, fingerprint.AccountName, fingerprint.Protocol);

            foreach (ConnectionContext item in this)
            {
                if (item.Equals(cc))
                {
                    return item;
                }
            }

            if (addIfNotFound)
            {
                Add(cc);
                return cc;
            }

            return null;
        }

        #endregion
    }
}