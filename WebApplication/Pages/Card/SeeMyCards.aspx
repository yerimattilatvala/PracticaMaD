<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="SeeMyCards.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Card.SeeMyCards" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
        -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
        <asp:GridView ID="gvAllCards"  runat="server" CssClass="productsResult"
            AutoGenerateColumns="False"
            OnSelectedIndexChanging="gvAllCards_SelectedIndexChanging"
            OnRowCreated="gvAllCards_RowCreated"
            OnRowDataBound="gvAllCards_RowDataBound"
            ShowHeaderWhenEmpty="True" meta:resourcekey="gvAllCardsResource1">
            <Columns>
            <asp:BoundField DataField="CardNumber"  HeaderText="<%$ Resources:, CardNumber %>" meta:resourcekey="BoundFieldResource1" />
            <asp:BoundField DataField="CardType" HeaderText="<%$ Resources:, CardType %>" meta:resourcekey="BoundFieldResource2" />
            <asp:BoundField DataField="ExpirateTime" HeaderText="<%$ Resources:, ExpirateTime %>" meta:resourcekey="BoundFieldResource3" />
            <asp:BoundField DataField="CardId" HeaderText="<%$ Resources:, CardId %>" Visible="true" meta:resourcekey="BoundFieldResource4"  />
            <asp:BoundField DataField="DefaultCard" HeaderText="<%$ Resources:, DefaultCard %>" Visible="true" meta:resourcekey="BoundFieldResource4"  />
                <asp:TemplateField  HeaderText="<%$ Resources:, DefaultCard %>">
                    <ItemTemplate>
                        <asp:checkbox ID="changeDefaultCard" AutoPostBack="true" runat="server" OnCheckedChanged="changeDefaultCard_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </form>
</asp:Content>
