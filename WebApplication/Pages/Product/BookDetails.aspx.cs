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
    public partial class BookDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Model.Product product = SessionManager.ProductService.FindProduct(productId);
            if (product is Book)
            {
                Book book = product as Book;
                SetBookInfo(book);
            }
        }

        private void SetBookInfo(Book book)
        {

            lclNameValue.Text = book.name;
            Category category = SessionManager.CategoryService.GetCategory(book.categoryId);
            lclCategoryValue.Text = category.name;
            lclTitleValue.Text = book.title;
            lclAuthorValue.Text = book.author;
            lclSummaryValue.Text = book.summary;
            lclTopicValue.Text = book.topic;
            lclPagesValue.Text = book.pages.ToString();
        }
    }
}