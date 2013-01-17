using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace _6_code_smells
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateDummyData();
        }

        private void CreateDummyData()
        {
            var fileContent = @"<Contacts>
  <Contact FirstName='Kalle' LastName='Karlsson' PhoneNumber='555-123456'>
    <EmailAddresses>
      <EmailAddress Type='1'>kalle.karlsson@swipnet.se</EmailAddress>
    </EmailAddresses>
  </Contact>
  <Contact FirstName='Pelle' LastName='Svensson' PhoneNumber='555-123456'>
    <EmailAddresses>
      <EmailAddress Type='1'>pelle.svensson@swipnet.se</EmailAddress>
    </EmailAddresses>
    <EmailAddresses>
      <EmailAddress Type='2'>pelle.svensson@acme.se</EmailAddress>
    </EmailAddresses>
  </Contact>
  <Contact FirstName='Nisse' LastName='Nilssson' PhoneNumber='555-123456'>
    <EmailAddresses>
      <EmailAddress Type='1'>nisse.nilssson@swipnet.se</EmailAddress>
    </EmailAddresses>
  </Contact>
  <Contact FirstName='Sture' LastName='Karlsson' PhoneNumber='555-123456'>
    <EmailAddresses>
      <EmailAddress Type='1'>sture.karlsson@swipnet.se</EmailAddress>
    </EmailAddresses>
    <EmailAddresses>
      <EmailAddress Type='2'>sture@karlssoncorp.com</EmailAddress>
    </EmailAddresses>
  </Contact>
</Contacts>";

            const string fileName = "_6_code_smells.xml";
            var path = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllText(path, fileContent);
        }
    }
}