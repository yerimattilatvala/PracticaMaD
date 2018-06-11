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
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Card
{
    public partial class AddCard : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                for (int i = 1; i < 13; i++)
                {
                    dropMonth.Items.Add(i.ToString());
                }
                int currentDate = DateTime.Now.Year;
                int endDate = currentDate + 20;
                while (currentDate <= endDate)
                {
                    dropYear.Items.Add(currentDate.ToString());
                    currentDate++;
                }
            }
        }
        
        protected void btnAddCard_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                if (lblCardNumberFormat.Visible == true)
                    lblCardNumberFormat.Visible = false;
                if (lblCardTypeError.Visible == true)
                    lblCardTypeError.Visible = false;
                if (lblCardNumberError.Visible == true)
                    lblCardNumberError.Visible = false;
                if (lblCVError.Visible == true)
                    lblCVError.Visible = true;
                try
                {
                    if (txtCardNumber.Text.Count()==16)
                    {
                        IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                        ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
                        string cardNumber = txtCardNumber.Text.ToString();
                        string cardType;
                        if (chBDebit.Checked==true)
                            cardType = "Debit";
                        else if (chBCredit.Checked)
                            cardType = "Credit";
                        else
                            throw new Exception("cardType");
                        // txtType.Text.ToString();
                        Int16 cv = 0;
                        bool canConvert = Int16.TryParse(txtCV.Text, out cv);
                        if (!canConvert || txtCV.Text.Count() != 3)
                            throw new Exception("cv");
                        Int32 dd1 = Convert.ToInt32(dropMonth.SelectedItem.Text);
                        Int32 dd2 = Convert.ToInt32(dropYear.SelectedItem.Text);
                        DateTime expirationTime = new DateTime(dd2, dd1, 1);
                        CardDetails newCard = new CardDetails(cardNumber, cv, expirationTime, cardType);
                        cardService.AddCard(SessionManager.GetUserSession(Context).UserProfileId, newCard);
                        string message = GetLocalResourceObject("messageCard.Text").ToString();
                        //Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Card/SeeMyCards.aspx"));
                        Response.Write("<script language=javascript>alert('" + message + "'); location.href='/Pages/Card/SeeMyCards.aspx';</script>");
                    }
                    else
                    {
                        lblCardNumberFormat.Visible = true;
                    }
                }
                catch (IncorrectCardNumberFormatException)
                {
                    lblCardNumberFormat.Visible = true;
                }
                catch (DuplicateInstanceException)
                {
                    lblCardNumberError.Visible = true;
                }
                catch (Exception w)
                {
                    if (w.Message.Equals("cv"))
                        lblCVError.Visible = true;
                    else
                        lblCardTypeError.Visible = true;
                }
            }
        }

        protected void dropYear_Load(object sender, EventArgs e)
        {
            
        }

        protected void dropMonth_Load(object sender, EventArgs e)
        {
            
        }

        protected void chBCredit_CheckedChanged(object sender, EventArgs e)
        {
            chBDebit.Checked = false;
        }

        protected void chBDebit_CheckedChanged(object sender, EventArgs e)
        {
            chBCredit.Checked = false;
        }
    }
}