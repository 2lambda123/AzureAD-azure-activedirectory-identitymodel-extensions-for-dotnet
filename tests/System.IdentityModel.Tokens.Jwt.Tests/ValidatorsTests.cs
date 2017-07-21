//-----------------------------------------------------------------------
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

using Microsoft.IdentityModel.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ValidatorsTests
    {
        internal class MyX509CertificateValidator : X509CertificateValidator
        {
            public override void Validate(X509Certificate2 certificate)
            {
                if (!certificate.Equals(KeyingMaterial.DefaultCert_2048))
                {
                    throw new ArgumentException();
                }
            }
        }

        public TestContext TestContext { get; set; }
        private static bool issuerSigningKeyValidatorCalled = false;

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestProperty("TestCaseID", "59fa90a3-caa1-46f0-a983-3686fbd1e876")]
        [Description("Tests: AudienceValidator")]
        public void Validators_Audience()
        {
            List<string> audiences = new List<string> { "", IdentityUtilities.DefaultAudience };
            List<string> invalidAudiences = new List<string> { "", IdentityUtilities.NotDefaultAudience };

            RunAudienceTest(audiences: null, securityToken: null, validationParameters: null, ee: ExpectedException.ArgumentNullException());
            RunAudienceTest(audiences: null, securityToken: null, validationParameters: new TokenValidationParameters { ValidateAudience = false }, ee: ExpectedException.NoExceptionExpected);
            RunAudienceTest(audiences: null, securityToken: null, validationParameters: new TokenValidationParameters(), ee: ExpectedException.SecurityTokenInvalidAudienceException(substringExpected: "IDX10214:"));
            RunAudienceTest(audiences: audiences, securityToken: null, validationParameters: new TokenValidationParameters(), ee: ExpectedException.SecurityTokenInvalidAudienceException(substringExpected: "IDX10208:"));
            RunAudienceTest(audiences: audiences, securityToken: null, validationParameters: new TokenValidationParameters{ValidAudience = IdentityUtilities.NotDefaultAudience}, ee: ExpectedException.SecurityTokenInvalidAudienceException(substringExpected: "IDX10214:"));
            RunAudienceTest(audiences: audiences, securityToken: null, validationParameters: new TokenValidationParameters { ValidAudiences = invalidAudiences }, ee: ExpectedException.SecurityTokenInvalidAudienceException(substringExpected: "IDX10214:"));
            RunAudienceTest(audiences: audiences, securityToken: null, validationParameters: new TokenValidationParameters { ValidAudience = IdentityUtilities.DefaultAudience }, ee: ExpectedException.NoExceptionExpected);
            RunAudienceTest(audiences: audiences, securityToken: null, validationParameters: new TokenValidationParameters { ValidAudiences = audiences }, ee: ExpectedException.NoExceptionExpected);
        }

        private void RunAudienceTest(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters, ExpectedException ee)
        {
            try
            {
                Validators.ValidateAudience(audiences, securityToken, validationParameters);
                ee.ProcessNoException();
            }
            catch(Exception ex)
            {
                ee.ProcessException(ex);
            }
        }

        [TestMethod]
        [TestProperty("TestCaseID", "ddecba25-1990-4e64-9997-a6835acab6c8")]
        [Description("Tests: IssuerValidator")]
        public void Validators_Issuer()
        {
            List<string> issuers = new List<string> { "", IdentityUtilities.DefaultIssuer };
            List<string> invalidIssuers = new List<string> { "", IdentityUtilities.NotDefaultIssuer };

            RunIssuerTest(issuer: null, securityToken: null, validationParameters: null, ee: ExpectedException.ArgumentNullException());
            RunIssuerTest(issuer: null, securityToken: null, validationParameters: new TokenValidationParameters { ValidateIssuer = false }, ee: ExpectedException.NoExceptionExpected);
            RunIssuerTest(issuer: null, securityToken: null, validationParameters: new TokenValidationParameters(), ee: ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10211:"));
            RunIssuerTest(issuer: IdentityUtilities.DefaultIssuer, securityToken: null, validationParameters: new TokenValidationParameters(), ee: ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10204:"));
            RunIssuerTest(issuer: IdentityUtilities.DefaultIssuer, securityToken: null, validationParameters: new TokenValidationParameters { ValidIssuer = IdentityUtilities.NotDefaultIssuer }, ee: ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10205:"));
            RunIssuerTest(issuer: IdentityUtilities.DefaultIssuer, securityToken: null, validationParameters: new TokenValidationParameters { ValidIssuers = invalidIssuers }, ee: ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10205:"));
            RunIssuerTest(issuer: IdentityUtilities.DefaultIssuer, securityToken: null, validationParameters: new TokenValidationParameters { ValidIssuer = IdentityUtilities.DefaultIssuer }, ee: ExpectedException.NoExceptionExpected);
            RunIssuerTest(issuer: IdentityUtilities.DefaultIssuer, securityToken: null, validationParameters: new TokenValidationParameters { ValidIssuers = issuers }, ee: ExpectedException.NoExceptionExpected);
        }

        private void RunIssuerTest(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters, ExpectedException ee)
        {
            try
            {
                Validators.ValidateIssuer(issuer, securityToken, validationParameters);
                ee.ProcessNoException();
            }
            catch (Exception ex)
            {
                ee.ProcessException(ex);
            }
        }

        [TestMethod]
        [TestProperty("TestCaseID", "f1718a32-19ce-420c-9efb-900dd1b85ca7")]
        [Description("Tests: LifetimeValidator")]
        public void Validators_Lifetime()
        {
            RunLifetimeTest(expires: null, notBefore: null, securityToken: null, validationParameters: null, ee: ExpectedException.ArgumentNullException());
            RunLifetimeTest(expires: null, notBefore: null, securityToken: null, validationParameters: new TokenValidationParameters { ValidateLifetime = false }, ee: ExpectedException.NoExceptionExpected);
            RunLifetimeTest(expires: null, notBefore: null, securityToken: null, validationParameters: new TokenValidationParameters { }, ee: ExpectedException.SecurityTokenNoExpirationException(substringExpected: "IDX10225:"));
            RunLifetimeTest(expires: DateTime.UtcNow, notBefore: DateTime.UtcNow + TimeSpan.FromHours(1), securityToken: null, validationParameters: new TokenValidationParameters { }, ee: ExpectedException.SecurityTokenInvalidLifetimeException(substringExpected: "IDX10224:"));
            RunLifetimeTest(expires: DateTime.UtcNow + TimeSpan.FromHours(2), notBefore: DateTime.UtcNow + TimeSpan.FromHours(1), securityToken: null, validationParameters: new TokenValidationParameters { }, ee: ExpectedException.SecurityTokenNotYetValidException(substringExpected: "IDX10222:"));
            RunLifetimeTest(expires: DateTime.UtcNow - TimeSpan.FromHours(1), notBefore: DateTime.UtcNow - TimeSpan.FromHours(2), securityToken: null, validationParameters: new TokenValidationParameters { }, ee: ExpectedException.SecurityTokenExpiredException(substringExpected: "IDX10223:"));
            RunLifetimeTest(expires: DateTime.UtcNow, notBefore: DateTime.UtcNow - TimeSpan.FromHours(2), securityToken: null, validationParameters: new TokenValidationParameters { }, ee: ExpectedException.NoExceptionExpected);
        }

        private void RunLifetimeTest(DateTime? expires, DateTime? notBefore, SecurityToken securityToken, TokenValidationParameters validationParameters, ExpectedException ee)
        {
            try
            {
                Validators.ValidateLifetime(notBefore: notBefore, expires: expires, securityToken: securityToken, validationParameters: validationParameters);
                ee.ProcessNoException();
            }
            catch (Exception ex)
            {
                ee.ProcessException(ex);
            }
        }

        [TestMethod]
        [TestProperty("TestCaseID", "955c110f-10b0-4db4-9983-567f2cc90631")]
        [Description("Tests: SecurityKeyValidator")]
        public void Validators_SecurityKey()
        {
            SecurityKey x509SecurityKey = new X509SecurityKey(KeyingMaterial.DefaultCert_2048);
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            // Test validationParameters == null
            RunSecurityKeyTest(x509SecurityKey, null, null, ExpectedException.ArgumentNullException());
            // Test validationParameters.ValidateIssuerSigningKey == false
            RunSecurityKeyTest(x509SecurityKey, null, validationParameters, ExpectedException.NoExceptionExpected);

            // Test ValidateIssuerSigningKey = true in following cases:
            validationParameters.ValidateIssuerSigningKey = true;

            // securityKey == x509SecurityKey, validationParameters.CertificateValidator == null, validationParameters.IssuerSigningKeyValidator == null
            validationParameters.CertificateValidator = null;
            validationParameters.IssuerSigningKeyValidator = null;
            RunSecurityKeyTest(x509SecurityKey, null, validationParameters, ExpectedException.SecurityTokenValidationException("IDX10232:"));

            // securityKey == x509SecurityKey with validate certificate, validationParameters.CertificateValidator != null, validationParameters.IssuerSigningKeyValidator == null
            validationParameters.CertificateValidator = new MyX509CertificateValidator();
            validationParameters.IssuerSigningKeyValidator = null;
            RunSecurityKeyTest(x509SecurityKey, null, validationParameters, ExpectedException.NoExceptionExpected);

            // securityKey == x509SecurityKey with invalidate certificate, validationParameters.CertificateValidator != null, validationParameters.IssuerSigningKeyValidator == null
            validationParameters.CertificateValidator = new MyX509CertificateValidator();
            validationParameters.IssuerSigningKeyValidator = null;
            X509SecurityKey x509NegCert = new X509SecurityKey(KeyingMaterial.Cert_1024);
            RunSecurityKeyTest(x509NegCert, null, validationParameters, ExpectedException.ArgumentException());

            // securityKey == x509SecurityKey, validationParameters.CertificateValidator == null, validationParameters.IssuerSigningKeyValidator != null
            validationParameters.CertificateValidator = null;
            validationParameters.IssuerSigningKeyValidator = IssuerSigningKeyValidator;
            RunSecurityKeyTest(x509SecurityKey, null, validationParameters, ExpectedException.NoExceptionExpected);
            Assert.AreEqual(issuerSigningKeyValidatorCalled, true);
            issuerSigningKeyValidatorCalled = false;

            // securityKey != x509SecurityKey, validationParameters.IssuerSigningKeyValidator == null
            validationParameters.IssuerSigningKeyValidator = null;
            RunSecurityKeyTest(KeyingMaterial.DefaultAsymmetricSigningCreds_2048_RsaSha2_Sha2.SigningKey, null, validationParameters, ExpectedException.SecurityTokenValidationException("IDX10233:"));

            // securityKey != x509SecurityKey, validationParameters.IssuerSigningKeyValidator != null
            validationParameters.IssuerSigningKeyValidator = IssuerSigningKeyValidator;
            RunSecurityKeyTest(KeyingMaterial.DefaultAsymmetricSigningCreds_2048_RsaSha2_Sha2.SigningKey, null, validationParameters, ExpectedException.NoExceptionExpected);
            Assert.AreEqual(issuerSigningKeyValidatorCalled, true);
            issuerSigningKeyValidatorCalled = false;
        }

        private void RunSecurityKeyTest(SecurityKey securityKey, SecurityToken securityToken, TokenValidationParameters validationParameters, ExpectedException ee)
        {
            try
            {
                Validators.ValidateIssuerSecurityKey(securityKey, securityToken, validationParameters);
                ee.ProcessNoException();
            }
            catch (Exception ex)
            {
                ee.ProcessException(ex);
            }
        }

        private static void IssuerSigningKeyValidator(SecurityKey key)
        {
            issuerSigningKeyValidatorCalled = true;
        }
    }
}