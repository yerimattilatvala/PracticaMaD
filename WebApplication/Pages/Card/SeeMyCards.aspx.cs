using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.CardService;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.WebApplication.Properties;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Card
{
    public partial class SeeMyCards : SpecificCulturePage
    {
        ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
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

        protected void changeDefaultCard_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            GridViewRow row = checkBox.NamingContainer as GridViewRow;
            long cardId = (long)Convert.ToInt32(row.Cells[3].Text.ToString());
            long usrId = SessionManager.GetUserSession(Context).UserProfileId;
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();
            cardService.ChangeDefaultCard(usrId, cardId);
            LoadGrid();
        }

        protected void gvAllCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllCards.PageIndex = e.NewPageIndex;
            LoadGrid();
            //gvAllCards.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICardService cardService = (ICardService)iocManager.Resolve<ICardService>();

            e.ObjectInstance = (ICardService)cardService;
        }

        protected void changeDefaultCard_DataBinding(object sender, EventArgs e)
        {
            CheckBox chB = sender as CheckBox;
            GridViewRow gvRow = chB.NamingContainer as GridViewRow;
            if (chB != null && gvRow.Cells[4].Text.Equals("True"))
                chB.Checked = true;
        }
    }
}