<%@ Page Language="C#" MasterPageFile="~/Pages/Product/ProductMasterPage.master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.ProductDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_ProductDetails_BodyContent"
    runat="server">
    <div id="form">
            <div class="button">
                <span>
                    <asp:Label ID="lclName" Font-Bold="true" Font-Size="X-Large" runat="server" meta:resourcekey="lclName" />
                </span>
                <span>
                <asp:Label ID="lclNameValue" Font-Bold="true" Font-Size="X-Large"  runat="server"/>
                </span>
            </div>
            <br />
            <div class="buttom">
                <span>
                    <asp:Label ID="lclCategory"  Font-Bold="true" Font-Size="Large" runat="server" meta:resourcekey="lclCategory" />
                </span>
                <span>
                    <asp:Label ID="lclCategoryValue" Font-Bold="true" Font-Size="Large" runat="server"/>
                </span>
            </div>
        <br />
    </div>
<br />
</asp:Content>
