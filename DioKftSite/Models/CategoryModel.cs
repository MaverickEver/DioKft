using System.Web.Mvc;

namespace MS.WebSolutions.DioKft.Models
{
    public class CategoryModel : SelectListItem
    {        
        public int Id
        {
            get { return int.Parse(this.Value); }
            set { this.Value = value.ToString(); }
        }
        public string Name
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
    }
}