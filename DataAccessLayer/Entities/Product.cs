using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.WebSolutions.DioKft.DataAccessLayer.Entities
{
    public class Product : EntityBase
    {
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Unit")]
        public int UnitId { get; set; }

        [InverseProperty("ProductId")]
        public List<ProductDocument> ProductDocuments { get; set; }

    }
}