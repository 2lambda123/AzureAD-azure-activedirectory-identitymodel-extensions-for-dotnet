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

using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Test;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Test
{
    public class CompareContext
    {
        Dictionary<string,  Dictionary<string, object>> _errors = new Dictionary<string,  Dictionary<string, object>>();
        public CompareContext()
        {
            IgnoreSubject = true;
            StringComparison = System.StringComparison.Ordinal;
        }

        public static CompareContext Default = new CompareContext();

        public Dictionary<string, Dictionary<string, object>> Errors { get { return _errors; } }
        public bool ExpectRawData { get; set; }
        public bool IgnoreProperties { get; set; }
        public bool IgnoreSubject { get; set; }
        public bool IgnoreType { get; set; }
        public StringComparison StringComparison { get; set; }
    }

    public class IdentityComparer
    {
        private static bool AreEnumsEqual<T>(IEnumerable<T> t1, IEnumerable<T> t2, CompareContext context, Func<T, T, CompareContext, bool> areEqual)
        {
            if (t1 == null && t2 == null)
                return true;

            if (t1 == null || t2 == null)
                return false;

            if (object.ReferenceEquals(t1, t2))
                return true;

            int numToMatch = 0;
            int numMatched = 0;

            List<T> toMatch = new List<T>(t2);
            List<T> expectedToMatch = new List<T>(t1);
            List<KeyValuePair<T,T>> matchedTs = new List<KeyValuePair<T,T>>();
            
            // helps debugging to see what didn't match
            List<T> notMatched = new List<T>();
            foreach (var t in t1)
            {
                numToMatch++;
                bool matched = false;
                for (int i = 0; i < toMatch.Count; i++)
                {
                    if (areEqual(t, toMatch[i], context))
                    {
                        numMatched++;
                        matchedTs.Add(new KeyValuePair<T, T>(toMatch[i], t));
                        matched = true;
                        toMatch.RemoveAt(i);
                        break;
                    }
                }

                if (!matched)
                {
                    notMatched.Add(t);
                }
            }

            return (toMatch.Count == 0 && numMatched == numToMatch && notMatched.Count == 0);
        }

        public static bool AreEqual<T>(T t1, T t2)
        {
            return AreEqual<T>(t1, t2, CompareContext.Default);
        }

        public static bool AreEqual<T>(T t1, T t2, CompareContext context)
        {
            if (t1 is TokenValidationParameters)
                return AreEqual<TokenValidationParameters>(t1 as TokenValidationParameters, t2 as TokenValidationParameters, context, AreTokenValidationParametersEqual);
            else if (t1 is ClaimsIdentity)
                return AreEqual<ClaimsIdentity>(t1 as ClaimsIdentity, t2 as ClaimsIdentity, context, AreClaimsIdentitiesEqual);
            else if (t1 is ClaimsPrincipal)
                return AreEqual<ClaimsPrincipal>(t1 as ClaimsPrincipal, t2 as ClaimsPrincipal, context, AreClaimsPrincipalsEqual);
            else if (t1 is IDictionary<string, string>)
                return AreEqual<Dictionary<string, string>>(t1 as Dictionary<string, string>, t2 as Dictionary<string, string>, context, AreDictionariesEqual);
            else if (t1 is JsonWebKey)
                return AreEqual<JsonWebKey>(t1 as JsonWebKey, t2 as JsonWebKey, context, AreJsonWebKeysEqual);
            else if (t1 is JsonWebKeySet)
                return AreEqual<JsonWebKeySet>(t1 as JsonWebKeySet, t2 as JsonWebKeySet, context, AreJsonWebKeyKeySetsEqual);
            else if (t1 is JwtPayload)
                return AreEqual<JwtPayload>(t1 as JwtPayload, t2 as JwtPayload, context, AreJwtPayloadsEqual);
            else if (t1 is JwtSecurityToken)
                return AreEqual<JwtSecurityToken>(t1 as JwtSecurityToken, t2 as JwtSecurityToken, context, AreJwtSecurityTokensEqual);
            else if (t1 is OpenIdConnectConfiguration)
                return AreEqual<OpenIdConnectConfiguration>(t1 as OpenIdConnectConfiguration, t2 as OpenIdConnectConfiguration, context, AreOpenIdConnectConfigurationEqual);
            else if (t1 is IEnumerable<Claim>)
                return AreEnumsEqual<Claim>(t1 as IEnumerable<Claim>, t2 as IEnumerable<Claim>, context, AreClaimsEqual);
            else if (t1 is IEnumerable<SecurityKey>)
                return AreEnumsEqual<SecurityKey>(t1 as IEnumerable<SecurityKey>, t2 as IEnumerable<SecurityKey>, context, AreSecurityKeysEqual);
            else if (t1 is IEnumerable<string>)
                return AreEnumsEqual<string>(t1 as IEnumerable<string>, t2 as IEnumerable<string>, context, AreStringsEqual);

            throw new InvalidOperationException("type not known");
        }

        public static bool AreEqual<T>(T t1, T t2, CompareContext context, Func<T, T, CompareContext, bool> areEqual)
        {
            if (t1 == null && t2 == null)
                return true;

            if (t1 == null || t2 == null)
                return false;

            if (object.ReferenceEquals(t1, t2))
                return true;

            return areEqual(t1, t2, context);
        }

        private static bool AreBootstrapContextsEqual(BootstrapContext bc1, BootstrapContext bc2, CompareContext context)
        {
            if (!AreEqual<SecurityToken>(bc1.SecurityToken, bc2.SecurityToken, context, AreSecurityTokensEqual))
                return false;

            if (!AreEqual<string>(bc1.Token, bc2.Token, context, AreStringsEqual))
                return false;

            return true;
        }

        private static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1.Length != bytes2.Length)
            {
                return false;
            }

            for (int i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreClaimsEqual(Claim claim1, Claim claim2, CompareContext context)
        {
            Dictionary<string, object> matchingFailures = new Dictionary<string, object>();
            if (claim1.Type != claim2.Type)
            {
                matchingFailures.Add("Type", new KeyValuePair<string, string>(claim1.Type, claim2.Type));
            }

            if (claim1.Issuer != claim2.Issuer)
            {
                matchingFailures.Add("Issuer", new KeyValuePair<string, string>(claim1.Issuer, claim2.Issuer));
            }

            if (claim1.OriginalIssuer != claim2.OriginalIssuer)
            { 
                matchingFailures.Add("OriginalIssuer", new KeyValuePair<string, string>(claim1.OriginalIssuer, claim2.OriginalIssuer));
            }

            if (!context.IgnoreProperties && !AreEqual<IDictionary<string, string>>(claim1.Properties, claim2.Properties, context, AreDictionariesEqual))
            {
                matchingFailures.Add("Properties", new KeyValuePair<IDictionary<string, string>, IDictionary<string, string>>(claim1.Properties, claim2.Properties));
            }

            if (claim1.Value != claim2.Value)
            {
                matchingFailures.Add("Value", new KeyValuePair<string, string>(claim1.Value, claim2.Value));
            }

            if (claim1.ValueType != claim2.ValueType)
            {
                matchingFailures.Add("ValueType", new KeyValuePair<string, string>(claim1.ValueType, claim2.ValueType));
            }

            if (!context.IgnoreSubject && !AreEqual<ClaimsIdentity>(claim1.Subject, claim2.Subject, context, AreClaimsIdentitiesEqual))
            {
                matchingFailures.Add("Subject", new KeyValuePair<ClaimsIdentity, ClaimsIdentity>(claim1.Subject, claim2.Subject));
            }

            return matchingFailures.Count == 0;
        }

        private static bool AreClaimsIdentitiesEqual(ClaimsIdentity ci1, ClaimsIdentity ci2, CompareContext context)
        {
            Dictionary<string, object> matchingFailures = new Dictionary<string, object>();

            if (!string.Equals(ci1.AuthenticationType, ci2.AuthenticationType, context.StringComparison))
                matchingFailures.Add("AuthenticationType", new KeyValuePair<string, string>(ci1.AuthenticationType, ci2.AuthenticationType));

            if (!string.Equals(ci1.Label, ci2.Label, context.StringComparison))
                matchingFailures.Add("Label", new KeyValuePair<string, string>(ci1.Label, ci2.Label));

            if (!string.Equals(ci1.Name, ci2.Name, context.StringComparison))
                matchingFailures.Add("Name", new KeyValuePair<string, string>(ci1.Name, ci2.Name));

            if (!string.Equals(ci1.NameClaimType, ci2.NameClaimType))
                matchingFailures.Add("NameClaimType", new KeyValuePair<string, string>(ci1.NameClaimType, ci2.NameClaimType));

            if (!string.Equals(ci1.RoleClaimType, ci2.RoleClaimType))
                matchingFailures.Add("RoleClaimType", new KeyValuePair<string, string>(ci1.RoleClaimType, ci2.RoleClaimType));

            if (!AreEnumsEqual<Claim>(ci1.Claims, ci2.Claims, context, AreClaimsEqual))
                matchingFailures.Add("Claims", new KeyValuePair<string, string>("ci1.Claims", "ci2.Claims"));

            if (ci1.IsAuthenticated != ci2.IsAuthenticated)
                matchingFailures.Add("IsAuthenticated", new KeyValuePair<bool, bool>(ci1.IsAuthenticated, ci2.IsAuthenticated));

            if (!AreEqual<ClaimsIdentity>(ci1.Actor, ci2.Actor, context, AreClaimsIdentitiesEqual))
                matchingFailures.Add("Actor", new KeyValuePair<string, string>("ci1.Actor", "ci2.Actor"));

            if (!context.IgnoreType && (ci1.GetType() != ci2.GetType()))
                matchingFailures.Add("GetType", new KeyValuePair<string, string>(ci1.GetType().ToString(), ci2.GetType().ToString()));

            // || TODO compare bootstrapcontext

            if (matchingFailures.Count != 0)
            {
                Dictionary<string, object> findMatchingFailures = null;
                if (context.Errors.TryGetValue("AreClaimsIdentitiesEqual", out findMatchingFailures))
                {
                }
                else
                {
                    context.Errors["AreClaimsIdentitiesEqual"] = matchingFailures;
                }
            }

            return matchingFailures.Count == 0;
        }

        private static bool AreClaimsPrincipalsEqual(ClaimsPrincipal principal1, ClaimsPrincipal principal2, CompareContext context)
        {
            if (!context.IgnoreType)
            {
                if (principal1.GetType() != principal2.GetType())
                    return false;
            }

            int numMatched = 0;
            int numToMatch = 0;
            List<ClaimsIdentity> identities2 = new List<ClaimsIdentity>(principal2.Identities);
            foreach (ClaimsIdentity identity in principal1.Identities)
            {
                numToMatch++;
                for (int i = 0; i < identities2.Count; i++)
                {
                    if (AreEqual<ClaimsIdentity>(identity, identities2[i], context, AreClaimsIdentitiesEqual))
                    {
                        numMatched++;
                        identities2.RemoveAt(i);
                        break;
                    }
                }
            }

            return identities2.Count == 0 && numToMatch == numMatched;
        }

        private static bool AreDictionariesEqual(IDictionary<string, string> dictionary1, IDictionary<string, string> dictionary2, CompareContext context)
        {
            if (dictionary1.Count != dictionary2.Count)
                return false;

            // have to assume same order here
            int numMatched = 0;
            foreach (KeyValuePair<string, string> kvp1 in dictionary1)
            {
                foreach (string key in dictionary2.Keys)
                {
                    if (kvp1.Key == key)
                    {
                        if (kvp1.Value != dictionary2[key])
                            return false;
                        numMatched++;
                        break;
                    }
                }
            }

            return numMatched == dictionary1.Count;
        }

        private static bool AreJsonWebKeysEqual(JsonWebKey jsonWebkey1, JsonWebKey jsonWebkey2, CompareContext context)
        {
            List<string> errors = new List<string>();
            if (!string.Equals(jsonWebkey1.Alg, jsonWebkey2.Alg, context.StringComparison))
            {
                errors.Add("jsonWebkey1.Alg != jsonWebkey2.Alg: " + jsonWebkey1.Alg ?? "null" + ", " + jsonWebkey2.Alg ?? "null");
            }

            if (!string.Equals(jsonWebkey1.KeyOps, jsonWebkey2.KeyOps, context.StringComparison))
            {
                errors.Add("jsonWebkey1.KeyOps != jsonWebkey2.KeyOps: " + jsonWebkey1.KeyOps + ", " + jsonWebkey2.KeyOps);
            }

            if (!string.Equals(jsonWebkey1.Kid, jsonWebkey2.Kid, context.StringComparison))
            {
                errors.Add("jsonWebkey1.Kid != jsonWebkey2.Kid: " + jsonWebkey1.Kid + ", " + jsonWebkey2.Kid);
            }

            if (!string.Equals(jsonWebkey1.Kty, jsonWebkey2.Kty, context.StringComparison))
            {
                errors.Add("jsonWebkey1.Kty != jsonWebkey2.Kty: " + jsonWebkey1.Kty + ", " + jsonWebkey2.Kty);
            }

            if (!string.Equals(jsonWebkey1.Use, jsonWebkey2.Use, context.StringComparison))
            {
                errors.Add("jsonWebkey1.Use != jsonWebkey2.Use: " + (jsonWebkey1.Use ?? "null") + ", " + (jsonWebkey2.Use ?? "null"));
            }

            if (!string.Equals(jsonWebkey1.X5t, jsonWebkey2.X5t, context.StringComparison))
            {
                errors.Add("jsonWebkey1.X5t != jsonWebkey2.X5t" + jsonWebkey1.X5t + ", " + jsonWebkey2.X5t);
            }

            if (!string.Equals(jsonWebkey1.X5u, jsonWebkey2.X5u, context.StringComparison))
            {
                errors.Add("jsonWebkey1.X5u != jsonWebkey2.X5u: " + jsonWebkey1.X5u + ", " + jsonWebkey2.X5u);
            }

            if (!AreEnumsEqual<string>(jsonWebkey1.X5c, jsonWebkey2.X5c, context, AreStringsEqual))
            {
                errors.Add("jsonWebkey1.X5c != jsonWebkey2.X5c: " + jsonWebkey1.X5c + ", " + jsonWebkey2.X5c);
            }

            return (errors.Count == 0);
        }

        private static bool AreJsonWebKeyKeySetsEqual(JsonWebKeySet jsonWebKeySet1, JsonWebKeySet jsonWebKeySet2, CompareContext context)
        {
            if (!AreEnumsEqual<JsonWebKey>(jsonWebKeySet1.Keys, jsonWebKeySet2.Keys, context, AreJsonWebKeysEqual))
            {
                return false;
            }

            return true;
        }

        private static bool AreJwtHeadersEqual(JwtHeader header1, JwtHeader header2, CompareContext context)
        {
            if (header1.Count != header2.Count)
            {
                return false;
            }

            return true;
        }

        private static bool AreJwtPayloadsEqual(JwtPayload payload1, JwtPayload payload2, CompareContext context)
        {
            if (!AreEnumsEqual<Claim>(payload1.Claims, payload2.Claims, context, AreClaimsEqual))
            {
                return false;
            }

            return true;
        }

        private static bool AreJwtSecurityTokensEqual(JwtSecurityToken jwt1, JwtSecurityToken jwt2, CompareContext context)
        {
            if (!AreEqual<JwtHeader>(jwt1.Header, jwt2.Header, context, AreJwtHeadersEqual))
                return false;

            if (!AreEqual<JwtPayload>(jwt1.Payload, jwt2.Payload, context, AreJwtPayloadsEqual))
                return false;

            if (!AreEnumsEqual<Claim>(jwt1.Claims, jwt2.Claims, context, AreClaimsEqual))
                return false;

            if (!string.Equals(jwt1.Actor, jwt2.Actor, context.StringComparison))
                return false;

            if (!AreEnumsEqual<string>(jwt1.Audiences, jwt2.Audiences, context, AreStringsEqual))
                return false;

            if (!string.Equals(jwt1.Id, jwt2.Id, context.StringComparison))
                return false;

            if (!string.Equals(jwt1.Issuer, jwt2.Issuer, context.StringComparison))
                return false;

            if (context.ExpectRawData && !string.Equals( jwt1.RawData, jwt2.RawData, context.StringComparison))
                return false;

            if (!string.Equals(jwt1.SignatureAlgorithm, jwt2.SignatureAlgorithm, context.StringComparison))
                return false;

            if (jwt1.ValidFrom != jwt2.ValidFrom)
                return false;

            if (jwt1.ValidTo != jwt2.ValidTo)
                return false;

            if (!AreEnumsEqual<SecurityKey>(jwt1.SecurityKeys, jwt2.SecurityKeys, context, AreSecurityKeysEqual))
                return false;

            // no reason to check keys, as they are always empty for now.
            //ReadOnlyCollection<SecurityKey> keys = jwtWithEntity.SecurityKeys;

            return true;
        }
        
        private static bool AreOpenIdConnectConfigurationEqual(OpenIdConnectConfiguration configuration1, OpenIdConnectConfiguration configuraiton2, CompareContext context)
        {
            if (!string.Equals(configuration1.AuthorizationEndpoint, configuraiton2.AuthorizationEndpoint, context.StringComparison))
                return false;

            if (!string.Equals(configuration1.CheckSessionIframe, configuraiton2.CheckSessionIframe, context.StringComparison))
                return false;

            if (!string.Equals(configuration1.EndSessionEndpoint, configuraiton2.EndSessionEndpoint, context.StringComparison))
                return false;

            if (!string.Equals(configuration1.Issuer, configuraiton2.Issuer, context.StringComparison))
                return false;

            if (!string.Equals(configuration1.JwksUri, configuraiton2.JwksUri, context.StringComparison))
                return false;

            if (!AreEnumsEqual<SecurityToken>(configuration1.SigningTokens, configuraiton2.SigningTokens, context, AreSecurityTokensEqual))
                return false;

            if (!AreEnumsEqual<SecurityKey>(configuration1.SigningKeys, configuraiton2.SigningKeys, context, AreSecurityKeysEqual))
                return false;

            if (!string.Equals(configuration1.TokenEndpoint, configuraiton2.TokenEndpoint, context.StringComparison))
                return false;

            if (!string.Equals(configuration1.UserInfoEndpoint, configuraiton2.UserInfoEndpoint, context.StringComparison))
                return false;

            return true;
        }
        
        private static bool AreSecurityKeyIdentifiersEqual(SecurityKeyIdentifier ski1, SecurityKeyIdentifier ski2, CompareContext context)
        {
            if (ski1.GetType() == ski1.GetType())
                return false;

            if (ski1.Count != ski2.Count)
                return false;

            return true;
        }
     
        private static bool AreSecurityKeysEqual(SecurityKey securityKey1, SecurityKey securityKey2, CompareContext context)
        {
            if (context.IgnoreType && (securityKey1.GetType() != securityKey2.GetType()))
                return false;

            // Check X509SecurityKey first so we don't have to use reflection to get cert.
            X509SecurityKey x509Key1 = securityKey1 as X509SecurityKey;
            if (x509Key1 != null)
            {
                X509SecurityKey x509Key2 = securityKey2 as X509SecurityKey;
                if (x509Key1.Certificate.Thumbprint == x509Key2.Certificate.Thumbprint)
                    return true;

                return false;
            }

            X509AsymmetricSecurityKey x509AsymmKey1 = securityKey1 as X509AsymmetricSecurityKey;
            if (x509AsymmKey1 != null)
            {
                X509AsymmetricSecurityKey x509AsymmKey2 = securityKey2 as X509AsymmetricSecurityKey;
                X509Certificate2 x509Cert1 = TestUtilities.GetField(x509AsymmKey1, "certificate") as X509Certificate2;
                X509Certificate2 x509Cert2 = TestUtilities.GetField(x509AsymmKey2, "certificate") as X509Certificate2;
                if (x509Cert1 == null && x509Cert2 == null)
                    return true;

                if (x509Cert1 == null || x509Cert2 == null)
                    return false;

                if (x509Cert1.Thumbprint != x509Cert2.Thumbprint)
                    return false;
            }

            SymmetricSecurityKey symKey1 = securityKey1 as SymmetricSecurityKey;
            if (symKey1 != null)
            {
                SymmetricSecurityKey symKey2 = securityKey2 as SymmetricSecurityKey;
                if (!AreBytesEqual(symKey1.GetSymmetricKey(), symKey2.GetSymmetricKey()))
                    return false;
            }

            RsaSecurityKey rsaKey = securityKey1 as RsaSecurityKey;
            if (rsaKey != null)
            {
                RSA rsa1 = (rsaKey.GetAsymmetricAlgorithm(SecurityAlgorithms.RsaSha256Signature, false)) as RSA;
                RSA rsa2 = ((securityKey2 as RsaSecurityKey).GetAsymmetricAlgorithm(SecurityAlgorithms.RsaSha256Signature, false)) as RSA;

                if (!string.Equals(rsa1.ToXmlString(false), rsa2.ToXmlString(false), StringComparison.Ordinal))
                    return false;
            }

            return true;
        }

        private static bool AreSecurityTokensEqual(SecurityToken token1, SecurityToken token2, CompareContext context)
        {
            if (token1.GetType() != token2.GetType())
                return false;

            if (!AreEnumsEqual<SecurityKey>(token1.SecurityKeys, token2.SecurityKeys, CompareContext.Default, AreSecurityKeysEqual))
                return false;

            return true;
        }

        private static bool AreSigningCredentialsEqual(SigningCredentials cred1, SigningCredentials cred2, CompareContext context)
        {
            if (cred1.GetType() != cred2.GetType())
                return false;

            if (!string.Equals(cred1.DigestAlgorithm, cred2.DigestAlgorithm, context.StringComparison))
                return false;

            if (!string.Equals(cred1.SignatureAlgorithm, cred2.SignatureAlgorithm, context.StringComparison))
                return false;

            // SigningKey, null match and type
            if (!AreEqual<SecurityKey>(cred1.SigningKey, cred2.SigningKey, context, AreSecurityKeysEqual))
                return false;

            if (!AreEqual<SecurityKeyIdentifier>(cred1.SigningKeyIdentifier, cred2.SigningKeyIdentifier, context, AreSecurityKeyIdentifiersEqual))
                return false;

            return true;
        }

        private static bool AreStringsEqual(string string1, string string2, CompareContext context)
        {
            return string.Equals(string1, string2, context.StringComparison);
        }

        private static bool AreTokenValidationParametersEqual(TokenValidationParameters validationParameters1, TokenValidationParameters validationParameters2, CompareContext context)
        {
            HashSet<string> matchingFailures = new HashSet<string>();

            if ((validationParameters1.AudienceValidator == null && validationParameters2.AudienceValidator != null) || (validationParameters1.AudienceValidator != null && validationParameters2.AudienceValidator == null))
                matchingFailures.Add("AudienceValidator");

            if (validationParameters1.AuthenticationType != validationParameters2.AuthenticationType)
                matchingFailures.Add("AuthenticationType");

            if ((validationParameters1.CertificateValidator == null && validationParameters2.CertificateValidator != null) || (validationParameters1.CertificateValidator != null && validationParameters2.CertificateValidator == null))
                matchingFailures.Add("CertificateValidator");

            if (validationParameters1.CertificateValidator != null)
            { 
                if (validationParameters1.CertificateValidator.GetType() != validationParameters2.CertificateValidator.GetType())
                    matchingFailures.Add("CertificateValidatorType");
            }
            if (validationParameters1.ClockSkew != validationParameters2.ClockSkew)
                matchingFailures.Add("ClockSkew");

            if (validationParameters1.ClockSkew != validationParameters2.ClockSkew)
                matchingFailures.Add("ClockSkew");

            if (!AreEqual<SecurityKey>(validationParameters1.IssuerSigningKey, validationParameters2.IssuerSigningKey, context, AreSecurityKeysEqual))
                matchingFailures.Add("IssuerSigningKey");

            //if (!AreEqual<SecurityKey>(validationParameters1.IssuerSigningKeyResolver, validationParameters2.IssuerSigningKeyResolver, context, AreSecurityKeysEqual))
            //    matchingFailures.Add("IssuerSigningKeyRetriever");

            if (!AreEnumsEqual<SecurityKey>(validationParameters1.IssuerSigningKeys, validationParameters2.IssuerSigningKeys, context, AreSecurityKeysEqual))
                matchingFailures.Add("IssuerSigningKeys");

            if ((validationParameters1.IssuerSigningKeyValidator == null && validationParameters2.IssuerSigningKeyValidator != null) || (validationParameters1.IssuerSigningKeyValidator != null && validationParameters2.IssuerSigningKeyValidator == null))
                matchingFailures.Add("IssuerSigningKeyValidator");

            if (!AreEqual<SecurityToken>(validationParameters1.IssuerSigningToken, validationParameters2.IssuerSigningToken, context, AreSecurityTokensEqual))
                matchingFailures.Add("IssuerSigningKey");

            if (!AreEnumsEqual<SecurityToken>(validationParameters1.IssuerSigningTokens, validationParameters2.IssuerSigningTokens, context, AreSecurityTokensEqual))
                matchingFailures.Add("IssuerSigningTokens");

            if ((validationParameters1.IssuerValidator == null && validationParameters2.IssuerValidator != null) || (validationParameters1.IssuerValidator != null && validationParameters2.IssuerValidator == null))
                matchingFailures.Add("IssuerValidator");

            if ((validationParameters1.LifetimeValidator == null && validationParameters2.LifetimeValidator != null) || (validationParameters1.LifetimeValidator != null && validationParameters2.LifetimeValidator == null))
                matchingFailures.Add("LifetimeValidator");

            if (validationParameters1.NameClaimType != validationParameters2.NameClaimType)
                matchingFailures.Add("NameClaimType");

            if ((validationParameters1.NameClaimTypeRetriever == null && validationParameters2.NameClaimTypeRetriever != null) || (validationParameters1.NameClaimTypeRetriever != null && validationParameters2.NameClaimTypeRetriever == null))
                matchingFailures.Add("NameClaimTypeRetriever");

            if (validationParameters1.RequireSignedTokens != validationParameters2.RequireSignedTokens)
                matchingFailures.Add("RequireSignedTokens");

            if (validationParameters1.RequireExpirationTime != validationParameters2.RequireExpirationTime)
                matchingFailures.Add("RequireExpirationTime");

            if (validationParameters1.RoleClaimType != validationParameters2.RoleClaimType)
                matchingFailures.Add("RoleClaimType");

            if ((validationParameters1.RoleClaimTypeRetriever == null && validationParameters2.RoleClaimTypeRetriever != null) || (validationParameters1.RoleClaimTypeRetriever != null && validationParameters2.RoleClaimTypeRetriever == null))
                matchingFailures.Add("RoleClaimTypeRetriever");

            if (validationParameters1.SaveSigninToken != validationParameters2.SaveSigninToken)
                matchingFailures.Add("SaveSigninToken");

            if ((validationParameters1.TokenReplayCache == null && validationParameters2.TokenReplayCache != null) || (validationParameters1.TokenReplayCache != null && validationParameters2.TokenReplayCache == null))
                matchingFailures.Add("TokenReplayCache");

            if (validationParameters1.ValidateActor != validationParameters2.ValidateActor)
                matchingFailures.Add("ValidateActor");

            if (validationParameters1.ValidateAudience != validationParameters2.ValidateAudience)
                matchingFailures.Add("ValidateAudience");

            if (validationParameters1.ValidateIssuer != validationParameters2.ValidateIssuer)
                matchingFailures.Add("ValidateIssuer");

            if (validationParameters1.ValidateIssuerSigningKey != validationParameters2.ValidateIssuerSigningKey)
                matchingFailures.Add("ValidateIssuerSigningKey");

            if (validationParameters1.ValidateLifetime != validationParameters2.ValidateLifetime)
                matchingFailures.Add("ValidateLifetime");

            if (!String.Equals(validationParameters1.ValidAudience, validationParameters2.ValidAudience, StringComparison.Ordinal))
                matchingFailures.Add("ValidAudience");

            if (!AreEnumsEqual<string>(validationParameters1.ValidAudiences, validationParameters2.ValidAudiences, new CompareContext { StringComparison = System.StringComparison.Ordinal }, AreStringsEqual))
                matchingFailures.Add("ValidAudiences");

            if (!String.Equals(validationParameters1.ValidIssuer, validationParameters2.ValidIssuer, StringComparison.Ordinal))
                matchingFailures.Add("ValidIssuer");

            if (!AreEnumsEqual<string>(validationParameters1.ValidIssuers, validationParameters2.ValidIssuers, CompareContext.Default, AreStringsEqual))
                matchingFailures.Add("ValidIssuers");

            return matchingFailures.Count == 0;
        }


        // Not currently used.

        private static bool AreKeyRetrieversEqual(Func<string, IEnumerable<SecurityKey>> keyRetrevier1, Func<string, IEnumerable<SecurityKey>> keyRetrevier2, CompareContext context)
        {
            if (!AreEnumsEqual<SecurityKey>(keyRetrevier1("keys"), keyRetrevier2("keys"), context, AreSecurityKeysEqual))
                return false;

            return true;
        }

        private static bool AreAudValidatorsEqual(Action<IEnumerable<string>, SecurityToken, TokenValidationParameters> validator1, Action<IEnumerable<string>, SecurityToken, bool> validator2, CompareContext context)
        {
            //validator1(new string[]{"str"}, null, IdentityUtilities.DefaultTokenValidationParameters);
            //validator2(new string[]{"str"}, null, IdentityUtilities.DefaultTokenValidationParameters);

            return true;
        }

        private static bool AreLifetimeValidatorsEqual(Func<SecurityToken, bool> validator1, Func<SecurityToken, bool> validator2, CompareContext context)
        {
            if (validator1(null) != validator2(null))
                return false;

            return true;
        }

        private static bool AreIssValidatorsEqual(Func<string, SecurityToken, bool> validator1, Func<string, SecurityToken, bool> validator2, CompareContext context)
        {
            if (validator1("bob", null) != validator2("bob", null))
                return false;

            return true;
        }

    }
}
