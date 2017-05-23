using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using MS.WebSolutions.DioKft.DataAccessLayer.Repositories;
using MS.WebSolutions.DioKft.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using MS.WebSolutions.DioKft.Models.ViewModels;
using System.Net.Mail;
using System.Threading.Tasks;
using MS.WebSolutions.DioKft.Helpers;

namespace MS.WebSolutions.DioKft.Controllers
{
    public class ContactController : Controller
    {

        // GET: Contact
        public ActionResult Index()
        {
            var viewModel = new ContactViewModel { Contacts = this.GetAllContacts() };
            return View(viewModel);
        }

        public ActionResult Admin()
        {
            var viewModel = new AdminContactViewModel { Contacts = this.GetAllContacts() };       
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SendEmail(Email email)
        {
            MailMessage message = CreateEmailMessage(email);

            try
            {
                SendEmailMessage(message);
            }
            catch
            {
                return Json("A problem has occured during contacting us. Please try it again!");
            }

            return Json("Email has been sent.");
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
                var fileHandler = new FileHandler(Server);
                fileHandler.Remove(contactToSave.ImageUrl);
                
                return Json("Saving of new contact has been failed.");                
            }

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

                    var fileHandler = new FileHandler(Server);
                    fileHandler.Remove(imageUrl);
                }
            }
            catch
            {                
                return Json("Removing contact has been failed.");
            }

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

        private ContactModel UpdateImage(int id, HttpPostedFileBase image)
        {
            using (var repository = new ContactRepository())
            {
                return this.UpdateImage(id, image, repository);
            }
        }

        private ContactModel UpdateImage(int id, HttpPostedFileBase image, ContactRepository repository)
        {
            var fileHandler = new FileHandler(Server);
            var url = fileHandler.Save(id, image, FileLocations.ContactImages);
            return (ContactModel)repository.UpdateImageUrl(id, url);
        }

        private void SendEmailMessage(MailMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                 smtp.Send(message);
            }
        }

        private static MailMessage CreateEmailMessage(Email newEmail)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("sapi.mihaly@gmail.com"));
            message.From = new MailAddress(newEmail.EmailAddress);
            message.Subject = "New Request from diokft.hu website.";
            message.Body = string.Format(body, newEmail.Name, newEmail.EmailAddress, newEmail.Message);
            message.IsBodyHtml = true;
            return message;
        }
        #endregion
    }
}