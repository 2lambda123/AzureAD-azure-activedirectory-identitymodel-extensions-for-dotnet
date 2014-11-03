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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.IdentityModel.Test
{
    public class OpenIdConfigData
    {
        private static string rsaImportTemplate = @"<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>";

        static OpenIdConfigData()
        {
            JsonWebKeyFromPingExpected1 =
                new JsonWebKey
                {
                    E = "AQAB",
                    Kid = "20am7",
                    Kty = "RSA",
                    N = "mhupHfUtg_gHIqwu2wm8CprXY-gKqbPMV6tEYVqkyYrHugzQ_YDYAHr7vWo5Pe_3gIujSFwpqIfXaP8-Fl3O5fQhMo1lMv4DdRabyDLEpv7YO9qoVKTmDOZqYZx-AYBr5x1Zh2xWByI6_0dsPtCjD1pFZfg_SxNEcLPyH1aY6dT8CWYu32qG4O0WF4EihZzMkzSn8fyh8RXbMf5U9Wm2kgb0g8jK62S7MoF4IlhFaJreq898wgUohhPwR8P3X-gk0XQJAFcogEf04Fw4UmKo3z1B6mcNbPRfImhWw4wtLkhp_KIqKNOkMsSpYGSLrCvqQpgK56EJZExrmb7WozjwHw",
                    Use = "sig"
                };

           JsonWebKeyFromPingExpected2 =
                new JsonWebKey
                {
                    E = "AQAB",
                    Kid = "20am3",
                    Kty = "RSA",
                    N = "wY2KNRyiEvyBFkr1IC_1UGWMPInkzVYpoap_-Zw5fYAXLVxKMSPdZVVLt9AVhuNtagOOQqlZ_Y32e4l19REHym6RGV9Sm1noKRxDUjkz7U8OVeUew7D7h4Dk6E2rrlIYpy9OmhhzWSS68pBTf0_ESdekKv3OQbEs99avEXOPK5uH3V-NHsy1YP3DAvl7HJaV6fn-1Nch1quLrg1G7ohBuTb4Zr-499TJ6bkfabaACz8bf-RHuPezFBjoY0LHNNu6-KQ-qqHVkoki_1OQwj2s_Lui3qYWOmLoaVN9ZzO90rBdhhg8t0JZv6pSlc7o0XT4fie5RRjiqCuOpuGQvNYKpQ",
                    Use = "sig"
                };

           JsonWebKeyFromPingExpected3 =
                new JsonWebKey
                {
                    E = "AQAB",
                    Kid = "20alz",
                    Kty = "RSA",
                    N = "tgLZUXY8mo2Y1TaXHjOYrFGs23jZxgpzEKfBz004AEeOMHFbEP1h1Lrqf2B7f49mOpXRkBgEm4tnSYzX7pDWrMvNeRVkTFXSXwHYvda1R1kmwiTxnrC9IWjvizrr22DtzHhSSpL_7xuXtmaid2orOF8mUoXnKesPQVfq33pCKm1QUV6oFNSVxAiOKJkzFmxjYvcqzryjYi10glxPSx3cmSI8RGqlxolJr0negfLmI9bNxuAvStf_L6zXB5NFqccmkCQXn_QC3P1N3j-HgwwHTVFxkrS8kZQOMTw3TMXbtTFNrVAx1QC_3M0ze4cVncr2zTSECS_2qXM5RS7xBTEDvQ",
                    Use = "sig"
                };

            JsonWebKeyExpected1 =
                new JsonWebKey
                {
                    Alg = "SHA256",
                    E = "AQAB",
                    KeyOps = "signing",
                    Kid = "NGTFvdK-fythEuLwjpwAJOM9n-A",
                    Kty = "RSA",
                    N = "rCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVw==",
                    X5t = "NGTFvdK-fythEuLwjpwAJOM9n-A",
                    X5u = "https://jsonkeyurl",
                    Use = "sig",
                };
            
            JsonWebKeyExpected1.X5c.Add(JsonWebKey_X5c_1);

            JsonWebKeyDictionary1 =
                new Dictionary<string, object>
                {
                    {"alg", "SHA256"},
                    {"e", "AQAB"},
                    {"key_ops", "signing"},
                    {"kid", "NGTFvdK-fythEuLwjpwAJOM9n-A"},
                    {"kty", "RSA"},
                    {"n", "kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="},                    
                    {"x5c", new ArrayList(new List<string> { "MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng"})},
                    {"x5t", "NGTFvdK-fythEuLwjpwAJOM9n-A"},
                    {"x5u", "https://jsonkeyurl"},
                    {"use", "sig"},
                };

            JsonWebKeyExpected2 = 
                new JsonWebKey
                {
                    Alg = "SHA256",
                    E = "AQAB",
                    Kid = "kriMPdmBvx68skT8-mPAB3BseeA",
                    Kty = "RSA",
                    N = "kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw==",
                    X5t = "kriMPdmBvx68skT8-mPAB3BseeA",
                    Use = "sig",
                };

            JsonWebKeyExpected2.X5c.Add(JsonWebKey_X5c_2);

            JsonWebKeySetExpected1 = new JsonWebKeySet();
            JsonWebKeySetExpected1.Keys.Add(JsonWebKeyExpected1);
            JsonWebKeySetExpected1.Keys.Add(JsonWebKeyExpected2);

            JsonWebKeyExpectedBadX509Data =
                new JsonWebKey
                {
                    Alg = null,
                    KeyOps = null,
                    Kid = "kriMPdmBvx68skT8-mPAB3BseeA",
                    Kty = "RSA",
                    X5t = "kriMPdmBvx68skT8-mPAB3BseeA",
                    X5u = null,
                    Use = "sig"
                };

            JsonWebKeyExpectedBadX509Data.X5c.Add("==MIIDPjCCAiqgAwIBAgIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTQwMTAxMDcwMDAwWhcNMTYwMTAxMDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuwIDAQABo2IwYDBeBgNVHQEEVzBVgBDLebM6bK3BjWGqIBrBNFeNoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAA4IBAQCJ4JApryF77EKC4zF5bUaBLQHQ1PNtA1uMDbdNVGKCmSf8M65b8h0NwlIjGGGy/unK8P6jWFdm5IlZ0YPTOgzcRZguXDPj7ajyvlVEQ2K2ICvTYiRQqrOhEhZMSSZsTKXFVwNfW6ADDkN3bvVOVbtpty+nBY5UqnI7xbcoHLZ4wYD251uj5+lo13YLnsVrmQ16NCBYq2nQFNPuNJw6t3XUbwBHXpF46aLT1/eGf/7Xx6iy8yPJX4DyrpFTutDz882RWofGEO5t4Cw+zZg70dJ/hH/ODYRMorfXEW+8uKmXMKmX2wyxMKvfiPbTy5LmAU8Jvjs2tLg4rOBcXWLAIarZ");

            OpenIdConnectConfiguration1 = 
                new OpenIdConnectConfiguration()
                {
                    AuthorizationEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/authorize",
                    CheckSessionIframe = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/checksession",
                    EndSessionEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/logout",
                    Issuer = "https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/",
                    JwksUri = "JsonWebKeySet.json",
                    TokenEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/token",
                };

            X509CertificateJsonWebKey1 = new X509Certificate2(Convert.FromBase64String("MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng"));
            X509CertificateJsonWebKey2 = new X509Certificate2(Convert.FromBase64String("MIIDPjCCAiqgAwIBAgIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTQwMTAxMDcwMDAwWhcNMTYwMTAxMDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuwIDAQABo2IwYDBeBgNVHQEEVzBVgBDLebM6bK3BjWGqIBrBNFeNoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAA4IBAQCJ4JApryF77EKC4zF5bUaBLQHQ1PNtA1uMDbdNVGKCmSf8M65b8h0NwlIjGGGy/unK8P6jWFdm5IlZ0YPTOgzcRZguXDPj7ajyvlVEQ2K2ICvTYiRQqrOhEhZMSSZsTKXFVwNfW6ADDkN3bvVOVbtpty+nBY5UqnI7xbcoHLZ4wYD251uj5+lo13YLnsVrmQ16NCBYq2nQFNPuNJw6t3XUbwBHXpF46aLT1/eGf/7Xx6iy8yPJX4DyrpFTutDz882RWofGEO5t4Cw+zZg70dJ/hH/ODYRMorfXEW+8uKmXMKmX2wyxMKvfiPbTy5LmAU8Jvjs2tLg4rOBcXWLAIarZ"));

            OpenIdConnectConfigurationWithKeys1 =
                new OpenIdConnectConfiguration()
                {
                    AuthorizationEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/authorize",
                    CheckSessionIframe = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/checksession",
                    EndSessionEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/logout",
                    Issuer = "https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/",
                    JwksUri = "JsonWebKeySet.json",
                    TokenEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/token",
                };

            RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider();
            RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider();
            RSACryptoServiceProvider rsa3 = new RSACryptoServiceProvider();
            string base64String1 = Convert.ToBase64String(Base64UrlEncoder.DecodeBytes(JsonWebKeyFromPingExpected1.N));
            rsa1.FromXmlString(string.Format(CultureInfo.InvariantCulture, rsaImportTemplate, base64String1, JsonWebKeyFromPingExpected1.E));
            string base64String2 = Convert.ToBase64String(Base64UrlEncoder.DecodeBytes(JsonWebKeyFromPingExpected2.N));
            rsa2.FromXmlString(string.Format(CultureInfo.InvariantCulture, rsaImportTemplate, base64String2, JsonWebKeyFromPingExpected2.E));
            string base64String3 = Convert.ToBase64String(Base64UrlEncoder.DecodeBytes(JsonWebKeyFromPingExpected3.N));
            rsa3.FromXmlString(string.Format(CultureInfo.InvariantCulture, rsaImportTemplate, base64String3, JsonWebKeyFromPingExpected3.E));

            OpenIdConnectConfigurationPing =
                new OpenIdConnectConfiguration()
                {
                    AuthorizationEndpoint = "https://connect-interop.pinglabs.org:9031/as/authorization.oauth2",
                    Issuer = "https://connect-interop.pinglabs.org:9031",
                    TokenEndpoint = "https://connect-interop.pinglabs.org:9031/as/token.oauth2",
                    UserInfoEndpoint = "https://connect-interop.pinglabs.org:9031/idp/userinfo.openid"
                };

            OpenIdConnectConfigurationPingLabsJWKS =
                new OpenIdConnectConfiguration()
                {
                    JwksUri = "PingLabsJWKS.json",
                };

            OpenIdConnectConfigurationPingLabsJWKS.SigningTokens.Add(new NamedKeySecurityToken(JsonWebKeyParameterNames.Kid, JsonWebKeyFromPingExpected1.Kid, new RsaSecurityKey(rsa1)));
            OpenIdConnectConfigurationPingLabsJWKS.SigningTokens.Add(new NamedKeySecurityToken(JsonWebKeyParameterNames.Kid, JsonWebKeyFromPingExpected2.Kid, new RsaSecurityKey(rsa2)));
            OpenIdConnectConfigurationPingLabsJWKS.SigningTokens.Add(new NamedKeySecurityToken(JsonWebKeyParameterNames.Kid, JsonWebKeyFromPingExpected3.Kid, new RsaSecurityKey(rsa3)));


            string n = "ns1cm8RU1hKZILPI6pB5Zoxn9mW2tSS0atV+o9FCn9NyeOktEOj1kEXOeIz0KfnqxgPMF1GpshuZBAhgjkyy2kNGE6Zx50CCJgq6XUatvVVJpMp8/FV18ynPf+/TRlF8V2HO3IVJ0XqRJ9fGA2f5xpOweWsdLYitdHbaDCl6IBNSXo52iNuqWAcB1k7jBlsnlXpuvslhLIzj60dnghAVA4ltS3NlFyw1Tz3pGlZQDt7x83IBHe7DA9bV3aJs1trkm1NzI1HoRS4vOqU3n4fn+DlfAE2vYKNkSi/PjuAX+1YQCq6e5uN/hOeSEqji8SsWC2nk/bMTKPwD67rn3jNC9w==";
            string e = "AQAB";
            string n2 = "kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw==";
            string e2 = "AQAB";

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string xml = string.Format(CultureInfo.InvariantCulture, rsaImportTemplate, n, e);
            rsa.FromXmlString(xml);
            OpenIdConnectConfigurationWithKeys1.SigningTokens.Add(new NamedKeySecurityToken("kid", "NGTFvdK-fythEuLwjpwAJOM9n-A", new RsaSecurityKey(rsa)));

            rsa = new RSACryptoServiceProvider();
            xml = string.Format(CultureInfo.InvariantCulture, rsaImportTemplate, n2, e2);
            rsa.FromXmlString(xml);
            OpenIdConnectConfigurationWithKeys1.SigningTokens.Add(new NamedKeySecurityToken("kid", "kriMPdmBvx68skT8-mPAB3BseeA", new RsaSecurityKey(rsa)));
            OpenIdConnectConfigurationWithKeys1.SigningTokens.Add(new X509SecurityToken(X509CertificateJsonWebKey1));
            OpenIdConnectConfigurationWithKeys1.SigningTokens.Add(new X509SecurityToken(X509CertificateJsonWebKey2));

            OpenIdConnectConfigurationSingleX509Data1 = 
                new OpenIdConnectConfiguration()
                {
                    AuthorizationEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/authorize",
                    CheckSessionIframe = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/checksession",
                    EndSessionEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/logout",
                    Issuer = "https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/",
                    JwksUri = "JsonWebKeySetSingleX509Data.json",
                    TokenEndpoint = "https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/token",
            };

            OpenIdConnectConfigurationSingleX509Data1.SigningTokens.Add(new X509SecurityToken(X509CertificateJsonWebKey1));

            // interrop
            GoogleCertsExpected = new JsonWebKeySet();
            GoogleCertsExpected.Keys.Add(
                new JsonWebKey
                {
                    Alg = "RS256",
                    E = "AQAB",
                    Kty = "RSA",
                    Kid = "ab844f3d4c69feee0de2501b04e1a4c8d78eead1",
                    N = "AKrMiv5vhYehVKXnSpZZN6lYymUIi+NS97ceYKYClMlNyj2Ln4ErWiOwjwdivG2kZnN0kKCC/XL9E+uEgsZO3ECvvDtgtFhPOR0MiqL7pp/K7d58dbKUWX/cWy8E4bm/Zmwa/g0HDcW6o19+Q85IPYXbY/6Z2oOgA9qDAoGHkjIv",
                    Use = "sig",
                });

           GoogleCertsExpected.Keys.Add(
                new JsonWebKey
                {
                    Alg = "RS256",
                    E = "AQAB",
                    Kty = "RSA",
                    Kid = "550326e0aacb4674d22905a1a51a808cfa7463b0",
                    N = "ANLFuJO6EoKczde+YP3b1yuz2b46D7Rd7CjrbvKrzbjkH29iRFLBagT7nojwdMOPrsV+WLp/C8lfkRT7UJ38lnQh3m4oEy98HdRRMZh5Vtpbotgt4S/ugh5ansJdHSXSBTxk+X1ZnTzMOUH7ZROpxw3NcX/IFl0sshFlTbebPrDj",
                    Use = "sig",
                });

        }

        public static string AADCommonUrl = "https://login.windows.net/common/.well-known/openid-configuration";
        public static string BadUri = "_____NoSuchfile____";     
   
        // Keys

        public static string JsonWebKeyFromPing =
            @"{ ""kty"":""RSA"",
                ""kid"":""20am7"",
                ""use"":""sig"",
                ""n"":""mhupHfUtg_gHIqwu2wm8CprXY-gKqbPMV6tEYVqkyYrHugzQ_YDYAHr7vWo5Pe_3gIujSFwpqIfXaP8-Fl3O5fQhMo1lMv4DdRabyDLEpv7YO9qoVKTmDOZqYZx-AYBr5x1Zh2xWByI6_0dsPtCjD1pFZfg_SxNEcLPyH1aY6dT8CWYu32qG4O0WF4EihZzMkzSn8fyh8RXbMf5U9Wm2kgb0g8jK62S7MoF4IlhFaJreq898wgUohhPwR8P3X-gk0XQJAFcogEf04Fw4UmKo3z1B6mcNbPRfImhWw4wtLkhp_KIqKNOkMsSpYGSLrCvqQpgK56EJZExrmb7WozjwHw"",
                ""e"":""AQAB""}";

        public static string JsonWebKey_X5c_1 = "MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng";
        public static string JsonWebKey_X5c_2 = "MIIDPjCCAiqgAwIBAgIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTQwMTAxMDcwMDAwWhcNMTYwMTAxMDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuwIDAQABo2IwYDBeBgNVHQEEVzBVgBDLebM6bK3BjWGqIBrBNFeNoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAA4IBAQCJ4JApryF77EKC4zF5bUaBLQHQ1PNtA1uMDbdNVGKCmSf8M65b8h0NwlIjGGGy/unK8P6jWFdm5IlZ0YPTOgzcRZguXDPj7ajyvlVEQ2K2ICvTYiRQqrOhEhZMSSZsTKXFVwNfW6ADDkN3bvVOVbtpty+nBY5UqnI7xbcoHLZ4wYD251uj5+lo13YLnsVrmQ16NCBYq2nQFNPuNJw6t3XUbwBHXpF46aLT1/eGf/7Xx6iy8yPJX4DyrpFTutDz882RWofGEO5t4Cw+zZg70dJ/hH/ODYRMorfXEW+8uKmXMKmX2wyxMKvfiPbTy5LmAU8Jvjs2tLg4rOBcXWLAIarZ";

        public static string JsonWebKeyString1 =
                                        @"{ ""alg"":""SHA256"",
                                            ""e"":""AQAB"",
                                            ""key_ops"":""signing"",
                                            ""kid"":""NGTFvdK-fythEuLwjpwAJOM9n-A"",
                                            ""kty"":""RSA"",                                            
                                            ""n"":""rCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVw=="",                                            
                                            ""x5c"":[""MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng""],
                                            ""x5t"":""NGTFvdK-fythEuLwjpwAJOM9n-A"",
                                            ""x5u"":""https://jsonkeyurl"",
                                            ""use"":""sig""
                                        }";

        public static string JsonWebKeyString2 =
                                            @"{ ""alg"":""SHA256"",
                                                ""e"":""AQAB"",                                              
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSA"",
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",
                                                ""x5t"":""kriMPdmBvx68skT8-mPAB3BseeA"",                                               
                                                ""x5c"":[""MIIDPjCCAiqgAwIBAgIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTQwMTAxMDcwMDAwWhcNMTYwMTAxMDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuwIDAQABo2IwYDBeBgNVHQEEVzBVgBDLebM6bK3BjWGqIBrBNFeNoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAA4IBAQCJ4JApryF77EKC4zF5bUaBLQHQ1PNtA1uMDbdNVGKCmSf8M65b8h0NwlIjGGGy/unK8P6jWFdm5IlZ0YPTOgzcRZguXDPj7ajyvlVEQ2K2ICvTYiRQqrOhEhZMSSZsTKXFVwNfW6ADDkN3bvVOVbtpty+nBY5UqnI7xbcoHLZ4wYD251uj5+lo13YLnsVrmQ16NCBYq2nQFNPuNJw6t3XUbwBHXpF46aLT1/eGf/7Xx6iy8yPJX4DyrpFTutDz882RWofGEO5t4Cw+zZg70dJ/hH/ODYRMorfXEW+8uKmXMKmX2wyxMKvfiPbTy5LmAU8Jvjs2tLg4rOBcXWLAIarZ""],
                                                ""use"":""sig""
                                        }";

        public static string JsonWebKeyBadFormatString1 =
                                            @"{ ""e"":""AQAB"",
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSA"",                                               
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                                                                                                                            
                                                ""x5c"":[""MIIDPjCCAiqgAwIBAgIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTQwMTAxMDcwMDAwWhcNMTYwMTAxMDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuwIDAQABo2IwYDBeBgNVHQEEVzBVgBDLebM6bK3BjWGqIBrBNFeNoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAA4IBAQCJ4JApryF77EKC4zF5bUaBLQHQ1PNtA1uMDbdNVGKCmSf8M65b8h0NwlIjGGGy/unK8P6jWFdm5IlZ0YPTOgzcRZguXDPj7ajyvlVEQ2K2ICvTYiRQqrOhEhZMSSZsTKXFVwNfW6ADDkN3bvVOVbtpty+nBY5UqnI7xbcoHLZ4wYD251uj5+lo13YLnsVrmQ16NCBYq2nQFNPuNJw6t3XUbwBHXpF46aLT1/eGf/7Xx6iy8yPJX4DyrpFTutDz882RWofGEO5t4Cw+zZg70dJ/hH/ODYRMorfXEW+8uKmXMKmX2wyxMKvfiPbTy5LmAU8Jvjs2tLg4rOBcXWLAIarZ""],
                                                ""x5t"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""use""::""sig""
                                               }";

        public static string JsonWebKeyBadFormatString2 =
                                            @"{ ""e"":""AQAB"",                                               
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSA"",
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                             
                                                ""x5c"":""M""IIDPjCCAiqgAwIBAgIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTQwMTAxMDcwMDAwWhcNMTYwMTAxMDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuwIDAQABo2IwYDBeBgNVHQEEVzBVgBDLebM6bK3BjWGqIBrBNFeNoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAA4IBAQCJ4JApryF77EKC4zF5bUaBLQHQ1PNtA1uMDbdNVGKCmSf8M65b8h0NwlIjGGGy/unK8P6jWFdm5IlZ0YPTOgzcRZguXDPj7ajyvlVEQ2K2ICvTYiRQqrOhEhZMSSZsTKXFVwNfW6ADDkN3bvVOVbtpty+nBY5UqnI7xbcoHLZ4wYD251uj5+lo13YLnsVrmQ16NCBYq2nQFNPuNJw6t3XUbwBHXpF46aLT1/eGf/7Xx6iy8yPJX4DyrpFTutDz882RWofGEO5t4Cw+zZg70dJ/hH/ODYRMorfXEW+8uKmXMKmX2wyxMKvfiPbTy5LmAU8Jvjs2tLg4rOBcXWLAIarZ""],
                                                ""x5t"":""kriMPdmBvx68skT8-mPAB3BseeA"",                                               
                                                ""use""::""sig""
                                               }";

        public static string JsonWebKeyBadRsaExponentString =
                                            @"{ ""e"":""AQABC"",
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSA"",                       
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                               
                                                ""use"":""sig""
                                             }";

        public static string JsonWebKeyBadRsaModulusString =
                                            @"{ ""e"":""AQAB"",
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSA"",
                                                ""n"":""kSSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                               
                                                ""use"":""sig""
                                             }";

        public static string JsonWebKeyRsaNoKidString =
                                            @"{ ""e"":""AQAB"",
                                                ""kty"":""RSA"",
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                               
                                                ""use"":""sig""
                                            }";

        public static string JsonWebKeyKtyNotRsaString =
                                            @"{ ""e"":""AQAB"",
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSAA"",
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                               
                                                ""use"":""sig""
                                             }";

        public static string JsonWebKeyUseNotSigString =
                                            @"{ ""e"":""AQAB"",
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSA"",
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                               
                                                ""use"":""sigg""
                                             }";

        public static string JsonWebKeyBadX509String =
                                            @"{ ""e"":""AQAB"",
                                                ""kid"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""kty"":""RSA"",
                                                ""n"":""kSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuw=="",                                               
                                                ""x5c"":[""==MIIDPjCCAiqgAwIBAgIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTQwMTAxMDcwMDAwWhcNMTYwMTAxMDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkSCWg6q9iYxvJE2NIhSyOiKvqoWCO2GFipgH0sTSAs5FalHQosk9ZNTztX0ywS/AHsBeQPqYygfYVJL6/EgzVuwRk5txr9e3n1uml94fLyq/AXbwo9yAduf4dCHTP8CWR1dnDR+Qnz/4PYlWVEuuHHONOw/blbfdMjhY+C/BYM2E3pRxbohBb3x//CfueV7ddz2LYiH3wjz0QS/7kjPiNCsXcNyKQEOTkbHFi3mu0u13SQwNddhcynd/GTgWN8A+6SN1r4hzpjFKFLbZnBt77ACSiYx+IHK4Mp+NaVEi5wQtSsjQtI++XsokxRDqYLwus1I1SihgbV/STTg5enufuwIDAQABo2IwYDBeBgNVHQEEVzBVgBDLebM6bK3BjWGqIBrBNFeNoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQsRiM0jheFZhKk49YD0SK1TAJBgUrDgMCHQUAA4IBAQCJ4JApryF77EKC4zF5bUaBLQHQ1PNtA1uMDbdNVGKCmSf8M65b8h0NwlIjGGGy/unK8P6jWFdm5IlZ0YPTOgzcRZguXDPj7ajyvlVEQ2K2ICvTYiRQqrOhEhZMSSZsTKXFVwNfW6ADDkN3bvVOVbtpty+nBY5UqnI7xbcoHLZ4wYD251uj5+lo13YLnsVrmQ16NCBYq2nQFNPuNJw6t3XUbwBHXpF46aLT1/eGf/7Xx6iy8yPJX4DyrpFTutDz882RWofGEO5t4Cw+zZg70dJ/hH/ODYRMorfXEW+8uKmXMKmX2wyxMKvfiPbTy5LmAU8Jvjs2tLg4rOBcXWLAIarZ""],
                                                ""x5t"":""kriMPdmBvx68skT8-mPAB3BseeA"",
                                                ""use"":""sig""
                                            }";


        public static string JsonWebKeySetBadRsaExponentString  = @"{ ""keys"":[" + JsonWebKeyBadRsaExponentString + "]}";
        public static string JsonWebKeySetBadRsaModulusString   = @"{ ""keys"":[" + JsonWebKeyBadRsaModulusString + "]}";
        public static string JsonWebKeySetUseNoKidString        = @"{ ""keys"":[" + JsonWebKeyRsaNoKidString + "]}";
        public static string JsonWebKeySetKtyNotRsaString       = @"{ ""keys"":[" + JsonWebKeyKtyNotRsaString + "]}";
        public static string JsonWebKeySetUseNotSigString       = @"{ ""keys"":[" + JsonWebKeyUseNotSigString + "]}";
        public static string JsonWebKeySetBadX509String         = @"{ ""keys"":[" + JsonWebKeyBadX509String + "]}";


        // Key Sets
        public static string JsonWebKeySet = "JsonWebKeySet.json";
        public static string JsonWebKeySetString1 = @"{ ""keys"":[" + JsonWebKeyString1 + "," + JsonWebKeyString2 + "]}";
        public static string JsonWebKeySetString2 = @"{ ""keys"":[" + JsonWebKeyString2 + "]}";
        public static string JsonWebKeySetBadFormatingString =
                                            @"{ ""keys"":[
                                                {   ""e"":""AQAB"",
                                                    ""kty"":""RSA"",
                                                    ""kid"":""NGTFvdK-fythEuLwjpwAJOM9n-A"",
                                                    ""n"":""rCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVw=="",
                                                    ""x5c"":[""MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng""
                                                    ""x5t"":""NGTFvdK-fythEuLwjpwAJOM9n-A"",
                                                    ""use"":""sig""
                                                }
                                            ]}";

        public static string JsonWebKeySetSingleX509DataString =
                                            @"{ ""keys"":[
                                                {   ""e"":""AQAB"",                                                                                                
                                                    ""kid"":""NGTFvdK-fythEuLwjpwAJOM9n-A"",                                               
                                                    ""kty"":""RSA"",
                                                    ""n"":""rCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVw=="",
                                                    ""x5c"":""MIIDPjCCAiqgAwIBAgIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAMC0xKzApBgNVBAMTImFjY291bnRzLmFjY2Vzc2NvbnRyb2wud2luZG93cy5uZXQwHhcNMTIwNjA3MDcwMDAwWhcNMTQwNjA3MDcwMDAwWjAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVwIDAQABo2IwYDBeBgNVHQEEVzBVgBCxDDsLd8xkfOLKm4Q/SzjtoS8wLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldIIQVWmXY/+9RqFA/OG9kFulHDAJBgUrDgMCHQUAA4IBAQAkJtxxm/ErgySlNk69+1odTMP8Oy6L0H17z7XGG3w4TqvTUSWaxD4hSFJ0e7mHLQLQD7oV/erACXwSZn2pMoZ89MBDjOMQA+e6QzGB7jmSzPTNmQgMLA8fWCfqPrz6zgH+1F1gNp8hJY57kfeVPBiyjuBmlTEBsBlzolY9dd/55qqfQk6cgSeCbHCy/RU/iep0+UsRMlSgPNNmqhj5gmN2AFVCN96zF694LwuPae5CeR2ZcVknexOWHYjFM0MgUSw0ubnGl0h9AJgGyhvNGcjQqu9vd1xkupFgaN+f7P3p3EVN5csBg5H94jEcQZT7EKeTiZ6bTrpDAnrr8tDCy8ng"",
                                                    ""x5t"":""NGTFvdK-fythEuLwjpwAJOM9n-A"",
                                                    ""use"":""sig""
                                               }
                                            ]}";


        // Metadata

        public static string OpenIdConnectMetadataPingString = @"{""version"":""3.0"",
                                                                  ""issuer"":""https:\/\/connect-interop.pinglabs.org:9031"",
                                                                  ""authorization_endpoint"":""https:\/\/connect-interop.pinglabs.org:9031\/as\/authorization.oauth2"",
                                                                  ""token_endpoint"":""https:\/\/connect-interop.pinglabs.org:9031\/as\/token.oauth2"",
                                                                  ""revocation_endpoint"":""https:\/\/connect-interop.pinglabs.org:9031\/as\/revoke_token.oauth2"",
                                                                  ""userinfo_endpoint"":""https:\/\/connect-interop.pinglabs.org:9031\/idp\/userinfo.openid"",
                                                                  ""ping_revoked_sris_endpoint"":""https:\/\/connect-interop.pinglabs.org:9031\/pf-ws\/rest\/sessionMgmt\/revokedSris"",
                                                                  ""ping_end_session_endpoint"":""https:\/\/connect-interop.pinglabs.org:9031\/idp\/startSLO.ping"",
                                                                  ""scopes_supported"":[""phone"",""address"",""email"",""openid"",""profile""],
                                                                  ""response_types_supported"":[""code"",""token"",""id_token"",""code token"",""code id_token"",""token id_token"",""code token id_token""],
                                                                  ""response_modes_supported"":[""fragment"",""query"",""form_post""],
                                                                  ""subject_types_supported"":[""public""],
                                                                  ""id_token_signing_alg_values_supported"":[""none"",""HS256"",""HS384"",""HS512"",""RS256"",""RS384"",""RS512"",""ES256"",""ES384"",""ES512""],
                                                                  ""token_endpoint_auth_methods_supported"":[""client_secret_basic"",""client_secret_post""],
                                                                  ""claim_types_supported"":[""normal""],
                                                                  ""claims_parameter_supported"":false,
                                                                  ""request_parameter_supported"":false,
                                                                  ""request_uri_parameter_supported"":false}";

        public static string OpenIdConnectMetadataFile = @"OpenIdConnectMetadata.json";
        public static string OpenIdConnectMetadataJsonWebKeySetBadUriFile = @"OpenIdConnectMetadataJsonWebKeySetBadUri.json";

        public static string OpenIdConnectMetadataString =
                                            @"{ ""authorization_endpoint"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/authorize"",
                                                ""check_session_iframe"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/checksession"",
                                                ""end_session_endpoint"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/logout"",
                                                ""id_token_signing_alg_values_supported"":[""RS256""],
                                                ""issuer"":""https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/"",
                                                ""jwks_uri"":""JsonWebKeySet.json"",
                                                ""microsoft_multi_refresh_token"":true,
                                                ""response_types_supported"":[""code"",""id_token"",""code id_token""],
                                                ""response_modes_supported"":[""query"",""fragment"",""form_post""],
                                                ""scopes_supported"":[""openid""],
                                                ""subject_types_supported"":[""pairwise""],
                                                ""token_endpoint"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/token"",
                                                ""token_endpoint_auth_methods_supported"":[""client_secret_post"",""private_key_jwt""]
                                            }";

        public static string OpenIdConnectMetadataSingleX509DataString =
                                            @"{ ""authorization_endpoint"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/authorize"",
                                                ""check_session_iframe"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/checksession"",
                                                ""end_session_endpoint"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/logout"",
                                                ""id_token_signing_alg_values_supported"":[""RS256""],
                                                ""issuer"":""https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/"",
                                                ""jwks_uri"":""JsonWebKeySetSingleX509Data.json"",
                                                ""microsoft_multi_refresh_token"":true,
                                                ""response_types_supported"":[""code"",""id_token"",""code id_token""],
                                                ""response_modes_supported"":[""query"",""fragment"",""form_post""],
                                                ""scopes_supported"":[""openid""],
                                                ""subject_types_supported"":[""pairwise""],
                                                ""token_endpoint"":""https://login.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/oauth2/token"",
                                                ""token_endpoint_auth_methods_supported"":[""client_secret_post"",""private_key_jwt""]
                                            }";

        public static string OpenIdConnectMetadataSingleItemString = @"{""{0}"":""{2}""}";
        public static string OpenIdConnectMetadataBadX509DataString = @"{""jwks_uri"":""JsonWebKeySetBadX509Data.json""}";
        public static string OpenIdConnectMetadataBadBase64DataString = @"{""jwks_uri"":""JsonWebKeySetBadBase64Data.json""}";
        public static string OpenIdConnectMetadataBadUriKeysString = @"{""jwks_uri"":""___NoSuchFile___""}";
        public static string OpenIdConnectMetadataBadFormatString = @"{""issuer""::""https://sts.windows.net/d062b2b0-9aca-4ff7-b32a-ba47231a4002/""}";
        public static string OpenIdConnectMetadataPingLabsJWKSString = @"{""jwks_uri"":""PingLabsJWKS.json""}";

        public static JsonWebKey JsonWebKeyFromPingExpected1;
        public static JsonWebKey JsonWebKeyFromPingExpected2;
        public static JsonWebKey JsonWebKeyFromPingExpected3;
        public static JsonWebKey JsonWebKeyExpected1;
        public static JsonWebKey JsonWebKeyExpected2;
        public static JsonWebKey JsonWebKeyExpectedBadX509Data;
        public static JsonWebKeySet JsonWebKeySetExpected1;
        public static JsonWebKeySet JsonWebKeySetExpected2;
        public static OpenIdConnectConfiguration OpenIdConnectConfiguration1;
        public static OpenIdConnectConfiguration OpenIdConnectConfigurationSingleX509Data1;
        public static OpenIdConnectConfiguration OpenIdConnectConfigurationWithKeys1;
        public static OpenIdConnectConfiguration OpenIdConnectConfigurationPingLabsJWKS;
        public static OpenIdConnectConfiguration OpenIdConnectConfigurationPing;
        public static X509Certificate2 X509CertificateJsonWebKey1;
        public static X509Certificate2 X509CertificateJsonWebKey2;
        public static IDictionary<string, object> JsonWebKeyDictionary1;

        // interop

        public static string GoogleCertsFile = "google-certs.json";
        public static JsonWebKeySet GoogleCertsExpected;
    }
}