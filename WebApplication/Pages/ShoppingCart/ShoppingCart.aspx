<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.ShoppingCart.ShoppingCart" meta:resourcekey="PageResource1" %>
<%@ MasterType  virtualPath="~/PracticaMaD.Master"%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
        <asp:GridView ID="gvProductsInCard" runat="server" CssClass="productsResult"
            AutoGenerateColumns="False"
            OnSelectedIndexChanging="gvProductsInCard_changing"
            OnRowCreated="gvProductsInCard_RowCreated"
            ShowHeaderWhenEmpty="True"  meta:resourcekey="gvProductsInCard">
            <Columns>
            <asp:BoundField DataField="Name" HeaderText="<%$ Resources:, productName %>" meta:resourcekey="BoundFieldResource1"/>
            <asp:BoundField DataField="Prize" HeaderText="<%$ Resources:, productPrize %>" meta:resourcekey="BoundFieldResource2"/>
            <asp:BoundField DataField="NumberOfUnits" HeaderText="<%$ Resources:, numberOfUnits %>" meta:resourcekey="BoundFieldResource3"/>
            <asp:BoundField DataField="ProductId" HeaderText="<%$ Resources:, productId %>" Visible="true"  meta:resourcekey="BoundFieldResource4" />
            <asp:CommandField ShowSelectButton="True" SelectText="<%$ Resources:, deleteProduct %>" meta:resourcekey="CommandFieldResource1" />           
            
                
            
                <asp:TemplateField HeaderText="<%$ Resources:, quantity %>" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:DropDownList ID="listaCantidades"  AutoPostBack="True" OnSelectedIndexChanged="listaCantidades_SelectedIndexChanged" runat="server" meta:resourcekey="listaCantidadesResource2">
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
        <div class="button">
                <asp:Button ID="btnPay" runat="server" OnClick="BtnPayClick" meta:resourcekey="btnPay" />
            </div>
    </form>
    <br/>
    <asp:label id="MessageLabel"
        forecolor="Red"
        runat="server" meta:resourcekey="MessageLabelResource1"/>
</asp:Content>
