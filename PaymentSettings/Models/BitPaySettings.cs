using System;
using System.Linq;
using Telerik.Sitefinity.Ecommerce.Payment.Model;

namespace SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Models
{
    // BitPay Api documentation: https://bitpay.com/downloads/bitpayApi.pdf
    public class BitPaySettings : IPaymentSettings
    {
        /// <summary>
        /// User entered URL where the create invoice request is send (provided by the payment processor)
        /// </summary>
        public string CreateInvoiceUrl
        {
            get;
            set;
        }
        /// <summary>
        /// User entered URL where the get invoice request is send (provided by the payment processor)
        /// </summary>
        public string GetInvoiceUrl
        {
            get;
            set;
        }

        /// <summary>
        /// The API key that indentifies the merchant in front of BitPay (provided by the payment processor)
        /// </summary>
        public string ApiKey
        {
            get;
            set;
        }

        /// <summary>
        /// An indicator how much should BitPay wait until it sends transaction confirmation notice. Possible values:
        /// - "high" - An invoice is considered to be "confirmed" immediately upon receipt of payment.
        /// - "medium" - An invoice is considered to be "confirmed" after 1 block confirmation (~10 minutes).
        /// - "low" - An invoice is considered to be "confirmed" after 6 block confirmations (~1 hour).
        /// </summary>
        public string TransactionSpeed
        {
            get;
            set;
        }

        /// <summary>
        /// This is the e-mail to which BitPay will send notification message when the invoice status is changed
        /// </summary>
        public string NotificationEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Timeout (in milliseconds) for post to payment processor
        /// </summary>
        public double Timeout
        {
            get;
            set;
        }

        /// <summary>
        /// Array of strings that are names of credit cards (lower case) processed by the payment processor
        /// </summary>
        public string[] ProcessorCreditCards
        {
            get;
            set;
        }

        /// <summary>
        /// Payment type (sale, auth, capture, auth and capture, etc)
        /// </summary>
        public string PaymentType
        {
            get;
            set;
        }
    }
}
