// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Protocols
{
    /// <summary>
    /// base class for authentication protocol messages.
    /// </summary>
    public abstract class AuthenticationProtocolMessage
    {
        private string _postTitle = "Working...";
        private string _scriptButtonText = "Submit";
        private string _scriptDisabledText = "Script is disabled. Click Submit to continue.";

        private Dictionary<string, string> _parameters = new Dictionary<string, string>();
        private string _issuerAddress = string.Empty;

        /// <summary>
        /// Initializes a default instance of the <see cref="AuthenticationProtocolMessage"/> class.
        /// </summary>
        protected AuthenticationProtocolMessage()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="AuthenticationProtocolMessage"/> class with a specific issuerAddress.
        /// </summary>
        protected AuthenticationProtocolMessage(string issuerAddress)
        {
            IssuerAddress = issuerAddress;
        }

        /// <summary>
        /// Builds a form post using the current IssuerAddress and the parameters that have been set.
        /// </summary>
        /// <returns>html with head set to 'Title', body containing a hiden from with action = IssuerAddress.</returns>
        public virtual string BuildFormPost()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<html><head><title>");
            strBuilder.Append(PostTitle);
            strBuilder.Append("</title></head><body><form method=\"POST\" name=\"hiddenform\" action=\"");
            strBuilder.Append(Uri.EscapeDataString(IssuerAddress));
            strBuilder.Append("\">");
            foreach (KeyValuePair<string, string> parameter in _parameters)
            {
                strBuilder.Append("<input type=\"hidden\" name=\"");
                strBuilder.Append(Uri.EscapeDataString(parameter.Key));
                strBuilder.Append("\" value=\"");
                strBuilder.Append(Uri.EscapeDataString(parameter.Value));
                strBuilder.Append("\" />");
            }

            strBuilder.Append("<noscript><p>");
            strBuilder.Append(ScriptDisabledText);
            strBuilder.Append("</p><input type=\"submit\" value=\"");
            strBuilder.Append(ScriptButtonText);
            strBuilder.Append("\" /></noscript>");
            strBuilder.Append("</form><script language=\"javascript\">window.setTimeout('document.forms[0].submit()', 0);</script></body></html>");
            return strBuilder.ToString();
        }

        /// <summary>
        /// Builds a Url using the current IssuerAddress and the parameters that have been set.
        /// </summary>
        /// <returns>UrlEncoded string.</returns>
        /// <remarks>Each parameter &lt;Key, Value> is first transformed using <see cref="HttpUtility.UrlEncode(string)"/>.</remarks>
        public virtual string BuildRedirectUrl()
        {
            StringBuilder strBuilder = new StringBuilder(_issuerAddress);
            bool issuerAddressHasQuery = _issuerAddress.Contains("?");
            foreach (KeyValuePair<string, string> parameter in _parameters)
            {
                if (parameter.Value == null)
                {
                    continue;
                }

                if (!issuerAddressHasQuery)
                {
                    strBuilder.Append('?');
                    issuerAddressHasQuery = true;
                }
                else
                {
                    strBuilder.Append('&');
                }

                strBuilder.Append(Uri.EscapeDataString(parameter.Key));
                strBuilder.Append('=');
                strBuilder.Append(Uri.EscapeDataString(parameter.Value));
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// Returns a parameter.
        /// </summary>
        /// <param name="parameter">The parameter name.</param>
        /// <returns>The value of the parameter or null if the parameter does not exists.</returns>
        /// <exception cref="ArgumentException">parameter is null</exception>
        public virtual string GetParameter(string parameter)
        {
            if (String.IsNullOrEmpty(parameter))
            {
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10000, GetType() + ": parameter"), typeof(ArgumentNullException), EventLevel.Verbose);
            }

            string value = null;
            _parameters.TryGetValue(parameter, out value);
            return value;
        }
        
        /// <summary>
        /// Gets or sets the issuer address.
        /// </summary>
        /// <exception cref="ArgumentNullException">The 'value' is null.</exception>
        public string IssuerAddress
        {
            get
            {
                return _issuerAddress;
            }
            set
            {
                if (value == null)
                {
                    LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10001, GetType() + ": IssuerAddress"), typeof(ArgumentNullException), EventLevel.Verbose);
                }

                _issuerAddress = value;
            }
        }

        /// <summary>
        /// Gets the message parameters as a Dictionary.
        /// </summary>
        public IDictionary<string, string> Parameters
        {
            get 
            { 
                return _parameters; 
            }
        }

        /// <summary>
        /// Gets or sets the title used when constructing the post string.
        /// </summary>
        /// <exception cref="ArgumentNullException">if the 'value' is null.</exception>
        public string PostTitle 
        {
            get
            { 
                return _postTitle; 
            }

            set
            {
                if (value == null)
                {
                    LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10001, GetType() + ": PostTitle"), typeof(ArgumentNullException), EventLevel.Verbose);
                }

                _postTitle = value;
            }
        }

        /// <summary>
        /// Removes a parameter.
        /// </summary>
        /// <param name="parameter">The parameter name.</param>
        /// <exception cref="ArgumentNullException">if 'parameter' is null or empty.</exception>
        public virtual void RemoveParameter(string parameter)
        {
            if (String.IsNullOrEmpty(parameter))
            {
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10000, GetType() + ": parameter"), typeof(ArgumentNullException), EventLevel.Verbose);
            }

            if (_parameters.ContainsKey(parameter))
            {
                _parameters.Remove(parameter);
            }
        }

        /// <summary>
        /// Sets a parameter to the Parameters Dictionary.
        /// </summary>
        /// <param name="parameter">The parameter name.</param>
        /// <param name="value">The value to be assigned to parameter.</param>
        /// <exception cref="ArgumentNullException">if 'parameterName' is null or empty.</exception>
        /// <remarks>If null is passed as value and a parameter exists, that parameter is removed.</remarks>
        public void SetParameter(string parameter, string value) 
        {
            if (String.IsNullOrEmpty(parameter))
            {
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10000, GetType() + ": parameter"), typeof(ArgumentNullException), EventLevel.Verbose);
            }

            if (value == null)
            {
                if (_parameters.ContainsKey(parameter))
                {
                    _parameters.Remove(parameter);
                }
            }
            else
            {
                _parameters[parameter] = value;
            }
        }

        /// <summary>
        /// Sets a collection parameters.
        /// </summary>
        /// <param name="nameValueCollection"></param>
        public virtual void SetParameters(NameValueCollection nameValueCollection)
        {
            if (nameValueCollection == null)
                return;

            foreach (string key in nameValueCollection.AllKeys)
            {
                SetParameter(key, nameValueCollection[key]);
            };
        }

        /// <summary>
        /// Gets or sets the script button text used when constructing the post string.
        /// </summary>
        /// <exception cref="ArgumentNullException">if the 'value' is null.</exception>
        public string ScriptButtonText
        {
            get
            {
                return _scriptButtonText;
            }

            set
            {
                if (value == null)
                {
                    LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10001, GetType() + ": ScriptButtonText"), typeof(ArgumentNullException), EventLevel.Verbose);
                }

                _scriptButtonText = value;
            }
        }

        /// <summary>
        /// Gets or sets the text used when constructing the post string that will be displayed to used if script is disabled.
        /// </summary>
        /// <exception cref="ArgumentNullException">if the 'value' is null.</exception>
        public string ScriptDisabledText
        {
            get
            {
                return _scriptDisabledText;
            }

            set
            {
                if (value == null)
                {
                    LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10001, GetType() + ": ScriptDisabledText"), typeof(ArgumentNullException), EventLevel.Verbose);
                }

                _scriptDisabledText = value;
            }
        }       
    }
}