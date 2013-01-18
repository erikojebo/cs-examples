using System.Collections.Generic;
using System.Linq;

namespace _6_code_smells.Models
{
    public class Contact
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; }
        public IList<string> EmailAddresses { get; set; }

        public bool Matches(string searchString)
        {
            return Matches(searchString, FirstName) || Matches(searchString, LastName) || EmailAddresses.Any(e => Matches(searchString, e));
        }

        private bool Matches(string searchString, string value)
        {
            return value.ToLower().Contains(searchString.ToLower());
        }

    }
}