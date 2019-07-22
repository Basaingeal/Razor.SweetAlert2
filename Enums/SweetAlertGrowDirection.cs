namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Collections.Generic;

    public sealed class SweetAlertGrowDirection
    {
        private static readonly Dictionary<string, SweetAlertGrowDirection> Instance =
            new Dictionary<string, SweetAlertGrowDirection>();

        private readonly string name;

        public SweetAlertGrowDirection(string name)
        {
            this.name = name;
            Instance[name] = this;
        }

        public static implicit operator SweetAlertGrowDirection(string str)
        {
            return FromString(str);
        }

        public static SweetAlertGrowDirection FromString(string str)
        {
            if (Instance.TryGetValue(str, out SweetAlertGrowDirection result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"{nameof(SweetAlertGrowDirection)} must be \"{Row}\", \"{Column}\", \"{Fullscreen}\", or {False}");
            }
        }

        public static implicit operator SweetAlertGrowDirection(bool boolean)
        {
            return FromBoolean(boolean);
        }

        public static SweetAlertGrowDirection FromBoolean(bool boolean)
        {
            if (boolean)
            {
                throw new ArgumentException("SweetAlertGrowDirection cannot be true.");
            }
            else
            {
                return False;
            }
        }

        public override string ToString()
        {
            return this.name;
        }

        public static readonly SweetAlertGrowDirection Row = new SweetAlertGrowDirection("row");
        public static readonly SweetAlertGrowDirection Column = new SweetAlertGrowDirection("column");
        public static readonly SweetAlertGrowDirection Fullscreen = new SweetAlertGrowDirection("fullscreen");
        public static readonly SweetAlertGrowDirection False = new SweetAlertGrowDirection("false");
    }
}
