using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order
{
    public partial class OrderDetail : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = (IProductService)iocManager.Resolve<IProductService>();
            IOrderService orderService = (IOrderService)iocManager.Resolve<IOrderService>();

            //Cogemos los keywords
            long orderId = (long)Convert.ToInt32(Request.Params.Get("orderId"));
            List<ProductDetails> products = productService.GetOrderLineProductsByOrderId(orderId);
            double prize = 0;
            for (int i = 0; i < products.Count; i++)
            {
                prize += products.ElementAt(i).numberOfUnits * products.ElementAt(i).prize;
            }
            gvOrderDetails.DataSource = products;
            gvOrderDetails.DataBind();
            txtPrize.Text = prize.ToString();
            OrderDetails order = orderService.FindOrder(orderId);
            txtCard.Text = order.CardNumber;
            txtPostalAddress.Text = order.PostalAddress.ToString();
        }

        protected void gvOrderDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
        }


        protected void gvOrderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewProduct")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOrderDetails.Rows[index];
                long productId = Convert.ToInt32(row.Cells[4].Text);
                Model.ProductDao.ProductDetails product = SessionManager.ProductService.FindProduct(productId);
                //Que pasa si se borra el producto de bd??
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
                    url = String.Format("~/Pages/Product/ProductDetails.aspx?productId={0}", productId);
                }
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}