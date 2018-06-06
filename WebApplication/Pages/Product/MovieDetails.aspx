<%@ Page Language="C#"  MasterPageFile="~/Pages/Product/ProductMasterPage.master" AutoEventWireup="true" CodeBehind="MovieDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.MovieDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_ProductDetails_BodyContent"
    runat="server">
    <div id="form">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" />
                </span>
                <span class="entry">
                <asp:Localize ID="lclNameValue" runat="server"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclCategoryValue" runat="server"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclTitleValue" runat="server" />
                    </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclDirector" runat="server" meta:resourcekey="lclDirector" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclDirectorValue" runat="server"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSummary" runat="server" meta:resourcekey="lclSummary" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclSummaryValue" runat="server" />
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTopic" runat="server" meta:resourcekey="lclTopic" />  
                </span>
                <span class="entry">
                    <asp:Localize ID="lclTopicValue" runat="server"/>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclDuration" runat="server" meta:resourcekey="lclDuration" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclDurationValue" runat="server"/>
                </span>
            </div>
    </div>
<br />

</asp:Content>
