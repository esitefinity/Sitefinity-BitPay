Type.registerNamespace("SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields");

SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField = function (element) {
    SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.initializeBase(this, [element]);
    this._element = element;

    this._apiKeyControl = null;
    this._createInvoiceUrlControl = null;
    this._getInvoiceUrlControl = null;
    this._transactionSpeedControl = null;
    this._notificationEmailControl = null;

    this._timeoutControl = null;
    this._paymentTypeControl = null;

    this._state = null;
    this._appLoadedDelegate = null;
    this._appLoaded = false;
};

SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.prototype =
{

    initialize: function () {
        SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.callBaseMethod(this, "initialize");

        if (this._appLoadedDelegate === null) {
            this._appLoadedDelegate = Function.createDelegate(this, this._applicationLoaded);
        }

        Sys.Application.add_load(this._appLoadedDelegate);
    },

    dispose: function () {
        SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.callBaseMethod(this, "dispose");

        if (this._appLoadedDelegate) {
            Sys.Application.remove_load(this._appLoadedDelegate);
            delete this._appLoadedDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    // this method is called by definitions engine just before saving. It should return true
    // if value is valid; otherwise false. As our field control is composed of several other
    // field controls, we will validate them (remember, both this control and child controls
    // inherit base FieldControl JS component.
    validate: function () {
        if (!$(this.get_element()).is(':visible')) {
            return true;
        }

        // if only one of the child field controls fails, this (parent/container) field control
        // should fail as well
        var isValid = true;

        if (!this.get_timeoutControl().validate()) {
            isValid = false;
        }

        // here you can also perform other validations that could depend
        // on the values of multiple fields, so you have a very extensible
        // validation mechanism

        if (!this.get_createInvoiceUrlControl().validate()) {
            isValid = false;
        }

        if (!this.get_getInvoiceUrlControl().validate()) {
            isValid = false;
        }

        if (!this.get_apiKeyControl().validate()) {
            isValid = false;
        }

        // we return the aggregate result of all our validations
        return isValid;
    },

    reset: function () {
        // Setup default values
        this._state = {
            'ApiKey': '',
            'CreateInvoiceUrl': 'https://bitpay.com/api/invoice',
            'GetInvoiceUrl': 'https://bitpay.com/invoice',
            'Timeout': '30000',
            'PaymentType': ''
        };
    },

    // Gets the value of the field control.
    get_value: function () {
        return Sys.Serialization.JavaScriptSerializer.serialize(this._getSettingsObject());
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        if (value !== null && value.length > 0) {
            var settingsObject = Sys.Serialization.JavaScriptSerializer.deserialize(value);
            this._state = settingsObject;
        }

        // as application load event happens only once during the lifetime of the page, thus
        // it won't happen the next time we open the detail form for creating or updating
        // payment methods, we need to implement dual handling for updating user interface.
        // a) at the very first time we have to assume that not all components have been
        // loaded, so we are persisting data in the state and relying on the applicationLoaded
        // handler to call the _updateUI method
        // b) on subsequent usage of the dialog, we now that controls have been ready and application
        // load event wont' be fired again (as it was loaded), therefore we updated the ui from the
        // set_value method. 
        // p.s. our script combining / compressing mechanism will remove this lengthy comment for the
        // release build, so don't worry commenting scripts extensively - it does not slow down
        // the application
        if (this._appLoaded) {
            this._updateUI();
        }
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
    },



    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _applicationLoaded: function (sender, agrs) {
        this._appLoaded = true;
        this._updateUI();
    },


    /* -------------------- private methods -------------------- */

    // this method updates the user interface with the values from the state
    _updateUI: function () {
        this.get_apiKeyControl().set_value(this._state.ApiKey);
        this.get_createInvoiceUrlControl().set_value(this._state.CreateInvoiceUrl);
        this.get_getInvoiceUrlControl().set_value(this._state.GetInvoiceUrl);
        this.get_transactionSpeedControl().set_value(this._state.TransactionSpeed);
        this.get_notificationEmailControl().set_value(this._state.NotificationEmail);

        this.get_timeoutControl().set_value(this._state.Timeout);

        this.get_paymentTypeControl().set_value(this._state.PaymentType);
    },

    _getSettingsObject: function () {

        var settings = {
            'ApiKey': this.get_apiKeyControl().get_value(),
            'CreateInvoiceUrl': this.get_createInvoiceUrlControl().get_value(),
            'GetInvoiceUrl': this.get_getInvoiceUrlControl().get_value(),
            'TransactionSpeed': this.get_transactionSpeedControl().get_value(),
            'NotificationEmail': this.get_notificationEmailControl().get_value(),
            'Timeout': this.get_timeoutControl().get_value(),
            'PaymentType': this.get_paymentTypeControl().get_value()
        };
        return settings;
    },

    // a more compact version
    _isArray: function (obj) {
        return (obj.constructor.toString().indexOf('Array') != -1);
    },

    /* -------------------- properties ---------------- */

    // gets the reference to the control that holds the timeout of the payment processor
    get_timeoutControl: function () {
        return this._timeoutControl;
    },
    // sets the reference to the control that holds the timeout of the payment processor
    set_timeoutControl: function (value) {
        this._timeoutControl = value;
    },

    // gets the reference to the control that holds the payment type of the payment processor
    get_paymentTypeControl: function () {
        return this._paymentTypeControl;
    },
    // sets the reference to the control that holds the payment type of the payment processor
    set_paymentTypeControl: function (value) {
        this._paymentTypeControl = value;
    },

    // BitPay specific properties

    // gets the reference to the control that holds the create invoice url of the payment processor
    get_createInvoiceUrlControl: function () {
        return this._createInvoiceUrlControl;
    },
    // sets the reference to the control that holds the create invoice url of the payment processor
    set_createInvoiceUrlControl: function (value) {
        this._createInvoiceUrlControl = value;
    },

    // gets the reference to the control that holds the get invoice url of the payment processor
    get_getInvoiceUrlControl: function () {
        return this._getInvoiceUrlControl;
    },
    // sets the reference to the control that holds the get invoice url of the payment processor
    set_getInvoiceUrlControl: function (value) {
        this._getInvoiceUrlControl = value;
    },

    // gets the reference to the control that holds the transaction speed for payment confirmation (An indicator how much should BitPay wait until it sends transaction confirmation notice - high, medium, low).
    get_transactionSpeedControl: function () {
        return this._transactionSpeedControl;
    },
    // sets the reference to the control that holds the transaction speed for payment confirmation (An indicator how much should BitPay wait until it sends transaction confirmation notice - high, medium, low).
    set_transactionSpeedControl: function (value) {
        this._transactionSpeedControl = value;
    },

    // gets the reference to the control that holds the notification e-mail to which the payment processor will send invoice status change notifications.
    get_notificationEmailControl: function () {
        return this._notificationEmailControl;
    },
    // sets the reference to the control that holds the notification e-mail to which the payment processor will send invoice status change notifications.
    set_notificationEmailControl: function (value) {
        this._notificationEmailControl = value;
    },

    // gets the reference to the control that holds the the key for the BitPay account
    get_apiKeyControl: function () {
        return this._apiKeyControl;
    },
    // sets the reference to the control that holds the api key for the BitPay account
    set_apiKeyControl: function (value) {
        this._apiKeyControl = value;
    }

};

SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField.registerClass("SitefinityWebApp.Sitefinity_BitPay.PaymentSettings.Fields.BitPaySettingsField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
