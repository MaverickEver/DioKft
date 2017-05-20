
using System;
using System.Linq;
using System.Collections.Generic;
using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using MS.WebSolutions.DioKft.DataAccessLayer.Contexts;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Repositories
{
    public class ContactRepository : RepositoryBase<ContactContext>, IRepository<Contact, int>
    {
        public void Delete(int id)
        {
            Contact contactToDelete;
            if (!TryGet(id, out contactToDelete)) { return; }

            this.context.Contacts.Remove(contactToDelete);
            this.context.SaveChanges();
        }

        public IEnumerable<Contact> ListAll()
        {
            return (from p in this.context.Contacts
                    select p).ToList();
        }

        public void Save(Contact entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            this.context.Contacts.Add(entity);
            this.context.SaveChanges();
        }

        public bool TryGet(int id, out Contact entity)
        {
            entity = (from p in this.context.Contacts
                      where p.Id == id
                      select p).SingleOrDefault();

            return entity != null;
        }
    }
}
