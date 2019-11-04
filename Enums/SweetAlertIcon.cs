namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Collections.Generic;

    public sealed class SweetAlertIcon
    {
        private static readonly Dictionary<string, SweetAlertIcon> Instance =
            new Dictionary<string, SweetAlertIcon>();

        private readonly string name;

        private SweetAlertIcon(string name)
        {
            this.name = name;
            Instance[name] = this;
        }

        public static implicit operator SweetAlertIcon(string str)
        {
            return FromString(str);
        }

        public static SweetAlertIcon FromString(string str)
        {
            if (Instance.TryGetValue(str, out SweetAlertIcon result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"{nameof(SweetAlertIcon)} must be \"{Success}\", \"{Error}\", \"{Warning}\", \"{Info}\", or \"{Question}.\"");
            }
        }

        public override string ToString()
        {
            return this.name;
        }

        public static readonly SweetAlertIcon Success = new SweetAlertIcon("success");
        public static readonly SweetAlertIcon Error = new SweetAlertIcon("error");
        public static readonly SweetAlertIcon Warning = new SweetAlertIcon("warning");
        public static readonly SweetAlertIcon Info = new SweetAlertIcon("info");
        public static readonly SweetAlertIcon Question = new SweetAlertIcon("question");
    }
}
