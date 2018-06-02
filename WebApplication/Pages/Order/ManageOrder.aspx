<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ManageOrder.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order.ManageOrder" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
       -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
         <asp:GridView ID="gvProductsToPay"  runat="server" CssClass="productsResult"
            AutoGenerateColumns="False"
             OnRowCreated="gvProductsToPay_RowCreated"
             OnSelectedIndexChanging="gvProductsToPay_SelectedIndexChanging"
            ShowHeaderWhenEmpty="True">
            <Columns>
            <asp:BoundField DataField="Name"  HeaderText="<%$ Resources:, productName %>"/>
            <asp:BoundField DataField="Prize" HeaderText="<%$ Resources:, productPrize %>"/>
            <asp:BoundField DataField="NumberOfUnits" HeaderText="<%$ Resources:, numberOfUnits %>"/>
            <asp:BoundField DataField="ProductId" HeaderText="<%$ Resources:, productId %>" Visible="true"/>
            <asp:CommandField ShowSelectButton="True" SelectText="<%$ Resources:, deleteProduct %>" />           
                <asp:TemplateField HeaderText="<%$ Resources:, quantity %>" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:DropDownList ID="listaCantidades"  AutoPostBack="True" runat="server" OnSelectedIndexChanged="listaCantidades_SelectedIndexChanged">
                            <asp:ListItem meta:resourcekey="ListItemResource7"></asp:ListItem>
                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource8">1</asp:ListItem>
                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource9">2</asp:ListItem>
                            <asp:ListItem  Value="3" meta:resourcekey="ListItemResource10">3</asp:ListItem>
                            <asp:ListItem  Value="4" meta:resourcekey="ListItemResource11">4</asp:ListItem>
                            <asp:ListItem value="5" meta:resourcekey="ListItemResource12">5</asp:ListItem>
                            <asp:ListItem Value="6" meta:resourcekey="ListItemResource13">6</asp:ListItem>
                            <asp:ListItem Value="7" meta:resourcekey="ListItemResource14">7</asp:ListItem>
                            <asp:ListItem Value="8" meta:resourcekey="ListItemResource15">8</asp:ListItem>
                            <asp:ListItem Value="9" meta:resourcekey="ListItemResource16">9</asp:ListItem>
                            <asp:ListItem Value="10" meta:resourcekey="ListItemResource17">10</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </form>
</asp:Content>
