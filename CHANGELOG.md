# v1.1.0

## Enhancements

* New Method: `SweetAlertServiceOptions.SetThemeForColorSchemePreference()` (https://github.com/Basaingeal/Razor.SweetAlert2/issues/189) (https://github.com/Basaingeal/Razor.SweetAlert2/issues/189)
  * When setting up the `SweetAlertService` in `Startup.cs`, you can now map themes to the results of a user's `prefers-color-scheme` CSS media query.
  * This means that you can now use the dark theme only when the user prefers dark them (via their OS, Android 10, iOS 13 ect.)
  * If the user's browser doesn't support `prefers-color-scheme`, the theme specified with `SweetAlertServiceOptions.SetThemeForColorSchemePreference()` will be used
  * For more information, see [here](README.md#setthemeforcolorschemepreference).
  * Thanks to [@UweKeim](https://github.com/UweKeim) for the issue and suggestions on implementation.
* New script for IE Compatibility
  * This is technically a breaking change because the original script no longer works the same with IE. I'm not bumping the major version because the APIs haven't changed, and it was impossible to use this library with IE11 before this change without using a separate polyfill.
  * The original script is slightly leaner than it used to be, and actually uses ES6+ syntax.
  * The new ieCompat script, is larger, but contains the polyfills needed for this library to work consistently.
  * Note: [Blazor.Polyfill](https://github.com/Daddoon/Blazor.Polyfill) library is still needed for general Blazor in IE functionality.
  * For more information, see [here](README.md#ie-compatibility).
  