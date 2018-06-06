using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class CDDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.Product product = SessionManager.ProductService.FindProduct(productId);
            if (product is CD)
            {
                CD cd = product as CD;
                SetCDInfo(cd);
            }
        }

        private void SetCDInfo(CD cd)
        {

            lclNameValue.Text = cd.name;
            Category category = SessionManager.CategoryService.GetCategory(cd.categoryId);
            lclCategoryValue.Text = category.name;
            lclTitleValue.Text = cd.title;
            lclArtistValue.Text = cd.artist;
            lclTopicValue.Text = cd.topic;
            lclSongsValue.Text = cd.songs.ToString();
        }
    }
}