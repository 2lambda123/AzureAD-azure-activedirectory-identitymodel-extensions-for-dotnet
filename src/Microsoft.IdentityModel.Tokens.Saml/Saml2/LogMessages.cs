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

namespace Microsoft.IdentityModel.Tokens.Saml2
{
    /// <summary>
    /// Log messages and codes for Saml2Processing
    /// Range: IDX11000 - IDX11200
    /// </summary>
    internal static class LogMessages
    {
#pragma warning disable 1591
        // general
        internal const string IDX11000 = "IDX11000: The parameter '{0}' cannot be a 'null' or an empty object.";

        // properties, configuration 
        internal const string IDX11010 = "IDX11010: MaximumTokenSizeInBytes must be greater than zero. value: '{0}'";
        internal const string IDX11011 = "IDX11011: TokenLifetimeInMinutes must be greater than zero. value: '{0}'";
        internal const string IDX11012 = "IDX11012: Uri must be an AbsoluteUri is: '{0}'";
        internal const string IDX11013 = "IDX11013: token has length: '{0}' which is larger than the MaximumTokenSizeInBytes: '{1}'.";

        // token validation
        internal const string IDX11040 = "IDX11040: token has length: '{0}' which is larger than the MaximumTokenSizeInBytes: '{1}'.";
        internal const string IDX11041 = "IDX11041: SecurityTokens must be signed. SecurityToken: '{0}'.";
        internal const string IDX11042 = "IDX11042: Unable to create claims from securityToken, 'issuer' is null or empty.";
        internal const string IDX11043 = "IDX11043: Lifetime validation failed. Delegate returned false, securitytoken: '{0}'.";
        internal const string IDX11044 = "IDX11044: Audience validation failed. Delegate returned false, securitytoken: '{0}'.";
        internal const string IDX11045 = "IDX11045: Replay detection requires that token's expiration period, '{0}', does not exceed the expiration period configured with '{1}' on the TokenReplayCache.";
        internal const string IDX11046 = "IDX11046: A SAML2 assertion that specifies an AuthenticationContext DeclarationReference is not supported.To handle DeclarationReference, extend the Saml2SecurityTokenHandler and override ProcessAuthenticationStatement.";

        // Saml2SecurityTokenHandler logging
        internal const string IDX11070 = "IDX11070: The '{0}', can only process SecurityTokens of type: '{1}'. The SecurityToken received is of type: '{2}'.";
        internal const string IDX11071 = "IDX11071: Creating SamlSecurityToken: Issuer: '{0}', Audience: '{1}'";

