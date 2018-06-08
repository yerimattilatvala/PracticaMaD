<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ManageOrder.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.WebApplication.Pages.Order.ManageOrder" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
       -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <form runat="server">
        <br />
        <div>
        <span>
            <asp:Label ID="lclIntroducePostalAddress" Font-Bold="true" Font-Size="Medium" runat="server" meta:resourcekey="lclIntroducePostalAddressResource1"/>
        </span>
        </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPostalAddress" runat="server" meta:resourcekey="lclPostalAddressResource1"  /></span><span
                        class="entry">
                        <asp:TextBox ID="txtPostalAddress" runat="server" Width="100px" Columns="16" meta:resourcekey="txtPostalAddressResource1" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPostalAddress" runat="server" ControlToValidate="txtPostalAddress"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvPostalAddressResource2" ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPostalAddress" runat="server" ControlToValidate="txtPostalAddress" Type="Integer"
                         Operator="DataTypeCheck" ErrorMessage="<%$ Resources:Common, integerField %>" meta:resourcekey="cvPostalAddressResource2"  /></span>
        </div>
        <br />
        <div class="button">
        <span>
            <asp:Label ID="lclSelectPayMethod" Font-Bold="true" Font-Size="Medium" runat="server" meta:resourcekey="lclSelectPayMethodResource1"  />
        </span>
        </div>
        <br />
        <asp:GridView ID="gvAllCards"  runat="server" CssClass="productsResult"
            AutoGenerateColumns="False"
            OnPageIndexChanging="gvAllCards_PageIndexChanging"
            ShowHeaderWhenEmpty="True" meta:resourcekey="gvAllCardsResource2" >
            <Columns>
                <asp:BoundField DataField="CardNumber"  HeaderText="<%$ Resources:, CardNumber %>" meta:resourcekey="BoundFieldResource10"  />
                <asp:BoundField DataField="CardType" HeaderText="<%$ Resources:, CardType %>" meta:resourcekey="BoundFieldResource11" />
                <asp:BoundField DataField="ExpirateTime" HeaderText="<%$ Resources:, ExpirateTime %>" meta:resourcekey="BoundFieldResource12"  />
                <asp:BoundField DataField="CardId" HeaderText="<%$ Resources:, CardId %>" Visible="true" meta:resourcekey="BoundFieldResource13" />
                <asp:BoundField DataField="DefaultCard"  Visible="true" />
                <asp:TemplateField  HeaderText="<%$ Resources:, changeDefault %>" >
                        <ItemTemplate>
                            <asp:checkbox ID="selectPayMent" AutoPostBack="true" runat="server" OnDataBinding="selectPayMent_DataBinding" OnCheckedChanged="selectPayMent_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
            <div id="form">
                <div class="field">
                <span class="label">
                    <asp:Localize Visible="false" ID="lclId" runat="server" meta:resourcekey="lclIdResource2" /></span><span
                        class="entry">
                        <asp:TextBox Visible="false" ID="txtId" runat="server" Width="100px" Columns="16" meta:resourcekey="txtIdResource2"  ></asp:TextBox>
                       </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardNumber" runat="server" meta:resourcekey="lclCardNumberResource2" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtCardNumber" ReadOnly="true" runat ="server" Width="100px" Columns="16" meta:resourcekey="txtCardNumberResource2"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCardNumber" runat="server" ControlToValidate="txtCardNumber"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvCardNumberResource2"></asp:RequiredFieldValidator>
                       </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardType" runat="server" meta:resourcekey="lclCardTypeResource2"  /></span><span
                        class="entry">
                        <asp:TextBox ID="txtType" ReadOnly="true" runat="server" 
                    Width="100px" Columns="16" meta:resourcekey="txtTypeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="txtType"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvTypeResource2"  ></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclExpirationTime" runat="server" meta:resourcekey="lclExpirationTimeResource2"  /></span><span
                        class="entry">
                        <asp:TextBox ID="txtExpirationTime" ReadOnly="true" runat="server" Width="100px" 
                    Columns="16" meta:resourcekey="txtExpirationTimeResource2" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvExpirationTime" runat="server" ControlToValidate="txtExpirationTime"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvExpirationTimeResource2"  ></asp:RequiredFieldValidator></span>
            </div>
    </div>
        <br />
        
        <div class="button">
        <span>
            <asp:Label ID="lclProductsToPay" Font-Bold="true" Font-Size="Medium" runat="server" meta:resourcekey="lclProductsToPayResource1" />
        </span>
        </div>
        <br />
        <asp:GridView ID="gvProductsToPay"  runat="server" CssClass="productsResult"
            AutoGenerateColumns="False"
             OnRowCreated="gvProductsToPay_RowCreated"
             OnSelectedIndexChanging="gvProductsToPay_SelectedIndexChanging"
            OnRowDataBound="gvProductsToPay_RowDataBound"
            ShowHeaderWhenEmpty="True" meta:resourcekey="gvProductsToPayResource2">
            <Columns>
            <asp:BoundField DataField="Name"  HeaderText="<%$ Resources:, productName %>" meta:resourcekey="BoundFieldResource14" />
            <asp:BoundField DataField="Prize" HeaderText="<%$ Resources:, productPrize %>" meta:resourcekey="BoundFieldResource15" />
            <asp:BoundField DataField="NumberOfUnits" HeaderText="<%$ Resources:, numberOfUnits %>" meta:resourcekey="BoundFieldResource16" />
            <asp:BoundField DataField="ProductId" HeaderText="<%$ Resources:, productId %>" Visible="true" meta:resourcekey="BoundFieldResource17" />
             <asp:TemplateField HeaderText="<%$ Resources:, ForGift %>">
                <ItemTemplate>
                    <asp:CheckBox ID="cbForGift" AutoPostBack="true" runat="server" OnCheckedChanged="cbForGift_CheckedChanged" />
                </ItemTemplate>
             </asp:TemplateField>     
            <asp:TemplateField HeaderText="<%$ Resources:, quantity %>" meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                    <asp:DropDownList ID="listaCantidades"  AutoPostBack="True" runat="server"  OnSelectedIndexChanged="listaCantidades_SelectedIndexChanged" meta:resourcekey="listaCantidadesResource2">
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
        <asp:CommandField ShowSelectButton="True" SelectText="<%$ Resources:, deleteProduct %>" meta:resourcekey="CommandFieldResource3"/>           
        </Columns>
        </asp:GridView>
        </div>
        <br />
        <div class="button">
                <span>
                    <asp:Label ID="lblError" Font-Bold="true" ForeColor="Red" runat="server" meta:resourcekey="lblError" /></span>
            </div>
        <br />
            <div class="button">
                <span>
                    <asp:Label ID="lclPrize" Font-Bold="true" runat="server" meta:resourcekey="lclPrizeResource1" /></span>
                <asp:TextBox ID="txtPrizeTotal" ReadOnly="True" runat="server" meta:resourcekey="txtPrizeTotalResource2"></asp:TextBox>
            </div>
        <br />
            <div class="button">
                <asp:Button ID="btnToPay" runat="server" OnClick="btnToPay_Click" meta:resourcekey="btnToPayResource1" />
            </div>
    </form>
    <br />
</asp:Content>
