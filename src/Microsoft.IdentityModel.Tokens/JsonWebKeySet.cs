//------------------------------------------------------------------------------
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
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Json.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Contains a collection of <see cref="JsonWebKey"/> that can be populated from a json string.
    /// </summary>
    /// <remarks>provides support for https://datatracker.ietf.org/doc/html/rfc7517.</remarks>
    [JsonObject]
    public class JsonWebKeySet
    {
        private const string _className = "Microsoft.IdentityModel.Tokens.JsonWebKeySet";

        /// <summary>
        /// Returns a new instance of <see cref="JsonWebKeySet"/>.
        /// </summary>
        /// <param name="json">a string that contains JSON Web Key parameters in JSON format.</param>
        /// <returns><see cref="JsonWebKeySet"/></returns>
        /// <exception cref="ArgumentNullException">If 'json' is null or empty.</exception>
        /// <exception cref="ArgumentException">If 'json' fails to deserialize.</exception>
        public static JsonWebKeySet Create(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw LogHelper.LogArgumentNullException(nameof(json));

            return new JsonWebKeySet(json);
        }

        /// <summary>
        /// Initializes an new instance of <see cref="JsonWebKeySet"/>.
        /// </summary>
        public JsonWebKeySet()
        {
        }

        /// <summary>
        /// Initializes an new instance of <see cref="JsonWebKeySet"/> from a json string.
        /// </summary>
        /// <param name="json">a json string containing values.</param>
        /// <exception cref="ArgumentNullException">If 'json' is null or empty.</exception>
        /// <exception cref="ArgumentException">If 'json' fails to deserialize.</exception>
        public JsonWebKeySet(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw LogHelper.LogArgumentNullException(nameof(json));

            try
            {
                LogHelper.LogVerbose(LogMessages.IDX10806, json, LogHelper.MarkAsNonPII(_className));
                JsonConvert.PopulateObject(json, this);
            }
            catch (Exception ex)
            {
                throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant(LogMessages.IDX10805, json, LogHelper.MarkAsNonPII(_className)), ex));
            }
        }

        /// <summary>
        /// When deserializing from JSON any properties that are not defined will be placed here.
        /// </summary>
        [JsonExtensionData]
        public virtual IDictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets a <see cref="IDictionary{JsonWebKey, ConvertKeyInfo}"/> that contains convert key information.
        /// </summary>
        internal IDictionary<JsonWebKey, List<string>> ConvertKeyInfos { get; } = new Dictionary<JsonWebKey, List<string>>();

        /// <summary>
        /// Gets the <see cref="IList{JsonWebKey}"/>.
        /// </summary>       
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = JsonWebKeySetParameterNames.Keys, Required = Required.Default)]
        public IList<JsonWebKey> Keys { get; private set; } = new List<JsonWebKey>();

        /// <summary>
        /// Default value for the flag that controls whether unresolved JsonWebKeys will be included in the resulting collection of <see cref="GetSigningKeys"/> method.
        /// </summary>
        [DefaultValue(true)]
        public static bool DefaultSkipUnresolvedJsonWebKeys = true;

        /// <summary>
        /// Flag that controls whether unresolved JsonWebKeys will be included in the resulting collection of <see cref="GetSigningKeys"/> method.
        /// </summary>
        [DefaultValue(true)]
        public bool SkipUnresolvedJsonWebKeys { get; set; } = DefaultSkipUnresolvedJsonWebKeys;

        /// <summary>
        /// Returns the JsonWebKeys as a <see cref="IList{SecurityKey}"/>.
        /// </summary>
        /// <remarks>
        /// To include unresolved JsonWebKeys in the resulting <see cref="SecurityKey"/> collection, set <see cref="SkipUnresolvedJsonWebKeys"/> to <c>false</c>.
        /// </remarks>
        public IList<SecurityKey> GetSigningKeys()
        {
            var signingKeys = new List<SecurityKey>();
            var errorList = new List<Exception>();
            foreach (var webKey in Keys)
            {
                // skip if "use" (Public Key Use) parameter is not empty or "sig".
                // https://datatracker.ietf.org/doc/html/rfc7517#section-4.2
                if (!string.IsNullOrEmpty(webKey.Use) && !webKey.Use.Equals(JsonWebKeyUseNames.Sig, StringComparison.Ordinal))
                {
                    string convertKeyInfo = LogHelper.FormatInvariant(LogMessages.IDX10808, webKey, webKey.Use);
                    LogHelper.LogInformation(convertKeyInfo);
                    ConvertKeyInfos.Add(webKey, new List<string> { convertKeyInfo });

                    if (!SkipUnresolvedJsonWebKeys)
                        signingKeys.Add(webKey);

                    continue;
                }

                if (JsonWebAlgorithmsKeyTypes.RSA.Equals(webKey.Kty, StringComparison.Ordinal))
                {
                    var rsaKeyResolved = true;

                    // in this case, even though RSA was specified, we can't resolve.
                    if ((webKey.X5c == null || webKey.X5c.Count == 0) && (string.IsNullOrEmpty(webKey.E) && string.IsNullOrEmpty(webKey.N)))
                    {                     
                        var missingComponent = new List<string> { JsonWebKeyParameterNames.X5c, JsonWebKeyParameterNames.E, JsonWebKeyParameterNames.N };
                        string convertKeyInfo = LogHelper.FormatInvariant(LogMessages.IDX10814, LogHelper.MarkAsNonPII(typeof(RsaSecurityKey)), webKey, string.Join(", ", missingComponent));
                        ConvertKeyInfos.Add(webKey, new List<string> { convertKeyInfo });
                        rsaKeyResolved = false;
                    }
                    else
                    {
                        // in this case X509SecurityKey should be resolved.
                        if (IsValidX509SecurityKey(webKey, ConvertKeyInfos))
                            if (JsonWebKeyConverter.TryConvertToX509SecurityKey(webKey, ConvertKeyInfos, out SecurityKey x509SecurityKey))
                                signingKeys.Add(x509SecurityKey);
                            else
                                rsaKeyResolved = false;

                        // in this case RsaSecurityKey should be resolved.
                        if (IsValidRsaSecurityKey(webKey, ConvertKeyInfos))
                            if (JsonWebKeyConverter.TryCreateToRsaSecurityKey(webKey, ConvertKeyInfos, out SecurityKey rsaSecurityKey))
                                signingKeys.Add(rsaSecurityKey);
                            else
                                rsaKeyResolved = false;
                    }

                    if (!rsaKeyResolved && !SkipUnresolvedJsonWebKeys)
                        signingKeys.Add(webKey);
                }
                else if (JsonWebAlgorithmsKeyTypes.EllipticCurve.Equals(webKey.Kty, StringComparison.Ordinal))
                {
                    if (JsonWebKeyConverter.TryConvertToECDsaSecurityKey(webKey, ConvertKeyInfos, out SecurityKey ecSecurityKey))
                        signingKeys.Add(ecSecurityKey);
                    else if (!SkipUnresolvedJsonWebKeys)
                        signingKeys.Add(webKey);
                }
                else
                {
                    LogHelper.LogInformation(LogHelper.FormatInvariant(LogMessages.IDX10810, webKey));
                    ConvertKeyInfos.Add(webKey, new List<string> { LogMessages.IDX10810 });

                    if (!SkipUnresolvedJsonWebKeys)
                        signingKeys.Add(webKey);
                }
            }

            return signingKeys;
        }

        private static bool IsValidX509SecurityKey(JsonWebKey webKey, IDictionary<JsonWebKey, List<string>> convertKeyInfos)
        {
            if (webKey.X5c == null || webKey.X5c.Count == 0)
            {
                convertKeyInfos.Add(webKey, new List<string> { LogHelper.FormatInvariant(LogMessages.IDX10814, LogHelper.MarkAsNonPII(typeof(X509SecurityKey)), webKey, JsonWebKeyParameterNames.X5c) });
                return false;
            }

            return true;
        }

        private static bool IsValidRsaSecurityKey(JsonWebKey webKey, IDictionary<JsonWebKey, List<string>> convertKeyInfos)
        {
            var missingComponent = new List<string>();
            if (string.IsNullOrWhiteSpace(webKey.E))
                missingComponent.Add(JsonWebKeyParameterNames.E);

            if (string.IsNullOrWhiteSpace(webKey.N))
                missingComponent.Add(JsonWebKeyParameterNames.N);

            if (missingComponent.Count > 0)
            {
                string convertKeyInfo = LogHelper.FormatInvariant(LogMessages.IDX10814, LogHelper.MarkAsNonPII(typeof(RsaSecurityKey)), webKey, string.Join(", ", missingComponent));
                if (convertKeyInfos.ContainsKey(webKey))
                    convertKeyInfos[webKey].Add(convertKeyInfo);
                else
                    convertKeyInfos.Add(webKey, new List<string> { convertKeyInfo });
            }

            return missingComponent.Count == 0;
        }
    }
}
