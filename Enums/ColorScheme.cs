using System;
using System.Collections.Generic;
using System.Text;

namespace CurrieTechnologies.Razor.SweetAlert2
{
    /// <summary>
    /// The <code>prefers-color-scheme</code> CSS media feature is used to detect if the user has requested the system use a light or dark color theme.
    /// </summary>
    public enum ColorScheme
    {
        /// <summary>
        /// Indicates that the user has made no preference known to the system.
        /// </summary>
        NoPreference,

        /// <summary>
        /// Indicates that user has notified the system that they prefer an interface that has a light theme.
        /// </summary>
        Light,

        /// <summary>
        /// Indicates that user has notified the system that they prefer an interface that has a dark theme.
        /// </summary>
        Dark
    }
}
