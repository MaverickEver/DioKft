using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using MS.WebSolutions.DioKft.Helpers;
using MS.WebSolutions.DioKft.Models;
using MS.WebSolutions.DioKft.Models.ViewModels;
using MS.WebSolutions.DioKft.ProductImporter;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MS.WebSolutions.DioKft.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var model = new ProductViewModel();
            using (var repository = new ProductRepository())
            {
                this.FillModelWithCategories(model, repository);                
                this.FillModelWithProducts(model, repository);                
            }

            return View(model);
        }

        public ActionResult Admin()
        {
            var model = new AdminProductViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(int selectedCategoryId)
        {
            var model = new ProductViewModel()
            {
                SelectedCategoryId = selectedCategoryId
            };

            using (var repository = new ProductRepository())
            {
                this.FillModelWithCategories(model, repository);
                this.FillModelWithProducts(model, repository);                
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase productCsv)
        {
            try
            {
                var fileHandler = new FileHandler(Server);
                var importer = new Importer();

                this.ExecuteImport(productCsv, fileHandler, importer);                
            }
            catch (Exception ex)
            {
                return Json("Import has been failed. Exception: " + ex.Message);
            }

            return Json("Import has been finished.");
        }

        #region [BusinessLogic]
        private void ExecuteImport(HttpPostedFileBase file, FileHandler fileHandler, Importer importer)
        {
            importer.ClearDatabase();

            var savedFileUrl = fileHandler.Save(1, file, FileLocations.ImportFolder);

            var errors = importer.ExecuteImportLogic(Server.MapPath(savedFileUrl));

            fileHandler.Remove(savedFileUrl);

            if (errors.Trim().Length != 0)
            {
                Console.WriteLine($"Missed items: {errors.ToString()}");
            }
        }     

        private void FillModelWithProducts(ProductViewModel model, ProductRepository repository)
        {
            IEnumerable<Product> products;
            if (model.SelectedCategoryId == -1)
            {
                products = repository.ListAll();
            }
            else
            {
                products = repository.ListAllByCategory(model.SelectedCategoryId);
            }

            this.FilProductModel(model, products);
        }

        private void FilProductModel(ProductViewModel model, System.Collections.Generic.IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                var productModel = new ProductModel
                {
                    Name = product.Name,
                    Unit = product.Unit?.Name ?? String.Empty
                };

                model.Products.Add(productModel);
            }
        }

        private void FillModelWithCategories(ProductViewModel model, ProductRepository repository)
        {
            var categories = repository.ListAllCategories();

            foreach (var category in categories)
            {
                var categoryModel = new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Selected = category.Id == model.SelectedCategoryId                    
                };

                model.Categories.Add(categoryModel);
            }
        }
        #endregion
    }
}