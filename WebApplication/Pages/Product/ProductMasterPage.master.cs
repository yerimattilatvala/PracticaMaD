using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class ProductMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaCantidades.SelectedValue = "1";
            }
        }

        protected void BtnAddToCartClick(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Int32 cant = Convert.ToInt32(listaCantidades.SelectedValue);
            SessionManager.AddToShoppingCart(productId, cant);
            Response.Redirect(Request.RawUrl.ToString());

        }

        protected void listaCantidades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}