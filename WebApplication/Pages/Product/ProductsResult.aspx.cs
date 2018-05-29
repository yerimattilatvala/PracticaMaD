using System;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.Properties;
using System.Data;
using System.Reflection;

using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class ProductsResult : SpecificCulturePage
    {
        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Si quitas el try da error siempre
            try {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;
                //Esto lo deberia de coger desde settings.settigns pero me daba error , CAMBIARLO LUEGO
                Type type = typeof(IProductService);
                string assemblyQualifiedName = type.AssemblyQualifiedName;
                pbpDataSource.TypeName = assemblyQualifiedName;


                pbpDataSource.EnablePaging = true;
                pbpDataSource.SelectMethod = Settings.Default.ObjectDS_ProductsResult_SelectMethod;

                //Cogemos los keywords
                String keywords = Request.Params.Get("keywords");

                //Cogemos la categoria. Se pasa el nombre o un int????
                //Int32 category = Convert.ToInt32(Request.Params.Get("category"));

                //Añadimos el parametro keywords
                pbpDataSource.SelectParameters.Add("keywords", DbType.String, keywords);
                //Depende de si pasamos categoria o no
                // if (category != null)
                // {
                //IMPORTANTE: en la base de datos está puesto como bigInt que es el equivalente a Int64
                //pero a lo largo del código se ha usado Int32. No debería dar problemas ya que no serán 
                //números muy grandes, pero cabe la posibilidad de overflow.
                //  pbpDataSource.SelectParameters.Add("category", DbType.Int64,category.ToString());
                // }
                pbpDataSource.SelectCountMethod = Settings.Default.ObjectDS_ProductsResult_CountMethod;
                pbpDataSource.StartRowIndexParameterName = Settings.Default.ObjectDS_ProductsResult_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName = Settings.Default.ObjectDS_AccountOperations_CountParameter;
                gvProductsResult.AllowPaging = true;
                gvProductsResult.PageSize = Settings.Default.AmazonMarket_defaultCount;
                gvProductsResult.DataSource = pbpDataSource;
                gvProductsResult.DataBind();
            } catch (TargetInvocationException)
            {

            }



        }

        protected void GvAccOperationsPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductsResult.PageIndex = e.NewPageIndex;
            gvProductsResult.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = (IProductService)iocManager.Resolve<IProductService>();

            e.ObjectInstance = (IProductService)productService;
        }

        protected void gvProductsResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
   
              GridViewRow row = gvProductsResult.Rows[e.NewSelectedIndex];

            // You can cancel the select operation by using the Cancel
            // property. For this example, if the user selects a customer with 
            // the ID "ANATR", the select operation is canceled and an error message
            // is displayed.
            MessageLabel.Text = "You selected " + row.Cells[0].Text + ".";

        }
    }
}