        // Saml2SecurityTokenHandler reading
        internal const string IDX11100 = "IDX11100: The '{0}', can only process SecurityTokens of type: '{1}'. The SecurityToken received is of type: '{2}'.";
        internal const string IDX11101 = "IDX11101: Creating Saml2SecurityToken: Issuer: '{0}', Audience: '{1}'";
        internal const string IDX11102 = "IDX11102: Exception thrown while reading '{0}' for Saml2SecurityToken. See inner exception for more details.";
        internal const string IDX11103 = "IDX11103: Unable to read '{0}' for Saml2SecurityToken. The element type '{1}' appears to have a custom format that cannot be parsed. If this is expected, you will need to override '{2}.{3}'.";
        internal const string IDX11104 = "IDX11104: Unable to read Saml2SecurityToken. Expecting XmlReader to be at element: '{0}', found 'Empty Element'";
        internal const string IDX11105 = "IDX11105: Unable to read Saml2SecurityToken. Expecting XmlReader to be at element: '{0}', found: '{1}'.";
        internal const string IDX11106 = "IDX11106: Unable to read for Saml2SecurityToken. Element: '{0}' as missing Attribute: '{1}'.";
        internal const string IDX11107 = "IDX11107: When reading '{0}', '{1}' was not a Absolute Uri, was: '{2}'.";
        internal const string IDX11108 = "IDX11108: When reading '{0}', Assertion.Subject is null and no Statements were found. [Saml2Core, line 585].";
        internal const string IDX11109 = "IDX11109: When reading '{0}', Assertion.Subject is null and an Authentication, Attribute or AuthorizationDecision Statement was found. and no Statements were found. [Saml2Core, lines 1050, 1168, 1280].";
        internal const string IDX11110 = "IDX11110: The SamlSecurityToken must have a value for its Assertion property.";
        internal const string IDX11111 = "IDX11111: Saml2Assertion.Issuer cannont be null or empty when creating claims.";
        internal const string IDX11112 = "IDX11112: A Saml2SecurityToken cannot be created from the Saml2Assertion because it contains a SubjectConfirmationData which specifies an Address value. Enforcement of this value is not supported by default. To customize SubjectConfirmationData processing, extend Saml2SecurityTokenHandler and override ValidateConfirmationData.";
        internal const string IDX11113 = "IDX11113: A Saml2SecurityToken cannot be created from the Saml2Assertion because it contains a SubjectConfirmationData which specifies an InResponseTo value. Enforcement of this value is not supported by default. To customize SubjectConfirmationData processing, extend Saml2SecurityTokenHandler and override ValidateConfirmationData.";
        internal const string IDX11114 = "IDX11114: A Saml2SecurityToken cannot be created from the Saml2Assertion because it contains a SubjectConfirmationData which specifies a Recipient value.Enforcement of this value is not supported by default. To customize SubjectConfirmationData processing, extend Saml2SecurityTokenHandler and override ValidateConfirmationData.";
        internal const string IDX11115 = "IDX11115: The Saml2SecurityToken is rejected because the SAML2:Assertion's SubjectConfirmationData NotBefore is not satisfied. NotBefore: '{0}' Current time: '{1}'";
        internal const string IDX11116 = "IDX11116: The Saml2SecurityToken is rejected because the SAML2:Assertion's SubjectConfirmationData NotOnOrAfter is not satisfied. NotOnOrAfter: '{0}' Current time: '{1}'";
        internal const string IDX11117 = "IDX11117: A <saml:EncryptedAttribute> was encountered while processing the attribute statement.To handle encrypted attributes, extend the Saml2SecurityTokenHandler and override ReadAttributeStatement.";
        internal const string IDX11118 = "IDX11118: A <saml:AuthnContextDecl> element was encountered.To handle by-value authentication context declarations, extend Saml2SecurityTokenHandler and override ReadAuthenticationContext.In addition, it may be necessary to extend Saml2AuthenticationContext so that its data model can accommodate the declaration value.";
        internal const string IDX11119 = "IDX11119: An abstract element was encountered which does not specify its concrete type. Element name: '{0}' Namespace: '{1}'";
        internal const string IDX11120 = "IDX11120: A <saml:Conditions> element contained more than one '{0}' condition.";
        internal const string IDX11121 = "IDX11121: A <saml:Condition> was encountered which specifies an unrecognized concrete type.To handle a custom Condition, extend Saml2SecurityTokenHandler and override ReadConditions.";
        internal const string IDX11122 = "IDX11122: The <saml:Evidence> element must contain at least one assertion or assertion reference.";
        internal const string IDX11123 = "IDX11123: When reading '{0}', '{1}' was not a valid Uri, was: '{2}'.";
        internal const string IDX11124 = "IDX11124: The SAML NameIdentifier '{0}' is of format '{1}' and NameQualifier/SPNameQualifier/SPProvidedID is not omitted.";
        internal const string IDX11125 = "IDX11125: A Saml2Subject that does not specify an NameId cannot have an empty SubjectConfirmations collection.";
        internal const string IDX11126 = "IDX11126: A <saml:SubjectConfirmationData> element of an unexpected type was encountered.The SubjectConfirmationDataType and KeyInfoConfirmationDataType are handled by default. To handle other types, extend Saml2SecurityTokenHandler and override ReadSubjectConfirmationData. Name: '{0}' Namespace: '{1}'";
        internal const string IDX11127 = "IDX11127: A <saml:SubjectConfirmationData> element cannot be empty when of type KeyInfoConfirmationDataType.";
        internal const string IDX11128 = "IDX11128: A <saml:'{0}'> contained unrecognized content.The schema allows arbitrary XML elements on this element without explicit schema extension.To handle the content, extend Saml2Serializer.";
        internal const string IDX11129 = "IDX11129: The SAML2:AttributeStatement must contain at least one SAML2:Attribute.";
        internal const string IDX11130 = "IDX11130: A Saml2AudienceRestriction must specify at least one Audience.";
        internal const string IDX11131 = "IDX11131: When writing the Saml2AuthenticationContext, at least one of ClassReference and DeclarationReference must be set.Set one of these properties before serialization.";
        internal const string IDX11132 = "IDX11132: 'Saml2NameIdentifier' encrypting credentials must have a Symmetric Key specified.";
        internal const string IDX11133 = "IDX11133: The Saml2Assertion Statements collection contains an unrecognized Saml2Statement.To handle custom Saml2Statement objects, extend Saml2SecurityTokenHandler and override WriteStatement. Type: '{0}'";
        internal const string IDX11134 = "IDX11134: '{1}' was not a Absolute Uri, was: '{2}'.";
        internal const string IDX11135 = "IDX11135: An unrecognized value was encountered for the SAML2:AuthorizationDecisionStatement element's Decision attribute: '{0}'";
        internal const string IDX11136 = "IDX11136: Unable to read for Saml2SecurityToken. Required Element: '{0}' is missing or empty.";
        internal const string IDX11137 = "IDX11137: Unable to read for Saml2SecurityToken. Version must be '2.0' was: '{0}'.";
        internal const string IDX11138 = "IDX11138: Unable to read for Saml2SecurityToken. the AttributeStatement does not contain any Attributes.";

