using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.OrderService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.WebApplication.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order
{
    public partial class ManageOrder : SpecificCulturePage
    {
        ObjectDataSource pbpDataSource = new ObjectDataSource();
        static Boolean paymentMethod = false;
        static long cardId;
        static string cardType;
        static string cardNumber;
        static string expirateTime;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblError.Visible = false;
                ChangeDefault();
                gvProductsToPay.DataSource = SessionManager.shoppingCart;
                gvProductsToPay.DataBind();
                LoadGrid();
                txtPrizeTotal.Text = SessionManager.GetTotalPrize().ToString();
            }
        }

        private void ChangeDefault()
        {
            if (paymentMethod == false)
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
                CardDetails card = cardService.GetUserDefaultCard(SessionManager.GetUserSession(Context).UserProfileId);
                if (card != null)
                {
                    cardId = card.CardId;
                    cardNumber = card.CardNumber;
                    cardType = card.CardType;
                    expirateTime = card.ExpirateTime.ToString();
                    txtId.Text = cardId.ToString();
                    txtCardNumber.Text = cardNumber.ToString();
                    txtType.Text = cardType;
                    txtExpirationTime.Text = expirateTime;
                }
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

        protected void btnToPay_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IOrderService orderService = (IOrderService)iocManager.Resolve<IOrderService>();
            long usrId = SessionManager.GetUserSession(Context).UserProfileId;
            List<ProductDetails> products = SessionManager.shoppingCart;
            long cardId = (long)Convert.ToInt32(txtId.Text);
            int postalAddress = Convert.ToInt32(txtPostalAddress.Text);
            try
            {
                orderService.GenerateOrder(usrId, cardId, postalAddress, products);
                SessionManager.shoppingCart.Clear();
                paymentMethod = false;
                //Page.ClientScript.RegisterStartupScript(this.GetType(),"Script","<script>alert('This is a alert.');</script>");
                //Response.Redirect(Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));
                string message = GetLocalResourceObject("message.Text").ToString();
                Response.Write("<script language=javascript>alert('"+message+"'); location.href='/Pages/MainPage.aspx';</script>");
            }
            catch (InsuficientNumberOfUnitsException w)
            {
                lblError.Visible = true;
                lblError.Text = w.Message.ToString();
            }
            
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

        protected void cbForGift_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            GridViewRow row = checkBox.NamingContainer as GridViewRow;
            long productId = (long)Convert.ToInt32(row.Cells[3].Text);
            ProductDetails productDetails = SessionManager.GetProductFromCart(productId);
            productDetails.forGift = checkBox.Checked;
        }

        protected void gvProductsToPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var dd = e.Row.Cells[5].FindControl("listaCantidades") as DropDownList;
            if (dd != null)
            {
                //Si el numero original del carrito es menor que 10 (que es lo maximo que permite el combo box) se inicializa a ese numero, sino se pone a 1
                if (Convert.ToInt32(e.Row.Cells[2].Text) <= 10)
                {
                    dd.SelectedValue = e.Row.Cells[2].Text;
                }
                else
                {
                    dd.SelectedValue = "1";
                }
            }

            var dd2 = e.Row.Cells[4].FindControl("cbForGift") as CheckBox;
            if (dd2 != null)
            {
                long productId = Convert.ToInt32(e.Row.Cells[3].Text);
                ProductDetails productDetails = SessionManager.GetProductFromCart(productId);
                dd2.Checked = productDetails.forGift;
            }
        }

        protected void selectPayMent_CheckedChanged(object sender, EventArgs e)
        {
            paymentMethod = true;
            CheckBox checkBox = sender as CheckBox;
            GridViewRow row = checkBox.NamingContainer as GridViewRow;
            cardId = Convert.ToInt32(row.Cells[3].Text);
            cardNumber = row.Cells[0].Text; 
            cardType = row.Cells[1].Text;
            expirateTime = row.Cells[2].Text;
            txtId.Text = row.Cells[3].Text;
            txtCardNumber.Text = row.Cells[0].Text;
            txtExpirationTime.Text = row.Cells[2].Text;
            txtType.Text = row.Cells[1].Text;
            for(int i = 0; i< gvAllCards.Rows.Count; i++)
            {
                if (gvAllCards.Rows[i] != row)
                {
                    var chB = gvAllCards.Rows[i].Cells[5].FindControl("selectPayMent") as CheckBox;
                    chB.Checked = false;
                }

            }
        }

        private void LoadGrid()
        {
            try
            {
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;
                //Esto lo deberia de coger desde settings.settigns pero me daba error , CAMBIARLO LUEGO
                Type type = typeof(ICardService);
                string assemblyQualifiedName = type.AssemblyQualifiedName;
                pbpDataSource.TypeName = assemblyQualifiedName;
                pbpDataSource.EnablePaging = true;
                pbpDataSource.SelectMethod = Settings.Default.ObjectDS_SeeMyCards_SelectMethod;

                // Añadimos el id de usuario
                string usrId = SessionManager.GetUserSession(Context).UserProfileId.ToString();
                pbpDataSource.SelectParameters.Add("userProfileId", DbType.Int32, usrId);
                pbpDataSource.SelectCountMethod = Settings.Default.ObjectDS_SeeMyCards_CountMethod;
                pbpDataSource.StartRowIndexParameterName = Settings.Default.ObjectDS_SeeMyCards_StartIndexParamete;
                pbpDataSource.MaximumRowsParameterName = Settings.Default.ObjectDS_SeeMyCards_CountParameter;
                gvAllCards.AllowPaging = true;
                gvAllCards.PageSize = Settings.Default.AmazonMarket_defaultCount;
                gvAllCards.DataSource = pbpDataSource;
                gvAllCards.Columns[3].Visible = true;
                gvAllCards.Columns[4].Visible = true;
                gvAllCards.DataBind();
                gvAllCards.Columns[3].Visible = false;
                gvAllCards.Columns[4].Visible = false;

            }
            catch (TargetInvocationException)
            {

            }
        }

        protected void gvAllCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllCards.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();

            e.ObjectInstance = (ICardService)cardService;
        }

        protected void selectPayMent_DataBinding(object sender, EventArgs e)
        {
            CheckBox chB = sender as CheckBox;
            GridViewRow gvRow = chB.NamingContainer as GridViewRow;
            if (chB != null && gvRow.Cells[4].Text.Equals("True") && paymentMethod==false)
            {
                chB.Checked = true;
                txtCardNumber.Text = gvRow.Cells[0].Text;
                txtType.Text = gvRow.Cells[1].Text;
                txtExpirationTime.Text = gvRow.Cells[2].Text;
                txtId.Text = gvRow.Cells[3].Text;
            } else if(paymentMethod == true && cardId == (Convert.ToInt32(gvRow.Cells[3].Text)))
            {
                chB.Checked = true;
                txtCardNumber.Text = cardNumber;
                txtType.Text = cardType;
                txtExpirationTime.Text = expirateTime;
                txtId.Text = cardId.ToString();
            }
        }
    }
}