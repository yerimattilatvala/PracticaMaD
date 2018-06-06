using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Web.UI.HtmlControls;

#pragma warning disable CS0116 // A namespace cannot directly contain members such as fields or methods
namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
#pragma warning restore CS0116 // A namespace cannot directly contain members such as fields or methods
{
    public partial class ProductDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.Product product = SessionManager.ProductService.FindProduct(productId);
            SetProductInfo(product);
        }
        private void SetProductInfo(Model.Product prod)
        {
            lclNameValue.Text = prod.name;
            Category category = SessionManager.CategoryService.GetCategory(prod.categoryId);
            lclCategoryValue.Text = category.name;
            
        }
    }


}