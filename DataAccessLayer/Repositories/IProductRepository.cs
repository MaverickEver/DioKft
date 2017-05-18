using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Repositories
{
    public interface IProductRepository : IRepository<Product, int>, IListableRepository
    {
        IEnumerable<Product> Find(int categoryId);        
    }
}
