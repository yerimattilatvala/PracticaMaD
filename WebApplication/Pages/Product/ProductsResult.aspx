<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master"
    AutoEventWireup="true" CodeBehind="ProductsResult.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.ProductsResult" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
        <br />
        <div class="button">
         <span>
             <asp:Label ID="lblAreProducts" runat="server" Visible="false" meta:resourcekey="lblAreProducts" ></asp:Label>
         </span>
        <br />
    <asp:GridView ID="gvProductsResult"  CssClass="productsResult"
        AutoGenerateColumns="False"
        AllowPaging="true"
         onpageindexchanging="GvAccOperationsPageIndexChanging"
        OnSelectedIndexChanging="gvProductsResult_SelectedIndexChanging"
        runat="server" meta:resourcekey="gvProductsResultResource1" OnRowCommand="OnProductNameClick" PageSize="2">
        <Columns>
            <asp:ButtonField DataTextField="name" ButtonType="Link" HeaderText="<%$ Resources:, productName %>" commandname="ViewProduct"/>
            <asp:BoundField DataField="Category" HeaderText="<%$ Resources:, productCategory %>"/>
            <asp:BoundField DataField="Prize" HeaderText="<%$ Resources:, productPrize %>"/>
            <asp:BoundField DataField="NumberOfUnits" HeaderText="<%$ Resources:, numberOfUnits %>"/>
            <asp:BoundField DataField="ProductId" HeaderText="<%$ Resources:, productId %>" Visible="False" />
            <asp:CommandField ShowSelectButton="True" SelectText="<%$ Resources:, addToCart %>" />
        </Columns>
    </asp:GridView>
     <div class="button">
         <span>
             <asp:Label ID="lblNoUnits" runat="server" Visible="false" meta:resourcekey="lblNoUnits" ></asp:Label>
         </span>
     </div>
        <br />
    <!-- "Previous" and "Next" links. -->
         <div class="button">
         <span>
             <asp:Label ID="lblCache" runat="server" Visible="false" ></asp:Label>
         </span>
     </div>
        <br />
    </form>

</asp:Content>

