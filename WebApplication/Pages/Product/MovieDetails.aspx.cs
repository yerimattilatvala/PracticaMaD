using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class MovieDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.ProductDao.ProductDetails product = SessionManager.ProductService.FindProduct(productId);
            if ( product is Model.ProductDao.MovieDetails)
            {
                Model.ProductDao.MovieDetails mov = product as Model.ProductDao.MovieDetails;
                SetMovieInfo(mov);
            }
        }

        private void SetMovieInfo(Model.ProductDao.MovieDetails mov)
        {
            lclNameValue.Text =  mov.name;
            lclCategoryValue.Text = mov.category;
            lclTitleValue.Text = mov.title;
            lclDirectorValue.Text = mov.director;
            lclSummaryValue.Text = mov.summary;
            lclTopicValue.Text = mov.topic;
            lclDurationValue.Text = mov.duration.ToString();
        }
    }
}