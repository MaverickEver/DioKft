using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using MS.WebSolutions.DioKft.DataAccessLayer.Repositories;
using MS.WebSolutions.DioKft.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MS.WebSolutions.DioKft.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var contacts = this.GetAllContacts();
            return View(contacts);
        }

        public ActionResult Admin()
        {
            var contacts = this.GetAllContacts();
            return View(contacts);
        }

        [HttpPost]
        public ActionResult CreateContact(ContactModel newContact)
        {
            if (newContact == null)
            {
                throw new ArgumentNullException($"The {nameof(newContact)} cannot be null.");
            }

            var contactToSave = new Contact
            {
                Name = newContact.Name,
                Email = newContact.Email,
                PhoneNumber = newContact.PhoneNumber,
                Role = newContact.Role,
                Imageurl = newContact.ImageUrl
            };

            try
            {
                var repository = new ContactRepository();
                repository.Save(contactToSave);
            }
            catch
            {
                return Json("Saving of new contact has been failed.");
            }

            return Json(newContact);
        }

        [HttpPost]
        public ActionResult RemoveContact(int id)
        {
            try
            {
                var repository = new ContactRepository();
                repository.Delete(id);
            }
            catch
            {
                return Json("Removing contact has been failed.");
            }

            return Json(id);
        }

        # region [BusinessLogic]
        private IEnumerable<ContactModel> GetAllContacts()
        {
            var contactModelList = new List<ContactModel>();        

            var repository = new ContactRepository();
            var contanctsFromDb = repository.ListAll();

            foreach (var contactInDb in contanctsFromDb)
            {
                var contactModel = (ContactModel)contactInDb;
                contactModelList.Add(contactModel);
            }
            
            return contactModelList;
        }
        #endregion
    }
}