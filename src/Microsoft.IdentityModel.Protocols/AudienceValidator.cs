﻿//-----------------------------------------------------------------------
// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
// Apache License 2.0
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal class AudienceValidator
    {
        public static void Validate(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            IdentityModelEventSource.Logger.WriteInformation("validating audience in the jwt token");

            if (audiences == null)
            {
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, ErrorMessages.IDX10000, "audiences"), typeof(ArgumentNullException), EventLevel.Verbose);
            }

            if (validationParameters == null)
            {
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, ErrorMessages.IDX10000, "validationParameters"), typeof(ArgumentNullException), EventLevel.Verbose);
            }

            if (string.IsNullOrWhiteSpace(validationParameters.ValidAudience) && (validationParameters.ValidAudiences == null))
            {
                LogHelper.Throw(ErrorMessages.IDX10208, typeof(SecurityTokenInvalidAudienceException), EventLevel.Error);
            }


            foreach (string audience in audiences)
            {
                if (string.IsNullOrWhiteSpace(audience))
                {
                    continue;
                }

                if (validationParameters.ValidAudiences != null)
                {
                    foreach (string str in validationParameters.ValidAudiences)
                    {
                        if (string.Equals(audience, str, StringComparison.Ordinal))
                        {
                            return;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(validationParameters.ValidAudience))
                {
                    if (string.Equals(audience, validationParameters.ValidAudience, StringComparison.Ordinal))
                    {
                        return;
                    }
                }
            }

            LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, ErrorMessages.IDX10214, Utility.SerializeAsSingleCommaDelimitedString(audiences), validationParameters.ValidAudience ?? "null", Utility.SerializeAsSingleCommaDelimitedString(validationParameters.ValidAudiences)), typeof(SecurityTokenInvalidAudienceException), EventLevel.Error);
        }
    }
}