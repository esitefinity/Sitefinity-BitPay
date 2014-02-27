Sitefinity BitPay Provider sample project.

ABOUT
This is a proof of concept implementation of the BitPay payment processor for Sitefinity
This implementation is compatible with Sitefinity build 6.3.5022 or higher

INSTALLATION 

1) Unzip the Sitefinity-BitPay folder in your SitefinityWebApp project

2) Register the BitPaySettingsField.js in the AssemblyInfo.cs with the following code:

    [assembly: WebResource(SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.scriptReference, "application/x-javascript")]

3) Register the custom resource file (BitPayResources.cs) - Add a global.asax file with the following code:

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += new EventHandler<Telerik.Sitefinity.Data.ExecutedEventArgs>(Bootstrapper_Initialized);
        }

        private void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                Res.RegisterResource<BitPayResources>();
            }
        }
    }


4) Register the BitPay payment processor provider:
http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/ecommerce/payment-methods/payment-processors/creating-payment-processor-providers/installing-the-payment-processor-provider


Administration / Advanced / PaymentProcessor / PaymentProcessorProviders / Create new


Id: Some Guid in the Registry format (example: DC03AC27-2910-420D-9077-A56130C2DAB3)
Name: BITPAY
Title: BitPay
Description: BitPay provider
IsActive: True
SectionName: BitPaySettingsSection
SectionCSSClass: sf_paymentSettingsField sf_bitPayStandardPaymentSettingsField sfSectionInSection sfDisplayNone
SettingsType: SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Models.BitPaySettings, SitefinityWebApp
ViewProviderType: SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField, SitefinityWebApp
ProviderType: SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Processors.BitPayProvider, SitefinityWebApp


5) Create a BitPay payment method:

Ecommerce / Payment Methods / Create payment method

Name: {Some name you find informative, for example: BitPay}
PaymentProcessor: BitPay
Create invoice URL: https://bitpay.com/api/invoice
Get invoice URL: https://bitpay.com/invoice
Api Key: {Your API key provided by BitPay}
Transaction speed: {
	The transaction speed you find comfortable: 
	- High - confirmation on transaction registration in the Bitcoin network;
	- Medium - confirmation after the transaction is included in 1 block;
	- Low - confirmation after the transaction is included in 6 blocks;
}
Notification e-mail: {The e-mail on which you wish to receive confirmation for the transactions completion}


NOTES: You will need to have BitPay account from which you will get your API key.