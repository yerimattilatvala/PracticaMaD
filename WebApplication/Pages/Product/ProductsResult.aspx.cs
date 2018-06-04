using System;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.Properties;
using System.Data;
using System.Reflection;

using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System.Security.Policy;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class ProductsResult : SpecificCulturePage
    {
        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;
                //Esto lo deberia de coger desde settings.settigns pero me daba error , CAMBIARLO LUEGO
                Type type = typeof(IProductService);
                string assemblyQualifiedName = type.AssemblyQualifiedName;
                pbpDataSource.TypeName = assemblyQualifiedName;


                pbpDataSource.EnablePaging = true;
                pbpDataSource.SelectMethod = Settings.Default.ObjectDS_ProductsResult_SelectMethod;

                //Cogemos los keywords
                String keywords = Request.Params.Get("keywords");
                Int32 category = Convert.ToInt32(Request.Params.Get("category"));

                //Añadimos el parametro keywords
                pbpDataSource.SelectParameters.Add("keywords", DbType.String, keywords);
                //Depende de si la categoria es all (-1) o una definida en la bd.
                 if (category !=-1)
                 {
                    pbpDataSource.SelectParameters.Add("categoryId", DbType.Int32,category.ToString());
                 }

                pbpDataSource.SelectCountMethod = Settings.Default.ObjectDS_ProductsResult_CountMethod;
                pbpDataSource.StartRowIndexParameterName = Settings.Default.ObjectDS_ProductsResult_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName = Settings.Default.ObjectDS_AccountOperations_CountParameter;
                gvProductsResult.AllowPaging = true;
                gvProductsResult.PageSize = Settings.Default.AmazonMarket_defaultCount;
                gvProductsResult.DataSource = pbpDataSource;
                //Antes de hacer el databind hay que poner la columna de id a visible para luego poder acceder a ella
                gvProductsResult.Columns[4].Visible = true;
                gvProductsResult.DataBind();
                //Luego ya se pone a false.
                gvProductsResult.Columns[4].Visible = false;


            }
            catch (TargetInvocationException)
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
            long idProduct = Convert.ToInt32(row.Cells[4].Text);
            //Si seleccionamos desde la pagina de resultados siempre añadimos de 1 en 1
            int numberOfElements = 1;
            SessionManager.AddToShoppingCart(idProduct, numberOfElements);
            //Para actualizar el texto del carrito. Creo que se puede acceder al elemento de la Master Page pero no se como, asiq recarga la pagina.
            Response.Redirect(Request.RawUrl.ToString());
        }
    }
}