using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderLineService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
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
            IOrderLineService orderLineService = (IOrderLineService)iocManager.Resolve<IOrderLineService>();
            IOrderService orderService = (IOrderService)iocManager.Resolve<IOrderService>();

            //Cogemos los keywords
            long orderId = (long)Convert.ToInt32(Request.Params.Get("orderId"));
            List<ProductDetails> products = orderLineService.GetOrderLineProductsByOrderId(orderId);
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
    }
}