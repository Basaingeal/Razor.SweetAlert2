namespace CurrieTechnologies.Razor.SweetAlert2
{
    public class SweetAlertCustomClass
    {
        /// <summary>
        /// Applies classnames to the `container-class` field.
        /// </summary>
        public string Container { get; set; }

        /// <summary>
        /// Applies classnames to the `popup-class` field.
        /// </summary>
        public string Popup { get; set; }

        /// <summary>
        /// Applies classnames to the `header-class` field.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Applies classnames to the `title-class` field.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Applies classnames to the `close-button-class` field.
        /// </summary>
        public string CloseButton { get; set; }

        /// <summary>
        /// Applies classnames to the `icon-class` field.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Applies classnames to the `image-class` field.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Applies classnames to the `content-class` field.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Applies classnames to the `input-class` field.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Applies classnames to the `validation-message-class` field.
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Applies classnames to the `actions-class` field.
        /// </summary>
        public string Actions { get; set; }

        /// <summary>
        /// Applies classnames to the `confirm-button-class` field.
        /// </summary>
        public string ConfirmButton { get; set; }

        /// <summary>
        /// Applies classnames to the `deny-button-class` field.
        /// </summary>
        public string DenyButton { get; set; }

        /// <summary>
        /// Applies classnames to the `cancel-button-class` field.
        /// </summary>
        public string CancelButton { get; set; }

        /// <summary>
        /// Applies classnames to the `footer-class` field.
        /// </summary>
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
