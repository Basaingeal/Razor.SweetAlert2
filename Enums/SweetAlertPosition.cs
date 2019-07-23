namespace CurrieTechnologies.Razor.SweetAlert2
{
    using System;
    using System.Collections.Generic;

    public sealed class SweetAlertPosition
    {
        private static readonly Dictionary<string, SweetAlertPosition> Instance =
            new Dictionary<string, SweetAlertPosition>();

        private readonly string name;

        private SweetAlertPosition(string name)
        {
            this.name = name;
            Instance[name] = this;
        }

        public static implicit operator SweetAlertPosition(string str)
        {
            return FromString(str);
        }

        public static SweetAlertPosition FromString(string str)
        {
            if (Instance.TryGetValue(str, out SweetAlertPosition result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"{nameof(SweetAlertPosition)} must be \"{Top}\", \"{TopStart}\", \"{TopEnd}\", \"{TopLeft}\", \"{TopRight}\", \"{Center}\", \"{CenterStart}\", \"{CenterEnd}\", \"{CenterLeft}\", \"{CenterRight}\", \"{Bottom}\", \"{BottomStart}\", \"{BottomEnd}\", \"{BottomLeft}\", or \"{BottomRight}.\"");
            }
        }

        public override string ToString()
        {
            return this.name;
        }

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
    }
}
