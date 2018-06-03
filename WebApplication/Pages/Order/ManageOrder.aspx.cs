using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order
{
    public partial class ManageOrder : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvProductsToPay.DataSource = SessionManager.shoppingCart;
                gvProductsToPay.DataBind();
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
                List<CardDetails> cards = cardService.ViewCardsByUser(SessionManager.GetUserSession(Context).UserProfileId);
                gvAllCards.DataSource = cards;
                gvAllCards.DataBind();
                txtPostalAddress.Text = SessionManager.FindUserProfileDetails(Context).PostalAddress.ToString();
                for (int i = 0; i < cards.Count; i++)
                {
                    if (cards.ElementAt(i).DefaultCard == true)
                    {
                        txtCardNumber.Text = cards.ElementAt(i).CardNumber;
                        txtExpirationTime.Text = cards.ElementAt(i).ExpirateTime.ToString();
                        txtType.Text = cards.ElementAt(i).CardType;
                        txtId.Text = cards.ElementAt(i).CardId.ToString();
                        break;
                    }
                }
                txtPrizeTotal.Text = SessionManager.GetTotalPrize().ToString();
            }
        }

        protected void gvProductsToPay_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }

        protected void gvProductsToPay_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvProductsToPay.Rows[e.NewSelectedIndex];
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            SessionManager.RemoveProductOfShoppingCart(idProduct);
            gvProductsToPay.DataSource = SessionManager.shoppingCart;
            gvProductsToPay.DataBind();
            if (SessionManager.shoppingCart.Count <= 0)
                btnToPay.Visible = false;
            txtPrizeTotal.Text = SessionManager.GetTotalPrize().ToString();
        }

        protected void gvAllCards_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }

        protected void gvAllCards_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvAllCards.Rows[e.NewSelectedIndex];
            txtCardNumber.Text = row.Cells[0].Text;
            txtExpirationTime.Text = row.Cells[2].Text;
            txtType.Text = row.Cells[1].Text;
            txtId.Text = row.Cells[3].Text;
        }

        protected void btnToPay_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IOrderService orderService = (IOrderService)iocManager.Resolve<IOrderService>();
            long usrId = SessionManager.GetUserSession(Context).UserProfileId;
            List<ProductDetails> products = SessionManager.shoppingCart;
            long cardId = (long)Convert.ToInt32(txtId.Text);
            int postalAddress = Convert.ToInt32(txtPostalAddress.Text);
            orderService.GenerateOrder(usrId,cardId,postalAddress,products);
            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));
        }

        protected void listaCantidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drop = sender as DropDownList;
            int units = Convert.ToInt32(drop.SelectedItem.Text);
            GridViewRow row = drop.NamingContainer as GridViewRow;
            long idProduct = (long)Convert.ToInt32(row.Cells[3].Text);
            SessionManager.IncrementProductUnits(idProduct, units);
            gvProductsToPay.DataSource = SessionManager.shoppingCart;
            gvProductsToPay.DataBind();
            txtPrizeTotal.Text = SessionManager.GetTotalPrize().ToString();
        }
    }
}