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

namespace Microsoft.IdentityModel.Logging
{
    /// <summary>
    /// Log messages and codes for Microsoft.IdentityModel.Logging
    /// </summary>
    internal static class LogMessages
    {
#pragma warning disable 1591
        // general
        internal const string MIML10000 = "MIML10000: The parameter '{0}' cannot be a 'null' or an empty object.";
        internal const string MIML10001 = "MIML10001: The property value '{0}' cannot be a 'null' or an empty object.";

        // logging
        internal const string MIML11000 = "MIML11000: eventData.Payload is null or empty. Not logging any messages.";
        internal const string MIML11001 = "MIML11001: Cannot create the fileStream or StreamWriter to write logs. See inner exception.";
        internal const string MIML11002 = "MIML11002: Unknown log level: {0}.";

        // general exceptions
        internal const string IDX10000 = "IDX10000: The parameter '{0}' cannot be a 'null' or an empty object.";
#pragma warning restore 1591

    }
}
