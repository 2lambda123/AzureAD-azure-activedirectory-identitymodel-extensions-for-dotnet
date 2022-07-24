// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace System.IdentityModel.Tokens.Jwt
{
    /// <summary>
    /// Constants for Json Web tokens.
    /// </summary>
    public static class JsonClaimValueTypes
    {
        /// <summary>
        /// A URI that represents the JSON XML data type.
        /// </summary>
        /// <remarks>When mapping json to .Net Claim(s), if the value was not a string (or an enumeration of strings), the ClaimValue will serialized using the current JSON serializer, a property will be added with the .Net type and the ClaimTypeValue will be set to 'JsonClaimValueType'.</remarks>
        public const string Json = Microsoft.IdentityModel.JsonWebTokens.JsonClaimValueTypes.Json;

        /// <summary>
        /// A URI that represents the JSON array XML data type.
        /// </summary>
        /// <remarks>When mapping json to .Net Claim(s), if the value was not a string (or an enumeration of strings), the ClaimValue will serialized using the current JSON serializer, a property will be added with the .Net type and the ClaimTypeValue will be set to 'JsonClaimValueType'.</remarks>
        public const string JsonArray = Microsoft.IdentityModel.JsonWebTokens.JsonClaimValueTypes.JsonArray;

        /// <summary>
        /// A URI that represents the JSON null data type
        /// </summary>
        /// <remarks>When mapping json to .Net Claim(s), we use empty string to represent the claim value and set the ClaimValueType to JsonNull</remarks>
        public const string JsonNull = Microsoft.IdentityModel.JsonWebTokens.JsonClaimValueTypes.JsonNull;
    }
}
