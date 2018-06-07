using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.WebApplication.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order
{
    public partial class SeeMyOrders : SpecificCulturePage
    {
        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }
        }
        
        private void LoadGrid()
        {
            try
            {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;
                //Esto lo deberia de coger desde settings.settigns pero me daba error , CAMBIARLO LUEGO
                Type type = typeof(IOrderService);
                string assemblyQualifiedName = type.AssemblyQualifiedName;
                pbpDataSource.TypeName = assemblyQualifiedName;
                pbpDataSource.EnablePaging = true;
                pbpDataSource.SelectMethod = Settings.Default.ObjectDS_SeeMyOrders_SelectMethod;

                // Añadimos el id de usuario
                string usrId = SessionManager.GetUserSession(Context).UserProfileId.ToString();
                pbpDataSource.SelectParameters.Add("usrId", DbType.Int32, usrId);
                pbpDataSource.SelectCountMethod = Settings.Default.ObjectDS_SeeMyOrders_CountMethod;
                pbpDataSource.StartRowIndexParameterName = Settings.Default.ObjectDS_SeeMyOrders_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName = Settings.Default.ObjectDS_SeeMyOrders_CountParameter;
                gvOrders.AllowPaging = true;
                gvOrders.PageSize = Settings.Default.Orders_defaultCount;
                gvOrders.DataSource = pbpDataSource;
                gvOrders.DataBind();

            }
            catch (TargetInvocationException)
            {

            }
        }

        protected void gvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrders.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IOrderService orderService = (IOrderService)iocManager.Resolve<IOrderService>();

            e.ObjectInstance = (IOrderService)orderService;
        }
    }
}