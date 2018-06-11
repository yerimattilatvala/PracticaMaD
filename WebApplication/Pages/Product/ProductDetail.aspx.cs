using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Web.UI.HtmlControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class ProductDetail: SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.ProductDao.ProductDetails product = SessionManager.ProductService.FindProduct(productId);
            SetProductInfo(product);
        }
        private void SetProductInfo(Model.ProductDao.ProductDetails prod)
        {
            lclNameValue.Text = prod.name;
            lclCategoryValue.Text = prod.category; 
        }
    }


}