using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace _6_code_smells.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Load the xml document
            var path = Path.Combine(Path.GetTempPath(), "_6_code_smells.xml");
            var xml = System.IO.File.ReadAllText(path);
            var document = XDocument.Parse(xml);


            // Parse contacts
            try
            {
                var contactNodes = document.Descendants("Contact")
                                           .Select(x =>
                                               {
                                                   var dictionary = new Dictionary<string, object>();


                                                   // Parse name
                                                   dictionary["LastName"] = x.Attribute("LastName").Value;
                                                   dictionary["FirstName"] = x.Attribute("FirstName").Value;


                                                   dictionary["PhoneNumber"] = x.Attribute("PhoneNumber").Value;


                                                   // Parse email addresses
                                                   var emailAddresses = new List<string>();

                                                   foreach (var emailAddress in x.Descendants("EmailAddress"))
                                                   {
                                                       emailAddresses.Add(emailAddress.Value);
                                                   }

                                                   dictionary["EmailAddresses"] = emailAddresses;


                                                   return dictionary;
                                               })
                                           .OrderBy(x => x["LastName"])
                                           .ThenBy(x => x["FirstName"])
                                           .ToList();

                return View(contactNodes);
            }
            catch (Exception e)
            {
                // Write log message
                var lp = GetPath();
                using (var stream = new FileStream(lp, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(string.Format("{0}: {1}", DateTime.Now, e.Message));
                }
                ViewBag.Message = "Något gick snett när kontakterna skulle laddas...";
                return View("Error");
            }
        }

        public ActionResult Search(string searchString)
        {
            // Load the xml document
            var path = Path.Combine(Path.GetTempPath(), "_6_code_smells.xml");
            var xml = System.IO.File.ReadAllText(path);
            var document = XDocument.Parse(xml);


            // Parse contacts
            try
            {
                var contactNodes = document.Descendants("Contact")
                                           .Where(x =>
                                               {
                                                   return x.Attribute("FirstName").Value.ToLower().Contains(searchString.ToLower()) ||
                                                       x.Attribute("LastName").Value.ToLower().Contains(searchString.ToLower()) ||
                                                       x.Descendants("EmailAddress").Any(e => x.Value.ToLower().Contains(searchString.ToLower()));
                                               })
                                           .Select(x =>
                                               {
                                                   var dictionary = new Dictionary<string, object>();


                                                   // Parse name
                                                   dictionary["LastName"] = x.Attribute("LastName").Value;
                                                   dictionary["FirstName"] = x.Attribute("FirstName").Value;


                                                   dictionary["PhoneNumber"] = x.Attribute("PhoneNumber").Value;


                                                   // Parse email addresses
                                                   var emailAddresses = new List<string>();

                                                   foreach (var emailAddress in x.Descendants("EmailAddress"))
                                                   {
                                                       emailAddresses.Add(emailAddress.Value);
                                                   }

                                                   dictionary["EmailAddresses"] = emailAddresses;


                                                   return dictionary;
                                               })
                                           .OrderBy(x => x["LastName"])
                                           .ThenBy(x => x["FirstName"])
                                           .ToList();

                return View("Index", contactNodes);
            }
            catch (Exception e)
            {
                // Write log message
                var lp = GetPath();
                using (var stream = new FileStream(lp, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(string.Format("{0}: {1}", DateTime.Now, e.Message));
                }
                ViewBag.Message = "Något gick snett när de privata kontakterna skulle laddas...";
                return View("Error");
            }
        }

        // Returns the path to the log file
        private static string GetPath()
        {
            return Path.Combine(Path.GetTempPath(), "_6_code_smells.log");
        }
    }
}