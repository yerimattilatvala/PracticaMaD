<%@ Page Language="C#" MasterPageFile="~/Pages/Product/ProductMasterPage.master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_ProductDetails_BodyContent"
    runat="server">
    <div id="form">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" />
                </span>
                <span class="entry">
                <asp:Localize ID="lclNameValue" runat="server"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclCategoryValue" runat="server"/>
                </span>
            </div>
    </div>
<br />
</asp:Content>
