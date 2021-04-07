namespace CurrieTechnologies.Razor.SweetAlert2
{
    public class SweetAlertCustomClass
    {
        public string Container { get; set; }

        public string Popup { get; set; }

        public string Header { get; set; }

        public string Title { get; set; }

        public string CloseButton { get; set; }

        public string Icon { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public string HtmlContainer { get; set; }

        public string Input { get; set; }

        public string InputLabel { get; set; }

        public string ValidationMessage { get; set; }

        public string Actions { get; set; }

        public string ConfirmButton { get; set; }

        public string DenyButton { get; set; }

        public string CancelButton { get; set; }

        public string Loader { get; set; }

        public string Footer { get; set; }

        public SweetAlertCustomClass()
        {

        }

        public SweetAlertCustomClass(string popup)
        {
            this.Popup = popup;
        }
    }
}
