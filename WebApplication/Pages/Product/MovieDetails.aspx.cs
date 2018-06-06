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
    public partial class MovieDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.Product product = SessionManager.ProductService.FindProduct(productId);
            if ( product is Movie)
            {
                Movie mov = product as Movie;
                SetMovieInfo(mov);
            }
        }

        private void SetMovieInfo(Movie mov)
        {

            lclNameValue.Text =  mov.name;
            Category category = SessionManager.CategoryService.GetCategory(mov.categoryId);
            lclCategoryValue.Text =category.name;
            lclTitleValue.Text = mov.title;
            lclDirectorValue.Text = mov.director;
            lclSummaryValue.Text = mov.summary;
            lclTopicValue.Text = mov.topic;
            lclDurationValue.Text = mov.duration.ToString();
        }
    }
}