using System;
using System.Collections.Generic;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    public sealed class SweetAlertGrowDirection
    {
        private static readonly Dictionary<string, SweetAlertGrowDirection> Instance =
            new Dictionary<string, SweetAlertGrowDirection>();

        public static readonly SweetAlertGrowDirection Row = new SweetAlertGrowDirection("row");
        public static readonly SweetAlertGrowDirection Column = new SweetAlertGrowDirection("column");
        public static readonly SweetAlertGrowDirection Fullscreen = new SweetAlertGrowDirection("fullscreen");
        public static readonly SweetAlertGrowDirection False = new SweetAlertGrowDirection("false");

        private readonly string _name;

        public SweetAlertGrowDirection(string name)
        {
            _name = name;
            Instance[_name] = this;
        }

        public static implicit operator SweetAlertGrowDirection(string str)
        {
            return FromString(str);
        }

        public static SweetAlertGrowDirection FromString(string str)
        {
            if (Instance.TryGetValue(str, out var result))
                return result;
            throw new ArgumentException(
                $"{nameof(SweetAlertGrowDirection)} must be \"{Row}\", \"{Column}\", \"{Fullscreen}\", or {False}");
        }

        public static implicit operator SweetAlertGrowDirection(bool boolean)
        {
            return FromBoolean(boolean);
        }

        public static SweetAlertGrowDirection FromBoolean(bool boolean)
        {
            if (boolean)
                throw new ArgumentException("SweetAlertGrowDirection cannot be true.");
            return False;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}