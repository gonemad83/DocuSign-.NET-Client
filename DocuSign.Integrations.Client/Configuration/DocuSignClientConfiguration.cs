// -----------------------------------------------------------------------
// <copyright file="DocuSignClientConfiguration.cs" company="Chris Stokes">
// Copyright (c) Chris Stokes
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Integrations.Client.Configuration
{
    /// <summary>
    /// Provides support for configuration via config files
    /// </summary>
    public class DocuSignClientConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Returns the first found configuration
        /// </summary>
        /// <returns></returns>
        public static DocuSignClientConfiguration GetConfig()
        {
            var config = (DocuSignClientConfiguration)System.Configuration.ConfigurationManager.GetSection("*");

            if (config == null)
                throw new ConfigurationException("No DocuSignClientConfiguration element found in config file");

            return config;
        }

        /// <summary>
        /// Returns the configuration section by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DocuSignClientConfiguration GetConfig(string name)
        {
            var config = (DocuSignClientConfiguration)System.Configuration.ConfigurationManager.GetSection(name);

            if (config == null)
                throw new ConfigurationException(string.Format("No DocuSignClientConfiguration element \"{0}\" found in config file", name));

            return config;
        }

        // $TODO: Add additional login configuration

        /// <summary>
        /// Docusign Integrator key
        /// </summary>
        [ConfigurationProperty("IntegratorKey", IsRequired = true)]
        public string IntegratorKey
        {
            get { return (string)this["IntegratorKey"]; }
        }

        /// <summary>
        /// Docusign Address (i.e. https://demo.docusign.com)
        /// </summary>
        [ConfigurationProperty("DocuSignAddress", IsRequired = true)]
        public string DocuSignAddress
        {
            get { return (string)this["DocuSignAddress"]; }
        }

        /// <summary>
        /// Docusign Web Service Base Addres (i.e. https://demo.docusign.com/restapi/v2)
        /// </summary>
        [ConfigurationProperty("WebServiceUrl", IsRequired = true)]
        public string WebServiceUrl
        {
            get { return (string)this["WebServiceUrl"]; }
        }

        /// <summary>
        /// Email address to be used as log in credentials
        /// </summary>
        [ConfigurationProperty("Email", IsRequired=true)]
        public string Email
        {
            get { return (string)this["Email"]; }
        }

        /// <summary>
        /// Password to be used as log in credentials
        /// </summary>
        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["Password"]; }
        }
    }
}
