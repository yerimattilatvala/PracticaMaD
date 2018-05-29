<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.MainPage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form id="SearchProductsForm" method="post" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclKeywords" runat="server" meta:resourcekey="searchProducts" />
            </span><span class="entry">
                <asp:TextBox ID="txtKeywords" runat="server" Width="200px" Columns="16" meta:resourcekey="txtKeywordsResource1" />
                <asp:RequiredFieldValidator ID="rfvKeywords" runat="server" ControlToValidate="txtKeywords"
                    Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" CssClass="errorMessage" meta:resourcekey="rfvKeywordsResource1" />
            </span>
            </div>
        <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCategory" runat="server" AutoPostBack="True" 
                    Width="100px" meta:resourcekey="comboCategoryResource1" 
                    onselectedindexchanged="ComboCategorySelectedIndexChanged">
                        </asp:DropDownList></span>
        </div>
        <div class="button">
            <asp:Button ID="btnFind" runat="server" meta:resourcekey="btnFind" OnClick="BtnFindClick" />
        </div>
        </form>

    <br />

</asp:Content>
