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
using System.Linq;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect.Configuration
{
    /// <summary>
    /// Defines a class for validating the OpenIdConnectConfiguration by using default policy.
    /// </summary>
    public class OpenIdConnectConfigurationValidator : IConfigurationValidator<OpenIdConnectConfiguration>
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
        /// <returns>A <see cref="ConfigurationValidationResult"/> that contains validation result.</returns>
        public ConfigurationValidationResult Validate(OpenIdConnectConfiguration openIdConnectConfiguration)
        {
            if (openIdConnectConfiguration == null)
                throw new ArgumentNullException(nameof(openIdConnectConfiguration));

            if (openIdConnectConfiguration.JsonWebKeySet == null || openIdConnectConfiguration.JsonWebKeySet.Keys.Count == 0)
            {
                return new ConfigurationValidationResult
                {
                    Exception = new ConfigurationValidationException(LogMessages.IDX21817),
                    Succeeded = false
                };
            }

            if (openIdConnectConfiguration.SigningKeys.Count < MinimumNumberOfKeys)
            {
                if (openIdConnectConfiguration.JsonWebKeySet.ConvertKeyInfos.Any())
                {
                    return new ConfigurationValidationResult
                    {
                        Exception = new ConfigurationValidationException(LogHelper.FormatInvariant(LogMessages.IDX21818, LogHelper.MarkAsNonPII(MinimumNumberOfKeys), string.Join("\n", openIdConnectConfiguration.JsonWebKeySet.ConvertKeyInfos.Select(x => x.Key.Kid.ToString() + ": " + string.Join("; ", x.Value))))),
                        Succeeded = false
                    };
                }
                else
                {
                    return new ConfigurationValidationResult
                    {
                        Exception = new ConfigurationValidationException(LogHelper.FormatInvariant(LogMessages.IDX21819, LogHelper.MarkAsNonPII(MinimumNumberOfKeys), LogHelper.MarkAsNonPII(openIdConnectConfiguration.SigningKeys.Count))),
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
                    throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException(nameof(value), LogHelper.FormatInvariant(LogMessages.IDX21816, LogHelper.MarkAsNonPII(DefaultMinimumNumberOfKeys), LogHelper.MarkAsNonPII(value))));

                _minimumNumberOfKeys = value;
            }
        }
    }
}
