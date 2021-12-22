﻿//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect.Configuration
{
    /// <summary>
    /// Defines a policy for OpenIdConnectConfiguration validation.
    /// </summary>
    public class ConfigurationValidationPolicy : IConfigurationValidationPolicy
    {
        private int _minimumNumberOfKeys = DefaultMinimumNumberOfKeys;

        /// <summary>
        /// 1 is the default minimum number of keys.
        /// </summary>
        public static readonly int DefaultMinimumNumberOfKeys = 1;

        /// <summary>
        /// Validates a OpenIdConnectConfiguration by using current policy.
        /// </summary>
        /// <param name="openIdConnectConfiguration">The OpenIdConnectConfiguration to validate.</param>
        /// <returns></returns>
        public ConfigurationValidationResult ValidateConfiguration(OpenIdConnectConfiguration openIdConnectConfiguration)
        {
            if (openIdConnectConfiguration == null)
                throw new ArgumentNullException(nameof(openIdConnectConfiguration));

            if (openIdConnectConfiguration.JsonWebKeySet == null)
            {
                return new ConfigurationValidationResult
                {
                    Exception = new OpenIdConnectConfigurationValidationException("Invalid configuation: didn't contain any JsonWebKeys."),
                    Succeeded = false
                };
            }

            if (openIdConnectConfiguration.SigningKeys.Count < MinimumNumberOfKeys)
            {
                if (openIdConnectConfiguration.JsonWebKeySet.AdditionalData.ContainsKey("ConvertKeyError"))
                {
                    return new ConfigurationValidationResult
                    {
                        Exception = new OpenIdConnectConfigurationValidationException("Invalid configuation: not enough keys:" + openIdConnectConfiguration.JsonWebKeySet.AdditionalData[JsonWebKeySet.ConvertKeyError]),
                        Succeeded = false
                    };
                }
            }

            return new ConfigurationValidationResult
            {
                Succeeded = true
            };
        }

        /// <summary>
        /// The minimum number of keys.
        /// </summary>
        public int MinimumNumberOfKeys
        {
            get { return _minimumNumberOfKeys; }
            set
            {
                if (value < _minimumNumberOfKeys)
                    throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(nameof(value), LogHelper.FormatInvariant(LogMessages.IDX20808, LogHelper.MarkAsNonPII(DefaultMinimumNumberOfKeys), LogHelper.MarkAsNonPII(value))));

                _minimumNumberOfKeys = value;
            }
        }
    }
}
