// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Contains the result of validating the signature over a token.
    /// The <see cref="TokenValidationResult"/> contains a collection of <see cref="ValidationResult"/> for each step in the token validation.
    /// </summary>
    public class SignatureValidationResult : ValidationResult
    {
        private Exception _exception;

        /// <summary>
        /// Creates an instance of <see cref="IssuerValidationResult"/>
        /// </summary>
        public SignatureValidationResult()
        {
        }

        /// <summary>
        /// Gets the <see cref="Exception"/> that occurred during validation.
        /// </summary>
        public override Exception Exception
        {
            get
            {
                // TODO - how to handle if exception was set?
                if (_exception != null || ExceptionDetail == null)
                    return _exception;

                HasValidOrExceptionWasRead = true;
                _exception = ExceptionDetail.GetException();
                SecurityTokenInvalidSignatureException securityTokenInvalidSignatureException = _exception as SecurityTokenInvalidSignatureException;
                if (securityTokenInvalidSignatureException != null)
                {
                    securityTokenInvalidSignatureException.ExceptionDetail = ExceptionDetail;
                    securityTokenInvalidSignatureException.Source = "Microsoft.IdentityModel.Tokens";
                }

                return _exception;
            }
            set
            {
                _exception = value;
            }
        }

        /// <summary>
        /// Gets or sets the issuer that was validated.
        /// </summary>
        public string Issuer { get; set; }
    }
}
