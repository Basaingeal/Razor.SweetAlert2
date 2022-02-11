using System.Text.Json.Serialization;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    public class SweetAlertCustomClass
    {
        public SweetAlertCustomClass()
        {
        }

        public SweetAlertCustomClass(string popup)
        {
            Popup = popup;
        }

        [JsonPropertyName("container")] public string Container { get; set; }

        [JsonPropertyName("popup")] public string Popup { get; set; }

        [JsonPropertyName("header")] public string Header { get; set; }

        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("closeButton")] public string CloseButton { get; set; }

        [JsonPropertyName("icon")] public string Icon { get; set; }

        [JsonPropertyName("image")] public string Image { get; set; }

        [JsonPropertyName("content")] public string Content { get; set; }

        [JsonPropertyName("htmlContainer")] public string HtmlContainer { get; set; }

        [JsonPropertyName("input")] public string Input { get; set; }

        [JsonPropertyName("inputLabel")] public string InputLabel { get; set; }

        [JsonPropertyName("validationMessage")]
        public string ValidationMessage { get; set; }

        [JsonPropertyName("actions")] public string Actions { get; set; }

        [JsonPropertyName("confirmButton")] public string ConfirmButton { get; set; }

        [JsonPropertyName("denyButton")] public string DenyButton { get; set; }

        [JsonPropertyName("cancelButton")] public string CancelButton { get; set; }

        [JsonPropertyName("loader")] public string Loader { get; set; }

        [JsonPropertyName("footer")] public string Footer { get; set; }

        [JsonPropertyName("timerProgressBar")] public string TimerProgressBar { get; set; }
    }
}