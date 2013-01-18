using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using _6_code_smells.Models;

namespace _6_code_smells.Controllers
{
    public class ContactRepository
    {
        public static XDocument LoadXmlDocument()
        {
            var path = Path.Combine(Path.GetTempPath(), "_6_code_smells.xml");
            var xml = System.IO.File.ReadAllText(path);
            return XDocument.Parse(xml);
        }

        public static List<Contact> ParseContacts(XDocument document)
        {
            var contactNodes = document.Descendants("Contact")
                                       .Select(x =>
                                       {
                                           var contact = new Contact();

                                           contact.FirstName = x.Attribute("LastName").Value;
                                           contact.LastName = x.Attribute("FirstName").Value;
                                           contact.PhoneNumber = x.Attribute("PhoneNumber").Value;
                                           contact.EmailAddresses = ParseEmailAddresses(x);

                                           return contact;
                                       })
                                       .OrderBy(x => x.LastName)
                                       .ThenBy(x => x.FirstName)
                                       .ToList();
            return contactNodes;
        }

        public static List<string> ParseEmailAddresses(XElement x)
        {
            var emailAddresses = new List<string>();

            foreach (var emailAddress in x.Descendants("EmailAddress"))
            {
                emailAddresses.Add(emailAddress.Value);
            }
            return emailAddresses;
        } 
    }
}