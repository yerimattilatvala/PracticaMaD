using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.User
{
    public partial class ChangePassword : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOldPasswordError.Visible = false;

        }

        protected void BtnChangePasswordClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SessionManager.ChangePassword(Context, txtOldPassword.Text,
                        txtNewPassword.Text);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));

                }
                catch (IncorrectPasswordException)
                {
                    lblOldPasswordError.Visible = true;
                }
            }
        }
    }
}