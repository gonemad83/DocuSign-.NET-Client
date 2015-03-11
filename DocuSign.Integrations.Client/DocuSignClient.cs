using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Integrations.Client
{
    /// <summary>
    /// Provides access to docusign api functionality
    /// </summary>
    public class DocuSignClient : IDisposable
    {
        #region Attributes

        protected Account _account;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the docusign from the default configuration
        /// </summary>
        public DocuSignClient()
        {
            // $TODO: Implement configuration
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new instance of the docusign from a named configuration element
        /// </summary>
        /// <param name="configurationName">Name of the configuration element</param>
        public DocuSignClient(string configurationName)
        {
            // $TODO: Implement configuration
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an instance of the docusign client with manual configuration
        /// </summary>
        /// <param name="integratorKey">This is the integrator key that can be found in the management panel</param>
        /// <param name="docuSignAddress">DocuSign Address (e.g. "https://demo.docusign.net")</param>
        /// <param name="webServiceUrl">DocuSign API Address (e.g. "https://demo.docusign.net/restapi/v2")</param>
        public DocuSignClient(string integratorKey, string docuSignAddress, string webServiceUrl)
        {
            RestSettings.Instance.IntegratorKey = integratorKey;
            RestSettings.Instance.DocuSignAddress = docuSignAddress;
            RestSettings.Instance.WebServiceUrl = webServiceUrl;
        }

        #endregion

        /// <summary>
        /// Login using configuration credentials
        /// </summary>
        public async Task LoginAsync()
        {
            // $TODO: Implement configuration
            throw new NotImplementedException();
        }

        /// <summary>
        /// Login using manual credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public async Task LoginAsync(string email, string password)
        {
            await Task.Run(() => {
                this._account = new Account();
                this._account.Email = email;
                this._account.Password = password;

                if (!this._account.Login())
                {
                    throw new Exception(string.Format("Login API call failed for user {0}.\nError Code:  {1}\nMessage:  {2}", 
                        this._account.Email, this._account.RestError.errorCode, this._account.RestError.message));
                }
            });
        }

        #region Public Methods

        #region Envelope Methods

        public async Task<string> CreateEnvelopeFromTemplate(string templateId, TemplateRole[] templateRoles, CustomFields customFields,
            string status, string subject = null)
        {
            var envelope = new EnvelopeCreate();
            envelope.templateId = templateId;
            envelope.templateRoles = templateRoles;
            envelope.customFields = customFields;

            if(!string.IsNullOrEmpty(status))
                envelope.emailSubject = subject;

            // $TODO: Change statuses to enums as developers shouldn't be expected to go and read rest API documentation to work it out
            envelope.status = status;

            var apiEndPoint = string.Format("{0}/envelopes", this._account.BaseUrl);
            var response = await ExecuteRESTRequestAsync<EnvelopeCreateResponse>(apiEndPoint, "POST", HttpStatusCode.Created, envelope);
            return response.envelopeId;
        }

        #endregion

        #region Template Methods

        /// <summary>
        /// Retrieves the template roles
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns>Collection of roles and tabs configured in the template</returns>
        public async Task<TemplateRole[]> RetrieveTemplateRolesAsync(string templateId)
        {
            var template = await RetrieveTemplateDetailsAsync(templateId);

            // $TODO: Add support for retrieving preconfigured 

            // need to order by routing order so as to correctly order the template roles
            var roles = template.AllRecipients.OrderBy(r => r.routingOrder)
                                              .GroupBy(r => r.roleName)
                                              .Select(rg => new TemplateRole
                                              {
                                                  roleName = rg.Key,
                                                  tabs = new RoleTabs
                                                  {
                                                      textTabs = rg.Where(r => r.tabs != null && r.tabs.textTabs != null)
                                                                   .SelectMany(r => r.tabs.textTabs.Select(t => new RoleTextTab { tabLabel = t.tabLabel, value = t.value })).ToArray()
                                                  }
                                              });

            return roles.ToArray();
        }

        /// <summary>
        /// Retrieves the template details from docusign
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public async Task<Template> RetrieveTemplateDetailsAsync(string templateId)
        {
            var apiEndPoint = string.Format("{0}/templates/{1}", this._account.BaseUrl, templateId);
            var template = await ExecuteRESTRequestAsync<Template>(apiEndPoint, "GET", HttpStatusCode.OK, null);
            return template;
        }

        #endregion

        #endregion

        #region Helper Methods
        
        protected virtual async Task<ResponseType> ExecuteRESTRequestAsync<ResponseType>(string apiEndPoint, string httpMethod, HttpStatusCode expectedResponse, object request)
        {
            // get template definition
            RequestBuilder builder = new RequestBuilder();
            RequestInfo req = new RequestInfo();
            List<RequestBody> requestBodies = new List<RequestBody>();

            req.RequestContentType = "application/json";
            req.AcceptContentType = "application/json";
            req.HttpMethod = httpMethod;
            req.LoginEmail = this._account.Email;
            req.ApiPassword = this._account.ApiPassword;
            req.DistributorCode = RestSettings.Instance.DistributorCode;
            req.DistributorPassword = RestSettings.Instance.DistributorPassword;
            req.IntegratorKey = RestSettings.Instance.IntegratorKey;
            req.Uri = apiEndPoint;

            if (request != null)
            {
                var jSettings = new JsonSerializerSettings();
                jSettings.NullValueHandling = NullValueHandling.Ignore;
                req.RequestBody = new RequestBody[] { new RequestBody { Text = JsonConvert.SerializeObject(request, Formatting.Indented, jSettings) } };
            }

            builder.Request = req;
            
            ResponseInfo response = await Task.Run(() => { return builder.MakeRESTRequest(); });
            
            if (response.StatusCode != expectedResponse)
            {
                try
                {
                    throw new Exception(Error.FromJson(response.ResponseText).message);
                }
                catch
                {
                    throw new Exception(response.ResponseText);
                }
            }

            return JsonConvert.DeserializeObject<ResponseType>(response.ResponseText);
        }

        #endregion

        #region IDisposable

        protected bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
                return;

            if(disposing)
            {
                // Managed Cleanup
            }

            // unmanaged cleanup

            this._disposed = true;
        }

        #endregion
    }
}
