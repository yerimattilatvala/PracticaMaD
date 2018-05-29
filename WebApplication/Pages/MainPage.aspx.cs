using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.ProductService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.Properties;



namespace Es.Udc.DotNet.PracticaMaD.WebApplication
{
    public partial class MainPage : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            String keywords = this.txtKeywords.Text;
            String url = String.Format("./Product/ProductsResult.aspx?keywords={0}", keywords);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }
    }






}