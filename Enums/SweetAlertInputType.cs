namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Collections.Generic;

    public sealed class SweetAlertInputType
    {
        private static readonly Dictionary<string, SweetAlertInputType> Instance =
            new Dictionary<string, SweetAlertInputType>();

        private readonly string name;

        public SweetAlertInputType(string name)
        {
            this.name = name;
            Instance[name] = this;
        }

        public static implicit operator SweetAlertInputType(string str)
        {
            return FromString(str);
        }

        public static SweetAlertInputType FromString(string str)
        {
            if (Instance.TryGetValue(str, out SweetAlertInputType result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"{nameof(SweetAlertInputType)} must be \"${Text}\", \"{Email}\", \"{Password}\", \"{Number}\", \"{Tel}\", \"{Range}\", \"{Textarea}\", \"{Select}\", \"{Radio}\", \"{Checkbox}\", \"{Url}\", or \"{File}\""); ;
            }
        }

        public override string ToString()
        {
            return this.name;
        }

        public static readonly SweetAlertInputType Text = new SweetAlertInputType("text");
        public static readonly SweetAlertInputType Email = new SweetAlertInputType("email");
        public static readonly SweetAlertInputType Password = new SweetAlertInputType("password");
        public static readonly SweetAlertInputType Number = new SweetAlertInputType("number");
        public static readonly SweetAlertInputType Tel = new SweetAlertInputType("tel");
        public static readonly SweetAlertInputType Range = new SweetAlertInputType("range");
        public static readonly SweetAlertInputType Textarea = new SweetAlertInputType("textarea");
        public static readonly SweetAlertInputType Select = new SweetAlertInputType("select");
        public static readonly SweetAlertInputType Radio = new SweetAlertInputType("radio");
        public static readonly SweetAlertInputType Checkbox = new SweetAlertInputType("checkbox");
        public static readonly SweetAlertInputType Url = new SweetAlertInputType("url");
        internal static readonly SweetAlertInputType File = new SweetAlertInputType("file");
    }
}
