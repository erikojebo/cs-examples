using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace _6_code_smells.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var path = Path.Combine(Path.GetTempPath(), "_6_code_smells.xml");
            var xml = System.IO.File.ReadAllText(path);
            var document = XDocument.Parse(xml);


            var contactNodes = document.Descendants("Contact")
                                       .Select(x =>
                                           {
                                               var dictionary = new Dictionary<string, string>();

                                               dictionary["PhoneNumber"] = x.Attribute("PhoneNumber").Value;
                                               dictionary["LastName"] = x.Attribute("LastName").Value;
                                               dictionary["FirstName"] = x.Attribute("FirstName").Value;
                                               dictionary["EmailAddress"] = x.Attribute("EmailAddress").Value;

                                               return dictionary;
                                           }).OrderBy(x => x["LastName"])
                                           .ToList();

            return View(contactNodes);
        }
    }
}
