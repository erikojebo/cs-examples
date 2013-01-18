using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using _6_code_smells.Models;

namespace _6_code_smells.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var document = ContactRepository.LoadXmlDocument();

            try
            {
                var contactNodes = ContactRepository.ParseContacts(document);

                return View(contactNodes);
            }
            catch (Exception e)
            {
                Logger.WriteLogMessage(e);

                return ErrorView("Något gick snett när kontakterna skulle laddas...");
            }
        }

        public ActionResult Search(string searchString)
        {
            var document = ContactRepository.LoadXmlDocument();

            try
            {
                var contacts = ContactRepository.ParseContacts(document);

                var filteredContacts = contacts.Where(x => x.Matches(searchString)).ToList();

                return View("Index", filteredContacts);
            }
            catch (Exception e)
            {
                Logger.WriteLogMessage(e);
                return ErrorView("Något gick snett under sökningen");
            }
        }

        private ActionResult ErrorView(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }

        

        
    }
}