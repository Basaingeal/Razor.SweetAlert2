using System;
using System.Collections.Generic;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    public sealed class SweetAlertPosition
    {
        private static readonly Dictionary<string, SweetAlertPosition> Instance =
            new Dictionary<string, SweetAlertPosition>();

        public static readonly SweetAlertPosition Top = new SweetAlertPosition("top");
        public static readonly SweetAlertPosition TopStart = new SweetAlertPosition("top-start");
        public static readonly SweetAlertPosition TopEnd = new SweetAlertPosition("top-end");
        public static readonly SweetAlertPosition TopLeft = new SweetAlertPosition("top-left");
        public static readonly SweetAlertPosition TopRight = new SweetAlertPosition("top-right");
        public static readonly SweetAlertPosition Center = new SweetAlertPosition("center");
        public static readonly SweetAlertPosition CenterStart = new SweetAlertPosition("center-start");
        public static readonly SweetAlertPosition CenterEnd = new SweetAlertPosition("center-end");
        public static readonly SweetAlertPosition CenterLeft = new SweetAlertPosition("center-left");
        public static readonly SweetAlertPosition CenterRight = new SweetAlertPosition("center-right");
        public static readonly SweetAlertPosition Bottom = new SweetAlertPosition("bottom");
        public static readonly SweetAlertPosition BottomStart = new SweetAlertPosition("bottom-start");
        public static readonly SweetAlertPosition BottomEnd = new SweetAlertPosition("bottom-end");
        public static readonly SweetAlertPosition BottomLeft = new SweetAlertPosition("bottom-left");
        public static readonly SweetAlertPosition BottomRight = new SweetAlertPosition("bottom-right");

        private readonly string _name;

        private SweetAlertPosition(string name)
        {
            _name = name;
            Instance[_name] = this;
        }

        public static implicit operator SweetAlertPosition(string str)
        {
            return FromString(str);
        }

        public static SweetAlertPosition FromString(string str)
        {
            if (Instance.TryGetValue(str, out var result))
                return result;
            throw new ArgumentException(
                $"{nameof(SweetAlertPosition)} must be \"{Top}\", \"{TopStart}\", \"{TopEnd}\", \"{TopLeft}\", \"{TopRight}\", \"{Center}\", \"{CenterStart}\", \"{CenterEnd}\", \"{CenterLeft}\", \"{CenterRight}\", \"{Bottom}\", \"{BottomStart}\", \"{BottomEnd}\", \"{BottomLeft}\", or \"{BottomRight}.\"");
        }

        public override string ToString()
        {
            return _name;
        }
    }
}