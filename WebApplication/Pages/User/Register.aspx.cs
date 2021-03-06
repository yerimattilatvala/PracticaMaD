﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.Session;
using System.Globalization;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.WebApplication.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ModelService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.User
{
    public partial class Register : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLoginError.Visible = false;
            lblEmailError.Visible = false;
            if (!IsPostBack)
            {

                /* Get current language and country from browser */
                String defaultLanguage =
                    GetLanguageFromBrowserPreferences();
                String defaultCountry =
                    GetCountryFromBrowserPreferences();

                /* Combo box initialization */
                UpdateComboLanguage(defaultLanguage);
                UpdateComboCountry(defaultLanguage, defaultCountry);

            }
        }

        private String GetLanguageFromBrowserPreferences()
        {
            String language;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
            language = cultureInfo.TwoLetterISOLanguageName;
            LogManager.RecordMessage("Preferred language of user" +
                                     " (based on browser preferences): " + language);
            return language;
        }

        private String GetCountryFromBrowserPreferences()
        {
            String country;
            CultureInfo cultureInfo =
                CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            if (cultureInfo.IsNeutralCulture)
            {
                country = "";
            }
            else
            {
                // cultureInfoName is something like en-US
                String cultureInfoName = cultureInfo.Name;
                // Gets the last two caracters of cultureInfoname
                country = cultureInfoName.Substring(cultureInfoName.Length - 2);

                LogManager.RecordMessage("Preferred region/country of user " +
                                         "(based on browser preferences): " + country);
            }

            return country;
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

      
        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
         /* After a language change, the countries are printed in the
         * correct language.
         */
            this.UpdateComboCountry(comboLanguage.SelectedValue,
                comboCountry.SelectedValue);
        }

        protected void BtnRegisterClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UserProfileDetails userProfileDetailsVO =
                        new UserProfileDetails(txtFirstName.Text, txtSurname.Text,
                            txtEmail.Text, comboLanguage.SelectedValue,
                            comboCountry.SelectedValue, Int32.Parse(txtPostalAddress.Text));

                    SessionManager.RegisterUser(Context, txtLogin.Text,
                        txtPassword.Text, userProfileDetailsVO);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));

                }
                catch (DuplicateEmailException)
                {
                    lblEmailError.Visible = true;
                }
                catch (DuplicateInstanceException)
                {
                    lblLoginError.Visible = true;
                }

            }
        }
    }
}