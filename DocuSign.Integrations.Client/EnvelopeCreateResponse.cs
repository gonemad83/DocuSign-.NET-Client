using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Integrations.Client
{
    [Serializable]
    public class EnvelopeCreateResponse
    {
		public string envelopeId { get; set; }
		public string status { get; set; }
		public string statusDateTime { get; set; }
		public string uri { get; set; }
    }
}
