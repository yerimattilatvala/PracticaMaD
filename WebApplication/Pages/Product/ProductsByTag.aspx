﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ProductsByTag.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.ProductsByTag" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form runat="server">
        <div class="button">
            <span>
                <asp:Label ID="lclTagName" Font-Size="X-Large" Font-Bold="true" runat="server" meta:resourcekey="lclTagNameResource1" />
            </span>
        </div>
        
        <br />
        <asp:GridView ID="gvProductsTag"  CssClass="productsResult"
        AutoGenerateColumns="False"
        OnPageIndexChanging="gvProductsTag_PageIndexChanging"
        OnSelectedIndexChanging="gvProductsTag_SelectedIndexChanging"
        OnRowCommand="gvProductsTag_RowCommand"
        runat="server"
        ShowHeaderWhenEmpty="True" meta:resourcekey="gvProductsTagResource1" >
        <Columns>
            <asp:ButtonField DataTextField="name" ButtonType="Link" HeaderText="<%$ Resources:, productName %>" commandname="ViewProduct" meta:resourcekey="ButtonFieldResource1"/>
            <asp:BoundField DataField="Category" HeaderText="<%$ Resources:, productCategory %>" meta:resourcekey="BoundFieldResource1"/>
            <asp:BoundField DataField="Prize" HeaderText="<%$ Resources:, productPrize %>" meta:resourcekey="BoundFieldResource2"/>
            <asp:BoundField DataField="NumberOfUnits" HeaderText="<%$ Resources:, numberOfUnits %>" meta:resourcekey="BoundFieldResource3"/>
            <asp:BoundField DataField="ProductId" HeaderText="<%$ Resources:, productId %>" Visible="False" meta:resourcekey="BoundFieldResource4" />
            <asp:CommandField ShowSelectButton="True" SelectText="<%$ Resources:, addToCart %>" meta:resourcekey="CommandFieldResource1" />
        </Columns>
    </asp:GridView>
    <div class="button">
        <span>
            <asp:Label ID="lblNoUnits" runat="server" Visible="false" meta:resourcekey="lblNoUnits" ></asp:Label>
        </span>
    </div>
    <br />
        <br />
        <div class="button">
            <asp:Button ID="btnDeleteTag" runat="server" OnClick="btnDeleteTag_Click" meta:resourcekey="btnDeleteTag"/>
        </div>
        <div class="button">
            <span>
                <asp:Label ID="lblAutenticated" Font-Bold="true" ForeColor="Red" runat="server" meta:resourcekey="lblAutenticated" />
            </span>
        </div>
        <br />
    </form>
</asp:Content>
