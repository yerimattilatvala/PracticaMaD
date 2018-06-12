using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using System.Drawing;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product
{
    public partial class ProductMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeProductTags();

            if (!IsPostBack)
            {
                Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
                if (SessionManager.ProductService.FindProduct(productId).numberOfUnits == 0)
                {
                    btnAddToCart.Visible = false;
                    lblNoUnits.Visible = true;
                }
                LoadTags();
                LoadUntag();
                listaCantidades.SelectedValue = "1";
                lblTagError.Visible = false;
            }
        }

        protected void BtnAddToCartClick(object sender, EventArgs e)
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            Int32 cant = Convert.ToInt32(listaCantidades.SelectedValue);
            SessionManager.AddToShoppingCart(productId, cant);
            string message = GetLocalResourceObject("message.Text").ToString();
            Response.Write("<script language=javascript>alert('" + message + "'); location.href='/Pages/MainPage.aspx';</script>");
            //Response.Redirect(Request.RawUrl.ToString());

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
            if (SessionManager.IsUserAuthenticated(Context))
            {
                Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
                Int32 tagId = Convert.ToInt32(tagList.SelectedValue);
                if (tagId != -1)
                {
                    SessionManager.TagService.TagProduct(productId, tagId);
                    Response.Redirect(Request.RawUrl.ToString());
                }
            }
            else
            {
                lblAuntenticated.Visible = true;
            }
        }

        protected void btnCreateTag_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                if (txtTag.Text.ToString().Equals(""))
                    lblTagError.Visible = true;
                else
                {
                    Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
                    TagDetails newTag = new TagDetails(txtTag.Text);
                    try
                    {
                        lblTagError.Visible = false;
                        long tagId = SessionManager.TagService.AddNewTag(newTag);
                        SessionManager.TagService.TagProduct(productId, tagId);
                        InitializeProductTags();
                        txtTag.Text = null;
                        Response.Redirect(Request.RawUrl.ToString());
                    }
                    catch (DuplicateInstanceException)
                    {
                        lblTagError.Visible = true;
                        lblTagError.Text = GetLocalResourceObject("TagExists").ToString();
                    }
                } 
            }
            else
            {
                lblAuntenticated.Visible = true;
            }
        }

        private void InitializeProductTags()
        {
            tagPanel.Controls.Clear();
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            List<TagDetails> tagDetails = SessionManager.TagService.GetTagsByProduct(productId);

            foreach (TagDetails tag in tagDetails)
            {
                HyperLink tagLink = new HyperLink();
                if (IsPopular(tag) == 1)
                {
                    tagLink.Font.Size = 16;
                } else if (IsPopular(tag) == 2)
                {
                    tagLink.Font.Size = 10;
                } else
                    tagLink.Font.Size = 6;
                tagLink.Text = tag.name + " ";
                string url = String.Format("~/Pages/Product/ProductsByTag.aspx?tagId={0}",tag.tagId);
                tagLink.NavigateUrl = url;
                tagPanel.Controls.Add(tagLink);
            }
        }

        protected void gvTags_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }

        private int IsPopular(TagDetails tagD){
            int pos = -1;

            List<TagDetails> tagDetails = SessionManager.TagService.GetMostPopularTags(8);
            int count = 0;

            foreach(TagDetails tag in tagDetails)
            {
                if (tag.tagId == tagD.tagId && count < 3)
                {
                    pos = 1;
                    break;
                }
                else if (tag.tagId == tagD.tagId && count > 2)
                {
                    pos = 2;
                    break;
                }
                count++;
            }

            return pos;
        }

        private void LoadUntag()
        {
            Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
            List<TagDetails> tags = SessionManager.TagService.GetTagsByProduct(productId);
            TagDetails tag1 = new TagDetails(-1, "Select a tag", 0);
            tags.Add(tag1);
            untagList.DataSource = tags;
            untagList.DataTextField = "name";
            untagList.DataValueField = "tagId";
            untagList.DataBind();
            untagList.SelectedValue = "-1";
        }

        protected void untagList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                try
                {
                    Int32 productId = Convert.ToInt32(Request.Params.Get("productId"));
                    long tagId = Convert.ToInt32(untagList.SelectedValue);
                    SessionManager.TagService.UntagProduct(productId, tagId);
                    Response.Redirect(Request.RawUrl.ToString());
                }
                catch (InstanceNotFoundException)
                {

                }
            }else
            {
                lblAuntenticated.Visible = true;
            }
        }
    }
}