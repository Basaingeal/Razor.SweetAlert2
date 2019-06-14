namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Collections.Generic;

    public sealed class SweetAlertType
    {
        private static readonly Dictionary<string, SweetAlertType> Instance =
            new Dictionary<string, SweetAlertType>();

        private readonly string name;

        private SweetAlertType(string name)
        {
            this.name = name;
            Instance[name] = this;
        }

        public static implicit operator SweetAlertType(string str)
        {
            if (Instance.TryGetValue(str, out SweetAlertType result))
            {
                return result;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public override string ToString()
        {
            return this.name;
        }

        public static readonly SweetAlertType Success = new SweetAlertType("success");
        public static readonly SweetAlertType Error = new SweetAlertType("error");
        public static readonly SweetAlertType Warning = new SweetAlertType("warning");
        public static readonly SweetAlertType Info = new SweetAlertType("info");
        public static readonly SweetAlertType Question = new SweetAlertType("question");
    }
}
