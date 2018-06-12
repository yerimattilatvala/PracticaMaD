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
    public partial class CDDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.ProductDao.ProductDetails product = SessionManager.ProductService.FindProduct(productId);
            if (product is Model.ProductDao.CDDetails)
            {
                Model.ProductDao.CDDetails cd = product as Model.ProductDao.CDDetails;
                SetCDInfo(cd);
            }
        }

        private void SetCDInfo(Model.ProductDao.CDDetails cd)
        {

            lclNameValue.Text = cd.name;
            lclCategoryValue.Text = cd.category;
            lclTitleValue.Text = cd.title;
            lclArtistValue.Text = cd.artist;
            lclTopicValue.Text = cd.topic;
            lclSongsValue.Text = cd.songs.ToString();
        }
    }
}