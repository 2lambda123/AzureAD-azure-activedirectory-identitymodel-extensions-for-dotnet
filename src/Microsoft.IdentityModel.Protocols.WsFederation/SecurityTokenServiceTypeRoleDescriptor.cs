﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.IdentityModel.Xml;

namespace Microsoft.IdentityModel.Protocols.WsFederation
{
    /// <summary>
    /// Class for SecurityTokenService type role descriptor
    /// </summary>
    public class SecurityTokenServiceTypeRoleDescriptor
    {
        /// <summary>
        /// KeyInfo
        /// </summary>
        public List<KeyInfo> KeyInfos
        {
            get;
            set;
        } = new List<KeyInfo>();

        /// <summary>
        /// Passive Requestor Token endpoint
        /// </summary>
        public string TokenEndpoint
        {
            get;
            set;
        }

        /// <summary>
        /// Active Requestor Token Endpoint
        /// </summary>
        public string ActiveTokenEndpoint
        {
            get;
            set;
        }
    }
}
