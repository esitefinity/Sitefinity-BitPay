<%@ Control Language="C#" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI.Fields" Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" %>

<asp:Label id="titleLabel" runat="server" CssClass="sfTxtLbl"></asp:Label>
<asp:Label id="exampleLabel" runat="server" CssClass="sfExample"></asp:Label>

<div class="sfForm">
    <div class="sfFormIn">
        <sitefinity:TextField ID="createInvoiceUrl" runat="server" CssClass="sfShortField350" DisplayMode="Write" Title='<%$Resources:BitPayResources, CreateInvoiceUrl %>'>
            <ValidatorDefinition Required="True" 
                RequiredViolationMessage="<%$Resources:BitPayResources, CreateInvoiceUrlRequired %>"
                MessageCssClass="sfError" />
        </sitefinity:TextField>

        <sitefinity:TextField ID="getInvoiceUrl" runat="server" CssClass="sfShortField350" DisplayMode="Write" Title='<%$Resources:BitPayResources, GetInvoiceUrl %>'>
            <ValidatorDefinition Required="True" 
                RequiredViolationMessage="<%$Resources:BitPayResources, GetInvoiceUrlRequired %>"
                MessageCssClass="sfError" />
        </sitefinity:TextField>

        <sitefinity:TextField ID="apiKey" runat="server" CssClass="sfShortField180" DisplayMode="Write" Title='<%$Resources:BitPayResources, ApiKey %>'>
            <ValidatorDefinition Required="True" 
                RequiredViolationMessage="<%$Resources:BitPayResources, ApiKeyRequired %>"
                MessageCssClass="sfError" />
        </sitefinity:TextField>
            
        <sitefinity:ChoiceField id="transactionSpeed" DisplayMode="Write" RenderChoicesAs="DropDown" runat="server" Title='<%$Resources:BitPayResources, PaymentMethodTransactionSpeed %>' CssClass="sfRadioList" >            
            <Choices>
                <sitefinity:ChoiceItem Text="<%$Resources:BitPayResources, High %>" Selected="true" Value="high"  />
                <sitefinity:ChoiceItem Text="<%$Resources:BitPayResources, Medium %>" Selected="false" Value="medium"  />
                <sitefinity:ChoiceItem Text="<%$Resources:BitPayResources, Low %>" Selected="false" Value="low"  />
            </Choices>        
        </sitefinity:ChoiceField>
            
        <sitefinity:TextField ID="notificationEmail" runat="server" CssClass="sfShortField180" DisplayMode="Write" Title='<%$Resources:BitPayResources, NotificationEmail %>'></sitefinity:TextField>
    </div>
</div>

<div class="sfForm">
    <ul class="sfFormIn">
        <sitefinity:TextField ID="timeout" runat="server" DisplayMode="Write" Title="<%$Resources:OrdersResources, PaymentMethodTimeout %>" CssClass="sfShortField80" WrapperTag="li">
            <ValidatorDefinition Required="false" ExpectedFormat="Numeric" 
                                    NumericViolationMessage="<%$Resources:OrdersResources, PaymentMethodTimeoutViolationMessage %>"
                                    MessageCssClass="sfError" />
        </sitefinity:TextField>
        <sitefinity:ChoiceField id="paymentType" DisplayMode="Write" RenderChoicesAs="DropDown" runat="server" Title='<%$Resources:OrdersResources, PaymentMethodPaymentType %>' CssClass="sfRadioList" WrapperTag="li">            
            <Choices>
                <sitefinity:ChoiceItem Text="<%$Resources:OrdersResources, PaymentTypeSale %>" Selected="true" Value="sale"  />
            </Choices>        
        </sitefinity:ChoiceField>
    </ul>
</div>
<asp:Label id="descriptionLabel" runat="server" CssClass="sfDescription"></asp:Label>


<asp:HiddenField ID="paymentMethodId" runat="server" />
