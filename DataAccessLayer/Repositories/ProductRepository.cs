using System;
using System.Linq;
using System.Collections.Generic;
using MS.WebSolutions.DioKft.DataAccessLayer.Repositories;
using MS.WebSolutions.DioKft.DataAccessLayer.Contexts;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Entities
{
    public class ProductRepository : RepositoryBase<ProductContext>, IRepository<Product, int>
    {
        public void Delete(int id)
        {
            Product productToDelete;
            if (!TryGet(id, out productToDelete)) { return; }

            this.context.Products.Remove(productToDelete);
            this.context.SaveChanges();
        }
                
        public IEnumerable<Product> ListAll()
        {
            return (from p in this.context.Products
                   select p).ToList();
        }

        public void Save(Product entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            this.context.Products.Add(entity);
            this.context.SaveChanges();
        }

        public bool TryGet(int id, out Product entity)
        {
            entity = (from p in this.context.Products
                          where p.Id == id
                          select p).SingleOrDefault();

            return entity != null;
        }
    }
}
