using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.WebSolutions.DioKft.Models.ViewModels
{
    public class AdminContactViewModel
    {
        public AdminContactViewModel()
        {
            this.Contacts = new List<ContactModel>();
            this.NewContact = new ContactModel();
        }

        public IEnumerable<ContactModel> Contacts {get;set;}
        public ContactModel NewContact { get; set; }
    }
}