using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.ShoppingCart
{
    public partial class ShoppingCart : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (SessionManager.shoppingCart.Count ==0)
                    btnPay.Visible = false;
                gvProductsInCard.DataSource = SessionManager.shoppingCart;
                gvProductsInCard.DataBind();
            }
        }
        protected void BtnPayClick(object sender, EventArgs e) {
            if (Page.IsValid)
            {
                if (!SessionManager.IsUserAuthenticated(Context))
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                else
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Order/ManageOrder.aspx"));
            }
        }
        protected void gvProductsInCard_changing(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvProductsInCard.Rows[e.NewSelectedIndex];
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            SessionManager.RemoveProductOfShoppingCart(idProduct);
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void listaCantidades_SelectedIndexChanged(object sender, EventArgs e){
            DropDownList drop = sender as DropDownList;
            int units = Convert.ToInt32(drop.SelectedItem.Text);
            GridViewRow row = drop.NamingContainer as GridViewRow;
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            // forma chapuza de comprobar que detecta que é envolto para regalo
            SessionManager.WrappedProductForGift(idProduct);
            SessionManager.IncrementProductUnits(idProduct, units);
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void gvProductsInCard_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
}