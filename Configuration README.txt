An example configuration for the docusign client is as follows:


<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
	<!-- Add the following element to the config sections, "docuSignClientConfiguration" is the default name, but you can override that -->
    <section name="docuSignClientConfiguration" type="DocuSign.Integrations.Client.Configuration.DocuSignClientConfiguration, DocuSign.Integration.Client" requirePermission="false"/>
  </configSections>

  <!-- Docusign configuration settings, the element name must match the name set in the configsections element -->

  <docuSignClientConfiguration
    IntegratorKey=""
    DocuSignAddress="https://demo.docusign.net"
    WebServiceUrl="https://demo.docusign.net/restapi/v2"
    Email=""
    Password="" />
  
  <!-- Proxy configuration settings for the docusign client is configured to use system.net config
	For more details, see MSDN documentation
   -->
  <system.net>
    <defaultProxy useDefaultCredentials="true">
      <proxy usesystemdefault="True" />
    </defaultProxy>
  </system.net>

</configuration>