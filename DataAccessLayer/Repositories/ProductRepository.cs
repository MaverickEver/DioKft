using System;
using System.Collections.Generic;
using MS.WebSolutions.DioKft.DataAccessLayer.Repositories;
using MS.WebSolutions.DioKft.DataAccessLayer.Contexts;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Entities
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext context;

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Find(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> ListAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
