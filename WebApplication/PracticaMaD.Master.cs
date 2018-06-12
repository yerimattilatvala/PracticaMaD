using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeCloudTags();
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                if (lblDash1 != null)
                    lblDash1.Visible = true;
                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lnkMyAccount != null)
                    lnkMyAccount.Visible = false;
            }
            else
            {
                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = true;
                if (lblDash2 != null)
                    lblDash2.Visible = true;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
                if (lnkMyAccount != null)
                    lnkMyAccount.Visible = true;
            }
            if (SessionManager.shoppingCart == null)
            {
                if(lnkCart != null)
                {
                    lnkCart.Visible = false;
                }
            }
            else
            {
                if (lnkCart != null)
                {
                    lnkCart.Visible = true;
                    lnkCart.Text = GetLocalResourceObject("lnkCart.Text").ToString() + "(" + SessionManager.GetNumberOfItemsShoppingCart() + ")";
                }
            }
        }

        private void InitializeCloudTags()
        {
            tagPanel.Controls.Clear();
            List<TagDetails> tagDetails = SessionManager.TagService.GetAllTags();
            foreach (TagDetails tag in tagDetails)
            {
                HyperLink tagLink = new HyperLink();
                if (IsPopular(tag) == 1)
                {
                    tagLink.Font.Size = 16;
                }
                else if (IsPopular(tag) == 2)
                {
                    tagLink.Font.Size = 10;
                }
                else
                    tagLink.Font.Size = 6;
                tagLink.Text = tag.name + " ";
                string url = String.Format("/Pages/Product/ProductsByTag.aspx?tagId={0}", tag.tagId);
                tagLink.NavigateUrl = url;
                tagPanel.Controls.Add(tagLink);
            }
        }
        private int IsPopular(TagDetails tagD)
        {
            int pos = -1;

            List<TagDetails> tagDetails = SessionManager.TagService.GetMostPopularTags(8);
            int count = 0;

            foreach (TagDetails tag in tagDetails)
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
    }
}