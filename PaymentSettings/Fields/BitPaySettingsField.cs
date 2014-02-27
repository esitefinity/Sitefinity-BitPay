using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields
{
    class BitPaySettingsField : FieldControl
    {
        #region Constructor

        public BitPaySettingsField()
        {
            this.LayoutTemplatePath = BitPaySettingsField.layoutTemplatePath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            ((ITextControl)this.TitleControl).Text = this.Title;
            ((ITextControl)this.ExampleControl).Text = this.Example;
            ((ITextControl)this.DescriptionControl).Text = this.Description;

            this.PaymentMethodIdHidden.Value = this.Value.ToString();
        }

        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return BitPaySettingsField.layoutTemplateName;
            }
        }

        /// <summary>
        /// Gets the reference to the control that represents the title of the field control.
        /// Return null if no such control exists in the template.
        /// </summary>
        /// <value>
        /// The control displaying the title of the field.
        /// </value>
        protected override WebControl TitleControl
        {
            get
            {
                return this.Container.GetControl<Label>("titleLabel", true);
            }
        }

        /// <summary>
        /// Gets the reference to the control that represents the description of the field control.
        /// Return null if no such control exists in the template.
        /// </summary>
        /// <value>
        /// The control displaying the description of the field.
        /// </value>
        protected override WebControl DescriptionControl
        {
            get
            {
                return this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);
            }
        }

        /// <summary>
        /// Gets the reference to the control that represents the example of the field control.
        /// Return null if no such control exists in the template.
        /// </summary>
        /// <value>
        /// The control displaying the sample usage of the field.
        /// </value>
        protected override WebControl ExampleControl
        {
            get
            {
                return this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);
            }
        }

        /// <summary>
        /// The payment method ID
        /// </summary>
        public override object Value
        {
            get
            {
                if (base.Value == null)
                    return Guid.Empty;
                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        /// <summary>
        /// The payment method ID
        /// </summary>
        protected Guid PaymentMethodId
        {
            get
            {
                return (this.Value == null) ? Guid.Empty : (Guid)this.Value;
            }
            set
            {
                this.Value = value;
            }
        }

        /// <summary>
        /// Gets the reference to the hidden field that holds the payment method ID.
        /// </summary>
        protected virtual HiddenField PaymentMethodIdHidden
        {
            get
            {
                return this.Container.GetControl<HiddenField>("paymentMethodId", this.DisplayMode == FieldDisplayMode.Write);
            }
        }

        /// <summary>
        /// Gets the reference to the control that holds the timeout of the payment processor.
        /// </summary>
        protected virtual TextField Timeout
        {
            get
            {
                return this.Container.GetControl<TextField>("timeout", true);
            }
        }

        /// <summary>
        /// Gets the reference to the control that holds the payment type of the payment processor.
        /// </summary>
        protected virtual ChoiceField PaymentType
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("paymentType", true);
            }
        }

        /// <summary>
        /// Gets the reference to the control that holds the api key for the BitPay account.
        /// </summary>
        protected virtual TextField ApiKeyControl
        {
            get
            {
                return this.Container.GetControl<TextField>("apiKey", true);
            }
        }

        /// <summary>
        /// Gets the reference to the control that holds the create invoice url of the payment processor.
        /// </summary>
        protected virtual TextField CreateInvoiceUrlControl
        {
            get
            {
                return this.Container.GetControl<TextField>("createInvoiceUrl", true);
            }
        }

        /// <summary>
        /// Gets the reference to the control that holds the get invoice url of the payment processor.
        /// </summary>
        protected virtual TextField GetInvoiceUrlControl
        {
            get
            {
                return this.Container.GetControl<TextField>("getInvoiceUrl", true);
            }
        }

        /// <summary>
        /// Gets the reference to the control that holds the transaction speed for payment confirmation (An indicator how much should BitPay wait until it sends transaction confirmation notice - high, medium, low).
        /// </summary>
        protected virtual ChoiceField TransactionSpeedControl
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("transactionSpeed", true);
            }
        }

        /// <summary>
        /// Gets the reference to the control that holds the notification e-mail to which the payment processor will send invoice status change notifications.
        /// </summary>
        protected virtual TextField NotificationEmailControl
        {
            get
            {
                return this.Container.GetControl<TextField>("notificationEmail", true);
            }
        }

        #endregion
        
        #region Public methods

        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of <see cref="T:System.Web.UI.ScriptDescriptor"/> objects.
        /// </returns>
        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)descriptors.Last();

            descriptor.AddComponentProperty("apiKeyControl", this.ApiKeyControl.ClientID);
            descriptor.AddComponentProperty("createInvoiceUrlControl", this.CreateInvoiceUrlControl.ClientID);
            descriptor.AddComponentProperty("getInvoiceUrlControl", this.GetInvoiceUrlControl.ClientID);
            descriptor.AddComponentProperty("transactionSpeedControl", this.TransactionSpeedControl.ClientID);
            descriptor.AddComponentProperty("notificationEmailControl", this.NotificationEmailControl.ClientID);

            descriptor.AddComponentProperty("timeoutControl", this.Timeout.ClientID);

            descriptor.AddComponentProperty("paymentTypeControl", this.PaymentType.ClientID);

            return descriptors;
        }

        /// <summary>
        /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference"/> objects that define script resources that the control requires.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection of <see cref="T:System.Web.UI.ScriptReference"/> objects.
        /// </returns>
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(BitPaySettingsField.scriptReference, typeof(BitPaySettingsField).Assembly.FullName));
            return scripts;
        }

        #endregion

        #region Private fields and constants

        internal const string scriptReference = "SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.js";
        private const string layoutTemplateName = "SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.ascx";
        internal static readonly string layoutTemplatePath = "~/Sitefinity-BitPay/PaymentSettings/Fields/BitPaySettingsField.ascx";

        #endregion
    }
}
