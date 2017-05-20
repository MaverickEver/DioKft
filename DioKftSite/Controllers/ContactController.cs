using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using MS.WebSolutions.DioKft.DataAccessLayer.Repositories;
using MS.WebSolutions.DioKft.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.IO;

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
            var model = new AdminContactModel { Contacts = this.GetAllContacts() };       
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateContact(ContactModel newContact)
        {
            if (newContact == null || newContact.Image == null)
            {
                throw new ArgumentNullException($"The {nameof(newContact)} and {nameof(newContact.Image)} cannot be null.");
            }

            var contactToSave = new Contact
            {
                Name = newContact.Name,
                Email = newContact.Email,
                PhoneNumber = newContact.PhoneNumber,
                Role = newContact.Role               
            };            

            try
            {
                using (var repository = new ContactRepository())
                {
                    repository.Save(contactToSave);
                    this.UpdateImage(contactToSave.Id, newContact.Image, repository);
                }
            }
            catch
            {
                this.RemoveImage(contactToSave.ImageUrl);
                return Json("Saving of new contact has been failed.");                
            }

            //return PartialView("_ContactList",this.GetAllContacts());
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult RemoveContact(int id, string imageUrl)
        {
            try
            {
                using (var repository = new ContactRepository())
                {
                    repository.Delete(id);

                    var serverImageUrl = Server.MapPath(imageUrl);
                    this.RemoveImage(serverImageUrl);
                }
            }
            catch
            {                
                return Json("Removing contact has been failed.");
            }

            //return Content(string.Empty);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult UpdateContactImage(int id, HttpPostedFileBase image)
        {
            ContactModel contactModel;
            try
            {
                contactModel = this.UpdateImage(id, image);
            }
            catch
            {
                return Json("Updating contact has been failed.");
            }

            //return PartialView("_ContactItem", contactModel);
            return RedirectToAction("Admin");
        }       

        #region [BusinessLogic]
        private IEnumerable<ContactModel> GetAllContacts()
        {
            var contactModelList = new List<ContactModel>();

            using (var repository = new ContactRepository())
            {
                var contanctsFromDb = repository.ListAll();

                foreach (var contactInDb in contanctsFromDb)
                {
                    var contactModel = (ContactModel)contactInDb;
                    contactModelList.Add(contactModel);
                }
            }

            return contactModelList;
        }

        private string SaveImage(int id, HttpPostedFileBase image)
        {
            var path = string.Empty;
            try
            {
                var extension = Path.GetExtension(image.FileName);
                path = $"~/Content/DynamicResources/ContactImages/{id}{extension}";
                var serverPath = Server.MapPath(path);
                image.SaveAs(serverPath);
            }
            catch
            {
                return string.Empty;
            }
            
            return path;
        }

        private void RemoveImage(string imageurl)
        {
            if (!System.IO.File.Exists(imageurl)) { return; }

            System.IO.File.Delete(imageurl);
        }

        private ContactModel UpdateImage(int id, HttpPostedFileBase image)
        {
            using (var repository = new ContactRepository())
            {
                return this.UpdateImage(id, image, repository);
            }
        }

        private ContactModel UpdateImage(int id, HttpPostedFileBase image, ContactRepository repository)
        {
            var url = this.SaveImage(id, image);
            return (ContactModel)repository.UpdateImageUrl(id, url);
        }
        #endregion
    }
}