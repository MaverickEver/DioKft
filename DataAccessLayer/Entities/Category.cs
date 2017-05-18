using System.ComponentModel.DataAnnotations.Schema;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Entities
{
    public class Category : EntityBase
    {
        [ForeignKey("Category")]
        public int ParentId { get; set; }
    }
}
