<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order.OrderDetail" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
          
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form runat="server">
        <br />
         <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPostalAddress" runat="server" meta:resourcekey="lclPostalAddressResource1"   /></span><span
                        class="entry">
                        <asp:TextBox ID="txtPostalAddress" ReadOnly="True" runat="server" Width="100px" Columns="16" meta:resourcekey="txtPostalAddressResource1"></asp:TextBox>
                </span>
        </div>
         <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCard" runat="server" meta:resourcekey="lclCardResource1"  /></span><span
                        class="entry">
                        <asp:TextBox ID="txtCard" ReadOnly="True" runat="server" Width="100px" Columns="16" meta:resourcekey="txtCardResource1"  ></asp:TextBox>
                    </span>
        </div>
        <br />
        <br />
         <asp:GridView ID="gvOrderDetails"  runat="server" CssClass="productsResult"
            AutoGenerateColumns="False"
             OnRowCreated="gvOrderDetails_RowCreated"
            ShowHeaderWhenEmpty="True" meta:resourcekey="gvOrderDetailsResource1" OnRowCommand="gvOrderDetails_RowCommand"  >
            <Columns>
            <asp:ButtonField DataTextField="Name" ButtonType="Link" HeaderText="<%$ Resources:, Name %>" commandname="ViewProduct"/>
            <asp:BoundField DataField="Category" HeaderText="<%$ Resources:, Category %>" meta:resourcekey="BoundFieldResource1" />
            <asp:BoundField DataField="Prize" HeaderText="<%$ Resources:, Prize %>" Visible="true" meta:resourcekey="BoundFieldResource2"  />
            <asp:BoundField DataField="NumberOfUnits"  HeaderText="<%$ Resources:, NumberOfUnits %>" meta:resourcekey="BoundFieldResource3"/>
            <asp:BoundField DataField="ProductId" Visible="true"  HeaderText="<%$ Resources:, ProductId %>" meta:resourcekey="BoundFieldResource4"/>
            <asp:CheckBoxField DataField="ForGift" Visible="true" HeaderText="<%$ Resources:, ForGift %>" />
            </Columns>
        </asp:GridView>
        <br />
        
         <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPrize" runat="server" meta:resourcekey="lclPrizeResource1"  /></span><span
                        class="entry">
                        <asp:TextBox ID="txtPrize" ReadOnly="True" runat="server" Width="100px" Columns="16" meta:resourcekey="txtPrizeResource1"  ></asp:TextBox>
                    </span>
        </div>
        <br />
        <br />
    </form>
</asp:Content>