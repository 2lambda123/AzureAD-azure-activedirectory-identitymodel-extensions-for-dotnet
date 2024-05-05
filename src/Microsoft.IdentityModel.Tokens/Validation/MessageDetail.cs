// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
    internal class MessageDetail
    {
        string _message;


        // TODO - to remove the need to create objects NonPII, we need to tuples <bool, object> where bool == true means object is PII.
        public MessageDetail(ReadOnlyMemory<char> messageId, params object[] parameters)
        {
            FormatString = messageId;
            Parameters = parameters;
        }

        public string Message
        {
            get
            {
                _message ??= LogHelper.FormatInvariant(FormatString.ToString(), Parameters);
                return _message;
            }
        }

        public ReadOnlyMemory<char> FormatString { get; }

        public object[] Parameters { get; }
    }
}
