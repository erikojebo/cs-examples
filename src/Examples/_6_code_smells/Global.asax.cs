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
    <Contact FirstName='Kalle' LastName='Karlsson' PhoneNumber='555-123456' EmailAddress='kalle.karlsson@swipnet.se' />
    <Contact FirstName='Pelle' LastName='Svensson' PhoneNumber='555-123456' EmailAddress='pelle.svensson@swipnet.se' />
    <Contact FirstName='Nisse' LastName='Nilssson' PhoneNumber='555-123456' EmailAddress='nisse.nilssson@swipnet.se' />
    <Contact FirstName='Sture' LastName='Karlsson' PhoneNumber='555-123456' EmailAddress='sture.karlsson@swipnet.se' />
</Contacts>";

            const string fileName = "_6_code_smells.xml";
            var path = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllText(path, fileContent);
        }
    }
}