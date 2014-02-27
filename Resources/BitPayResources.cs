using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace SitefinityWebApp.Sitefinity_BitPay.Resources
{
    /// <summary>
    /// Localization class which holds localizable lables for the BitPay module.
    /// </summary>
    public class BitPayResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes new instance of <see cref="BitPayResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public BitPayResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="BitPayResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public BitPayResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// phrase: Create invoice URL
        /// </summary>
        /// <value>Create invoice URL</value>
        [ResourceEntry("CreateInvoiceUrl",
            Value = "Create invoice URL",
            Description = "phrase: Create invoice URL",
            LastModified = "2014/02/04")]
        public string CreateInvoiceUrl
        {
            get
            {
                return this["CreateInvoiceUrl"];
            }
        }

        /// <summary>
        /// phrase: Get invoice info URL
        /// </summary>
        /// <value>Get invoice info URL</value>
        [ResourceEntry("GetInvoiceInfoUrl",
            Value = "Get invoice info URL",
            Description = "phrase: Get invoice info URL",
            LastModified = "2014/02/04")]
        public string GetInvoiceInfoUrl
        {
            get
            {
                return this["GetInvoiceInfoUrl"];
            }
        }

        /// <summary>
        /// The payment method create invoice URL is required
        /// </summary>
        /// <value>The payment method create invoice URL is required</value>
        [ResourceEntry("CreateInvoiceUrlRequired",
            Value = "The payment method create invoice URL is required",
            Description = "The payment method create invoice URL is required",
            LastModified = "2014/02/04")]
        public string CreateInvoiceUrlRequired
        {
            get
            {
                return this["CreateInvoiceUrlRequired"];
            }
        }

        /// <summary>
        /// phrase: Get invoice URL
        /// </summary>
        /// <value>Get invoice URL</value>
        [ResourceEntry("GetInvoiceUrl",
            Value = "Get invoice URL",
            Description = "phrase: Get invoice URL",
            LastModified = "2014/02/04")]
        public string GetInvoiceUrl
        {
            get
            {
                return this["GetInvoiceUrl"];
            }
        }

        /// <summary>
        /// The payment method get invoice URL is required
        /// </summary>
        /// <value>The payment method get invoice URL is required</value>
        [ResourceEntry("GetInvoiceUrlRequired",
            Value = "The payment method get invoice URL is required",
            Description = "The payment method get invoice URL is required",
            LastModified = "2014/02/04")]
        public string GetInvoiceUrlRequired
        {
            get
            {
                return this["GetInvoiceUrlRequired"];
            }
        }

        /// <summary>
        /// phrase: Api key
        /// </summary>
        /// <value>Api key</value>
        [ResourceEntry("ApiKey",
            Value = "Api key",
            Description = "phrase: Api key",
            LastModified = "2014/02/04")]
        public string ApiKey
        {
            get
            {
                return this["ApiKey"];
            }
        }

        /// <summary>
        /// The payment method Api key is required
        /// </summary>
        /// <value>The payment method Api key is required</value>
        [ResourceEntry("ApiKeyRequired",
            Value = "The payment method Api key is required",
            Description = "The payment method Api key is required",
            LastModified = "2014/02/04")]
        public string ApiKeyRequired
        {
            get
            {
                return this["ApiKeyRequired"];
            }
        }

        /// <summary>
        /// phrase: Transacion speed
        /// </summary>
        /// <value>Transaction speed</value>
        [ResourceEntry("PaymentMethodTransactionSpeed",
            Value = "Transaction speed",
            Description = "phrase: Transacion speed",
            LastModified = "2014/02/04")]
        public string PaymentMethodTransactionSpeed
        {
            get
            {
                return this["PaymentMethodTransactionSpeed"];
            }
        }

        /// <summary>
        /// phrase: High
        /// </summary>
        /// <value>High</value>
        [ResourceEntry("High",
            Value = "High",
            Description = "phrase: High",
            LastModified = "2014/02/04")]
        public string High
        {
            get
            {
                return this["High"];
            }
        }

        /// <summary>
        /// phrase: Medium
        /// </summary>
        /// <value>Medium</value>
        [ResourceEntry("Medium",
            Value = "Medium",
            Description = "phrase: Medium",
            LastModified = "2014/02/04")]
        public string Medium
        {
            get
            {
                return this["Medium"];
            }
        }

        /// <summary>
        /// phrase: Low
        /// </summary>
        /// <value>Low</value>
        [ResourceEntry("Low",
            Value = "Low",
            Description = "phrase: Low",
            LastModified = "2014/02/04")]
        public string Low
        {
            get
            {
                return this["Low"];
            }
        }

        /// <summary>
        /// phrase: Notification email
        /// </summary>
        /// <value>Notification email</value>
        [ResourceEntry("NotificationEmail",
            Value = "Notification email",
            Description = "phrase: Notification email",
            LastModified = "2014/02/04")]
        public string NotificationEmail
        {
            get
            {
                return this["NotificationEmail"];
            }
        }
    }
}