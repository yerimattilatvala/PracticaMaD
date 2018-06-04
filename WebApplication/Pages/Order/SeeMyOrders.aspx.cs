using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order
{
    public partial class SeeMyOrders : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IOrderService orderService = (IOrderService)iocManager.Resolve<IOrderService>();
            gvOrders.DataSource = orderService.ViewOrdersByUser(SessionManager.GetUserSession(Context).UserProfileId);
            gvOrders.DataBind();
        }
        
        /*protected void gvOrders_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            String orderId = gvOrders.Rows[e.NewSelectedIndex].Cells[5].Text;
            String url = String.Format("OrderDetails.aspx?orderId={0}",orderId);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }*/
    }
}