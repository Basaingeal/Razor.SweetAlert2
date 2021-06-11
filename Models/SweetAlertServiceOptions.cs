using System.Collections.Generic;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    /// <summary>
    ///     Options to configure the <see cref="SweetAlertService" /> at Startup.
    /// </summary>
    public class SweetAlertServiceOptions
    {
        /// <summary>
        ///     An official theme for SweetAlert2
        /// </summary>
        public SweetAlertTheme Theme { get; set; }

        internal Dictionary<ColorScheme, SweetAlertTheme> ColorSchemeThemes { get; } =
            new Dictionary<ColorScheme, SweetAlertTheme>();

        /// <summary>
        ///     Sets the SweetAlertTheme to be used with a given color scheme preference.
        ///     This utilizes the <code>prefers-color-scheme</code> CSS media feature, and behaves similarly.
        ///     Browsers that do not support the feature will fall back to the theme set in
        ///     <code>SweetAlertServiceOptions.Theme</code>.
        /// </summary>
        /// <param name="scheme">The user color scheme preference to apply this theme to</param>
        /// <param name="theme">The theme to use when the user has the provided color scheme preference</param>
        public void SetThemeForColorSchemePreference(ColorScheme scheme, SweetAlertTheme theme)
        {
            ColorSchemeThemes.Add(scheme, theme);
        }
    }
}