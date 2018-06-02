using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Card
{
    public partial class AddCard : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddCard_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
                    string cardNumber = txtCardNumber.Text.ToString();
                    string cardType = txtType.Text.ToString();
                    int cv = Convert.ToInt16(txtCV.Text.ToString());
                    DateTime expirationTime = DateTime.Now;
                    CardDetails newCard = new CardDetails(cardNumber,cv,expirationTime,cardType);
                    cardService.AddCard(SessionManager.GetUserSession(Context).UserProfileId,newCard);
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Card/SeeMyCards.aspx"));

                }
                catch (DuplicateInstanceException)
                {
                    lblCardNumberError.Visible = true;
                }

            }
        }
    }
}