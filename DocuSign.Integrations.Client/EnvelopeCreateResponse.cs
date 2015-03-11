// -----------------------------------------------------------------------
// <copyright file="EnvelopeCreateResponse.cs" company="Chris Stokes">
// Copyright (c) Chris Stokes
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Integrations.Client
{
    /// <summary>
    /// Wraps the json object that is returend when the client creates a new envelope
    /// This is useful for retrieving the envelope id to store as a reference
    /// </summary>
    [Serializable]
    public class EnvelopeCreateResponse
    {
        /// <summary>
        /// Id of the newly created envelope (can be used in future calls)
        /// </summary>
		public string envelopeId { get; set; }

        /// <summary>
        /// Status of the envelope
        /// </summary>
		public string status { get; set; }

        /// <summary>
        /// Date/time the status was last changed
        /// </summary>
		public string statusDateTime { get; set; }

        /// <summary>
        /// ??? I should look at the REST API Docco some more
        /// </summary>
		public string uri { get; set; }
    }
}
