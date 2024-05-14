﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using BenchmarkDotNet.Attributes;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Benchmarks
{
    // dotnet run -c release -f net8.0 --filter Microsoft.IdentityModel.Benchmarks.CreateTokenTests*

    public class CreateTokenTests
    {
        private JsonWebTokenHandler _jsonWebTokenHandler;
        private SecurityTokenDescriptor _tokenDescriptor;
        private SecurityTokenDescriptor _tokenDescriptorMultipleAudiencesMemberAndClaims;
        private SecurityTokenDescriptor _tokenDescriptorMultipleAudiencesMemberOnly;
        private SecurityTokenDescriptor _tokenDescriptorSingleAudienceUsingAudiencesMember;

        [GlobalSetup]
        public void Setup()
        {
            DateTime now = DateTime.UtcNow;
            _jsonWebTokenHandler = new JsonWebTokenHandler();
            _tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = BenchmarkUtils.Claims,
                SigningCredentials = BenchmarkUtils.SigningCredentialsRsaSha256,
            };

            _tokenDescriptorMultipleAudiencesMemberOnly = new SecurityTokenDescriptor
            {
                Claims = BenchmarkUtils.ClaimsNoAudience,
                SigningCredentials = BenchmarkUtils.SigningCredentialsRsaSha256,
                Audiences = BenchmarkUtils.Audiences,
            };

            _tokenDescriptorMultipleAudiencesMemberAndClaims = new SecurityTokenDescriptor
            {
                Claims = BenchmarkUtils.ClaimsMultipleAudiences,
                SigningCredentials = BenchmarkUtils.SigningCredentialsRsaSha256,
                Audiences = BenchmarkUtils.Audiences,
            };

            _tokenDescriptorSingleAudienceUsingAudiencesMember = new SecurityTokenDescriptor
            {
                Claims = BenchmarkUtils.ClaimsNoAudience,
                SigningCredentials = BenchmarkUtils.SigningCredentialsRsaSha256,
                Audiences = new string[] { BenchmarkUtils.Audience }
            };
        }

        [Benchmark]
        public string JsonWebTokenHandler_CreateToken() => _jsonWebTokenHandler.CreateToken(_tokenDescriptor);

        [Benchmark]
        public string JsonWebTokenHandler_CreateToken_SingleAudienceUsingAudiencesMemberOnly() =>
            _jsonWebTokenHandler.CreateToken(_tokenDescriptorSingleAudienceUsingAudiencesMember);

        [Benchmark]
        public string JsonWebTokenHandler_CreateToken_MultipleAudiencesMemberOnly() =>
            _jsonWebTokenHandler.CreateToken(_tokenDescriptorMultipleAudiencesMemberOnly);

        [Benchmark]
        public string JsonWebTokenHandler_CreateToken_MultipleAudiencesMemberAndClaims() =>
            _jsonWebTokenHandler.CreateToken(_tokenDescriptorMultipleAudiencesMemberAndClaims);


    }
}
