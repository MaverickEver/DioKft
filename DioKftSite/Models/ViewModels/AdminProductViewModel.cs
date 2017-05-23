using Microsoft.Web.Mvc;
using System.Web;

namespace MS.WebSolutions.DioKft.Models.ViewModels
{
    public class AdminProductViewModel
    {
        [FileExtensions(Extensions = "csv")]
        public HttpPostedFileBase ProductCsv { get; set; }
    }
}