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
                gvProductsInCard.DataSource = SessionManager.shoppingCart;
                gvProductsInCard.DataBind();
            }
        }
        protected void BtnPayClick(object sender, EventArgs e) {
        }
        protected void gvProductsInCard_changing(object sender, GridViewSelectEventArgs e)
        {

            GridViewRow row = gvProductsInCard.Rows[e.NewSelectedIndex];

            // You can cancel the select operation by using the Cancel
            // property. For this example, if the user selects a customer with 
            // the ID "ANATR", the select operation is canceled and an error message
            // is displayed.
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            //Si seleccionamos desde la pagina de resultados siempre añadimos de 1 en 1
            SessionManager.RemoveProductOfShoppingCart(idProduct);     
            /*Label lb = Master.FindControl("lnkCart.Text") as Label;
            lb.Text = lb.Text + "(" + SessionManager.GetNumberOfItemsShoppingCart() + ")";*/
            gvProductsInCard.DataSource = SessionManager.shoppingCart;
            gvProductsInCard.DataBind();
        }

        protected void listaCantidades_SelectedIndexChanged(object sender, EventArgs e){
            DropDownList drop = sender as DropDownList;
            int units = Convert.ToInt32(drop.SelectedItem.Text);
            GridViewRow row = drop.NamingContainer as GridViewRow;
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            SessionManager.IncrementProductUnits(idProduct, units);
            Label lb = (Label)Master.FindControl("lnkCart");
            MessageLabel.Text = "Cart" + "(" + SessionManager.GetNumberOfItemsShoppingCart() + ")";
            lb.Text = "Cart" + "(" + SessionManager.GetNumberOfItemsShoppingCart() + ")";
            gvProductsInCard.DataSource = SessionManager.shoppingCart;
            gvProductsInCard.DataBind(); 
        }

        protected void gvProductsInCard_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
}