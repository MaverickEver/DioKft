using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Entities
{
    public class ProductDocument : EntityBase
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public string FileUrl { get; set; }
    }
}
