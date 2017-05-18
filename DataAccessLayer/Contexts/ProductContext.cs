using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using System.Data.Entity;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Contexts
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
