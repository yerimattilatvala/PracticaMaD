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
using Es.Udc.DotNet.PracticaMaD.Model;




namespace Es.Udc.DotNet.PracticaMaD.WebApplication
{
    public partial class MainPage : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateComboLanguage();
            }

        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            String keywords = this.txtKeywords.Text;
            String category = this.comboCategory.SelectedValue.ToString();
            String url = String.Format("./Product/ProductsResult.aspx?keywords={0}&category={1}", keywords,category);
            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void ComboCategorySelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdateComboLanguage()
        {
            List<Category> categories = SessionManager.GetAllCategories();
            //Hay que añadir una categoria 'virtual' para coger todo
            Category allCat = new Category();
            allCat.categoryId = -1;
            allCat.name = "All";
            categories.Add(allCat);
            this.comboCategory.DataSource = categories;
            this.comboCategory.DataTextField = "name";
            this.comboCategory.DataValueField = "categoryId";
            this.comboCategory.DataBind();
            this.comboCategory.SelectedValue = "-1";
        }
    }






}