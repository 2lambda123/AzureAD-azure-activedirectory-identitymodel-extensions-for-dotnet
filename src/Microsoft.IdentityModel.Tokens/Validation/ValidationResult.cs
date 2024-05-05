// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Contains artifacts obtained when a SecurityToken is validated.
    /// A <see cref="TokenValidationResult"/> returns a collection of <see cref="ValidationResult"/> for each step in the token validation.
    /// </summary>
    public abstract class ValidationResult
    {
        private bool _isValid = false;

        /// <summary>
        /// Creates an instance of <see cref="TokenValidationResult"/>
        /// </summary>
        public ValidationResult()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="Exception"/> that occurred during validation.
        /// </summary>
        public virtual Exception Exception { get; set; }

        /// <summary>
        /// True if the token was successfully validated, false otherwise.
        /// </summary>
        public bool IsValid
        {
            get
            {
                HasValidOrExceptionWasRead = true;
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasValidOrExceptionWasRead { get; protected set; }

        /// <summary>
        /// Adds a new stack frame to the exception details.
        /// </summary>
        /// <param name="stackFrame"></param>
        public void AddStackFrame(StackFrame stackFrame)
        {
            ExceptionDetail.StackFrames.Add(stackFrame);
        }

        /// <summary>
        /// Logs the validation result.
        /// </summary>
#pragma warning disable CA1822 // Mark members as static
        public void Log()
#pragma warning restore CA1822 // Mark members as static
        {

        }

        internal ExceptionDetail ExceptionDetail { get; set; }

        internal IList<LogDetail> LogDetails { get; } = new List<LogDetail>();

        internal CallContext CallContext { get; set; }

        /// <summary>
        /// Gets the <see cref="ValidationFailureType"/> indicating why the validation was not satisfied.
        /// This should not be set to null.
        /// </summary>
        public ValidationFailureType ValidationFailureType
        {
            get;
            set;
        } = ValidationFailureType.ValidationNotEvaluated;

        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        public string ValidationMessage { get; }
    }
}
