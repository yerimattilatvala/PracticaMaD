<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddCard.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Card.AddCard" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
     -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form runat="server">
    <div id="form">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardNumber" runat="server" meta:resourcekey="lclCardNumberResource1"/></span><span
                        class="entry">
                        <asp:TextBox ID="txtCardNumber" runat="server" Width="100px" Columns="16" meta:resourcekey="txtCardNumberResource1" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCardNumber" runat="server" ControlToValidate="txtCardNumber"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvCardNumberResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblCardNumberError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblCardNumberErrorResource1"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardType" runat="server" meta:resourcekey="lclCardTypeResource1"  /></span><span
                        class="entry">
                        <asp:TextBox ID="txtType" runat="server" 
                    Width="100px" Columns="16" meta:resourcekey="txtTypeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="txtType"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvTypeResource1" ></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclExpirationTime" runat="server" meta:resourcekey="lclExpirationTimeResource1" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtExpirationTime" runat="server" Width="100px" 
                    Columns="16" meta:resourcekey="txtExpirationTimeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvExpirationTime" runat="server" ControlToValidate="txtExpirationTime"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvExpirationTimeResource1" ></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCV" runat="server" meta:resourcekey="lclCVResource1"/></span><span
                        class="entry">
                        <asp:TextBox ID="txtCV" runat="server" Width="100px" Columns="16" meta:resourcekey="txtCVResource1" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCV" runat="server" ControlToValidate="txtCV"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvCVResource1" ></asp:RequiredFieldValidator></span>
            </div>
            <div class="button">
                <asp:Button ID="btnAddCard" runat="server" OnClick="btnAddCard_Click" meta:resourcekey="btnAddCardResource1"/>
            </div>
    </div>
    </form>
</asp:Content>