        // Saml2SecurityTokenHandler writing
        internal const string IDX11200 = "IDX11200: The Saml2SecurityTokenHandler can only write SecurityToken objects of type Saml2SecurityToken.";
        internal const string IDX11201 = "IDX11201: A Saml2SamlAttributeStatement can only have one Saml2Attribute of type 'Actor'. This special Saml2Attribute is used in delegation scenarios.";
        internal const string IDX11142 = "IDX11142: A Saml2SamlAttributeStatement can only have one Saml2Attribute of type 'Actor'. This special Saml2Attribute is used in delegation scenarios.";
        internal const string IDX11143 = "IDX11143: A Saml2SecurityToken cannot be created from the Saml2Assertion because it has no Subject.";
        internal const string IDX11144 = "IDX11144: A Saml2SecurityToken cannot be created from the Saml2Assertion because it has no SubjectConfirmation.";
        internal const string IDX11145 = "IDX11145: A Saml2SecurityToken cannot be created from the Saml2Assertion because it has more than one SubjectConfirmation.";
        internal const string IDX11146 = "IDX11146: A Saml2SecurityToken cannot be created from the Saml2Assertion because it specifies the Bearer confirmation method but identifies keys in the SubjectConfirmationData.";
        internal const string IDX11147 = "IDX11147: A Saml2SecurityToken cannot be created from the Saml2Assertion because it specifies the Holder-of-key confirmation method but identifies no keys in the SubjectConfirmationData.";
        internal const string IDX11148 = "IDX11148: A Saml2SecurityToken cannot be created from the Saml2Assertion because it specifies an unsupported confirmation method: '{0}'";
        internal const string IDX11149 = "IDX11149: Both AuthenticationContext ClassReference DeclarationReference can not be null.";

        internal const string IDX11300 = "IDX11300: '{0}' must be an absolute Uri, was: '{1}'";

        // NotSupported Exceptions
#pragma warning restore 1591
    }
}
