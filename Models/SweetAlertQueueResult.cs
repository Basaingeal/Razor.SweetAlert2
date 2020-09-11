using System.Collections.Generic;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    public class SweetAlertQueueResult
    {
        public IEnumerable<string> Value { get; set; }

        public DismissReason? Dismiss { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsDenied { get; set; }

        public bool IsDismissed { get; set; }
    }
}
