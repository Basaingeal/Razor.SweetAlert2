using System;
using System.Collections.Generic;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    public sealed class SweetAlertIcon
    {
        private static readonly Dictionary<string, SweetAlertIcon> Instance =
            new Dictionary<string, SweetAlertIcon>();

        public static readonly SweetAlertIcon Success = new SweetAlertIcon("success");
        public static readonly SweetAlertIcon Error = new SweetAlertIcon("error");
        public static readonly SweetAlertIcon Warning = new SweetAlertIcon("warning");
        public static readonly SweetAlertIcon Info = new SweetAlertIcon("info");
        public static readonly SweetAlertIcon Question = new SweetAlertIcon("question");

        private readonly string _name;

        private SweetAlertIcon(string name)
        {
            _name = name;
            Instance[_name] = this;
        }

        public static implicit operator SweetAlertIcon(string str)
        {
            return FromString(str);
        }

        public static SweetAlertIcon FromString(string str)
        {
            if (Instance.TryGetValue(str, out var result))
                return result;
            throw new ArgumentException(
                $"{nameof(SweetAlertIcon)} must be \"{Success}\", \"{Error}\", \"{Warning}\", \"{Info}\", or \"{Question}.\"");
        }

        public override string ToString()
        {
            return _name;
        }
    }
}