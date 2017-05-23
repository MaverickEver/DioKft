using System.Collections.Generic;

namespace MS.WebSolutions.DioKft.Models.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
            this.Contacts = new List<ContactModel>();
            this.Email = new Email();
        }

        public IEnumerable<ContactModel> Contacts { get; set; }
        public Email Email { get; set; }
    }
}