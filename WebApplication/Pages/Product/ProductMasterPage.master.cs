using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class ProductMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTagGrid();
                LoadTags();
                listaCantidades.SelectedValue = "1";
                lblTagError.Visible = false;
            }
        }

        protected void BtnAddToCartClick(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Int32 cant = Convert.ToInt32(listaCantidades.SelectedValue);
            SessionManager.AddToShoppingCart(productId, cant);
            Response.Redirect(Request.RawUrl.ToString());

        }

        protected void listaCantidades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadTags()
        {
            List<TagDetails> tags = SessionManager.GetAllTags();
            TagDetails tag1 = new TagDetails(-1, "Select a tag", 0);
            tags.Add(tag1);
            tagList.DataSource = tags;
            tagList.DataTextField = "name";
            tagList.DataValueField = "tagId";
            tagList.DataBind();
            tagList.SelectedValue = "-1";
        }

        protected void tagList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Int32 tagId  = Convert.ToInt32(tagList.SelectedValue);
            SessionManager.TagService.TagProduct(productId,tagId);
            LoadTagGrid();
        }

        protected void btnCreateTag_Click(object sender, EventArgs e)
        {
            if (txtTag.Text.ToString().Equals(""))
                lblTagError.Visible = true;
            else
            {
                Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
                TagDetails newTag = new TagDetails(txtTag.Text);
                long tagId = SessionManager.TagService.AddNewTag(newTag);
                SessionManager.TagService.TagProduct(productId,tagId);
                LoadTagGrid();
                LoadTags();
            }
        }

        private void LoadTagGrid()
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            gvTags.DataSource = SessionManager.TagService.GetTagsByProduct(productId);
            gvTags.DataBind();
        }

        protected void gvTags_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}