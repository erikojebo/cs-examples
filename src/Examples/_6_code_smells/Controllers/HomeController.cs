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
            var document = LoadXmlDocument();

            try
            {
                var contactNodes = ParseContacts(document);

                return View(contactNodes);
            }
            catch (Exception e)
            {
                WriteLogMessage(e);

                return ErrorView("Något gick snett när kontakterna skulle laddas...");
            }
        }

        public ActionResult Search(string searchString)
        {
            var document = LoadXmlDocument();

            try
            {
                var contacts = ParseContacts(document);

                var filteredContacts = FilterContacs(searchString, contacts);

                return View("Index", filteredContacts);
            }
            catch (Exception e)
            {
                return ErrorView("Något gick snett under sökningen");
            }
        }

        private List<Dictionary<string, object>> FilterContacs(string searchString, IEnumerable<Dictionary<string, object>> contact)
        {
            var filteredContacts = contact.Where(x => Matches(searchString, x["FirstName"]) || Matches(searchString, x["LastName"]) ||
                                                      ((List<string>)x["EmailAddresses"]).Any(e => Matches(searchString, e))).ToList();
            return filteredContacts;
        }

        private ActionResult ErrorView(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }

        private static void WriteLogMessage(Exception e)
        {
            var logFilePath = GetLogFilePath();

            using (var stream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(string.Format("{0}: {1}", DateTime.Now, e.Message));
            }
        }

        private bool Matches(string searchString, object value)
        {
            return ((string)value).ToLower().Contains(searchString.ToLower());
        }

        private static XDocument LoadXmlDocument()
        {
            var path = Path.Combine(Path.GetTempPath(), "_6_code_smells.xml");
            var xml = System.IO.File.ReadAllText(path);
            return XDocument.Parse(xml);
        }

        private static List<Dictionary<string, object>> ParseContacts(XDocument document)
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
            return contactNodes;
        }

        private static string GetLogFilePath()
        {
            return Path.Combine(Path.GetTempPath(), "_6_code_smells.log");
        }
    }
}