namespace CurrieTechnologies.Razor.SweetAlert2
{
    public class SweetAlertResult
    {
        public string Value { get; set; }

        public DismissReason? Dismiss { get; set; }

        public bool IsConfirmed { get; set; }
        
        public bool IsDismissed { get; set; }
    }
}
