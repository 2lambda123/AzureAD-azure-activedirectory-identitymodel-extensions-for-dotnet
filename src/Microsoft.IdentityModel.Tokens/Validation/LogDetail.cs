// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Contains information so that logs can be written when needed.
    /// </summary>
    internal class LogDetail
    {
        /// <summary>
        /// Creates an instance of <see cref="LogDetail"/>
        /// </summary>
        public LogDetail(MessageDetail messageDetail, EventLogLevel eventLogLevel)
        {
            EventLogLevel = eventLogLevel;
            MessageDetail = messageDetail;
        }

        public EventLogLevel EventLogLevel { get; }

        public MessageDetail MessageDetail { get; }
    }
}
