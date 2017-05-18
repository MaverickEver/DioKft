
using System;
using System.Collections.Generic;
using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using MS.WebSolutions.DioKft.DataAccessLayer.Contexts;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Repositories
{
    public class ContactRepository : IListableRepository, IRepository<Contact, int>
    {
        private readonly ContactContext context;

        public void Delete(Contact entity)
        {
            throw new NotImplementedException();
        }

        public Contact Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> ListAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Contact entity)
        {
            throw new NotImplementedException();
        }
    }
}
