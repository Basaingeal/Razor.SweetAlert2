# CurrieTechnologies.Razor.SweetAlert2

|                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: |
| ![Blazor](BrandBlazor_nohalo_140x.png) <br/> ➕ <br/> ![SweetAlert2](https://raw.github.com/sweetalert2/sweetalert2/master/assets/swal2-logo.png) <br/> A beautiful, responsive, customizable, accessible (WAI-ARIA) replacement for JavaScript's popup boxes. <br/> All wrapped inside a Razor Component Library for use in Blazor Server and WebAssembly applications. <br/> ![SweetAlert2 in action](https://raw.github.com/sweetalert2/sweetalert2/master/assets/sweetalert2.gif) <br/> [See SweetAlert2 in action ↗](https://sweetalert2.github.io/) |

|                                                                                                                                  Master                                                                                                                                   |                                                                                                                                   Develop                                                                                                                                   |                                            Version                                             |                                   Downloads                                    |                                                                         Mergify                                                                         |
| :-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------------: | :----------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------------------------------------------------------: |
| [![Build Status](https://dev.azure.com/michaeljcurrie136/CurrieTechnologies.Blazor/_apis/build/status/Basaingeal.Razor.SweetAlert2?branchName=master)](https://dev.azure.com/michaeljcurrie136/CurrieTechnologies.Blazor/_build/latest?definitionId=16&branchName=master) | [![Build Status](https://dev.azure.com/michaeljcurrie136/CurrieTechnologies.Blazor/_apis/build/status/Basaingeal.Razor.SweetAlert2?branchName=develop)](https://dev.azure.com/michaeljcurrie136/CurrieTechnologies.Blazor/_build/latest?definitionId=16&branchName=develop) | ![Nuget](https://img.shields.io/nuget/v/CurrieTechnologies.Razor.SweetAlert2.svg?style=popout) | ![Nuget](https://img.shields.io/nuget/dt/CurrieTechnologies.Razor.SweetAlert2) | [![mergify-status](https://img.shields.io/endpoint.svg?url=https://gh.mergify.io/badges/Basaingeal/Razor.SweetAlert2&style=popout)](https://mergify.io) |

👉 **Upgrading from v3.x to v4.x?** [Read the release notes!](https://github.com/Basaingeal/Razor.SweetAlert2/releases/tag/v4.0.0)

👉 **Upgrading from v4.x to v5.x?** [Read the release notes!](https://github.com/Basaingeal/Razor.SweetAlert2/releases/tag/v5.0.0)

## This package is for both Blazor Server Apps and Blazor WebAssembly Apps. It should be used instead of [`CurrieTechnologies.Blazor.SweetAlert2`](https://github.com/Basaingeal/Blazor.SweetAlert2) which is now deprecated.

### 🙌 Includes themes from the [Official SweetAlert2 Themes project](https://github.com/sweetalert2/sweetalert2-themes) 🙌

## Installation

```sh
Install-Package CurrieTechnologies.Razor.SweetAlert2
```

Or install from the NuGet Package Manager

## Usage

### Register the service in your Startup file.

```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
...
 services.AddSweetAlert2();
...
}
```

**NB:** For newer .NET 6 minimal project templates, this will actually be in `Program.cs`

```csharp
// Program.cs
...
builder.Services.AddSweetAlert2();
```

### OR

If you want to use one of the Official SweetAlert2 themes:

```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
...
 services.AddSweetAlert2(options => {
   options.Theme = SweetAlertTheme.Dark;
 });
...
}
```

_See [Configuration](#configuration) for more information._

### Add the script tag

Add this script tag in your root html file (Likely \_Host.cshtml for Blazor Server or index.html for Blazor WebAssembly), right under the framework script tag. (i.e `<script src="_framework/blazor.server.js"></script>` for Blazor Server or `<script src="_framework/blazor.webassembly.js"></script>` for Blazor WebAssembly)

**NB:** In newer templates, this will be towards the bottom of `Pages/_Layout.cshtml` for Blazor Server or `wwwroot/index.html` for Blazor WebAssembly.

```html
<script src="_content/CurrieTechnologies.Razor.SweetAlert2/sweetAlert2.min.js"></script>
```

**If you need to support IE11, this script tag is different. See [IE Compatibility](#ie-compatibility).**

### \_Imports.razor

_Recommended:_ Add `@using CurrieTechnologies.Razor.SweetAlert2` to your `_Imports.razor` file, to avoid having to put the using call in every component that requires it.

### Inject the SweetAlertService into any Blazor component.

```razor
// Sample.razor
@inject SweetAlertService Swal;
<button class="btn btn-primary"
   @onclick="@(async () => await Swal.FireAsync("Any fool can use a computer"))">
 Try me!
</button>
```

## Examples

The most basic message:

```csharp
await Swal.FireAsync("Hello world!");
```

A message signaling an error:

```csharp
await Swal.FireAsync("Oops...", "Something went wrong!", "error");
```

Handling the result of SweetAlert2 modal:

```csharp
// async/await
SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
 {
   Title = "Are you sure?",
   Text = "You will not be able to recover this imaginary file!",
   Icon = SweetAlertIcon.Warning,
   ShowCancelButton = true,
   ConfirmButtonText = "Yes, delete it!",
   CancelButtonText = "No, keep it"
 });

if (!string.IsNullOrEmpty(result.Value))
{
 await Swal.FireAsync(
   "Deleted",
   "Your imaginary file has been deleted.",
   SweetAlertIcon.Success
   );
}
else if (result.Dismiss == DismissReason.Cancel)
{
 await Swal.FireAsync(
   "Cancelled",
   "Your imaginary file is safe :)",
   SweetAlertIcon.Error
   );
}

// Promise/Task based
Swal.FireAsync(new SweetAlertOptions
 {
   Title = "Are you sure?",
   Text = "You will not be able to recover this imaginary file!",
   Icon = SweetAlertIcon.Warning,
   ShowCancelButton = true,
   ConfirmButtonText = "Yes, delete it!",
   CancelButtonText = "No, keep it"
 }).ContinueWith(swalTask =>
 {
   SweetAlertResult result = swalTask.Result;
   if (!string.IsNullOrEmpty(result.Value))
   {
     Swal.FireAsync(
       "Deleted",
       "Your imaginary file has been deleted.",
       SweetAlertIcon.Success
       );
   }
   else if (result.Dismiss == DismissReason.Cancel)
   {
     Swal.FireAsync(
       "Cancelled",
       "Your imaginary file is safe :)",
       SweetAlertIcon.Error
       );
   }
 });
```

### [More examples can be found on the SweetAlert2 project site](https://sweetalert2.github.io/)

## Configuration

In `Startup.cs` you have the opportunity to configure how `sweetalert2` will behave in your application.

### Theme

With `SweetAlertServiceOptions.Theme` you can specify one of the official sweetalert2 themes to apply to your modal throughout your application.

#### SetThemeForColorSchemePreference()

With the `SweetAlertServiceOptions.SetThemeForColorSchemePreference()` method, you can specify which theme the user uses, based on the result of their `prefers-color-scheme` CSS media query. Most commonly this can be used to help create a dark version of your application, based on user preference. Browsers that do not support the `prefers-color-scheme` media query will fall back to the theme specified in `SweetAlertServiceOptions.Theme`

#### Theme Examples

If you want the default theme by default, and the dark theme if the user prefers a dark color scheme:

```csharp
services.AddSweetAlert2(options => {
 options.SetThemeForColorSchemePreference(ColorScheme.Dark, SweetAlertTheme.Dark);
});
```

A dark theme by default, and a lighter theme if the user prefers a light color scheme:

```csharp
services.AddSweetAlert2(options => {
 options.Theme = SweetAlertTheme.Dark;
 options.SetThemeForColorSchemePreference(ColorScheme.Light, SweetAlertTheme.Bootstrap4);
});
```

A minimal theme as a fallback, and a dark/light theme to match user preference:

```csharp
services.AddSweetAlert2(options => {
 options.Theme = SweetAlertTheme.Minimal;
 options.SetThemeForColorSchemePreference(ColorScheme.Light, SweetAlertTheme.Default);
 options.SetThemeForColorSchemePreference(ColorScheme.Dark, SweetAlertTheme.Dark);
});
```

_See [prefers-color-scheme](https://developer.mozilla.org/en-US/docs/Web/CSS/@media/prefers-color-scheme) for more information._

### Default Settings

If you want some settings globally applied to all of your SweetAlert2 dialogs, configure your default settings in `Startup.cs`

```csharp
services.AddSweetAlert2(options => {
 options.DefaultOptions = new SweetAlertOptions {
   HeightAuto = false
 };
});
```

These can be overriden in individual `FireAsync()` calls.

**NB:** This will only apply to `FireAsync()` calls that take a `SweetAlertOptions` object as a parameter. The methods that take in primitive types bypass `SweetAlertOptions` entirely on both the C# and JS libraries.

## Notable differences from the JavaScript library

- No methods that return an HTMLElement are included (e. g. `Swal.getHtmlContainer()`)
- The value of a `SweetAlertResult` (`result.Value`) can only be a string. Numbers and booleans must be converted. Object must be parsed to/from JSON in your code.
- `DidOpenAsync()`, `WillCloseAsync()`, `WillOpenAsync()`, and `DidCloseAsync()` can all take asynchronous callbacks. 🎉 (none will return an HTMLElement though.)
- No support for `<optgroup>` in the select input type.
- No async option for `InputOptions` or `InputValue`
- Callbacks must be passed inside of objects specifically designed for the given callback property. e.g. the `InputValidator` property takes an `InputValidatorCallback` created like so:

```csharp
new SweetAlertOptions {
 ...
 InputValidator = new InputValidatorCallback((string input) => input.Length == 0 ? "Please provide a value" : null, this),
 ...
}
```

`this` is passed in so that the Blazor `EventCallback` used behind the scenes can trigger a re-render if the state of the calling component was changed in the callback. If the callback does not require the calling component to re-render, passing in `this` is optional.
These callbacks are necessary because there is currently no way to create an `EventCallback` in Blazor that isn't a component parameter without using the `EventCallbackFactory` which is clunky. It also allows the callback to return a value that can be used by the SweetAlert2 library. (e.g. A validation message to show if input validation fails.) Native Blazor `EventCallback`s only return generic `Task`s.

## Browser compatibility

| IE11\* | Edge | Chrome | Firefox | Safari | Opera | UC Browser |
| ------ | ---- | ------ | ------- | ------ | ----- | ---------- |
| ❌     | ✔    | ✔      | ✔       | ✔      | ✔     | ✔          |

### IE Compatibility\*

IE Compatibility has been removed in `v5` due to `sweetalert2@11` dropping their support for it.

If you need to support IE11, use `v4` or earlier, and use this script tag instead. (file size is about 35% larger)

```html
<script src="_content/CurrieTechnologies.Razor.SweetAlert2/sweetAlert2.ieCompat.min.js"></script>
```

You will also likely need to utilize the [Blazor.Polyfill](https://github.com/Daddoon/Blazor.Polyfill) library, for general Blazor functionality in IE.

## Protestware

Currently, the original `sweetalert2` library contains protestware to disable Russian websites using `sweetalert2` when visited by Russian-speaking users. This wrapper library bypasses the effects of that protestware, so there should be no undesired effects when using this library.

## Related projects

- [SweetAlert2](https://sweetalert2.github.io/) - Original SweetAlert2 project
