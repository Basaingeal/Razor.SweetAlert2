namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System.Collections.Generic;

    public class SweetAlertOptions
    {
        /// <summary>
        /// The title of the modal, as HTML.
        /// <para>It can either be added to the object under the key "title" or passed as the first parameter of the function.</para>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The title of the modal, as text. Useful to avoid HTML injection.
        /// </summary>
        public string TitleText { get; set; }

        /// <summary>
        /// A description for the modal.
        /// <para>It can either be added to the object under the key "text" or passed as the second parameter of the function.</para>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// A HTML description for the modal.
        /// <para>If "text" and "html" parameters are provided in the same time, "text" will be used.</para>
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// The footer of the modal, as HTML.
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// The icon of the modal.
        /// <para>SweetAlert2 comes with 5 built-in icons which will show a corresponding icon animation: 'warning', 'error', 'success', 'info' and 'question'.</para>
        /// <para>It can either be put in the array under the key "icon" or passed as the third parameter of the function.</para>
        /// </summary>
        public SweetAlertIcon Icon { get; set; }

        /// <summary>
        /// Whether or not SweetAlert2 should show a full screen click-to-dismiss backdrop.
        /// </summary>
        public bool? Backdrop { get; set; }

        /// <summary>
        /// Whether or not an alert should be treated as a toast notification.
        /// <para>This option is normally coupled with the `position` parameter and a timer.</para>
        /// <para>Toasts are NEVER autofocused.</para>
        /// </summary>
        public bool? Toast { get; set; }

        /// <summary>
        /// The container element for adding modal into (query selector only).
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Input field type, can be text, email, password, number, tel, range, textarea, select, radio, checkbox, file and url.
        /// </summary>
        public SweetAlertInputType Input { get; set; }

        /// <summary>
        /// Modal window width, including paddings (box-sizing: border-box). Can be in px or %.
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// Modal window padding.
        /// </summary>
        public string Padding { get; set; }

        /// <summary>
        /// Modal window background (CSS background property).
        /// </summary>
        public string Background { get; set; }

        /// <summary>
        /// Modal window position.
        /// </summary>
        public SweetAlertPosition Position { get; set; }

        /// <summary>
        /// Modal window grow direction.
        /// </summary>
        public SweetAlertGrowDirection Grow { get; set; }

        /// <summary>
        /// CSS classes for animations when showing a popup (fade in)
        /// </summary>
        public SweetAlertShowClass ShowClass { get; set; }

        // <summary>
        /// CSS classes for animations when hiding a popup (fade out)
        /// </summary>
        public SweetAlertHideClass HideClass { get; set; }

        /// <summary>
        /// A custom CSS class for the modal.
        /// <para>If a string value is provided, the classname will be applied to the popup.</para>
        /// <para></para>
        /// </summary>
        public SweetAlertCustomClass CustomClass { get; set; }

        /// <summary>
        /// Auto close timer of the modal. Set in ms (milliseconds).
        /// </summary>
        public decimal? Timer { get; set; }

        /// <summary>
        /// By default, SweetAlert2 sets html's and body's CSS height to auto !important.
        /// <para>If this behavior isn't compatible with your project's layout, set heightAuto to false.</para>
        /// </summary>
        public bool? HeightAuto { get; set; }

        /// <summary>
        /// If set to false, the user can't dismiss the modal by clicking outside it.
        /// </summary>
        public bool? AllowOutsideClick { get; set; }

        /// <summary>
        /// If set to false, the user can't dismiss the modal by pressing the Escape key.
        /// </summary>
        public bool? AllowEscapeKey { get; set; }

        /// <summary>
        /// If set to false, the user can't confirm the modal by pressing the Enter or Space keys, unless they manually focus the confirm button.
        /// </summary>
        public bool? AllowEnterKey { get; set; }

        /// <summary>
        /// If set to false, SweetAlert2 will allow keydown events propagation to the document.
        /// </summary>
        public bool? StopKeydownPropagation { get; set; }

        /// <summary>
        /// Useful for those who are using SweetAlert2 along with Bootstrap modals.
        /// <para>By default keydownListenerCapture is false which means when a user hits Esc, both SweetAlert2 and Bootstrap modals will be closed.</para>
        /// <para>Set keydownListenerCapture to true to fix that behavior.</para>
        /// </summary>
        public bool? KeydownListenerCapture { get; set; }

        /// <summary>
        /// If set to false, a "Confirm"-button will not be shown.
        /// <para>It can be useful when you're using custom HTML description.</para>
        /// </summary>
        public bool? ShowConfirmButton { get; set; }

        /// <summary>
        /// If set to true, a "Cancel"-button will be shown, which the user can click on to dismiss the modal.
        /// </summary>
        public bool? ShowCancelButton { get; set; }

        /// <summary>
        /// Use this to change the text on the "Confirm"-button.
        /// </summary>
        public string ConfirmButtonText { get; set; }

        /// <summary>
        /// Use this to change the text on the "Cancel"-button.
        /// </summary>
        public string CancelButtonText { get; set; }

        /// <summary>
        /// Use this to change the background color of the "Confirm"-button (must be a HEX value).
        /// </summary>
        public string ConfirmButtonColor { get; set; }

        /// <summary>
        /// Use this to change the background color of the "Cancel"-button (must be a HEX value).
        /// </summary>
        public string CancelButtonColor { get; set; }

        /// <summary>
        /// Use this to change the aria-label for the "Confirm"-button.
        /// </summary>
        public string ConfirmButtonAriaLabel { get; set; }

        /// <summary>
        /// Use this to change the aria-label for the "Cancel"-button.
        /// </summary>
        public string CancelButtonAriaLabel { get; set; }

        /// <summary>
        /// Whether to apply the default SweetAlert2 styling to buttons.
        /// <para>If you want to use your own classes (e.g. Bootstrap classes) set this parameter to false.</para>
        /// </summary>
        public bool? ButtonsStyling { get; set; }

        /// <summary>
        /// Set to true if you want to invert default buttons positions.
        /// </summary>
        public bool? ReverseButtons { get; set; }

        /// <summary>
        /// Set to false if you want to focus the first element in tab order instead of "Confirm"-button by default.
        /// </summary>
        public bool? FocusConfirm { get; set; }

        /// <summary>
        /// Set to true if you want to focus the "Cancel"-button by default.
        /// </summary>
        public bool? FocusCancel { get; set; }

        /// <summary>
        /// Set to true to show close button in top right corner of the modal.
        /// </summary>
        public bool? ShowCloseButton { get; set; }

        /// <summary>
        /// Use this to change the content of the close button.
        /// </summary>
        public string CloseButtonHtml { get; set; }

        /// <summary>
        /// Use this to change the `aria-label` for the close button.
        /// </summary>
        public string CloseButtonAriaLabel { get; set; }

        /// <summary>
        /// Set to true to disable buttons and show that something is loading. Useful for AJAX requests.
        /// </summary>
        public bool? ShowLoaderOnConfirm { get; set; }

        /// <summary>
        /// Function to execute before confirm.
        /// <para>Receives the value from the request, and returns a new value for the request.</para>
        /// </summary>
        public PreConfirmCallback PreConfirm { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
        /// <summary>
        /// Add a customized icon for the modal. Should contain a string with the path or URL to the image.
        /// </summary>
        public string ImageUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        /// <summary>
        /// If imageUrl is set, you can specify imageWidth to describes image width in px.
        /// </summary>
        public double? ImageWidth { get; set; }

        /// <summary>
        /// If imageUrl is set, you can specify imageHeight to describes image height in px.
        /// </summary>
        public double? ImageHeight { get; set; }

        /// <summary>
        /// An alternative text for the custom image icon.
        /// </summary>
        public string ImageAlt { get; set; }

        /// <summary>
        /// Input field placeholder.
        /// </summary>
        public string InputPlaceholder { get; set; }

        /// <summary>
        /// Input field initial value.
        /// </summary>
        public string InputValue { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
        /// <summary>
        /// If input parameter is set to "select" or "radio", you can provide options.
        /// <para>Object keys will represent options values, object values will represent options text values.</para>
        /// </summary>
        public IDictionary<string, string> InputOptions { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Automatically remove whitespaces from both ends of a result string.
        /// <para>Set this parameter to false to disable auto-trimming.</para>
        /// </summary>
        public bool? InputAutoTrim { get; set; }

        //TODO: Input Attributes POCO
#pragma warning disable CA2227 // Collection properties should be read only
        /// <summary>
        /// HTML input attributes (e.g. min, max, step, accept...), that are added to the input field.
        /// </summary>
        public IDictionary<string, string> InputAttributes { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Validator for input field.
        /// </summary>
        public InputValidatorCallback InputValidator { get; set; }

        /// <summary>
        /// A custom validation message for default validators (email, url).
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Progress steps, useful for modal queues, see usage example.
        /// </summary>
        public IEnumerable<string> ProgressSteps { get; set; }

        /// <summary>
        /// Current active progress step.
        /// </summary>
        public string CurrentProgressStep { get; set; }

        /// <summary>
        /// Distance between progress steps.
        /// </summary>
        public string ProgressStepsDistance { get; set; }

        /// <summary>
        /// Function to run when modal built, but not shown yet. Provides modal DOM element as the first argument.
        /// </summary>
        public SweetAlertCallback OnBeforeOpen { get; set; }

        /// <summary>
        /// Function to run after modal has been disposed.
        /// </summary>
        public SweetAlertCallback OnAfterClose { get; set; }

        /// <summary>
        /// Function to run when modal opens, provides modal DOM element as the first argument.
        /// </summary>
        public SweetAlertCallback OnOpen { get; set; }

        /// <summary>
        /// Function to run when modal closes, provides modal DOM element as the first argument.
        /// </summary>
        public SweetAlertCallback OnClose { get; set; }

        /// <summary>
        /// Function to run after modal DOM has been updated.
        /// <para>Typically, this will happen after Swal.FireAsync() or Swal.UpdateAsync().</para>
        /// <para>If you want to perform changes in the modal's DOM, that survive Swal.UpdateAsync(), onRender is a good place for that.</para>
        /// </summary>
        public SweetAlertCallback OnRender { get; set; }

        /// <summary>
        /// Set to false to disable body padding adjustment when scrollbar is present.
        /// </summary>
        public bool? ScrollbarPadding { get; set; }

        public SweetAlertOptions()
        {

        }

        public SweetAlertOptions(string title)
        {
            this.Title = title;
        }

        internal SweetAlertOptionPOCO ToPOCO()
        {
            return new SweetAlertOptionPOCO
            {
                Title = this.Title,
                TitleText = this.TitleText,
                Text = this.Text,
                Html = this.Html,
                Footer = this.Footer,
                Icon = this.Icon?.ToString(),
                Backdrop = this.Backdrop,
                Toast = this.Toast,
                Target = this.Target,
                Input = this.Input?.ToString(),
                Width = this.Width,
                Padding = this.Padding,
                Background = this.Background,
                Position = this.Position?.ToString(),
                Grow = this.Grow?.ToString(),
                ShowClass = this.ShowClass,
                HideClass = this.HideClass,
                CustomClass = this.CustomClass,
                Timer = this.Timer,
                HeightAuto = this.HeightAuto,
                AllowOutsideClick = this.AllowOutsideClick,
                AllowEscapeKey = this.AllowEscapeKey,
                AllowEnterKey = this.AllowEnterKey,
                StopKeydownPropagation = this.StopKeydownPropagation,
                KeydownListenerCapture = this.KeydownListenerCapture,
                ShowConfirmButton = this.ShowConfirmButton,
                ShowCancelButton = this.ShowCancelButton,
                ConfirmButtonText = this.ConfirmButtonText,
                CancelButtonText = this.CancelButtonText,
                ConfirmButtonColor = this.ConfirmButtonColor,
                CancelButtonColor = this.CancelButtonColor,
                ConfirmButtonAriaLabel = this.ConfirmButtonAriaLabel,
                CancelButtonAriaLabel = this.CancelButtonAriaLabel,
                ButtonsStyling = this.ButtonsStyling,
                ReverseButtons = this.ReverseButtons,
                FocusConfirm = this.FocusConfirm,
                FocusCancel = this.FocusCancel,
                ShowCloseButton = this.ShowCloseButton,
                CloseButtonHtml = this.CloseButtonHtml,
                CloseButtonAriaLabel = this.CloseButtonAriaLabel,
                ShowLoaderOnConfirm = this.ShowLoaderOnConfirm,
                PreConfirm = this.PreConfirm != null,
                ImageUrl = this.ImageUrl,
                ImageWidth = this.ImageWidth,
                ImageHeight = this.ImageHeight,
                ImageAlt = this.ImageAlt,
                InputPlaceholder = this.InputPlaceholder,
                InputValue = this.InputValue,
                InputOptions = this.InputOptions,
                InputAutoTrim = this.InputAutoTrim,
                InputAttributes = this.InputAttributes,
                InputValidator = this.InputValidator != null,
                ValidationMessage = this.ValidationMessage,
                ProgressSteps = this.ProgressSteps,
                CurrentProgressStep = this.CurrentProgressStep,
                ProgressStepsDistance = this.ProgressStepsDistance,
                OnBeforeOpen = this.OnBeforeOpen != null,
                OnAfterClose = this.OnAfterClose != null,
                OnOpen = this.OnOpen != null,
                OnClose = this.OnClose != null,
                OnRender = this.OnRender != null,
                ScrollbarPadding = this.ScrollbarPadding,
            };
        }
    }
}