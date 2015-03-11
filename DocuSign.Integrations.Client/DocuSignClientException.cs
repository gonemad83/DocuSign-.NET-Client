// -----------------------------------------------------------------------
// <copyright file="DocuSignClientException.cs" company="Chris Stokes">
// Copyright (c) Chris Stokes
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Integrations.Client
{
    /// <summary>
    /// Exception thrown by the docusign client
    /// </summary>
    public class DocuSignClientException : Exception
    {
        public DocuSignClientException() : base() { }
        public DocuSignClientException(string message) : base(message) { }
        public DocuSignClientException(string message, Exception innerException) : base(message, innerException) { }
        public DocuSignClientException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Creates a new instance of DocuSignClientException using a formatted message string
        /// </summary>
        /// <param name="message">Message format</param>
        /// <param name="args">Message format arguments</param>
        public DocuSignClientException(string message, params object[] args) : base(string.Format(message, args)) { }
    }
}
