using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order
{
    public partial class ManageOrder : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvProductsToPay.DataSource = SessionManager.shoppingCart;
            gvProductsToPay.DataBind();
        }

        protected void gvProductsToPay_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }

        protected void gvProductsToPay_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvProductsToPay.Rows[e.NewSelectedIndex];
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            SessionManager.RemoveProductOfShoppingCart(idProduct);
            gvProductsToPay.DataSource = SessionManager.shoppingCart;
            gvProductsToPay.DataBind();
        }

        protected void listaCantidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drop = sender as DropDownList;
            int units = Convert.ToInt32(drop.SelectedItem.Text);
            GridViewRow row = drop.NamingContainer as GridViewRow;
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            SessionManager.IncrementProductUnits(idProduct, units);
            gvProductsToPay.DataSource = SessionManager.shoppingCart;
            gvProductsToPay.DataBind();
        }
    }
}