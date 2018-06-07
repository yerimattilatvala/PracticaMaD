<%@ Page Language="C#"  MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.User.MyAccount" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <div>
     <asp:HyperLink ID="lnkUpdate" runat="server" meta:resourcekey="lnkUpdate" NavigateUrl="~/Pages/User/UpdateUserProfile.aspx" >[lnkUpdate]</asp:HyperLink>
    </div>
    <br />
    <div>                       
        <asp:HyperLink ID="lnkCards" runat="server" meta:resourcekey="lnkCards" NavigateUrl="~/Pages/Card/SeeMyCards.aspx" >[lnkCards]</asp:HyperLink>
    </div>
    <br />
    <div>
        <asp:HyperLink ID="lnlkAddCard" runat="server" meta:resourcekey="lnlkAddCard" NavigateUrl="~/Pages/Card/AddCard.aspx" >[lnlkAddCard]</asp:HyperLink>
    </div>
    <br />
    <div>                        
        <asp:HyperLink ID="lnlOrders" runat="server" meta:resourcekey="lnlOrders" NavigateUrl="~/Pages/Order/SeeMyOrders.aspx" >[lnlOrders]</asp:HyperLink>
    </div>
    <br />
</asp:Content>
