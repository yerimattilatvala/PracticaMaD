﻿using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
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
            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lnlkAddCard != null)
                    lnlkAddCard.Visible = false;
                if (lnkCards != null)
                    lnkCards.Visible = false;
                if (lnlOrders != null)
                    lnlOrders.Visible = false;
            }
            else
            {
                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
                if (lnlkAddCard != null)
                    lnlkAddCard.Visible = true;
                if (lnkCards != null)
                    lnkCards.Visible = true;
                if (lnlOrders != null)
                    lnlOrders.Visible = true;
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
    }
}