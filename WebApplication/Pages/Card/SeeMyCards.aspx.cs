using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Card
{
    public partial class SeeMyCards : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
            gvAllCards.DataSource = cardService.ViewCardsByUser(SessionManager.GetUserSession(Context).UserProfileId);
            gvAllCards.DataBind();
        }

        protected void gvAllCards_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        protected void RadioButton1_DataBinding(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            GridViewRow gvR = rb.NamingContainer as GridViewRow;
            if (gvR.Cells[4].Text.Equals("True"))
                rb.Checked = true;
        }

            /*RadioButton rb = sender as RadioButton;
            rb.Checked = true;
            GridViewRow gvR = rb.NamingContainer as GridViewRow;
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
            long idCard = Convert.ToInt32(gvR.Cells[3].Text.ToString());
            long usrId = SessionManager.GetUserSession(Context).UserProfileId;
            cardService.ChangeDefaultCard(usrId, 2);
            Response.Redirect(Request.RawUrl.ToString());*/

        protected void gvAllCards_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
            long idCard = Convert.ToInt32(gvAllCards.Rows[e.NewSelectedIndex].Cells[3].Text.ToString());
            long usrId = SessionManager.GetUserSession(Context).UserProfileId;
            cardService.ChangeDefaultCard(usrId, idCard);
            Response.Redirect(Request.RawUrl.ToString());
            gvAllCards.DataSource = cardService.ViewCardsByUser(usrId);
            gvAllCards.DataBind();
        }
    }
}