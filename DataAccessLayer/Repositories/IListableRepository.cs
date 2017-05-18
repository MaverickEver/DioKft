using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Repositories
{
    public interface IListableRepository
    {
        IEnumerable<Contact> ListAll();
    }
}
