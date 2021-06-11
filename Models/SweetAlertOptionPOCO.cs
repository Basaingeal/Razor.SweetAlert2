using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    internal class SweetAlertOptionPOCO
    {
        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("titleText")] public string TitleText { get; set; }

        [JsonPropertyName("text")] public string Text { get; set; }

        [JsonPropertyName("html")] public string Html { get; set; }

        [JsonPropertyName("footer")] public string Footer { get; set; }

        [JsonPropertyName("icon")] public string Icon { get; set; }

        [JsonPropertyName("iconColor")] public string IconColor { get; set; }

        [JsonPropertyName("iconHtml")] public string IconHtml { get; set; }

        [JsonPropertyName("backdrop")] public bool? Backdrop { get; set; }

        [JsonPropertyName("toast")] public bool? Toast { get; set; }

        [JsonPropertyName("target")] public string Target { get; set; }

        [JsonPropertyName("input")] public string Input { get; set; }

        [JsonPropertyName("width")] public string Width { get; set; }

        [JsonPropertyName("padding")] public string Padding { get; set; }

        [JsonPropertyName("background")] public string Background { get; set; }

        [JsonPropertyName("position")] public string Position { get; set; }

        [JsonPropertyName("grow")] public string Grow { get; set; }

        [JsonPropertyName("showClass")] public SweetAlertShowClass ShowClass { get; set; }

        [JsonPropertyName("hideClass")] public SweetAlertHideClass HideClass { get; set; }

        [JsonPropertyName("customClass")] public SweetAlertCustomClass CustomClass { get; set; }

        [JsonPropertyName("timer")] public decimal? Timer { get; set; }

        [JsonPropertyName("timerProgressBar")] public bool? TimerProgressBar { get; set; }

        [JsonPropertyName("heightAuto")] public bool? HeightAuto { get; set; }

        [JsonPropertyName("allowOutsideClick")]
        public bool? AllowOutsideClick { get; set; }

        [JsonPropertyName("allowEscapeKey")] public bool? AllowEscapeKey { get; set; }

        [JsonPropertyName("allowEnterKey")] public bool? AllowEnterKey { get; set; }

        [JsonPropertyName("stopKeydownPropagation")]
        public bool? StopKeydownPropagation { get; set; }

        [JsonPropertyName("keydownListenerCapture")]
        public bool? KeydownListenerCapture { get; set; }

        [JsonPropertyName("showConfirmButton")]
        public bool? ShowConfirmButton { get; set; }

        [JsonPropertyName("showDenyButton")] public bool? ShowDenyButton { get; set; }

        [JsonPropertyName("showCancelButton")] public bool? ShowCancelButton { get; set; }

        [JsonPropertyName("confirmButtonText")]
        public string ConfirmButtonText { get; set; }

        [JsonPropertyName("denyButtonText")] public string DenyButtonText { get; set; }

        [JsonPropertyName("cancelButtonText")] public string CancelButtonText { get; set; }

        [JsonPropertyName("confirmButtonColor")]
        public string ConfirmButtonColor { get; set; }

        [JsonPropertyName("denyButtonColor")] public string DenyButtonColor { get; set; }

        [JsonPropertyName("cancelButtonColor")]
        public string CancelButtonColor { get; set; }

        [JsonPropertyName("confirmButtonAriaLabel")]
        public string ConfirmButtonAriaLabel { get; set; }

        [JsonPropertyName("denyButtonAriaLabel")]
        public string DenyButtonAriaLabel { get; set; }

        [JsonPropertyName("cancelButtonAriaLabel")]
        public string CancelButtonAriaLabel { get; set; }

        [JsonPropertyName("buttonsStyling")] public bool? ButtonsStyling { get; set; }

        [JsonPropertyName("reverseButtons")] public bool? ReverseButtons { get; set; }

        [JsonPropertyName("focusConfirm")] public bool? FocusConfirm { get; set; }

        [JsonPropertyName("focusDeny")] public bool? FocusDeny { get; set; }

        [JsonPropertyName("focusCancel")] public bool? FocusCancel { get; set; }

        [JsonPropertyName("returnFocus")] public bool? ReturnFocus { get; set; }

        [JsonPropertyName("showCloseButton")] public bool? ShowCloseButton { get; set; }

        [JsonPropertyName("closeButtonHtml")] public string CloseButtonHtml { get; set; }

        [JsonPropertyName("closeButtonAriaLabel")]
        public string CloseButtonAriaLabel { get; set; }

        [JsonPropertyName("loaderHtml")] public string LoaderHtml { get; set; }

        [JsonPropertyName("showLoaderOnConfirm")]
        public bool? ShowLoaderOnConfirm { get; set; }

        [JsonPropertyName("showLoaderOnDeny")] public bool? ShowLoaderOnDeny { get; set; }

        [JsonPropertyName("preConfirm")] public bool PreConfirm { get; set; }

        [JsonPropertyName("preDeny")] public bool PreDeny { get; set; }

        [JsonPropertyName("imageUrl")] public string ImageUrl { get; set; }

        [JsonPropertyName("imageWidth")] public double? ImageWidth { get; set; }

        [JsonPropertyName("imageHeight")] public double? ImageHeight { get; set; }

        [JsonPropertyName("imageAlt")] public string ImageAlt { get; set; }

        [JsonPropertyName("inputLabel")] public string InputLabel { get; set; }

        [JsonPropertyName("inputPlaceholder")] public string InputPlaceholder { get; set; }

        [JsonPropertyName("inputValue")] public string InputValue { get; set; }

        [JsonPropertyName("inputOptions")] public IDictionary<string, string> InputOptions { get; set; }

        [JsonPropertyName("inputAutoTrim")] public bool? InputAutoTrim { get; set; }

        [JsonPropertyName("inputAttributes")] public IDictionary<string, string> InputAttributes { get; set; }

        [JsonPropertyName("inputValidator")] public bool InputValidator { get; set; }

        [JsonPropertyName("returnInputValueOnDeny")]
        public bool? ReturnInputValueOnDeny { get; set; }

        [JsonPropertyName("validationMessage")]
        public string ValidationMessage { get; set; }

        [JsonPropertyName("progressSteps")] public IEnumerable<string> ProgressSteps { get; set; }

        [JsonPropertyName("currentProgressStep")]
        public string CurrentProgressStep { get; set; }

        [JsonPropertyName("progressStepsDistance")]
        public string ProgressStepsDistance { get; set; }

        [JsonPropertyName("willOpen")] public bool WillOpen { get; set; }

        [JsonPropertyName("didClose")] public bool DidClose { get; set; }

        [JsonPropertyName("didDestroy")] public bool DidDestroy { get; set; }

        [JsonPropertyName("didOpen")] public bool DidOpen { get; set; }

        [JsonPropertyName("willClose")] public bool WillClose { get; set; }

        [JsonPropertyName("didRender")] public bool DidRender { get; set; }

        [JsonPropertyName("scrollbarPadding")] public bool? ScrollbarPadding { get; set; }
    }
}