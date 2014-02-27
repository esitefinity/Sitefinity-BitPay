using SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Models;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Ecommerce.Payment.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Payment;
using Telerik.Sitefinity.Modules.Ecommerce.Payment.Helpers;
using Telerik.Sitefinity.Utilities.OpenPOP.POP3;

namespace SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Processors
{
    /// <summary>
    /// This class is the PayPal Standard payment processor implementation of <see cref="IPaymentProcessorProvider"/>.
    /// </summary>
    public class BitPayProvider : PaymentProcessorProviderBase, IOffsitePaymentProcessorProvider
    {
        #region fields and constants
        protected BitPaySettings bitPaySettings;

        private const string TheBitPayStatusConfirmed = "confirmed";
        private const string TheBitPayStatusComplete = "complete";
        #endregion

        #region Properties
        public int Timeout
        {
            get
            {
                return this.bitPaySettings.Timeout > 0 ? int.Parse(this.bitPaySettings.Timeout.ToString()) : PaymentProcessorProviderBase.DefaultWebRequestTimeout;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Handles the offsite notification send by them via async call.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="request">The HTTP request object .</param>
        /// <param name="paymentMethod">The payment method which is used.</param>
        /// <returns></returns>
        public IPaymentResponse HandleOffsiteNotification(int orderNumber, HttpRequest request, PaymentMethod paymentMethod)
        {
            IPaymentResponse response = new PaymentResponse() { IsOffsitePayment = true };

            //Extract the BitPay settings object
            this.bitPaySettings = (BitPaySettings)PaymentProcessorHelper.DeserializePaymentSettings<BitPaySettings>(paymentMethod);

            BitPayServerResponse bitpayServerResponce = GetBitPayServiceResponce(request);

            bool isNotificationValid = CheckIfNotificationIsValid(orderNumber, bitpayServerResponce);

            if (!isNotificationValid)
            {
                response.GatewayFraudResponse = "BitPay Payment response not valid for this order.";
                return response;
            }

            var paymentStatus = bitpayServerResponce.Status;
            if (!paymentStatus.IsNullOrEmpty())
            {
                //check for successful
                if (paymentStatus == TheBitPayStatusConfirmed || paymentStatus == TheBitPayStatusComplete)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        /// <summary>
        /// Handles the offsite response after redirect back to our site.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="request">The HTTP request object .</param>
        /// <param name="paymentMethod">The current payment method in use.</param>
        /// <returns></returns>
        public IPaymentResponse HandleOffsiteReturn(int orderNumber, HttpRequest request, PaymentMethod paymentMethod)
        {
            // No processing needed for return (BitPay redirect back to Sitefinity)
            IPaymentResponse response = new PaymentResponse() { IsOffsitePayment = true };
            return response;
        }

        /// <summary>
        /// Submits a credit request to the payment processor
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse Credit(IPaymentRequest data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a sale request to the payment processor
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse Sale(IPaymentRequest data)
        {
            BitPayServerResponse bitpayServerResponce = CreateInvoiceRequest(data);

            NameValueCollection createPaymentPostValues = new NameValueCollection();
            createPaymentPostValues.Add("id", bitpayServerResponce.Id);

            IPaymentResponse response = CreatePaymentResponse(data.OrderNumber, createPaymentPostValues);
            return response;
        }

        /// <summary>
        /// Submits a capture request to the payment processor
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <param name="originalTransactionID"></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse Capture(IPaymentRequest data, string originalTransactionID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a void request to the payment processor
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <param name="originalTransactionID"></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse Void(IPaymentRequest data, string originalTransactionID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a auth void and capture request to the payment processor
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse AuthVoidCapture(IPaymentRequest data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs validations on payment processor
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <param name="avsAddress"></param>
        /// <param name="avsZip"></param>
        /// <param name="csc"></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse PerformValidations(IPaymentRequest data, bool avsAddress, bool avsZip, bool csc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the connection between merchant and payment processor
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse TestConnection(IPaymentRequest data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generic Submit Transaction method which will called by the processor. All other transactions that can happen with the provider have to be called from this method.
        /// </summary>
        /// <param name="data">An instance of type <see cref="IPaymentRequest" /></param>
        /// <returns>An instance of type <see cref="IPaymentResponse" /></returns>
        public IPaymentResponse SubmitTransaction(IPaymentRequest data)
        {
            this.bitPaySettings = base.GetDeserializedSettings<BitPaySettings>(data.PaymentProcessorSettings);

            return Sale(data);
        }

        public IPaymentResponse Authorize(IPaymentRequest data)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Write a string message to the Sitefinity Log
        /// </summary>
        /// <param name="message">Message string to be logged</param>
        protected virtual void LogMessage(string message)
        {
            if (this.bitPaySettings != null)
                base.LogMessage(message, this.bitPaySettings.ApiKey);
        }

        /// <summary>
        /// Return the data from the request object (data), into a NameValueCollection
        /// which can then be encoded into a byte array and posted to the processor.
        /// </summary>
        /// <param name="data">Request data</param>
        /// <returns>NameValue collection of the parameter data</returns>
        protected virtual NameValueCollection GetPostValues(IPaymentRequest data)
        {
            NameValueCollection postValues = new NameValueCollection();

            // Required POST fields
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = "";
            var amount = data.Amount.ToString("N2", nfi);

            postValues.Add("price", amount);
            postValues.Add("currency", data.CurrencyCode);

            // Optional Payment Notification fields

            // We generate a hash code which will be passed in the posData property and will later be used to confirm that 
            // the status notifications are actually coming from BitPay
            // To generate the hash we concatenate the merchant API key, the order number and the selected transaction speed and we generate a MD5 hash for that string
            // This is only an example of how such a hash can be generated. It is up to the developer to decide what data, hash algorithm and salting to use
            string orderHash = MD5.ComputeHashHex(
                    String.Format("{0}:{1}:{2}", this.bitPaySettings.ApiKey, data.OrderNumber.ToString(), this.bitPaySettings.TransactionSpeed)
                    );

            postValues.Add("posData", orderHash);
            postValues.Add("notificationURL", data.GatewayNotificationUrl);
            postValues.Add("transactionSpeed", this.bitPaySettings.TransactionSpeed);

            if (!string.IsNullOrEmpty(this.bitPaySettings.NotificationEmail))
                postValues.Add("notificationEmail", this.bitPaySettings.NotificationEmail);

            // Optional Order Handling fields
            postValues.Add("redirectURL", data.GatewayRedirectSuccessUrl);

            // Optional Buyer Information to display
            postValues.Add("orderID", data.OrderNumber.ToString());

            var order = OrdersManager.GetManager().GetOrder(data.OrderID);
            if (order != null)
            {
                bool isShippable = false;
                StringBuilder itemDesc = new StringBuilder();
                for (int i = 0; i < order.Details.Count; i++)
                {
                    OrderDetail detail = order.Details[i];

                    if (detail.IsShippable && detail.Quantity > 0)
                        isShippable = true;

                    if (itemDesc.Length + 2 + detail.Title.Length < 100)
                        itemDesc.Append(detail.Title + "; ");
                }

                // We remove the last "; "
                if (itemDesc.Length > 0)
                    itemDesc.Length -= 2;

                postValues.Add("itemDesc", itemDesc.ToString());
                postValues.Add("physical", isShippable.ToString().ToLower());
            }

            string name = GetBuyerName(data);
            if (!name.IsNullOrWhitespace())
            {
                postValues.Add("buyerName", name);
            }

            //shipping & billing information
            AddPostValue("buyerAddress1", data.ShippingStreet, data.BillingStreet, postValues);
            AddPostValue("buyerAddress2", data.ShippingStreet2, data.BillingStreet2, postValues);
            AddPostValue("buyerCity", data.ShippingCity, data.BillingCity, postValues);
            AddPostValue("buyerState", data.ShippingState, data.BillingState, postValues);
            AddPostValue("buyerZip", data.ShippingZip, data.BillingZip, postValues);
            AddPostValue("buyerCountry", data.ShippingCountry, data.BillingCountry, postValues);
            postValues.Add("buyerEmail", data.CustomerEmail);
            AddPostValue("buyerPhone", data.ShippingPhone, data.BillingPhone, postValues);

            LogMessage("PostValues obtained for OrderID " + data.OrderID.ToString());

            return postValues;
        }

        #endregion

        #region private method

        /// <summary>
        /// Verifies if the order status changed notification is valid
        /// </summary>
        private bool CheckIfNotificationIsValid(int orderNumber, BitPayServerResponse bitpayServerResponce)
        {
            if (bitpayServerResponce == null)
                return false;

            // We check if the hash stored in the posData propery of the BitPay responce is valid
            // We use this hash to confirm that the Notification is actually coming from BitPay
            // In order to check the hash we regenerate it the same way it was generated earlier in the Invoice request
            string orderHash = MD5.ComputeHashHex(
                String.Format("{0}:{1}:{2}", this.bitPaySettings.ApiKey, orderNumber.ToString(), this.bitPaySettings.TransactionSpeed));

            var posData = bitpayServerResponce.PosData;
            if (posData.IsNullOrEmpty() || posData != orderHash)
                return false;

            return true;
        }

        private static BitPayServerResponse GetBitPayServiceResponce(HttpRequest request)
        {
            string jsonString = String.Empty;

            HttpContext.Current.Request.InputStream.Position = 0;
            using (StreamReader inputStream = new StreamReader(request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            BitPayServerResponse bitpayServerResponce = ParseBitPayServerResponce(jsonString);
            return bitpayServerResponce;
        }

        /// <summary>
        /// Creates the payment response.
        /// </summary>
        private IPaymentResponse CreatePaymentResponse(int orderNumber, NameValueCollection postValues)
        {
            var paymentResponse = new PaymentResponse();
            paymentResponse.OrderNumber = orderNumber;
            paymentResponse.GatewayRedirectUrl = bitPaySettings.GetInvoiceUrl;
            paymentResponse.GatewayRedirectUrlPostValues = postValues;
            paymentResponse.GatewayRedirectMethod = "GET";
            paymentResponse.IsOffsitePayment = true;
            return paymentResponse;
        }

        /// <summary>
        /// Gets the buyers name
        /// </summary>
        private string GetBuyerName(IPaymentRequest data)
        {
            StringBuilder name = new StringBuilder();
            if (!data.ShippingFirstName.IsNullOrWhitespace())
            {
                name.Append(data.ShippingFirstName);
            }

            if (!data.ShippingLastName.IsNullOrWhitespace())
            {
                if (name.Length > 0)
                    name.Append(" ");

                name.Append(data.ShippingLastName);
            }

            if (name.Length == 0)
            {
                if (!data.BillingFirstName.IsNullOrWhitespace())
                {
                    name.Append(data.BillingFirstName);
                }

                if (!data.BillingLastName.IsNullOrWhitespace())
                {
                    if (name.Length > 0)
                        name.Append(" ");

                    name.Append(data.BillingLastName);
                }
            }

            return name.ToString();
        }

        /// <summary>
        /// Adds to a NameValueCollection a specific property from the shipping or billing data
        /// </summary>
        private void AddPostValue(string paramName, string shippingValue, string billingValue, NameValueCollection postValues)
        {
            if (!shippingValue.IsNullOrWhitespace())
            {
                postValues.Add(paramName, shippingValue);
            }
            else
            {
                postValues.Add(paramName, billingValue);
            }
        }

        /// <summary>
        /// Makes an Create invoice request and returns the BitPay server response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private BitPayServerResponse CreateInvoiceRequest(IPaymentRequest data)
        {
            NameValueCollection headerValues = new NameValueCollection();
            byte[] encodedByteApiKey = System.Text.ASCIIEncoding.ASCII.GetBytes(this.bitPaySettings.ApiKey);
            string base64EncodedApiKey = Convert.ToBase64String(encodedByteApiKey);
            headerValues.Add("Authorization", "Basic " + base64EncodedApiKey);

            NameValueCollection postValues = GetPostValues(data);
            string responseJson = base.PostWebRequest(this.bitPaySettings.CreateInvoiceUrl, headerValues, postValues, Timeout);

            BitPayServerResponse bitpayServerResponce = ParseBitPayServerResponce(responseJson);

            return bitpayServerResponce;
        }

        private static BitPayServerResponse ParseBitPayServerResponce(string responseJson)
        {
            BitPayServerResponse bitpayServerResponce = null;
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.GetEncoding("utf-8").GetBytes(responseJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(BitPayServerResponse));
                bitpayServerResponce = (BitPayServerResponse)serializer.ReadObject(stream);
            }
            return bitpayServerResponce;
        }

        #endregion

        #region Inner classes

        [DataContract]        
        private class BitPayServerResponse
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "url")]
            public string Url { get; set; }

            [DataMember(Name = "posData")]
            public string PosData { get; set; }

            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "price")]
            public decimal Price { get; set; }

            [DataMember(Name = "currency")]
            public string Currency { get; set; }

            [DataMember(Name = "btcPrice")]
            public decimal BtcPrice { get; set; }

            // The date is as a UnixTimeStamp in milliseconds
            // Can be turned into DateTime like this: 
            // System.DateTime dtDateTime = new System.DateTime(1970,1,1,0,0,0,0);
            // dtDateTime1.AddSeconds( bitpayServerResponce.InvoiceTime / 1000 ).ToLocalTime();
            [DataMember(Name = "invoiceTime")]
            public long InvoiceTime { get; set; }

            [DataMember(Name = "expirationTime")]
            public long ExpirationTime { get; set; }

            [DataMember(Name = "currentTime")]
            public long CurrentTime { get; set; }
        }
        #endregion
    }
}
