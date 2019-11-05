namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System.Collections.Generic;

    internal class SweetAlertOptionPOCO
    {
        public string Title { get; set; }

        public string TitleText { get; set; }

        public string Text { get; set; }

        public string Html { get; set; }

        public string Footer { get; set; }

        public string Icon { get; set; }

        public string IconHtml { get; set; }

        public bool? Backdrop { get; set; }

        public bool? Toast { get; set; }

        public string Target { get; set; }

        public string Input { get; set; }

        public string Width { get; set; }

        public string Padding { get; set; }

        public string Background { get; set; }

        public string Position { get; set; }

        public string Grow { get; set; }

        public SweetAlertShowClass ShowClass { get; set; }

        public SweetAlertHideClass HideClass { get; set; }

        public SweetAlertCustomClass CustomClass { get; set; }

        public decimal? Timer { get; set; }

        public bool? HeightAuto { get; set; }

        public bool? AllowOutsideClick { get; set; }

        public bool? AllowEscapeKey { get; set; }

        public bool? AllowEnterKey { get; set; }

        public bool? StopKeydownPropagation { get; set; }

        public bool? KeydownListenerCapture { get; set; }

        public bool? ShowConfirmButton { get; set; }

        public bool? ShowCancelButton { get; set; }

        public string ConfirmButtonText { get; set; }

        public string CancelButtonText { get; set; }

        public string ConfirmButtonColor { get; set; }

        public string CancelButtonColor { get; set; }

        public string ConfirmButtonAriaLabel { get; set; }

        public string CancelButtonAriaLabel { get; set; }

        public bool? ButtonsStyling { get; set; }

        public bool? ReverseButtons { get; set; }

        public bool? FocusConfirm { get; set; }

        public bool? FocusCancel { get; set; }

        public bool? ShowCloseButton { get; set; }

        public string CloseButtonHtml { get; set; }

        public string CloseButtonAriaLabel { get; set; }

        public bool? ShowLoaderOnConfirm { get; set; }

        public bool PreConfirm { get; set; }

        public string ImageUrl { get; set; }

        public double? ImageWidth { get; set; }

        public double? ImageHeight { get; set; }

        public string ImageAlt { get; set; }

        public string InputPlaceholder { get; set; }

        public string InputValue { get; set; }

        public IDictionary<string, string> InputOptions { get; set; }

        public bool? InputAutoTrim { get; set; }

        public IDictionary<string, string> InputAttributes { get; set; }

        public bool InputValidator { get; set; }

        public string ValidationMessage { get; set; }

        public IEnumerable<string> ProgressSteps { get; set; }

        public string CurrentProgressStep { get; set; }

        public string ProgressStepsDistance { get; set; }

        public bool OnBeforeOpen { get; set; }

        public bool OnAfterClose { get; set; }

        public bool OnOpen { get; set; }

        public bool OnClose { get; set; }

        public bool OnRender { get; set; }

        public bool? ScrollbarPadding { get; set; }
    }
}
