using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
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
                /*if (!SessionManager.IsUserAuthenticated(Context))
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));*/
                //else
                //No habria que comprobar si está autenticado ya que eso se indica en el web.config
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Order/ManageOrder.aspx"));
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
            SessionManager.IncrementProductUnits(idProduct, units);
            Response.Redirect(Request.RawUrl.ToString());
        }

        protected void gvProductsInCard_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }

        protected void gvProductsInCard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var dd = e.Row.Cells[5].FindControl("listaCantidades") as DropDownList;
            if (dd != null)
            {
                //Si el numero original del carrito es menor que 10 (que es lo maximo que permite el combo box) se inicializa a ese numero, sino se pone a 1
                if (Convert.ToInt32(e.Row.Cells[2].Text) <= 10)
                {
                    dd.SelectedValue = e.Row.Cells[2].Text;
                }
                else
                {
                    dd.SelectedValue = "1";
                }
            }

            var dd2 = e.Row.Cells[4].FindControl("cbForGift") as CheckBox;
            if (dd2 != null)
            {
                long productId = Convert.ToInt32(e.Row.Cells[3].Text);
                ProductDetails productDetails = SessionManager.GetProductFromCart(productId);
                dd2.Checked = productDetails.forGift;
            }
        }

        protected void cbForGift_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            GridViewRow row = checkBox.NamingContainer as GridViewRow;
            long productId = (long)Convert.ToInt32(row.Cells[3].Text);
            ProductDetails productDetails = SessionManager.GetProductFromCart(productId);
            productDetails.forGift = checkBox.Checked;

        }

        protected void gvProductsInCard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewProduct")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvProductsInCard.Rows[index];
                long productId = Convert.ToInt32(row.Cells[3].Text);
                Model.ProductDao.ProductDetails product = SessionManager.ProductService.FindProduct(productId);
                String url;
                if (product is Model.ProductDao.MovieDetails)
                {
                    url = String.Format("~/Pages/Product/MovieDetails.aspx?productId={0}", productId);
                }
                else if (product is Model.ProductDao.BookDetails)
                {
                    url = String.Format("~/Pages/Product/BookDetails.aspx?productId={0}", productId);

                }
                else if (product is Model.ProductDao.CDDetails)
                {
                    url = String.Format("~/Pages/Product/CDDetails.aspx?productId={0}", productId);
                }
                else
                {
                    url = String.Format("~/Pages/Product/ProductDetail.aspx?productId={0}", productId);
                }
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}