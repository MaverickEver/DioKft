using System.Collections.Generic;

namespace MS.WebSolutions.DioKft.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.Products = new List<ProductModel>();
            this.Categories = new List<CategoryModel>() { new CategoryModel { Id = -1, Name = "------------" } };
            this.SelectedCategoryId = -1;
        }

        public IList<ProductModel> Products { get; set; }
        public IList<CategoryModel> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}