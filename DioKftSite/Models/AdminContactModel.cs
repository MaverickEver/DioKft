using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.WebSolutions.DioKft.Models
{
    public class AdminContactModel
    {
        public AdminContactModel()
        {
            this.Contacts = new List<ContactModel>();
            this.NewContact = new ContactModel();
        }

        public IEnumerable<ContactModel> Contacts {get;set;}
        public ContactModel NewContact { get; set; }
    }
}