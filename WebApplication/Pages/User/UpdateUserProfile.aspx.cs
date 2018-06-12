using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.User
{
    public partial class UpdateUserProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblEmailError.Visible = false;
            if (!IsPostBack)
            {
                UserProfileDetails userProfileDetails =
                    SessionManager.FindUserProfileDetails(Context);

                txtFirstName.Text = userProfileDetails.FirstName;
                txtSurname.Text = userProfileDetails.Lastname;
                txtEmail.Text = userProfileDetails.Email;
                txtPostalAddress.Text = userProfileDetails.PostalAddress.ToString();

                /* Combo box initialization */
                UpdateComboLanguage(userProfileDetails.Language);
                UpdateComboCountry(userProfileDetails.Language,
                    userProfileDetails.Country);
            }
        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            /* After a language change, the countries are printed in the
          * correct language.
          */
            this.UpdateComboCountry(comboLanguage.SelectedValue,
                comboCountry.SelectedValue);
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UserProfileDetails userProfileDetails =
                    new UserProfileDetails(txtFirstName.Text, txtSurname.Text,
                        txtEmail.Text, comboLanguage.SelectedValue,
                        comboCountry.SelectedValue,Int32.Parse(txtPostalAddress.Text));

                try
                {
                    SessionManager.UpdateUserProfileDetails(Context,
                        userProfileDetails);

                    Response.Redirect(
                        Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));
                }
                catch (DuplicateEmailException)
                {
                    lblEmailError.Visible = true;
                }
            }
        }

        /// <summary>
        /// Loads the languages in the comboBox in the *selectedLanguage*. 
        /// Also, the selectedLanguage will appear selected in the 
        /// ComboBox
        /// </summary>
        private void UpdateComboLanguage(String selectedLanguage)
        {
            this.comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
            this.comboLanguage.DataTextField = "text";
            this.comboLanguage.DataValueField = "value";
            this.comboLanguage.DataBind();
            this.comboLanguage.SelectedValue = selectedLanguage;
        }

        /// <summary>
        /// Loads the countries in the comboBox in the *selectedLanguage*. 
        /// Also, the *selectedCountry* will appear selected in the 
        /// ComboBox
        /// </summary>
        private void UpdateComboCountry(String selectedLanguage, String selectedCountry)
        {
            this.comboCountry.DataSource = Countries.GetCountries(selectedLanguage);
            this.comboCountry.DataTextField = "text";
            this.comboCountry.DataValueField = "value";
            this.comboCountry.DataBind();
            this.comboCountry.SelectedValue = selectedCountry;
        }

 
    }
}