// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Contains information so that logs can be written when needed.
    /// </summary>
    internal class ExceptionDetail
    {
        /// <summary>
        /// Creates an instance of <see cref="ExceptionDetail"/>
        /// </summary>
        public ExceptionDetail(MessageDetail messageDetails, Type exceptionType, StackFrame stackFrame)
            : this(messageDetails, exceptionType, stackFrame, null)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="ExceptionDetail"/>
        /// </summary>
        public ExceptionDetail(MessageDetail messageDetails, Type exceptionType, StackFrame stackFrame, Exception innerException)
        {
            ExceptionType = exceptionType;
            InnerException = innerException;
            MessageDetail = messageDetails;
            StackFrames.Add(stackFrame);
        }

        public Exception GetException()
        {
            if (InnerException != null)
                return Activator.CreateInstance(ExceptionType, MessageDetail.Message, InnerException) as Exception;

            return Activator.CreateInstance(ExceptionType, MessageDetail.Message) as Exception;
        }

        public Type ExceptionType { get; }

        public Exception InnerException { get; }

        public MessageDetail MessageDetail { get; }

        public IList<StackFrame> StackFrames { get; } = [];
    }
}
