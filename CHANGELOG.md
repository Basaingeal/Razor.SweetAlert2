# v3.0.0

## 🔴 Breaking Changes

In this major release, no breaking changes have been made in the API surface. However, the underlying SweetAlert2 library has changed the HTML structure of the loader.

> For the sake of customization, the loader has been separated from the confirm button into its own DOM element (`<div class="swal2-loader"></div>`).
>
> As a part of that breaking change, there's a new API param `loaderHtml` which allow to set custom HTML, e.g. SVG icon inside of the loader.
>
> [sweetalert2 v10.0.0 release](https://github.com/sweetalert2/sweetalert2/releases/tag/v10.0.0).

## 🎉 New Features

### The third DENY button

#### Additional `SweetAlertOptions` properties

- `ShowDenyButton`
- `DenyButtonText`
- `DenyButtonColor`
- `DenyButtonAriaLabel`
- `FocusDeny`
- `CustomerClass.DenyButton`

#### Additional `SweetAlertResult` and `SweetAlertQueueResult` properties

- `IsDenied`

#### Additional `SweetAlertService` methods

- `Swal.ClickDenyAsync()`

**NB**: There is no `Swal.GetDenyButtonAsync()` method as methods which return HTMLElements are not included in the wrapper library.

### Loader HTML

As part of the breaking change in `sweetalert2@10` there is a new options property to set custom HTML for the loader.

#### Additional `SweetlertOptions` properties

- `LoaderHtml`

### Icon Color

Introduced in [sweetalert2@10.1](https://github.com/sweetalert2/sweetalert2/releases/tag/v10.1.0), there is a new options property to set the color of the popup icon.

#### Additional `SweetlertOptions` properties

- `IconColor`

## Dependencies

- bump `sweetalert2` to `10.1.0`
- bump `@sweetalert2/themes` to `4.0.0`

For more information, read the [sweetalert2 v10.0.0 release](https://github.com/sweetalert2/sweetalert2/releases/tag/v10.0.0).
