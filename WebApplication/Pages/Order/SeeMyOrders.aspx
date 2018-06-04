<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="SeeMyOrders.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order.SeeMyOrders" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
           -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form runat="server">
         <asp:GridView ID="gvOrders"  runat="server" CssClass="productsResult"
            AutoGenerateColumns="False"
            ShowHeaderWhenEmpty="True" meta:resourcekey="gvOrdersResource1" >
            <Columns>
            <asp:HyperLinkField DataTextField="OrderId" HeaderText="<%$ Resources:, OrderId %>" DataNavigateUrlFields="OrderId" DataNavigateUrlFormatString="OrderDetail.aspx?orderId={0}" meta:resourcekey="BoundFieldResource5"/>
            <asp:BoundField DataField="CardNumber" HeaderText="<%$ Resources:, CardNumber %>" meta:resourcekey="BoundFieldResource1"/>
            <asp:BoundField DataField="PostalAddress" HeaderText="<%$ Resources:, PostalAddress %>" meta:resourcekey="BoundFieldResource2"/>
            <asp:BoundField DataField="OrderDate" HeaderText="<%$ Resources:, OrderDate %>" Visible="true" meta:resourcekey="BoundFieldResource3"  />
            <asp:BoundField DataField="NumberOfProducts"  HeaderText="<%$ Resources:, NumberOfProducts %>" meta:resourcekey="BoundFieldResource4"/>
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
