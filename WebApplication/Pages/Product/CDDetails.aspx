<%@ Page Language="C#"  MasterPageFile="~/Pages/Product/ProductMasterPage.master" AutoEventWireup="true" CodeBehind="CDDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.CDDetails" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_ProductDetails_BodyContent"
    runat="server">
    <div id="form">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" />
                </span>
                <span class="entry">
                <asp:Localize ID="lclNameValue" runat="server" meta:resourcekey="lclNameValueResource1"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclCategoryValue" runat="server" meta:resourcekey="lclCategoryValueResource1"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclTitleValue" runat="server" meta:resourcekey="lclTitleValueResource1" />
                    </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclArtist" runat="server" meta:resourcekey="lclArtist" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclArtistValue" runat="server" meta:resourcekey="lclArtistValueResource1"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTopic" runat="server" meta:resourcekey="lclTopic" />  
                </span>
                <span class="entry">
                    <asp:Localize ID="lclTopicValue" runat="server" meta:resourcekey="lclTopicValueResource1"/>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSongs" runat="server" meta:resourcekey="lclSongs" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclSongsValue" runat="server" meta:resourcekey="lclSongsValueResource1"/>
                </span>
            </div>
    </div>
<br />

</asp:Content>