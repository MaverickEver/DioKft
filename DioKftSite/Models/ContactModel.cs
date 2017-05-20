using System.Web;
using DAL = MS.WebSolutions.DioKft.DataAccessLayer.Entities;
namespace MS.WebSolutions.DioKft.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }
        [Microsoft.Web.Mvc.FileExtensions(Extensions = "jpg, png")]
        public HttpPostedFileBase Image { get; set; }

        public static explicit operator ContactModel(DAL.Contact contact)
        {
            if (contact == null) return null;

            return new ContactModel
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                ImageUrl = contact.ImageUrl,
                Role = contact.Role
            };
        }
    }
}