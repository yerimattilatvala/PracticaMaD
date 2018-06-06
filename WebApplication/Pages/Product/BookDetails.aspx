<%@ Page Language="C#" MasterPageFile="~/Pages/Product/ProductMasterPage.master" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Product.BookDetails" meta:resourcekey="PageResource1" %>

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
                    <asp:Localize ID="lclAuthor" runat="server" meta:resourcekey="lclAuthor" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclAuthorValue" runat="server" meta:resourcekey="lclAuthorValueResource1"/>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSummary" runat="server" meta:resourcekey="lclSummary" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclSummaryValue" runat="server" meta:resourcekey="lclSummaryValueResource1" />
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
                    <asp:Localize ID="lclPages" runat="server" meta:resourcekey="lclPages" />
                </span>
                <span class="entry">
                    <asp:Localize ID="lclPagesValue" runat="server" meta:resourcekey="lclPagesValueResource1"/>
                </span>
            </div>
    </div>
<br />