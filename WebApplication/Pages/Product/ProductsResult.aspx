<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master"
    AutoEventWireup="true" CodeBehind="ProductsResult.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.ProductsResult" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
    <asp:GridView ID="gvProductsResult" runat="server" CssClass="productsResult"
        AutoGenerateColumns="False" 
        onpageindexchanging="GvAccOperationsPageIndexChanging" 
        ShowHeaderWhenEmpty="True" meta:resourcekey="gvProductsResultResource1">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="<%$ Resources:, productName %>"/>
            <asp:BoundField DataField="Category" HeaderText="<%$ Resources:, productCategory %>"/>
            <asp:BoundField DataField="Prize" HeaderText="<%$ Resources:, productPrize %>"/>
        </Columns>
    </asp:GridView>
    <!-- "Previous" and "Next" links. -->
    </form>

    <br />

</asp:Content>

