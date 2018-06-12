using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class BookDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.ProductDao.ProductDetails product = SessionManager.ProductService.FindProduct(productId);
            if (product is Model.ProductDao.BookDetails)
            {
                Model.ProductDao.BookDetails book = product as Model.ProductDao.BookDetails;
                SetBookInfo(book);
            }
        }

        private void SetBookInfo(Model.ProductDao.BookDetails book)
        {

            lclNameValue.Text = book.name;
            lclCategoryValue.Text = book.category;
            lclTitleValue.Text = book.title;
            lclAuthorValue.Text = book.author;
            lclSummaryValue.Text = book.summary;
            lclTopicValue.Text = book.topic;
            lclPagesValue.Text = book.pages.ToString();
        }
    }
}