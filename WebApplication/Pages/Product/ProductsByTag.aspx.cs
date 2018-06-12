using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
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

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class ProductsByTag : SpecificCulturePage
    {
        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblAutenticated.Visible = false;
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ITagService tagService = (ITagService)iocManager.Resolve<ITagService>();
                long tagId = Convert.ToInt32(Request.Params.Get("tagId"));
                lclTagName.Text = tagService.FinTagById(tagId).name;
                LoadGrid();
            }
        }

        private void LoadGrid()
        {

            try
            {
                lblNoUnits.Visible = false;
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;
                //Esto lo deberia de coger desde settings.settigns pero me daba error , CAMBIARLO LUEGO
                Type type = typeof(IProductService);
                string assemblyQualifiedName = type.AssemblyQualifiedName;
                pbpDataSource.TypeName = assemblyQualifiedName;


                pbpDataSource.EnablePaging = true;
                pbpDataSource.SelectMethod = Settings.Default.ObjectDS_ProductsByTag_SelectMethod;

                //Cogemos los keywords
                long tagId = Convert.ToInt32(Request.Params.Get("tagId"));
                pbpDataSource.SelectParameters.Add("tagId", DbType.Int32, tagId.ToString());

                pbpDataSource.SelectCountMethod = Settings.Default.ObjectDS_ProductsByTag_CountMethod;
                pbpDataSource.StartRowIndexParameterName = Settings.Default.ObjectDS_ProductsByTag_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName = Settings.Default.ObjectDS_ProductsByTag_CountParameter;
                gvProductsTag.AllowPaging = true;
                gvProductsTag.PageSize = Settings.Default.Tags_defaultCount;
                gvProductsTag.DataSource = pbpDataSource;
                //Antes de hacer el databind hay que poner la columna de id a visible para luego poder acceder a ella
                gvProductsTag.Columns[4].Visible = true;
                gvProductsTag.Columns[3].Visible = true;
                gvProductsTag.DataBind();
                //Luego ya se pone a false.
                gvProductsTag.Columns[4].Visible = false;
                gvProductsTag.Columns[3].Visible = false;
            }
            catch (TargetInvocationException)
            {

            }
        }

        protected void gvProductsTag_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductsTag.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void gvProductsTag_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvProductsTag.Rows[e.NewSelectedIndex];
            long idProduct = Convert.ToInt32(row.Cells[4].Text);
            int numberOfElements = 1;
            int units = Convert.ToInt32(row.Cells[3].Text);
            string name = row.Cells[0].Text;
            if (units == 0)
            {
                lblNoUnits.Visible = true;
            }
            else
            {
                lblNoUnits.Visible = false;
                SessionManager.AddToShoppingCart(idProduct, numberOfElements);
                Response.Redirect(Request.RawUrl.ToString());
            }
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = (IProductService)iocManager.Resolve<IProductService>();

            e.ObjectInstance = (IProductService)productService;
        }

        protected void gvProductsTag_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewProduct")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvProductsTag.Rows[index];
                long productId = Convert.ToInt32(row.Cells[4].Text);
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

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            long tagId = Convert.ToInt32(Request.Params.Get("tagId"));
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ITagDao tagDao = (ITagDao)iocManager.Resolve<ITagDao>();
            if (SessionManager.IsUserAuthenticated(Context))
            { 
                tagDao.Remove(tagId);
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));
            }
            else
            {
                lblAutenticated.Visible = true;
            }
        }
    }
}